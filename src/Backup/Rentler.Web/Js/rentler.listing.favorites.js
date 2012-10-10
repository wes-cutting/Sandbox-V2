rentler = window.rentler || {};
rentler.__namespace = true;

rentler.listing = rentler.listing || {};
rentler.listing.__namespace = true;

rentler.listing.favorites = rentler.listing.favorites || {};
rentler.listing.favorites.__namespace = true;

(function ($self, undefined) {

    $self.infiniteScroll = function () {
        $('.search-results').infinitescroll({
            navSelector: "div.navigation", 			// selector for the paged navigation (it will be hidden)
            nextSelector: "div.navigation a:last", 	// selector for the NEXT link (to page 2)
            itemSelector: ".result", 			// selector for all items you'll retrieve
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

} (rentler.listing.favorites));