var ImageZoom = {

    ChangeZoomImage: function (imageId, newPath) {
        var zoomElement = $(imageId);

        // destroy old one
        zoomElement.removeData('zoom-image');
        $('.zoomContainer').remove();

        // set new one
        zoomElement.attr('data-zoom-image', newPath);
        zoomElement.attr('src', newPath);

        // reinitial ezPluszoom
        zoomElement.ezPlus({
            scrollZoom: true
        });
    },
    Zoom: function (imageId, scrollZoom = true) {
        $(imageId).ezPlus({
            scrollZoom: scrollZoom
        });
    },

}