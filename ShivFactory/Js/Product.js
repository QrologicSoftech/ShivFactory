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
        if (stepId.toLowerCase() == 'step1') {
            product.ValidateProductStep1(function (res) {
                result = res;
                alert(res);
                if (res == true) {
                    return callback(true);
                } else {
                    return false;;
                }
            });
        }
        
       // return callback(result);
    },
    ValidateProductStep1: function (callback) {
        alert($('#Category').find(":selected").val());
        if ($('#Category').find(":selected").val() == '-1' || $('#Category').find(":selected").val() == 'undefined' || $('#Category').find(":selected").val().length == '0')
        {
            alert("select cat");
            $('#valforcat').html("Select Category!");
            return callback("false");
        }
        return callback("true");
    }

}


