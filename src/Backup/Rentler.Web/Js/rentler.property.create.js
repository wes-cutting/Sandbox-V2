/// <reference path="/js/rentler.js" />

/*
    Base namespace configuration.
*/
rentler = window.rentler || {};
rentler.__namespace = true;

rentler.property = rentler.property || {};
rentler.property.__namespace = true;

rentler.property.create = rentler.property.create || {};
rentler.property.create.__namespace = true;

(function ($self, undefined) {

    $self.CreateViewModel = function() {
        /// <summary>View model for the entire page.</summary>
        var self = this;

        self.selectBox = $('select[name="Input.ContactInfoId"]');
        self.professionalSelectBox = $('[name="Input.ContactInfo.ContactInfoTypeCode"]');
        self.customAmenities = ko.observableArray([]);
        self.newCustomAmenity = ko.observable("");

        self.contactInfos = new Array();

        self.addCustomAmenityCallback = function() {
            /// <summary>Called when a custom amenity is added..</summary>

            var finalName = self.newCustomAmenity();
            // validate
            if(finalName === "")
                return;
            // add to the array
            self.customAmenities.push({
                submitName: 'Input.CustomAmenities[' + finalName + '].Name',
                name: finalName
            });
            self.newCustomAmenity("");
        };

        self.professionalSelectBox.change(function(e) {
            /// <summary>Called whenever the personal/professional dropdown is changed.</summary>

            var value = $(this).val(),
                company = $('[name="Input.ContactInfo.CompanyName"]').parent();

            if(value === "1")
                company.addClass("hide");
            else
                company.removeClass("hide");
        });

        self.selectBox.change(function(e) {
            /// <summary>Called whenever the persontype dropdown is changed.</summary>

            var value = $(this).val(),
                contactInfoTypeCode = $('[name="Input.ContactInfo.ContactInfoTypeCode"]'),
                companyName = $('[name="Input.ContactInfo.CompanyName"]'),
                name = $('[name="Input.ContactInfo.Name"]'),
                showPhoneNumber = $('[name="Input.ContactInfo.ShowPhoneNumber"]'),
                email = $('[name="Input.ContactInfo.Email"]'),
                phoneNumber = $('[name="Input.ContactInfo.PhoneNumber"]');

            if(value == 0) {
                // set them to nothing
                contactInfoTypeCode.val(1);
                companyName.val("");
                name.val("");
                showPhoneNumber.removeAttr("checked");
                email.val("");
                phoneNumber.val("");
                companyName.parent().addClass("hide");
            } else {
                // set them to the correct object
                for(var x = 0; x < self.contactInfos.length; ++x) {
                    if(value == self.contactInfos[x].contactInfoId) {
                        var selected = self.contactInfos[x];
                        contactInfoTypeCode.val(selected.contactInfoTypeCode);
                        companyName.val(selected.companyName);
                        name.val(selected.name);
                        email.val(selected.email);
                        phoneNumber.val(selected.phoneNumber);
                        if(selected.contactInfoTypeCode === 1)
                            companyName.parent().addClass("hide");
                        else
                            companyName.parent().removeClass("hide");
                        if(selected.showPhoneNumber)
                            showPhoneNumber.attr("checked", "checked");
                        else
                            showPhoneNumber.removeAttr("checked");
                    }
                }
            }
        });
    };
    $self.CreateViewModel.__class = true;

} (rentler.property.create));