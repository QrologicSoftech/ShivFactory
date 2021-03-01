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
 
    BindDatatable: function (divId, url, ColumnArray, Isorderable = true) {

        let tr = '';
        ColumnArray.forEach((item) => {
            if (item == 'Id') { return; }
            tr += `<th>${item}</th>`;
        });


        // Create table
        var table = `<div class="table-responsive">
                        <table class="table table-bordered w-auto table-center" id="datatable">
                            <thead class="thead-light">
                                <tr>
                                    ${tr}                                            
                                </tr>
                            </thead> 
                        </table>
                    </div>`;
        $(`#${divId}`).html(table);
        // Prepare columns
        commonFunction.GetColumnArray(ColumnArray, function (result) {
            dtTable.bindDataToTable(url, null, result, '#datatable', `#${divId}`, null, true, true);
        })
    },

    BindServerSideTable: function (tableId, columns, url) {

        dtTable.bindDataToTable(url, null, columns, tableId, tableId, null,true,true);
    },
    ShowMessage: function (data) {
        if (data.ResultFlag) {
            toastr.success(data.Message);
        }
        else { toastr.error(data.Message); }
        
    },


}


