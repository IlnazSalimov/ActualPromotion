(function ($) {
    var delay = 0;
    $.fn.translate3d = function (translations, speed, easing, complete) {
        var opt = $.speed(speed, easing, complete);
        opt.easing = opt.easing || 'ease';
        translations = $.extend({ x: 0, y: 0, z: 0 }, translations);

        return this.each(function () {
            var $this = $(this);

            $this.css({
                transitionDuration: opt.duration + 'ms',
                transitionTimingFunction: opt.easing,
                transform: 'translate3d(' + translations.x + '%, ' + translations.y + 'px, ' + translations.z + 'px)'
            });

            setTimeout(function () {
                $this.css({
                    transitionDuration: '0s',
                    transitionTimingFunction: 'ease'
                });

                opt.complete();
            }, opt.duration + (delay || 0));
        });
    };

    var settings = {
        // Fullscreen?
        fullScreen: true,

        // Section Transitions?
        sectionTransitions: true,

        // Fade in speed (in ms).
        fadeInSpeed: 1000,
        minWidth: 800,
        minHeight: 480,

    };

    $(function () {
        var $window = $(window),
            $body = $('body'),
            $header = $('#header'),
            $footer = $('#footer'),
            $all = $body.add($header).add($footer),
            sectionTransitionState = false;

        $("body").css("overflow", "hidden");

        var newWidth = $window.width();
        var newHeight = $window.height() - $header.outerHeight() - $footer.outerHeight();
        $(".content .slider").css('height', newHeight > settings.minHeight ? newHeight : settings.minHeight);
        $(".content .slider").css('width', newWidth > settings.minWidth ? newWidth : settings.minWidth);
        $(".content .container").css('width', ((newWidth > settings.minWidth ? newWidth : settings.minWidth) * 3));

        // Resize.
        var resizeTimeout, resizeScrollTimeout;

        $window.resize(function () {
            window.clearTimeout(resizeTimeout);
            resizeTimeout = window.setTimeout(function () {
                // Resize fullscreen elements.
                $("body").css("overflow", "hidden");

                var newWidth = $window.width();
                var newHeight = $window.height() - $header.outerHeight() - $footer.outerHeight();
                $(".content .slider").css('height', newHeight > settings.minHeight ? newHeight : settings.minHeight);
                $(".content .slider").css('width', newWidth > settings.minWidth ? newWidth : settings.minWidth);
                $(".content .container").css('width', ((newWidth > settings.minWidth ? newWidth : settings.minWidth) * 3));
            }, 100);
        });
        // Trigger events on load.
        $window.load(function () {
            $window.trigger('resize');
        });


        $(".prevButton").bind("click", function () {
            if ($(".container").hasClass(SLIDER1)) {
                switchSlider(SLIDER3);
            }
            else if ($(".container").hasClass(SLIDER2)) {
                switchSlider(SLIDER1);
            }
            else if ($(".container").hasClass(SLIDER3)) {
                switchSlider(SLIDER2);
            }
        });

        $(".nextButton").bind("click", function () {
            if ($(".container").hasClass(SLIDER1)) {
                switchSlider(SLIDER2);
            }
            else if ($(".container").hasClass(SLIDER2)) {
                switchSlider(SLIDER3);
            }
            else if ($(".container").hasClass(SLIDER3)) {
                switchSlider(SLIDER1);
            }
        });

        $(".slide_switch").bind("click", function () {
            switch ($(this).attr("data-id")) {
                case "0": switchSlider(SLIDER1); break;
                case "1": switchSlider(SLIDER2); break;
                case "2": switchSlider(SLIDER3); break;
            }
        });
    });
})(jQuery);