$(document).ready(function() {

    //$('div.cards-container.first').lazyload({
    //    treshold: 150,
    //    effect: 'fadeIn'

    //});

    $(window).scroll(function() {
        menu_fixed();
        buttons_responsive();
        //console.log($(this).scrollTop());
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
        //console.log($(this).scrollTop());
        if ($(this).scrollTop() > 0) {
            $('header').addClass('fixed').fadeIn(500);
        } else $('header').removeClass('fixed').fadeIn(500);

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

    $('#menu.owl-carousel').owlCarousel({
        loop: true,
        margin: 0,
        nav: false,
        dots: false,
        center: true,

        autoplay: false,

        autoplayTimeout: 4000,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
                margin: 50,
                autoHeight: true,
                nav: true,
                // autoplayTimeout: 2,
            },
            900: {
                items: 3,
                // nav: true
            },
            1400: {
                items: 5,
                // nav: true,

            }
        }
    })

    $('div#landing.owl-carousel').owlCarousel({
        loop: true,
        margin: 0,
        nav: false,
        dots: true,
        center: true,

        autoplay: true,

        autoplayTimeout: 4000,
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
                items: 1,
                // nav: truek
            },
            1000: {
                items: 1,
                // nav: true,

            }
        }
    })

    $('#give_review_aux').click(function () {
        var position = $("button#send_review_button").position().top;
        $('.modal-box').animate({
            scrollTop: $(".modal-box").offset().top + position
        }, 1000);
    });
    //if ($.trim($("#Name").html()) != '') {
    //    alert(5);
    //    $("#NameForm").addClass("error-input");
    //    //alert($("#Name").text().length );
    //} else {
    //    alert($.trim($("#Name").html()));
    //    //$("#NameForm").addClass("error-input");
    //}
 
})

