/// <reference path="/js/rentler.js" />

/*
Base namespace configuration.
*/
rentler = window.rentler || {};
rentler.__namespace = true;

rentler.google = rentler.google || {};
rentler.google.__namespace = true;

rentler.google.maps = rentler.google.maps || {};
rentler.google.maps.__namespace = true;

(function ($self, undefined) {
      
    $self.Map = function(domMapId) {
        var self = this;
                
        self.map = new google.maps.Map(
            document.getElementById(domMapId), 
            { 
                zoom: 12,
                center: new google.maps.LatLng(40.7608, -111.8910),
                mapTypeId: google.maps.MapTypeId.ROADMAP
            }
        );

        // register info window close handler on map
        google.maps.event.addListener(self.map, 'click', function () {
            if(infoWindow)
                infoWindow.close();
        });

        markers = new Array();
        infoWindow = null;
        
        self.addMarker = function(lat, lon, title, objRefId) {
            // for now objRefId is listingId
            var marker = new google.maps.Marker({
                position: new google.maps.LatLng(lat, lon),
                map: self.map,
                title: title
            });

            marker.listingId = objRefId;

            markers.push(marker);
            return marker;
        };

        self.clearMarkers = function() {
            if (markers) {
                for (i in markers) {
                    markers[i].setMap(null);
                }
                markers.length = 0;
            }
        };

        self.bindInfoWindow = function(content, marker) {
            if (!infoWindow)
                infoWindow = new google.maps.InfoWindow();

            google.maps.event.addListener(
                marker, 'click', 
                function () {
                    infoWindow.setContent(content);
                    infoWindow.open(self.map, marker);
                }
            );            

            return infoWindow;
        };
        
        self.frameResults = function() {
            //finished adding markers; now we set the map up
            //so it will nicely frame all of the results in the view.
            var latlngbounds = new google.maps.LatLngBounds();
                    
            for (i in markers) {
                var pos = markers[i].getPosition();                
                latlngbounds.extend(markers[i].getPosition());
            }

            self.map.setCenter(latlngbounds.getCenter());
            self.map.fitBounds(latlngbounds);
        };
    };
    $self.Map.__class = true;
    
    $self.init = function () {
        // initialize here        
    };

    // initialize nicely
    $(document).ready(function () { $self.init(); });

} (rentler.google.maps));