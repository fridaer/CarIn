/// <reference path="mapbox.js" />
"use strict";
$(document).ready(function () {

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
                    var id = 0; 
                    $.each(json.TrafficIncidents, function () {
                        if (this.PointLong !== this.ToPointLong || this.PointLat !== this.ToPointLat) {

                            id++;
                            var myIcon = L.divIcon({ className: 'traffic-problem icon-attention' });
                            var popupContent = '<p id="' + id + '">' + this.Description + '</p>';
                            var themarker = L.marker([this.PointLat, this.PointLong], { icon: myIcon }).addTo(map).bindPopup(popupContent);

                            var myIcon = L.divIcon({ className: 'traffic-problem icon-attention' });
                            var popupContent = '<p id="' + id + '">' + this.Description + '</p>';
                            var themarker = L.marker([this.ToPointLat, this.ToPointLong], { icon: myIcon }).addTo(map).bindPopup(popupContent);
                        }
                        else {
                            id++;
                            var myIcon = L.divIcon({ className: 'traffic-problem2 icon-attention' });
                            var popupContent = '<p id="' + id + '">' + this.Description + '</p>';
                            var themarker = L.marker([this.PointLat, this.PointLong], { icon: myIcon }).addTo(map).bindPopup(popupContent);
                        }
                    });
                    var i = 0;
                    $.each(json.MapQuestDirections, function () {

                        var LatlongArrayen = StringToLatLongArray(this.shapePoints);
                        var polyline_options = {
                            color: '#000'
                        };
                        var polyline = L.polyline(LatlongArrayen, polyline_options).addTo(map);
                        });

                   

                    $('.vader span').text(json.WheatherPeriods[0].TemperatureCelsius + "\u2103");
                },
                error: function (e) {
                    console.log("Det gick åt apan :( ingen data via apit! ");
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
                                
                return LongLatArrayInArray
            }


    });
})