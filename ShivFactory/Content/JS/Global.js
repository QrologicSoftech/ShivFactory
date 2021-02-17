$(document).ready(function () {
    $('#example').DataTable({
        dom: 'Bfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ]
    });
});

//$(document).ready(function () {
//    //'use strick';.
//    $('.bindPopupModal').on("click", bindSerivcePartial);
//    function bindSerivcePartial(e) {
//        e.preventDefault();
//        var $this = $(this);

//        $.ajax({
//            url: $this.attr("data-ajaxurl"),
//            method: "Get",
//            dataType: "html",
//            //contentType: "",
//            success: function (resultData) { $("#Modal_body").html(resultData); console.log(resultData); },
//            error: function (a, b, c) { console.log(a, b, c); }
//        });
//    }
//});


//var btnEleShow = document.getElementById('toastBtnShow');
//var btnEleHide = document.getElementById('toastBtnHide');
//document.onclick = function (e) {
//    if (e.target !== btnEleShow) {
//        toastObj.hide('All');
//    }
//};

//btnEleShow.onclick = function () {
//    toastObj.show();
//};

//btnEleHide.onclick = function () {
//    toastObj.hide('All');
//};

//function created() {
//    toastObj = this;
//}
//function onclose(e) {
//    if (e.toastContainer.childElementCount === 0) {
//        btnEleHide.style.display = 'none';
//    }
//}

//function onBeforeOpen() {
//    btnEleHide.style.display = 'inline-block';
//}
//setTimeout(function () {
//    toastObj.show({
//        target: document.body
//    });
//}, 500);

