/// <reference path="/js/rentler.js" />

/*
Base namespace configuration.
*/
rentler = window.rentler || {};
rentler.__namespace = true;

rentler.uploader = rentler.uploader || {};
rentler.uploader.__namespace = true;

(function ($self, undefined) {

    $self.vars = {
        buildingId: 0,
        uploadingPhotoCount: 0
    };

    $self.photoUpload = function(id, fileName) {
        
        $self.vars.uploadingPhotoCount++;
        //getter
        var disabled = $(".qq-upload-list" ).sortable("option", "disabled");

        if(disabled !== true)
            $(".qq-upload-list" ).sortable("option", "disabled", true);
    };
            
    $self.photoUploaded = function(id, fileName, responseJSON) {
        
        $self.vars.uploadingPhotoCount--;

        if($self.vars.uploadingPhotoCount == 0) {
            $(".qq-upload-list" ).sortable("option", "disabled", false);
        }
                
        if(responseJSON.StatusCode == 200) {
            // a photo has been successfully uploaded                                        

            // get li using filename
            $('.qq-upload-list li.uploading').each(function(index) {                                                
                var itemFileName = $self.extractFilename($(this));

                if(fileName == itemFileName) {
                    // are there any existing photos ?
                    var uploadedCount = $('.qq-upload-list li.uploaded').length;

                    // no existing photos
                    if(uploadedCount == 0) {

                        // as long as its not already first in the list move it to the beginning
                        // of the uploading list
                        if(index != 0)
                            $(this).insertBefore($('.qq-upload-list li.uploading').first());
                                    
                        //$self.setPrimary(responseJSON.Result.PhotoId);
                    }
                    else {
                        // move li to last position after existing items
                        $(this).insertAfter($('.qq-upload-list li.uploaded').last());
                    }

                    // no longer uploading, mark item as uploaded
                    $(this).removeClass('uploading').addClass('uploaded');
                                                        
                    // li id = photo id after upload
                    $(this).attr('id', responseJSON.Result[0].PhotoId);                                        

                    // set link and show image
                    var src = $self.generatePhotoSrc(responseJSON.Result[0], 115, 85);

                    $(this).find('.file-details img').attr('src', src).removeClass('hide');
                            
                    // register click event, set photo id on link and show
                    var removeLink = $(this).find('.file-remove .file-upload-actions a');
                    /*removeLink.live('click', function(e) {
                        e.preventDefault();
                        var photoId = $(this).attr("data-id");
                        $self.removePhoto(photoId);
                    });*/

                    removeLink.attr('data-id', responseJSON.Result[0].PhotoId);
                    removeLink.removeClass('hide');                                                           

                    $self.reorderPhotos(true);
                    $self.refreshFeaturedPreview();
                }                       
            });
        }
        else { 
            // failed to upload file                   
            $('.qq-upload-list li.uploading').each(function(index) {                                                
                var itemFileName = $self.extractFilename($(this));

                if(fileName == itemFileName) {
                    // no longer uploading, mark item as failed
                    $(this).removeClass('uploading').addClass('failed');
                            
                    $(this).find('.file-order').html('-');

                    // find failed text div                            
                    $(this).find('.file-details .qq-upload-failed-text').html('- Failed -').removeClass('hide');
                            
                    $(this).find('.file-remove .file-upload-actions a').remove();
                }
            });

            $('#uploadFailures').append(
                '<li>' + 
                    '<span class="failed-filename">* ' + fileName + '</span>' +
                    ' - upload failed with message : ' +
                    '<span class="failed-message">' + responseJSON.Message + '</span>' +
                '</li>'
            );

            $('.qq-upload-list').sortable({ cancel : '.qq-upload-list li.failed' });
        }        
    }; 
    
    $self.toPhotoItemTemplate = function(photo, index) {
        var src = $self.generatePhotoSrc(photo, 115, 85);

        var order = (index == 0) ? 'Primary' : index + 1;

        var id = ' id="' + photo.PhotoId + '"';                
        var cl = ' class="uploaded"';
            
        var markup = '<li' + id + cl + '>' +                
            '<div class="file-order">' + order + '</div>' +                 
            '<div class="file-details">' +                    
                '<img src="' + src + '" alt="" width="115" height="85" />' +
            '</div>' +
            '<div class="file-remove">' +
                '<span class="file-upload-actions">' +
                    '<a data-id="' + photo.PhotoId + '" class="qq-upload-remove" href="#">Remove</a>' +                    
                '</span>' +
            '</div>' +
            '<div class="item-handle">&nbsp;</div>' +
            '<div style="clear:both;"></div>' +
        '</li>';

        return markup;
    }; 
    
    $self.clearFailedUploads = function() {
        $('#uploadFailures').html('');
        $('.qq-upload-list li.failed').remove();
    }; 
    
    $self.reorderPhotos = function(uiOnly) {               
        $('.qq-upload-list li.uploaded').each(function(index) {                        
            var order = (index == 0) ? 'Primary' : index + 1
            $(this).find('.file-order').html(order);
        });
        
        if(!uiOnly) {
            var data = { buildingId : $self.vars.buildingId, photoIds : $('.qq-upload-list').sortable('toArray') };            
        
            $.ajax(
                '/dashboard/photo/reorder', 
                { 
                    data : data, 
                    type : 'POST', 
                    traditional : true, 
                    success: function(status) 
                    {
                        if(status.StatusCode != 200) 
                        {
                            var msg = "Unable to complete request to re-order photos: ";
                            
                            // request to reorder failed. make sure we have the original order
                            // of photos so they can be restored properly
                            if(status.Result) {
                                alert(msg + "try again");

                                // put photo back in the old order
                                for(var i = 0; i < status.Result.length; ++i) {
                                    var id = '#' + status.Result[i];
                                    var currentIndex = $('.qq-upload-list li').index($(id));

                                    // we have to move it
                                    if(currentIndex != i) {
                                        // grab the previous one and insertAfter
                                        // if i - 1 < 0 we have to insert before 0
                                        var prevIndex = i - 1;
                                    
                                        if(prevIndex < 0) {
                                            var prevItem = $('.qq-upload-list li').eq(0);                                        
                                            $(id).insertBefore($(prevItem));
                                        }
                                        else {
                                            var prevItem = $('.qq-upload-list li').eq(prevIndex);
                                            $(id).insertAfter($(prevItem));
                                        }
                                    }
                                }

                                // reset order but don't post
                                $self.reorderPhotos(true);
                            }
                            else {
                                alert(msg + "refresh page before trying again");
                            }                            
                        }
                    }                    
                }
            );                   
        }        
    };   
    
    $self.refreshFeaturedPreview = function() {
        if($('.qq-upload-list li').length > 0) {
            var primaryPhotoLnk = $('.qq-upload-list li').first().find('.file-details img').attr('src');
            $('#imgFeaturedPreview').attr('src', primaryPhotoLnk);
        }
        else {
            $('#imgFeaturedPreview').attr('src', '/images/noimage-200x150.jpg');
        }
    };    

    $self.setPrimary = function(photoId) {
        $self.setSortOrder(photoId, 0);
    };

    $self.extractFilename = function(listItem) {
        return listItem.find('.file-details .qq-upload-file').html();
    };

    $self.removePhoto = function(photoId) { 
        $self.clearFailedUploads();

        var idSelector = '#' + photoId;       
        $(idSelector).hide();
                        
        amplify.request(
            'rentler.photo.remove',
            { id : photoId },
            function(status) {
                if(status.StatusCode == 200) {                   
                    $(idSelector).remove();
                    $self.reorderPhotos(true);                    
                    $self.refreshFeaturedPreview();
                }
                else {
                    alert("Error: failed to remove photo");
                    $(idSelector).show();                    
                }
            }
        );
    };

    $self.generatePhotoSrc = function(photo, width, height) {
        // remove period if it is there
        var extension = photo.Extension.replace(".","");        

        var src = photoStore + '/' + photo.BuildingId + '/' + 
            photo.PhotoId + '-' + width + 'x' + height + "." + 
            extension;

        return src;
    };

    $self.create = function (baseUrl, buildingId) {
        $self.vars.buildingId = buildingId;

        // requires qq file uploader
        //create uploader
		$self.uploader = new qq.FileUploader({
			element : document.getElementById('uploader'),
			action : baseUrl + '/dashboard/photo/upload',
			allowedExtensions : ['jpg', 'jpeg', 'png', 'gif'],
			sizeLimit : 10485760,
			debug : true,
            maxConnections : 1,
            params : { id : buildingId },			
            onSubmit : $self.photoUpload,
			onComplete : $self.photoUploaded,                                        
            fileTemplate: '<li class="uploading">' +
                '<div class="file-order"></div>' +                             
                '<div class="file-details">' +                       
                    '<div class="qq-upload-spinner"></div>' + 
                    '<div class="qq-upload-failed-text hide"></div>' +
                    '<img class="hide" src="" alt="" width="115" height="85" />' +
                    '<div class="file-info">' +                                                   
                        '<span class="qq-upload-file"></span>' +
                        '<span class="qq-upload-size"></span>' +
                    '</div>' + 
                    '<span class="qq-upload-cancel"></span>' +                                                                                                             
                '</div>' +
                '<div class="file-remove">' +
                    '<span class="file-upload-actions">' +
                        '<a class="qq-upload-remove hide" href="#">Remove</a>' +                    
                    '</span>' +
                '</div>' +
                '<div class="item-handle">&nbsp;</div>' +
                '<div style="clear:both;"></div>' +
            '</li>'
		});

        // show existing photos
        amplify.request(
            'rentler.property.getphotos',
            { id : buildingId },
            function (status) {            
                if (status.StatusCode == 200) {
                    for(var i = 0; i < status.Result.length; ++i) {
                        var itemMarkup = 
                            $self.toPhotoItemTemplate(
                                status.Result[i],
                                i
                            );
                            
                        $('.qq-upload-list').append(itemMarkup);
                    }
                }
                else {
                    alert("Failed to get photos because: " + status.Message);
                }
            }
        );

        // requires jquery ui
        // make photos list sortable by dragging
        $('.qq-upload-list').sortable({ 
            revert: true,
            handle: "div.item-handle",
            start: function(event, ui) {
                // disable upload button
                // disable remove links

                // re-enable both after successful change of sort order
                $self.clearFailedUploads();                               
            },
            stop: function(event, ui) {
                $self.reorderPhotos(false);                              
                //var photoId = ui.item.attr('id');
                $self.refreshFeaturedPreview();                
            }
        });

        // not sure why this is done
        $('.qq-upload-list').disableSelection();        
        
        $('.qq-upload-list li.uploaded')
            .find('.file-remove .file-upload-actions a')
            .live(
                'click',
                function(e) {
                    e.preventDefault();
                    var photoId = $(this).attr("data-id");
                    $self.removePhoto(photoId);
                } 
            );

        $('.qq-upload-button').live('click', function(e) {
            $self.clearFailedUploads();
        });
    };

    function configure() {        
        /// <summary>Configures all of the amplify requests.</summary>

        // get photos for building
        amplify.request.define('rentler.property.getphotos', 'ajax',{
		    url: '/dashboard/property/photos/{id}',
		    type: 'GET'
	    });

        // remove photo by id
        amplify.request.define('rentler.photo.remove', 'ajax', {
		    url: '/dashboard/photo/remove/{id}',
		    type: 'POST'
	    });                                
    };

    configure();

} (rentler.uploader));