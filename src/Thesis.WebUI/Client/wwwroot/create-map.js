﻿let map = undefined;
let pluginSet = false;
let geolocation = false;
let geolocate = undefined;
let OSMServerURI = undefined;

function createMap() {
    var q = (location.search || '').substr(1).split('&');
    var preference =
        q.indexOf('vector') >= 0 ? 'vector' :
            (q.indexOf('raster') >= 0 ? 'raster' :
                (mapboxgl.supported() ? 'vector' : 'raster'));
    if (preference == 'vector') {
        console.log("Creating map - Vector mode");
        mapboxgl.accessToken = 'pk.eyJ1IjoibXBkOTciLCJhIjoiY2twNzdheDNiMTM5bTJvczFvb3FvMDZjciJ9.SsZFQE9EsGcgE5l8_etrlw';
        map = new mapboxgl.Map({
            container: 'map',
            center: [21.014, 52.2364],
            zoom: 10.55,
            style: 'mapbox://styles/mapbox/outdoors-v11',
            hash: true
        });

        map.addControl(new mapboxgl.NavigationControl());
        map.addControl(new mapboxgl.FullscreenControl());
        geolocate = new mapboxgl.GeolocateControl({
            positionOptions: {
                enableHighAccuracy: true
            },
            trackUserLocation: true
        });
        map.addControl(geolocate);


    } else {
        console.error("Creating map - Raster mode. Not Implemented!");
    }

    map.on('load', function () {
        console.log("map-loaded");
        map.loadImage(
            'img/marker-green.png',
            function (error, image) {
                if (error) throw error;
                map.addImage('marker-1', image);
            }
        );
        map.loadImage(
            'img/marker-blue.png',
            function (error, image) {
                if (error) throw error;
                map.addImage('marker-2', image);
            }
        );
        map.loadImage(
            'img/marker-red.png',
            function (error, image) {
                if (error) throw error;
                map.addImage('marker-3', image);
            }
        );
        map.loadImage(
            'img/marker-black.png',
            function (error, image) {
                if (error) throw error;
                map.addImage('marker-4', image);
            }
        );
        $(".mapboxgl-ctrl-geolocate").click();

        map.addSource('mapbox-dem', {
            'type': 'raster-dem',
            'url': 'mapbox://mapbox.mapbox-terrain-dem-v1',
            'tileSize': 512,
            'maxzoom': 14
        });
        // add the DEM source as a terrain layer with exaggerated height
        map.setTerrain({ 'source': 'mapbox-dem', 'exaggeration': 2.0 });

        // add a sky layer that will show when the map is highly pitched
        map.addLayer({
            'id': 'sky',
            'type': 'sky',
            'paint': {
                'sky-type': 'atmosphere',
                'sky-atmosphere-sun': [0.0, 0.0],
                'sky-atmosphere-sun-intensity': 15
            }
        });
    });
}
