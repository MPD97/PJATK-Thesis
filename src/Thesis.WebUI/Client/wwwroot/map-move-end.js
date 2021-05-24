window.mapHelper = {
    init: dotnetHelper => {
        let lastTL = [];
        let lastBR = [];
        let sources = [];
        let layers = [];

        map.on('moveend', (eventData) => {
            let canvas = map.getCanvas()
            let w = canvas.width
            let h = canvas.height

            let coordTopLeft = map.unproject([0, 0]).toArray()
            let coordBottomRight = map.unproject([w, h]).toArray()

            if (lastTL.length == 2 || lastBR.length == 2) {
                if (lastTL[1] >= coordTopLeft[1] && lastTL[0] <= coordTopLeft[0] && lastBR[1] <= coordBottomRight[1] && lastBR[0] >= coordBottomRight[0]) {
                    console.log("skip");
                    return;
                }
            }
            console.log("continyue");

            lastTL = coordTopLeft;
            lastBR = coordBottomRight;

            let zoom = map.getZoom();

            dotnetHelper.invokeMethodAsync('GetRoutesGeoJson', coordTopLeft[1], coordTopLeft[0], coordBottomRight[1], coordBottomRight[0], zoom)
                .then(result => {


                    $.each(layers, function (index, value) {
                        map.removeLayer(value);
                    });

                    $.each(sources, function (index, value) {
                        map.removeSource(value);
                    });

                  
                    sources = [];
                    layers = [];

                    $.each(result.sources, function (index, source) {

                        map.addSource('route-' + index, source);
                        sources.push('route-' + index);

                        map.addLayer({
                            'id': 'route-' + index,
                            'type': 'line',
                            'source': 'route-' + index,
                            'layout': {
                                'line-join': 'round',
                                'line-cap': 'round'
                            },
                            'paint': {
                                'line-color': '#888',
                                'line-width': 8
                            }
                        });
                        layers.push('route-' + index);
                    });
                });
        });
    }
};