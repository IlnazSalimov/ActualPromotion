(function ($) {
    var settings = {
        // Fullscreen?
        fullScreen: true,

        // Section Transitions?
        sectionTransitions: true,

        // Fade in speed (in ms).
        fadeInSpeed: 1000,
        minHeight: 640
    };

    $(function () {
        var $window = $(window),
            $body = $('body'),
            $header = $('#header'),
            $footer = $('#footer'),
            $all = $body.add($header).add($footer),
            sectionTransitionState = false;

        var newHeight = $window.height();
        $(".content").css('height', newHeight > settings.minHeight ? newHeight : settings.minHeight);

        // Resize.
        var resizeTimeout, resizeScrollTimeout;

        $window.resize(function () {
            window.clearTimeout(resizeTimeout);
            resizeTimeout = window.setTimeout(function () {
                // Resize fullscreen elements.
                $("body").css("overflow", "hidden");

                var newHeight = $window.height();
                $(".content").css('height', newHeight > settings.minHeight ? newHeight : settings.minHeight);
            }, 100);
        });
        // Trigger events on load.
        $window.load(function () {
            $window.trigger('resize');
        });
    });
})(jQuery);