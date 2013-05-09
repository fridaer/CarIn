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
            var url = "";
            $.ajax({
                type: 'GET',
                url: url,
                async: true,
                jsonpCallback: 'jsonCallback',
                contentType: 'application/json',
                dataType: 'jsonp',
                success: function (json) {
                    console.log(json);
                    $.each(json.resourceSets[0].resources, function () {
                        console.log(this);
                        L.mapbox.markerLayer({
                            type: 'Feature',
                            geometry: {
                                type: 'Point',
                                coordinates: [this.point.coordinates[1], this.point.coordinates[0]]
                            },
                            properties: {
                                title: this.description,
                                description: this.description,
                                'marker-symbol': 'cross',
                                'marker-size': 'large',
                                'marker-color': '#EB0000'
                            }
                        }).addTo(map);
                        
                        if (this.point.coordinates[0] !== this.toPoint.coordinates[0] || this.point.coordinates[1] !== this.toPoint.coordinates[1])
                        {
                            console.log(this);
                            L.mapbox.markerLayer({
                                type: 'Feature',
                                geometry: {
                                    type: 'Point',
                                    coordinates: [this.toPoint.coordinates[1], this.toPoint.coordinates[0]]
                                },
                                properties: {
                                    title: this.description,
                                    description: this.description,
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