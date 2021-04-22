

var Account = {
    CustomerLogIn: function (returnUrl) {
        common.ShowLoader();
        $('#returnUrl').val(returnUrl);
        $('#LoginModal').find(`span[data-valmsg-for]`).html(null);
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
    CustomerLogInPost: function () {
        //common.ShowLoader();
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
            //common.HideLoader();
        });
    },

    CustomerSignUpPost: function () {
        common.ShowLoader();
        $(`span[data-valmsg-for|='${'SignUp'}']`).html(null);
        data = {
            "PhoneNumber": $('#PhoneNumberSignUp').val(),
            "Password": $('#PasswordSignUp').val(),
            "Emailid": $('#EmailIdSignUp').val()
        }
        ajax.doPostAjax(`/${accountController}/Register?returnUrl=${$('#returnUrl').val()}`, data, function (result) {
            if (result.ResultFlag == false) {
                result.Data.forEach((item) => {
                    console.log(item);
                    if (item.Values.length > 0) {
                        $(`span[data-valmsg-for|='${item.Key +'SignUp'}']`).html(item.Values[0]);
                    }
                    else { $(`span[data-valmsg-for|='${item.Key +'SignUp'}']`).html(null); }
                });
            }
            else {
              
                window.location.href = result.Message;
            }
            common.HideLoader();
        });
    },

    HideLoginSignUpModal: function () {
        $('#PhoneNumber').val(''); 
        $('#Password').val('') ; 
        $('#PhoneNumberSignUp').val('');
        $('#PasswordSignUp').val('') ;
        $('#EmailIdSignUp').val('');
        $('#LoginModal').css('display', 'none');
    },
}