/// <reference path="/js/rentler.js" />

/*
    Base namespace configuration.
*/
rentler = window.rentler || {};
rentler.__namespace = true;

rentler.scrim = rentler.scrim || {};
rentler.scrim.__namespace = true;

(function ($self, undefined) {

    $self.scrimLoaded = false;

    $self.loadScrim = function() {
        var self = this;

        self.overlay = '<div class="scrim background hide">&nbsp;</div>';
        self.content = '<div class="scrimContent hide"><div class="scrimInnerContent"><div class="scrimLoader"><img src="https://www.rentler.com/images/ajax-loader.gif" alt="loading" />&nbsp;&nbsp;Loading</div></div></div>';
        self.closeButton = '<div class="scrimClose hide"><a class="button closeScrim scrimClose" href="#">Close</a></div>';
        self.scrimHtml = self.overlay + self.content + self.closeButton;
        self.body = $('body');
            
        self.body.prepend(self.scrimHtml);
            
        self.closeLink = $(".scrimClose .closeScrim");
            
        self.closeLink.live("click", function(e) {
            e.preventDefault();
            $self.closeScrim();
        });

        $self.scrimLoaded = true;
    };

    $self.openScrim = function(url) {
        if(!($self.scrimLoaded)) {
            alert("Scrim is not loaded");
            return;
        }

        var self = this;

        self.scrimContent = $(".scrimContent");
        self.scrimOverlay = $(".scrim.background");
        self.scrimCloser = $(".scrimClose");
            
        self.scrimContent.removeClass("hide");
        self.scrimOverlay.removeClass("hide");
        self.scrimCloser.removeClass("hide");

        if(url === undefined)
            return;

        $.get(url, function(data) {
            self.scrimInnerContent = $(".scrimInnerContent");
            self.scrimInnerContent.html(data);
        });
    };

    $self.closeScrim = function() {
        var self = this;

        if(!($self.scrimLoaded)) {
            alert("Scrim is not loaded");
            return;
        }

        self.scrimContent = $(".scrimContent"),
        self.scrimOverlay = $(".scrim.background"),
        self.scrimCloser = $(".scrimClose"),
        self.scrimInnerContent = $(".scrimInnerContent");

        self.scrimContent.addClass("hide");
        self.scrimOverlay.addClass("hide");
        self.scrimCloser.addClass("hide");

        self.scrimInnerContent.html('<div class="scrimLoader"><img src="/images/ajax-loader.gif" alt="loading" />&nbsp;&nbsp;Loading</div>');
    };

    $self.openScrimBlank = function(content) {
        var self = this;

		if(!($self.scrimLoaded)) {
            alert("Scrim is not loaded");
            return;
        }

        self.scrimContent = $(".scrimContent"),
        self.scrimOverlay = $(".scrim.background"),
        self.scrimCloser = $(".scrimClose");
            
        self.scrimContent.removeClass("hide");
        self.scrimOverlay.removeClass("hide");
        self.scrimCloser.removeClass("hide");
	};

} (rentler.scrim));