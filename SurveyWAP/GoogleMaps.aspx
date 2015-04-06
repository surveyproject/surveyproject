<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoogleMaps.aspx.cs" Inherits="Votations.NSurvey.WebAdmin.GoogleMaps" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <meta charset="utf-8" />

    <title>SP Maps</title>

       <style>
      html, body {
        height: 100%;
        margin: 0px;
        padding: 0px
      }

      #map-canvas
      {


          width:480px; 
          height: 300px; 
          margin-left: auto;
          margin-right: auto

      }

      #panel {
        position: absolute;
        top: 5px;
        left: 50%;
        margin-left: -180px;
        z-index: 5;
        background-color: #fff;
        padding: 5px;
        border: 1px solid #999;
      }
    </style>
     <style>
      #locationField, #controls {
        position: relative;
        width: 480px;
      }
      #autocomplete {
        position: absolute;
        top: 0px;
        left: 0px;
        width: 99%;
      }
      .label {
        text-align: right;
        font-weight: bold;
        width: 100px;
        color: #303030;
      }
      #address {
        border: 1px solid #000090;
        background-color: #f0f0ff;
        width: 480px;
        padding-right: 2px;
      }
      #address td {
        font-size: 10pt;
      }
      .field {
        width: 99%;
      }
      .slimField {
        width: 80px;
      }
      .wideField {
        width: 200px;
      }
      #locationField {
        height: 20px;
        margin-bottom: 2px;
      }
    </style>


     <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false&key=AIzaSyDwh2agwB_vfybUaiuLrl4Hopr9EDX6rUI&language=en"></script>
     <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&libraries=places"></script>
    
    <script>
        var geocoder;

        var map;
        var markers = [];

        var contentString = '<div style="overflow:visible" id="content">' +
'<h4 id="firstHeading">Position</h4>' +
'</div>';


        function initialize()
        {

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

        }



        function codeAddress()
        {
            var address = document.getElementById('autocomplete').value;

            geocoder.geocode({ 'address': address }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    map.setCenter(results[0].geometry.location);
                    map.setZoom(17);
                    var marker = new google.maps.Marker({
                        map: map,
                        draggable: false,
                        title: 'Your Position' ,
                        position: results[0].geometry.location
                    });

                    markers.push(marker);

                    var infowindow = new google.maps.InfoWindow({
                        content: contentString + '<div style="min-width:250px;">' + 'Latitude: ' + results[0].geometry.location.lat() + '<br />Longitude: ' + results[0].geometry.location.lng() + '</div>',
                        maxWidth: 425
                    });

                    google.maps.event.addListener(marker, 'click', function () {
                        infowindow.open(map, marker);
                    });


                } else {
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
            clearMarkers();
            markers = [];
            map.setZoom(2);
        }


        google.maps.event.addDomListener(window, 'load', initialize);

    </script>

    <script>
        // This example displays an address form, using the autocomplete feature
        // of the Google Places API to help users fill in the information.

        var placeSearch, autocomplete;
        var componentForm = {
            street_number: 'short_name',
            route: 'long_name',
            locality: 'long_name',
            administrative_area_level_1: 'short_name',
            country: 'long_name',
            postal_code: 'short_name'
        };

        function initialize() {
            // Create the autocomplete object, restricting the search
            // to geographical location types.
            autocomplete = new google.maps.places.Autocomplete(
                /** @type {HTMLInputElement} */(document.getElementById('autocomplete')),
                { types: ['geocode'] });
            // When the user selects an address from the dropdown,
            // populate the address fields in the form.
            google.maps.event.addListener(autocomplete, 'place_changed', function () {
                fillInAddress();
            });
        }

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
        }
        // [END region_fillform]



        // [START region_geolocation]
        // Bias the autocomplete object to the user's geographical location,
        // as supplied by the browser's 'navigator.geolocation' object.
        function geolocate() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    var geolocation = new google.maps.LatLng(
                        position.coords.latitude, position.coords.longitude);
                    autocomplete.setBounds(new google.maps.LatLngBounds(geolocation,
                        geolocation));
                });
            }
        }
        // [END region_geolocation]

    </script>


</head>
<body onload="initialize()">

        <form id="form1" runat="server">
    <div id="panel">
    <div id="locationField">
      <input id="autocomplete" placeholder="Enter your address" onfocus="geolocate()" type="text"/>
    </div>
    <table id="address">
      <tr>
        <td class="label">Street address</td>
        <td class="slimField">
            <input class="field" id="street_number" disabled="disabled" />
        </td>
        <td class="wideField" colspan="2">
            <input class="field" id="route" disabled="disabled" />
        </td>
      </tr>
      <tr>
        <td class="label">City</td>
        <td class="wideField" colspan="3">
            <input class="field" id="locality" disabled="disabled" />
        </td>
      </tr>
      <tr>
        <td class="label">State</td>
        <td class="slimField">
            <input class="field" id="administrative_area_level_1" disabled="disabled" />
        </td>
        <td class="label">Zip code</td>
        <td class="wideField">
            <input class="field" id="postal_code" disabled="disabled" />
        </td>
      </tr>
      <tr>
        <td class="label">Country</td>
        <td class="wideField" colspan="3">
            <input class="field" id="country" disabled="disabled" />
        </td>
      </tr>
    </table>
         <br />

<!-- 
      <input id="form_address" type="text" value="Type Your Address" />
    -->      
     <input type="button" value="Show on Map" onclick="codeAddress()" />

      <input onclick="deleteMarkers(); initialize()" type="button" value="Delete Markers"/>
        <br /><br />

       <div id="map-canvas"> </div>
    </div>
    </form>


</body>
</html>
