var SLIDER1 = "run1",
    SLIDER2 = "run2",
    SLIDER3 = "run3";

function switchSlider(slider) {
    $(".container").removeClass(SLIDER1).removeClass(SLIDER2).removeClass(SLIDER3);
    $(".slide_switch").removeClass("active");
    switch (slider) {
        case SLIDER1: {
            $(".container").addClass(SLIDER1);
            $(".slide_switch[data-id='0']").addClass("active");
            $(".container").translate3d({ x: 0, y: 0, z: 0 }, 500, true, function () { });
        } break;
        case SLIDER2: {
            $(".container").addClass(SLIDER2);
            $(".slide_switch[data-id='1']").addClass("active");
            $(".container").translate3d({ x: -33.333333, y: 0, z: 0 }, 500, true, function () { });
        } break;
        case SLIDER3: {
            $(".container").addClass(SLIDER3);
            $(".slide_switch[data-id='2']").addClass("active");
            $(".container").translate3d({ x: -66.666666, y: 0, z: 0 }, 500, true, function () { });
        } break;
        default: { };
    }
}