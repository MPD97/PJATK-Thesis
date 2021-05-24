var map = undefined;
var pluginSet = false;

function createMap() {
    console.log("map init");
    var q = (location.search || '').substr(1).split('&');
    var preference =
        q.indexOf('vector') >= 0 ? 'vector' :
            (q.indexOf('raster') >= 0 ? 'raster' :
                (mapboxgl.supported() ? 'vector' : 'raster'));
    if (preference == 'vector') {
        console.log("map vector");
        if (pluginSet === false) {
            mapboxgl.setRTLTextPlugin('http://localhost:8080/mapbox-gl-rtl-text.js');
            pluginSet = true;
        }
        map = new mapboxgl.Map({
            container: 'map',
            center: [21.014, 52.2364],
            zoom: 10.55,
            style: 'http://localhost:8080/styles/klokantech-basic/style.json',
            hash: true
        });

        map.addControl(new mapboxgl.NavigationControl());
        map.addControl(new mapboxgl.FullscreenControl());

    } else {
        console.log("map raster");
        map = L.mapbox.map('map', 'http://localhost:8080/styles/klokantech-basic.json', { zoomControl: false });
        new L.Control.Zoom({ position: 'topright' }).addTo(map);
        setTimeout(function () {
            new L.Hash(map);
        }, 0);
    }
}