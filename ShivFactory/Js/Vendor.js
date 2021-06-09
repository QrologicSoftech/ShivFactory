let vendorArea = 'Vendor', vendorController = 'Vendor';

var Vendor = {

    RenderPartialView: function (PartialView) {
        common.ShowLoader('.partialView');
        ajax.doGetAjax(`/${vendorArea}/${vendorController}/${PartialView}`, function (result) {
            common.HideLoader('.partialView');
            $('.partialView').html(result);
        })
    },
    ///  Product Functions  start

    DeleteProduct: function (element) {

        if (confirm("Are you sure want to delete this product?")) {
            data = {
                "id": $(element).closest('tr').attr('Id')
            }
            ajax.doPostAjax(`/${vendorArea}/${vendorController}/DeleteProduct`, data, function (result) {
                common.ShowMessage(result);
                if (result.ResultFlag) {
                    location.reload();
                }
            });
        }
    },
    EditProduct: function (element) {

        if (confirm("Are you sure want to edit this product? \n after update product need to approved for listing")) {

            var productId = $(element).closest('tr').attr('Id');

            location.replace(`/Vendor/Vendor/ProductVarient/${productId}`);
        }
    },
    GetProductVarients: function (element) {
        let productId = $(element).closest('tr').attr('Id');
        if (productId == undefined || productId == null) { return false; }
        common.ShowLoader();
        location.replace(`/Vendor/Vendor/VarientPartialView/${productId}`);
    },
    ShowVarientQty: function (element) {
        let qty = $(element).html();

        if (confirm("Are you sure want to update Qty this product?")) {
            let varientId = $(element).closest('tr').attr('Id');
            let qty = $(element).html();
            if (varientId == undefined || varientId == null) { return false; }
            common.ShowLoader();
            let html = `<div class="container">
    <div class="container-fluid page-body-wrapper">
        <div class="main-panel">
            <div class="content-wrapper">
<input type="hidden" id="varient-Id" value="${varientId}"/>
                <div class="row">
                    <div class="col-md-12 grid-margin stretch-card">
                        <div class="card">
                            <div class="card-body">
                                <h4 class="card-title text-center">Varient Quantity</h4>
                               <input type="number" value="${qty}" class="form-control col-md-12" id="txtvar-qty" minValue="0" /> 
                              </div>
                             <button type="button" class="btn btn-block btn-gradient-primary" onclick="Vendor.UpdateVarientQty();"> Update</button>
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
    UpdateVarientQty: function () {

        let qty = $("#txtvar-qty").val();
        if (qty == undefined || qty == "" || qty == null || qty <0) {
            toastr.error("Please select qty min value is 0 !");
            return false;
        }

        let varientId = $("#varient-Id").val();
        if (varientId == undefined || varientId == null) { return false; }
        common.ShowLoader();
        data = {
            "varientId": varientId,
            "qty": qty
        }
        ajax.doPostAjax(`/${vendorArea}/${vendorController}/UpdateVarientQty`, data, function (result) {
            if (result.ResultFlag) {
                commonFunction.HideModel('#Modal');
                location.reload();
            }
            common.ShowMessage(result);
            common.HideLoader();
        });
    },
    DeleteProductVarient: function (id) {

        if (confirm("Are you sure want to delete this product?")) {
            data = {
                "id": id
            }
            ajax.doPostAjax(`/${vendorArea}/${vendorController}/DeleteProductVarient`, data, function (result) {
                common.ShowMessage(result);
                if (result.ResultFlag) {
                    location.reload();
                }
            });
        }
        //else {
            
        //    commonFunction.ReloadCallback(function () {
        //        $(document).ready(function () {
        //            setTimeout(function () { toastr.success('Lokesh Choudhary'); }, 5000);
                    
        //        });
        //    });
        //}
    },
    UpdateProductColors: function () {
       
        common.ShowLoader();
        var productColors = $('.productColor:checkbox:checked').map(function () {
            return this.value;
        }).get();


        let productId = $("#hndProductId").val();
        if (productId == undefined || productId == null) { return false; }

        data = {
            "productId": productId,
            "colors": productColors.join(",")
        }
        ajax.doPostAjax(`/${vendorArea}/${vendorController}/UpdateProductColor`, data, function (result) {
            if (result.ResultFlag) {
                commonFunction.HideModel('#Modal');
            }
            common.ShowMessage(result);
            common.HideLoader();
        });
    }, 


    ///  Product Functions  End

    DeletePincode: function (element) {

        if (confirm("Are you sure want to delete this pincode?")) {
            data = {
                "id": $(element).closest('tr').attr('Id')
            }
            ajax.doPostAjax(`/${vendorArea}/${vendorController}/DeletePincode`, data, function (result) {
                common.ShowMessage(result);
                if (result.ResultFlag) {
                    location.reload();
                }
            });
        }
    },

    EditPincode: function (element) {

        if (confirm("Are you sure want to edit this pincode?")) {

            var pincodeId = $(element).closest('tr').attr('Id');

            location.replace(`/Vendor/Vendor/AddShippingArea/${pincodeId}`);
        }
    },

}


