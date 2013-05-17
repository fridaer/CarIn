$(document).ready(function () {
    $("#tooltipbox").hide();

    $("#olyckaBtn").click(function(){
        var message = "Information om olyckor som orsakar trafikst�rningar";
        alertInTooltipbox(message);
        
    });
    $("#tullarBtn").click(function () {
        var message = "Information om tullarna och kostnader";
        alertInTooltipbox(message);

    });
    $("#vaderBtn").click(function () {
        var message = "V�derinformation";
        alertInTooltipbox(message);

    });
    $("#lokalBtn").click(function () {
        var message = "Information om lokaltrafikst�rningar";
        alertInTooltipbox(message);

    });
    $("#installningarBtn").click(function () {
        alert("Inst�llningar");

    });

    function alertInTooltipbox(message) {
        $("#tooltipbox").html(message);
        $("#tooltipbox").show(200);
    }
});



