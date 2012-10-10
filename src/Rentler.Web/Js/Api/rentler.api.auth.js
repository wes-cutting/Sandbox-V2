/// <reference path="/js/rentler.js" />

/*
Base namespace configuration.
*/
rentler = window.rentler || {};
rentler.__namespace = true;

rentler.api = rentler.api || {};
rentler.api.__namespace = true;

rentler.api.auth = rentler.api.auth || {};
rentler.api.auth.__namespace = true;

(function ($self, undefined) {

	$self.isAuthenticated = function (onSuccess) {
		/// <summary>Checks to see if the current user is authenticated.</summary>
		/// <param name="onSuccess" type="Function">The method to call when the api is finished executing.</param>

		amplify.request(
			"rentler.api.auth.isAuthenticated",
			onSuccess
		);
	};

	function configure() {
		/// <summary>Configures all of the amplify requests.</summary>

		amplify.request.define('rentler.api.auth.isAuthenticated', 'ajax',{
			url: '/api/auth/isauthenticated',
			type: 'POST'
		});
	};

	configure();

} (rentler.api.auth));