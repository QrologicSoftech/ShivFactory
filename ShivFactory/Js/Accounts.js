

var Account = {
    CustomerLogIn: function (returnUrl) {
        common.ShowLoader();
        debugger;
        $('#LoginModal').css('display', 'block');
        $('#signIn').css('display', 'block');
        $('#signUp').css('display', 'none');
        
            common.HideLoader();
    },

    CustomerSignUp: function () {
        common.ShowLoader();
        $('#LoginModal').css('display', 'block');
        $('#signIn').css('display', 'none');
        $('#signUp').css('display', 'block');
        common.HideLoader();
    },
    //CustomerLogIn: function (returnUrl) {
    //    common.ShowLoader();
    //    ajax.doGetAjax(`/${accountController}/LogIn?returnUrl=${returnUrl}`, function (result) {
    //        if (result) {
    //            // alert(result);
    //            $('#Modal').children('div').children('div').html(result);
    //            $('#Modal').show();
    //        }
    //        common.HideLoader();
    //    });
    //},
    CustomerLogInPost: function () {
        common.ShowLoader();
        data = {
            "PhoneNumber": $('#PhoneNumber').val(),
            "Password": $('#Password').val()
        }
        ajax.doPostAjax(`/${accountController}/LogInCustomer?returnUrl=${$('#returnUrl').val()}`, data, function (result) {
            if (result.ResultFlag == false) {
                result.Data.forEach((item) => {
                    console.log(item);
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

    CustomerSignUpPost: function () {
        common.ShowLoader();
        data = {
            "PhoneNumber": $('#PhoneNumberSignUp').val(),
            "Password": $('#PasswordSignUp').val(),
            "Email": $('#EmailSignUp').val()
        }
        ajax.doPostAjax(`/${accountController}/Register?returnUrl=${$('#returnUrl').val()}`, data, function (result) {
            if (result.ResultFlag == false) {
                debugger;
                result.Data.forEach((item) => {
                    console.log(item);
                    if (item.Values.length > 0) {
                        $(`span[data-valmsg-for|='${item.Key}']`).html(item.Values[0]);
                    }
                    else { $(`span[data-valmsg-for|='${item.Key}']`).html(null); }
                });
            }
            else {
                debugger;
                window.location.href = result.Message;
            }
            common.HideLoader();
        });
    },

    HideLoginSignUpModal: function () {
        $('#LoginModal').css('display', 'none');
    },
}