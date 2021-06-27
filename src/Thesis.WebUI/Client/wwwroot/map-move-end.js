const colors = ["#00DC30", "#2D35FF", "#DC0D00", "#000000"];
const geolocationOptions = {
    enableHighAccuracy: true,
    timeout: 5000,
    maximumAge: 250
};
let current_accuracy = undefined;
let current_latitude = undefined;
let current_longitude = undefined;
let dotnetHelper = undefined;

window.mapHelper = {
    init: dotnetHelper => {
        dotnetHelper = dotnetHelper;

        let lastTL = [];
        let lastBR = [];

        let sources = [];
        let routelayers = [];
        let runlayers = [];

        map.on('moveend', (eventData) => {
            let canvas = map.getCanvas()
            let w = canvas.width
            let h = canvas.height

            let coordTopLeft = map.unproject([0, 0]).toArray()
            let coordBottomRight = map.unproject([w, h]).toArray()

            if (lastTL.length == 2 || lastBR.length == 2) {
                if (lastTL[1] >= coordTopLeft[1] && lastTL[0] <= coordTopLeft[0] && lastBR[1] <= coordBottomRight[1] && lastBR[0] >= coordBottomRight[0]) {
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

                        console.log(result.source);

                        const routeSourceId = 'route-' + result.routeId.toString() + '-source';
                        const routeLayerLineId = 'route-' + routeSourceId + "-line";
                        const routeLayerSymbolId = 'route-' + routeSourceId + '-symbol';

                        if (jQuery.inArray(routeSourceId, sources) !== -1) {
                            return;
                        }

                        map.addSource(routeSourceId, result.source);

                        map.addLayer({
                            'id': routeLayerLineId,
                            'type': 'line',
                            'source': routeSourceId,
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

                        map.addLayer({
                            'id': routeLayerSymbolId,
                            'type': 'symbol',
                            'source': routeSourceId,
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
                            'filter': ['all', ['==', '$type', 'Point'], ['==', 'type', 0]]
                        });

                        sources.push(routeSourceId);
                        routelayers.push(routeLayerLineId);
                        routelayers.push(routeLayerSymbolId);

                        map.on('click', routeLayerSymbolId, onRouteClick);

                        map.on('mouseenter', routeLayerSymbolId, function (e) {
                            map.getCanvas().style.cursor = 'pointer';
                        });

                        map.on('mouseleave', routeLayerSymbolId, function () {
                            map.getCanvas().style.cursor = '';
                        });
                    });
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

            var currentCoordinates = e.features[0].geometry.coordinates.slice();
            var routeName = e.features[0].properties.title;
            var routeId = e.features[0].properties.routeId;
            var routeSourceId = e.features[0].source.toString();

            while (Math.abs(e.lngLat.lng - currentCoordinates[0]) > 180) {
                currentCoordinates[0] += e.lngLat.lng > currentCoordinates[0] ? 360 : -360;
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
            info.prop("id", "info-" + routeId);

            var distance = $("<div />");
            distance.addClass("m-2");
            distance.prop("id", "distance-" + routeId);
            distance.text("Odległość: obliczanie...");

            var accuracy = $("<div />");
            accuracy.addClass("m-2");
            accuracy.prop("id", "accuracy-" + routeId);
            accuracy.text("Dokładność: obliczanie...");

            var rankBtn = $("<button />");
            rankBtn.addClass("btn btn-info m-2");
            rankBtn.text("Ranking");

            var playBtn = $("<button />");
            playBtn.addClass("btn btn-warning m-2");
            playBtn.prop("id", "play-" + routeId);
            playBtn.prop('disabled', true);
            playBtn.text("Trwa lokalizacja");

            bodyContainer.append(info, distance, accuracy, rankBtn, playBtn);

            mainContainer.append(headerWrapper, bodyContainer);

            var mainContainerHTML = $("<div />").append($(mainContainer).clone()).html()
            console.log(mainContainerHTML);

            popup.setLngLat(currentCoordinates).setHTML(mainContainerHTML).addTo(map);

            let geolocation = window.navigator.geolocation.watchPosition(
                function (position) {
                    var crd = position.coords;

                    let playBtnEle = $("#play-" + routeId);
                    current_accuracy = crd.accuracy;
                    current_latitude = position.coords.latitude;
                    current_longitude = position.coords.longitude;
                    var distance = CalculateDistance(currentCoordinates[1], currentCoordinates[0], current_latitude, current_longitude);
                    console.log("Distance: " + distance);

                    let distanceEle = $("#distance-" + routeId);
                    let accuracyEle = $("#accuracy-" + routeId);

                    if (distance > 150000) {
                        distanceEle.text(`Odległość: > 150 km`);
                    }
                    else if (distance > 1000) {
                        distanceEle.text(`Odległość: ${(distance / 1000).toFixed(1)} km`);
                    }
                    else {
                        distanceEle.text(`Odległość: ${distance.toFixed(0)} metrów`);
                    }


                    if (current_accuracy.toFixed(0) > 10000) {
                        accuracyEle.text(`Dokładność: > 10 km`);
                    }
                    else if (current_accuracy.toFixed(0) > 1000) {
                        accuracyEle.text(`Dokładność: ${(current_accuracy / 1000).toFixed(1)} km`);
                    }
                    else {
                        accuracyEle.text(`Dokładność: ${current_accuracy.toFixed(0)} metrów`);
                    }

                    let infoEle = $("#info-" + routeId)

                    if (distance > 10) {
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

            $("#play-" + routeId).on('click', function () {

                // Hide all routes
                $.each(routelayers, function (index, value) {
                    map.setLayoutProperty(value, 'visibility', 'none');
                });

                const routeRunDottedLineId = 'route-run-' + routeId + '-dotted-line';

                const routeRunStartLayer = 'route-run-' + routeId + '-start';
                const routeRunOtherLayer = 'route-run-' + routeId + '-other';
                const routeRunFinishLayer = 'route-run-' + routeId + '-finish';

                map.addLayer({
                    'id': routeRunDottedLineId,
                    'type': 'line',
                    'source': routeSourceId,
                    'layout': {
                        'line-join': 'round',
                        'line-cap': 'round'
                    },
                    'paint': {
                        'line-color': 'grey',
                        'line-width': 4,
                        'line-dasharray': [4, 3],
                    },
                    'filter': ['==', '$type', 'LineString']
                });

                map.addLayer({
                    'id': routeRunStartLayer,
                    'type': 'circle',
                    'source': routeSourceId,
                    'paint': {
                        'circle-color': 'green',
                        'circle-radius': 8,
                    },
                    'filter': ['all', ['==', '$type', 'Point'], ['==', 'type', 0]]
                });

                map.addLayer({
                    'id': routeRunOtherLayer,
                    'type': 'circle',
                    'source': routeSourceId,
                    'paint': {
                        'circle-color': 'grey',
                        'circle-radius': 8,
                    },
                    'filter': ['all', ['==', '$type', 'Point'], ['==', 'type', 3]]
                });


                map.addLayer({
                    'id': routeRunFinishLayer,
                    'type': 'circle',
                    'source': routeSourceId,
                    'paint': {
                        'circle-color': 'red',
                        'circle-radius': 8,
                    },
                    'filter': ['all', ['==', '$type', 'Point'], ['==', 'type', 2]]
                });

                $(".mapboxgl-popup").remove();

                alertify.set('notifier', 'position', 'top-center');

                alertify.message('Przygotuj się', 2.5);

                setTimeout(function () {
                    alertify.message('3', 1);
                    setTimeout(function () {
                        alertify.message('2', 1);
                        setTimeout(function () {
                            alertify.message('1', 1);

                            dotnetHelper.invokeMethodAsync('CreateRun', parseInt(routeId), current_latitude, current_longitude, current_accuracy)
                                .then(json => {

                                    playButton.text(textLast);
                                    playButton.prop('disabled', false);

                                    console.log(json);
                                    if (json.isSuccess === false) {
                                        infoButton.text(json.message);
                                        return false;

                                    } else {
                                        //Draw Route
                                    }
                                });


                            setTimeout(function () {
                                alertify.success('Start!', 2);
                            }, 1000);
                        }, 1000);
                    }, 1000);
                }, 3200);


                //console.log("playBtn click");

                //let playButton = $("#play-" + routeId);
                //let infoButton = $("#info-" + routeId);
                //infoButton.text('');

                //playButton.prop('disabled', true);
                //var textLast = playButton.text();
                //playButton.text("Rozpoczynanie");

                //dotnetHelper.invokeMethodAsync('CreateRun', parseInt(source), current_latitude, current_longitude, current_accuracy)
                //    .then(json => {

                //        playButton.text(textLast);
                //        playButton.prop('disabled', false);

                //        console.log(json);
                //        if (json.isSuccess === false) {
                //            infoButton.text(json.message);
                //            return false;

                //        } else {
                //            //Draw Route
                //        }
                //    });
            })
            function error(err) {
                console.warn(`ERROR(${err.code}): ${err.message}`);

                info.text(`Błąd (${err.code}): ${err.message}`)
            }
        }
    }
};