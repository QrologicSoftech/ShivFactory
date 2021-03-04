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
    },
}

var commonFunction = {

    GetColumnArray: function (ColumnArray, Isorderable = true, editLink, deleteLink, callback) {
        
        common.ShowLoader('.datatable');
        let columns = [];
        ColumnArray.forEach((item) => {
            if (item == 'Id') { return; }
            else if (item == 'ImagePath') {
                columns.push({
                    "data": "ImagePath", "name": "ImagePath", "defaultContent": "<i>-</i>", "orderable": false,
                    "render": function (data, type, row, meta) {
                        return '<a onclick="return LoadDiv(`' + data + '`);" target="_blank"><img src="' + data + '" alt="" style="height:40px;width:40px;"></a>';
                    }
                });
            }
            else {
                columns.push({ "data": `${item}`, "name": `${item}`, "orderable": Isorderable });
            }
        });
        if (editLink && deleteLink) {
            columns.push({
                "data": "Id", "name": "Id", "defaultContent": "<i>-</i>", "orderable": false,
                "render": function (data, type, row, meta) {

                    return '<button class="btn btn-light"><i class= "mdi mdi-database-edit text-primary" ></i><a href="' + editLink + '/' + data + '">Edit</a></button >'

                        + '<button class="btn btn-light" onclick="' + deleteLink + '(' + data + ')">'
                        + '<i class="mdi mdi-close text-danger" >Delete</i >'
                        + '</button >';
                }
            });
        }
        else if (editLink) {
            columns.push({
                "data": "Id", "name": "Id", "defaultContent": "<i>-</i>", "orderable": false,
                "render": function (data, type, row, meta) {

                    return '<button class="btn btn-light"><i class= "mdi mdi-database-edit text-primary" ></i><a href="' + editLink + '/' + data + '">Edit</a></button >';
                }
            });
        }
        else if (deleteLink) {
            columns.push({
                "data": "Id", "name": "Id", "defaultContent": "<i>-</i>", "orderable": false,
                "render": function (data, type, row, meta) {

                    return '<button class="btn btn-light" onclick="' + deleteLink + '(' + data + ')">'
                        + '<i class="mdi mdi-close text-danger" >Delete</i >'
                        + '</button >';
                }
            });
        }
        common.HideLoader('.datatable');
        return callback(columns);
    }

}


