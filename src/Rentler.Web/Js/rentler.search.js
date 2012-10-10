/// <reference path="/js/rentler.js" />
/// <reference path="/scripts/jquery.infinitescroll.js" />

/*
    Base namespace configuration.
*/
rentler = window.rentler || {};
rentler.__namespace = true;

rentler.search = rentler.search || {};
rentler.search.__namespace = true;

(function ($self, undefined) {
    
    $self.vars = {
        hasPopupOpen: false,
        ajaxSearchUrl: "/search/ajaxsearch"
    };

    $self.displayListing = function() {
        var resultLinks = $('.result a'),
            scrimBackground = $('.scrim.background'),
            container = $('.listing-scrim-container'),
            listingScrim = $('.listing-scrim'),
            searchResults = $('.search-results'),
            footer = $('#footer-container'),
            header = $("#header"),
            activePosition = 0,
            savedHeight = 0,
            win = $(window);

        container.live('click', function(e) {
            e.stopPropagation();
        });

        resultLinks.live('click', function(e) {
            e.preventDefault();
            e.stopPropagation();

            var url = $(this).attr("data-quick");
            var currentUrl = window.location.href;

            rentler.api.stats.trackPageView(url);

            $self.vars.hasPopupOpen = true;

            if (window.history && window.history.replaceState) {
                window.history.replaceState({}, "", url);
                window.history.replaceState({}, "", currentUrl);
            }

            // set fixed positioning on the results
            var position = win.scrollTop();
            activePosition = position;
            searchResults.css('margin-top', -position + 'px');
            savedHeight = searchResults.height();
            searchResults.addClass('active').height(savedHeight);
            footer.hide();

            if(activePosition > 49)
                header.css('top', '-63px');

            // show all the information
            scrimBackground.removeClass('hide');
            listingScrim.removeClass('hide');

            win.scrollTop(0);

            $.get(url, function(data) {
                container.html(data);
				rentler.listing.init();

//                try
//                {
//                    var script = 'https://s7.addthis.com/js/250/addthis_widget.js?domready=1#pubid=ra-4e495ef7138737dd';
//		            if (window.addthis) window.addthis = null;
//		            $.getScript(script);
//                } catch(err) {
//                    // ignore addthis errors for now
//                }
            });
        });

        listingScrim.live('click', function(e) {
            // undo listing init so we don't unnecessarily
            // register event handlers that are already
            // registered

			//need to explicitly remove the map to avoid an error in IE7/8.
			//see #35714547
			$('#map').remove();

            rentler.listing.finalize();
            
            scrimBackground.addClass('hide');
            listingScrim.addClass('hide');

            searchResults.css('margin-top', '0px');
            searchResults.removeClass('active').height('auto');
            footer.show();
            win.scrollTop(activePosition);
            $self.vars.hasPopupOpen = false;

			container.html('<img class="container-loader" src="/images/loader.gif" alt="Loading..." />');
        });
    };

    $self.advancedDropDown = function() {
        var self = this,
            openLink = $('[data-open]'),
            closeLink = $('[data-close]');

        openLink.live("click", function(e) {
            e.preventDefault();
            var link = $(this),
                id = $(this).attr("data-open"),
                item = $('#' + id);
            item.addClass("open");
        });

        closeLink.live("click", function(e) {
            e.preventDefault();
            var link = $(this),
                id = $(this).attr("data-close"),
                item = $('#' + id);
            item.removeClass("open");
        });
    };

    $self.infiniteScroll = function() {        
		var searchResults = $('.search-results');
		
		if(searchResults.length == 0)
			return;

        searchResults.infinitescroll({
			navSelector: "div.navigation", 			// selector for the paged navigation (it will be hidden)
            nextSelector: "div.navigation a:last", 	// selector for the NEXT link (to page 2)
            itemSelector: ".result",				// selector for all items you'll retrieve
			loading : { 
				img : "/images/loader.gif", 
				msgText : "",
				finishedMsg : "",
				selector : ".loading"
			},
            animate: false,
            bufferPx: 600
        }, function(e) {
            // set a google analytics section                                    
                        
            // get count of visible results
            var totalResults = $('li.result').length;

            // if less than 30 only 1 page and it was
            // tracked initially
            if(totalResults >= 30) { 
                var page = 1;               
                var rem = totalResults % 30;
                
                if(rem > 0) {
                    page = (totalResults - rem) / 30;
                    page = page + 1;
                }
                else {
                    page = totalResults / 30;
                }
                
                var url = 'search' + location.search + '&Page=' + page;
                
                rentler.api.stats.trackPageView(url);
            }            
        });
    };

    $self.scroll = function() {
        var win = $(window),
            advSearch = $("#advanced-search"),
            search = $("#search"),
            options = $("#search-result-options"),
            header = $("#header");

        win.bind('scroll', function() {
            if($self.vars.hasPopupOpen)
                return;

            if(win.scrollTop() > 49) {
                advSearch.addClass("moved");
                search.addClass("moved");
                options.addClass("moved");
                header.css('top', '-63px');
            } else {
                advSearch.removeClass("moved");
                search.removeClass("moved");
                options.removeClass("moved");
                header.css('top', '0px');
            }
        });
    };

    $self.placeholders = function() {
        Placeholder.init({
            normal: "#707070",
            placeholder: "#bbbbbb"
        });
    };

    $self.ajaxSearch = function(params, callback) {
        var options = {
            Location: '',
            MinPrice: null,
            MaxPrice: null,            
            PropertyTypeCode: "0",
            Bedrooms: null,
            Bathrooms: null,
            MinSquareFootage: null,
            MaxSquareFootage: null,
            YearBuiltMin: null,
            YearBuiltMax: null,
            SellerTypeCode: "0",
            Keywords: null,
            Amenities: null,
            Terms: null,
            LeaseLengthCode: "0",
            PhotosOnly: false,
            OrderBy: "Map",
            Page: 1,
            ResultsPerPage: 27
        };

        var opts = {};

        if (params.ResultsPerPage) {
            options.Location = params.Location;
            options.MinPrice = params.MinPrice;
            options.MaxPrice = params.MaxPrice;
            options.PropertyTypeCode = params.PropertyTypeCode;
            options.Bedrooms = params.Bedrooms;
            options.Bathrooms = params.Bathrooms;
            options.MinSquareFootage = params.MinSquareFootage;
            options.MaxSquareFootage = params.MaxSquareFootage;
            options.YearBuiltMin = params.YearBuiltMin;
            options.YearBuiltMax = params.YearBuiltMax;
            options.SellerTypeCode = params.SellerTypeCode;
            options.Keywords = params.Keywords;
            options.Amenities = params.Amenities;
            options.Terms = params.Terms;
            options.LeaseLengthCode = params.LeaseLengthCode;
            options.PhotosOnly = params.PhotosOnly;
            options.OrderBy = params.OrderBy;
            options.Page = params.Page;
            options.ResultsPerPage = params.ResultsPerPage;

            opts = options;
        }
        else {
            $.extend(opts, params, options);
        }
                
        var ajaxSettings = {
            url: hostname + $self.vars.ajaxSearchUrl,
            data: opts,
            type: 'get',
            dataType: 'json',
            success: callback
        };

        $.ajax(ajaxSettings);
    };

    $self.init = function () {
    	$self.advancedDropDown();
		$self.infiniteScroll();
        // set placeholders if ie
        if($.browser.msie)
            $self.placeholders();
        $self.displayListing();
        $self.scroll();
    };

    // initialize nicely
    $(document).ready(function () { $self.init(); });

} (rentler.search));