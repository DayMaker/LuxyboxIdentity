﻿$(document).ready(function () {
    totalPriceRefresh();
});

$(".btn-decrease").on("click", function () {
    ProductQuantityChange($(this), "-");
    totalPriceRefresh();
});

$(".btn-increase").on("click", function () {
    ProductQuantityChange($(this), "+");
    totalPriceRefresh();
});

$(".btn-delete").on("click", function () {
    var $productItem = $(this).closest(".product-item");
    var productId = $productItem.attr("data-product-id");
    updateDelete(productId, $productItem);
});

function ProductQuantityChange($this, type) {
    var $productItem = $this.closest(".product-item");
    var $inputQuantity = $productItem.find(".product-quantity");
    var productId = $productItem.attr("data-product-id");
    var productPrice = $productItem.attr("data-product-price");

    var value = parseInt($inputQuantity.val());

    if (type === "+") {
        value = value + 1;
        if (value > 10)
            value = 10;
    } else {
        value = value - 1;
        if (value < 1)
            value = 1;
    }


    $inputQuantity.val(value);
    updateQuantity(productId, value);

    productPrice = productPrice.replace(",", ".");
    productPrice = (parseFloat(productPrice) * value).toFixed(2);
    $productItem.find(".product-price").text(productPrice);
}


function totalPriceRefresh() {
    var totalPriceElements = $(".product-price");

    var totalPrice = 0;
    totalPriceElements.each(function (index) {
        totalPrice += parseFloat($(this).text().replace(",", "."));
    });


    $(".cart-total-price").text(totalPrice.toFixed(2));
}



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
                totalPriceRefresh();
                refreshCartIconCount();
            }
            // Do something with the result
        }
    });
}

function refreshCartIconCount() {
    var $productItems = $(".product-list .product-item");   
    $(".cart-icon .cart-icon-count").text($productItems.length);
}


Number.prototype.round = function (p) {
    p = p || 10;
    return parseFloat(this.toFixed(p));
};