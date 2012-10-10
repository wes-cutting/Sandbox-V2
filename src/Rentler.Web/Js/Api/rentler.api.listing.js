/// <reference path="/js/rentler.js" />

/*
Base namespace configuration.
*/
rentler = window.rentler || {};
rentler.__namespace = true;

rentler.api = rentler.api || {};
rentler.api.__namespace = true;

rentler.api.listing = rentler.api.listing || {};
rentler.api.listing.__namespace = true;

(function ($self, undefined) {

    $self.addView = function (id, onSuccess) {
        /// <summary>Increments the listing views for a given listing.</summary>
        /// <param name="id" type="Number">The id of the listing to add the view to.</param>
        /// <param name="onSuccess" type="Function">The method to call when the api is finished executing.</param>

        amplify.request(
			"rentler.api.listing.addView",
            {
                id: id
            },
			onSuccess
		);
    };

    $self.getTotalViews = function (onSuccess) {
        /// <summary>Gets all of the listing views for the site.</summary>
        /// <param name="onSuccess" type="Function">The method to call when the api is finished executing.</param>

        amplify.request(
			"rentler.api.listing.getTotalViews", {},
			onSuccess
		);
    };

    $self.getTotalSearchViews = function (onSuccess) {
        /// <summary>Gets all of the search views for the site.</summary>
        /// <param name="onSuccess" type="Function">The method to call when the api is finished executing.</param>

        amplify.request(
			"rentler.api.listing.getTotalSearchViews", {},
			onSuccess
		);
    };

	$self.favorite = function(id, onSuccess){
		/// <summary>Saves a listing to a user's saved properties.</summary>
		/// <param name="id">The id of the property to save.</summary>
		/// <param name="onSuccess" type="Function">The method to call when the api is finished executing.</param>

		amplify.request(
			'rentler.api.listing.favorite', 
			{ id: id }, 
			onSuccess
		);
	};

	$self.unfavorite = function(id, onSuccess){
		/// <summary>Removes a saved property from a user's saved properties.</summary>
		/// <param name="id">The id of the property to remove.</summary>
		/// <param name="onSuccess" type="Function">The method to call when the api is finished executing.</param>

		amplify.request(
			'rentler.api.listing.unfavorite', 
			{ id: id }, 
			onSuccess
		);
	};

    $self.report = function(id, message, onSuccess) {
        /// <summary>Submits a report listing request with the attached message</summary>
		/// <param name="id">The id of the listing to submit.</summary>
		/// <param name="onSuccess" type="Function">The method to call when the api is finished executing.</param>

        amplify.request(
            'rentler.api.listing.report',
            { 
                id: id,
                message: message 
            },
            onSuccess
        );
    };

	$self.interested = function(data, onSuccess){
		/// <summary>Submits an email to the landlord of a property.</summary>
		/// <param name="data">An object with BuildingId and Content parameters.</summary>
		/// <param name="onSuccess" type="Function">The method to call when the api is finished executing.</param>		

		amplify.request(
			'rentler.api.listing.interested', 
			data, 
			onSuccess);
	};

    function configure() {
        /// <summary>Configures all of the amplify requests.</summary>

        amplify.request.define("rentler.api.listing.addView", "ajax", {
            url: "/api/listing/addview/{id}",
            type: "POST"
        });

        amplify.request.define("rentler.api.listing.report", "ajax", {
            url: "/api/listing/report/{id}",
            type: "POST"
        });

        amplify.request.define("rentler.api.listing.getTotalViews", "ajax", {
            url: "/api/listing/totalviews",
            type: "POST"
        });

        amplify.request.define("rentler.api.listing.getTotalSearchViews", "ajax", {
            url: "/api/listing/totalsearchviews",
            type: "POST"
        });

		amplify.request.define('rentler.api.listing.favorite', 'ajax', {
			url: '/api/listing/favorite/{id}',
			type: 'POST'
		});

		amplify.request.define('rentler.api.listing.unfavorite', 'ajax',{
			url: '/api/listing/unfavorite/{id}',
			type: 'POST'
		});

		amplify.request.define('rentler.api.listing.interested', 'ajax', {
			url: '/api/listing/interested',
			type: 'POST'
		});

		amplify.request.define
    };

    configure();

} (rentler.api.listing));