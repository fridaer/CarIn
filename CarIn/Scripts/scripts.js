$(document).ready(function () {
    if (Modernizr.mq('only all and (max-width: 767px)')) { //480px
        //var viewportHeight = $(window).height();
        var navMenuHight = $("nav.mainmenu").height();
        var deviceHight = $('#map').height($(window).height());
        var mapHight = deviceHight - navMenuHight;
        $('#map').height(mapHight);


    }
    window.setTimeout(moveMapNavArrows, 100)
    function moveMapNavArrows() {
        $('.leaflet-left').removeClass('leaflet-left').addClass('leaflet-right');
    }


});