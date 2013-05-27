$(document).ready(function () {
    $("#tooltipbox").hide();

    $("#olyckaBtn").click(function(){
        var message = "Information om olyckor som orsakar trafikst&#246;rningar";
        $(".traffic-problem").toggle();
        $(".traffic-problem2").toggle();
        $("path").toggle();
        alertInTooltipbox(message );
        
    });
    $("#tullarBtn").click(function () {
        var message = "Information om tullarna och kostnader";
        alertInTooltipbox(message);

    });
    $("#vaderBtn").click(function () {
        var message = "V&#228;derinformation";
        alertInTooltipbox(message);

    });
    $("#lokalBtn").click(function () {
        var message = "Information om lokaltrafikst&#246;ningar";
        alertInTooltipbox(message);

    });
    $("#installningarBtn").click(function () {
        alert("Inst&#228;llningar");

    });

    function alertInTooltipbox(message) {
        $("#tooltipbox").html(message);
        $("#tooltipbox").show(200);
    }
});



