/// <reference path="/js/rentler.js" />

/*
Base namespace configuration.
*/
rentler = window.rentler || {};
rentler.__namespace = true;

rentler.property = rentler.property || {};
rentler.property.__namespace = true;

rentler.property.promote = rentler.property.promote || {};
rentler.property.promote.__namespace = true;

(function ($self, undefined) {

    $self.CreateViewModel = function () {
        /// <summary>View model for the entire page.</summary>
        var self = this;        

        self.title = ko.observable("");
        self.description = ko.observable("");
        
        self.limitTitle = function(charLimit) {
            if(self.title().length >= charLimit)
                self.title(self.title().substring(0, charLimit - 1));
        };
        self.remainingChar = function(charLimit, value) { 
            var rem = charLimit - value.length;
            return (rem == 1) ? "1 Character" : rem + " Characters";
        },
        self.limitDescription = function(charLimit) {
            if(self.description().length >= charLimit)
                self.description(self.description().substring(0, charLimit - 1));
        };

        // ribbons
        self.ribbons = ko.observableArray();        
        self.selectedRibbon = ko.observable();
        
        self.ribbonPrice = ko.computed(function() {
            return (self.selectedRibbon() && self.selectedRibbon().id == "none") ? 0 : 0.99;
        });
        
        self.ribbonPriceFormatted = ko.computed(function() {            
            return '$' + self.ribbonPrice().toFixed(2).toString();
        });

        $('input[name="Input.SelectedRibbonId"]').live("click", function() {
            self.selectedRibbon({ id: $(this).val(), name: $(this).attr("data-name") });
        });                                       

        // featured
        self.featured = ko.observableArray([]);
        
        self.featuredPrice = ko.computed(function() {
            return (self.featured()) ? self.featured().length * 9.95 : 0;
        });

        self.featuredPriceFormatted = ko.computed(function() {
            return '$' + self.featuredPrice().toFixed(2).toString();
        });

        self.featuredName = ko.computed(function() {
            var featuredCount = new Number();
            var featuredDisplay = "";

            if(self.featured()) {
                featuredCount = self.featured().length;
            }

            if(featuredCount == 1)
                return "1 Day";
            else
                return featuredCount + " Days";
        });
        
        self.cartTotalFormatted = ko.computed(function() {            
            var total = self.ribbonPrice() + self.featuredPrice();
            return '$' + total.toFixed(2).toString();
        });

        self.submitText = function() {
            // no ribbon or featured have been defined
            if(self.selectedRibbon() == undefined && self.featured() == undefined)
                return "View Listing";

            var noRibbon = (self.selectedRibbon() && self.selectedRibbon().id == "none");
            var noFeatured = (self.featured() && self.featured().length == 0);  

            if(noRibbon && noFeatured)
                return "View Listing";

            return "Checkout";          
        };        

    };
    $self.CreateViewModel.__class = true;

} (rentler.property.promote));