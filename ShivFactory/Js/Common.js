var common = {

    RenderPartialView: function (controller, PartialView) {
        common.ShowLoader('.partialView');
        ajax.doGetAjax(`/${controller}/${PartialView}`, function (result) {
            common.HideLoader('.partialView');
            $('.partialView').html(result);
        })
    },
    ShowLoader: function (idOrClass) {
        if (idOrClass && idOrClass != undefined) {
            $(idOrClass).addClass('loader-spinner');
        }
        else { $('#loader').addClass('loader-spinner'); }
    },
    HideLoader: function (idOrClass) {
        if (idOrClass && idOrClass != undefined) {
            $(idOrClass).removeClass('loader-spinner');
        }
        else { $('#loader').removeClass('loader-spinner'); }
    },

    BindDatatable: function (divId, url, ColumnArray, rowId, actionArray) {
        let tr = '';
        ColumnArray.forEach((item) => {
            if (item == 'Id') { return; }
            tr += `<th>${item}</th>`;
        });
        if (actionArray) { tr += `<th>Action</th>`; }


        // Create table
        var table = `<div class="table-responsive">
                        <table class="table datatable" cellspacing="0" width="100%" id="datatable">
                            <thead class="thead-light">
                                <tr class="bg-primary text-white">
                                    ${tr}                                            
                                </tr>
                            </thead> 
                        </table>
                    </div>`;
        $(`#${divId}`).html(table);
        // Prepare columns
        commonFunction.GetColumnForDataTable(ColumnArray, actionArray, '', function (result) {
            dtTable.bindDataToTable(url, null, result, rowId, '#datatable', `#${divId}`, null, true, true);
        })
    },

    BindServerSideTable: function (tableId, columns, url, rowId) {

        dtTable.bindDataToTable(url, null, columns, rowId, tableId, tableId, null, true, true);
    },
    ShowMessage: function (data) {
        if (data.ResultFlag) {
            toastr.success(data.Message);
        }
        else { toastr.error(data.Message); }

    },

    Timer: function (remaining, id, timerOn) {
        var m = Math.floor(remaining / 60);
        var s = remaining % 60;

        m = m < 10 ? '0' + m : m;
        s = s < 10 ? '0' + s : s;
        document.getElementById(id).innerHTML = m + ':' + s;
        remaining -= 1;

        if (remaining >= 0 && timerOn) {
            setTimeout(function () {
                common.Timer(remaining, id, true);
            }, 1000);
            return;
        }

        if (!timerOn) {
            // Do validate stuff here
            return;
        }
        // Do timeout stuff here
        alert('Timeout for otp');
    }
}


