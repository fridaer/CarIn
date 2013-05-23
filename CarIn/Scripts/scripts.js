$(document).ready(function () {
    if (Modernizr.mq('only all and (max-width: 767px)')) { //480px
        //var viewportHeight = $(window).height();
        var navMenuHight = $("nav.mainmenu").height();
        console.log(navMenuHight);
        var deviceHight = $('#map').height($(window).height());
        var mapHight = deviceHight - navMenuHight;
        $('#map').height(mapHight);

    }


});