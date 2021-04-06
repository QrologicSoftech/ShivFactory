var cart = {
    AddToCart: function (IsUserWishList) {
        var data = {
            "ProductID": $('#ProductId').val(),
            "ProductVarientId": $('#ProductVarientId').val(),
            "ProductName": $('#ProductName').html(),
            "Price": parseFloat($('#SalePrice').html()),
            "Quantity": $('#Quantity').val(),
            "VendorId": $('#vendorId').val(),
            "IsUserWishList": IsUserWishList,
        }
        ajax.doPostAjax(`/Home/AddToCart`, data, function (result) {
                common.ShowMessage(result);
        });

    },

    UpdateCart: function (element) {
        let TempOrderDetailId = $(element).closest('tr').attr('Id');
        var data = {
            "TempOrderDetailId": TempOrderDetailId,
            "Quantity": $(element).val()
        }
        ajax.doPostAjax(`/Home/UpdateCart`, data, function (result) {
            //common.ShowMessage(result);
            //if (result.ResultFlag == true) {
            //    location.reload('/Home/ShowCart');
            //}
          
        });

    }
};