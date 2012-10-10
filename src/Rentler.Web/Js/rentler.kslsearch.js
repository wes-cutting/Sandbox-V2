/// <reference path="/js/rentler.js" />

/*
Base namespace configuration.
*/
rentler = window.rentler || {};
rentler.__namespace = true;

rentler.kslsearch = rentler.kslsearch || {};
rentler.kslsearch.__namespace = true;

(function ($self, undefined) {

    $self.vars = {
        ajaxSearchUrl: "/search/ajaxsearch"
    };

    
    $.fn.serializeObject = function () {
        var o = {};
        var a = this.serializeArray();
        $.each(a, function () {
            if (o[this.name] !== undefined) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    };

    $self.ViewModel = function () {
        /// <summary>View model for the entire page.</summary>
        var self = this;

        self.isAdvanced = ko.observable(false);
        self.advancedTriggerText = ko.computed(function() {
            return (self.isAdvanced()) ? "less options" : "more options";
        });

        self.setupAdvanced = function(isAdvanced) {
            self.isAdvanced(isAdvanced);
            if(!isAdvanced) {
                self.disableAdvanced();
            }
        };

        self.toggleAdvanced = function (data, event) {            
            if(self.isAdvanced()) {
                // currently advanced, restore to basic
                self.isAdvanced(false);
                self.disableAdvanced();
                $(event.target).html("more options");
            }
            else {
                // currently basic, set to advanced
                self.isAdvanced(true);
                self.enableAdvanced();
                $(event.target).html("less options");
            }                      
        };

        self.disableAdvanced = function() {
            // reset advanced controls to defaults            
            // beds select
            // baths select
            // min square feet select
            // max square feet select
            // min year built select
            // max year built select
            // terms select
            $('.search-form-advanced select,#Keywords')
                .val('')
                .attr('disabled', 'disabled');                
            
            // amenities checkboxes
            // terms checkboxes            
            // photosOnly checkbox
            $('.search-form-advanced input:checkbox')
                .removeAttr('checked')
                .attr('disabled', 'disabled');
        };

        self.enableAdvanced = function() {
            // reset advanced controls to defaults            
            // beds select
            // baths select
            // min square feet select
            // max square feet select
            // min year built select
            // max year built select
            // terms select
            $('.search-form-advanced select,#Keywords')                
                .removeAttr('disabled');                
            
            // amenities checkboxes
            // terms checkboxes            
            // photosOnly checkbox
            $('.search-form-advanced input:checkbox')                
                .removeAttr('disabled');
        };

        var map = null;

        self.viewOptions = ['Grid','Map'];

        self.viewMode = ko.observable();        

        self.viewMode.subscribe(function(newValue) {
            // when view mode changes
            if(self.viewMode() == 'Map') {
                // go get map of current results
                
                //initialize the map
                if(!map)
                    map = new rentler.google.maps.Map('map');

                //get search results            
                var options = $('form').serializeObject();
            
                // add page
                options['Page'] = $('#Page').val();

                rentler.kslsearch.ajaxSearch(options, function (data) {
                    //get geocode based on the address and make a marker
                    for (var i in data.Results) {                    
                        var marker = map.addMarker(
						    data.Results[i].Latitude,
                            data.Results[i].Longitude, 
                            data.Results[i].FullAddress,
						    data.Results[i].BuildingId
                        );
                    
                        //bind infoWindows to each marker
					    var content = '<div class="mapTip">' + $('.hiddenInfo[data-id=' + data.Results[i].BuildingId + ']').html() + '</div>';                    
					    $(content).removeClass('hide');
                    
					    map.bindInfoWindow(content, marker);
                    }                    

                    map.frameResults();                    

                    //trigger complete event
                    //$('html').trigger('loadMapComplete');
                });
            }
        });
        self.showMap = ko.computed(function() {
            return self.viewMode() == 'Map';
        });
        self.showGrid = ko.computed(function() {
            return self.viewMode() == 'Grid';
        });
    };
    $self.ViewModel.__class = true;

    $self.ajaxSearch = function(params, callback) {
        var options = {
            Location: '',
            MinPrice: null,
            MaxPrice: null,            
            PropertyTypeCode: "0",
            Bedrooms: null,
            Bathrooms: null,
            MinSquareFootage: null,
            MaxSquareFootage: null,
            YearBuiltMin: null,
            YearBuiltMax: null,
            SellerTypeCode: "0",
            Keywords: null,
            Amenities: null,
            Terms: null,
            LeaseLengthCode: "0",
            PhotosOnly: false,
            OrderBy: "Map",
            Page: 1,
            ResultsPerPage: 27
        };

        var opts = {};

        if (params.ResultsPerPage) {
            options.Location = params.Location;
            options.MinPrice = params.MinPrice;
            options.MaxPrice = params.MaxPrice;
            options.PropertyTypeCode = params.PropertyTypeCode;
            options.Bedrooms = params.Bedrooms;
            options.Bathrooms = params.Bathrooms;
            options.MinSquareFootage = params.MinSquareFootage;
            options.MaxSquareFootage = params.MaxSquareFootage;
            options.YearBuiltMin = params.YearBuiltMin;
            options.YearBuiltMax = params.YearBuiltMax;
            options.SellerTypeCode = params.SellerTypeCode;
            options.Keywords = params.Keywords;
            options.Amenities = params.Amenities;
            options.Terms = params.Terms;
            options.LeaseLengthCode = params.LeaseLengthCode;
            options.PhotosOnly = params.PhotosOnly;
            options.OrderBy = params.OrderBy;
            options.Page = params.Page;
            options.ResultsPerPage = params.ResultsPerPage;

            opts = options;
        }
        else {
            $.extend(opts, params, options);
        }
                
        var ajaxSettings = {
            url: hostname + $self.vars.ajaxSearchUrl,
            data: opts,
            type: 'get',
            dataType: 'json',
            success: callback
        };

        $.ajax(ajaxSettings);
    };

    $self.fireEvent = function(element,event) {
		if(element.click){
			element.click();
			return true;
		}
		if (document.createEvent) {
			// dispatch for firefox + others
			var evt = document.createEvent("HTMLEvents");
			evt.initEvent(event, true, true ); // event type,bubbling,cancelable
			return !element.dispatchEvent(evt);
		} else {
			// dispatch for IE
			var evt = document.createEventObject();
			return element.fire('on'+event,evt)
		}
    };

    $self.init = function () {
        // initialize here

        // submit form as-is when order by changed
        $('#OrderBy').on("change", null, null, function() {            
            $('form button[type="submit"]').trigger("click");
        });

        $('.kslsearch form').live("submit", function(e) {
            e.preventDefault();
            var loc = kslurl + "?" + $(this).serialize();
            $("#search").append('<a href="' + loc + '" target="_top" style="display: none;" id="gogogo">Search</a>');
			var elem = document.getElementById("gogogo");
            $self.fireEvent(elem, 'click');
        });
    };

    // initialize nicely
    $(document).ready(function () { $self.init(); });

} (rentler.kslsearch));