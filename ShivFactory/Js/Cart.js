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
            if (result.ResultFlag == true) {
                cart.RenderCart();
            }
        });
    },
    RenderCart: function () {
        common.ShowLoader('#cart_partial');
        ajax.doGetAjax(`/Home/ShowCart`, function (result) {
            common.HideLoader('#cart_partial');
            $('#cart_partial').html(result);
        });
    },

    DeleteCart: function (id) {
        debugger;
        var data = {
            "id": id,
        }
         ajax.doPostAjax(`/Home/DeleteCartItem`, data, function (result) {
            if (result.ResultFlag == true) {
            }
        });
    }
};