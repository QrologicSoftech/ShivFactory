var productVarient = {

    AddNewTextBox: function () {
        $("#append").click(function (e) {
            e.preventDefault();
            $(".inc").append('<div class="controls">\
                <input class="form-control" type="text" name="textbox" placeholder="textbox">\
                <input class="form-control" type="text" name="text" placeholder="text">\
                <a href="#" class="remove_this btn btn-danger" onclick="productVarient.RemoveTextBox()">remove</a>\
                <br>\
                <br>\
            </div>');
            return false;
        });
    },

    RemoveTextBox: function () {
        jQuery(document).on('click', '.remove_this', function () {
            jQuery(this).parent().remove();
            return false;
        });
    },

    SaveData: function () {

        $("input[type=submit]").click(function (e) {
            e.preventDefault();
            $(this).next("[name=textbox]")
                .val(
                    $.map($(".inc :text"), function (el) {
                        return el.value
                    }).join(",\n")
                )
        });
    },
    BindVarientDDL: function (varientddl_id) {
        var ddlVarient = $(varientddl_id);
      
        common.ShowLoader();
        var data = { "categoryId": 'subcatId',"varients":'' };
        ddlVarient
            .empty()
            .append('<option selected="selected" value="-1">Select</option>');
        ajax.doPostAjax(`/Vendor/Vendor/GetVarientDdlByCategoryId`, data, function (result) {
            if (result.length > 0) {
                $.each(result, function (Value, Text) {
                    ddlSubCategory.append($('<option></option>').val(Text.Value).html(Text.Text));
                });
            }
            common.HideLoader(subcat_id);
        });
    },


}
        


