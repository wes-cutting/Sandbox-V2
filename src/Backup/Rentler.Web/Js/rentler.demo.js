rentler = window.rentler || {};
rentler.__namespace = true;

rentler.demo = rentler.demo || {};
rentler.demo.__namespace = true;

(function ($self, undefined) {

    // now you can reference anything within
    // the namespace as $self

    $self.MyClass = function() {
        // <summary>This is a class demonstration</summary>

        var self = this;

        // everything within the class object is now referenced
        // as self;

        self.myPublicMethod = function() {
            // <summary>This is a public method</summary>
        };

        privateMethod = function() {
            // <summary>This is a private method</summary>
        }
    };
    $self.MyClass.__class = true;

    $self.myPublicStaticNamespaceMethod = function() {
        // <summary>This is a public static namespace method.</summary>
    };

}(rentler.demo));