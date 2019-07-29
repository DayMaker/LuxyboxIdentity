$(document).ready(function () {
    $("#btnDecrease").on("click", function () {
        var value = parseInt($("#inputPrice").val());

        value = value - 1;

        if (value < 1)
            value = 1;

        $("#inputPrice").val(value);

        var productId = $(this).closest(".product-item").attr("data-product-id");

        updateQuantity(productId, value);
        var productPrice = $(this).closest(".product-item").attr("data-product-price");
        productPrice = productPrice.replace(",", ".");
        console.log((parseFloat(productPrice) * value).toFixed(2));
        updateQuantity(productPrice, value);
       
    });

    $("#btnIncrease").on("click", function () {
        var value = parseInt($("#inputPrice").val());

        value = value + 1;

        if (value > 10)
            value = 10;

        $("#inputPrice").val(value);

        var productId = $(this).closest(".product-item").attr("data-product-id");
        updateQuantity(productId, value);
        var productPrice = $(this).closest(".product-item").attr("data-product-price");
        productPrice = productPrice.replace(",", ".");
        console.log((parseFloat(productPrice) * value).toFixed(2));
        
    });
    

    function updateQuantity(productId, quantity) {
        $.ajax({
            method: "POST",
            url: "/Home/CartItemQuantityUpdate",
            data: { productId: productId, quantity: quantity}
        })
            .done(function (msg) {
            });
    }


});

Number.prototype.round = function (p) {
    p = p || 10;
    return parseFloat(this.toFixed(p));
};