let adminArea = 'Admin', adminController = 'Admin';

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

    ChangeProfileImagee: function () {

        if (confirm("Are you sure want to Change  this image?")) {
            ajax.doPostAjax(`/${adminArea}/${adminController}/ChangeProfileImage`, "", function (result) {
                common.ShowMessage(result);
                if (result.ResultFlag) {
                    location.reload();
                }
            });
        }
    },

       

}


