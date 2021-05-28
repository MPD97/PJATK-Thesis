const colors = ["#00DC30", "#2D35FF", "#DC0D00", "#000000"];
const geolocationOptions = {
    enableHighAccuracy: true,
    timeout: 5000,
    maximumAge: 0
};
window.mapHelper = {
    init: dotnetHelper => {
        let lastTL = [];
        let lastBR = [];
        let sources = [];
        let layers = [];
        let sourcesCount = 0;

        map.on('moveend', (eventData) => {
            let canvas = map.getCanvas()
            let w = canvas.width
            let h = canvas.height

            let coordTopLeft = map.unproject([0, 0]).toArray()
            let coordBottomRight = map.unproject([w, h]).toArray()

            if (lastTL.length == 2 || lastBR.length == 2) {
                if (sourcesCount < 50 && lastTL[1] >= coordTopLeft[1] && lastTL[0] <= coordTopLeft[0] && lastBR[1] <= coordBottomRight[1] && lastBR[0] >= coordBottomRight[0]) {
                    return;
                }
            }

            lastTL = coordTopLeft;
            lastBR = coordBottomRight;

            let zoom = map.getZoom();

            dotnetHelper.invokeMethodAsync('GetRoutesGeoJson', coordTopLeft[1], coordTopLeft[0], coordBottomRight[1], coordBottomRight[0], zoom)
            .then(json => {
                json = JSON.parse(json);
                $.each(json, function (index, result) {

                    const routeId = 'route-' + result.routeId;

                    if (jQuery.inArray(routeId, sources) !== -1) {
                        return;
                    }
                    console.log(result.source);

                    map.addSource(routeId, result.source);
                    sources.push(routeId);

                    map.addLayer({
                        'id': 'line-' + routeId,
                        'type': 'line',
                        'source': routeId,
                        'layout': {
                            'line-join': 'round',
                            'line-cap': 'round'
                        },
                        'paint': {
                            'line-color': colors[result.difficulty - 1],
                            'line-width': 6
                        },
                        'filter': ['==', '$type', 'LineString']
                    });

                    // Add a symbol layer
                    map.addLayer({
                        'id': 'sp-' + routeId,
                        'type': 'symbol',
                        'source': routeId,
                        'layout': {
                            'icon-image': 'marker-' + result.difficulty,
                            // get the title name from the source's "title" property
                            'text-field': ['get', 'title'],
                            'text-font': [
                                'Open Sans Regular'
                            ],
                            'text-allow-overlap': true,
                            'icon-anchor': 'bottom',
                            "icon-size": 1.25,
                            'text-offset': [0, 0.2],
                            'text-anchor': 'top'
                        },
                        'filter': ['==', '$type', 'Point']
                    });
                    layers.push(routeId);



                    map.on('click', 'sp-' + routeId, onRouteClick());

                    map.on('mouseenter', 'sp-' + routeId, function (e) {
                        map.getCanvas().style.cursor = 'pointer';
                    });

                    map.on('mouseleave', 'sp-' + routeId, function () {
                        map.getCanvas().style.cursor = '';
                    });
                });
                sourcesCount = json.length;
            });
        });

        function onRouteClick(e) {
            var popup = new mapboxgl.Popup({
                closeButton: true,
                closeOnClick: false,
                offset: [0, -50]
            });

            var coordinates = e.features[0].geometry.coordinates.slice();
            var routeName = e.features[0].properties.title;

            while (Math.abs(e.lngLat.lng - coordinates[0]) > 180) {
                coordinates[0] += e.lngLat.lng > coordinates[0] ? 360 : -360;
            }
            function error(err) {
                console.warn(`ERROR(${err.code}): ${err.message}`);
            }
            navigator.geolocation.getCurrentPosition(function (position) {
                var crd = position.coords;
                console.log('Your current position is:');
                console.log(`Latitude : ${crd.latitude}`);
                console.log(`Longitude: ${crd.longitude}`);
                console.log(`More or less ${crd.accuracy} meters.`);

                var lat = position.coords.latitude;
                var long = position.coords.longitude;
                var distance = CalculateDistance(coordinates[1], coordinates[0], lat, long);

                var value = "Rozpocznij";
                if (crd.accuracy > 150) {
                    value = "Zbyt słaby sygnał";
                }
                if (distance > 50) {
                    value = "Jesteś za daleko";
                }

                var btn = '<button class="btn btn-primary">' + value + '</button>';
                var h1 = '<h1>' + routeName + '</h1>';
                var p = '<p> odległość: ' + distance.toFixed(0) + ' metrów</p>';

                popup.setLngLat(coordinates).setHTML(h1 + p + btn).addTo(map);
            }, error, geolocationOptions);

        }
    }
};