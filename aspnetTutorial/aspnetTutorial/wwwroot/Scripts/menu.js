//var wasClicked = 0;

var _globalSelection = "Roll";
//$('.clickable-image').click((element) => {
//    var selection = element.target.id;
//    getDishesAjaxRequest(selection);
//    wasClicked = 1;
//    console.log("Hice click en clickable image");
//});
//$('owl-item').click((element) = {
//    alert("AAAA");
//    var selection = element.children("a").attr("id");

//    getDishesAjaxRequest(selection);
//});
//$('.item').click((element) => {
//    var selection = element.target.id;
//    getDishesAjaxRequest(selection);

//});
//$('a.image-menu').click((element) => {
//    var selection = element.target.id;
//    getDishesAjaxRequest(selection);
//    });

$(document).click(function (event) {

    var text = $(event.target).attr('class');
    var id = $(event.target).attr('id');
    //console.log(text);
    //console.log("Es:" + wasClicked);

    if (text == "clickable-image") {
        getDishesAjaxRequest(id);
    }
   var currentSelection= $('.menu-option').attr('id')
});

function loading() {
    /***** Element 1 *****/
    // Initialize Progress and show LoadingOverlay
    var progress1 = new LoadingOverlayProgress();
    $(".cards").LoadingOverlay("show", {
        custom: progress1.Init()
    });
    // Simulate some action:
    var count1 = 0;
    var iid1 = setInterval(function () {
        if (count1 >= 100) {
            clearInterval(iid1);
         
            $(".cards").LoadingOverlay("hide");
            return;
        }
        count1++;
        progress1.Update(count1);
    }, 100);
    /*********************/

}
        /***** Element 2 *****/
      
function spinner() {

}

function getDishesAjaxRequest(selection) {
  
    _globalSelection = selection;
    $('.menu-option').text(selection);
    $.ajax({
        type: "GET",
        url: "/Home/GetSelection",
        data: {
            selection: selection
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $(".cards-container").LoadingOverlay("hide");
            fillMenu(response);
        },
     
        beforeSend: function (xhr) {
            var progress1 = new LoadingOverlayProgress();
            $(".cards-container").LoadingOverlay("show");
        },
        failure: function (response) {
            alert("Falla");
        },
        error: function (response) {
            alert("Falla");

        }
    });
}

function fillMenu(response) {
    var finalFill = "";

    var notFoundContainer = $("<div></div>").addClass("not-found-message");

    var imgNotFound = $('<img/>', {
        src: "/Images/Background/food_onigirazu.png"
    });

    var apology = $('<h1></h1>').text('Lo sentimos, intenta buscar con otro precio');

    notFoundContainer.append(imgNotFound);
    notFoundContainer.append(apology);

    var containerParent = $('.cards');
    var not_found = $('.not-found-message');
    containerParent.html = "";
    clearContents();
   
    for (let i = 0; i < response.data.length; i++) {


        var parent = $("<div></div>").addClass("card");


 
        var anchorTag = $('<a></a>');
        var dishName = $('<h1></h1>').text(response.data[i].name);
        var price = $('<h2></h2>').text(response.data[i].price);
        var clickHere = $('<h3></h3>').text("Haz click para ver las reseñas");
        //var button = $('<button></button>').addClass('active-modal');
        var hidden = $('<div></div>').addClass('hidden-card-content');
        if (response.sesion) {
            anchorTag.attr("id", response.data[i].id);
            hidden.attr("id", response.data[i].id); 
            dishName.attr("id", response.data[i].id);
            price.attr("id", response.data[i].id);
            clickHere.attr("id", response.data[i].id);

            anchorTag.addClass('active-modal');
        } else {
            anchorTag.attr("href", "/Nikkei/Login/SignIn");

        }
    
        var img = $('<img/>', {
            id: response.data[i].id,
            src: "data:image/jpg;base64," + response.data[i].imageBinary
        });

        anchorTag.append(img)

        hidden.append(dishName);
        hidden.append(price);
        hidden.append(clickHere);
        anchorTag.append(hidden);
        parent.append(anchorTag);

        containerParent.hide();
        containerParent.append(parent);

    


        containerParent.show('fast');
       
    }

    if (response.data.length == 0) {
        
            containerParent.append(notFoundContainer);
            containerParent.show('fast');
        
    } 

   
}



function clearContents() {
    $('.cards').empty();
}
$('.fas.fa-arrow-alt-circle-up').click(() => {

    $("html, body").animate({
        scrollTop: "0"
    }, 1000);

});

//$("#give_review_aux").on("click", function () {
//    var path = $(this).attr("data-path");
//    var anchor = $("#" + path);
//    var position = anchor.position().top;
//    $("#container").animate({ scrollTop: position });
//});



    const slideValue = document.querySelector("span.slider-number");
    const inputSlider = document.querySelector("input#precio_slide");
    var value = 0;
    inputSlider.oninput = (() => {

        value = inputSlider.value;
        slideValue.textContent = value;

        slideValue.style.left = ((value) / 2) + "%";
        slideValue.classList.add("show");

    });



    $('.not-found-message').hide();
    inputSlider.onchange = (() => {
        slideValue.classList.remove("show");
        _globalSelection
        getDishesByPriceAjaxRequest(_globalSelection, value)
    });


    function getDishesByPriceAjaxRequest(selection, value) {
        $.ajax({
            type: "GET",
            url: "/Home/GetSelectionByPrice",
            data: {
                selection: selection,
                price: value
            },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                fillMenu(response);
            },
            failure: function (response) {
                alert("Falla");
            },
            error: function (response) {
                alert("Falla");

            }
        });
    }
