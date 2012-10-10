/// <reference path="/js/rentler.js" />

/*
Base namespace configuration.
*/
rentler = window.rentler || {};
rentler.__namespace = true;

rentler.property = rentler.property || {};
rentler.property.__namespace = true;

rentler.property.manage = rentler.property.manage || {};
rentler.property.manage.__namespace = true;

(function ($self, undefined) {

    $self.deleteProperty = function(e) {
        var deleteBox = $('.scrimInnerContent .delete-text');

        if(deleteBox.val().toLowerCase() !== "delete")
            e.preventDefault();
    };

    $self.showDelete = function() {
        rentler.scrim.openScrimBlank();

        var innerContent = $('.scrimInnerContent'),
            template = $('#deletePropertyTemplate');

        innerContent.html(template.html());
        var form = $('.scrimInnerContent .delete-property-form');

        form.on("submit", $self.deleteProperty);
    };

    $self.deleteScrim = function() {
        var deleteButton = $('a[data-item="delete-property"]');

        if(window.location.hash == "#delete") {
            $self.showDelete();
        }

        deleteButton.on("click", function(e) {      
            e.preventDefault();
            $self.showDelete();
        });
    };

    $self.loadApplications = function() {
        var container = $('.app-accent'),
            loadingTemplate = $('#applicationLoadingTemplate').html(),
            buildingId = container.attr('data-buildingId'),
            applicationUrl = "/application/property/" + buildingId;

        if(!buildingId) return;

        container.html(loadingTemplate);

        $.get(applicationUrl, null, function(data) {
            container.html(data);

            var deleteButtons = $('.delete-application');

            deleteButtons.on('click', function(e) {
                e.preventDefault();
                var url = $(this).attr('href');
                
                container.html(loadingTemplate);

                $.post(url, null, function(data) {
                    $self.loadApplications();
                }, "json");
            });
        });
    };

    $self.activateAndDeactivate = function() {
        var checkbox = $('#activate'),
            container = $('.hide-from-results');
        
        if(checkbox.length < 1)
            return;
        
        var buildingId = checkbox.attr('data-buildingId');

        checkbox.on('change', function(e) {
            if(checkbox.attr('checked')) {
                // activate the listing
                checkbox.attr('disabled', 'disabled');
                container.children('div').html('Showing...');
                var url = '/property/activate/' + buildingId;
                $.post(url, null, function(data) {
                    checkbox.removeAttr('disabled');
                    container.children('div').html('Show in Search Results');
                }, "json");
            } else {
                // deactivate the listing
                checkbox.attr('disabled', 'disabled');
                container.children('div').html('Hiding...');
                var url = '/property/deactivate/' + buildingId;
                $.post(url, null, function(data) {
                    checkbox.removeAttr('disabled');
                    container.children('div').html('Show in Search Results');
                }, "json");
            }
        });
    };

    $self.init = function () {
        rentler.scrim.loadScrim();
        $self.deleteScrim();
        $self.loadApplications();
        $self.activateAndDeactivate();
    };

    // initialize nicely
    $(document).ready(function () { $self.init(); });


} (rentler.property.manage));