$('label.fas.fa-star').click(function() {

    if ($(this).get(0).id == 'rate-1') {
        $(this).css("color", "black");
        ratingValue = 1;
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

$(".menu-option").click((element) => {

    var id = element.target.id
    $(".menu-option").html = id;
});
// review_box();
// $('.button-show-reviews-container>button').click(() => {
//     var totalHeight = 40 + $('.right-modal-top').outerHeight() + $('.reviews-box').outerHeight() + $('.give-review-button').outerHeight();
//     $('.reviews-box').toggle();
//     console.log(totalHeight);
//     $('.right-modal').height(totalHeight);

// });
var ratingValue = 1;
var ID = 0;


$('.stars-container input[type="radio"]').click(function() {
    ratingValue = parseInt($(this).val());
    console.log(ratingValue);
});
$('.confirm-review').click(() => {
    console.log(ratingValue);
    var reviewText = "";
    reviewText = $('#review_text').val();
    $('#review_text').val('');
    var reviewDish = { review: reviewText, rating: ratingValue };
    $.ajax({
        type: "GET",
        url: "/Home/Review",
        data: {
            review: reviewText,
            rating: ratingValue,
            ID: ID,
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(response) {
            //$('#dialog').html(response);
            fillReviews(response);
        },
       
        failure: function(response) {
            alert("Falla");
        },
        error: function(response) {
            alert("Falla");

        }
    });
})
$(".container>main.cards-container.cards>div.card>a").hover(
    function() {
        alert("aaa");
    },
    function() {

    }
);


$(document).on('click', '.active-modal', function(element) {
    $('label#rate-1').css("color", "black");
    // $('.right-modal').height(totalHeight);
    //$("#rate-1").attr('checked', 'checked'); activar
    //$('div.modal-bg').addClass('hidden'); activar

    //Esto es una prueba
    //var data = {
    //    review: {
    //        ID: element.target.id
    //    }};

    ID = element.target.id;
    //var idReal = e.attr.ID;

    $.ajax({
        type: "GET",
        url: "/Home/Modal",
        data: { ID: ID },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(response) {
        
            fillReviews(response);
            $('form.send-review-box').show();
            $('div.modal-bg').addClass('hidden');
            $("#rate-1").attr('checked', 'checked');
        },
        failure: function(response) {
            // alert("Falla");
        },
        error: function(response) {
            //alert("Falla");
        }
    });

});
//$('.active-modal').click((element) => {


//    //$('form.send-review-box').show(); activar
//    // var totalHeight = 40 + $('.right-modal-top').outerHeight() + $('.reviews-box').outerHeight() + $('.give-review-button').outerHeight();

//});

//Aqui puedo hacer maravillas claro que si
function fillReviews(response) {
    //"@String.Format("data:image/jpg;base64,{0}",Convert.ToBase64String(Model.imageBinary))" )"
    var stars = "";
    $('.reviews-box').empty();
    for (let j = 0; j < response.rate; j++) {

        stars += '<li><i class="fas fa-star"></i></li>';
    }
    for (let j = response.rate; j < 5; j++) {
        stars += '<li><i class="far fa-star"></i></li>';
    }
    //for (let j = 0; j < 3; j++) {

    //    stars += '<li><i class="fas fa-star"></i></li>';
    //}
    $('#dish_description').html(response.description);
    $('#dish_name').html(response.name);
    if (response.numberReviews == 1) {
        $('#number_reviews').html('Basado en ' + response.numberReviews + ' reseña');
    } else if (response.numberReviews == 0) {
        $('#number_reviews').html('0 reseñas');
    } else {
        $('#number_reviews').html('Basado en ' + response.numberReviews + ' reseñas');
    }

 
    $('#real_rate').html(response.realRate)
    $('#review_image').attr('src', "data:image/jpg;base64," + response.imageData)
    $('#dish_stars').html(stars);

  
    for (let i = 0; i < response.reviews.length; i++) {

        var starsUserUl = $('<ul></ul>');
       

       var starsLiValue = $('<li></li>');
        var starsValue = $('<span></span>').text(response.reviews[i].rating);
        starsLiValue.append(starsValue);
        for (let j = 0; j < parseInt(response.reviews[i].rating, 10); j++) {
            var starActive = $('<i></i>').addClass('fas fa-star');
            var starsUserli = $('<li></li>');
            starsUserli.append(starActive);
            starsUserUl.append(starsUserli);
        }

        for (let j = parseInt(response.reviews[i].rating, 10); j < 5; j++) {
            var starsInactive = $('<i></i>').addClass('far fa-star');
            var starsUserli = $('<li></li>');
            starsUserli.append(starsInactive);
            starsUserUl.append(starsUserli);
        }

     
        starsUserUl.append(starsLiValue);

        var reviewBox = $('<div></div>').addClass("review");
        var topReview = $('<div></div>').addClass('top-review');
        var userReaction = $('<section></section>').addClass('user-reaction');
        var reaction = $('<i></i>').addClass('far');

        if (response.reviews[i].rating < 3) {
            reaction.addClass('fa-frown');
        } else if (response.reviews[i].rating == 3) {
            reaction.addClass('fa-meh');
        } else if (response.reviews[i].rating > 3){
            reaction.addClass('fa-smile');
        }
        var userRating = $('<section></section>').addClass('user-rating');
        var faceNameRate = $('<div></div>').addClass('face-name-date');

        var userName = $('<h2></h2>').text(response.reviews[i].nombreUsuario);

        var sectionDays = $('<section></section>').addClass('days');

        var days = $('<span></span>').text(response.reviews[i].finalResult);

        var bottomReview = $('<div></div>').addClass('bottom-review');

        var reviewText = $('<p></p>').text(response.reviews[i].review);

        userReaction.append(reaction);
        topReview.append(userReaction);

        faceNameRate.append(userName);

        userRating.append(faceNameRate)

        userRating.append(starsUserUl);

        topReview.append(userRating);

        sectionDays.append(days);
        topReview.append(sectionDays);

        reviewBox.append(topReview);

        bottomReview.append(reviewText);


        reviewBox.append(bottomReview);


        $('.reviews-box').hide();

        $('.reviews-box').append(reviewBox);

        $('.reviews-box').show("slow");
    
    }
    
    if (response.reviews.length == 0) {
        $('.reviews-box').hide();
        var message = $('<div></div>').addClass('first-review-message');
        var messageText = $('<h1></h1>').text('¡Se el primero en dar una reseña!');
        message.append(messageText);
        $('.reviews-box').append(message);
        $('.reviews-box').show('slow');

    }
  

}
$('.close-modal').click(() => {
    $('div.modal-bg').removeClass('hidden');
    $('#review_text').prop('placeholder', "Escribe tu reseña");
    $('#review_text').removeClass('error-color')
});

// $('.review-button').click(() => {



//     $('form.send-review-box').show();


// });