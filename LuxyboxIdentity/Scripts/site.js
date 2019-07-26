$(document).ready(function () {
    $("#btnDecrease").on("click", function () {
        var value = parseInt($("#inputPrice").val());

        value = value - 1;

        if (value < 1)
            value = 1;

        $("#inputPrice").val(value)
    });

    $("#btnIncrease").on("click", function () {
        var value = parseInt($("#inputPrice").val());

        value = value + 1;

        if (value > 10)
            value = 10;

        $("#inputPrice").val(value)
    });

});