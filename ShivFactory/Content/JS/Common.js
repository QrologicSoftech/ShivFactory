var common = {

    RenderPartialView: function (controller, PartialView) {
        common.ShowLoader('.partialView');
        ajax.doGetAjax(`/${controller}/${controller}/${PartialView}`, function (result) {
            common.HideLoader('.partialView');
            $('.partialView').html(result);
        })
    },
    ShowLoader: function (idOrClass) {
        $(idOrClass).addClass('loader-spinner');
    },
    HideLoader: function (idOrClass) {
        $(idOrClass).removeClass('loader-spinner');
    },

    //RenderPartialView: function (contoller, view) {
    //    ajax.doGetAjax(`@Url.Content(/${contoller}/${view})`, `${view}`), function (result) {
    //        $('.partialView').innerHTML = result;
    //    }
    //},

}


