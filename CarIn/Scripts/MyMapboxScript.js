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
            'marker-symbol': 'cross',
            'marker-size': 'large',
            'marker-color': '#3E679E'
        }
    }).addTo(map);
    L.mapbox.markerLayer({
        type: 'Feature',
        geometry: {
            type: 'Point',
            coordinates: [12.25, 57.8]
        },
        properties: {
            title: 'Stora störningar i skogen i bushen.',
            description: 'Var vaksam när du är ute och går',
            'marker-symbol': 'cross',
            'marker-size': 'small',
            'marker-color': '#3E679E'
        }
    }).addTo(map);
    L.mapbox.markerLayer({
        type: 'Feature',
        geometry: {
            type: 'Point',
            coordinates: [12.032776, 57.63511]
        },
        properties: {
            title: 'Stora störningar i trafiken',
            description: 'Medelhastighet 55km/h',
            'marker-symbol': 'cross',
            'marker-size': 'small',
            'marker-color': '#3E679E'
        }
    }).addTo(map);
    L.mapbox.markerLayer({
        type: 'Feature',
        geometry: {
            type: 'Point',
            coordinates: [12.008743, 57.774884]
        },
        properties: {
            title: 'Stora störningar i trafiken',
            description: 'Medelhastighet 35km/h',
            'marker-symbol': 'cross',
            'marker-size': 'small',
            'marker-color': '#3E679E'
        }
    }).addTo(map);
    L.mapbox.markerLayer({
        type: 'Feature',
        geometry: {
            type: 'Point',
            coordinates: [12.098694, 57.736784]
        },
        properties: {
            title: 'Stora störningar i trafiken',
            description: 'Medelhastighet 35km/h',
            'marker-symbol': 'cross',
            'marker-size': 'small',
            'marker-color': '#3E679E'
        }
    }).addTo(map);
    L.mapbox.markerLayer({
        type: 'Feature',
        geometry: {
            type: 'Point',
            coordinates: [11.990891, 57.718086]
        },
        properties: {
            title: 'Spår Arbete',
            description: 'Försvårad framkomlighet i centrala delar av stan.',
            'marker-symbol': 'cross',
            'marker-size': 'small',
            'marker-color': '#3E679E'
        }
    }).addTo(map);
})