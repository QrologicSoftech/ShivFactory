let accountController = 'Account';

var Account = {
    CustomerLogIn: function () {
        common.ShowLoader();

        ajax.doGetAjax(`/${accountController}/LogIn`, function (result) {
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
        ajax.doPostAjax(`/${accountController}/LogIn?returnUrl=${$('#returnUrl').val()}`, data, function (result) {
            if (result) {
                $('#Modal').children('div').children('div').html(result);
                $('#Modal').show();
            }
            common.HideLoader();
        });
    },
}