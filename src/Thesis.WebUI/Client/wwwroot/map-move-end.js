const colors = ["#00DC30", "#2D35FF", "#DC0D00", "#000000"];
const geolocationOptions = {
    enableHighAccuracy: true,
    timeout: 5000,
    maximumAge: 250
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



                        map.on('click', 'sp-' + routeId, onRouteClick);

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

            console.log("Route click");

            var popup = new mapboxgl.Popup({
                closeButton: true,
                closeOnClick: true,
                offset: [0, -50]
            });
            console.log(e.features[0]);

            var coordinates = e.features[0].geometry.coordinates.slice();
            var routeName = e.features[0].properties.title;
            var source = e.features[0].source;

            while (Math.abs(e.lngLat.lng - coordinates[0]) > 180) {
                coordinates[0] += e.lngLat.lng > coordinates[0] ? 360 : -360;
            }

            //Header
            var mainContainer = $("<div />");
            mainContainer.addClass("d-flex flex-column text-center popup-main");

            var headerWrapper = $("<div />");
            headerWrapper.addClass("d-flex flex-row justify-content-center");

            var title = $("<div />").text(routeName);
            title.addClass("m-2 font-weight-bold");
            headerWrapper.append(title);

            //Body
            var bodyContainer = $("<div />");
            bodyContainer.addClass("d-flex flex-column");

            var info = $("<div />");
            info.addClass("m-2 text-danger");
            info.prop("id", "info-" + source);

            var distance = $("<div />");
            distance.addClass("m-2");
            distance.prop("id", "distance-" + source);
            distance.text("Odległość: obliczanie...");

            var accuracy = $("<div />");
            accuracy.addClass("m-2");
            accuracy.prop("id", "accuracy-" + source);
            accuracy.text("Dokładność: obliczanie...");

            var rankBtn = $("<button />");
            rankBtn.addClass("btn btn-info m-2");
            rankBtn.text("Ranking");

            var playBtn = $("<button />");
            playBtn.addClass("btn btn-warning m-2");
            playBtn.prop("id", "play-" + source);
            playBtn.prop('disabled', true);
            playBtn.text("Trwa lokalizacja");

            bodyContainer.append(info, distance, accuracy, rankBtn, playBtn);

            mainContainer.append(headerWrapper, bodyContainer);

            var mainContainerHTML = $("<div />").append($(mainContainer).clone()).html()
            console.log(mainContainerHTML);

            popup.setLngLat(coordinates).setHTML(mainContainerHTML).addTo(map);

            let geolocation = window.navigator.geolocation.watchPosition(
                function (position) {
                    var crd = position.coords;

                    let playBtnEle = $("#play-" + source);
                    var lat = position.coords.latitude;
                    var long = position.coords.longitude;
                    var distance = CalculateDistance(coordinates[1], coordinates[0], lat, long);
                    console.log("Distance: " + distance);

                    let distanceEle = $("#distance-" + source);
                    let accuracyEle = $("#accuracy-" + source);

                    if (distance > 150000) {
                        distanceEle.text(`Odległość: > 150 km`);
                    }
                    else if (distance > 1000) {
                        distanceEle.text(`Odległość: ${(distance / 1000).toFixed(1)}} km`);
                    }
                    else {
                        distanceEle.text(`Odległość: ${distance.toFixed(0)} metrów`);
                    }


                    if (crd.accuracy.toFixed(0) > 10000) {
                        accuracyEle.text(`Dokładność: > 10 km`);
                    }
                    else if (crd.accuracy.toFixed(0) > 1000) {
                        accuracyEle.text(`Dokładność: ${(crd.accuracy / 1000).toFixed(1)} km`);
                    }
                    else {
                        accuracyEle.text(`Dokładność: ${crd.accuracy.toFixed(0)} metrów`);
                    }

                    let infoEle = $("#info-" + source)

                    if (distance > 50) {
                        playBtnEle.removeClass("btn-warning");
                        playBtnEle.removeClass("btn-success");
                        playBtnEle.addClass("btn-danger");
                        playBtnEle.text("Jesteś zbyt daleko");
                        playBtnEle.prop('disabled', true);
                    }
                    else if (crd.accuracy > 150) {
                        playBtnEle.removeClass("btn-danger");
                        playBtnEle.removeClass("btn-success");
                        playBtnEle.addClass("btn-warning");
                        playBtnEle.text("Zbyt słaby sygnał");
                        playBtnEle.prop('disabled', true);
                    }
                    else {
                        playBtnEle.removeClass("btn-danger");
                        playBtnEle.removeClass("btn-warning");
                        playBtnEle.addClass("btn-success");
                        playBtnEle.prop('disabled', false);
                        playBtnEle.text("Rozpocznij");
                    }

                    infoEle.text();

                }, error, geolocationOptions
            );

            popup.on('close', function (e) {
                console.log("popup close");
                window.navigator.geolocation.clearWatch(geolocation);
            });

            function error(err) {
                console.warn(`ERROR(${err.code}): ${err.message}`);

                info.text(`Błąd (${err.code}): ${err.message}`)
            }
        }
    }
};