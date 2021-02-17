var ajax = {
    doPostAjax: function (url, data, callback) {
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            success: function (result) {
                return callback(result);
            }
        });
    },
    doGetAjax: function (url, callback) {
        $.ajax({
            type: 'Get',
            url: url,
            success: function (result) {
                return callback(result);
            }
        });
    }
}



