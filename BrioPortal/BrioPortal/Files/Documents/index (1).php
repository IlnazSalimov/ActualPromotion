<?
require($_SERVER['DOCUMENT_ROOT'].'/bitrix/header.php');
$APPLICATION->SetPageProperty("TITLE", "������������ ������ �������������� ������ �� ������, ������� � ������");
$APPLICATION->SetPageProperty("keywords", "�������������� �� ������, �������, �������������� �� ������, �������������� ������, ������������ ��������������, ������������ ������, ��������, �������, ���������");
$APPLICATION->SetPageProperty("description", "�����������-�������������� �������� ������������ ������������� ��������� �������������� ��������������� ������ �� ������, ���������� ������� � ������. ������� ������������ ������ ��������������.");
$APPLICATION->SetTitle("Главная");
?>  <tr>
    <td><!--���� ��� ���������-->
      
      <div class="main-wrapp img-wrapp">
<iframe style="width: 100%" height="315" src="//www.youtube.com/embed/AL8LjQypI3Y?rel=0&amp;controls=0&amp;showinfo=0" frameborder="0" allowfullscreen></iframe></div>
      
      <!--����� �����--></td>
  </tr>
  <tr>
    <td><!--����� �������� �����-->
      
      <div class="main-wrapp">
<?$APPLICATION->IncludeComponent("bitrix:main.feedback", "feeback", array(
	"USE_CAPTCHA" => "N",
	"OK_TEXT" => "�������, ���� ��������� �������.",
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
      
      <!--����� �����--></td>
  </tr>
  <tr>
    <td><!--���� ���������-->
      
      <div class="main-wrapp">
        <div class="ico">
          <ul>
            <li onclick="location.href='/services/1';">
               <div></div>
               ������������<br>
 
               ���������</li>
            <li onclick="location.href='/services/2';">
               <div></div>
               �������������<br>

               ���������</li>
            <li onclick="location.href='/services/3';">
              <div></div>
              ���������<br>

              ���������</li>
            <li onclick="location.href='/services/4';">
              <div></div>
              �������������<br>

              ���������</li>
            <li>
              <div></div>
              �����������
			</li>
          </ul>
        </div>
      </div>
      
      <!--����� �����--></td>
  </tr>
  <tr>
    <td><!--�������-->
      
      <div class="content-wrapp">
        <table width="100%" border="0">
          <tr>
            <td style="vertical-align:top;"><!--����� ����-->
              
              <div class="left-bar"> <a href="#">� ��������</a>
                <h3 class="yellow">�������� � ������� ��������</h3>
                <a href="#" class="pressa">������ � ���</a>
                <article>
                  <div id="fb-root"></div>
					<script>(function(d, s, id) {
					  var js, fjs = d.getElementsByTagName(s)[0];
					  if (d.getElementById(id)) return;
					  js = d.createElement(s); js.id = id;
					  js.src = "//connect.facebook.net/ru_RU/sdk.js#xfbml=1&version=v2.0";
					  fjs.parentNode.insertBefore(js, fjs);
					}(document, 'script', 'facebook-jssdk'));</script>
					<div class="fb-like-box" data-href="https://www.facebook.com/pages/������������/496946963773449" data-width="216" data-colorscheme="dark" data-show-faces="true" data-header="true" data-stream="false" data-show-border="true"></div>
                  <div id="vk_groups"></div>
                </article>
              </div>
              
              <!--����� ������ �����--></td>
            <td>�</td>
            <td style="vertical-align:top"><div class="content">
                <h1>������������ ������ �������������� ������ �� ������, ������� � ������</h1>
				<p class="sep">����������� ������������ �������������� ������������ ����� �������� ��������, �������������� ������� �������� ������ �� ����������. ��������� ����, ��� ���������� �����������-�������������� �������� ����������� ���������� ���� �������, ������� �������� ��������������� ���� ����� ���������� ����������� ��������.</p>
				<p>���� ����������� ��������� ������ �� ��������� �������������� ��������������� �� ������, ������� � ������. � ����� ��������� ������ ���� ������������ ������� ������������ ������ � ������, � ����. ��� � ��������� �������� ���� 5, 10 � 20 ���� � ����� ����� ������, ���������� �������, � ����� ������.</p>
				<p>���������� ���� ������ � �������� �������� � ��������� ����� ��������, �������������� ��������������, ��������� ������������� ����� �������� ������� ������� ����� �� ��������������� � �������������� ���� ����� ��������, ��������������� ��� ������� ������. ���������� �������������� � ������������ �� ����� ������������ ����������� ��������, ��������� �������������� ����� ������������, ������������ ��� ����������� ����������.</p>
				<p>������������ ������������ � �������������� ������, ��� ���������� ������� ������� ������������ ������, ��� � ��������� ���������� �����, �� ����������� ��������, ������� � ���������� �������� ����� � ������������ �� ����� ������ ������������.</p>
			</div>
			</tr>
        </table>
      </div>
      
      <!--����� ��������--></td>
  </tr>
  <tr>
    <td><div class="black-line"> </div></td>
  </tr>
  <tr>
    <td><div class="content-wrapp">
        <table width="100%" border="0">
          <tr>
            <td style="vertical-align:top;"><!--����� ����-->
              
              <div class="left-bar bottom-left-bar"> <a href="#">������� ��������</a>
                <h3 class="yellow">��������������� �������</h3>
                <p><a href="#">�����</a></p>
                <p><a href="#">������ � ������</a></p>
                <p><a href="#">��� �������</a></p>
              </div>
              
              <!--����� ������ �����--></td>
            <td>�</td>
            <td style="vertical-align:top"><!--������ �������-->
              
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
	"DETAIL_PAGER_TITLE" => "��������",
	"DETAIL_PAGER_TEMPLATE" => "",
	"DETAIL_PAGER_SHOW_ALL" => "Y",
	"PAGER_TEMPLATE" => "",
	"DISPLAY_TOP_PAGER" => "N",
	"DISPLAY_BOTTOM_PAGER" => "N",
	"PAGER_TITLE" => "�������",
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
              
              <!--����� ������� ��������--></td>
          </tr>
        </table>
      </div></td>
  </tr>
  <tr>
    <td><!--������ �����-->
<?$APPLICATION->IncludeComponent("bitrix:main.feedback", "feeback", array(
	"USE_CAPTCHA" => "N",
	"OK_TEXT" => "�������, ���� ��������� �������.",
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
      <!--����� ������ �����--></td>
  </tr>
  <tr>
    <td><div class="content-wrapp">
        <table width="100%" border="0">
          <tr>
            <td style="vertical-align:top;"></td>
            <td>�</td>
            <td style="vertical-align:top"><!--������ �������-->
              
              <div class="content bottom-bottom-content">
                <h2>������</h2>
                <a href="#" class="arrow"></a>
                <div class="response">
                  <p class="response-title">������ ������� ���������, �� ����������-����������, ������</p>
                  <p>��������� �� � "������������" �� ������ ���, �� ��� ����� �������� ��������������� ��������, �� ������ �������� �� ����� ��������, � �������, �������������� ���������� �������������. �������!�</p>
                </div>
                <p class="link-wrapp"><a href="#" class="yellow detal">��� ������</a></p>
                <p class="link-wrapp"><a href="" onclick="return false;" class="yellow detal write_review">�������� �����</a></p>
              </div>
              
              <!--����� ������� ��������--></td>
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