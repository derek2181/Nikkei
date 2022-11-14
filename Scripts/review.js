$('label.fas.fa-star').click(function() {

    if ($(this).get(0).id == 'rate-1') {
        $(this).css("color", "black");
        $('label#rate-2').css("color", " rgb(192, 192, 192)");
        $('label#rate-3').css("color", " rgb(192, 192, 192)");
        $('label#rate-4').css("color", " rgb(192, 192, 192)");
        $('label#rate-5').css("color", " rgb(192, 192, 192)");
    }
    if ($(this).get(0).id == 'rate-2') {
        $(this).css("color", "black");
        $('label#rate-1').css("color", "black");
        $('label#rate-3').css("color", " rgb(192, 192, 192)");
        $('label#rate-4').css("color", " rgb(192, 192, 192)");
        $('label#rate-5').css("color", " rgb(192, 192, 192)");
    }
    if ($(this).get(0).id == 'rate-3') {
        $(this).css("color", "black");
        $('label#rate-1').css("color", "black");
        $('label#rate-2').css("color", "black");
        $('label#rate-4').css("color", " rgb(192, 192, 192)");
        $('label#rate-5').css("color", " rgb(192, 192, 192)");
    }
    if ($(this).get(0).id == 'rate-4') {
        $(this).css("color", "black");
        $('label#rate-1').css("color", "black");
        $('label#rate-2').css("color", "black");
        $('label#rate-3').css("color", "black");
        $('label#rate-5').css("color", " rgb(192, 192, 192)");

    }
    if ($(this).get(0).id == 'rate-5') {
        $(this).css("color", "black");
        $('label#rate-1').css("color", "black");
        $('label#rate-2').css("color", "black");
        $('label#rate-3').css("color", "black");
        $('label#rate-4').css("color", "black");
    }


});

// review_box();
// $('.button-show-reviews-container>button').click(() => {
//     var totalHeight = 40 + $('.right-modal-top').outerHeight() + $('.reviews-box').outerHeight() + $('.give-review-button').outerHeight();
//     $('.reviews-box').toggle();
//     console.log(totalHeight);
//     $('.right-modal').height(totalHeight);

// });


$('.active-modal').click(() => {


    $('form.send-review-box').show();
    // var totalHeight = 40 + $('.right-modal-top').outerHeight() + $('.reviews-box').outerHeight() + $('.give-review-button').outerHeight();
    $('label#rate-1').css("color", "black");
    // $('.right-modal').height(totalHeight);
    $("#rate-1").attr('checked', 'checked');
    $('div.modal-bg').addClass('hidden');


});

$('.close-modal').click(() => {
    $('div.modal-bg').removeClass('hidden');

});

// $('.review-button').click(() => {



//     $('form.send-review-box').show();


// });