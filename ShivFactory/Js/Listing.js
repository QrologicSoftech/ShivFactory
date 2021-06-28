var ProductFilter = {
    "CategoryId": '', "SubCategoryId": '', "MiniCategoryId": '', "SearchText": '', "PageIndex": '', "PageSize": '',
    "VarientName1": '', "VarientValue1": '', "VarientName2": '', "VarientValue2": '', "VarientName3": '', "VarientValue3": '',
    "VarientName4": '', "VarientValue4": '', "VarientName5": '', "VarientValue5": '', "VarientName6": '', "VarientValue6": '',
    "VarientName7": '', "VarientValue7": '', "VarientName8": '', "VarientValue8": '', "VarientName9": '', "VarientValue9": '',
    "VarientName10": '', "VarientValue10": ''
};
var action = "Listing";

var Listing = {
    OnPageLoad: function () {
        action = "Listing";
        ProductFilter.CategoryId = $('#CategoryId').val();
        ProductFilter.SubCategoryId = $('#SubCategoryId').val();
        ProductFilter.MiniCategoryId = $('#MiniCategoryId').val();
        Listing.BindVarientByCategoryId();
        Listing.GetRecords(1);
    },

    BindVarientByCategoryId: function () {
        //   common.ShowLoader();
        var data = {
            "CategoryId": ProductFilter.CategoryId,
            "SubCategoryId": ProductFilter.SubCategoryId,
            "MiniCategoryId": ProductFilter.MiniCategoryId,
            "SearchText": ''
        };
        ajax.doPostAjax(`/Home/GetVarientsFilter`, data, function (result) {
            if (result.ResultFlag == true) {
                varient = result.Data;

                var html = `<section class="JWMl0H _2hbLCH">
                                <div class="_2ssEMF">
                                    <div class="_3V8rao"><span>Filters</span></div>
                                    <div class="_2id1nE"><span onclick="Listing.ClearFilter();">Clear all</span></div>
                                </div>
                            </section>`;



                $.each(varient, function (Value, varient) {
                    html += `<section class="_167Mu3 _2hbLCH">
                                <div class="_213eRC _2ssEMF" data-toggle="collapse" data-target="#${varient.VarientName}" aria-expanded="true">
                                    <div class="_2gmUFU _3V8rao" title="filter-product">${varient.VarientName}</div>
                                    <svg width="16" height="27" viewBox="0 0 16 27" xmlns="http://www.w3.org/2000/svg" class="ttx38n _3DyGEM"><path d="M16 23.207L6.11 13.161 16 3.093 12.955 0 0 13.161l12.955 13.161z" fill="#fff" class="IIvmWM"></path></svg>
                                </div>
                                <div class="_3FPh42 collapse" id="${varient.VarientName}" style="">
                                    <div class="_2d0we9">
                                        <div class="_2pBqj6">
                                            <svg width="20" height="20" viewBox="0 0 17 18" class="_3WAvPc" xmlns="http://www.w3.org/2000/svg"><g fill="#2874F1" fill-rule="evenodd"><path class="-OwdlC" d="m11.618 9.897l4.225 4.212c.092.092.101.232.02.313l-1.465 1.46c-.081.081-.221.072-.314-.02l-4.216-4.203"></path><path class="-OwdlC" d="m6.486 10.901c-2.42 0-4.381-1.956-4.381-4.368 0-2.413 1.961-4.369 4.381-4.369 2.42 0 4.381 1.956 4.381 4.369 0 2.413-1.961 4.368-4.381 4.368m0-10.835c-3.582 0-6.486 2.895-6.486 6.467 0 3.572 2.904 6.467 6.486 6.467 3.582 0 6.486-2.895 6.486-6.467 0-3.572-2.904-6.467-6.486-6.467"></path></g></svg>
                                            <input type="text" class="_34uFYj" placeholder="Search ${varient.VarientName}" value="" onkeyup="Listing.FilterVarients(this)">
                                        </div>`;

                    varient.VarientValue.forEach((item) => {
                        html += `<div class="_4921Z t0pPfW" title="${item}">
                                            <div class="_1Y4Vhm _4FO7b6">
                                                <label class="_2iDkf8 t0pPfW">
                                                    <input type="checkbox" value="${item}" class="_30VH1S" onchange="Listing.ApplyFilter();">
                                                    <div class="_24_Dny"></div>
                                                    <div class="_3879cV">${item}</div>
                                                </label>
                                            </div>
                                        </div>`;
                    });

                    html += `</div>
                                </div>
                            </section>`;

                    $("#product-Filter").html(html);

                });

                //  $.each(varient, function (Value, varient) {
                //      var html = `<article class="filter-group">\
                //          <h6 class="title" > <a href="#" class="dropdown-toggle"  data-toggle="collapse" data-target="#${varient.VarientName}"> ${varient.VarientName} </a> </h6 >\
                //              <div class="filter-content collapse"  id="${varient.VarientName}" style="">\
                //                  <div class="inner">`;

                //      varient.VarientValue.forEach((item) => {
                //          html += ` <label class="custom-control custom-checkbox">
                //<input type="checkbox" class="custom-control-input" name="${item}" onchange="Listing.ApplyFilter();">
                //<div class="custom-control-label">${item}</div>
                //</label>`;
                //      });
                //      html += `</div></div></article>`;

                //      $("#partialViewFilter").append(html);

                //  });
            }
            //  common.HideLoader();
        });

    },
    FilterVarients: function (element) {

        var filter, div, a, i, txtValue;
        filter = element.value.toUpperCase();

        div = $(element).parent('div').parent().find("._4921Z");
        for (i = 0; i < div.length; i++) {
            a = div[i].getElementsByTagName("input")[0];
            txtValue = a.value || a.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                div[i].style.display = "";
            } else {
                div[i].style.display = "none";
            }
        }


    },


    ClearFilter: function () {

        $('#product-Filter ._167Mu3 ._3FPh42').find('._2iDkf8 input').each(function () {
            $(this).prop("checked", false);
        })
        Listing.ApplyFilter();
    },

    ApplyFilter: function () {

        for (var a = 0; a < 10; a++) {
            ProductFilter["VarientName" + parseInt(a)] = '';
            ProductFilter["VarientName" + parseInt(a)] = '';
        }
        // common.ShowLoader();        
        var varientsName = $("#product-Filter ._167Mu3").find('[title="filter-product"]').map(function () {
            return $(this).html().trim()
        }).get()

        index = 1;
        for (var i = 0; i < varientsName.length; i++) {
            var varientVal = $(`#product-Filter #${varientsName[i]} ._4921Z  input:checked`).map(function () {
                return $(this).attr('value').trim()
            }).get();

            if (varientVal.length > 0) {

                var parameter = "VarientName" + parseInt(index);
                var value = "VarientValue" + parseInt(index);
                ProductFilter[parameter] = varientsName[i];
                ProductFilter[value] = varientVal.toString();

                index++;
            }
        };
        // common.HideLoader();
        if (ProductFilter["VarientName1"] != null ||
            ProductFilter["VarientName2"] != null ||
            ProductFilter["VarientName3"] != null ||
            ProductFilter["VarientName4"] != null ||
            ProductFilter["VarientName5"] != null ||
            ProductFilter["VarientName6"] != null ||
            ProductFilter["VarientName7"] != null ||
            ProductFilter["VarientName8"] != null ||
            ProductFilter["VarientName9"] != null ||
            ProductFilter["VarientName10"] != null) {
            action = "Filter";
        }
        Listing.GetRecords(1);
    },

    GetRecords: function (pageIndex = 1) {
        //common.ShowLoader('#partialViewListing');
        var PageSize = 12;
        $('#Current-Page').val(pageIndex);
        ProductFilter["SearchText"] = '';
        ProductFilter["PageIndex"] = pageIndex;
        ProductFilter["PageSize"] = PageSize;

        ajax.doPostAjax(`/Home/GetProducts`, ProductFilter, function (result) {
            //alert(result.ResultFlag);
            if (result.ResultFlag == true) {
                $("#itemcount").text("Showing  " + result.Data.length + " Products out of " + result.TotalRecords + "");
                if (pageIndex == 1) {
                    rem = result.TotalRecords % PageSize;
                    let TotalPage = parseInt(result.TotalRecords / PageSize);
                    if (rem > 0) { TotalPage += 1; }
                    $('#Total-Pages').val(TotalPage);
                }
                Listing.OnSuccessFilter(result.Data);
            }
            else {
                Listing.NoDataFound(pageIndex);
            }
            //common.HideLoader('#partialViewListing');

        });


    },

    NoDataFound: function (pageIndex) {
        if (pageIndex <= 1) {
            $("#partialViewListing").append(`<div class="row">
                <div class="col-md-8 offset-2">
                    <div class="error-template">
                       
                        <div class="error-details text-center ">
                             <img src="/Content/UploadedImages/Images/imgNoProduct.png" />
                </div>
                        <div class="error-actions text-center">
                            <a href="/Home" class="btn btn-primary btn-lg"><span class="glyphicon glyphicon-home"></span>
                       Continue Shopping </a>
                        </div>
                    </div>
                </div>
            </div>`);
        }
    },



    OnSuccessFilter: function (response) {
        //var node = document.getElementById("partialViewListing");
        //node.querySelectorAll('*').forEach(n => n.remove());       

        if ($('#Current-Page').val() == 1) {
            var itemName = $("#itemName");
            itemName.html(response[0].SubCategoryName);
            $("#partialViewListing").html('');
        }

        $.each(response, function (j, dataval) {

            $("#partialViewListing").append(`<div class="col-xl-3 col-lg-3 col-md-3 col-sm-4 col-6" data-id="LCHFXQWAGHRZAG9K" id="LCHFXQWAGHRZAG9K-1">
                                <div class="_1xHGtK _373qXS e30oNt" data-tkid="a03e7e77-aae2-4bae-a994-d4b172f3bd24.LCHFXQWAGHRZAG9K.SEARCH">
<a class="_2UzuFa" target="_blank" rel="noopener noreferrer" href="/Home/ProductDetail?productId=${dataval.ProductId}&Name=${dataval.ProductName}">
                                        <div>
                                            <div>
                                                <div class="_3ywSr_" style="padding-top: 120%;">
                                                    <div class="_312yBx SFzpgZ" style="padding-top: 120%;">
                                                        <img class="_2r_T1I" alt="" src="${dataval.MainImage}" style="height: 281px;" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
</a>
                                        <div class="_3kGRXm" style="bottom: 40px;"></div>
                                        <div class="_2hVSre _1DmLJ5 -o7Q4n">
                                            <div class="_36FSn5">
                                                <svg xmlns="http://www.w3.org/2000/svg" class="_1l0elc" width="28" height="28" viewBox="0 0 20 16"><path d="M8.695 16.682C4.06 12.382 1 9.536 1 6.065 1 3.219 3.178 1 5.95 1c1.566 0 3.069.746 4.05 1.915C10.981 1.745 12.484 1 14.05 1 16.822 1 19 3.22 19 6.065c0 3.471-3.06 6.316-7.695 10.617L10 17.897l-1.305-1.215z" fill="#2874F0" class="eX72wL" stroke="#FFF" fill-rule="evenodd" opacity=".9"></path></svg>
                                            </div>
                                        </div>
                                    </a>
                                    <div class="_2B099V" style="transform: translate3d(0px, 0px, 0px);">
                                        <div class="_2WkVRV">${dataval.SubCategoryName}</div>
                                        <a class="IRpwTa" title="${dataval.ProductName}" target="_blank" rel="noopener noreferrer" href="#">${dataval.ProductName}</a>
                                        <div class="_1a8UBa">
                                            <img height="18" src="/Content/UploadedImages/Images/small-logo.png" class="_3U-Vxu">
                                        </div>
                                        <a class="_3bPFwb" target="_blank" rel="noopener noreferrer" href="#">
                                            <div class="_25b18c">
                                                <div class="_30jeq3">₹${dataval.SalePrice}</div>
                                                <div class="_3I9_wc">₹${dataval.SalePrice}</div>
                                                <div class="_3Ay6Sb"><span>${dataval.SalePrice}% off</span></div>
                                            </div>
                                        </a>
                                    </div>
                                </div>
                            </div>`);




























            //        $("#partialViewListing").append('<div class="col-6 col-md-4 col-lg-3" >\
            //                    <figure class="card card-product-grid" >\
            //                        <div class="img-wrap">  <a href="/Home/ProductDetail?productId='+ dataval.ProductId + '&Name=' + dataval.ProductName + '" alt=' + dataval.ProductName + '><img src="' + dataval.MainImage + '"></a> </div>\
            //                            <figcaption class="info-wrap"> <a id="ProductName" href="#" class="title mb-2">' + dataval.ProductName + '</a>\
            //                                <div class="price-wrap"> <span class="price"><i class="fas fa-rupee-sign"></i>'+ dataval.SalePrice + '</span> &nbsp;<small class="text-muted"><s><i class="fas fa-rupee-sign"></i>' + dataval.ListPrice + '</s></small> </div>\
            //                                <!-- price-wrap.// -->\
            //                              <div class="rating-wrap mb-2">\
            //                                    <ul class="rating-stars">\
            //                                        <li class="stars-active"> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> </li>\
            //                                        <li> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> </li>\
            //                                    </ul>\
            //                                    <div class="label-rating">2/10</div>\
            //                                </div>\
            //            <input type="hidden" id="ProductId" value="'+ dataval.ProductId + '" /><input type = "hidden" id = "ProductVarientId" value = "' + dataval.ProductVarientId + '" />\
            //<input type="hidden" id="vendorId" value="'+ dataval.VendorId + '" />\
            //<input type="hidden" id="Quantity" value="1" /><span style="display:none" id="SalePrice">'+ dataval.SalePrice + '</span>\
            //                                <button onclick="cart.AddToCartByHome(false,`'+ dataval.ProductId + '`,`' + dataval.ProductVarientId + '`,`' + dataval.VendorId + '`,`' + dataval.SalePrice + '`,`' + dataval.ProductName + '`);"  alt=' + dataval.ProductName + ' class="btn btn-outline-primary" val=' + dataval.ProductId + '> <i class="fas fa-cart-plus"></i> Add to cart </button> </figcaption>\
            //                          </figure>\
            //                        </div >');

            pageCount = dataval.PageCount;
        });
        // common.HideLoader('#partialViewListing');
    },

    BindIndexPage: function () {
        //common.ShowLoader();

        var bannerli = "", bannerdiv = "", productSlider = '';

        ajax.doPostAjax(`/Home/GetIndexData`, '', function (result) {
            console.log(result);
            $("#carousel").html(result);
            $('#carousel').carousel();
            //        if (result.ResultFlag == true) {
            //            $.each(result.Data.Banners, function (index, Value) {
            //                bannerli += `<li data-target="#carousel1_indicator" data-slide-to="${index}" class="${index == 0 ? "active" : ""}"></li>`;
            //                bannerdiv += `<div class="carousel-item ${index == 0 ? "active" : ""}"> <img src="${Value.ImagePath}" alt=""> </div>`;
            //            });
            //            $("#carousel1_indicator ol").html(bannerli);
            //            $("#carousel1_indicator div").html(bannerdiv);

            //            //Bind Product slider
            //            $.each(result.Data.Products, function (index, Value) {
            //                productSlider += `<section class="padding-bottom-sm bg-white  mb-3 pb-3 pt-0">
            //<div class=" container">
            //    <header class="section-heading pt-3 pb-2">
            //        <h3 class="section-title text-left">${Value.Title} </h3>
            //    </header>
            //    <div class="card-deal px-1">
            //        <div class="allitem-slider owl-carousel owl-button"><div class="owl-stage-outer"><div class="owl-stage" style="transform: translate3d(0px, 0px, 0px); transition: all 0s ease 0s; width: 2010px;">`;

            //                $.each(Value.SubCategory, function (a, b) {

            //                    productSlider += `<div class="owl-item active" style="width: 236.2px; margin-right: 15px;"><div class="item">
            //                <figure class="card-product-grid card-sm">
            //                    <a href="/Home/ProductListing?subId=${b.SubCategoryId}" class="img-wrap"> <img src="${b.ImagePath}"> </a>
            //                    <div class="text-center">
            //                        <div class="price mt-1"><a href='#'>`+ b.SubCategoryName + `</a></div>
            //                        <div class="price mt-1">`+ b.price + `  <s class="old-rrice"> ` + b.ListPrice + `</s></div>
            //                        <div class="add-cart pt-1"></div>
            //                        <span class="badge badge-danger"> -20% </span>
            //                    </div>
            //                </figure>
            //            </div></div>`;
            //                })
            //                productSlider += `</div><div class="owl-nav"><button type="button" role="presentation" class="owl-prev"><span><i class="fa fa-angle-right"></i></span></button><button type="button" role="presentation" class="owl-next"><span><i class="fa fa-angle-left"></i></span></button></div></div></div></div></div></section>`;

            //            });
            //            $("#Product-slider").html(productSlider);

            //        }
            // common.HideLoader();

        });

    },
    CheckPincodeAvailibity: function () {
        //   common.ShowLoader();
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
        // common.HideLoader();
    },


}