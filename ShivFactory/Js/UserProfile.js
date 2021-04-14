let homeArea = 'Home', homeController = 'Home',accountController='Account' ,PartialView ='GetCurrentUserDetails';
var userprofile = {

    BindBasicDetail: function () {
        common.ShowLoader();
        var form;
        ajax.doGetAjax(`/${homeController}/${PartialView}`, function (result) {
            if (result.ResultFlag == false) { common.ShowMessage(result); }
            let user = result.Data;
            // Create table
            form = `<div class="form-text-acount" style="background:#fff">
<h4 class = "text-center">Update Basic Details</h4>  
                <div class="form-horizontal" name="personal-details" autocomplete="off">
                    <div class="form-fields">
                        <div class="form-group ">
                            <div class="row offset-3">
                                <div class="col-sm-6">
                                    <div>
      <label class="control-label required" for="display_name">First Name</label> <input class="form-control"  id="FirstName" name="FirstName" placeholder="FirstName" type="text" value="${user.FirstName == null ? '' : user.FirstName}" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group ">
                            <div class="row offset-3">
                               

                                <div class="col-sm-6">
                                    <div>
                               <label class="control-label required" for="display_name">Enter Last Name</label>     <input class="form-control" id="LastName" name="LastName" placeholder="LastName" type="text" value="${user.LastName == null ? '' : user.LastName}" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group ">
                            <div class="row offset-3">

                                <div class="col-sm-6">
                                    <div>
                    <label class="control-label required" for="display_name">Gender</label>                 <input type="radio" class="form-radio-input" id="male" name="gender" value="Male" >Male</input>  &nbsp; &nbsp;&nbsp; <input type="radio" id="female" name="gender" value="Female">Female</input> 
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

 
                    <div class="modal-footer form-submit col-md-12 col-sm-12">
                        <div class="action-btn">
                            <span uif-append="submit" class="inline-block">
                                <div class="" >

                                    <button type="submit" name="submit" onclick="userprofile.UpdateBasicDetails();" class="btn submit-btn btn-primary" id="save" >SAVE</button>

                                </div>
                            </span>
                        </div>
                    </div>
                </div>
            </div>`;
            common.HideLoader();
            $('#Modal').children('div').children('div').html(form);
            $("input[name='gender'][value='" + user.Gender + "']").prop('checked', true);
            $('#Modal').show();
        });
    },
    UpdateBasicDetails: function () {
        if (userprofile.ValidateBasicDetail()) {
            data = {
                "FirstName": $('#FirstName').val(),
                "LastName": $('#LastName').val(),
                "Gender": $('input[name="gender"]:checked').val(),
            }
            ajax.doPostAjax(`/${homeController}/SaveCurrentUserBasicDetails`, data, function (result) {
                common.ShowMessage(result);
                if (result.ResultFlag) {
                    commonFunction.HideModel('#Modal');
                }
            });
        }
    },


    BindMobileDetail: function () {
        common.ShowLoader();
        var form;
        // Create table
        form = `<div class="form-text-acount" style ="background:#fff">
<h4 class = "text-center">Change Mobile</h4>
                <div class="form-horizontal" name="mobile" autocomplete="off">
                    <div class="form-fields">
                        <div class="form-group ">
                            <div class="row offset-3">
                              

                                <div class="col-sm-6">
                                    <div>
  <label class="control-label required" for="display_name">Mobile</label>    <input class="form-control" data-val="true"  id="Mobile" name="Mobile" placeholder="Enter Mobile" type="mobile" />
                                      
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                    <div class="modal-footer form-submit col-md-12 col-sm-12">
                        <div class="action-btn">
                            <span uif-append="submit" class="inline-block">
                                <div >
                                    <button type="submit" name="submit" onclick="profile.UpdateMobileDetail();" class="btn submit-btn btn-primary" id="save" >SAVE</button>
                                </div>
                            </span>
                        </div>
                    </div>
                </div>
            </div>`;
        common.HideLoader();
        $('#Modal').children('div').children('div').html(form);
        $('#Modal').show();

    },
    UpdateMobileDetail: function () {
        if (userprofile.ValidateUserPasswordDetail()) {
            data = {
                "Mobile": $('#Mobile').val(),
            }
            ajax.doPostAjax(`/${accountController}/UpdateCurrentUserMobile`, data, function (result) {
                common.ShowMessage(result);
                if (result.ResultFlag) {
                    commonFunction.HideModel('#Modal');
                }
            });
        }

    },

    BindEmailDetail: function () {
        common.ShowLoader();
        var form;
        // Create table
        form = `   <div class="form-text-acount" style ="background:white">
<h4 class = "text-center">Change Email</h4>
                <div class="form-horizontal" name="email" autocomplete="off">
                    <div class="form-fields">
                        <div class="form-group ">
                            <div class="row offset-3">
                                <div class="col-sm-6">
                                    <div>
  <label class="control-label required" for="display_name">Email</label>    <input required class="form-control"   id="Email" name="Email" placeholder="Enter Email" type ="email"  />
                                      
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                    <div class="modal-footer form-submit col-md-12 col-sm-12">
                        <div class="action-btn">
                            <span uif-append="submit" class="inline-block">
                                <div >
                                    <button type="submit" name="submit" onclick="profile.UpdateEmailDetail();" class="btn submit-btn btn-primary" id="save" >SAVE</button>
                                </div>
                            </span>
                        </div>
                    </div>
                </div>
            </div>`;
        common.HideLoader();
        $('#Modal').children('div').children('div').html(form);
        $('#Modal').show();

    },
    UpdateEmailDetail: function () {
        if (userprofile.ValidateUserPasswordDetail()) {
            data = {
                "Email": $('#Email').val(),
            }
            ajax.doPostAjax(`/${accountController}/UpdateCurrentUserEmail`, data, function (result) {
                common.ShowMessage(result);
                if (result.ResultFlag) {
                    commonFunction.HideModel('#Modal');
                }
            });
        }

    },


    BindUserPasswordDetails: function () {
        common.ShowLoader();
        var form;
        // Create table
        form = `   <div class="form-text-acount" style = "background:#fff">
<h4  class = "text-center">Change Password</h4>
                <div class="form-horizontal" name="bank-details" autocomplete="off">
                    <div class="form-fields">
                        <div class="form-group ">
                            <div class="row offset-3">
                               

                                <div class="col-sm-6">
                                    <div>
     <label class="control-label required" for="display_name">Password</label>   <input class="form-control"   id="Password" name="Password" placeholder="Enter Password" type="password" />
                                      
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group ">
                            <div class="row">
                        
                                <div class="col-sm-6">
                                    <div>
                            <label class="control-label required" for="display_name">Confirm Password</label>       <input class="form-control" id="ConfirmPassword" name="ConfirmPassword" placeholder="Confirm Password" type="password"  />
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                    <div class="modal-footer form-submit col-md-12 col-sm-12">
                        <div class="action-btn">
                            <span uif-append="submit" class="inline-block">
                                <div >
                                    <button type="submit" name="submit" onclick="userprofile.UpdateUserPasswordDetails();" class="btn submit-btn btn-primary" id="save" >SAVE</button>
                                </div>
                            </span>
                        </div>
                    </div>
                </div>
            </div>`;
        common.HideLoader();
        $('#Modal').children('div').children('div').html(form);
        $('#Modal').show();

    },
    UpdateUserPasswordDetails: function () {
        if (userprofile.ValidateUserPasswordDetail()) {
            data = {
                "NewPassword": $('#Password').val(),
                "ConfirmPassword": $('#ConfirmPassword').val()
            }
            ajax.doPostAjax(`/${accountController}/UpdateCurrentUserPassword`, data, function (result) {
                common.ShowMessage(result);
                if (result.ResultFlag) {
                    commonFunction.HideModel('#Modal');
                }
            });
        }

    },

    ValidateMobileDetail: function () {
        if ($('#Mobile').val() == null || $('#Mobile').val() == 'undefined' || $('#Mobile').val().length == '0') {
            toastr.error('Enter Mobile!');
            return false;
        } 
        return true;
    },
    ValidateBasicDetail: function () {
        if ($('#FirstName').val() == null || $('#FirstName').val() == 'undefined' || $('#FirstName').val().length == '0') {
            toastr.error('Enter First Name!');
            return false;
        } else if ($('#LastName').val() == null || $('#LastName').val() == 'undefined' || $('#LastName').val().length == '0') {
            toastr.error('Enter Last Name!');
            return false;
        }

        return true;
    },

    ValidateUserPasswordDetail: function () {
        if ($('#Password').val() == null || $('#Password').val() == 'undefined' || $('#Password').val().length == '0') {
            toastr.error('Enter Password!');
            return false;
        } else if ($('#ConfirmPassword').val() == null || $('#ConfirmPassword').val() == 'undefined' || $('#ConfirmPassword').val().length == '0') {
            toastr.error('Enter Confirm Password !');
            return false;
        } else if ($('#Password').val() != $('#ConfirmPassword').val()) {
            toastr.error('Password and Confirm Password Mismatch !');
            return false;
        }
        return true;

    }


}
