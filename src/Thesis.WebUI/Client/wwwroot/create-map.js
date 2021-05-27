let map = undefined;
let pluginSet = false;
let geolocation = false;
let geolocate = undefined;
let OSMServerURI = undefined;

window.mapInitializer = {
    create: dotnetHelper => {
        dotnetHelper.invokeMethodAsync('GetOSMServerAddress')
            .then(URI => {
                OSMServerURI = URI;
                var q = (location.search || '').substr(1).split('&');
                var preference =
                    q.indexOf('vector') >= 0 ? 'vector' :
                        (q.indexOf('raster') >= 0 ? 'raster' :
                            (mapboxgl.supported() ? 'vector' : 'raster'));
                if (preference == 'vector') {
                    console.log("map vector");
                    if (pluginSet === false) {
                        mapboxgl.setRTLTextPlugin(OSMServerURI + '/mapbox-gl-rtl-text.js');
                        pluginSet = true;
                    }
                    map = new mapboxgl.Map({
                        container: 'map',
                        center: [21.014, 52.2364],
                        zoom: 10.55,
                        style: OSMServerURI + '/styles/klokantech-basic/style.json',
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
                    console.log("map raster");
                    map = L.mapbox.map('map', OSMServerURI + '/styles/klokantech-basic.json', { zoomControl: false });
                    new L.Control.Zoom({ position: 'topright' }).addTo(map);
                    setTimeout(function () {
                        new L.Hash(map);
                    }, 0);
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
                });
            });
    }

};

