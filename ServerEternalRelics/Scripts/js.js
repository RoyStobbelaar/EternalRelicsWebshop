/* Javascript file for shopping cart etc. */

$(function () {

    $("#addedToCart").dialog({
        autoOpen: false,
        height: 300,
        width: 350,
        modal: true,
        close: function (event, ui) {
            setTimeout("location.reload(true)", 0);
        }
    })

    $("#removedFromCart").dialog({
        autoOpen: false,
        height: 300,
        width: 350,
        modal: true,
        close: function (event, ui) {
            setTimeout("location.reload(true)", 0);
        }
    })

});


function Add(productId, amount) {

    console.log(productId, amount);

    $("#addedToCart").dialog("open");


    $.ajax({
        type: 'POST',
        url:'/ShoppingCart/AddToCart/',
        data: { productId: productId, amount: amount },
        success: function (data) {
        },
        failure: function (response) {
            alert(response.d);
        }
    })
}

function Remove(productId, amount) {

    console.log(productId, amount);

    $("#removedFromCart").dialog("open");

    $.ajax({
        type: 'POST',
        url: '/ShoppingCart/RemoveFromCart/',
        data: { productId: productId, amount: amount },
        success: function (data) {
        },
        failure: function (response) {
            alert(response.d);
        }
    })
};
