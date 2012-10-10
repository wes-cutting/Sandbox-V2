/// <reference path="/js/rentler.js" />

/*
Base namespace configuration.
*/
rentler = window.rentler || {};
rentler.__namespace = true;

rentler.api = rentler.api || {};
rentler.api.__namespace = true;

rentler.api.stats = rentler.api.stats || {};
rentler.api.stats.__namespace = true;

(function ($self, undefined) {

    $self.trackPageView = function (url) {
        _gaq.push(['_trackPageview', url]);
    };

    $self.trackEvent = function (category, action, label) {
        _gaq.push(['_trackEvent', category, action, label]);
    };

}(rentler.api.stats));