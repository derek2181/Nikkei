var labelID;

$('#persons_book').val(1);

$('.modal-bg').click(() => {
    $('.modal-bg').hide();
})
$('#book_date').click(function () {

    $('#date').datepicker('open');
    // labelID = $(this).attr('for');

    // $('#' + labelID).trigger('click');


});




$('#plus_one').click(() => {
    var currentNumber = $('#persons_book').val();

    if (currentNumber > 9) {
        currentNumber = 10;

    } else {
        currentNumber++;
    }
    $('#persons_book').val(currentNumber);

});

$('#minus_one').click(() => {
    var currentNumber = $('#persons_book').val();

    if (currentNumber < 2) {
        currentNumber = 1;

    } else {
        currentNumber--;
    }
    $('#persons_book').val(currentNumber);

});
$('.error-book').hide();
$('.success-book').hide();

$('.close-error-book').click(() => {
    $('.error-book').fadeOut(200, function () {
        $(this).hide();
    });
    $('.success-book').fadeOut(200, function () {
        $(this).hide();
    });

})


function toggleError(element) {
    if (element == 2) {
        $('.error-book').toggle();
    }
    if (element == 1) {
        $('.success-book').toggle();
    }
   
}
$('#persons_book').keypress((e) => {
    e.preventDefault();

    $(this).blur();
});
$('.modal-bg').hide();
// $('input#date').click(() => {
//     $('.modal-bg').show();
// });


$('#persons_book').focus(() => {
    $('#persons_book').blur();
});
$(".hours-container input:first").attr('checked', 'checked');