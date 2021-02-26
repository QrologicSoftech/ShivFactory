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
    BindDatatable: function (div, url) {
        // Create table
        var table = `<div class="table-responsive">
                        <table class="table table-bordered w-auto table-center" id="datatable">
                            <thead class="thead-light">
                                <tr>
                                    <th>Sr No.</th>
                                    <th>Category Name</th>
                                    <th>Category Image</th>
                                    <th>Is Active</th>
                                    <th>Add Date</th>
                                    <th>Actions</th>                                              
                                </tr>
                            </thead>      
                            <tfoot>
                       <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>                   
                                </tr>
                       </tfoot>
                        </table>
                    </div>`;

        // Prepare columns
        let columns = [
            { "data": 'ID' },
            { "data": 'CategoryName' },
            { "data": 'CatImage' },
            { "data": 'IsActive' },
            { "data": 'AddDate' },
            { "data": 'CategoryName' }
        ];

        // Create set footer function
        //const setFooter = function (api, intVal) {
        //    // api is DataTable method
        //    // intVal is method defined in db.common.js

        //    const totalAvgSale = api.column(4).data().reduce(function (a, b) { return intVal(a) + intVal(b); }, 0);
        //    const totalCurrentSale = api.column(5).data().reduce(function (a, b) { return intVal(a) + intVal(b); }, 0);
        //    const totalContribution = api.column(6).data().reduce(function (a, b) { return intVal(a) + intVal(b); }, 0);
        //    const totalPrvSale = api.column(7).data().reduce(function (a, b) { return intVal(a) + intVal(b); }, 0);

        //    $(api.column(3).footer()).html('<b>Total</b>');
        //    $(api.column(4).footer()).html(`<b>${totalAvgSale.toFixed(2)}</b>`);
        //    $(api.column(5).footer()).html(`<b>${totalCurrentSale.toFixed(2)}</b>`);
        //    $(api.column(6).footer()).html(`<b>${totalContribution.toFixed(2)} %</b>`);
        //    $(api.column(7).footer()).html(`<b>${totalPrvSale.toFixed(2)}</b>`);
        //};

        $(`#${div}`).html(table);
        $('#datatable')
        dtTable.bindDataToTable(url, null, columns, '#customerDetail', `#${div}`, setFooter);
    },

    BindServerSideTable: function (tableId, columns ,url) {
       
        

  
        //$(`#${tableId}`).DataTable({
        //    "language":
        //    {
        //        "processing": "<div class='overlay custom-loader-background'><i class='fa fa-cog fa-spin custom-loader-color'></i></div>"
        //    },
        //    "processing": true, // for show progress bar  
        //    "serverSide": true, // for process server side  
        //    "filter": true, // this is for disable filter (search box)  
        //    "orderMulti": false, // for disable multiple column at once  
        //    "ajax": {
        //        "url": url,
        //        "type": "POST",
        //        "datatype": "json",
        //    },
        //    "columns": columns
        //});



        dtTable.bindDataToTable(url, null, columns, tableId, tableId, null,true,true);
    },
}


