$(document).ready(function () {
    var navElements = $('.main-menu .visible a');
    navElements.bind('click', function () {
        console.log($(this));
        navElements.removeClass('active');
        $(this).addClass('active');
    });

    var hiddenNav = $('.main-menu .hidden'),
        enrollForm = $('.enroll-form'),
        onlineButton = $('.o-button');

    $('.main-menu .visible li:not(.button-cont), .main-menu .hidden').hover(function () {
        hiddenNav.show();
    }, function () {
        hiddenNav.hide();
    });

    onlineButton.click(function () {
        enrollForm.toggle("slide", { direction: "up" }, 1000);
    });

    enrollForm.children('.title').click(function () {
        enrollForm.toggle("slide", { direction: "up" }, 1000);
    });
});