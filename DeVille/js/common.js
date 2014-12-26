$(document).ready(function () {
    var navElements = $('.main-menu .visible a');
    navElements.bind('click', function () {
        navElements.removeClass('active');
        $(this).addClass('active');
    });

    var hiddenNav = $('.main-menu .hidden');
    $('.main-menu .visible li:not(.button), .main-menu .hidden').hover(function () {
        hiddenNav.show();
    }, function () {
        hiddenNav.hide();
    });
});