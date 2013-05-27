$(document).ready(function () {

   if (Modernizr.mq('only all and (max-width: 767px)')) { //480px
        //var viewportHeight = $(window).height();
        $("nav.mainmenu img").imagesLoaded(function(){
            var navMenuHight = $("nav.mainmenu").height();
            console.log("navMenuHight = " + navMenuHight);
            var deviceHight = $(window).height();
            console.log("deviceHight = " + deviceHight);
            var mapHight = deviceHight - navMenuHight;
            console.log("mapHight = " + mapHight);
            $('#map').height(mapHight);
        })

    }
    window.setTimeout(moveMapNavArrows, 300)
    function moveMapNavArrows() {
        $('.leaflet-left').removeClass('leaflet-left').addClass('leaflet-right');
    }

    if (window.navigator.standalon) {
        alert("window.navigator.standalon = true");
    }
});