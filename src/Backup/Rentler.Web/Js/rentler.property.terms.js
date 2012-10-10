/// <reference path="/js/rentler.js" />

/*
Base namespace configuration.
*/
rentler = window.rentler || {};
rentler.__namespace = true;

rentler.property = rentler.property || {};
rentler.property.__namespace = true;

rentler.property.terms = rentler.property.terms || {};
rentler.property.terms.__namespace = true;

(function ($self, undefined) {

    $self.CreateViewModel = function () {
        /// <summary>View model for the entire page.</summary>
        var self = this;
                
        // set focus on first input field
        $(':input:enabled:visible:first').focus();

        $(".datepicker").datepicker();

        self.petsAllowed = ko.observable("true"); 
        self.arePetsAllowed = function() {
            return (self.petsAllowed() == "true") ? true : false;
        };                       
    };
    $self.CreateViewModel.__class = true;

} (rentler.property.terms));