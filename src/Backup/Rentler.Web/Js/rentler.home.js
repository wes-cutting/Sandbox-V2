/// <reference path="/js/rentler.js" />

/*
Base namespace configuration.
*/
rentler = window.rentler || {};
rentler.__namespace = true;

rentler.home = rentler.home || {};
rentler.home.__namespace = true;

(function ($self, undefined) {

    $self.video = function() {
        var self = this;

        self.button = $('.featureSet.talking a');
		self.vidId = self.button.attr('data-video');
		self.videoLink = '<iframe src="https://player.vimeo.com/video/' + self.vidId + '?title=0&amp;byline=0&amp;portrait=0&autoplay=true" width="640" height="360" frameborder="0" webkitAllowFullScreen mozallowfullscreen allowFullScreen></iframe>';

		rentler.scrim.loadScrim();

		self.button.live('click', function(e) {
			e.preventDefault();
			rentler.scrim.openScrimBlank(self.videoLink);
			$('.scrimInnerContent').html(self.videoLink);
		});
    };

    $self.placeholders = function() {
        Placeholder.init({
            normal: "#707070",
            placeholder: "#bbbbbb"
        });
    };
    
    $self.init = function () { 
        $self.video();

        // set placeholders if ie
        if($.browser.msie)
            $self.placeholders();
    };

    // initialize nicely
    $(document).ready(function () { $self.init(); });

} (rentler.home));