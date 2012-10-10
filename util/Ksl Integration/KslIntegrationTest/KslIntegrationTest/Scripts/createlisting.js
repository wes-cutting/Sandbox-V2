var baseUrl = 'http://stage-v2.ksl.com';
var listingPath = baseUrl + '/index.php?sid=17403849&nid=651&ad=';
var defaultRentlerFrameHeight = 720;

//if postMessage is supported
if (window.addEventListener) {    
	function goToListing(event) {

		//the event.data should be the listingId, a number.
		//If it is, go to the listing page
	    if (!isNaN(event.data)) {	        
	        window.location.href = listingPath + event.data;
	    }
		//might be reporting the frame's height
		if (!isNaN(event.data.replace('Height:', ''))) {
			if (parseInt(event.data.replace('Height:', '')) != defaultRentlerFrameHeight) {
				defaultRentlerFrameHeight = event.data.replace('Height:', '');
				$('#rentler_frame').height(defaultRentlerFrameHeight);
				console.log(event.data);
			}
		}
	}

    // IE9 and all other browsers	
	window.addEventListener('message', goToListing, false)		 
}
//otherwise use old method
else {    
    // IE 8
    if (window.attachEvent) {
        window.attachEvent('onmessage', goToListing);
    }
    else {
        // IE7 and Safari 3.2

        var messageCount = 0;
        var lastId = '';

        function checkForMessages() {
            if (location.hash != lastId && location.hash !== '#!') {
                lastId = location.hash;
                messageCount++;

                //match anything after # but before the _timestamp
                var pattern = /(?!#)\d{0,}(?=_)/;
                var message = lastId.match(pattern);

                if (!isNaN(message)) {
                    //preserve the back button
                    window.location.href = '#!';
                    //redirect!
                    window.location.href = listingPath + message;
                }
            }
        }

        //check the hash on an interval
        setInterval(checkForMessages, 100);
    }
}