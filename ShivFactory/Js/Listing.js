
var Listing = {
    BindVarientByCategoryId: function (subCatId) {
        var data = { "SubcategoryId": 5};
        ajax.doPostAjax(`/Vendor/Vendor/GetVarientDdlByCategoryId`, data, function (result) {
            if (result.ResultFlag == true) {
                varient = result.Data;
                $.each(varient, function (Value, varient) {
                    var varient = '<article class="filter-group">\
                        <h6 class="title" > <a href="#" class="dropdown-toggle" data-toggle="collapse" data-target="#collapse_1-1"> '+ varient.Text+' </a> </h6 >\
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
            }
            common.HideLoader();
        });
        Listing.GetRecords();
    }, 

    //BindProducts: function (subCatId) {
    //    var data = { "SubcategoryId": subCatId };
    //    ajax.doPostAjax(`/Vendor/Vendor/GetVarientDdlByCategoryId`, data, function (result) {
    //        if (result.ResultFlag == true) {
    //            varient = result.Data;
    //            $.each(varient, function (Value, varient) {
    //                console.log(varient);
    //                var varient = '<div class="col-6 col-md-4 col-lg-3">\
    //                    < figure class="card card-product-grid" >\
    //                        <div class="img-wrap"> <span class="badge badge-danger"> NEW </span> <a href="#"><img src="images/items/1.jpg"></a> </div>\
    //                            <figcaption class="info-wrap"> <a href="#" class="title mb-2">Hot sale unisex New Design Shirt for sport polo shirts latest design</a>\
    //                                <div class="price-wrap"> <span class="price"><i class="fas fa-rupee-sign"></i> 32.00</span> &nbsp;<small class="text-muted"><s><i class="fas fa-rupee-sign"></i> 100</s></small> </div>\
    //                                <!-- price-wrap.// -->\
    //                              <div class="rating-wrap mb-2">\
    //                                    <ul class="rating-stars">\
    //                                        <li class="stars-active"> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> </li>\
    //                                        <li> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> </li>\
    //                                    </ul>\
    //                                    <div class="label-rating">2/10</div>\
    //                                </div>\
    //                                <a href="#" class="btn btn-outline-primary"> <i class="fas fa-cart-plus"></i> Add to cart </a> </figcaption>\
    //                          </figure>\
    //                        </div >'
    //                $("#partialViewListing").append(varient);

    //            });
    //            // $("#partialViewFilter").html(varient);
    //        }
    //        common.HideLoader();
    //    });
   // }, 

    //Function to make AJAX call to the Jsonresult
    GetRecords: function () {
pageIndex++;
       // if (pageIndex == 2 || pageIndex <= pageCount) {
            common.ShowLoader();
            $.ajax({
                type: "POST",
                url: "/Home/GetProduct",
                data: '{pageIndex: ' + pageIndex + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: Listing.OnSuccess,
                failure: function (response) {
                    console.log("failure:: :: " + response);
                    //alert(response.d);
                },
                error: function (response) {
                    console.log("error:: :: " + response);
                }
            });
      //  } 
    },

OnSuccess: function(response) {

    $.each(response, function (j, dataval) {
        console.log(response);
        $("#partialViewListing").append('<div class="col-6 col-md-4 col-lg-3">\
                        <figure class="card card-product-grid" >\
                            <div class="img-wrap"> <span class="badge badge-danger"> NEW </span> <a href="#"><img src="images/items/1.jpg"></a> </div>\
                                <figcaption class="info-wrap"> <a href="#" class="title mb-2">Hot sale unisex New Design Shirt for sport polo shirts latest design</a>\
                                    <div class="price-wrap"> <span class="price"><i class="fas fa-rupee-sign"></i> 32.00</span> &nbsp;<small class="text-muted"><s><i class="fas fa-rupee-sign"></i> 100</s></small> </div>\
                                    <!-- price-wrap.// -->\
                                  <div class="rating-wrap mb-2">\
                                        <ul class="rating-stars">\
                                            <li class="stars-active"> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> </li>\
                                            <li> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> </li>\
                                        </ul>\
                                        <div class="label-rating">2/10</div>\
                                    </div>\
                                    <a href="#" class="btn btn-outline-primary"> <i class="fas fa-cart-plus"></i> Add to cart </a> </figcaption>\
                              </figure>\
                            </div >*');

       
        pageCount = dataval.PageCount;
    });
    common.HideLoader();
}
}