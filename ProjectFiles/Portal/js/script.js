// JavaScript Document

$(document).ready(function ()
{
	
	
	
//Загрузка файлов

function upload (){
	var btnUpload = $('#upload');
new AjaxUpload(btnUpload, {
        action: '',//Путь к обработчику
        name: 'uploadfile',
        type: "POST",
        scriptCharset: "utf8",
        data:
        {
            
        },

        onComplete: function (file, response)
        { 
            //On completion clear the status
            status.text('');
            //Add uploaded file to list

            if (response.indexOf('success') == 0)
            {
              //Если все хорошо
            }
            else
            {
             //Если все хреново
            }
        }
    })
	}

if($("button").is("#upload")){
	upload();
};
	//Календарь
	$("#datepicker").datepicker();
		//Тултипы календаря
		$('.ui-state-default').hover(function(){
			$(this).append('<div class="tooltip">Добавить заметку</div>')
			},
			function(){
				$('.tooltip').remove()
				});
				
				//Левое меню
			
    $('.wrapp ul li').has('ul').bind('click',
	
	
	function(){
		$('p:first-child',$(this)).addClass('select');
		var form_continer=$(this).children('ul').children('li');
		var form='<form action="" method="post" name="add"><p>Название: <input name="file-name" type="text"></p><div> <p class="upload-wrapp"><button id="upload" class="open-file ">Открыть</button></p><p>Файл:</p></div><button type="submit"><h2 class="right creat-title"><span>Добавить</span></h2></button></form>';
		if($(form_continer).is(".list-none")){
			$("li.list-none",$(this)).append($(form))
						}
       $(this).children('ul').slideToggle(600, function(){
		  
		   if($(this).css('display')=='none'){
			   $('form[name="add"]').remove();
			   $('.wrapp ul li p').removeClass('select');
			   }
			   else{
			  if($("button").is("#upload")){
	upload();
};
			   }
		   });
       return false;
   
});
//Поп-ап
$(document).click(function(){
	$('#pop-up').hide()
	})

});