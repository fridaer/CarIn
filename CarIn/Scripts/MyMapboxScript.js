var layer = L.mapbox.tileLayer('tobohr.map-n6vjouf7');
layer.on('ready', function () {
    // the layer has been fully loaded now, and you can
    // call .getTileJSON and investigate its properties

    var map = L.mapbox.map('map', 'tobohr.map-n6vjouf7')
        .setView([57.681789894876026, 12.239759687484923], 11);
    L.mapbox.markerLayer({
        // this feature is in the GeoJSON format: see geojson.org
        // for the full specification
        type: 'Feature',
        geometry: {
            type: 'Point',
            // coordinates here are in longitude, latitude order because
            // x, y is the standard for GeoJSON and many formats
            coordinates: [12.239759687484923, 57.681789894876026]
        },
        properties: {
            title: 'Feeeting kön',
            description: '2 långtradare har koliderat, brinner på vägbannan',
            // one can customize markers by adding simplestyle properties
            // http://mapbox.com/developers/simplestyle/
            'marker-size': 'large',
            'marker-color': '#f0a'
        }
    }).addTo(map);
})