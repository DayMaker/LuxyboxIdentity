$(document).ready(function () {
    $("#btnDecrease").on("click", function () {
        var value = parseInt($("#inputPrice").val());

        value = value - 1;

        if (value < 1)
            value = 1;

        $("#inputPrice").val(value);

        var productId = $(this).closest(".product-item").attr("data-product-id");

        updateQuantity(productId, value);
       
    });

    $("#btnIncrease").on("click", function () {
        var value = parseInt($("#inputPrice").val());

        value = value + 1;

        if (value > 10)
            value = 10;

        $("#inputPrice").val(value);

        var productId = $(this).closest(".product-item").attr("data-product-id");

        updateQuantity(productId, value);
    });


    function updateQuantity(productId, quantity) {
        $.ajax({
            method: "POST",
            url: "/Home/CartItemQuantityUpdate",
            data: { productId: productId, quantity: quantity }
        })
            .done(function (msg) {
            });
    }


});

