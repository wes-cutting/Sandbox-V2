/// <reference path="/js/rentler.js" />

/*
Base namespace configuration.
*/
rentler = window.rentler || {};
rentler.__namespace = true;

rentler.property = rentler.property || {};
rentler.property.__namespace = true;

rentler.property.requestapp = rentler.property.requestapp || {};
rentler.property.requestapp.__namespace = true;

(function ($self) {

    $self.init = function () {
        $("#Input_Message").live('keyup', function(e) {
            var item = $(this),
                value = item.val();
            if(value > 1000)
                item.val(value.substring(0, 1000));
            $(".characters").text(1000 - value.length);
        });
    };

	// initialize nicely
	$(document).ready(function () { $self.init(); });

})(rentler.property.requestapp);