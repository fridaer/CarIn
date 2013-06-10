$(document).ready(function () {
   
    if (Modernizr.mq('only all and (max-width: 767px)')) { //480px
        if ((navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPod/i))) {
            if (!window.navigator.standalone){
                window.location.replace("home/iOS_Install");
                //$("#container").remove();
                //$.get('home/iOS_Install', function (data) {
                //    $('body').html(data);
                //    alert('Load was performed.');
                //});
            }
        }
        
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
    if (window.navigator.standalon) {
        alert("window.navigator.standalon = true");
    }
});



function ShowLoadingDiv() {
    $("#appStartLoading").slideDown("slow", function () {
        $("#balls").fadeIn("slow");
    });
}

function HideLoadingDiv() {
    $("#balls").fadeOut("fast", function () {
        $("#appStartLoading").slideUp("slow");
    });

}