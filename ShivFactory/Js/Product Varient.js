let previousValue = '',newValue=''; 
var productVarient = {
    AddNewTextBox: function () {
        $(".inc").append('<div class="col-lg-4 col-sm-4" > New Varient Value <br>\
                <input class="controls" type="text" name="textbox" placeholder="textbox">\
          <a id="append" name="append" href="#" onclick="productVarient.AddNewTextBox()">Add New Box</a> &nbsp; &nbsp; &nbsp; <a href="#" class="remove_this_varient" onclick="productVarient.RemoveTextBox()">remove</a>\
                <br>\
            </div>');
        return false;
    },

    AddVarient: function (varient) {
        $(".inc").append('<div class="row">\
            <div class="col-lg-2 col-sm-2">\
                                                <div class="form-group">\
                                                   <p><b>"'+ varient+'"</b><p>\
                                                </div>\
                                                <div class="form-group">\
                                                    \
                                                    \
                                                </div>\
                                            </div>\
            <div class= "col-lg-2 col-sm-2" > <div class="form-group">\
                                                    <label>Value 1 </label><enum><b>*</b></enum>\
                                                </div>\
                                                <div class="form-group">\
                                                    <input autocomplete="off" class="form-control"  name="textbox" type="text" value="">\
                                                    <span class=""></span>\
                                                </div>\
                                            </div>\
        <div class= "col-lg-2 col-sm-2" >\
                                                <div class="form-group">\
                                                    <label>Value 2</label><enum><b>*</b></enum>\
                                                </div>\
                                                <div class="form-group">\
        <input class= "form-control"  name = "textbox"  type = "text" value = "" >\
                                                </div >\
                                            </div >\
<div class="col-lg-2 col-sm-2">\
                                                <div class="form-group">\
                                                    <label>Value 3 </label><enum><b>*</b></enum>\
                                                </div>\
                                                <div class="form-group">\
                                                    <input autocomplete="off" class="form-control"  name="textbox" type="text" value="">\
                                                    \
                                                </div>\
                                            </div>\
            <div class="col-lg-2 col-sm-2">\
                                                <div class="form-group">\
                                                    <label>Action </label><enum><b>*</b></enum>\
                                                </div>\
                                                <div class="form-group">\
                                                   \
                                                    \
                                                </div>\
                                            </div>\<a href="#" class="remove_this_varient" onclick="productVarient.RemoveVarient()">remove</a>\
                                     </div>')
        return true;
    },

    RemoveTextBox: function () {
        jQuery(document).on('click', '.remove_this', function () {
            jQuery(this).parent().remove();
            return false;
        });
    },

    RemoveVarient: function () {
        jQuery(document).on('click', '.remove_this_varient', function () {
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
    BindVarientDDL: function () {
        var subCatId = $('#SubCategoryId').val();
        var ddlVarient = $('#Varient');
        var selectedVarient = ddlVarient.find(":selected").text();
        //newValue = previousValue.concat(selectedVarient);
        common.ShowLoader();
        productVarient.AddVarient(selectedVarient);
        var data = { "SubcategoryId": subCatId, "varients": selectedVarient };
        ddlVarient.empty();
        ddlVarient.append('<option selected="selected" value="-1">Select</option>');
        ajax.doPostAjax(`/Vendor/Vendor/GetVarientDdlByCategoryId`, data, function (result) {
            if (result.ResultFlag == true) {
                varient = result.Data;
                $.each(varient, function (Value, varient) {
                    ddlVarient.append($('<option></option>').val(varient.Value).html(varient.Text));
                });
            }
            common.HideLoader();
        });
    },
    }


    


        


