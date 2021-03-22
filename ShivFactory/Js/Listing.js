
var Listing = {
    BindVarientByCategoryId: function (subCatId) {
        common.ShowLoader();
        var data = { "SubcategoryId": 5};
        ajax.doPostAjax(`/Home/GetVarientsFilter`, data, function (result) {
            if (result.ResultFlag == true) {
                varient = result.Data;
                //$.each(varient, function (Value, varient) {
                //    var varient = `<article class="filter-group">\
                //        <h6 class="title" > <a href="#" class="dropdown-toggle" data-toggle="collapse" data-target="#${varient.Text}"> ${varient.Text} </a> </h6 >\
                //            <div class="filter-content collapse" id="${varient.Text}" style="">\
                //                <div class="inner">\
                //                    <ul class="list-menu">\
                //                        <li><a href="#">Shorts </a></li>\
                //                        <li><a href="#">Trousers </a></li>\
                //                        <li><a href="#">Sweaters </a></li>\
                //                        <li><a href="#">Clothes </a></li>\
                //                        <li><a href="#">Home items </a></li>\
                //                        <li><a href="#">Jackats</a></li>\
                //                        <li><a href="#">Somethings </a></li>\
                //                    </ul></div></div></article >`;
                //    $("#partialViewFilter").append(varient);
                    
                //});
               
                    
                    $.each(varient, function (Value, varient) {
                        var html = `<article class="filter-group">\
                        <h6 class="title" > <a href="#" class="dropdown-toggle" data-toggle="collapse" data-target="#${varient.VarientName}"> ${varient.VarientName} </a> </h6 >\
                            <div class="filter-content collapse" id="${varient.VarientName}" style="">\
                                <div class="inner">\
                                    <ul class="list-menu">`;
                        debugger;
                        varient.VarientValue.forEach((item) => {
                            html += `<li><a href="#">${item} </a></li>`;
                        });
                       
                                      html += `</ul></div></div></article >`;

                        $("#partialViewFilter").append(html);

                    });
               
            }
            common.HideLoader();
        });
        //Listing.GetRecords();
    }, 

    GetRecords: function () {
pageIndex++;
       // if (pageIndex == 2 || pageIndex <= pageCount) {
        common.ShowLoader();
        var data = {
            "CategoryId": $('#CategoryId').val(),
            "SubCategoryId": $('#SubCategoryId').val(),
            "MiniCategoryId": $('#MiniCategoryId').val(),
            "SearchText": '',
            "PageIndex": 2,
            "PageSize": 10
        }
        console.log(data);
        ajax.doPostAjax(`/Home/GetProducts`, data, function (result) {
            if (result.ResultFlag == true) {
                Listing.OnSuccess(result.Data)
                } 
        });      
           
      //  } 
    },

OnSuccess: function(response) {

    $.each(response, function (j, dataval) {
        $("#partialViewListing").append('<div class="col-6 col-md-4 col-lg-3">\
                        <figure class="card card-product-grid" >\
                            <div class="img-wrap"> <span class="badge badge-danger"> NEW </span> <a href="#"><img src="'+dataval.MainImage+'"></a> </div>\
                                <figcaption class="info-wrap"> <a href="#" class="title mb-2">'+ dataval.ProductName +'</a>\
                                    <div class="price-wrap"> <span class="price"><i class="fas fa-rupee-sign"></i>'+ dataval.SalePrice + '</span> &nbsp;<small class="text-muted"><s><i class="fas fa-rupee-sign"></i>' + dataval.ListPrice +'</s></small> </div>\
                                    <!-- price-wrap.// -->\
                                  <div class="rating-wrap mb-2">\
                                        <ul class="rating-stars">\
                                            <li class="stars-active"> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> </li>\
                                            <li> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> </li>\
                                        </ul>\
                                        <div class="label-rating">2/10</div>\
                                    </div>\
                                    <a href="#" class="btn btn-outline-primary" val='+ dataval.ProductId+'> <i class="fas fa-cart-plus"></i> Add to cart </a> </figcaption>\
                              </figure>\
                            </div >');
       
        pageCount = dataval.PageCount;
    });
    common.HideLoader();
}
}