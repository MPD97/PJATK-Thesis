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

            dotnetHelper.invokeMethodAsync('GetRoutesGeoJsonQuick', coordTopLeft[1], coordTopLeft[0], coordBottomRight[1], coordBottomRight[0], zoom)
                .then(json => {

                    //$.each(layers, function (index, value) {
                    //    map.removeLayer(value);
                    //});

                    //$.each(sources, function (index, value) {
                    //    map.removeSource(value);
                    //});

                    //sources = [];
                    //layers = [];

                    $.each(json.results, function (index, result) {

                        const sourceId = 'route-' + result.sourceId;

                        if (jQuery.inArray(sourceId, sources) !== -1) {
                            return;
                        }

                        map.addSource(sourceId, result.source);
                        sources.push(sourceId);

                        map.addLayer({
                            'id': sourceId,
                            'type': 'line',
                            'source': sourceId,
                            'layout': {
                                'line-join': 'round',
                                'line-cap': 'round'
                            },
                            'paint': {
                                'line-color': '#888',
                                'line-width': 6
                            }
                        });
                        layers.push(sourceId);
                    });
                    sourcesCount = json.results.length;
                });
        });
    }
};