﻿var quantity = 'Quantity', listPrice = 'ListPrice', salePrice = 'SalePrice', image = 'Image';  

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
"><a href="#" onclick="$(this).parent().parent().remove();" style="COLOR: RED;">X</a></span>
        <div class="col-sm-2">
            <input name="lok" type="text" onkeyup="productVarient.AddBox(this)"  class="form-control"></div>
            <div class="col-sm-2">
                <a href="#" class=""></a>
            </div></div>`);
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
        var maxinputVal = $('.inc').children('div .varientSection:first-child').children('div .col-sm-2');
        var divinputVal = $(element).parent().parent('div .varientSection').children('div .col-sm-2');
        if (divinputVal.length-1 > maxinputVal.length) {
            return false;
        } else {
            var lastinputVal = $(element).parents('div .col-sm-2').siblings('div .col-sm-2').last().prev().find('input').val()
            if (lastinputVal.length > 0) {
                productVarient.AddNewTextBox(element);
            } else {
            }
        }
    },

    BindVariationToProductTbl: function () {
        $('#step2').show(); 
        var map = {};
        var QtyArr = []; 
        var spArr = []; 
        var lpArr = []; 
        var imgArr = []; 
     
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
        }
        //set table header name 
        product.push(label_values);
        product[0].push(quantity);
        product[0].push(salePrice);
        product[0].push(listPrice);
        product[0].push(image);
        
        QtyArr = new Array(product[0].length);
        spArr = new Array(product[0].length);
        lpArr = new Array(product[0].length);
        imgArr = new Array(product[0].length);
     
        QtyArr.push($('#ProductQty').val());
        spArr.push($('#SalePrice').val());
        lpArr.push($('#ListPrice').val());
        imgArr.push($('#Image1').val());
       
        QtyArr.copyWithin(0, product[0].length);
        spArr.copyWithin(0, product[0].length);
        lpArr.copyWithin(0, product[0].length);
        imgArr.copyWithin(0, product[0].length);
        map[quantity] = QtyArr;
        map[salePrice] = spArr;
        map[listPrice] = lpArr;
        map[image] = imgArr; 


        $('#step1').css('display', 'none');
        
            //Build an grid of  n number of Product Varient.
        var MaxProductNumber = $("#" + label_values[0] + "").children('div .col-sm-2').find('input');
    
        var mapKey = Object.keys(map);
        var mapValues = Object.values(map);
      
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
        
        for (var i = 1; i <product.length-1; i++) {
                row = $(table[0].insertRow(-1));
                for (var j = 0; j < columnCount; j++) {
                    var cell = $("<td />");
                   
                    if (product[0][j] == quantity || product[0][j] == salePrice || product[0][j] == listPrice) {
                        cell.html("<input type='number' class='form-control' value=" + product[i][j] + " />")
                    } else if (product[0][j] == image) {
                        cell.html("<button type='button' onclick='productVarient.BindVarientImagePopup(this)' class='btn btn-primary'>View</button>&nbsp<button onclick='productVarient.UploadVarientsImage(this)' type='button' class='btn btn-primary'>Upload</button>\
        <input id = 'Image1' name = 'Image1' type = 'hidden' > <input id='Image2' name='Image2' type='hidden' ><input id='Image3' name='Image3' type='hidden' ><input id='Image4' name='Image4' type='hidden'><input id='Image5' name='Image5' type='hidden'><input id='Image6' name='Image6' type='hidden'>")
                    } else {
                        cell.html(product[i][j]);
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
                    else if ($(th).text() == image) {
                        
                    }
                    else {
                        obj["Image1"] = $tds.eq(3).val();
                        obj["Image2"] = $tds.eq(4).val();
                        obj["Image3"] = $tds.eq(5).val();
                        obj["Image4"] = $tds.eq(6).val();
                        obj["Image5"] = $tds.eq(7).val();
                        obj["Image6"] = $tds.eq(8).val();
                            obj[`VarientName${index + 1}`] = $(th).text();
                            obj[`VarientValue${index + 1}`] = $tdvarientValue.eq(index).html();
                    }
                });
                jsonString.push(obj);
                console.log(JSON.stringify(obj));
            }
            console.log(JSON.stringify(jsonString));
        });
        productVarient.SaveData(JSON.stringify(jsonString));
    },

    SaveData: function (jsonString) {
        common.ShowLoader();
        var data = { "Rows": jsonString};
        ajax.doPostAjax(`/Vendor/Vendor/SaveProductVarients`, data, function (result) {
            common.HideLoader();
            if (result.ResultFlag == true) {
                location.replace("/Vendor/Vendor/Product");
            } 
           
            common.ShowMessage(result);
            });
        

    },

    UploadVarientsImage: function (element) {
        common.ShowLoader();
        var form;
        var index = $(element).parent('td').parent('tr').index();
        form = `  <h4><b>Product Images</b> </h4>
                                        <p></p>
                                        <div class="row">
                                            <div class="col-lg-4 col-sm-4">
                                                <div class="form-group">
                                                    <label>Main Image</label><enum><b>*</b></enum>
                                                    <input type="file" name="img[]" class="file-upload-default">
                                                </div>
                                                <div class="ajax-file-upload-container form-group">
                                                    <input accept="image/*" class="form-control file-upload-info" id="PostedFile" name="PostedFile" onchange="common.readfile(this,'imagePreview')" type="file" value="">
                                                    <span Id="imgspan" style = "color:Red; display:none"> Please Upload Main image of varient</span>
                                                </div>

                                                <div class="form-group">

                                                    <img id="imagePreview" src="" class="img-preview img-preview-sm img-thumbnail">

                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-sm-4">
                                                <div class="form-group">
                                                    <label>Image1</label>
                                        <input required type="file" name="img1[]" class="file-upload-default">

                                                </div>

                                                <div class="ajax-file-upload-container form-group">
                                                    <input accept="image/*" class="form-control file-upload-info" id="files" name="files" onchange="common.readfile(this,'imagePreview1')" type="file" value="" required>
                                                    <span Id="imgspan1" style = "color:Red; display:none"> Please Upload Image</span>
                                                </div>

                                                <div class="form-group">

                                                    <img id="imagePreview1" src="" class="img-preview img-preview-sm img-thumbnail">
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-sm-4">
                                                <div class="form-group">
                                                    <label>Image 2</label>
<input required type="file" name="img2[]" class="file-upload-default">
                                                </div>
                                                <div class="ajax-file-upload-container form-group">
                                                    <input  accept="image/*" class="form-control file-upload-info" id="files" name="files" onchange="common.readfile(this,'imagePreview2')" type="file" value="">
                                                    <span Id="imgspan2" style = "color:Red; display:none"> Please Upload Image</span>
                                                </div>

                                                <div class="form-group">

                                                    <img id="imagePreview2" src="" class="img-preview img-preview-sm img-thumbnail">
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-lg-4 col-sm-4">
                                                <div class="form-group">
                                                    <label>Image 3</label>
                                                    <input required type="file" name="img3[]" class="file-upload-default">
                                                </div>
                                                <div class="ajax-file-upload-container form-group">
                                                    <input  accept="image/*" class="form-control file-upload-info" id="files" name="files" onchange="common.readfile(this,'imagePreview3')" type="file" value="">
                                                    <span Id="imgspan3" style = "color:Red; display:none"> Please Upload Image</span>
                                                </div>
                                                <div class="form-group">

                                                    <img id="imagePreview3" src="" class="img-preview img-preview-sm img-thumbnail" >
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-sm-4">
                                                <div class="form-group">
                                                    <label>Image 4</label>
                                                    <input required type="file" name="img4[]" class="file-upload-default">
                                                </div>
                                                <div class="ajax-file-upload-container form-group">
                                                    <input accept="image/*" class="form-control file-upload-info" id="files" name="files" onchange="common.readfile(this,'imagePreview4')" type="file" value="">
                                                    <span Id="imgspan4" style = "color:Red; display:none"> Please Upload Image</span>
                                                </div>


                                                <div class="form-group">

                                                    <img id="imagePreview4" src="" class="img-preview img-preview-sm img-thumbnail" >
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-sm-4">
                                                <div class="form-group">
                                                    <label>Image 5</label>
                                                    <input required type="file" name="img5[]" class="file-upload-default">
                                                </div>
                                                <div class="ajax-file-upload-container form-group">
                                                    <input accept="image/*" class="form-control file-upload-info" id="files" name="files" onchange="common.readfile(this,'imagePreview5')" type="file" value="">
<span Id="imgspan5" style = "color:Red; display:none"> Please Upload Image</span>                                               
</div>

                                                <div class="form-group">

                                                    <img id="imagePreview5" src="" class="img-preview img-preview-sm img-thumbnail">
                                                </div>
                                            </div>

                                        </div>
                    <div class="modal-footer form-submit col-md-12 col-sm-12">
                        <div class="action-btn">
                            <span uif-append="submit" class="inline-block">
                                <div class="">

                                    <button type="button"  name="submit" onclick ="productVarient.setVarientImageHidden(`+ index +`)"   class="btn submit-btn btn-primary" id="save" >SAVE</button>

                                </div>
                            </span>
                        </div>
                    </div>
                </div>
            </div>`;
        productVarient.BindVarientImagePopup(element);
            common.HideLoader();
        $('#Modal').children('div').children('div').html(form);
        $('#Modal').show();
       
       
    },
    setVarientImageHidden: function (index) {
        var tr = $('#tblProduct').find('tr').eq(index);

        if ($('#imagePreview').attr('src') == "" || $('#imagePreview').attr('src') == undefined) {
            $('#imgspan').css('display', 'block');
                return false;
        } else if ($('#imagePreview1').attr('src').length == 0 || $('#imagePreview1').attr('src') == undefined) {
            $('#imgspan1').css('display', 'block');
            return false;
        } else if ($('#imagePreview2').attr('src').length == 0 || $('#imagePreview2').attr('src') == undefined) {
            $('#imgspan2').css('display', 'block');
            return false;
        } else if ($('#imagePreview3').attr('src').length == 0 || $('#imagePreview3').attr('src') == undefined) {
            $('#imgspan3').css('display', 'block');
            return false;
        } else if ($('#imagePreview4').attr('src').length == 0 || $('#imagePreview4').attr('src') == undefined) {
            $('#imgspan4').css('display', 'block');
            return false;
        } else if ($('#imagePreview5').attr('src').length == 0 || $('#imagePreview5').attr('src') == undefined) {
            $('#imgspan5').css('display', 'block');
            return false;
        } else {
        $(tr).find("#Image1").val($('#imagePreview').attr('src'));
        $(tr).find("#Image2").val($('#imagePreview1').attr('src'));
        $(tr).find("#Image3").val($('#imagePreview2').attr('src'));
        $(tr).find("#Image4").val($('#imagePreview3').attr('src'));
        $(tr).find("#Image5").val($('#imagePreview4').attr('src'));
        $(tr).find("#Image6").val($('#imagePreview5').attr('src'));

        commonFunction.HideModel('#Modal');
    }
        
    },

    BindVarientImagePopup: function (element) {
        var rowIndex = $(element).parent('td').parent('tr').index();
        var tr = $('#tblProduct').find('tr').eq(rowIndex);
        $("#imagePreview").attr("src", $(tr).find(('#Image1')).val());
        $("#imagePreview1").attr("src", $(tr).find(('#Image2')).val());
        $("#imagePreview2").attr("src", $(tr).find(('#Image3')).val());
        $("#imagePreview3").attr("src", $(tr).find(('#Image4')).val());
        $("#imagePreview4").attr("src", $(tr).find(('#Image5')).val());
        $("#imagePreview5").attr("src", $(tr).find(('#Image6')).val());

        $('#Modal').show();
        
    },
   
    }


    


        


