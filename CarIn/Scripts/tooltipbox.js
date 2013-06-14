$(document).ready(function () {

    var vastbuttonclick = 0;
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
    $("#vaderBtn").click(function(event) {
        event.preventDefault();
        var message = GetAdvancedWheatherInfo();
        alertInTooltipbox(message);

    });
    
    $("#lokalBtn").click(function(event) {
        event.preventDefault();
        if (vastbuttonclick === 0) {
            $(".VasttrafikIncidentsMarker1").hide();
            alertInTooltipbox("Döljer nu Västraffikincedenter som är 'Lätta' Och visar Endast 'Svåra' och 'Normala'");
            vastbuttonclick++;
        }
        else if (vastbuttonclick === 1) {
            $(".VasttrafikIncidentsMarker2").hide();
            alertInTooltipbox("Döljer nu Västraffikincedenter som är 'Normala' Och visar Endast 'Svåra'.");
            vastbuttonclick++;
        }
        else if (vastbuttonclick === 2) {
            $(".VasttrafikIncidentsMarker3").hide();
            alertInTooltipbox("Döljer nu alla Västraffikincedenter ");
            vastbuttonclick++;
        }
        else if (vastbuttonclick === 3) {
            $(".VasttrafikIncidentsMarker1").show();
            alertInTooltipbox("Västtrafik nu Västraffikincedenter som är 'Lätta'");
            vastbuttonclick++;
        }
        else if (vastbuttonclick === 4) {
            $(".VasttrafikIncidentsMarker2").show();
            alertInTooltipbox("Västtrafik nu Västraffikincedenter som är 'Normala'");
            vastbuttonclick++;
        }
        else if (vastbuttonclick === 5) {
            $(".VasttrafikIncidentsMarker3").show();
            alertInTooltipbox("Västtrafik nu Västraffikincedenter som är 'Svåra'");
            vastbuttonclick = 0;
        }
        var message = "Information om lokaltrafikst&#246;ningar";
        alertInTooltipbox(message);

    });
    
    $("#installningarBtn").click(function (event) {
        event.preventDefault();
        //alert("Inst&#228;llningar");

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
    
    var messageToBeDisplayed = "Temperaturen : " + +$WheatherDiv.attr('data-WheatherTemp') + "\u2103" +"\n";
    messageToBeDisplayed += " Vindriktning : " + $WheatherDiv.attr('data-WheatherWindCode') + "\n";
    messageToBeDisplayed += " Vindhastighet : " + $WheatherDiv.attr('data-WindSpeedMps') + "Mps";
   
    $WheatherDiv.children('img').attr('src', GetUrlForSymbolName($WheatherDiv.attr('data-WheatherSymbolName')));
    return messageToBeDisplayed;
}
function GetUrlForSymbolName(symbolname) {
    switch (symbolname) {
        case "Sun":
            return "/Images/Wheather_Icons/sun.png";
        case "clear sky":
            return "/Images/Wheather_Icons/sun.png";
        case "Fair":
            return "/Images/Wheather_Icons/Fair.png";
        case "Partly cloudy":
            return "/Images/Wheather_Icons/Partly_cloudy.png";
        case "Cloudy":
            return "/Images/Wheather_Icons/Cloudy.png";
        case "Rain showers":
            return "/Images/Wheather_Icons/Rain_showers.png";
        case "Rain showers with thunder":
            return "/Images/Wheather_Icons/Rain_and_thunder.png";
        case "Sleet showers":
            return "/Images/Wheather_Icons/Sleet.png";
        case "Snow showers":
            return "/Images/Wheather_Icons/Snow.png";
        case "Rain":
            return "/Images/Wheather_Icons/Rain.png";
        case "Heavy rain":
            return "/Images/Wheather_Icons/Heavy_rain.png";
        case "Rain and thunder":
            return "/Images/Wheather_Icons/Rain_and_thunder.png";
        case "Sleet":
            return "/Images/Wheather_Icons/Sleet.png";
        case "Snow":
            return "/Images/Wheather_Icons/Snow.png";
        case "Fog":
            return "/Images/Wheather_Icons/Fog.png";
        default:
            return "/Images/Wheather_Icons/Fair.png";
    }
}