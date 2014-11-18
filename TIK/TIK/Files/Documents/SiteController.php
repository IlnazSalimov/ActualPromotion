<?php

class SiteController extends Controller
{
	/**
	 * Declares class-based actions.
	 */
	public function actions()
	{
		return array(
			// captcha action renders the CAPTCHA image displayed on the contact page
			'captcha'=>array(
				'class'=>'CCaptchaAction',
				'backColor'=>0xFFFFFF,
			),
		);
	}
	
	public function actionHotels($hotel)
	{
		$this->cs->registerCssFile($this->cssPath.'/tours.css');
		if($hotel==0)
		{
			$hotelsRecords = Hotel::model()->findAll('status=:status',array(':status'=>'work'));
			$hotels = array();
	        foreach($hotelsRecords as $hotel)
	        {
			  	$hotels[]=$hotel;
	        }
	        $page = Page::model()->find("code=:code",array(':code'=>'hotels'));
	        $this->render('hotels',array('hotels'=>$hotels,'page'=>$page));
		}
		else
		{
			$hotel = Hotel::model()->findByPk($hotel);
			
			$this->render('hotel',array('hotel'=>$hotel) );
		}
		

	}

    public function actionNewslist()
    {
        $criteria = new CDbCriteria();
        $criteria->order = /*'substr(created,7,4)||substr(created,4,2)||substr(created,1,2) desc'*/'id desc';
        $criteria->condition = 'hide != 1';
        $news = News::model()->findAll($criteria);

        foreach($news as $new)
        {
            if($new->preview)
            {
                $image = Yii::app()->image->load('uploads/news/preview/'.$new->preview);
                if($image->width!=200)
                {
                    $image->resize(200, 200, Image::WIDTH);
                    $image->save();
                }
            }
        }

        $this->pageTitle = Yii::t('site','News');

        $this->render('news',array('items'=>$news) );
    }

    public function actionNews($code)
    {
        $news = News::model()->find('code=:code AND hide!=1',array(':code'=>$code));
        if($news==null)
        {
            $this->throw404();
        }
        else
        {
            $news->gallery_id = Gallery::model()->findByPk($news->gallery_id);
            $this->pageTitle = Yii::t('site','News').' - '.($news->title?$news->title:$news->name);
            $this->render('new',array('data'=>$news));
        }
    }

    /**
     * This is the action for reset user password
     */
    public function actionRecovery($key)
    {
        $model = new RecoveryPasswordForm();

        $user = User::model()->find('key=:key',array(
            ':key' => $key
        ));

        if($user==null)
        {
            Yii::app()->user->setFlash('error', 'Срок действия ссылки истек' );
        }
        else
        {
            $user->key = '';

            $length = 10;
            $chars = array_merge(range(0,9), range('a','z'), range('A','Z'));
            shuffle($chars);
            $password = implode(array_slice($chars, 0, $length));
            $user->password = $password;
            $user->save();



            $mail = new YiiMailer('register', array(
                'name'=> $user->firstName.' '.$user->lastName.' '.$user->middleName,
                'mail' => $user->email,
                'password' => $password,
                'title' => 'Ваш новый пароль для входа на сайт сети отелей «Регина»'
            ) );

            $mail->setTo($user->email);


            $mail->setSubject('Ваш новый пароль для входа на сайт сети отелей «Регина»');

            $mail->setFrom('robot@'.str_replace('http://','',Yii::app()->request->hostInfo), Yii::t('YiiMailer','Robot informer'));

            if ($mail->send())
            {
                Yii::app()->user->setFlash('success',  'Ваш новый пароль для входа в кабинет был отправлен вам на E-mail');
            }
            else
            {
                Yii::app()->user->setFlash('error', Yii::t('YiiMailer','Error while sending email: ').$mail->getError());
            }
        }

        $this->redirect('/');
    }

    /**
     * This is the action for add to basket
     */
    public function actionBasket()
    {
        Yii::app()->session['var'];
    }

    /**
     * This is the action of form recovery password
     */
    public function actionForgot()
    {
        $this->pageTitle = 'Восстановление пароля';
        $model = new RecoveryPasswordForm();
        if(isset($_POST['RecoveryPasswordForm']))
        {
            $model->attributes = $_POST['RecoveryPasswordForm'];
            if($model->validate())
            {
                $user = User::model()->find('email=:email',array(':email'=>$model->email));
                if($user == null)
                {
                    Yii::app()->user->setFlash('error', 'Пользователь с таким e-mail не найден' );
                }
                else
                {

                    $user->key = md5(rand(0,1000).$user->email);
                    $user->save();

                    $url = Yii::app()->request->hostInfo.'/recovery/key/'.$user->key.'/';

                    $mail = new YiiMailer('forgot', array(
                        'url'=> $url,
                        'title' => 'Восстановление пароля от учетной записи сети отелей «Регина»'
                    ) );

                    $mail->setTo($user->email);


                    $mail->setSubject('Восстановление пароля от учетной записи сети отелей «Регина»');

                    $mail->setFrom('robot@'.str_replace('http://','',Yii::app()->request->hostInfo), Yii::t('YiiMailer','Robot informer'));

                    if ($mail->send())
                    {
                        Yii::app()->user->setFlash('success', 'Инструкции по восстановлению пароля отправлены на указанный E-mail' );
                    }
                    else
                    {
                        Yii::app()->user->setFlash('error', 'Ошибка отправки E-mail, повторите попытку позже' );
                    }

                }
            }
        }

        $this->render('forgotForm',array(
            'model' => $model
        ));
    }

    /**
     * This is the action of ajax login
     */
    public function actionLogin()
    {
        //$user = User::model()->findByPk(1);
        //$user->password = 'lithium';
        //$user->save();

        if(isset($_POST['email'])&&$_POST['email']&&isset($_POST['password'])&&$_POST['password'])
        {
            $identity = new UserIdentity($_POST['email'],$_POST['password']);
            if (!$identity->authenticate())
            {
                echo json_encode(array('error'=>'Неправильный E-mail или пароль', 'success' => false));
            }
            else
            {
                $duration=$_POST['remember']=='yes' ? 3600*24*30 : 0; // 30 days
                Yii::app()->user->login($identity,$duration);
                echo json_encode(array('error'=>false, 'success' => true));
            }
        }
    }

    /**
     * This is the action of logout user
     */
    public function actionLogout()
    {
        if(!Yii::app()->user->isGuest)
        {
            Yii::app()->user->logout();
        }
        $this->redirect('/');
    }

    /**
     * This is the action of validate booking form
     */

    public function actionValidate()
    {
        if(isset($_POST['BookingForm']))
        {
            $model = new BookingForm();
            $model->attributes = $_POST['BookingForm'];
            if($model->validate())
            {
                echo json_encode(array('errors'=>false));
            }
            else
            {
                echo json_encode(array('errors'=>$model->errors));
            }
        }
    }

    public function actionClear()
    {
        Yii::app()->session['basket']=array();
        $this->redirect('/booking/');
    }

    /**
     * This is the add to basket action
     */
    public function actionAdd($in, $out, $apartment, $count, $guests,$early, $later)
    {
        if(!is_array(Yii::app()->session['basket']))
        {
            Yii::app()->session['basket']=array();
        }

        $apartment = Apartment::model()->findByPk($apartment);

        if($apartment->availableCount($in,$out)<$count)
        {
            echo 'FAIL';
            Yii::app()->end();
        }

        $basket = Yii::app()->session['basket'];

        $price = $apartment->price($in,$out,$count,$guests, $early, $later);

        $basket[md5(time()+rand(0,1000))] = array(
            'in'=>$in,
            'out'=>$out,
            'apartment'=>$apartment->id,
            'count' => $count,
            'guests'=>$guests,
            'early' => $early,
            'later' => $later,
            'price' => $price['total']
        );

        Yii::app()->session['basket'] = $basket;

        $this->renderPartial('basket');
    }

    /**
     * This is the action of finish order
     */
    public function actionFinish()
    {
        $apartments = array();
        $model = new BookingForm();

        foreach( Yii::app()->session['basket'] as $iid=>$booking)
        {
            $in = $booking['in'];
            $out = $booking['out'];
            $apartment = $booking['apartment'];
            $count = $booking['count'];
            $guests = $booking['guests'];

            $apartment = Apartment::model()->findByPk($apartment);
            $price = $apartment->price($in,$out,$count,$guests,$booking['early'],$booking['later']);

            $extra=0;
            $standard = $guests;
            if($guests > $apartment->places * $count)
            {
                $extra = $guests - $apartment->places * $count;
                $standard = $apartment->places * $count;
            }

            $apartments[$iid]=array(
                'apartment'=>$apartment,
                'in'=>$in,
                'out'=>$out,
                'count'=>$count,
                'guests'=>$guests,
                'extra'=> $extra,
                'price'=>$price,
                'early' => $booking['early']=='true',
                'later' => $booking['later']=='true',
            );

            if(isset($_POST['BookingForm']))
            {
                $model = new Booking();
                $model->attributes = $_POST['BookingForm'];
                $model->name = strtolower($model->name);
				if ($model->passport == "")
					$model->passport = "0";
                $model->passport = strtolower($model->passport);
                $model->phone = strtolower($model->phone);
                $model->comment = strip_tags(isset($_POST['comment'])?$_POST['comment']:'');

                $model->rate = $apartment->rate;

                if($booking['early']=='true')
                {
                    $model->early = 1;
                }

                if($booking['later']=='true')
                {
                    $model->later = 1;
                }

                $model->hotel = $apartment->hotel;
                $model->apartment = $apartment->id;
                $model->persons = $guests;
                $model->price = $price['avg'];
                $model->cost = $price['total'];
                $model->begin = $in;
                $model->end = $out;
                $model->count = $count;
                $model->standard = $standard;
                $model->extra = $extra;
                $model->email = strtolower($_POST['BookingForm']['mail']);
                if($model->standard > $apartment->places * $count)
                {
                    $model->standard = $apartment->places * $count;
                    $model->extra = $guests - $model->standard;
                }

                if(Yii::app()->user->getId()==0)
                {
                    $userExists = User::model()->find("email=:mail",array(":mail" => $_POST['BookingForm']['mail']));
                    if($userExists != null)
                    {
                        $model->user = $userExists->id;
                    }
                    else
                    {
                        $user = new User;
                        $names = explode(' ',$model->name);
                        if(count($names)==3)
                        {
                            $user->lastName = $names[0];
                            $user->firstName = $names[1];
                            $user->middleName = $names[2];
                            $user->email = $_POST['BookingForm']['mail'];
                            $user->superuser = 0;
                            $user->phone = $model->phone;
                            $user->passport = $model->passport;

                            $length = 10;
                            $chars = array_merge(range(0,9), range('a','z'), range('A','Z'));
                            shuffle($chars);
                            $password = implode(array_slice($chars, 0, $length));
                            $user->password = $password;

                            if($user->validate() && $user->save())
                            {
								Yii::log("validate1", CLogger::LEVEL_INFO, "system.web");
                                $model->user = $user->id;

                                $identity = new UserIdentity($user->email,$password);

                                if (!$identity->authenticate())
                                {
                                    Yii::app()->user->setFlash('error', Yii::t('booking','An error occurred while automatically logging') );
                                }
                                else
                                {
                                    Yii::app()->user->login($identity,0);
                                }

                                $mail = new YiiMailer('register', array(
                                    'name'=> $model->name,
                                    'mail' => $user->email,
                                    'password' => $password,
                                    'title' => Yii::t('account','Your password to access the site hotel chain «Regina»')
                                ) );

                                $mail->setTo($user->email);


                                $mail->setSubject( Yii::t('account','Your password to access the site hotel chain «Regina»') );

                                $mail->setFrom('robot@'.str_replace('http://','',Yii::app()->request->hostInfo), Yii::t('YiiMailer','Robot informer'));

                                if ($mail->send())
                                {
                                    Yii::app()->user->setFlash('success',  Yii::t('account','You have successfully registered and logged in. Your password to log into account has been sent to you to E-mail') );
                                } else {
                                    Yii::app()->user->setFlash('error', Yii::t('YiiMailer','Error while sending email: ').$mail->getError());
                                }
                            }
                            else
                            {
                                Yii::app()->user->setFlash('error', Yii::t('account','Error creating user') );
                            }
                        }
                    }
                }
                else
                {
                    $model->user = Yii::app()->user->getId();
                    $model->email = Yii::app()->user->getUsername();
                }

                foreach($price['days'] as $day)
                {
                    $model->note.= $day['date'].' - '.$day['price'].' &#8399; - '.$day['tariff'].'<br>';
                }

                if($model->validate() && $model->save())
                {
                    Yii::app()->user->setFlash('success',Yii::t('booking',"Booking success! Wait we'll call you"));
	
					if($apartment->Hotel!=null && $apartment->Hotel->email)
					{
						$mail = new YiiMailer('info', array(
		                	'title' => "Новая бронь гостиницы {$apartment->Hotel->caption} $in - $out",
		                    'header' => "Новая бронь гостиницы {$apartment->Hotel->caption} $in - $out",
		                    'text' => "На сайте reginahotel.ru добавлена новая <a href='http://{$_SERVER['HTTP_HOST']}/ycm/model/update/name/Booking/pk/{$model->id}'>бронь</a>",
		                ) );
		                
		                $mail->setTo($apartment->Hotel->email);
	                    $mail->setSubject( "Новая бронь гостиницы {$apartment->Hotel->caption} $in - $out" );
	                    $mail->setFrom('robot@'.str_replace('http://','',Yii::app()->request->hostInfo), Yii::t('YiiMailer','Robot informer'));
	                    $mail->send();
                        $host = Yii::app()->request->hostInfo;
                        $date = date('d.m.Y');
                        $userText = <<<EOD
    <img src="$host/themes/regina/css/images/bron.jpg" alt="Регина" style="width:700px"><br><br>
    <b>Дата бронирования:</b>$date<br>
    <h2>Подтверждение бронирования</h2>
    <table>
        <thead>
            <th>Дата заезда</th>
            <th>Дата выезда</th>
            <th>Категория номера</th>
            <th>Количество номеров</th>
            <th>Цена в руб. сутки</th>
            <th>Итого</th>
        </thead>
        <tbody>
EOD;
                        /*$apartments[$iid]=array(
                            'apartment'=>$apartment,
                            'in'=>$in,
                            'out'=>$out,
                            'count'=>$count,
                            'guests'=>$guests,
                            'extra'=> $extra,
                            'price'=>$price,
                            'early' => $booking['early']=='true',
                            'later' => $booking['later']=='true',
                        );*/
                        $total = 0;
                        foreach($apartments as $a)
                        {
                            $total+=$a['price']['total'];
                            $userText.=<<<EOD
                            <tr>
                                <td>{$a['in']}</td>
                                <td>{$a['out']}</td>
                                <td>{$a['apartment']->caption}</td>
                                <td>{$a['count']}</td>
                                <td>{$a['price']['avg']} руб</td>
                                <td>{$a['price']['total']} руб</td>
                            </tr>
EOD;
                        }
                        $userText.=<<<EOD
                        </body>
                        </table>
                        <br>
                        <b>Примечание</b>:$model->comment<br>
                        <b>Всего к оплате: $total руб</b><br>
                        <h3>Оплата принимается: в рублях, банковским переводом или кредитной картой.</h3>

                        <b>Благодарим за выбор нашего отеля.  Мы рады подтвердить Ваше бронирование.</b><br>
                        <p>Время заезда - 13.00. Расчетный час – 12.00. Поздний выезд оплачивается дополнительно.</p>
                        <p>Аннуляция бронирования должна быть направлена не позднее, чем за 48 часов до официально установленного часа заезда.</p>

                        <h3>Для заселения необходимо иметь при себе паспорт.</h3>

                        <p style="text-align:center">Сеть отелей и ресторанов «Регина»<br>Тел.:  8 (843) 203-3-203</p>
                        <br>
                        <a href="http://www.hotelregina.ru" target="_blank>www.hotelregina.ru</a>
EOD;


                        $mail = new YiiMailer('info', array(
                            'title'=> Yii::t('booking',"You order new booking on the hotel chain «Regina»"),
                            'header' => Yii::t('booking',"You order new booking on the hotel chain «Regina»"),
                            'text' => $userText
                        ) );

                        $mail->setTo($model->email);


                        $mail->setSubject( Yii::t('booking',"You order new booking on the hotel chain «Regina»") );

                        $mail->setFrom('robot@'.str_replace('http://','',Yii::app()->request->hostInfo), Yii::t('YiiMailer','Robot informer'));
                        $mail->send();
					}
					
	                
                }
                else
                {
                    Yii::app()->user->setFlash('error',Yii::t('booking',"Booking error! Try again later"));
                }
            }
        }

        if(Yii::app()->user->hasFlash('success')||Yii::app()->user->hasFlash('error'))
        {
            if(Yii::app()->user->hasFlash('success'))
            {
                Yii::app()->session['basket'] = array();
            }
            $this->redirect('/booking/');
        }

        if(!Yii::app()->user->isGuest && !isset($_POST['BookingForm']))
        {
            $user = User::model()->findByPk(Yii::app()->user->getId());

            $model->name = $user->lastName.' '.$user->firstName.' '.$user->middleName;
            $model->mail = Yii::app()->user->getUsername();
            $model->phone = $user->phone;
            $model->passport = $user->passport;
        }
        else
        {
            $user = null;
        }

        $this->renderPartial('bookingForm',array(
            'model' => $model,
            'apartments' => $apartments,
        ));
    }

    public function actionDelete($id)
    {
        $basket = Yii::app()->session['basket'];
        if(is_array($basket) && isset($basket[$id]))
        {
            $e = $basket[$id];
            echo "/site/add/in/{$e['in']}/out/{$e['out']}/apartment/{$e['apartment']}/count/{$e['count']}/guests/{$e['guests']}/early/{$e['early']}/later/{$e['later']}/";
            unset($basket[$id]);
            Yii::app()->session['basket'] = $basket;
        }
    }

    /**
     * This is the action of booking form
     */
    private function actionAddBooking($in, $out, $apartment, $count, $guests)
    {
        $apartment = Apartment::model()->findByPk($apartment);
        $price = $apartment->price($in,$out,$count,$guests);
        $model = new BookingForm();
        if(isset($_POST['BookingForm']))
        {
            $model = new Booking();
            $model->attributes = $_POST['BookingForm'];
            $model->rate = $apartment->rate;

            $model->hotel = $apartment->hotel;
            $model->apartment = $apartment->id;
            $model->persons = $guests;
            $model->price = $price['avg'];
            $model->cost = $price['total'];
            $model->begin = $in;
            $model->end = $out;
            $model->count = $count;
            $model->standard = $guests;
            $model->extra = 0;
            $model->email = $_POST['BookingForm']['mail'];
            if($model->standard > $apartment->places * $count)
            {
                $model->standard = $apartment->places * $count;
                $model->extra = $guests - $model->standard;
            }

            if(Yii::app()->user->getId()==0)
            {
                $user = new User;
                $names = explode(' ',$model->name);
                if(count($names)==3)
                {
                    $user->lastName = $names[0];
                    $user->firstName = $names[1];
                    $user->middleName = $names[2];
                    $user->email = $_POST['BookingForm']['mail'];
                    $user->superuser = 0;
                    $user->phone = $model->phone;
                    $user->passport = $model->passport;

                    $length = 10;
                    $chars = array_merge(range(0,9), range('a','z'), range('A','Z'));
                    shuffle($chars);
                    $password = implode(array_slice($chars, 0, $length));
                    $user->password = $password;

                    if($user->validate() && $user->save())
                    {
                        $model->user = $user->id;

                        $identity = new UserIdentity($user->email,$password);

                        if (!$identity->authenticate())
                        {
                            Yii::app()->user->setFlash('error', Yii::t('booking','An error occurred while automatically logging') );
                        }
                        else
                        {
                            Yii::app()->user->login($identity,0);
                        }

                        $mail = new YiiMailer('register', array(
                            'name'=> $model->name,
                            'mail' => $user->email,
                            'password' => $password,
                            'title' => Yii::t('account','Your password to access the site hotel chain «Regina»')
                        ) );

                        $mail->setTo($user->email);


                        $mail->setSubject( Yii::t('account','Your password to access the site hotel chain «Regina»') );

                        $mail->setFrom('robot@'.str_replace('http://','',Yii::app()->request->hostInfo), Yii::t('YiiMailer','Robot informer'));

                        if ($mail->send()) {
                            Yii::app()->user->setFlash('success',  Yii::t('account','You have successfully registered and logged in. Your password to log into account has been sent to you to E-mail') );
                        } else {
                            Yii::app()->user->setFlash('error', Yii::t('YiiMailer','Error while sending email: ').$mail->getError());
                        }
                    }
                    else
                    {
                        Yii::app()->user->setFlash('error', Yii::t('account','Error creating user') );
                    }
                }
            }
            else
            {
                $model->user = Yii::app()->user->getId();
                $model->email = Yii::app()->user->getUsername();
            }

            //die();

            foreach($price['days'] as $day)
            {
                $model->note.= $day['date'].' - '.$day['price'].' &#8399; - '.$day['tariff'].'<br>';
            }

            if($model->validate() && $model->save())
            {
                Yii::app()->user->setFlash('success',Yii::t('booking',"Booking success! Wait we'll call you"));
            }
            else
            {
                Yii::app()->user->setFlash('error',Yii::t('booking',"Booking error! Try again later"));
            }
            $this->redirect('/booking/');
        }

        $extra = 0;

        if($guests > $apartment->places * $count)
        {
            $extra = $guests - $apartment->places * $count;
            $guests = $apartment->places * $count;
        }

        if(!Yii::app()->user->isGuest)
        {
            $user = User::model()->findByPk(Yii::app()->user->getId());

            $model->name = $user->lastName.' '.$user->firstName.' '.$user->middleName;
            $model->mail = $user->email;
            $model->phone = $user->phone;
            $model->passport = $user->passport;
        }
        else
        {
            $user = null;
        }


        $this->renderPartial('bookingForm',array(
            'model' => $model,
            'in' => $in,
            'out' => $out,
            'apartment' => $apartment,
            'image' => Gallery::model()->findByPk($apartment->gallery_id)->galleryPhotos[0]->id,
            'apartmentCount' => $count,
            'guestsCount' => $guests,
            'extraCount' => $extra,
            'price' => $price,
        ));
    }

    /**
     * This is the action of calculate apartment cost
     */
    public function actionCalculate($in, $out, $apartment, $count, $guests, $early, $later)
    {
        $apartment = Apartment::model()->findByPk($apartment);
        if($apartment==null)
        {
            echo json_encode(array('error'=>Yii::t('booking','Error! Apartment not found') ) ) ;
        }
        else
        {
            echo json_encode($apartment->price($in,$out, $count, $guests, $early, $later));
        }
    }

    /**
     * This is the action of list hotels
     */

    public function actionBooking($in,$out, $location, $guests)
    {
        // default period
        if($in=='all')
        {
            $in = date('d.m.Y');
        }
        if($out=='all')
        {
            $out = date('d.m.Y',time()+86400*1);
        }

        // check if in more than out
        $inTimestamp = CDateTimeParser::parse($in,'dd.MM.yyyy');
        $outTimestamp = CDateTimeParser::parse($out,'dd.MM.yyyy');
        if($outTimestamp<$inTimestamp)
        {
            // exchange in and out
            $out = date('d.m.Y',$inTimestamp+86400*1);
            $this->redirect("/booking/$in/$out/$location/$guests/");
        }

        if($outTimestamp==$inTimestamp)
        {
            // change out
            $out = date('d.m.Y',$inTimestamp+86400);
            $this->redirect("/booking/$in/$out/$location/$guests/");
            Yii::app()->end();
        }

        $inTimestamp = CDateTimeParser::parse($in,'dd.MM.yyyy');
        $outTimestamp = CDateTimeParser::parse($out,'dd.MM.yyyy');

        $days = round( ($outTimestamp-$inTimestamp)/86400 );
        if($days==0)
        {
            $days=1;
        }

        $this->cs->registerScriptFile('http://api-maps.yandex.ru/2.0-stable/?load=package.standard,package.geoObjects&lang='.Yii::app()->getLanguage().'-'.(Yii::app()->getLanguage()=='en'?'US':strtoupper(Yii::app()->getLanguage())) );
        $this->cs->registerCssFile($this->cssPath.'/booking.css');
        $this->cs->registerCssFile($this->cssPath.'/booking-form.css');
        $bookingRoot = Page::model()->find('code=:code',array(':code'=>'booking'));
        if($bookingRoot!=null)
        {
            $this->pageTitle = $bookingRoot->title?$bookingRoot->title:$bookingRoot->name;
            $this->cs->registerMetaTag($bookingRoot->description,"description");
            $this->cs->registerMetaTag($bookingRoot->keywords,"keywords");
        }
        $header = $bookingRoot == null?'Гостиницы':$bookingRoot->name;
        if($location==0)
        {
            $hotelsRecords = Hotel::model()->findAll('status=:status',array(':status'=>'work'));
        }
        else
        {
            $hotelsRecords = Hotel::model()->findAll('status=:status and location=:location',array(':status'=>'work','location'=>$location));
        }

        $hotels = array();
        foreach($hotelsRecords as $hotel)
        {
            $apartmentCount = 0;
            foreach($hotel->apartments as $apartment)
            {
                $apartmentCount+=$apartment->availableCount($in, $out);
            }
            if($apartmentCount)
            {
                $hotel->gallery_id = Gallery::model()->findByPk($hotel->gallery_id);
                $hotels[]=$hotel;
            }
        }

        $locations = array('items'=>array(
            array(
                'label' => Yii::t('site','All'),
                'url' => '#0'
            )
        ));
        $locationCriteria = new CDbCriteria();
        $locationCriteria->order='caption asc';
        $locationsArray = Location::model()->findAll($locationCriteria);
        foreach($locationsArray as $_location)
        {
            $locations['items'][] = array(
                'label' => $_location->caption,
                'url' => '#'.$_location->id,
            );
            if($location==$_location->id)
            {
                $locations['label'] = $_location->caption;
            }
        }
        $locations['label'] = isset($locations['label'])?$locations['label']:$locations['items'][0]['label'];


        $this->render('booking',array(
            'header' => $header,
            'hotels' => $hotels,
            'in' => $in,
            'out' => $out,
            'locations' => array($locations),
            'location' => $location,
            'guests' => $guests,
            'days' => $days,
            'basket' => $this->renderPartial('basket',array(),true)
        ));
    }

    /**
     * This is the action of tours
     */

    public function actionTours($view)
    {
        $this->cs->registerCssFile($this->cssPath.'/tours.css');
        if($view)
        {
            $tour = Tour::model()->find('code=:code',array('code'=>$view));
            if($tour==null)
            {
                $this->throw404();
            }
            $this->render('tour',array(
                'tour' => $tour
            ));
        }
        else
        {
            $tourRoot = Page::model()->find("code=:code",array(':code'=>'tours'));
            $tours = Tour::model()->findAll("disable is null OR disable = 0");
            foreach($tours as $i=>$tour)
            {
                $tours[$i]->image = '/uploads/tour/image/'.$tour->image;
                $tours[$i]->code = '/tours/'.$tour->code.'/';
            }

            if($tourRoot==null)
            {
                $header = 'Экскурсии';
                $html = '';
                $this->pageTitle = 'Экскурсии';
            }
            else
            {
                $header = $tourRoot->name;
                $html = $tourRoot->detail_text;

                $this->cs->registerMetaTag($tourRoot->description,"description");
                $this->cs->registerMetaTag($tourRoot->keywords,"keywords");
                $this->pageTitle = $tourRoot->title?$tourRoot->title:$tourRoot->name;
            }


            $this->render('tours',array(
                'tours' => $tours,
                'header' => $header,
                'html' => $html
            ));
        }
    }

    public function actionSpecials($code)
    {
        $news = Special::model()->find('code=:code AND hide!=1',array(':code'=>$code));
        if($news==null)
        {
            $this->throw404();
        }
        else
        {
            $this->pageTitle = Yii::t('site','Specials').' - '.($news->title?$news->title:$news->name);
            $this->render('new',array('data'=>$news));
        }
    }

    /**
     * This is the action of main page
     */

    public function actionIndex()
    {
        $page = new Page;
        $page = $page->find('code=:code and disable=0',array(':code'=>'main'));
        if($page == null)
        {
            $this->pageTitle = Yii::t('site','Main');
            $this->render('index',array(
                'html' => '<noindex>'.Yii::t('site','Main page not found. Create page with code "main"').'</noindex>',
                'error' => true
            ));
        }
        else
        {
            // white back on main
            $this->cs->registerCss('body-white-circle',"body { background: url('/themes/regina/css/images/white-circle.png') no-repeat scroll center 22px,url('/themes/regina/css/images/pattern.png') }");

            // seo things
            $this->pageTitle = $page->title?$page->title:$page->name;
            $this->cs->registerMetaTag($page->description,"description");
            $this->cs->registerMetaTag($page->keywords,"keywords");

            // slider on main
            $slider = Slider::model()->find( 'code=:code', array(':code'=>'main') );
            if($slider==null)
            {
                $slides = array();
            }
            else
            {
                $slides = Gallery::model()->findByPk($slider->gallery_id)->galleryPhotos;
            }

            // guests on main find form
            $guests = array( 'label'=>'1', 'items'=>array() );
            $largestCriteria = new CDbCriteria();
            $largestCriteria->select = 'places, extra, places + extra as total';
            $largestCriteria->order = 'total desc';
            $largestApartment = Apartment::model()->find($largestCriteria);
            if($largestApartment!=null)
            {
                $guestsCount = $largestApartment->places+$largestApartment->extra;
            }
            else
            {
                $guestsCount= 0;
            }

            unset($largestApartment);
            unset($largestCriteria);
            for($i=1;$i<=$guestsCount;$i++)
            {
                $guests['items'][]=array(
                    'label' => $i,
                    'url' => '#'.$i
                );
            }

            // locations on main find form
            $locations = array('items'=>array(
            	array(
            		'label'=> Yii::t('booking','All'),
            		'url'=> '#0'
            	)
            ));
            $locationCriteria = new CDbCriteria();
            $locationCriteria->order='caption asc';
            $locationsArray = Location::model()->findAll($locationCriteria);
            foreach($locationsArray as $location)
            {
                $locations['items'][] = array(
                    'label' => $location->caption,
                    'url' => '#'.$location->id,
                );
            }
            if(count($locationsArray))
            {
                $locations['label'] = $locations['items'][0]['label'];
            }


            // specials on main
            $criteria = new CDbCriteria();
            $criteria->order = 'sort desc';
            $criteria->limit = 3;
            $criteria->condition = 'hide!=1';
            $specials = Special::model()->findAll( $criteria );

            // news on main
            $newsCriteria = new CDbCriteria();
            $newsCriteria->order = /*"substr(created,7,4)||substr(created,4,2)||substr(created,1,2) desc"*/'id desc';
            $newsCriteria->condition='hide!=1';
            $newsCriteria->limit=3;
            $news = News::model()->findAll($newsCriteria);


            // default period

            $in = date('d.m.Y');

            $out = new DateTime();
            $out->add(new DateInterval('P1D'));
            $out = $out->format('d.m.Y');


            $this->render('index',array(
                'html' => $page->detail_text,
                'slides' => $slides,
                'guests' => array($guests),
                'specials' => $specials,
                'locations' => array($locations),
                'news' => $news,
                'in' => $in,
                'out' => $out,
                'error' => false
            ) );
        }

    }

    /**
     * This is the action of feedback
     */

    public function actionFeedback()
    {
        $model = new FeedbackForm;

        if(isset($_POST['FeedbackForm'])) // sending mail
        {
            $model->attributes=$_POST['FeedbackForm'];
            if($model->validate())
            {
                $header = Setting::findByCode('mail_header','feedback');
                $title = Setting::findByCode('mail_title','feedback');
                $theme = Setting::findByCode('theme','feedback');

                $mail = new YiiMailer('feedback', array(
                    'model'=>$model,
                    'header' => $header==null?Yii::t('feedback','You have a new message from feedback form'):$header->value,
                    'title' => $title==null?Yii::t('feedback','Feedback message'):$title->value,
                ) );
                $mailSettingOption = Setting::findByCode('mail','feedback');

                if($mailSettingOption == null)
                {
                    $mailSettingOption = Option::findByCode('mail');
                }

                if($mailSettingOption == null)
                {
                    $mail->setTo('info@hotelregina.ru');
                }
                else
                {
                    $mail->setTo($mailSettingOption->clearValue());
                }

                $mail->setSubject($theme==null?Yii::t('feedback','New feedback message'):$theme->clearValue());

                $mail->setFrom('robot@'.str_replace('http://','',Yii::app()->request->hostInfo), Yii::t('YiiMailer','Robot informer'));

                if ($mail->send()) {
                    Yii::app()->user->setFlash('success',Yii::t('YiiMailer','Thank you for contacting us. We will respond to you as soon as possible.'));
                } else {
                    Yii::app()->user->setFlash('error', Yii::t('YiiMailer','Error while sending email: ').$mail->getError());
                }
            }
        }

        $url = explode('/',trim(Yii::app()->request->requestUri,'/'));

        $feedbackPage = Page::model()->find("code=:code",array(':code'=>$url[count($url)-1] ));

        $this->pageTitle = $feedbackPage->title?$feedbackPage->title:$feedbackPage->name;

        $this->cs->registerScriptFile('http://api-maps.yandex.ru/2.0-stable/?load=package.standard,package.geoObjects&lang='.Yii::app()->getLanguage().'-'.(Yii::app()->getLanguage()=='en'?'US':strtoupper(Yii::app()->getLanguage())) );

        $this->render('feedback', array(
            'model' =>  $model,
            'header' => $feedbackPage==null?'Обратная связь':$feedbackPage->name,
            'text' => $feedbackPage==null?'':$feedbackPage->detail_text,
            'hotels' => Hotel::model()->findAll()
        ));
    }

    /**
     * This is the action to display pages from database
     * @param $pk
     * @throws 404
     */

    public function actionPage($pk)
	{
        if( strpos(Yii::app()->request->url, 'site/page/pk') )
        {
            $this->throw404();
        }
        $page = Page::model()->findByPk($pk);
        if($page == null || $page->code == 'main')
        {
            $this->throw404();
        }
        if($page->code == 'gruppovyie_zaezdyi')
        {
            $this->forward('site/feedback');
        }
        if($page->title)
        {
            $this->pageTitle = $page->title;
        }
        else
        {
            $this->pageTitle = $page->name;
        }
        //var_dump(Gallery::model()->findByPk($page->gallery_id)->galleryPhotos);
        $this->cs->registerMetaTag($page->description,"description");
        $this->cs->registerMetaTag($page->keywords,"keywords");

        $photos = array();
        foreach($page->files as $gallery)
        {
            $gallery = Gallery::model()->findByPk($gallery->disable);
            if($gallery!=null)
            {
                $photos = array_merge($photos,$gallery->galleryPhotos);
            }
        }

		$this->render('page', array(
            'page'=>$page,
            'block' => Gallery::model()->findByPk($page->gallery_id),
            'photos' => $photos
        ) );
	}

	/**
	 * This is the action to handle external exceptions.
	 */
	public function actionError()
	{
        $this->pageTitle = Yii::t('site','Page not found');

		if($error=Yii::app()->errorHandler->error)
		{
			if(Yii::app()->request->isAjaxRequest)
				echo $error['message'];
			else
				$this->render('error', $error);
		}
	}
}