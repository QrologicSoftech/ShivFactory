var product = {

    BindSubCategoryByCatId: function (cat_id, subcat_id, mini_id) {
        var ddlSubCategory = $(subcat_id);
        var ddlminiCategory = $(mini_id);
        var selectedCatId = $(cat_id + " :selected").attr('value');
        //if (! typeof selectedCatId === "undefined") {
        common.ShowLoader(subcat_id);
        var data = { "categoryId": selectedCatId };
        ddlSubCategory.empty().append('<option selected="selected" value="-1">Select</option>');
        ddlminiCategory.empty().append('<option selected="selected" value="-1">Select</option>');

        ajax.doPostAjax(`/Vendor/Vendor/GetSubcategoryByCategoryId`, data, function (result) {
            common.HideLoader(subcat_id);
            if (result.length > 0) {
                $.each(result, function (Value, Text) {
                    ddlSubCategory.append($('<option></option>').val(Text.Value).html(Text.Text));
                });
            }
        });
    },
    BindMiniCategoryBySubCatId: function (subcat_id, mini_id) {
        var selectedSubCatId = $(subcat_id + " :selected").attr('value');
        if (selectedSubCatId > 0) {
            common.ShowLoader(mini_id);
            var ddlMiniCategory = $(mini_id);
            ddlMiniCategory.empty().append('<option selected="selected" value="-1">Select</option>');
            data = { "subcategoryId": selectedSubCatId }
            ajax.doPostAjax(`/Vendor/Vendor/GetMinicategoryBySubCategoryId`, data, function (result) {
                common.HideLoader(mini_id);
                if (result.length > 0) {
                    $.each(result, function (Value, Text) {
                        ddlMiniCategory.append($('<option></option>').val(Text.Value).html(Text.Text));
                    });
                }
            });
        }
    },
    NextLevel: function (stepId, callback) {
        let result = true;

        if (stepId.toLowerCase() == 'step1') {
            product.ValidateProductStep1(function (res) {
                result = res;
            });
        } else if (stepId.toLowerCase() == 'step2') {
            product.ValidateProductStep2(function (res) {
                result = res;
            });
        } else if (stepId.toLowerCase() == 'step3') {
            product.ValidateProductStep3(function (res) {
                result = res;
            });
        } else if (stepId.toLowerCase() == 'step4') {
            product.ValidateProductStep4(function (res) {
                result = res;
            });
        } else if (stepId.toLowerCase() == 'step5') {
            product.ValidateProductStep5(function (res) {
                result = res;
            });
        }

         return callback(result);
    },
    ValidateProductStep1: function (callback) {
        if ($('#Category').find(":selected").val() == '-1' || $('#Category').find(":selected").val() == 'undefined' || $('#Category').find(":selected").val().length == '0') {
            $('span[data-valmsg-for="CategoryId"]').text($('#Category').attr('data-val-required'));
            return callback("false");
        }
        else if ($('#SubCategory').find(":selected").val() == '-1' || $('#SubCategory').find(":selected").val() == undefined || $('#SubCategory').find(":selected").val().length == '0') {
            $('span[data-valmsg-for="SubCategoryId"]').text($('#SubCategory').attr('data-val-required'));
            return callback("false");
        }
        //else if ($('#MiniCategory').find(":selected").val() == '-1' || $('#MiniCategory').find(":selected").val() == undefined || $('#MiniCategory').find(":selected").val().length == '0') {
        //    $('span[data-valmsg-for="MiniCategoryId"]').text($('#MiniCategory').attr('data-val-required'));
        //    return callback("false");
        //}
        else if ($('#ProductName').val() == '-1' || $('#ProductName').val() == undefined || $('#ProductName').val().length == '0') {
            $('span[data-valmsg-for="ProductName"]').text($('#ProductName').attr('data-val-required'));
            return callback("false");
        } else if ($('#ProductCode').val() == undefined || $('#ProductCode').val().length == '0') {
            $('span[data-valmsg-for="ProductCode"]').text($('#ProductCode').attr('data-val-required'));
            return callback("false");
        }
        if ($('#ProductCode').val()) {
            ajax.doPostAjax(`/${vendorArea}/${vendorController}/CheckProductCode`, {
                "productCode": $('#ProductCode').val()
            }, function (result) {
                if (result.ResultFlag) {
                    callback(false);
                }
            });
        }        
        return callback(true);
    },
    ValidateProductStep2: function (callback) {
        if ($('#LocalShipingCharge').val() == 'undefined' || $('#LocalShipingCharge').val()== '0') {
            $('span[data-valmsg-for="LocalShipingCharge"]').text($('#LocalShipingCharge').attr('data-val-required'));
            return callback("false");
        } else if ($('#ZonalShipingCharge').val() == undefined || $('#ZonalShipingCharge').val() == '0') {
            $('span[data-valmsg-for="ZonalShipingCharge"]').text($('#ZonalShipingCharge').attr('data-val-required'));
            return callback("false");
        } else if ($('#NationalShippingCharge').val() == undefined || $('#NationalShippingCharge').val()== '0') {
            $('span[data-valmsg-for="NationalShippingCharge"]').text($('#NationalShippingCharge').attr('data-val-required'));
            return callback("false");
        } else if ($('#SalePrice').val() == undefined || $('#SalePrice').val() == '0') {
            $('span[data-valmsg-for="SalePrice"]').text($('#SalePrice').attr('data-val-required'));
            return callback("false");
        } else if ($('#ListPrice').val() == undefined || $('#ListPrice').val() == '0') {
            $('span[data-valmsg-for="ListPrice"]').text($('#ListPrice').attr('data-val-required'));
            return callback("false");
        }
        return callback(true);
    },
    ValidateProductStep3: function (callback) {
        return callback(true);
    },
    ValidateProductStep4: function (callback) {
       
        if ($('#StockCount').val() == '0' || $('#StockCount').val() == undefined || $('#StockCount').val().length == '0') {
            $('span[data-valmsg-for="StockCount"]').text($('#StockCount').attr('data-val-required'));
            return callback("false");
        } else if ($('#Description').val() == '' || $('#Description').val() == undefined || $('#Description').val().length == '0' || $('#Description').val().length > '299') {
            $('span[data-valmsg-for="Description"]').text($('#Description').attr('data-val-maxlength'));
            return callback("false");
        }
        return callback(true);
    },
    ValidateProductStep5: function (callback) {
         if ($('#ReturnDays').val() == '0' || $('#ReturnDays').val() == undefined) {
            $('span[data-valmsg-for="ReturnDays"]').text($('#ReturnDays').attr('data-val-required'));
            return callback("false");
        } else if (!$('#EstimateDeliveryTime').val() || $('#EstimateDeliveryTime').val() == undefined) {
            $('span[data-valmsg-for="EstimateDeliveryTime"]').text($('#EstimateDeliveryTime').attr('data-val-required'));
            return callback("false");
        }
        return callback(true);
    },

    CheckProductCode: function (productCode) {
        data = {
            "productCode": productCode,
        };
        ajax.doPostAjax(`/${vendorArea}/${vendorController}/CheckProductCode`, new {
            "productCode": productCode,
        }, function (result) {
            if (result.length > 0) {
                alert(result);
            }
        });

    }

}


