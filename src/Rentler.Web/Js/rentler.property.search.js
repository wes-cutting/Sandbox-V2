/// <reference path="/js/rentler.js" />
/// <reference path="/scripts/jquery.infinitescroll.js" />

/*
Base namespace configuration.
*/
rentler = window.rentler || {};
rentler.__namespace = true;

rentler.property = rentler.property || {};
rentler.property.__namespace = true;

rentler.property.search = rentler.property.search || {};
rentler.property.search.__namespace = true;

(function ($self, undefined) {

    $self.infiniteScroll = function () {
        $('.my-properties').infinitescroll({
            navSelector: "div.navigation", 			// selector for the paged navigation (it will be hidden)
            nextSelector: "div.navigation a:last", 	// selector for the NEXT link (to page 2)
            itemSelector: ".prop-result", 			// selector for all items you'll retrieve
            loading: {
                img: "/images/loader.gif",
                msgText: "",
                finishedMsg: "Done",
                selector: ".loading"
            },
            animate: false,
            bufferPx: 600
        });
    };

    $self.autoSearch = function() {
        var form = $('.property-search form'),
            container = $('.search-results-container'),
            loader = $('.live-loader');

        container.empty();
        loader.removeClass('hide');

        $.ajax({  
            type: "GET",  
            url: form.attr("action"),
            data: form.serialize(),  
            success: function(data) {
                loader.addClass('hide'); 
                container.html(data);
                $self.infiniteScroll();
            }  
        });
    };

    $self.bindSearch = function() {
        var dropdown = $('select[name="OrderBy"]'),
            form = $('.property-search form'),
            textbox = $('input[name="Keywords"]');

        dropdown.on("change", null, null, $self.autoSearch);
        form.on("submit", null, null, function(e) { 
            e.preventDefault();
            $self.autoSearch();
        });
        textbox.on("input", null, null, $self.autoSearch);
    };

    $self.init = function () {
        $self.infiniteScroll();
        $self.bindSearch();
    };

    // initialize nicely
    $(document).ready(function () { $self.init(); });

} (rentler.property.search));