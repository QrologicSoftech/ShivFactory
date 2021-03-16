let previousValue = '',newValue=''; 
var productVarient = {
  
    AddNewTextBox: function (element) {
        var valofvar = $(element).parents("div .varientSection").children('div:nth-last-child(2)').children('input:nth-last-child(1)').val();
        if (valofvar != '' && valofvar != undefined) {
            $(`<div class="col-sm-2">
     <input type="text" class="form-control" placeholder="Variation " onkeyup="productVarient.AddBox(this)"></div>`).insertBefore($(element).parents('div .col-sm-2').siblings('div .col-sm-2').last());
        }

    },

    AddVarient: function (varient) {
        $(".inc").append(`<div class="row varientSection labelVarient">
            <label class= "col-sm-12 col-form-label varientName"  > ${ varient }</label ><span
"><a href="#" onclick="productVarient.RemoveVarient(this)">-</a></span>
        <div class="col-sm-2">
            <input name="lok" type="text" onkeyup="productVarient.AddBox(this)"  class="form-control"></div>
            <div class="col-sm-2">
                <a href="#" class="" onclick="productVarient.AddNewTextBox(this)">+</a>
            </div></div>`);
    },
    
    RemoveVarient: function () {
        jQuery(document).on('click', '.remove_this_varient', function () {
            jQuery(this).parent().remove();
            return false;
        });
    },

    GetAllValues: function () {
        var values = $(".row .varientSection input").map(function () {
                return $(this).val()
        }).get().join(",");
        console.log(values)
    },

    BindVarientDDL: function () {
        var label_values = $(".row .varientSection label").map(function () {
            return $(this).html().trim()
        }).get().join(",");
        var subCatId = $('#SubCategoryId').val();
        var ddlVarient = $('#Varient');
        var selectedVarient = ddlVarient.find(":selected").text().trim();
        if (ddlVarient.find(":selected").val() > 0) {
            common.ShowLoader();
            productVarient.AddVarient(selectedVarient);
            var data = { "SubcategoryId": subCatId, "varients": selectedVarient+","+label_values };
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

    AddBox: function (element) {
        var lastinputVal = $('input[name="lok"]').parents('div .col-sm-2').siblings('div .col-sm-2').last().prev().find('input').val()
        if (lastinputVal.length > 0) {
            productVarient.AddNewTextBox(element); 
        } else {
            alert("Please enter some value in variation");
        }
    },

    BindVariationToProductTbl: function () {
        var arrVariation = [];
        var label_values = $(".row .varientSection label").map(function () {
            return $(this).html().trim()
        }).get()
        arrVariation.push(label_values)
        console.log(arrVariation);

        $('#step1').css('display', 'none');
            //Build an array containing  Product Varient.
            var product = new Array();
        product.push(["Sr","Color", "Size", "ProductName"]);
        product.push(["", "", "", "Bedsheet"]);
        product.push(["","", "", "Bedsheet"]);
        product.push(["", "", "", "Bedsheet"]);
        product.push(["", "", "", "Bedsheet"]);

        
            var table = $("<table />");
            table[0].border = "1";

            //Get the count of columns.
        var columnCount = product[0].length;

            //Add the header row.
            var row = $(table[0].insertRow(-1));
            for (var i = 0; i < columnCount; i++) {
                var headerCell = $("<th />");
                headerCell.html(customers[0][i]);
                row.append(headerCell);
            }

            //Add the data rows.
            for (var i = 1; i < customers.length; i++) {
                row = $(table[0].insertRow(-1));
                for (var j = 0; j < columnCount; j++) {
                    var cell = $("<td />");
                    cell.html(customers[i][j]);
                    row.append(cell);
                }
            }

            var dvTable = $("#dvTable");
            dvTable.html("");
            dvTable.append(table);
       
    },

    }


    


        


