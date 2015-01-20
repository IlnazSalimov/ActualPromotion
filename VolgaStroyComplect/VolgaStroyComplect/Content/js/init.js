function getCompanyWorks(callBack) {
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: "/Articles/GetCompanyWorks",
            success: function (response) {
                callBack(response);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log("request failed");
            },

            processData: false,
            async: false
        });
}

function showOrHidePageLogo(showingBlock) {
    var blocksCollection = $(".block_ins");
    if (showingBlock.hasClass("partner")) {
        if ($(window).height() < 700 || blocksCollection.index(showingBlock) == blocksCollection.length - 1) {
            $(".bottom_arrows_container").hide();
        }

        if ($(window).height() >= 800) {
            if (!showingBlock.hasClass("products")) {
                $(".page_logo").css("background-image", "url('../Content/images/partners.png')");
            }
        }
    }
    else {
        $(".bottom_arrows_container").show();
        if ($(window).height() >= 860) {
            if (!showingBlock.hasClass("products")) {
                $(".page_logo").css("background-image", "url('../Content/images/about_company.png')");
            }
        }
    }
}
var isBusy = false;
function toogleBlock(showingBlockIndex /*Индекс DOM элемента в коллекции $(".block_ins")*/, duration, onComplete, isRemove) {
    if (isBusy) {
        return false;
    }
    isBusy = true;
    var blocksCollection = $(".block_ins"),
        currentBlock = $(".block_ins.current"),
        currentBlockIndex = blocksCollection.index(currentBlock);

    var isFirstElement = currentBlockIndex == 0;
    var isLastElement = currentBlockIndex == blocksCollection.length - 1;
    if (showingBlockIndex != -1 && showingBlockIndex > currentBlockIndex && !isLastElement) {
        var showingBlock = showingBlockIndex === undefined ? currentBlock.next() : blocksCollection.eq(showingBlockIndex);

        currentBlock.toggle("drop", { direction: (isRemove !== undefined && isRemove ? "right" : "up") }, duration, function () {
            currentBlock.removeClass("current");
            showOrHidePageLogo(showingBlock);
            showingBlock.addClass("current").toggle("drop", { direction: "down" }, duration, function () {
                activeElementIndex = blocksCollection.index(showingBlock);
                if (activeElementIndex !== undefined) {
                    $(".unit_nav").removeClass("active");
                    $(".unit_nav").eq(activeElementIndex).addClass("active");
                }
                onComplete(activeElementIndex);
                isBusy = false;
            });
        });
    }
    else if (showingBlockIndex != -1 && showingBlockIndex < currentBlockIndex && !isFirstElement) {
        var showingBlock = showingBlockIndex === undefined ? currentBlock.prev() : blocksCollection.eq(showingBlockIndex);

        currentBlock.toggle("drop", { direction: (isRemove !== undefined && isRemove ? "right" : "down") }, duration, function () {
            currentBlock.removeClass("current");
            showOrHidePageLogo(showingBlock);
            showingBlock.addClass("current").toggle("drop", { direction: "up" }, duration, function () {
                activeElementIndex = blocksCollection.index(showingBlock);
                if (activeElementIndex !== undefined) {
                    $(".unit_nav").removeClass("active");
                    $(".unit_nav").eq(activeElementIndex).addClass("active");
                }
                onComplete(activeElementIndex);
                isBusy = false;
            });
        });
    }
    else {
        onComplete();
        isBusy = false;
    }
    
}

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
        minWidth: 990,
        minHeight: 480
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
        //$(".content .cont").css('width', ((newWidth > settings.minWidth ? newWidth : settings.minWidth) * 3));

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
                //$(".content .cont").css('width', ((newWidth > settings.minWidth ? newWidth : settings.minWidth) * 3));
            }, 100);
        });
        // Trigger events on load.
        $window.load(function () {
            $window.trigger('resize');
        });

        $(".prevButton").bind("click", function () {
            var owl = $(".owl-carousel").data('owlCarousel');
            prev(owl);
        });

        $(".nextButton").bind("click", function () {
            var owl = $(".owl-carousel").data('owlCarousel');
            next(owl);
        });

        $(".slide_switch").bind("click", function () {
            var owl = $(".owl-carousel").data('owlCarousel');
            var slideId = $(this).attr("data-id");
            owl.goTo(slideId);
        });

        if ($("#owl-demo").length > 0










            +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++) {
            $("#owl-demo").owlCarousel({
                navigation: false, // Show next and prev buttons
                pagination: false,
                slideSpeed: 300,
                paginationSpeed: 400,
                singleItem: true,
                mouseDrag: false,
                touchDrag: true,
                afterMove: after,
                startDragging: dragg,
            });
        }

        var isDragg = false;
        
        function after() {
            var owl = $(".owl-carousel").data('owlCarousel');
            markActualElementsToActive(owl);

            if (!isDragg) {
                return false;
            }

            if (owl.currentItem >= currentSlideIndex()) {
                next(owl);
            }
            else {
                prev(owl);
            }
            isDragg = false;
        }

        function markActualElementsToActive(owl) {
            $('.slide_switch').removeClass('active');
            $('.slide_switch[data-id="' + owl.currentItem + '"]').addClass('active');
            $('.slider').removeClass('active');
            $('.slider[data-id="' + owl.currentItem + '"]').addClass('active');
        }

        function dragg() {
            isDragg = true;
        }

        function prev(owl) {
            if (!isDragg) {
                owl.prev();
            }
        }

        function next(owl) {
            if (!isDragg) {
                owl.next();
            }
        }



        function currentSlideIndex() {
            var currentSlideIndex = $('.slider.active').attr('data-id');
            console.log('currentSlideIndex: ' + currentSlideIndex);
            return currentSlideIndex != undefined && (currentSlideIndex >= 0 && currentSlideIndex <= 2) ? currentSlideIndex : 0;
        }

        var isBusy = false;
        $(document).mousewheel(function (event, delta) {
            if (isBusy) {
                return false;
            }
            isBusy = true;
            if (delta < 0) {
                var blockIndexToShow = $(".block_ins").index($(".block_ins.current").next());
                toogleBlock(blockIndexToShow, 350, function (activeElementIndex) {
                    isBusy = false;
                    if (activeElementIndex !== undefined) {
                        $(".unit_nav").removeClass("active");
                        $(".unit_nav").eq(activeElementIndex).addClass("active");
                    }
                });
            }

            if (delta > 0) {
                var blockIndexToShow = $(".block_ins").index($(".block_ins.current").prev());
                toogleBlock(blockIndexToShow, 350, function (activeElementIndex) {
                    isBusy = false;
                    if (activeElementIndex !== undefined) {
                        $(".unit_nav").removeClass("active");
                        $(".unit_nav").eq(activeElementIndex).addClass("active");
                    }
                });
            }
        });
    });
})(jQuery);



