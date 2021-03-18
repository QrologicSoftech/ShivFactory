var quantity = 'Quantity', listPrice = 'ListPrice',salePrice = 'SalePrice'; 
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
"><a href="#" onclick="$(this).parent().parent().remove();">X</a></span>
        <div class="col-sm-2">
            <input name="lok" type="text" onkeyup="productVarient.AddBox(this)"  class="form-control"></div>
            <div class="col-sm-2">
                <a href="#" class=""></a>
            </div></div>`);
    },
    
    GetAllValues: function () {
        var values = $(".row .varientSection input").map(function () {
                return $(this).val()
        }).get().join(",");
        console.log(values)
    },

    BindVarientDDL: function () {
        $('#btnStep1').show(); 
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
        //var lastinputVal = $('input[name="lok"]').parents('div .col-sm-2').siblings('div .col-sm-2').last().prev().find('input').val()
        var lastinputVal = $(element).parents('div .col-sm-2').siblings('div .col-sm-2').last().prev().find('input').val()
        if (lastinputVal.length > 0) {
            productVarient.AddNewTextBox(element); 
        } else {
        }
    },

    BindVariationToProductTbl: function () {
        $('#step2').show(); 
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
        //product[0].push("Name"); 
        product[0].push(quantity);
        product[0].push(salePrice);
        product[0].push(listPrice); 
        

           // put here vale from product model 
       // nameArr = new Array(product[0].length);
        QtyArr = new Array(product[0].length);
        spArr = new Array(product[0].length);
        lpArr = new Array(product[0].length);
     
      //  nameArr.push("BedSheet");
        QtyArr.push($('#ProductQty').val());
        spArr.push($('#SalePrice').val());
        lpArr.push($('#ListPrice').val());
        //copyWithin is done to copy all elements upto header length. 
       
        //nameArr.copyWithin(0, product[0].length);   
        QtyArr.copyWithin(0, product[0].length);
        spArr.copyWithin(0, product[0].length);
        lpArr.copyWithin(0, product[0].length);
        
        map[quantity] = QtyArr;
        map[salePrice] = spArr;
        map[listPrice] = lpArr; 

        console.log(map);
        console.log(Object.keys(map));
        console.log(Object.values(map));
        $('#step1').css('display', 'none');
        
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
                   
                    if (product[0][j] == quantity || product[0][j] == salePrice || product[0][j] == listPrice) {
                        cell.html("<input type='number' class='form-control' value=" + product[i][j] + " />")
                    } else {
                        cell.html(product[i][j]);
                   //     cell.html("<label class='form-control'>" + product[i][j] + " />")
                    }
                    row.append(cell);
                }
            }

            var dvTable = $("#dvTable");
            dvTable.html("");
        dvTable.append(table);


       
    },
    TableToJSON: function (table) {
        table = $('#tblProduct')
        var jsonString = [];
        var $th = $(table).find('th');
        $(table).find('tbody tr').each(function (i, tr) {
            if (i > 0) {
                let obj = {};
                $tds = $(tr).find('td').find('input');
                $tdvarientValue = $(tr).find('td');
                $th.each(function (index, th) {
                    obj["ProductId"] = $("#ProductId").val();

                    if ($(th).text() == quantity) {
                        obj["ProductQty"] = $tds.eq(0).val();
                    }
                    else if ($(th).text() == salePrice) {
                        obj["SalePrice"] = $tds.eq(1).val();
                    }
                    else if ($(th).text() == listPrice) {
                        obj["ListPrice"] = $tds.eq(2).val();
                    }
                    else {
                        obj[`VarientName${index + 1}`] = $(th).text();
                        obj[`VarientValue${index + 1}`] = $tdvarientValue.eq(index).html();
                    }
                });
                jsonString.push(obj);
                console.log(JSON.stringify(obj));
            }
            console.log(JSON.stringify(jsonString));
        });
        //console.log(JSON.stringify(jsonString));
        productVarient.SaveData(JSON.stringify(jsonString));
    },

    SaveData: function (jsonString) {
        common.ShowLoader();
        var data = { "Rows": jsonString};
       
        //var data = [
        //    { "VarientName1": "Color", "VarientValue1": "Red", "VarientName2": "Size", "VarientValue2": "M", "ProductQty": "15", "SalePrice": "15300", "ListPrice": "12300" }
            
        //];
        ajax.doPostAjax(`/Vendor/Vendor/SaveProductVarients`, data, function (result) {
                if (result.ResultFlag == true) {
                
                }
                common.HideLoader();
            });
        

    },
   
    }


    


        


