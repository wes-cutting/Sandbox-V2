﻿/// <reference path="/js/rentler.js" />
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
            itemSelector: ".prop-result", 			    // selector for all items you'll retrieve
            loading: {
                img: "/images/loader.gif",
                msgText: "",
                finishedMsg: "",
                selector: ".loading"
            },
            animate: false,
            bufferPx: 600
        });
    };

    $self.init = function () {
        $self.infiniteScroll();
    };

    // initialize nicely
    $(document).ready(function () { $self.init(); });

} (rentler.property.search));