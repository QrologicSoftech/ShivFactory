var product = {

    BindSubCategoryByCatId: function (cat_id, subcat_id, mini_id) {
        var selectedCatId = $(cat_id + " :selected").attr('value');
        common.ShowLoader(subcat_id);
        var ddlSubCategory = $(subcat_id);
        var data = { "categoryId": selectedCatId };
        ajax.doPostAjax(`/Vendor/Vendor/GetSubcategoryByCategoryId`, data, function (result) {
            ddlSubCategory.find('option')
                .remove()
                .end()
            if (typeof result !== 'undefined' && result.length > 0) {
            $.each(result, function (Value, Text) {
                ddlSubCategory.append($('<option></option>').val(Text.Value).html(Text.Text));
                });
                common.HideLoader(subcat_id);
            } else {
                ddlSubCategory.append($('<option></option>').val('-1').html('Selelct'));
            }
            });
    },
    BindMiniCategoryBySubCatId: function (subcat_id, mini_id) {
        //alert("Cat Id" + $(subcat_id + " :selected").attr('value'))
        var selectedSubCatId = $(subcat_id + " :selected").attr('value');
        common.ShowLoader(mini_id);
        var ddlMiniCategory = $(mini_id);
        data = { "subcategoryId": selectedSubCatId }
        ajax.doPostAjax(`/Vendor/Vendor/GetMinicategoryBySubCategoryId`, data, function (result) {
            ddlMiniCategory.find('option')
                .remove()
                .end()
            if (typeof result !== 'undefined' && result.length > 0) {
                $.each(result, function (Value, Text) {
                ddlMiniCategory.append($('<option></option>').val(Text.Value).html(Text.Text));
            });
            common.HideLoader(mini_id);
            } else {
                ddlMiniCategory.append($('<option></option>').val('-1').html('Select'));
            }
            });
    
    },

}


