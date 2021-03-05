var product = {

    BindSubCategoryByCatId: function (cat_id, subcat_id, mini_id) {

        var ddlSubCategory = $(subcat_id);
        var ddlminiCategory = $(mini_id);
        var selectedCatId = $(cat_id + " :selected").attr('value');
        //if (! typeof selectedCatId === "undefined") {
        common.ShowLoader(subcat_id);
        var data = { "categoryId": selectedCatId };
        ddlSubCategory.find('option')
            .remove()
            .end()
        ddlSubCategory.append($('<option></option>').val('-1').html('Select'));

        ddlminiCategory.find('option')
            .remove()
            .end()
        ddlminiCategory.append($('<option></option>').val('-1').html('Select'));
        ajax.doPostAjax(`/Vendor/Vendor/GetSubcategoryByCategoryId`, data, function (result) {
            if (result.length > 0) {
                $.each(result, function (Value, Text) {
                    ddlSubCategory.append($('<option></option>').val(Text.Value).html(Text.Text));
                });
                common.HideLoader(subcat_id);
            }
        });
    },
    BindMiniCategoryBySubCatId: function (subcat_id, mini_id) {
        var selectedSubCatId = $(subcat_id + " :selected").attr('value');
        if (selectedSubCatId > 0) {
            common.ShowLoader(mini_id);
            var ddlMiniCategory = $(mini_id);
            data = { "subcategoryId": selectedSubCatId }
            ajax.doPostAjax(`/Vendor/Vendor/GetMinicategoryBySubCategoryId`, data, function (result) {
                ddlMiniCategory.find('option')
                    .remove()
                    .end()
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
            });
        }
        
        return callback(result);
    },
    ValidateProductStep1: function (callback) {
        return callback("true");
    }

}


