let adminArea = 'Admin', adminController = 'Admin', apiController = 'System', manageController = 'Manage';

var Admin = {

    RenderPartialView: function (PartialView) {
        common.ShowLoader('.partialView');
        ajax.doGetAjax(`/${adminArea}/${adminController}/${PartialView}`, function (result) {
            common.HideLoader('.partialView');
            $('.partialView').html(result);
        })
    },

    DeleteCategory: function (categoryId) {

        if (confirm("Are you sure want to delete this category?")) {
            data = {
                "id": categoryId
            }
            ajax.doPostAjax(`/${adminArea}/${adminController}/DeleteCategory`, data, function (result) {
                common.ShowMessage(result);
                if (result.ResultFlag) {
                    location.reload();
                }
            });
        }
    },
    DeleteSubCategory: function (SubcategoryId) {

        if (confirm("Are you sure want to delete this subCategory?")) {
            data = {
                "id": SubcategoryId
            }
            ajax.doPostAjax(`/${adminArea}/${adminController}/DeleteSubCategory`, data, function (result) {
                common.ShowMessage(result);
                if (result.ResultFlag) {
                    location.reload();
                }
            });
        }
    },
    GetSubCategoryVarients: function (element) {
        let subCategoryId = $(element).closest('tr').attr('Id');
        if (subCategoryId == undefined || subCategoryId == null) { return false; }
        common.ShowLoader();
        data = {
            "subCategoryId": subCategoryId
        }
        ajax.doPostAjax(`/${adminArea}/${adminController}/SubCategoryVarients`, data, function (result) {
            if (result) {
                $('#Modal').children('div').children('div').html(result);
                $('#Modal').show();
            }
            common.HideLoader();
        });
    },
    UpdateSubCategoryVarients: function () {

        common.ShowLoader();
        var varients = $('.Sub_varient:checkbox:checked').map(function () {
            return this.value;
        }).get();


        let subCategoryId = $("#hndSubCategory").val();
        if (subCategoryId == undefined || subCategoryId == null) { return false; }

        data = {
            "subCategoryId": subCategoryId,
            "varientIds": varients.join(",")
        }
        ajax.doPostAjax(`/${adminArea}/${adminController}/UpdateSubCategoryVarients`, data, function (result) {
            if (result.ResultFlag) {
                commonFunction.HideModel('#Modal');
            }
            common.ShowMessage(result);
            common.HideLoader();
        });
    },


    DeleteMiniCategory: function (MinicategoryId) {

        if (confirm("Are you sure want to delete this miniCategory?")) {
            data = {
                "id": MinicategoryId
            }
            ajax.doPostAjax(`/${adminArea}/${adminController}/DeleteMiniCategory`, data, function (result) {
                common.ShowMessage(result);
                if (result.ResultFlag) {
                    location.reload();
                }
            });
        }
    },
    DeleteBrand: function (BrandId) {

        if (confirm("Are you sure want to delete this Brand?")) {
            data = {
                "id": BrandId
            }
            ajax.doPostAjax(`/${adminArea}/${adminController}/DeleteBrand`, data, function (result) {
                common.ShowMessage(result);
                if (result.ResultFlag) {
                    location.reload();
                }
            });
        }
    },
    DeleteBanner: function (BannerId) {

        if (confirm("Are you sure want to delete this Banner?")) {
            data = {
                "id": BannerId
            }
            ajax.doPostAjax(`/${adminArea}/${adminController}/DeleteBanner`, data, function (result) {
                common.ShowMessage(result);
                if (result.ResultFlag) {
                    location.reload();
                }
            });
        }
    },

    DeleteDimension: function (dimensionId) {

        if (confirm("Are you sure want to delete this Dimension?")) {
            data = {
                "id": dimensionId
            }
            ajax.doPostAjax(`/${adminArea}/${adminController}/DeleteDimension`, data, function (result) {
                common.ShowMessage(result);
                if (result.ResultFlag) {
                    location.reload();
                }
            });
        }
    },

    DeleteWeight: function (weightId) {

        if (confirm("Are you sure want to delete this Weight parameter?")) {
            data = {
                "id": weightId
            }
            ajax.doPostAjax(`/${adminArea}/${adminController}/DeleteWeight`, data, function (result) {
                common.ShowMessage(result);
                if (result.ResultFlag) {
                    location.reload();
                }
            });
        }
    },
    DeleteColor: function (colorId) {

        if (confirm("Are you sure want to delete this color?")) {
            data = {
                "id": colorId
            }
            ajax.doPostAjax(`/${adminArea}/${adminController}/DeleteColor`, data, function (result) {
                common.ShowMessage(result);
                if (result.ResultFlag) {
                    location.reload();
                }
            });
        }
    },

    ChangeProfileImage: function () {

        if (confirm("Are you sure want to Change  this image?")) {
            window.location.replace("/Home/ChangeProfileImage");
        }
    },

    //  Vendor products function  Start


    BindProductTable: function (divId, url, ColumnArray, rowId, actionArray) {

        data = {
            "AdminStatus": $('select#productStatus option:selected').val()
        }

        let tr = '';
        ColumnArray.forEach((item) => {
            if (item == 'Id') { return; }
            tr += `<th>${item}</th>`;
        });
        if (actionArray) {
            actionArray.forEach((item) => {
                tr += `<th>${item.Header}</th>`;
            });
        }

        // Create table
        var table = `<div class="table-responsive">
                        <table class="table datatable" cellspacing="0" width="100%" id="datatable">
                            <thead class="thead-light">
                                <tr class="bg-primary text-white">
                                    ${tr}                                            
                                </tr>
                            </thead> 
                        </table>
                    </div>`;
        $(`#${divId}`).html(table);

        // Prepare columns
        commonFunction.GetColumnForDataTable(ColumnArray, actionArray, true, function (result) {
            dtTable.bindDataToTable(url, data, result, rowId, '#datatable', `#${divId}`, null, true, true);
        })
    },

    GetProductImages: function (element, isvarient) {
        let productId = $(element).closest('tr').attr('Id');
        if (productId == undefined || productId == null) { return false; }
        common.ShowLoader();
        data = {
            "productId": isvarient == false ? productId : 0,
            "varientId": isvarient == true ? productId : 0,
        }
        ajax.doPostAjax(`/${manageController}/ProductImage`, data, function (result) {
            if (result) {
                $('#Modal').children('div').children('div').html(result);
                $('#Modal').show();
            }
            common.HideLoader();
        });
    },

    GetProductBasicInfo: function (element) {
        let productId = $(element).closest('tr').attr('Id');
        if (productId == undefined || productId == null) { return false; }
        common.ShowLoader();
        data = {
            "productId": productId
        }
        ajax.doPostAjax(`/${manageController}/ProductBasicInfo`, data, function (result) {
            if (result) {
                $('#Modal').children('div').children('div').html(result);
                $('#Modal').show();
            }
            common.HideLoader();
        });
    },
    GetProductDetails: function (element) {
        let productId = $(element).closest('tr').attr('Id');
        if (productId == undefined || productId == null) { return false; }
        common.ShowLoader();
        data = {
            "productId": productId
        }
        ajax.doPostAjax(`/${manageController}/ProductDetails`, data, function (result) {
            if (result) {
                $('#Modal').children('div').children('div').html(result);
                $('#Modal').show();
            }
            common.HideLoader();
        });
    },
    GetProductVarients: function (element) {
        let productId = $(element).closest('tr').attr('Id');
        if (productId == undefined || productId == null) { return false; }
        common.ShowLoader();
        data = {
            "productId": productId
        }
        ajax.doPostAjax(`/${adminArea}/${adminController}/ProductVarients`, data, function (result) {
            if (result) {
                $('#Modal').children('div').children('div').html(result);
                $('#Modal').show();
            }
            common.HideLoader();
        });
    },

    ApprovedProduct: function (element) {
        if (confirm("Are you sure want to approved this product?")) {
            let productId = $(element).closest('tr').attr('Id');
            if (productId == undefined || productId == null) { return false; }
            common.ShowLoader();
            data = {
                "productId": productId
            }
            ajax.doPostAjax(`/${adminArea}/${adminController}/ApprovedProduct`, data, function (result) {
                if (result.ResultFlag) {
                    location.reload();
                }
                common.ShowMessage(result);
            });
        }
    },

    UnApprovedProduct: function (element) {
        if (confirm("Are you sure want to reject this product?")) {
            let productId = $(element).closest('tr').attr('Id');
            if (productId == undefined || productId == null) { return false; }
            common.ShowLoader();
            let html = `<div class="container">
    <div class="container-fluid page-body-wrapper">
        <div class="main-panel">
            <div class="content-wrapper">
<input type="hidden" id="RejectProductId" value="${productId}"/>
                <div class="row">
                    <div class="col-md-12 grid-margin stretch-card">
                        <div class="card">
                            <div class="card-body">
                                <h4 class="card-title text-center">Reject region</h4>
                               <textarea type="text" class="form-control col-md-12" placeholder="Product reject region" id="txtProductReject" maxlength="200"></textarea> 
                              </div>
                             <button type="button" class="btn btn-block btn-gradient-primary" onclick="Admin.RejectProduct();"> Update</button>
                            </div>
                           </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>`;
            $('#Modal').children('div').children('div').html(html);
            $('#Modal').show();
            common.HideLoader();
        }
    },
    RejectProduct: function () {

        let rejectRegion = $("#txtProductReject").val();
        if (rejectRegion == undefined || rejectRegion == "" || rejectRegion == null) {
            toastr.error("Please enter reject region.");
            return false;
        }
        if (rejectRegion.length > 200) {
            toastr.error("The field value characters are less than or equal to 200 characters.");
            return false;
        }

        let productId = $("#RejectProductId").val();
        if (productId == undefined || productId == null) { return false; }
        common.ShowLoader();
        data = {
            "productId": productId,
            "rejectRegion": rejectRegion
        }
        ajax.doPostAjax(`/${adminArea}/${adminController}/RejectProduct`, data, function (result) {
            if (result.ResultFlag) {
                commonFunction.HideModel('#Modal');
                location.reload();
            }
            common.ShowMessage(result);
            common.HideLoader();
        });
    },

    //  Vendor products functions  End

    BlockUser: function (element) {
        if (confirm("Are you sure want to block this customer? \n after block user can't able to login.")) {
            let userId = $(element).closest('tr').attr('Id');
            if (userId == 'undefined' || userId == null) { return false; }
            common.ShowLoader();
            data = {
                "userId": userId
            }
            ajax.doPostAjax(`/${manageController}/BlockUser`, data, function (result) {
                debugger;
                if (result.ResultFlag) {
                    location.reload();
                }
                common.ShowMessage(result);
            });
        }
    },

}


