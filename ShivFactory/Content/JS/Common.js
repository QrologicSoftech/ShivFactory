var common = {
    RenderPartialView: function (controller, PartialView) {
        ajax.doGetAjax(`/${controller}/${controller}/${PartialView}`, function(result) {         
            $('.partialView').html( result);
        })
    },


    //RenderPartialView: function (contoller, view) {
    //    ajax.doGetAjax(`@Url.Content(/${contoller}/${view})`, `${view}`), function (result) {
    //        $('.partialView').innerHTML = result;
    //    }
    //},
    
}


