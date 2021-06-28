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


let lastTL = [];
let lastBR = [];

let sources = [];
let routelayers = [];
let runLayers = [];
let runCompletedPointLayers = [];

window.mapHelper = {
    init: dotnetHelper => {
        dotnetHelper = dotnetHelper;



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

                }, geolocationError, geolocationOptions
            );

            popup.on('close', function (e) {
                console.log("popup close");
                window.navigator.geolocation.clearWatch(geolocation);
            });

            $("#play-" + routeId).on('click', function () {

                hideAllRoutes();

                const routeRunDottedLineId = 'route-' + routeId + '-run-dotted-line';

                const routeRunStartLayer = 'route-' + routeId + '-run-start';
                const routeRunOtherLayer = 'route-' + routeId + '-run-other';
                const routeRunFinishLayer = 'route-' + routeId + '-run-finish';

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
                runLayers.push(routeRunDottedLineId);

                map.addLayer({
                    'id': routeRunStartLayer,
                    'type': 'circle',
                    'source': routeSourceId,
                    'paint': {
                        'circle-color': 'blue',
                        'circle-radius': 8,
                    },
                    'filter': ['all', ['==', '$type', 'Point'], ['==', 'type', 0]]
                });
                runLayers.push(routeRunStartLayer);

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
                runLayers.push(routeRunOtherLayer);


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
                runLayers.push(routeRunFinishLayer);

                $(".mapboxgl-popup-close-button").click();

                alertify.set('notifier', 'position', 'top-center');

                alertify.message('Przygotuj się', 2.5);

                console.log(routeId);

                setTimeout(function () {
                    alertify.message('3', 1);
                    setTimeout(function () {
                        alertify.message('2', 1);
                        setTimeout(function () {
                            alertify.message('1', 1);

                            createRun(dotnetHelper, routeId, routeSourceId);


                            setTimeout(function () {
                                alertify.success('Start!', 2);
                            }, 1000);
                        }, 1000);
                    }, 1000);
                }, 3200);

            })

        }
    }
};



function createRun(dotnetHelper, routeId, routeSourceId) {
    dotnetHelper.invokeMethodAsync('CreateRun', parseInt(routeId), current_latitude, current_longitude, current_accuracy)
        .then(json => {

            console.log(json);
            if (json.isSuccess === false) {
                alertify.error(json.message);
                alertify.error(json.errors);

                removeRunLayers();
                showAllRoutes();
                return false;

            } else {
                if (json.result.status == 2) {
                    console.log('Run completed');
                    alertify.success('Run completed');

                    removeRunLayers();
                    showAllRoutes();
                    return false;
                }

                completePoint(json, routeId, routeSourceId);

                let runId = json.result.id;
                let nextPoint = json.result.nextPoint;
                let nextPointId = nextPoint.pointId;
                let nextPointCoordinates = [nextPoint.longitude, nextPoint.latitude];
                let nextPointRadius = nextPoint.radius;

                let geolocationRunHandler = window.navigator.geolocation.watchPosition(
                    function(position) {
                        let crd = position.coords;

                        current_accuracy = crd.accuracy;
                        current_latitude = position.coords.latitude;
                        current_longitude = position.coords.longitude;

                        let distance = CalculateDistance(nextPointCoordinates[1], nextPointCoordinates[0], current_latitude, current_longitude);

                        let distanceText = '';
                        let accuracyText = '';

                        if (distance > 150000) {
                            distanceText = `Odległość do następnego punktu: > 150 km`;
                        }
                        else if (distance > 1000) {
                            distanceText = `Odległość do następnego punktu: ${(distance / 1000).toFixed(1)} km`;
                        }
                        else {
                            distanceText = `Odległość do następnego punktu: ${distance.toFixed(0)} metrów`;
                        }


                        if (current_accuracy.toFixed(0) > 10000) {
                            accuracyText = `Dokładność: > 10 km`;
                        }
                        else if (current_accuracy.toFixed(0) > 1000) {
                            accuracyText = `Dokładność: ${(current_accuracy / 1000).toFixed(1)} km`;
                        }
                        else {
                            accuracyText = `Dokładność: ${current_accuracy.toFixed(0)} metrów`;
                        }



                        if (distance <= nextPointRadius) {
                            console.log('Prawidłowa odległośc od następnego punktu');
                            dotnetHelper.invokeMethodAsync('ReachPoint', parseInt(runId), parseInt(nextPointId), current_latitude, current_longitude, current_accuracy)
                                .then(reachPointResult => {

                                    if (reachPointResult.isSuccess === false) {
                                        alertify.error(reachPointResult.message);
                                        alertify.error(reachPointResult.errors);

                                        return;
                                    }
                                    else {
                                        if (reachPointResult.result.status == 2) {
                                            console.log('Run completed');
                                            alertify.success('Run completed');

                                            removeRunLayers();
                                            showAllRoutes();

                                            window.navigator.geolocation.clearWatch(geolocationRunHandler);
                                            return false;
                                        }


                                        completePoint(reachPointResult, routeId, routeSourceId);

                                        nextPoint = reachPointResult.result.nextPoint;
                                        nextPointId = nextPoint.pointId;
                                        nextPointCoordinates = [nextPoint.longitude, nextPoint.latitude];
                                        nextPointRadius = nextPoint.radius;
                                    }

                                });
                        } else {
                            console.log(distanceText);
                            console.log(accuracyText);
                        }


                    }, geolocationError, geolocationOptions
                );

            }
        });
}

function hideAllRoutes() {
    $.each(routelayers, function (index, layerId) {
        map.setLayoutProperty(layerId, 'visibility', 'none');
    });
}

function removeRunLayers() {
    $.each(runLayers, function (index, layerId) {
        map.removeLayer(layerId);
    });
    runLayers = [];

    $.each(runCompletedPointLayers, function (index, layerId) {
        map.removeLayer(layerId);
    });
    runCompletedPointLayers = [];
}

function showAllRoutes() {
    $.each(routelayers, function (index, value) {
        map.setLayoutProperty(value, 'visibility', 'visible');
    });
}

function geolocationError(err) {
    console.warn(`ERROR(${err.code}): ${err.message}`);
}

function completePoint(json, routeId, routeSourceId) {
    const pointId = json.result.completedPoint.pointId;
    const runCompletedPoint = 'route-' + routeId + '-run-completed-point-' + pointId;

    if (jQuery.inArray(runCompletedPoint, runCompletedPointLayers) !== -1) {
        console.error("Point: " + runCompletedPoint + ' is already completed.');
        return;
    }

    map.addLayer({
        'id': runCompletedPoint,
        'type': 'circle',
        'source': routeSourceId,
        'paint': {
            'circle-color': 'green',
            'circle-radius': 6,
        },
        'filter': ['all', ['==', '$type', 'Point'], ['==', 'pointId', pointId]]
    });

    runCompletedPointLayers.push(runCompletedPoint);
}
