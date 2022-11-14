


if ($.trim($("#Name").html()) != '') {
    alert(5);
    $("#NameForm").addClass("error-input");
    //alert($("#Name").text().length );
} else {
    alert($.trim($("#Name").html()));
    //$("#NameForm").addClass("error-input");
}