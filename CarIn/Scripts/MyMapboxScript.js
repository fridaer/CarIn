/// <reference path="mapbox.js" />
"use strict";
var Priorty = [];
var p;

var TrafficIncidentsMarkers = [];
var TollLocationsMarkers = [];
var VasttrafikIncidentsMarkers = [];
$(document).ready(function () {

    ShowLoadingDiv();

        var map = L.mapbox.map('map','tobohr.map-n6vjouf7', {
            detectRetina: true,
            retinaVersion: 'tobohr.map-fkbh0rtn',
            minZoom: 11,
            zoomControl: false,
            attributionControl: false,
        }).setView([57.75, 11.974749], 11);
        new L.Control.Zoom({ position: 'topright' }).addTo(map);

        var southWest = new L.LatLng(58.076242, 11.472473);
        var northEast = new L.LatLng(57.537758, 12.675476);
        map.setMaxBounds(new L.LatLngBounds(southWest, northEast));
        L.marker([58.076242, 11.472473]).addTo(map).bindPopup("Kartyta slutar här");
        L.marker([58.09657, 12.565613]).addTo(map).bindPopup("Kartyta slutar här");
        L.marker([57.465058, 11.820946]).addTo(map).bindPopup("Kartyta slutar här");
        L.marker([57.537758, 12.675476]).addTo(map).bindPopup("Kartyta slutar här");


        var VasttrafikIncidentsMarkerIcon;
        var TollmarkerIcon;
        var TrafficIncidentsIcon;

      
        var url = "/api/v1/CarInRESTful/GetAllInfo/";
        $.ajax({
            type: 'GET',
            url: url,
            async: true,
            success: function (json) {
                console.log(json);
                $.each(json.TrafficIncidents, function () {
                    if (this.PointLong !== this.ToPointLong || this.PointLat !== this.ToPointLat) {
                        Priorty[p] = this.Priorty;
                        var myIcon = L.divIcon({
                            className: 'traffic-problem icon-attention',
                            iconAnchor: [0, 0], // point of the icon which will correspond to marker's location
                            popupAnchor: [8, -10]  // point from which the popup should open relative to the iconAnchor 
                        });
                        var popupContent = this.Description + "  - Beräknat klart " + this.End;
                        var themarker = L.marker([this.PointLat, this.PointLong], { icon: myIcon }).addTo(map).bindPopup(popupContent);
                        TrafficIncidentsMarkers.push(themarker);
                        var myIcon = L.divIcon({ className: 'traffic-problem icon-attention' });
                        var popupContent = this.Description + "  - Beräknat klart " + this.End;
                        var themarker = L.marker([this.ToPointLat, this.ToPointLong], { icon: myIcon }).addTo(map).bindPopup(popupContent);
                        TrafficIncidentsMarkers.push(themarker);
                    }
                    else {
                        var myIcon = L.divIcon({ className: 'traffic-problem2 icon-attention' });
                        var popupContent = this.Description + "  - Beräknat klart " + this.End;

                        var themarker = L.marker([this.PointLat, this.PointLong], { icon: myIcon }).addTo(map).bindPopup(popupContent);
                        TrafficIncidentsMarkers.push(themarker);
                    }
                });

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
                        iconSize: [10, 10], // size of the icon
                        iconAnchor: [0, 0], // point of the icon which will correspond to marker's location
                        popupAnchor: [13, -10],  // point from which the popup should open relative to the iconAnchor
                        className: 'TollsMarker'
                    })

                    var popupContent = this.Name;
                    var themarker = L.marker([this.PointLat, this.PointLong], { icon: myIcon }).addTo(map).bindPopup(popupContent);
                    TollLocationsMarkers.push(themarker);
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
                    
                    var ClassnameBuilder = 'VasttrafikIncidentsMarker' + this.Priority.toString();
                    var myIcon = L.divIcon({
                        //iconUrl: '../images/icon-lokaltrafik.png',
                        //iconRetinaUrl: '../images/icon-lokaltrafik.png',
                        //iconSize: [10, 10], // size of the icon
                        iconAnchor: [0, 0], // point of the icon which will correspond to marker's location
                        popupAnchor: [13, -10],  // point from which the popup should open relative to the iconAnchor
                        className: ClassnameBuilder
                    })
                    var LatLongArray = StringToLatLongArray(this.TrafficChangesCoords);
                    var popupContent = this.Title;
                    var themarker = L.marker([LatLongArray[0][0], LatLongArray[0][1]], { icon: myIcon }).addTo(map).bindPopup(popupContent);
                    VasttrafikIncidentsMarkers.push(themarker);

                    if (typeof LatLongArray[1] !== 'undefined' && LatLongArray[1] !== null) {
                        var themarker = L.marker([LatLongArray[1][0], LatLongArray[1][1]], { icon: myIcon }).addTo(map).bindPopup(popupContent);
                        VasttrafikIncidentsMarkers.push(themarker);
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
                    'data-WheatherTemp': json.WheatherPeriods[0].TemperatureCelsius,
                    'data-WheatherWindCode': json.WheatherPeriods[0].WindCode,
                    'data-WindSpeedMps': json.WheatherPeriods[0].WindSpeedMps,
                    'data-WheatherSymbolName': json.WheatherPeriods[0].SymbolName
                });
                $WheatherDiv.children('img').attr('src', getUrlForSymbolName(json.WheatherPeriods[0].SymbolName));

                window.setTimeout(function () {
                    HideLoadingDiv();
                }, 1000);
            },
            error: function (e) {
                console.log("Det gick åt apan :( ingen data via apit! ");
            }
        });
        map.addEventListener("viewreset", function (e) {
            console.log("Klickat!!");
            var a = 0;
            var i = 0;

            var zoomlevel = map.getZoom();
            if (zoomlevel === 11) {
                TollmarkerIcon = L.icon({ iconUrl: '../images/trangselskatt25x25.png', iconRetinaUrl: '../images/trangselskatt50x50.png', iconSize: [10, 10], iconAnchor: [0, 0], popupAnchor: [13, -10], className: 'TollsMarker' });
            }
            else if (zoomlevel === 12) {
                TollmarkerIcon = L.icon({ iconUrl: '../images/trangselskatt25x25.png', iconRetinaUrl: '../images/trangselskatt50x50.png', iconSize: [13, 13], iconAnchor: [0, 0], popupAnchor: [13, -10], className: 'TollsMarker' });
            }
            else if (zoomlevel === 13) {
                TollmarkerIcon = L.icon({ iconUrl: '../images/trangselskatt25x25.png', iconRetinaUrl: '../images/trangselskatt50x50.png', iconSize: [17, 17], iconAnchor: [0, 0], popupAnchor: [13, -10], className: 'TollsMarker' });
            }
            else if (zoomlevel === 14 || zoomlevel === 15) {
                TollmarkerIcon = L.icon({ iconUrl: '../images/trangselskatt25x25.png', iconRetinaUrl: '../images/trangselskatt50x50.png', iconSize: [23, 23], iconAnchor: [0, 0], popupAnchor: [13, -10], className: 'TollsMarker' });
            }
            else if ((zoomlevel === 17 || zoomlevel === 16)) {
                TollmarkerIcon = L.icon({ iconUrl: '../images/trangselskatt25x25.png', iconRetinaUrl: '../images/trangselskatt50x50.png', iconSize: [25, 25], iconAnchor: [0, 0], popupAnchor: [13, -10], className: 'TollsMarker' });
            }
            else {
                TollmarkerIcon = L.icon({ iconUrl: '../images/trangselskatt25x25.png', iconRetinaUrl: '../images/trangselskatt50x50.png', iconSize: [30, 30], iconAnchor: [0, 0], popupAnchor: [13, -10], className: 'TollsMarker' });
            }
            $.each(TollLocationsMarkers, function () {
                this.setIcon(TollmarkerIcon);
            })
            //$.each(VasttrafikIncidentsMarkers, function () {
            //    if (zoomlevel === 11 && TollLocationsMarkers[0]._popup._source.options.icon.options.iconSize[0] !== 10) {
            //        VasttrafikIncidentsMarkerIcon = L.icon({ iconAnchor: [0, 0], popupAnchor: [13, -10], className: VasttrafikIncidentsMarkers[a]._popup._source.options.icon.options.className });
            //    }
            //    else if (zoomlevel === 12) {
            //        VasttrafikIncidentsMarkerIcon = L.icon({ iconAnchor: [0, 0], popupAnchor: [13, -10], className: VasttrafikIncidentsMarkers[a]._popup._source.options.icon.options.className });
            //    }
            //    else if (zoomlevel === 13) {
            //        VasttrafikIncidentsMarkerIcon = L.icon({ iconUrl: '../images/icon-lokaltrafik.png', iconRetinaUrl: '../images/icon-lokaltrafik.png', iconSize: [17, 17], iconAnchor: [0, 0], popupAnchor: [13, -10], className: VasttrafikIncidentsMarkers[a]._popup._source.options.icon.options.className });
            //    }
            //    else if (zoomlevel === 14 || zoomlevel === 15) {
            //        VasttrafikIncidentsMarkerIcon = L.icon({ iconUrl: '../images/icon-lokaltrafik.png', iconRetinaUrl: '../images/icon-lokaltrafik.png', iconSize: [23, 23], iconAnchor: [0, 0], popupAnchor: [13, -10], className: VasttrafikIncidentsMarkers[a]._popup._source.options.icon.options.className });
            //    }
            //    else if ((zoomlevel === 17 || zoomlevel === 16)) {
            //        VasttrafikIncidentsMarkerIcon = L.icon({ iconUrl: '../images/icon-lokaltrafik.png', iconRetinaUrl: '../images/icon-lokaltrafik.png', iconSize: [25, 25], iconAnchor: [0, 0], popupAnchor: [13, -10], className: VasttrafikIncidentsMarkers[a]._popup._source.options.icon.options.className });
            //    }
            //    else {
            //        VasttrafikIncidentsMarkerIcon = L.icon({ iconUrl: '../images/icon-lokaltrafik.png', iconRetinaUrl: '../images/icon-lokaltrafik.png', iconSize: [30, 30], iconAnchor: [0, 0], popupAnchor: [13, -10], className: VasttrafikIncidentsMarkers[a]._popup._source.options.icon.options.className });
            //    }
            //    this.setIcon(VasttrafikIncidentsMarkerIcon);
            //    a++;
            //})
            if (zoomlevel === 11) {
                $(".VasttrafikIncidentsMarker1").removeClass("icon-13px").removeClass("icon-17px").removeClass("icon-23px").removeClass("icon-25px").addClass("icon-10px");
                $(".VasttrafikIncidentsMarker2").removeClass("icon-13px").removeClass("icon-17px").removeClass("icon-23px").removeClass("icon-25px").addClass("icon-10px");
                $(".VasttrafikIncidentsMarker3").removeClass("icon-13px").removeClass("icon-17px").removeClass("icon-23px").removeClass("icon-25px").addClass("icon-10px");

            }
            else if (zoomlevel === 12) {
                $(".VasttrafikIncidentsMarker1").removeClass("icon-10px").removeClass("icon-17px").removeClass("icon-23px").removeClass("icon-25px").addClass("icon-13px");
                $(".VasttrafikIncidentsMarker2").removeClass("icon-10px").removeClass("icon-17px").removeClass("icon-23px").removeClass("icon-25px").addClass("icon-13px");
                $(".VasttrafikIncidentsMarker3").removeClass("icon-10px").removeClass("icon-17px").removeClass("icon-23px").removeClass("icon-25px").addClass("icon-13px");

            }
            else if (zoomlevel === 13) {
                $(".VasttrafikIncidentsMarker1").removeClass("icon-13px").removeClass("icon-23px").removeClass("icon-25px").removeClass("icon-10px").addClass("icon-17px");
                $(".VasttrafikIncidentsMarker2").removeClass("icon-13px").removeClass("icon-23px").removeClass("icon-25px").removeClass("icon-10px").addClass("icon-17px");
                $(".VasttrafikIncidentsMarker3").removeClass("icon-13px").removeClass("icon-23px").removeClass("icon-25px").removeClass("icon-10px").addClass("icon-17px");

            }
            else if (zoomlevel === 14 || zoomlevel === 15) {
                $(".VasttrafikIncidentsMarker1").removeClass("icon-17px").removeClass("icon-23px").removeClass("icon-25px").removeClass("icon-10px").addClass("icon-23px");
                $(".VasttrafikIncidentsMarker2").removeClass("icon-17px").removeClass("icon-23px").removeClass("icon-25px").removeClass("icon-10px").addClass("icon-23px");
                $(".VasttrafikIncidentsMarker3").removeClass("icon-17px").removeClass("icon-23px").removeClass("icon-25px").removeClass("icon-10px").addClass("icon-23px");

            }
            else if ((zoomlevel === 17 || zoomlevel === 16)) {
                $(".VasttrafikIncidentsMarker1").removeClass("icon-10px").removeClass("icon-13px").removeClass("icon-17px").removeClass("icon-23px").addClass("icon-25px");
                $(".VasttrafikIncidentsMarker2").removeClass("icon-10px").removeClass("icon-13px").removeClass("icon-17px").removeClass("icon-23px").addClass("icon-25px");
                $(".VasttrafikIncidentsMarker3").removeClass("icon-10px").removeClass("icon-13px").removeClass("icon-17px").removeClass("icon-23px").addClass("icon-25px");

            }
            else {
                $(".VasttrafikIncidentsMarker1").removeClass("icon-10px").removeClass("icon-13px").removeClass("icon-17px").removeClass("icon-23px").addClass("icon-25px");
                $(".VasttrafikIncidentsMarker2").removeClass("icon-10px").removeClass("icon-13px").removeClass("icon-17px").removeClass("icon-23px").addClass("icon-25px");
                $(".VasttrafikIncidentsMarker3").removeClass("icon-10px").removeClass("icon-13px").removeClass("icon-17px").removeClass("icon-23px").addClass("icon-25px");

            }
            if (zoomlevel === 11) {
                $(".icon-attention").removeClass("icon-13px").removeClass("icon-17px").removeClass("icon-23px").removeClass("icon-25px").addClass("icon-10px");
            }
            else if (zoomlevel === 12) {
                $(".icon-attention").removeClass("icon-10px").removeClass("icon-17px").removeClass("icon-23px").removeClass("icon-25px").addClass("icon-13px");
            }
            else if (zoomlevel === 13) {
                $(".icon-attention").removeClass("icon-13px").removeClass("icon-23px").removeClass("icon-25px").removeClass("icon-10px").addClass("icon-17px");
            }
            else if (zoomlevel === 14 || zoomlevel === 15) {
                $(".icon-attention").removeClass("icon-17px").removeClass("icon-23px").removeClass("icon-25px").removeClass("icon-10px").addClass("icon-23px");
            }
            else if ((zoomlevel === 17 || zoomlevel === 16)) {
                $(".icon-attention").removeClass("icon-10px").removeClass("icon-13px").removeClass("icon-17px").removeClass("icon-23px").addClass("icon-25px");
            }
            else {
                $(".icon-attention").removeClass("icon-10px").removeClass("icon-13px").removeClass("icon-17px").removeClass("icon-23px").addClass("icon-25px");
            }
        })
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
            LatLongStringArray.splice(LatLongStringArray.length - 1, 1);

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

        function getUrlForSymbolName(symbolname) {
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
});