/// <reference path="/js/rentler.js" />

/*
    Base namespace configuration.
*/
rentler = window.rentler || {};
rentler.__namespace = true;

rentler.property = rentler.property || {};
rentler.property.__namespace = true;

rentler.property.list = rentler.property.list || {};
rentler.property.list.__namespace = true;

(function ($self, undefined) {
    $self.CreateViewModel = function() {
        /// <summary>View model for the entire page.</summary>
        var self = this;

        self.selectBox = $('select[name="Input.ContactInfo.ContactInfoId"]');
        self.professionalSelectBox = $('[name="Input.ContactInfo.ContactInfoTypeCode"]');
        self.customAmenities = ko.observableArray([]);
        self.newCustomAmenity = ko.observable("");                

        self.contactInfos = new Array();
        
        self.selectedContact = ko.observable();
        self.selectedContact.subscribe(
            function(newValue) { 
                var contactInfoTypeCode = $('[name="Input.ContactInfo.ContactInfoTypeCode"]'),
                    companyName = $('[name="Input.ContactInfo.CompanyName"]'),
                    name = $('[name="Input.ContactInfo.Name"]'),
                    showPhoneNumber = $('[name="Input.ContactInfo.ShowPhoneNumber"]'),
                    email = $('[name="Input.ContactInfo.Email"]'),
                    phoneNumber = $('[name="Input.ContactInfo.PhoneNumber"]'); 
                
                if(newValue) {
                    contactInfoTypeCode.val(self.selectedContact().contactInfoTypeCode);

                    if(self.selectedContact().contactInfoTypeCode === 1)
                        companyName.parent().addClass("hide");
                    else
                        companyName.parent().removeClass("hide");

                    companyName.val(self.selectedContact().companyName);
                    name.val(self.selectedContact().name);
                    email.val(self.selectedContact().email);
                    phoneNumber.val(self.selectedContact().phoneNumber);

                    if(self.selectedContact().showPhoneNumber)
                        showPhoneNumber.attr("checked", "checked");
                    else
                        showPhoneNumber.removeAttr("checked");
                }
                else {
                    contactInfoTypeCode.val(1);
                    companyName.val("");
                    name.val("");
                    showPhoneNumber.removeAttr("checked");
                    email.val("");
                    phoneNumber.val("");
                    companyName.parent().addClass("hide");
                }
            }
        );              
                
        self.findContactById = function(id) {
            for(var i = 0; i < self.contactInfos.length; ++i)
                if(self.contactInfos[i].contactInfoId == id)
                    return self.contactInfos[i];

            return null;
        };

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

		$('#step1Form').on('submit', function(e){
            if($('form').valid()) {
			    $('#listsubmit').addClass('hide');
			    $('#listsubmitloader').removeClass('hide');
            }
		});
    };
    $self.CreateViewModel.__class = true;

} (rentler.property.list));