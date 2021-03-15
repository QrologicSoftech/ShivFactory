let previousValue = '',newValue=''; 
var productVarient = {
    AddNewTextBox: function (element) {
       
        var varientval = $(element).parent('div .row').children('input').last().val();
        debugger;
        jQuery(document).on('click', '.add_this_varient', function () {
            jQuery(this).parent().parent().parent().parent().append('<div class="col-lg-2 col-sm-2" >\
             <div class="form-group"><input class="form-control" type="text" name="textbox" placeholder="textbox">\
           <span >  <a id="append" name="append" class="add_this_varient" href="#" onclick="productVarient.AddNewTextBox()">Add New Box</a> &nbsp; &nbsp; &nbsp; <a href="#" class="remove_this_varient" onclick="productVarient.RemoveTextBox()">remove</a></span>\
                 </div>\
            </div>');
            return false;
        });
    },

    AddVarient: function (varient) {
        $(".inc").append('<div class="row"><div class="col-lg-2 col-sm-2">\
                                                <h2>\
                                                   <p><b>"'+ varient+'"</b><p>\
                                                </h2>\
                                            </div>\
            <div class= "col-lg-2 col-sm-2" >  <div class="form-group">\
                                                    <input autocomplete="off" class="form-control" id="te"  name="textbox" type="text" value="">\
                                                </div>\
                                            </div>\
              <span><a href="#" class="add_this_varient" onclick="productVarient.AddNewTextBox(this)">+</a> </span>\
        \<a href="#" class="remove_this_varient" onclick="productVarient.RemoveVarient(this)">Delete this Varient for Product</a>\
                                     </div>')
        return true;
    },

    RemoveTextBox: function () {
        debugger;
        jQuery(document).on('click', '.remove_this', function () {
            jQuery(this).parent().parent().parent().remove();
            return false;
        });
    },

    RemoveVarient: function () {
        jQuery(document).on('click', '.remove_this_varient', function () {
            jQuery(this).parent().remove();
            return false;
        });
    },

    GetAllValues: function () {
        var values = $(".row input").map(function () {
                return $(this).val()
        }).get().join(",");
        alert("Entered Varient is ==> " + values);
            console.log(values)
     
    },
    BindVarientDDL: function () {
        var subCatId = $('#SubCategoryId').val();
        var ddlVarient = $('#Varient');
        var selectedVarient = ddlVarient.find(":selected").text();
        if (ddlVarient.find(":selected").val() > 0) {
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
        }
    },
    }


    


        


