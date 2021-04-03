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
                        <h6 class="title" > <a href="#" class="dropdown-toggle"  data-toggle="collapse" data-target="#${varient.VarientName}"> ${varient.VarientName} </a> </h6 >\
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

        common.HideLoader();
    },

    GetRecords: function () {
        common.ShowLoader('#partialViewListing');
        var data
        if (pageIndex > pageCount) {
            common.ShowLoader('#partialViewListing');
            data = {
                "CategoryId": $('#CategoryId').val(),
                "SubCategoryId": $('#SubCategoryId').val(),
                "MiniCategoryId": $('#MiniCategoryId').val(),
                "SearchText": '',
                "PageIndex": pageIndex,
                "PageSize": 10
            }
            pageIndex++;
        } else {
            data = {
                "CategoryId": $('#CategoryId').val(),
                "SubCategoryId": $('#SubCategoryId').val(),
                "MiniCategoryId": $('#MiniCategoryId').val(),
                "SearchText": '',
                "PageIndex": 1,
                "PageSize": 10
            }
            pageIndex++;
        }
        ajax.doPostAjax(`/Home/GetProducts`, data, function (result) {
            console.log(result)
            if (result.ResultFlag == true) {
                Listing.OnSuccess(result.Data)
            }
        });


    },

    OnSuccess: function (response) {
        var itemcount = $("#itemcount");
        itemcount.text(response.length + " items found");
        $('#categoryName').append(response[0].CategoryName);
        $('#categoryName').attr('href', '/Home/ProductListing?Id=' + response[0].CategoryId);
        $('#subcategoryName').append(response[0].SubCategoryName);
        $('#subcategoryName').attr('href', '/Home/ProductListing?subId=' + response[0].SubCategoryId);
        $.each(response, function (j, dataval) {
            $("#partialViewListing").append('<div class="col-6 col-md-4 col-lg-3" >\
                        <figure class="card card-product-grid" >\
                            <div class="img-wrap">  <a href="/Home/ProductDetail?productId='+ dataval.ProductId + '" alt='+dataval.ProductName+'><img src="' + dataval.MainImage + '"></a> </div>\
                                <figcaption class="info-wrap"> <a href="#" class="title mb-2">' + dataval.ProductName + '</a>\
                                    <div class="price-wrap"> <span class="price"><i class="fas fa-rupee-sign"></i>'+ dataval.SalePrice + '</span> &nbsp;<small class="text-muted"><s><i class="fas fa-rupee-sign"></i>' + dataval.ListPrice + '</s></small> </div>\
                                    <!-- price-wrap.// -->\
                                  <div class="rating-wrap mb-2">\
                                        <ul class="rating-stars">\
                                            <li class="stars-active"> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> </li>\
                                            <li> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> </li>\
                                        </ul>\
                                        <div class="label-rating">2/10</div>\
                                    </div>\
                                    <a href="#"  alt='+ dataval.ProductName +' class="btn btn-outline-primary" val='+ dataval.ProductId + '> <i class="fas fa-cart-plus"></i> Add to cart </a> </figcaption>\
                              </figure>\
                            </div >');

            pageCount = dataval.PageCount;
        });
        common.HideLoader('#partialViewListing');
    },

    GetDiscountPercentage: function (Price1, Price2) {
        var discout = parseFloat(Price1) - parseFloat(Price2);
        var percentage = discout / 100 * 100
    },
    BindIndexPage: function () {
        common.ShowLoader();

        var bannerli = "", bannerdiv = "", productSlider = '';

        ajax.doPostAjax(`/Home/GetIndexData`, '', function (result) {
            if (result.ResultFlag == true) {
                $.each(result.Data.Banners, function (index, Value) {
                    bannerli += `<li data-target="#carousel1_indicator" data-slide-to="${index}" class="${index == 0 ? "active" : ""}"></li>`;
                    bannerdiv += `<div class="carousel-item ${index == 0 ? "active" : ""}"> <img src="${Value.ImagePath}" alt=""> </div>`;
                });
                $("#carousel1_indicator ol").html(bannerli);
                $("#carousel1_indicator div").html(bannerdiv);

                //Bind Product slider

                $.each(result.Data.Products, function (index, Value) {
                    productSlider += `<section class="padding-bottom-sm bg-white  mb-3 pb-3 pt-0">
    <div class=" container">
        <header class="section-heading pt-3 pb-2">
            <h3 class="section-title text-left">${Value.Title} </h3>
        </header>
        <div class="card-deal px-1">
            <div class="allitem-slider owl-carousel owl-button"><div class="owl-stage-outer"><div class="owl-stage" style="transform: translate3d(0px, 0px, 0px); transition: all 0s ease 0s; width: 2010px;">`;
                    
                    $.each(Value.SubCategory, function (a, b) {
                    
                        productSlider += `<div class="owl-item active" style="width: 236.2px; margin-right: 15px;"><div class="item">
                    <figure class="card-product-grid card-sm">
                        <a href="/Home/ProductListing?subId=${b.SubCategoryId}" class="img-wrap"> <img src="${b.ImagePath}"> </a>
                        <div class="text-center">
                            <div class="price mt-1"><a href='#'>`+ b.SubCategoryName + `</a></div>
                            <div class="price mt-1">`+ b.price +`  <s class="old-rrice"> `+ b.ListPrice+`</s></div>
                            <div class="add-cart pt-1"></div>
                            <span class="badge badge-danger"> -20% </span>
                        </div>
                    </figure>
                </div></div>`;
                    })
                    productSlider += `</div>******<div class="owl-nav"><button type="button" role="presentation" class="owl-prev"><span><i class="fa fa-angle-right"></i></span></button><button type="button" role="presentation" class="owl-next"><span><i class="fa fa-angle-left"></i></span></button></div></div></div></div></div></section>`;

                });
               $("#Product-slider").html(productSlider);

            }
            common.HideLoader();
        });

    },
    CheckPincodeAvailibity: function () {
        common.ShowLoader();
        var pincod = $('#pincode').val();
        var vendorId = $('#vendorId').val();
        ajax.doPostAjax(`/Home/CheckPincodeAvailibity?pincode=` + pincod + `&vendorId=` + vendorId, null, function (result) {
            if (result.ResultFlag == true) {
                $('#deliverytime').css('display', 'block');
            } else {
                $('#lblDeliery').text("Delivery is not available for area");
                $('#lblDeliery').css("color", "Red");
                $('#deliverytime').css('display', 'none');
            
            }
    });
        common.HideLoader();
    },

   
}