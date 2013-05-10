/// <reference path="mapbox.js" />
var layer = L.mapbox.tileLayer('tobohr.map-n6vjouf7');
layer.on('ready', function () {
    // the layer has been fully loaded now, and you can
    // call .getTileJSON and investigate its properties
    var map = L.mapbox.map('map', 'tobohr.map-n6vjouf7').setView([57.75, 11.974749], 11);
   
    var updateEvery2sec = setInterval(function () {
        var zoomlevel = map.getZoom();
        if (zoomlevel <= 11) {
            map.setView([57.75, 11.974749], 11);
            map.dragging.disable();
        }
        else {
            map.dragging.enable();
        }
    },500);

        (function ($) {
            //var url = 'http://dev.virtualearth.net/REST/v1/Traffic/Incidents/57.497813,11.602687,57.885356,12.406062?key=AoWk0xixw7Xr16xE6Tne-3nNsYihl9ab7yIhnoASonYm2sWCdYk7VNhhAUg82cUj&o=json&jsonp=?';

            var url = "/api/v1/CarInRESTful/GetAllInfo/";
            $.ajax({
                type: 'GET',
                url: url,
                async: true,
                success: function (json) {
                    //json[0] = 
                    //Description: "mellan Pinan och Färjeled till Hönö - Vägarbete."
                    //End: "06/28/2013 21:00:00"
                    //ID: 1
                    //IncidentId: "403657022"
                    //Lane: null
                    //LastModified: "05/07/2013 06:01:26"
                    //PointLat: "57.70863"
                    //PointLong: "11.703"
                    //RoadClosed: "False"
                    //Severity: "1"
                    //Start: "04/30/2013 23:00:00"
                    //ToPointLat: "57.70742"
                    //ToPointLong: "11.69865"
                    //Type: "9"
                    //Verified: "True"
                    console.log(json);
                    $.each(json, function () {
                        console.log(this);
                        L.mapbox.markerLayer({
                            type: 'Feature',
                            geometry: {
                                type: 'Point',
                                coordinates: [this.PointLong, this.PointLat]
                            },
                            properties: {
                                title: this.Description,
                                description: this.Description,
                                'marker-symbol': 'cross',
                                'marker-size': 'large',
                                'marker-color': '#EB0000'
                            }
                        }).addTo(map);
                        
                        if (this.PointLong !== this.ToPointLong || this.PointLat !== this.ToPointLat)
                        {
                            console.log(this);
                            L.mapbox.markerLayer({
                                type: 'Feature',
                                geometry: {
                                    type: 'Point',
                                    coordinates: [this.ToPointLong, this.ToPointLat]
                                },
                                properties: {
                                    title: this.Description,
                                    description: this.Description,
                                    'marker-symbol': 'cross',
                                    'marker-size': 'large',
                                    'marker-color': '#EB0000'
                                }
                            }).addTo(map);
                        }
                    });

                },
                error: function (e) {
                    console.log("Det gick åt apan :( ");
                }
            });

        })(jQuery);
})