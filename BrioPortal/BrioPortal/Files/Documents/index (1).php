<?
require($_SERVER['DOCUMENT_ROOT'].'/bitrix/header.php');
$APPLICATION->SetPageProperty("TITLE", "Транспортные услуги грузоперевозок грузов по Москве, области и России");
$APPLICATION->SetPageProperty("keywords", "грузоперевозки по москве, области, грузоперевозки по россии, грузоперевозки грузов, транспортные грузоперевозки, транспортные услуги, недорого, дешевые, недорогие");
$APPLICATION->SetPageProperty("description", "Транспортно-экспедиционной компания МакСпецТранс предоставляет недорогие грузоперевозки автотранспортом грузов по Москве, московской области и России. Дешевые транспортные услуги грузоперевозок.");
$APPLICATION->SetTitle("Р“Р»Р°РІРЅР°СЏ");
?>  <tr>
    <td><!--Блок для видеогида-->
      
      <div class="main-wrapp img-wrapp">
<iframe style="width: 100%" height="315" src="//www.youtube.com/embed/AL8LjQypI3Y?rel=0&amp;controls=0&amp;showinfo=0" frameborder="0" allowfullscreen></iframe></div>
      
      <!--Конец блока--></td>
  </tr>
  <tr>
    <td><!--Форма обратной связи-->
      
      <div class="main-wrapp">
<?$APPLICATION->IncludeComponent("bitrix:main.feedback", "feeback", array(
	"USE_CAPTCHA" => "N",
	"OK_TEXT" => "Спасибо, ваше сообщение принято.",
	"EMAIL_TO" => "roman.alik2013@yandex.ru",
	"REQUIRED_FIELDS" => array(
		0 => "NAME",
		1 => "EMAIL",
		2 => "MESSAGE",
	),
	"EVENT_MESSAGE_ID" => array(
		0 => "7",
	)
	),
	false
);?>
      </div>
      
      <!--Конец формы--></td>
  </tr>
  <tr>
    <td><!--Виды перевозок-->
      
      <div class="main-wrapp">
        <div class="ico">
          <ul>
            <li onclick="location.href='/services/1';">
               <div></div>
               Негабаритные<br>
 
               перевозки</li>
            <li onclick="location.href='/services/2';">
               <div></div>
               Международные<br>

               перевозки</li>
            <li onclick="location.href='/services/3';">
              <div></div>
              Городские<br>

              перевозки</li>
            <li onclick="location.href='/services/4';">
              <div></div>
              Междугородние<br>

              перевозки</li>
            <li>
              <div></div>
              Спецтехника
			</li>
          </ul>
        </div>
      </div>
      
      <!--Конец видов--></td>
  </tr>
  <tr>
    <td><!--Контент-->
      
      <div class="content-wrapp">
        <table width="100%" border="0">
          <tr>
            <td style="vertical-align:top;"><!--Левый блок-->
              
              <div class="left-bar"> <a href="#">О компании</a>
                <h3 class="yellow">Политика в области качества</h3>
                <a href="#" class="pressa">Пресса о нас</a>
                <article>
                  <div id="fb-root"></div>
					<script>(function(d, s, id) {
					  var js, fjs = d.getElementsByTagName(s)[0];
					  if (d.getElementById(id)) return;
					  js = d.createElement(s); js.id = id;
					  js.src = "//connect.facebook.net/ru_RU/sdk.js#xfbml=1&version=v2.0";
					  fjs.parentNode.insertBefore(js, fjs);
					}(document, 'script', 'facebook-jssdk'));</script>
					<div class="fb-like-box" data-href="https://www.facebook.com/pages/МакСпецТранс/496946963773449" data-width="216" data-colorscheme="dark" data-show-faces="true" data-header="true" data-stream="false" data-show-border="true"></div>
                  <div id="vk_groups"></div>
                </article>
              </div>
              
              <!--Конец левого блока--></td>
            <td> </td>
            <td style="vertical-align:top"><div class="content">
                <h1>Транспортные услуги грузоперевозок грузов по Москве, области и России</h1>
				<p class="sep">Современные транспортные грузоперевозки представляют собой комплекс действий, обеспечивающих процесс доставки грузов по назначению. Благодаря тому, что сотрудники транспортно-экспедиционной компании рационально организуют этот процесс, уровень качества предоставляемых нами услуг пользуется заслуженным доверием.</p>
				<p>Наша организация принимает заказы на недорогие грузоперевозки автотранспортом по Москве, области и России. В нашем автопарке всегда рады предоставить дешевые транспортные услуги и Газель, и фура. Они с легкостью доставят груз 5, 10 и 20 тонн в любую точку Москвы, московской области, а также России.</p>
				<p>Длительный опыт работы в рыночных условиях и репутация нашей компании, осуществляющей грузоперевозки, позволяют гарантировать своим клиентам высокий уровень услуг по транспортировке и экспедированию всех видов массовых, мелкопартионных или сборных грузов. Выполнение грузоперевозок в соответствии со всеми требованиями отправителя возможно, благодаря компетентности наших специалистов, использующих все современные технологии.</p>
				<p>Предоставляя транспортные и экспедиторские услуги, как владельцам больших объемов промышленных грузов, так и отдельным физическим лицам, мы гарантируем надежную, удобную и безопасную доставку груза в соответствии со всеми вашими требованиями.</p>
			</div>
			</tr>
        </table>
      </div>
      
      <!--Конец контента--></td>
  </tr>
  <tr>
    <td><div class="black-line"> </div></td>
  </tr>
  <tr>
    <td><div class="content-wrapp">
        <table width="100%" border="0">
          <tr>
            <td style="vertical-align:top;"><!--Левый блок-->
              
              <div class="left-bar bottom-left-bar"> <a href="#">Новости компании</a>
                <h3 class="yellow">Законодательные новости</h3>
                <p><a href="#">Акции</a></p>
                <p><a href="#">Обзоры и статьи</a></p>
                <p><a href="#">Все новости</a></p>
              </div>
              
              <!--Конец левого блока--></td>
            <td> </td>
            <td style="vertical-align:top"><!--Нижний контент-->
              
              <div class="content bottom-content">
				<?$APPLICATION->IncludeComponent("bitrix:news", "news", array(
	"IBLOCK_TYPE" => "NEWS",
	"IBLOCK_ID" => "1",
	"NEWS_COUNT" => "4",
	"USE_SEARCH" => "N",
	"USE_RSS" => "N",
	"USE_RATING" => "N",
	"USE_CATEGORIES" => "N",
	"USE_FILTER" => "N",
	"SORT_BY1" => "",
	"SORT_ORDER1" => "",
	"SORT_BY2" => "",
	"SORT_ORDER2" => "",
	"CHECK_DATES" => "Y",
	"SEF_MODE" => "N",
	"SEF_FOLDER" => "/news1/",
	"AJAX_MODE" => "Y",
	"AJAX_OPTION_JUMP" => "N",
	"AJAX_OPTION_STYLE" => "Y",
	"AJAX_OPTION_HISTORY" => "N",
	"CACHE_TYPE" => "A",
	"CACHE_TIME" => "36000000",
	"CACHE_FILTER" => "N",
	"CACHE_GROUPS" => "Y",
	"SET_STATUS_404" => "N",
	"SET_TITLE" => "Y",
	"INCLUDE_IBLOCK_INTO_CHAIN" => "Y",
	"ADD_SECTIONS_CHAIN" => "Y",
	"ADD_ELEMENT_CHAIN" => "N",
	"USE_PERMISSIONS" => "N",
	"PREVIEW_TRUNCATE_LEN" => "",
	"LIST_ACTIVE_DATE_FORMAT" => "j M Y",
	"LIST_FIELD_CODE" => array(
		0 => "",
		1 => "",
	),
	"LIST_PROPERTY_CODE" => array(
		0 => "",
		1 => "",
	),
	"HIDE_LINK_WHEN_NO_DETAIL" => "N",
	"DISPLAY_NAME" => "Y",
	"META_KEYWORDS" => "-",
	"META_DESCRIPTION" => "-",
	"BROWSER_TITLE" => "-",
	"DETAIL_ACTIVE_DATE_FORMAT" => "j M Y",
	"DETAIL_FIELD_CODE" => array(
		0 => "",
		1 => "",
	),
	"DETAIL_PROPERTY_CODE" => array(
		0 => "",
		1 => "",
	),
	"DETAIL_DISPLAY_TOP_PAGER" => "N",
	"DETAIL_DISPLAY_BOTTOM_PAGER" => "Y",
	"DETAIL_PAGER_TITLE" => "Страница",
	"DETAIL_PAGER_TEMPLATE" => "",
	"DETAIL_PAGER_SHOW_ALL" => "Y",
	"PAGER_TEMPLATE" => "",
	"DISPLAY_TOP_PAGER" => "N",
	"DISPLAY_BOTTOM_PAGER" => "N",
	"PAGER_TITLE" => "Новости",
	"PAGER_SHOW_ALWAYS" => "Y",
	"PAGER_DESC_NUMBERING" => "N",
	"PAGER_DESC_NUMBERING_CACHE_TIME" => "36000",
	"PAGER_SHOW_ALL" => "Y",
	"DISPLAY_DATE" => "Y",
	"DISPLAY_PICTURE" => "N",
	"DISPLAY_PREVIEW_TEXT" => "Y",
	"USE_SHARE" => "N",
	"AJAX_OPTION_ADDITIONAL" => "",
	"VARIABLE_ALIASES" => array(
		"SECTION_ID" => "SECTION_ID",
		"ELEMENT_ID" => "ELEMENT_CODE",
	)
	),
	false
);?>
              </div>
              
              <!--Конец нижнего контента--></td>
          </tr>
        </table>
      </div></td>
  </tr>
  <tr>
    <td><!--Нижняя форма-->
<?$APPLICATION->IncludeComponent("bitrix:main.feedback", "feeback", array(
	"USE_CAPTCHA" => "N",
	"OK_TEXT" => "Спасибо, ваше сообщение принято.",
	"EMAIL_TO" => "roman.alik2013@yandex.ru",
	"REQUIRED_FIELDS" => array(
		0 => "NAME",
		1 => "EMAIL",
		2 => "MESSAGE",
	),
	"EVENT_MESSAGE_ID" => array(
		0 => "7",
	)
	),
	false
);?>     
      <!--Конец нижней формы--></td>
  </tr>
  <tr>
    <td><div class="content-wrapp">
        <table width="100%" border="0">
          <tr>
            <td style="vertical-align:top;"></td>
            <td> </td>
            <td style="vertical-align:top"><!--Нижний контент-->
              
              <div class="content bottom-bottom-content">
                <h2>отзывы</h2>
                <a href="#" class="arrow"></a>
                <div class="response">
                  <p class="response-title">Илюшин Дмитрий Сергеевич, ИП Экспедитор-перевозчик, Казань</p>
                  <p>“Работаем мы с "МакСпецТранс" не первый год, за это время отметили ответственность компании, ее умение выходить из любых ситуаций, а главное, добросовестное исполнение обязательства. Спасибо!”</p>
                </div>
                <p class="link-wrapp"><a href="#" class="yellow detal">Все отзывы</a></p>
                <p class="link-wrapp"><a href="" onclick="return false;" class="yellow detal write_review">Оставить отзыв</a></p>
              </div>
              
              <!--Конец нижнего контента--></td>
          </tr>
        </table>
      </div></td>
  </tr>
  <noindex>
  <!-- Yandex.Metrika counter -->
<script type="text/javascript">
(function (d, w, c) {
    (w[c] = w[c] || []).push(function() {
        try {
            w.yaCounter24165163 = new Ya.Metrika({id:24165163,
                    webvisor:true,
                    clickmap:true,
                    trackLinks:true,
                    accurateTrackBounce:true});
        } catch(e) { }
    });

    var n = d.getElementsByTagName("script")[0],
        s = d.createElement("script"),
        f = function () { n.parentNode.insertBefore(s, n); };
    s.type = "text/javascript";
    s.async = true;
    s.src = (d.location.protocol == "https:" ? "https:" : "http:") + "//mc.yandex.ru/metrika/watch.js";

    if (w.opera == "[object Opera]") {
        d.addEventListener("DOMContentLoaded", f, false);
    } else { f(); }
})(document, window, "yandex_metrika_callbacks");
</script>
<!-- /Yandex.Metrika counter -->
  </noindex>
  <?
require($_SERVER['DOCUMENT_ROOT'].'/bitrix/footer.php');
?>