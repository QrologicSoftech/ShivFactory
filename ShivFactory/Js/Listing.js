var ProductFilter = {
    "CategoryId": '', "SubCategoryId": '', "MiniCategoryId": '', "SearchText": '', "PageIndex": '', "PageSize": '',
    "VarientName1": '', "VarientValue1": '', "VarientName2": '', "VarientValue2": '', "VarientName3": '', "VarientValue3": '',
    "VarientName4": '', "VarientValue4": '', "VarientName5": '', "VarientValue5": '', "VarientName6": '', "VarientValue6": '',
    "VarientName7": '', "VarientValue7": '', "VarientName8": '', "VarientValue8": '', "VarientName9": '', "VarientValue9": '',
    "VarientName10": '', "VarientValue10": ''
};

var Listing = {
    OnPageLoad: function () {
        ProductFilter.CategoryId = $('#CategoryId').val();
        ProductFilter.SubCategoryId = $('#SubCategoryId').val();
        ProductFilter.MiniCategoryId = $('#MiniCategoryId').val();

        Listing.BindVarientByCategoryId();
        Listing.GetRecords();
    },

    BindVarientByCategoryId: function () {
        common.ShowLoader();
        var data = {
            "CategoryId": ProductFilter.CategoryId,
            "SubCategoryId": ProductFilter.SubCategoryId,
            "MiniCategoryId": ProductFilter.MiniCategoryId,
            "SearchText": ''
        };
        ajax.doPostAjax(`/Home/GetVarientsFilter`, data, function (result) {
            if (result.ResultFlag == true) {
                varient = result.Data;

                $.each(varient, function (Value, varient) {
                    var html = `<article class="filter-group">\
                        <h6 class="title" > <a href="#" class="dropdown-toggle" data-toggle="collapse" data-target="#${varient.VarientName}"> ${varient.VarientName} </a> </h6 >\
                            <div class="filter-content collapse" id="${varient.VarientName}" style="">\
                                <div class="inner">`;

                    varient.VarientValue.forEach((item) => {
                        html += ` <label class="custom-control custom-checkbox">
              <input type="checkbox" class="custom-control-input" name="${item}" onchange="Listing.ApplyFilter();">
              <div class="custom-control-label">${item}</div>
              </label>`;
                    });
                    html += `</div></div></article >`;

                    $("#partialViewFilter").append(html);

                });
            }
            common.HideLoader();
        });
        
    },

    ApplyFilter: function () {
        
        for (var a = 0; a < 10; a++) {
            ProductFilter["VarientName" + parseInt(a)] = '';
            ProductFilter["VarientName" + parseInt(a)] = '';
        }


        common.ShowLoader();
        var varientsName = $("#partialViewFilter").find('.filter-group a').map(function () {
            return $(this).html().trim()
        }).get()
        index = 1;
        for (var i = 0; i < varientsName.length; i++) {
            varientVal = $(`#${varientsName[i]} input:checked`).map(function () {
                return $(this).attr('name').trim()
            }).get();

            if (varientVal.length > 0) {

                var parameter = "VarientName" + parseInt(index);
                var value = "VarientValue" + parseInt(index);

                ProductFilter[parameter] = varientsName[i];
                ProductFilter[value] = varientVal.toString();

                index++;
            }
        };
       
        console.log(ProductFilter);
        common.HideLoader();
    },

    GetRecords: function () {
        pageIndex++;
        // if (pageIndex == 2 || pageIndex <= pageCount) {
        common.ShowLoader('#partialViewListing');
        var data = {
            "CategoryId": $('#CategoryId').val(),
            "SubCategoryId": $('#SubCategoryId').val(),
            "MiniCategoryId": $('#MiniCategoryId').val(),
            "SearchText": '',
            "PageIndex": 2,
            "PageSize": 10
        }        
        ajax.doPostAjax(`/Home/GetProducts`, data, function (result) {
            if (result.ResultFlag == true) {
                Listing.OnSuccess(result.Data)
            }
        });

        //  } 
    },

    OnSuccess: function (response) {

        $.each(response, function (j, dataval) {
            $("#partialViewListing").append('<div class="col-6 col-md-4 col-lg-3">\
                        <figure class="card card-product-grid" >\
                            <div class="img-wrap"> <span class="badge badge-danger"> NEW </span> <a href="#"><img src="'+ dataval.MainImage + '"></a> </div>\
                                <figcaption class="info-wrap"> <a href="#" class="title mb-2">'+ dataval.ProductName + '</a>\
                                    <div class="price-wrap"> <span class="price"><i class="fas fa-rupee-sign"></i>'+ dataval.SalePrice + '</span> &nbsp;<small class="text-muted"><s><i class="fas fa-rupee-sign"></i>' + dataval.ListPrice + '</s></small> </div>\
                                    <!-- price-wrap.// -->\
                                  <div class="rating-wrap mb-2">\
                                        <ul class="rating-stars">\
                                            <li class="stars-active"> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> </li>\
                                            <li> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> </li>\
                                        </ul>\
                                        <div class="label-rating">2/10</div>\
                                    </div>\
                                    <a href="#" class="btn btn-outline-primary" val='+ dataval.ProductId + '> <i class="fas fa-cart-plus"></i> Add to cart </a> </figcaption>\
                              </figure>\
                            </div >');

            pageCount = dataval.PageCount;
        });
        common.HideLoader('#partialViewListing');
    }
}