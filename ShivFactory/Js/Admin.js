var Admin = {

    DeleteCategory: function (categoryId) {

        if (confirm("Are you sure want to delete this category!")) {
            data = {
                "id": categoryId
            }
            ajax.doPostAjax(`/Admin/Admin/DeleteCategory`, data, function (result) {
                common.ShowMessage(result);
                if (result.ResultFlag) {
                    location.reload();
                }

            });
        }
    }

}


