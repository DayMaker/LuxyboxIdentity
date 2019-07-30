$(document).ready(function () {
    $(".btn-decrease").on("click", function () {
        var $productItem = $(this).closest(".product-item");
        var $inputQuantity = $productItem.find(".product-quantity");
        var productId = $productItem.attr("data-product-id");
        var productPrice = $productItem.attr("data-product-price");
        var cardPrice = $cardPriceItem.attr("card-price");

        var value = parseInt($inputQuantity.val());
        value = value - 1;
        if (value < 1)
            value = 1;

        $inputQuantity.val(value);
        updateQuantity(productId, value);

        productPrice = productPrice.replace(",", ".");
        productPrice = (parseFloat(productPrice) * value).toFixed(2);
        $productItem.find(".product-price").text(productPrice);

    });

    $(".btn-increase").on("click", function () {
        var $productItem = $(this).closest(".product-item");
        var $inputQuantity = $productItem.find(".product-quantity");
        var productId = $productItem.attr("data-product-id");
        var productPrice = $productItem.attr("data-product-price");

        var value = parseInt($inputQuantity.val());
        value = value + 1;
        if (value > 10)
            value = 10;

        $inputQuantity.val(value);
        updateQuantity(productId, value);

        productPrice = productPrice.replace(",", ".");
        productPrice = (parseFloat(productPrice) * value).toFixed(2);
        $productItem.find(".product-price").text(productPrice);
    });


    $(".btn-delete").on("click", function () {
        var $productItem = $(this).closest(".product-item");
        var productId = $productItem.attr("data-product-id");
        updateDelete(productId, $productItem);
    });
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
function updateDelete(productId, $productItem) {
    $.ajax({
        url: "/Home/CartItemDeleteUpdate",
        type: 'POST',
        data: { productId: productId },
        success: function (r) {
            if (r.result === true) {
                $productItem.remove();
            }
            // Do something with the result
        }
    });
}




Number.prototype.round = function (p) {
    p = p || 10;
    return parseFloat(this.toFixed(p));
};