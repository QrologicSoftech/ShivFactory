let vendorArea = 'Vendor', vendorController = 'Vendor';

var Vendor = {

    RenderPartialView: function (PartialView) {
        common.ShowLoader('.partialView');
        ajax.doGetAjax(`/${vendorArea}/${vendorController}/${PartialView}`, function (result) {
            common.HideLoader('.partialView');
            $('.partialView').html(result);
        })
    },

    DeleteProduct: function (productId) {

        if (confirm("Are you sure want to delete this product?")) {
            data = {
                "id": productId
            }
            ajax.doPostAjax(`/${vendorArea}/${vendorController}/DeleteProduct`, data, function (result) {
                common.ShowMessage(result);
                if (result.ResultFlag) {
                    location.reload();
                }
            });
        }
    }, 

}


