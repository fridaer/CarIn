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
        CheckTollHelpFunction();

    });
    $("#vaderBtn").click(function (event) {
        event.preventDefault();
        var message = GetAdvancedWheatherInfo();
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
    $("#tooltipbox").css({
        'white-space': 'pre-line',
        'line-height' : '1.1em'
    });
    $("#tooltipbox").html(message);
    $("#tooltipbox").show(200);
}


function GetAdvancedWheatherInfo() {
    var $WheatherDiv = $('#vaderBtn');
    
    var messageToBeDisplayed = "Temperaturen : " + $WheatherDiv.attr('data-WheatherTemp') + "\u2103" + "\n";
    messageToBeDisplayed += " Vindriktning : " + $WheatherDiv.attr('data-WheatherWindCode') + "\n";
    messageToBeDisplayed += " Vindhastighet : " + $WheatherDiv.attr('data-WindSpeedMps') + "Mps";
    
    return messageToBeDisplayed;
}
