ko.bindingHandlers.executeOnEnter = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var allBindings = allBindingsAccessor();
        $(element).keypress(function (event) {
            var keyCode = (event.which ? event.which : event.keyCode);
            if (keyCode === 13) {
                allBindings.executeOnEnter.call(viewModel);
                return false;
            }
            return true;
        });
    }
};

ko.bindingHandlers.limitText = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        $(element).keydown(function (event) {
            var allBindings = allBindingsAccessor();
            allBindings.limitText.call(viewModel, allBindings.charLimit);
        });
    }
};

ko.bindingHandlers.readonly = {
    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        if (value)
            $(element)
                .attr("readonly", "readonly")
                .addClass("readonly");
        else
            $(element)
                .removeAttr("readonly")
                .removeClass("readonly");
    }
};

ko.bindingHandlers.disabled = {
    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        if (value) {
            $(element)
                .attr("disabled", "disabled")
                .addClass("readonly");
        }
        else {
            $(element)
                .removeAttr("disabled")
                .removeClass("readonly")
        }
    }
};

ko.bindingHandlers.decodeValue = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        value = value.replace(/&quot;/g, '"')
            .replace(/&#39;/g, "'")
            .replace(/&lt;/g, "<")
            .replace(/&gt;/g, ">");

        var observableValue = valueAccessor();
        observableValue(value);

        ko.bindingHandlers.value.init(element, valueAccessor, allBindingsAccessor);
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        ko.bindingHandlers.value.update(element, valueAccessor);
    }
};