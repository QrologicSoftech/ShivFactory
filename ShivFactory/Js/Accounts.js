

var Account = {
    CustomerLogIn: function (returnUrl) {
        common.ShowLoader();        
        ajax.doGetAjax(`/${accountController}/LogIn?returnUrl=${returnUrl}`, function (result) {
            if (result) {
                $('#Modal').children('div').children('div').html(result);
                $('#Modal').show();
            }
            common.HideLoader();
        });
    },
    CustomerLogInPost: function () {
        common.ShowLoader();
        data = {
            "PhoneNumber": $('#PhoneNumber').val(),
            "Password": $('#Password').val()
        }
        ajax.doPostAjax(`/${accountController}/LogInCustomer?returnUrl=${$('#returnUrl').val()}`, data, function (result) {
            if (result.ResultFlag == false) {
                result.Data.forEach((item) => {
                    if (item.Values.length > 0) {
                        $(`span[data-valmsg-for|='${item.Key}']`).html(item.Values[0]);
                    }
                    else { $(`span[data-valmsg-for|='${item.Key}']`).html(null); }
                });
            }
            else {
                window.location.href = result.Message;
            }
            common.HideLoader();
        });
    },
}