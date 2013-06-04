/// <reference path="mapbox.js" />
"use strict";
$(document).ready(function () {
    ShowLoadingDiv();
    var layer = L.mapbox.tileLayer('tobohr.map-n6vjouf7', {
        detectRetina: true,
        retinaVersion: 'tobohr.map-fkbh0rtn'
    });
    layer.on('ready', function () {
        // the layer has been fully loaded now, and you can
        // call .getTileJSON and investigate its properties
        /*        
                var map = L.mapbox.map('map', 'tobohr.map-n6vjouf7', {   
                detectRetina: true,
                retinaVersion: 'tobohr.map-fkbh0rtn'
                }).setView([57.75, 11.974749], 11);
        
        */
        var map = L.map('map')
        .setView([57.75, 11.974749], 11)
        .addLayer(L.mapbox.tileLayer('tobohr.map-n6vjouf7', {
            detectRetina: true,
            retinaVersion: 'tobohr.map-fkbh0rtn'
        }));

        var updateEvery2sec = setInterval(function () {
            var zoomlevel = map.getZoom();
            if (zoomlevel <= 11) {
                map.setView([57.75, 11.974749], 11);
                map.dragging.disable();
            }
            else {
                map.dragging.enable();
            }
        }, 500);
    

            var url = "/api/v1/CarInRESTful/GetAllInfo/";
            $.ajax({
                type: 'GET',
                url: url,
                async: true,
                success: function (json) {
                    console.log(json);
                    $.each(json.TrafficIncidents, function () {
                        if (this.PointLong !== this.ToPointLong || this.PointLat !== this.ToPointLat) {

                            var myIcon = L.divIcon({
                                className: 'traffic-problem icon-attention',
                                iconAnchor: [0, 0], // point of the icon which will correspond to marker's location
                                popupAnchor: [8, -10]  // point from which the popup should open relative to the iconAnchor 
                            });
                            var popupContent = this.Description + "  - Beräknat klart " + this.End;
                            var themarker = L.marker([this.PointLat, this.PointLong], { icon: myIcon }).addTo(map).bindPopup(popupContent);

                            var myIcon = L.divIcon({ className: 'traffic-problem icon-attention' });
                            var popupContent = this.Description + "  - Beräknat klart " + this.End;
                            var themarker = L.marker([this.ToPointLat, this.ToPointLong], { icon: myIcon }).addTo(map).bindPopup(popupContent);
                        }
                        else {
                            var myIcon = L.divIcon({ className: 'traffic-problem2 icon-attention' });
                            var popupContent = this.Description +  "  - Beräknat klart " + this.End;

                            var themarker = L.marker([this.PointLat, this.PointLong], { icon: myIcon }).addTo(map).bindPopup(popupContent);
                        }
                        window.setTimeout(function () {
                            HideLoadingDiv();
                        }, 2000);
                    });
                    var i = 0;
                    $.each(json.MapQuestDirections, function () {

                        var LatlongArrayen = StringToLatLongArray(this.shapePoints);
                        var polyline_options = {
                            color: '#000'
                        };
                        var polyline = L.polyline(LatlongArrayen, polyline_options).addTo(map);
                    });

                    $.each(json.TollLocations, function () {

                        var myIcon = L.icon({
                            iconUrl: '../images/trangselskatt25x25.png',
                            iconRetinaUrl: '../images/trangselskatt50x50.png',
                            iconSize:     [25, 25], // size of the icon
                            iconAnchor:   [0,0], // point of the icon which will correspond to marker's location
                            popupAnchor: [13, -10],  // point from which the popup should open relative to the iconAnchor
                            className: 'TollsMarker'
                        })

                        var popupContent = this.Name;
                        var themarker = L.marker([this.PointLat, this.PointLong], { icon: myIcon }).addTo(map).bindPopup(popupContent);
                    });

                    $.each(json.VasttrafikIncidents, function () {

                        //VasttrafikIncidents: Array[102]
                        //DateFrom: "2013-05-28T14:02:00"
                        //DateTo: "2013-05-28T14:30:00"
                        //ID: 1
                        //Line: ""
                        //Priority: "3"
                        //Title: "Linje 7, förseningar Gamlestadstorget mot Komettorget."
                        //TrafficChangesCoords: "57,7277270041909.12,0052370015729;57,7292460015351.12,0135969965802;"

                        var myIcon = L.icon({
                            iconUrl: '../images/icon-lokaltrafik.png',
                            iconRetinaUrl: '../images/icon-lokaltrafik.png',
                            iconSize: [25, 25], // size of the icon
                            iconAnchor: [0, 0], // point of the icon which will correspond to marker's location
                            popupAnchor: [13, -10],  // point from which the popup should open relative to the iconAnchor
                            className: 'VasttrafikIncidentsMarker'
                        })
                        var LatLongArray = StringToLatLongArray(this.TrafficChangesCoords);

                       

                        var popupContent = this.Title;
                        var themarker = L.marker([LatLongArray[0][0], LatLongArray[0][1]], { icon: myIcon }).addTo(map).bindPopup(popupContent);

                        if (typeof LatLongArray[1] !== 'undefined' && LatLongArray[1] !== null) {
                            var themarker = L.marker([LatLongArray[1][0], LatLongArray[1][1]], { icon: myIcon }).addTo(map).bindPopup(popupContent);
                        }
                    });
                    //ID: 1
                    //PeriodNumber: "1"
                    //SymbolName: "Fair"
                    //TemperatureCelsius: "14"
                    //WindCode: "NNE"
                    //WindSpeedMps: "4.9"
                    var $WheatherDiv = $('#vaderBtn');
                    $WheatherDiv.attr({
                        'data-WheatherTemp' : json.WheatherPeriods[0].TemperatureCelsius,
                        'data-WheatherWindCode' : json.WheatherPeriods[0].WindCode,
                        'data-WindSpeedMps' : json.WheatherPeriods[0].WindSpeedMps
                    });
                    //$WheatherDiv.children('span').text(json.WheatherPeriods[0].TemperatureCelsius + "\u2103");
                    
                    $WheatherDiv.children('img').attr('src', getUrlForSymbolName(json.WheatherPeriods[0].SymbolName));
                },
                error: function (e) {
                    console.log("Det gick åt apan :( ingen data via apit! ");
                }
            });
           
            map.on('popupopen', function (e) {
                var width = (window.innerWidth > 0) ? window.innerWidth : screen.width;
                if (width < 767) {
                    $(".leaflet-popup").hide();
                    var marker = e.popup._source;
                    alertInTooltipbox(marker._popup._content);
                }
            });
        

            function StringToLatLongArray(StringWithLatlong) {

                var LongLatArrayInArray = new Array();
                var LatLongStringArray = StringWithLatlong.split(";");
                //console.log(LatLongStringArray);
                LatLongStringArray.splice(LatLongStringArray.length-1, 1);
                      
                //console.log(LatLongStringArray);

                $.each(LatLongStringArray, function () {

                    var point = this.split(".");
                    point[0] = point[0].replace(",", ".");
                    point[1] = point[1].replace(",", ".");
                    point[0] = parseFloat(point[0]);
                    point[1] = parseFloat(point[1]);
                    LongLatArrayInArray.push(point);
                });

                return LongLatArrayInArray;
            }


    });
    function getUrlForSymbolName(symbolname)
    {
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
            default :
                return "/Images/Wheather_Icons/Fair.png";
        }
    }
})