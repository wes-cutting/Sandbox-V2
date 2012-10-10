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
        hasPopupOpen: false
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

            $self.vars.hasPopupOpen = true;

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
                rentler.listing.carousel();
                rentler.listing.imInterested();
                rentler.listing.reporting();
            });
        });

        listingScrim.live('click', function(e) {
            scrimBackground.addClass('hide');
            listingScrim.addClass('hide');

            searchResults.css('margin-top', '0px');
            searchResults.removeClass('active').height('auto');
            footer.show();
            win.scrollTop(activePosition);
            $self.vars.hasPopupOpen = false;
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
        $('.search-results').infinitescroll({
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