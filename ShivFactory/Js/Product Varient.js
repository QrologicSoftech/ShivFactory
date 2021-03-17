let quentity = '',newValue=''; 
var productVarient = {
  
    AddNewTextBox: function (element) {
        var valofvar = $(element).parents("div .varientSection").children('div:nth-last-child(2)').children('input:nth-last-child(1)').val();
        if (valofvar != '' && valofvar != undefined) {
            $(`<div class="col-sm-2">
     <input type="text" class="form-control" placeholder="Variation " onkeyup="productVarient.AddBox(this)"></div>`).insertBefore($(element).parents('div .col-sm-2').siblings('div .col-sm-2').last());
        }

    },

    AddVarient: function (varient) {
        $(".inc").append(`<div id="${varient}" class="row varientSection labelVarient">
            <label class= "col-sm-12 col-form-label varientName"  > ${ varient }</label ><span
"><a href="#" onclick="productVarient.RemoveVarient(this)">-</a></span>
        <div class="col-sm-2">
            <input name="lok" type="text" onkeyup="productVarient.AddBox(this)"  class="form-control"></div>
            <div class="col-sm-2">
                <a href="#" class=""></a>
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
        }
    },

    BindVariationToProductTbl: function () {
        var map = {};
        var nameArr = []; 
        var QtyArr = []; 
        var spArr = []; 
        var lpArr = []; 
     
       
        var selectedVarient = $(".row .varientSection label").map(function () {
            return $(this).html().trim()
        }).get().join(",");
         //add n number of product based on variation in  table 
        var product = new Array();
        var label_values = $(".row .varientSection label").map(function () {
            return $(this).html().trim()
        }).get()
        // map each varient with all values and product description 
        // here put condition for limitation of second varient input must ot ge more than first input boxes. 
        for (var r = 0; r < label_values.length; r++) {
            var MaxProductNumber = $("#" + label_values[r] + "").children('div .col-sm-2').find('input');
            var varientVal = MaxProductNumber.map(function () {
                return $(this).val().trim()
            }).get(); //.join(",")
            map[label_values[r]] = varientVal;
            //alert(label_values[r] + " qqqq " + varientVal);
        }
        //set table header name 
        product.push(label_values);
        product[0].push("Name"); 
        product[0].push("Quantity"); 
        product[0].push("SalePrice"); 
        product[0].push("ListPrice"); 
        

           // put here vale from product model 
        nameArr = new Array(product[0].length);
        QtyArr = new Array(product[0].length);
        spArr = new Array(product[0].length);
        lpArr = new Array(product[0].length);
     
        nameArr.push("BedSheet");
        QtyArr.push("12");
        spArr.push("123");
        lpArr.push("159");
        //copyWithin is done to copy all elements upto header length. 
       
        nameArr.copyWithin(0, product[0].length);   
        QtyArr.copyWithin(0, product[0].length);
        spArr.copyWithin(0, product[0].length);
        lpArr.copyWithin(0, product[0].length);
        
        map["Quantity"] = QtyArr;//["12"];
        map["SalePrice"] = spArr;//["123"];
        map["ListPrice"] = lpArr; //["1234"];
        console.log(map);
        console.log(Object.keys(map));
        console.log(Object.values(map));
       // $('#step1').css('display', 'none');
        
            //Build an grid of  n number of Product Varient.
        var MaxProductNumber = $("#" + label_values[0] + "").children('div .col-sm-2').find('input');
    
        var mapKey = Object.keys(map);
        var mapValues = Object.values(map);
       // console.log("aaa " + map[mapKey[0]][0]);
        for (var k = 0; k < MaxProductNumber.length; k++) {
            var arrProduct_Row_Value = new Array();
            for (var key = 0; key < mapKey.length; key++) {
                if (map[mapKey[key]][k] == undefined) {
                    arrProduct_Row_Value.push(mapValues[key][0]);
                } else {
                    arrProduct_Row_Value.push(map[mapKey[key]][k]);  //  = [k, "Red1", "Product1", "2", "12"];
                }
            }
            product.push(arrProduct_Row_Value);
        }
        var table = $("<table id='tblProduct' class='table datatable dataTable no-footer'  cellspacing='0' width='100 %' />");
      
            //Get the count of columns.
        var columnCount = product[0].length;
        console.log(product);
            //Add the header row.
        var row = $(table[0].insertRow(-1));
        row.css('class', 'thead-light bg-primary text-white');
            for (var i = 0; i < columnCount; i++) {
                var headerCell = $("<th />");
                headerCell.html(product[0][i]);
                row.append(headerCell);
            }

        //Add the data rows.
        for (var i = 1; i < product.length; i++) {
                row = $(table[0].insertRow(-1));
                for (var j = 0; j < columnCount; j++) {
                    var cell = $("<td />");
                    cell.html("<input type='text' class='form-control' value="+product[i][j]+" />")
                    row.append(cell);
                }
            }

            var dvTable = $("#dvTable");
            dvTable.html("");
        dvTable.append(table);


       
    },
    TableToJSON: function (table) {
        //table = $('#tblProduct')
        //var jsonString = { "Rows": [] };
        //var $th = $(table).find('th');
        //$(table).find('tbody tr').each(function (i, tr) {
        //    if (i > 0) {
        //        var obj = {};
        //        $tds = $(tr).find('td').find('input');
        //        $th.each(function (index, th) {
        //            obj[$(th).text()] = $tds.eq(index).val();
        //        });
        //        jsonString.Rows.push(obj);
        //    }
        //});
        //console.log(JSON.stringify(jsonString));
        //productVarient.SaveData(JSON.stringify(jsonString));
        table = $('#tblProduct')
        var jsonString = [];
        var $th = $(table).find('th');
        $(table).find('tbody tr').each(function (i, tr) {
            if (i > 0) {
                var obj = {};
                $tds = $(tr).find('td').find('input');
                $th.each(function (index, th) {
                    //let obj = {
                    //    "VarientName1": $(th).text(),
                    //    "VarientValue1": $tds.eq(index).val(),
                    //    if()
                    //};
                    if ($(th).text().toLowerCase() == 'quantity') {

                    }
                    else {
                        obj[`VarientName${index}`] = $(th).text();
                        obj[`VarientValue${index}`] = $tds.eq(index).val();
                    }
                });
                jsonString.push(obj);
            }
        });
        console.log(JSON.stringify(jsonString));
        productVarient.SaveData(JSON.stringify(jsonString));
    },

    SaveData: function (jsonString) {
        common.ShowLoader();
        // var data = { "Product": jsonString};
        debugger; 
        var data = {"Rows" : jsonString };
        ajax.doPostAjax(`/Vendor/Vendor/SaveProductVarients`, jsonString, function (result) {
                if (result.ResultFlag == true) {
                
                }
                common.HideLoader();
            });
        

    },
   
    }


    


        


