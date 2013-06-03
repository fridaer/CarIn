$(document).ready(function () {
    $("#tooltipbox").hide();

    $("#olyckaBtn").click(function (event) {
        event.preventDefault();
        var message = "Information om olyckor som orsakar trafikst&#246;rningar";
        $(".traffic-problem").toggle();
        $(".traffic-problem2").toggle();
        $("path").toggle();
        alertInTooltipbox(message );
        
    });
    $("#tullarBtn").click(function (event) {
        event.preventDefault();
        var message = "Information om tullarna och kostnader";
        $(".TollsMarker").toggle();
        alertInTooltipbox(message);

    });
    $("#vaderBtn").click(function (event) {
        event.preventDefault();
        var message = "V&#228;derinformation";
        alertInTooltipbox(message);

    });
    $("#lokalBtn").click(function (event) {
        event.preventDefault();
        var message = "Information om lokaltrafikst&#246;ningar";
        alertInTooltipbox(message);

    });
    $("#installningarBtn").click(function (event) {
        event.preventDefault();
        alert("Inst&#228;llningar");

    });

});

function alertInTooltipbox(message) {
    $("#tooltipbox").html(message);
    $("#tooltipbox").show(200);
}



