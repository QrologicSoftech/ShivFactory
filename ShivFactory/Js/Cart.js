﻿var cart = {
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
                //cart.RenderCart();
                cart.CalculateCart();
            }
        });
    },
    CalculateCart: function () {
        common.ShowLoader('#cart_partial');      
        let cartvalue = 0;
        $(".table-shopping-cart tbody tr").each(function () {           
            var itemPrice = $(this).find("#SalePrice").html();
            var itemQty = $(this).find("#Quantity").val();
            let totalAmount = itemPrice * itemQty;
            $(this).find(".price").html('<i class="fas fa-rupee-sign"></i>' + totalAmount.toFixed(2));
            cartvalue += totalAmount;
        });
        $('#total-cart-val').html('<i class="fas fa-rupee-sign"></i>' +cartvalue.toFixed(2));
        common.HideLoader('#cart_partial');
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