/// <reference path="/js/rentler.js" />

/*
Base namespace configuration.
*/
rentler = window.rentler || {};
rentler.__namespace = true;

rentler.checkout = rentler.checkout || {};
rentler.checkout.__namespace = true;

(function ($self, undefined) {
    $self.CreateViewModel = function () {
        /// <summary>View model for the entire page.</summary>
        var self = this;  
        
        self.months = ko.observableArray([new $self.selectListItem("", "0")]);
        self.years = ko.observableArray([new $self.selectListItem("", "0")]);
        self.states = ko.observableArray();
        self.paymentMethods = ko.observableArray();          
        self.selectedPaymentMethod = ko.observable();
        self.isSaved = ko.observable(); 
        
        $('.pmradio').live('click', function(e) {                        
            for(var i = 0; i < self.paymentMethods().length; ++i) {                
                if(self.paymentMethods()[i].accountReference === $(this).val()) {
                    self.selectedPaymentMethod(self.paymentMethods()[i]);
                    self.isSaved(self.paymentMethods()[i].userCreditCardId > 0);
                    break;
                }                    
            }            
        });

        self.findPaymentMethodById = function(id) {
            if(self.paymentMethods()) {
                for(var i = 0; i < self.paymentMethods().length; ++i) {  
                    if(self.paymentMethods()[i].userCreditCardId == id)
                        return self.paymentMethods()[i];
                }
            }

            return undefined;
        };

    };
    $self.CreateViewModel.__class = true;

    $self.selectListItem = function(text, value) {
        this.text = text;
        this.value = value;
    };
    $self.selectListItem.__class = true;

    $self.paymentMethod = function(
        id, alias, ref, nameOnCard, cardNumber, expirationMonth, expirationYear, 
        firstName, lastName, address1, address2, city, state, zip, 
        createDate, createdBy) {
        // <summary>This is the paymentMethod class</summary>
        
        this.userCreditCardId = id;
        this.alias = alias;
        this.accountReference = ref;
        this.cardName = nameOnCard;
        this.cardNumber = cardNumber;
        this.expirationMonth = expirationMonth;
        this.expirationYear = expirationYear;
        this.firstName = firstName;
        this.lastName = lastName;
        this.address1 = address1;
        this.address2 = address2;
        this.city = city;
        this.state = state;
        this.zip = zip;
        this.createDate = createDate;
        this.createdBy = createdBy;        

        this.expiration = function() {
            return this.expirationMonth + "/" + this.expirationYear;
        };        
    };
    $self.paymentMethod.__class = true;
    
} (rentler.checkout));