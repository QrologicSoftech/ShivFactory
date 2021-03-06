let vendorArea = 'Vendor', vendorController = 'Vendor';

var Vendor = {

    RenderPartialView: function (PartialView) {
        common.ShowLoader('.partialView');
        ajax.doGetAjax(`/${vendorArea}/${vendorController}/${PartialView}`, function (result) {
            common.HideLoader('.partialView');
            $('.partialView').html(result);
        })
    },

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

        if (confirm("Are you sure want to edit this product? <br> after update product need to approved for listing")) {

            var productId = $(element).closest('tr').attr('Id');

            location.replace(`/Vendor/Vendor/AddProduct/${productId}`);
        }
    },
    GetProductColors: function (element) {
        let productId = $(element).closest('tr').attr('Id');
        if (productId == undefined || productId == null) { return false; }
        common.ShowLoader();
        data = {
            "productId": productId
        }
        ajax.doPostAjax(`/${vendorArea}/${vendorController}/ProductColors`, data, function (result) {
            if (result) {
                $('#Modal').children('div').children('div').html(result);
                $('#Modal').show();
            }
            common.HideLoader();
        });
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

}


