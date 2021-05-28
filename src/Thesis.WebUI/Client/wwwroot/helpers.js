function CalculateDistance(lat1, long1, lat2, long2) {

    console.debug('lat1: ' + lat1 + ' lon1: ' + long1);
    console.debug('lat2: ' + lat2 + ' lon2: ' + long2);

    var R = 6378137; // Earth’s mean radius in meter
    var dLat = rad(lat2 - lat1);
    var dLong = rad(long2 - long1);
    var a = Math.sin(dLat / 2) * Math.sin(dLat / 2) +
        Math.cos(rad(lat1)) * Math.cos(rad(lat2 )) *
        Math.sin(dLong / 2) * Math.sin(dLong / 2);
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    var d = R * c;
    return d; // returns the distance in meter
} 
var rad = function (x) {
    return x * Math.PI / 180;
};