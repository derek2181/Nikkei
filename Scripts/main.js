$(document).ready(function() {

    $(window).scroll(function() {
        menu_fixed();
        buttons_responsive();
        console.log($(this).scrollTop());
        // if ($(this).scrollTop() > 120) {
        //     $('div.cards-container.first').animate({
        //         opacity: '1.0',
        //     }, 600);
        //     console.log("ayuda");
        // }
        // if ($(this).scrollTop() > 1100) {
        //     $('div.cards-container.second').animate({
        //         opacity: '1.0',
        //     }, 800);
        //     console.log("ayuda");
        // }
    });


    menu_fixed();

    function menu_fixed() {
        console.log($(this).scrollTop());
        if ($(this).scrollTop() > 0) {
            $('header').addClass('fixed');
        } else $('header').removeClass('fixed');

    }

    function buttons_responsive() {

        // if ($(document).scrollTop() + $(window).height() + 2 <= $(".container").height()) {
        //     console.log("fue true");
        //     console.log($(document).scrollTop() + $(window).height() + " Tamaño de scroll");
        //     console.log($(".container").height() + " tamaño de container");
        //     $("div.header-buttons-responsive").addClass('buttons-responsive-fixed');
        // } else {
        //     console.log("fue false");
        //     console.log($(document).scrollTop() + $(window).height() + " Tamaño de scroll");
        //     console.log($(".container").height() + " tamaño de container");
        //     $("div.header-buttons-responsive").removeClass('buttons-responsive-fixed');
        // }
    }

    //Owl carousel

    $('.owl-carousel').owlCarousel({
        loop: true,
        margin: 0,
        nav: false,
        dots: false,
        center: true,

        autoplay: true,

        autoplayTimeout: 3000,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
                margin: 50,
                autoHeight: true,
                nav: true,
                autoplayTimeout: 2000,
            },
            600: {
                items: 3,
                nav: true
            },
            1000: {
                items: 5,
                nav: true,

            }
        }
    })



})