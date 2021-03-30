var cart = {
    AddToCart: function () {
        var data = {
            "ProductID": $('#ProductId').val(),
            "ProductVarientId": $('#ProductVarientId').val(),
            "ProductName": $('#ProductName').html(),
            "Price": parseFloat($('#SalePrice').html()),
            "Quantity": '1',
            "Brand": '',
            "Varient": ''
        }
        ajax.doPostAjax(`/Home/AddToCart`, data, function (result) {
            common.ShowMessage(result);
        });

}
};