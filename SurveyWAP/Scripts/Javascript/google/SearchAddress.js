//JQUERY
//check empty streetnumber input:

$(document).ready(function () {

    $('#SurveyControl_SurveySubmit').click(function (event) {
        if ($('#street_number').val() == '' && $('#autocomplete').val() != '') {
            alert('Street Number can not be left blank');
            event.preventDefault();
        }
    });

});

//JAVASCRIPT:
// I. Google Maps: Show Markers

var geocoder;

var map;
var markers = [];

var contentString = '<div style="overflow:visible" id="content">' +
'<h4 id="firstHeading">Position</h4>' +
'</div>';


function initialize() {

    geocoder = new google.maps.Geocoder();

    var latlng = new google.maps.LatLng(52.3655169, 4.9409481);

    var mapOptions = {
        zoom: 6,
        center: latlng,

        mapTypeId: google.maps.MapTypeId.ROADMAP,
        mapTypeControl: true,
        mapTypeControlOptions: {
            style: window.google.maps.MapTypeControlStyle.HORIZONTAL_BAR
        }
    }


        map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);


    // Create the autocomplete object, restricting the search
    // to geographical location types.

    autocomplete = new google.maps.places.Autocomplete(
        /** @type {HTMLInputElement} */
        (document.getElementById('autocomplete')),
        { types: ['geocode'] }
        );  
    

    // When the user selects an address from the dropdown,
    // populate the address fields in the form.
    google.maps.event.addListener(autocomplete, 'place_changed', function () {
        emptyFullAddress();
        fillInAddress();
        codeAddress();
    });


    if (document.getElementById('autocomplete').value != "")
    {
        codeAddress();
        fillAddressFields();
    }

}

//function initialize() {
//    // Create the autocomplete object, restricting the search
//    // to geographical location types.
//    autocomplete = new google.maps.places.Autocomplete(
//        /** @type {HTMLInputElement} */(document.getElementById('autocomplete')),
//        { types: ['geocode'] });
//    // When the user selects an address from the dropdown,
//    // populate the address fields in the form.
//    google.maps.event.addListener(autocomplete, 'place_changed', function () {
//        fillInAddress();
//    });
//}


function codeAddress() {
    var address = document.getElementById('autocomplete').value;

    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            map.setCenter(results[0].geometry.location);
            map.setZoom(17);
            var marker = new google.maps.Marker({
                map: map,
                draggable: false,
                title: 'Your Position',
                position: results[0].geometry.location
            });

            markers.push(marker);

            var infowindow = new google.maps.InfoWindow({
                content: contentString + '<div style="min-width:250px;">' + address + '<br />'+ 'Latitude: ' + results[0].geometry.location.lat() + '<br />Longitude: ' + results[0].geometry.location.lng() + '</div>',
                maxWidth: 325
            });

            google.maps.event.addListener(marker, 'click', function () {
                infowindow.open(map, marker);
            });

        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }

    });
}


// function to fill address fields if address selected before
function fillAddressFields() {
    var address = document.getElementById('autocomplete').value;

    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK)
        {
            for (var component in componentForm) {
                document.getElementById(component).value = '';
                document.getElementById(component).disabled = false;
            }

            // Get each component of the address from the place details
            // and fill the corresponding field on the form.
            for (var i = 0; i < results[0].address_components.length; i++) {
                var addressType = results[0].address_components[i].types[0];
                if (componentForm[addressType]) {
                    var val = results[0].address_components[i][componentForm[addressType]];
                    document.getElementById(addressType).value = val;
                }
            }


        }
        else
        {
            alert('Geocode was not successful for the following reason: ' + status);
        }

    });
}



// Sets the map on all markers in the array.
function setAllMap(map) {
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(map);
    }
}

// Removes the markers from the map, but keeps them in the array.
function clearMarkers() {
    setAllMap(null);
}

// Shows any markers currently in the array.
function showMarkers() {
    setAllMap(map);
}

// Deletes all markers in the array by removing references to them.
function deleteMarkers() {
    deleteFullAddress();
    clearMarkers();
    markers = [];
    map.setZoom(2);
}

google.maps.event.addDomListener(window, 'load', initialize);

//////////////////////////////////////////////////////////////////////////////////////////////////

//II. Google Maps: Search Address

// This example displays an address form, using the autocomplete feature
// of the Google Places API to help users fill in the information.

var placeSearch, autocomplete;

var componentForm = {
    street_number: 'short_name',
    route: 'long_name',
    locality: 'long_name',
    administrative_area_level_1: 'short_name',
    postal_code: 'short_name',
    country: 'long_name'
};

//function initialize() {
//    // Create the autocomplete object, restricting the search
//    // to geographical location types.
//    autocomplete = new google.maps.places.Autocomplete(
//        /** @type {HTMLInputElement} */(document.getElementById('autocomplete')),
//        { types: ['geocode'] });
//    // When the user selects an address from the dropdown,
//    // populate the address fields in the form.
//    google.maps.event.addListener(autocomplete, 'place_changed', function () {
//        fillInAddress();
//    });
//}

// [START region_fillform]
function fillInAddress() {
    // Get the place details from the autocomplete object.

        var place = autocomplete.getPlace();

    for (var component in componentForm) {
        document.getElementById(component).value = '';
        document.getElementById(component).disabled = false;
    }

    // Get each component of the address from the place details
    // and fill the corresponding field on the form.
    for (var i = 0; i < place.address_components.length; i++) {
        var addressType = place.address_components[i].types[0];
        if (componentForm[addressType]) {
            var val = place.address_components[i][componentForm[addressType]];
            document.getElementById(addressType).value = val;
        }
    }

    // Get each component of the address from the place details
    // and fill the hidden field on the form.
    for (var i = 0; i < place.address_components.length; i++) {
        var addressType = place.address_components[i].types[0];

        if (componentForm[addressType]) {
            var val = place.address_components[i][componentForm[addressType]];
            document.getElementById('hiddenFullAddress').value += val + ",";
        }
    }


}
// [END region_fillform]



function emptyFullAddress()
{
   // hiddenFullAddress.value = "";
    document.getElementById('hiddenFullAddress').value = "";
    //autocomplete.value = "";
}

function deleteFullAddress()
{
    //document.forms['Form1'].reset();

    //hiddenFullAddress.value = "";
    document.getElementById('hiddenFullAddress').value = null;
    document.getElementById('autocomplete').value = "";

    document.getElementById('street_number').value = "";
    document.getElementById('route').value = "";
    document.getElementById('locality').value = "";
    document.getElementById('administrative_area_level_1').value = "";
    document.getElementById('postal_code').value = "";
    document.getElementById('country').value = "";

    document.getElementById('street_number').disabled = true;
    document.getElementById('route').disabled = true;
    document.getElementById('locality').disabled = true;
    document.getElementById('administrative_area_level_1').disabled = true;
    document.getElementById('postal_code').disabled = true;
    document.getElementById('country').disabled = true;
}


// NOT USED: part of FILLFORM code
//function fullAddress()
//{
//    var place = autocomplete.getPlace();

//    for (var i = 0; i < place.address_components.length; i++)
//    {
//        var addressType = place.address_components[i].types[0];

//        if (componentForm[addressType])
//        {
//            var val = place.address_components[i][componentForm[addressType]];
//            testAddress.value += val + ",";
//        }
//    }

//}
        


// REDUNDANT CODE: NOT USED on SP
// [START region_geolocation]
// Bias the autocomplete object to the user's geographical location,
// as supplied by the browser's 'navigator.geolocation' object.
//function geolocate()
//{
//    if (navigator.geolocation)
//    {
//        navigator.geolocation.getCurrentPosition(function (position)
//        {
//            var geolocation = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
//            autocomplete.setBounds(new google.maps.LatLngBounds(geolocation, geolocation));
//        }
//        );
//    } else
//    {
//        alert("Sorry, browser does not support geolocation!");
//    }
//}
// [END region_geolocation]
