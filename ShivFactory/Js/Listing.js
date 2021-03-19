var Listing = {
    BindVarientByCategoryId: function (subCatId) {
        var data = { "SubcategoryId": subCatId};
        ajax.doPostAjax(`/Vendor/Vendor/GetVarientDdlByCategoryId`, data, function (result) {
            if (result.ResultFlag == true) {
                varient = result.Data;
                $.each(varient, function (Value, varient) {
                    console.log(varient);
                    var varient = '<article class="filter-group">\
                        <h6 class="title" > <a href="#" class="dropdown-toggle" data-toggle="collapse" data-target="#collapse_1-1"> "'+ varient.Text+'" </a> </h6 >\
                            <div class="filter-content collapse" id="collapse_1-1" style="">\
                                <div class="inner">\
                                    <ul class="list-menu">\
                                        <li><a href="#">Shorts </a></li>\
                                        <li><a href="#">Trousers </a></li>\
                                        <li><a href="#">Sweaters </a></li>\
                                        <li><a href="#">Clothes </a></li>\
                                        <li><a href="#">Home items </a></li>\
                                        <li><a href="#">Jackats</a></li>\
                                        <li><a href="#">Somethings </a></li>\
                                    </ul></div></div></article >'
                    $("#partialViewFilter").append(varient);
                    
                });
               // $("#partialViewFilter").html(varient);
            }
            common.HideLoader();
        });
    }, 
}