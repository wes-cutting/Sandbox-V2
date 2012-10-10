/// <reference path="/js/rentler.js" />

/*
Base namespace configuration.
*/
rentler = window.rentler || {};
rentler.__namespace = true;

rentler.property = rentler.property || {};
rentler.property.__namespace = true;

rentler.property.advertise = rentler.property.advertise || {};
rentler.property.advertise.__namespace = true;

(function ($self, undefined) {

    $self.CreateViewModel = function (id) {
        /// <summary>View model for the entire page.</summary>
        var self = this;

        rentler.scrim.loadScrim();

        self.buildingId = id;
        self.hasRibbon = false;
        self.isVisibleIsDirty = false;

        self.isVisible = ko.observable();
        self.leads = ko.observableArray();

        self.setIsVisible = function (visibility) {
            self.isVisibleIsDirty = false;
            self.isVisible(visibility);
        };

        self.loadRibbon = function (ribbonId) {
            if (ribbonId) {
                if (ribbonId == "") {
                    self.hasRibbon = false;
                }
                else {
                    self.hasRibbon = true;
                }
            }
            else {
                self.hasRibbon = false;
            }
        };

        self.isVisible.subscribe(function (newValue) {
            if (self.isVisibleIsDirty) {
                var action = (self.isVisible() == "true") ? "activate" : "deactivate";

                if (action == "deactivate") {
                    if (self.hasRibbon)
                        self.deactivateWithPrompt();
                    else
                        self.deactivateBuilding();
                }
                else {
                    self.activateBuilding();
                }
            }
            else {
                self.isVisibleIsDirty = true;
            }
        });
        
        self.activateBuilding = function () {
            $.ajax(
                "/dashboard/property/activate",
                {
                    data: { id: self.buildingId },
                    type: 'POST',
                    traditional: true,
                    success: function (status) {
                        if (status.StatusCode != 200) {
                            alert(status.Message);

                            self.setIsVisible("false");
                        }
                    },
                    error: function () {
                        alert(
                            "An unexpected error occurred: unable to communicate with the server. " +
                            "Please contact Rentler Support if this problem persists"
                        );

                        self.setIsVisible("false");
                    }
                }
            );
        };

        self.deactivateBuilding = function () {
            $.ajax(
                "/dashboard/property/deactivate",
                {
                    data: { id: self.buildingId },
                    type: 'POST',
                    traditional: true,
                    success: function (status) {
                        if (status.StatusCode != 200) {
                            alert(status.Message);

                            // restore visibility back to active since we were unable to deactivate                            
                            self.setIsVisible("true");
                        }
                    },
                    error: function () {
                        alert(
                            "An unexpected error occurred: unable to communicate with the server. " +
                            "Please contact Rentler Support if this problem persists"
                        );

                        self.setIsVisible("true");
                    }
                }
            );
        };

        self.deactivateWithPrompt = function () {
            rentler.scrim.openScrimBlank();
            $('.scrimInnerContent').html($('#deactivateWithRibbonTemplate').html());

            // register handler for close
            $('a.closeScrim').on("click", self.cancelDeactivate);
        };

        self.cancelDeactivate = function () {
            self.setIsVisible("true");
            $('a.closeScrim').off("click", self.cancelDeactivate);
        };

        self.showActionError = function (error, callback) {
            alert(error);
        };
        
        if (window.location.hash == "#deactivate") {
            $('#deactivateError').removeClass('hide');
            self.deactivateWithPrompt();
        }
    };
    $self.CreateViewModel.__class = true;

} (rentler.property.advertise));