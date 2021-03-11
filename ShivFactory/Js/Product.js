var product = {

    BindSubCategoryByCatId: function (cat_id, subcat_id, mini_id) {
        var ddlSubCategory = $(subcat_id);
        var ddlminiCategory = $(mini_id);
        var selectedCatId = $(cat_id + " :selected").attr('value');
        //if (! typeof selectedCatId === "undefined") {
        common.ShowLoader(subcat_id);
        var data = { "categoryId": selectedCatId };
        ddlSubCategory
            .empty()
            .append('<option selected="selected" value="-1">Select</option>');
        ddlminiCategory
            .empty()
            .append('<option selected="selected" value="-1">Select</option>');
        ajax.doPostAjax(`/Vendor/Vendor/GetSubcategoryByCategoryId`, data, function (result) {
            if (result.length > 0) {
                $.each(result, function (Value, Text) {
                    ddlSubCategory.append($('<option></option>').val(Text.Value).html(Text.Text));
                });
            }
            common.HideLoader(subcat_id);
        });
    },
    BindMiniCategoryBySubCatId: function (subcat_id, mini_id) {
        var selectedSubCatId = $(subcat_id + " :selected").attr('value');
        if (selectedSubCatId > 0) {
            common.ShowLoader(mini_id);
            var ddlMiniCategory = $(mini_id);
            data = { "subcategoryId": selectedSubCatId }
            ajax.doPostAjax(`/Vendor/Vendor/GetMinicategoryBySubCategoryId`, data, function (result) {
                ddlMiniCategory
                    .empty()
                if (result.length > 0) {
                    $.each(result, function (Value, Text) {
                        ddlMiniCategory.append($('<option></option>').val(Text.Value).html(Text.Text));
                    });
                    common.HideLoader(mini_id);
                } else {
                    ddlMiniCategory.append($('<option></option>').val('-1').html('Select'));
                }
            });
        }
    },
    NextLevel: function (stepId, callback) {
        let result = true;
        alert(stepId.toLowerCase());
        if (stepId.toLowerCase() == 'step1') {
            product.ValidateProductStep1(function (res) {
                result = res;
                if (res == true) {
                    return callback(true);
                } else {
                    return false;;
                }
            });
        } else if(stepId.toLowerCase() == 'step2') {
            product.ValidateProductStep2(function (res) {
                result = res;
                if (res == true) {
                    alert("Result is  :" + res);
                    return callback(true);
                } else {
                    alert("Result is  in else :" + res);
                    return false;;
                }
            });
        } else if (stepId.toLowerCase() == 'step3') {
            product.ValidateProductStep3(function (res) {
                result = res;
                if (res == true) {
                    alert("Result is  :" + res);
                    return callback(true);
                } else {
                    alert("Result is  in else :" + res);
                    return false;;
                }
            });
        } else if (stepId.toLowerCase() == 'step4') {
            product.ValidateProductStep4(function (res) {
                result = res;
                if (res == true) {
                    alert("Result is  :" + res);
                    return callback(true);
                } else {
                    alert("Result is  in else :" + res);
                    return false;;
                }
            });
        } else if (stepId.toLowerCase() == 'step5') {
            product.ValidateProductStep5(function (res) {
                result = res;
                if (res == true) {
                    alert("Result is  :" + res);
                    return callback(true);
                } else {
                    alert("Result is  in else :" + res);
                    return false;;
                }
            });
        }
        
       // return callback(result);
    },
    ValidateProductStep1: function (callback) {
        if ($('#Category').find(":selected").val() == '-1' || $('#Category').find(":selected").val() == 'undefined' || $('#Category').find(":selected").val().length == '0') {
            $('span[data-valmsg-for="CategoryId"]').text($('#Category').attr('data-val-required'));
            return callback("false");
        } else if ($('#SubCategory').find(":selected").val() == '-1' || $('#SubCategory').find(":selected").val() == undefined || $('#SubCategory').find(":selected").val().length == '0') {
            $('span[data-valmsg-for="SubCategoryId"]').text($('#SubCategory').attr('data-val-required'));
            return callback("false");
        } else if ($('#MiniCategory').find(":selected").val() == '-1' || $('#MiniCategory').find(":selected").val() == undefined || $('#MiniCategory').find(":selected").val().length == '0') {
            $('span[data-valmsg-for="MiniCategoryId"]').text($('#MiniCategory').attr('data-val-required'));
            return callback("false");
        } else if ($('#ProductName').val() == '-1' || $('#ProductName').val() == undefined || $('#ProductName').val().length == '0') {
            $('span[data-valmsg-for="ProductName"]').text($('#ProductName').attr('data-val-required'));
            return callback("false");
        } else if ($('#ProductCode').val() == undefined || $('#ProductCode').val().length == '0') {
            $('span[data-valmsg-for="ProductCode"]').text($('#ProductCode').attr('data-val-required'));
            return callback("false");
        } else {
            product.CheckProductCode($('#ProductCode').val());
        }
        return callback(true);
    },
    ValidateProductStep2: function (callback) {
        if ($('#LocalShipingCharge').val() == '-1' || $('#LocalShipingCharge').val() == 'undefined' || $('#LocalShipingCharge').val().length == '0') {
            $('span[data-valmsg-for="LocalShipingCharge"]').text($('#LocalShipingCharge').attr('data-val-required'));
            return callback("false");
        } else if ($('#ZonalShipingCharge').val() == '-1' || $('#ZonalShipingCharge').val() == undefined || $('#ZonalShipingCharge').val().length == '0') {
            $('span[data-valmsg-for="ZonalShipingCharge"]').text($('#ZonalShipingCharge').attr('data-val-required'));
            return callback("false");
        } else if ($('#NationalShippingCharge').val() == '-1' || $('#NationalShippingCharge').val() == undefined || $('#NationalShippingCharge').val().length == '0') {
            $('span[data-valmsg-for="NationalShippingCharge"]').text($('#NationalShippingCharge').attr('data-val-required'));
            return callback("false");
        } else if ($('#SalePrice').val() == '0' || $('#SalePrice').val() == undefined || $('#SalePrice').val().length == '0') {
            $('span[data-valmsg-for="SalePrice"]').text($('#SalePrice').attr('data-val-required'));
            return callback("false");
        } else if ($('#ListPrice').val() == '0' || $('#ListPrice').val() == undefined || $('#ListPrice').val().length == '0') {
            debugger;
            $('span[data-valmsg-for="ListPrice"]').text($('#ListPrice').attr('data-val-required'));
            return callback("false");
        }
        return callback(true);
    },
    ValidateProductStep3: function (callback) {
        if ($('#ProductLength').val() == '0' || $('#ProductLength').val() == 'undefined' || $('#ProductLength').val().length == '0') {
            $('span[data-valmsg-for="ProductLength"]').text($('#ProductLength').attr('data-val-required'));
            return callback("false");
        } else if ($('#ProductWidth').val() == '0' || $('#ProductWidth').val() == undefined || $('#ProductWidth').val().length == '0') {
            $('span[data-valmsg-for="ProductWidth"]').text($('#ProductWidth').attr('data-val-required'));
            return callback("false");
        } else if ($('#ProductHeight').val() == '-1' || $('#ProductHeight').val() == undefined || $('#ProductHeight').val().length == '0') {
            $('span[data-valmsg-for="ProductHeight"]').text($('#ProductHeight').attr('data-val-required'));
            return callback("false");
        } else if ($('#PackageLength').val() == '0' || $('#PackageLength').val() == undefined || $('#PackageLength').val().length == '0') {
            $('span[data-valmsg-for="PackageLength"]').text($('#PackageLength').attr('data-val-required'));
            return callback("false");
        } else if ($('#PackageWidth').val() == '0' || $('#PackageWidth').val() == undefined || $('#PackageWidth').val().length == '0') {
            $('span[data-valmsg-for="PackageWidth"]').text($('#PackageWidth').attr('data-val-required'));
            return callback("false");
        } else if ($('#PackageHeight').val() == '0' || $('#PackageHeight').val() == undefined || $('#PackageHeight').val().length == '0') {
            $('span[data-valmsg-for="PackageHeight"]').text($('#PackageHeight').attr('data-val-required'));
            return callback("false");
        } else if ($('#ProductWeight').val() == '0' || $('#ProductWeight').val() == undefined || $('#ProductWeight').val().length == '0') {
            $('span[data-valmsg-for="ProductWeight"]').text($('#ProductWeight').attr('data-val-required'));
            return callback("false");
        } else if ($('#PackageWeight').val() == '0' || $('#PackageWeight').val() == undefined || $('#PackageWeight').val().length == '0') {
            $('span[data-valmsg-for="PackageWeight"]').text($('#PackageWeight').attr('data-val-required'));
            return callback("false");
        }
        return callback(true);
    },
    ValidateProductStep4: function (callback) {
        debugger;
        if ($('#StockCount').val() == '0' || $('#StockCount').val() == undefined || $('#StockCount').val().length == '0') {
            $('span[data-valmsg-for="StockCount"]').text($('#StockCount').attr('data-val-required'));
            return callback("false");
        } else if ($('#Description').val() == '' || $('#Description').val() == undefined || $('#Description').val().length == '0' || $('#Description').val().length > '299') {
            $('span[data-valmsg-for="Description"]').text($('#Description').attr('data-val-required') + $('#Description').attr('data-val-maxlength'));
            return callback("false");
        }
        return callback(true);
    },
    ValidateProductStep5: function (callback) {
        debugger;
        if ($('#ReturnDays').val() == '0' || $('#ReturnDays').val() == undefined || $('#ReturnDays').val().length == '0') {
            $('span[data-valmsg-for="ReturnDays"]').text($('#ReturnDays').attr('data-val-required'));
            return callback("false");
        } else if ($('#EstimateDeliveryTime').val() == '' || $('#EstimateDeliveryTime').val() == undefined || $('#EstimateDeliveryTime').val().length == '0' || $('#Description').val().length > '299') {
            $('span[data-valmsg-for="EstimateDeliveryTime"]').text($('#EstimateDeliveryTime').attr('data-val-required'));
            return callback("false");
        }
        return callback(true);
    },

    CheckProductCode: function (productCode) {
        alert("CheckProductCode");
        common.ShowLoader();
        data = {
            "productCode": productCode,
        };
        ajax.doPostAjax(`/Vendor/Vendor/CheckProductCode`, data, function (result) {
            alert(result.ResultFlag);
            if (result.length > 0) {
                alert(result);
            }
            common.HideLoader();
        });
      
    }

}


