const colors = ["#00DC30", "#2D35FF", "#DC0D00", "#000000"];

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
                    //$.each(layers, function (index, value) {
                    //    map.removeLayer(value);
                    //});

                    //$.each(sources, function (index, value) {
                    //    map.removeSource(value);
                    //});

                    //sources = [];
                    //layers = [];

                    $.each(json, function (index, result) {

                        const routeId = 'route-' + result.routeId;

                        if (jQuery.inArray(routeId, sources) !== -1) {
                            return;
                        }
                        console.log(result.source);

                        map.addSource(routeId, result.source);
                        sources.push(routeId);

                        //map.addLayer({
                        //    'id': 'point-' + routeId,
                        //    'type': 'circle',
                        //    'source': routeId,
                        //    'paint': {
                        //        'circle-radius': 16,
                        //        'circle-color': '#B42222'
                        //    },
                        //    'filter': ['==', '$type', 'Point']

                        //});
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
                                'text-offset': [0, 1.25],
                                'text-anchor': 'top'
                            },
                            'filter': ['==', '$type', 'Point']
                        });
                        layers.push(routeId);
                    });
                    sourcesCount = json.length;
                });
        });
    }
};