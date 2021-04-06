﻿var cart = {
    AddToCart: function () {
        var data = {
            "ProductID": $('#ProductId').val(),
            "ProductVarientId": $('#ProductVarientId').val(),
            "ProductName": $('#ProductName').html(),
            "Price": parseFloat($('#SalePrice').html()),
            "Quantity": $('#Quantity').val(),
            "VendorId": $('#vendorId').val()
        }
        ajax.doPostAjax(`/Home/AddToCart`, data, function (result) {
                common.ShowMessage(result);
        });

    },

    UpdateCart: function (element) {
        let TempOrderDetailId = $(element).closest('tr').attr('Id');
        var data = {
            "TempOrderDetailId": TempOrderDetailId,
            "Quantity": $(element).val(),
        }
        ajax.doPostAjax(`/Customer/UpdateCart`, data, function (result) {
            common.ShowMessage(result);
            if (result.ResultFlag == true) {
                location.replace('/Customer/ShowCart');
            }
          
        });

    }
};