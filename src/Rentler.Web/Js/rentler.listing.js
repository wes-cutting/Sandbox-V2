/// <reference path="/js/rentler.js" />

/*
Base namespace configuration.
*/
rentler = window.rentler || {};
rentler.__namespace = true;

rentler.listing = rentler.listing || {};
rentler.listing.__namespace = true;

(function ($self) {

    $self.vars = {
        maxReportCharacters: 1000
    };    

    $self.imInterested = function() {
        var buttons = $('[data-link="interested"]'),
            characterCount = $('.im-interested .characters'),
            text = $('#interestMessage'),            
            cancelButton = $('a[data-link="cancel-interest"]');

        buttons.live('click', function(e) {
            e.preventDefault();

			//todo:brb
			var authUrl = $(this).attr('data-auth-url');
			if(authUrl != "")
			{
				top.location.href = authUrl;
				return false;
			}
			
			rentler.api.stats.trackEvent("listing", "click", "iminterested");

            var interested = $('.im-interested');
            interested.removeClass('hide');
            text.focus();            
        });

        text.live('keyup', function(e) {
            var item = $(this),
                value = item.val();
            if(value > $self.vars.maxReportCharacters)
                item.val(value.substring(0, $self.vars.maxReportCharacters));
            characterCount.text($self.vars.maxReportCharacters - value.length);
        });
        
        cancelButton.live('click', function(e) {
            e.preventDefault();
            var interested = $('.im-interested');
            text.val("");
            characterCount.text($self.vars.maxReportCharacters);
            interested.addClass('hide');            
            $('.interest-error').addClass('hide').html("");
        });        
    };

    $self.reporting = function() {
        var reportShare = $('.report-share'),
            reportInfo = $('.report-info'),
            cancel = $('.cancel-report'),
            text = $('textarea[name="reportText"]'),
            reportButton = $('.report a'),
            submit = $('.report-info button[type="submit"]'),
            characterCount = $('.report-info .character-count'),
            report = $('.report'),
            listingReported = $('.listing-reported');
        
        reportButton.live('click', function(e) {
            e.preventDefault();
            reportShare.hide();
            reportInfo.show();
            text.focus();
        });

        cancel.live('click', function(e) {
            e.preventDefault();
            reportShare.show();
            reportInfo.hide();
            text.val("");
        });

        text.live('keyup', function(e) {
            var item = $(this),
                value = item.val();   
            if(value > $self.vars.maxReportCharacters)
                item.val(value.substring(0, $self.vars.maxReportCharacters));
            characterCount.text($self.vars.maxReportCharacters - value.length);
        });

        submit.live('click', function(e) {
            e.preventDefault();
            var buildingId = $(this).attr("data-buildingId");

            reportShare.show();
            reportInfo.hide();
            report.html('Thank you');

            rentler.api.listing.report(buildingId, text.val(), function(data) {
                listingReported.show();
            });
        });
    };

    $self.favoriteListing = function() {
        var button = $('.button.favorite');

        button.live('click', function(e) {
            e.preventDefault();

            var item = $(this),
                label = $('.label.favorite'),
                buildingId = item.attr('data-buildingId')
                authUrl = item.attr('data-auth-url');
            
            if(authUrl != "")
				top.location.href = authUrl;

            rentler.api.listing.favorite(buildingId, function(data) { });

            label.html("Favorited");
            item.removeClass("favorite").addClass("unfavorite");
        });
    };

    $self.unfavoriteListing = function() {
        var button = $('.button.unfavorite');

        button.live('click', function(e) {
            e.preventDefault();

            var item = $(this),
                label = $('.label.favorite'),
                buildingId = item.attr('data-buildingId');
            
            rentler.api.listing.unfavorite(buildingId, function(data){ });

            label.html("Favorite");
            item.removeClass("unfavorite").addClass("favorite");
        });
    };

    $self.listingTabs = function() {
        links = $('.listing-tabs a');

        links.live('click', function(e) {
            e.preventDefault();

            var link = $(this),
                tab = link.parent(),
                id = link.attr('data-tab'),
                div = $("#" + id)
                tabs = $('.listing-tabs li');
                tabDivs = $('.amenities-list');;

            tabs.removeClass("active");
            tabDivs.removeClass("active");
            div.addClass("active");
            tab.addClass("active");
        });
    };

    $self.carousel = function() {

        var car = $('.image-carousel'),
            imageLinks = $('.image-carousel a');

        car.jCarouselLite({
            btnNext: ".carousel-next",
            btnPrev: ".carousel-previous",
            circular: false,
            visible: 8
        });

        imageLinks.live('click', function(e) {
            e.preventDefault();

            var imageUrl = $(this).attr('href'),
                photo = $('.photo-container .photo img'),
                map = $('#map');

            photo.attr('src', imageUrl);
            photo.parent().removeClass('hide');
            map.addClass('hide');
        });
    };

    $self.map = function() {
        var self = this;

        self.selection = $('.map-selection');

        self.addMarker = function(map, latitude, longitude, title) {
            var marker = new google.maps.Marker({
				position: new google.maps.LatLng(latitude, longitude),
				map: map,
				title: title
			});
            return marker;
        };

        self.load = function() {
            var lat = $('div[data-lat]').attr('data-lat'),
                long = $('div[data-long]').attr('data-long'),
                latLong = new google.maps.LatLng(lat, long),
                mapOptions = {
				    zoom: 15,
				    center: latLong,
				    mapTypeControl: false,
				    mapTypeId: google.maps.MapTypeId.ROADMAP,
					zoomControlOptions : {
						position: google.maps.ControlPosition.LEFT_CENTER
					},
					panControlOptions: {
						position: google.maps.ControlPosition.TOP_RIGHT
					},
					streetViewControlOptions : {
						position: google.maps.ControlPosition.LEFT_BOTTOM
					}
			    },
                map = new google.maps.Map(document.getElementById('map'), mapOptions);

            self.addMarker(map, lat, long, "Listing");
        };

		self.selection.live('click', function(e) {
			e.preventDefault();
			var jMap = $('#map'),
				photo = $('.photo-container .photo');

			jMap.removeClass('hide');

			photo.addClass('hide');
			$('.interest-listing-preview .photo').removeClass('hide');

			self.load();
		});

		$('.listing').live('loadMap', function(e){
			var jMap = $('#map'),
				photo = $('.photo');

			jMap.removeClass('hide');

			photo.addClass('hide');
			$('.interest-listing-preview .photo').removeClass('hide');

			self.load();
		});
    };

    $self.setupSharing = function() {
        var script = 'https://s7.addthis.com/js/250/addthis_widget.js?domready=1#pubid=ra-4e495ef7138737dd';
		if (window.addthis) window.addthis = null;
		$.getScript(script);
    };

	$self.init = function () {
        $self.carousel();
        $self.map();
        $self.listingTabs();
        $self.favoriteListing();
        $self.unfavoriteListing();
        $self.reporting();
        $self.imInterested();

		//no photos? just load the map then.
		if($('.carousel').length == 0){
			$('.listing').trigger('loadMap');
		}
        
		

        $('#sendInterest').on('click', function() {
            var error = $('.interest-error');

            // hide any previous errors
            error.addClass('hide').html("");

            var interested = $('.im-interested');
            var text = $('#interestMessage');                                    

            var data = { 
                BuildingId : text.attr('data-listingid'),
                Content : text.val()
            };

            rentler.api.stats.trackEvent("listing", "click", "sendemail");

            rentler.api.listing.interested(data, function(status) {                
                if(status.StatusCode == 200) {                                   
                    text.val("");
                    $('.im-interested .characters').text($self.vars.maxReportCharacters);
                    interested.addClass('hide');
                }
                else {
                    error
                        .html(
                            "We're sorry but we were unable to notify the landlord of your interest in this property. " + 
                            "Please try again and if the problem persists please contact Rentler Support.")
                        .removeClass('hide');
                }
            });
        });
    };

    $self.finalize = function() {
        $('#sendInterest').off('click');
    };

	// initialize nicely
	$(document).ready(function () { $self.init(); });

})(rentler.listing);