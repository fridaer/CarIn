$(document).ready(function () {
    $("#tooltipbox").hide();

    $("#olyckaBtn").click(function(){
        var message = "Information om olyckor som orsakar trafikstörningar";
        alertInTooltipbox(message);
        
    });
    $("#tullarBtn").click(function () {
        var message = "Information om tullarna och kostnader";
        alertInTooltipbox(message);

    });
    $("#vaderBtn").click(function () {
        var message = "Väderinformation";
        alertInTooltipbox(message);

    });
    $("#lokalBtn").click(function () {
        var message = "Information om lokaltrafikstörningar";
        alertInTooltipbox(message);

    });
    $("#installningarBtn").click(function () {
        alert("Inställningar");

    });

    function alertInTooltipbox(message) {
        $("#tooltipbox").html(message);
        $("#tooltipbox").show(200);
    }
});



