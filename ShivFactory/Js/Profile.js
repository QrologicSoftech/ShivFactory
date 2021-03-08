let homeArea = 'Home', homeController = 'Home',accountController='Account' ,PartialView ='GetCurrentUserDetails';
var profile = {

    BindBasicDetail: function () {
        common.ShowLoader();
        var form;
        ajax.doGetAjax(`/${homeController}/${PartialView}`, function (result) {
            if (result.ResultFlag == false) { common.ShowMessage(result); }
            let user = result.Data;
            // Create table
            form = `<div class="col-md-12 grid-margin stretch-card" style="padding: 0 20% 0;">
                        <div class="card">
                            <div class="card-body">
                                <p class="card-description text-center"> Update Details</p>
			                                   <div class="form-group">
                                        
                                        <div class="input-group">
                                           <label class="form-control form-control-lg">FirstName </label>&nbsp;&nbsp;<input class="form-control form-control-lg border-left-0" data-val="true" data-val-required="FirstName Required!" id="FirstName" name="FirstName" placeholder="FirstName" type="text" value="${user.FirstName}" />
                                        </div>
                                     
                                    </div>
                                    <div class="form-group">
                                       
                                        <div class="input-group">
                                           <label class="form-control form-control-lg">LastName</label>&nbsp;&nbsp;<input class="form-control form-control-lg border-left-0" id="LastName" name="LastName" placeholder="LastName" type="text" value="${user.LastName}" />
                                        </div>
                                       
                                    </div>
                                   
                                    <div class="form-group">
                                       
                                        <div class="input-group">
                                           
                                           <label class="form-control form-control-lg">Address</label> &nbsp;&nbsp;<input class="form-control form-control-lg border-left-0" data-val="true"  data-val-required="User Address Required!" id="UserAddress" name="UserAddress" placeholder="UserAddress" type="text" value="${user.Address}" />
                                        </div>
                                       
                                    </div>
                                    
                                      <div class="form-group">
                                       
                                        <div class="input-group">
                                         <label class="form-control form-control-lg"> Gender <input type="radio" class="form-radio-input" id="male" name="gender" value="Male" >Male</input>  &nbsp; &nbsp;&nbsp; <input type="radio" id="female" name="gender" value="Female">Female</input> 
</label>                                        
</div>
                                    </div>
                                    
                                    <div class="mt-3">
                                        <button type="submit" onclick="profile.UpdateBasicDetails();" class="btn btn-block btn-gradient-primary mr-2">Update</button>
                                    </div>
                          </div>
                        </div>
                    </div>`;
            common.HideLoader();
            $('#Modal').children('div').children('div').html(form);
            $('#Modal').show();
        });
    },
    UpdateBasicDetails: function () {
        if (profile.ValidateBasicDetail()) {
            data = {
                "FirstName": $('#FirstName').val(),
                "LastName": $('#LastName').val(),
                "Gender": $('input[name="gender"]:checked').val(),
                "Address": $('#UserAddress').val()
            }
            ajax.doPostAjax(`/${homeController}/SaveCurrentUserBasicDetails`, data, function (result) {
                common.ShowMessage(result);
                if (result.ResultFlag) {
                    commonFunction.HideModel('#Modal');
                }
            });
        }

    },



    BindVendorDetail: function () {

        common.ShowLoader();
        var form;
        ajax.doGetAjax(`/${homeController}/${PartialView}`, function (result) {
            if (result.ResultFlag == false) { common.ShowMessage(result); }
            let user = result.Data;
            // Create table
            form = `<div class="col-md-12 grid-margin stretch-card" style="padding: 0 20% 0;">
                        <div class="card">
                            <div class="card-body">
                                <p class="card-description text-center"> Update Details</p>
			                                   <div class="form-group">
                                        <label>Firm Name<span style="color:red">*</span></label>
                                        <div class="input-group">
                                            <input class="form-control form-control-lg border-left-0" data-val="true" data-val-required="FirmName Required!" id="FirmName" name="FirmName" placeholder="FirmName" type="text" value="${user.FirstName}" />
                                        </div>
                                     
                                    </div>
                                    <div class="form-group">
                                        <label>GST<span style="color:red">*</span></label>
                                        <div class="input-group">
                                            <input class="form-control form-control-lg border-left-0" id="GST" name="GST" placeholder="GST" type="text" value="${user.GSTIN}" />
                                        </div>
                                    
                                    </div>
                                   
                                    <div class="form-group">
                                        <label>PAN<span style="color:red">*</span></label>
                                        <div class="input-group">
                                            <div class="input-group-prepend bg-transparent">
                                                <span class="input-group-text bg-transparent border-right-0">
                                                    <i class="mdi mdi-email-outline text-primary"></i>
                                                </span>
                                            </div>
                                            <input class="form-control form-control-lg border-left-0" data-val="true"  id="PAN" name="PAN" placeholder="PAN" type="text" value="${user.PanNo}" />
                                        </div>
                                        <span class="field-validation-valid text-danger" data-valmsg-for="PAN" data-valmsg-replace="true"></span>
                                    </div>
                                    
                                      <div class="form-group">
                                        <label>Address<span style="color:red">*</span></label>
                                        <div class="input-group">
                                            <div class="input-group-prepend bg-transparent">
                                                <span class="input-group-text bg-transparent border-right-0">
                                                    <i class="mdi mdi-email-outline text-primary"></i>
                                                </span>
                                            </div>
                                            <input class="form-control form-control-lg border-left-0"  id="FullAddress" name="FullAddress" placeholder="Vendor Address" type="text" value="${user.FullAddress}" />
                                        </div>
                                     
                                    </div>
 <div class="form-group">
                                        <label>PIN<span style="color:red">*</span></label>
                                        <div class="input-group">
                                            <div class="input-group-prepend bg-transparent">
                                                <span class="input-group-text bg-transparent border-right-0">
                                                    <i class="mdi mdi-email-outline text-primary"></i>
                                                </span>
                                            </div>
                                            <input class="form-control form-control-lg border-left-0"  id="PIN" name="PIN" placeholder="PIN" type="text" value="${user.PIN}" />
                                        </div>
                                      
                                    </div>
                                    
 <div class="form-group">
                                        <label>City<span style="color:red">*</span></label>
                                        <div class="input-group">
                                            <div class="input-group-prepend bg-transparent">
                                                <span class="input-group-text bg-transparent border-right-0">
                                                    <i class="mdi mdi-email-outline text-primary"></i>
                                                </span>
                                            </div>
                                            <input class="form-control form-control-lg border-left-0"  id="City" name="City" placeholder="City" type="text" value="${user.City}" />
                                        </div>
                                    </div>

 <div class="form-group">
                                        <label>State<span style="color:red">*</span></label>
                                        <div class="input-group">
                                            <div class="input-group-prepend bg-transparent">
                                                <span class="input-group-text bg-transparent border-right-0">
                                                    <i class="mdi mdi-email-outline text-primary"></i>
                                                </span>
                                            </div>
                                            <input class="form-control form-control-lg border-left-0"  id="State" name="State" placeholder="State" type="text" value="${user.State}" />
                                        </div>
                                       
                                    </div>

<div class="form-group">
                                        <label>Address Proof<span style="color:red">*</span></label>
                                        <div class="input-group">
                                            <div class="input-group-prepend bg-transparent">
                                                <span class="input-group-text bg-transparent border-right-0">
                                                    <i class="mdi mdi-email-outline text-primary"></i>
                                                </span>
                                            </div>
                                            <input type="file" name="img[]" id="fileupload" onchange = "profile.readfile(this,'imagePreview')" />
 <div class="input-group">
                                                    <img id="imagePreview"   runat="server" style="border: 1px solid #eee3e3; @display;" height="100" width="100" />
                                                </div>
                                        </div>
                                       
                                    </div>
                                    <div class="mt-3">
                                        <button type="submit" onclick="profile.UpdateVendorDetails();" class="btn btn-block btn-gradient-primary mr-2">Update</button>
                                    </div>

                          </div>
                        </div>
                    </div>`;
            common.HideLoader();
            $('#Modal').children('div').children('div').html(form);
            $('#Modal').show();
        });
    },
    UpdateVendorDetails: function () {
        console.log($('#imagePreview').attr('src'));
        if (profile.ValidateVendorDetail()) {
            data = {
                "FirmName": $('#FirmName').val(),
                "GSTIN": $('#GST').val(),
                "PanNo": $('#PAN').val(),
                "FullAddress": $('#FullAddress').val(),
                "City": $('#City').val(),
                "State": $('#State').val(),
                "PIN": $('#PIN').val(),
                "AddressProof": $('#imagePreview').attr('src')
            }
            ajax.doPostAjax(`/${homeController}/SaveCurrentVendorDetails`, data, function (result) {
                common.ShowMessage(result);
                if (result.ResultFlag) {
                    commonFunction.HideModel('#Modal');
                }
            });
        }

    },
    BindVendorBankDetails: function () {

        common.ShowLoader();
        var form;
        ajax.doGetAjax(`/${homeController}/${PartialView}`, function (result) {
            if (result.ResultFlag == false) { common.ShowMessage(result); }
            let user = result.Data;
            // Create table
            form = `<div class="col-md-12 grid-margin stretch-card" style="padding: 0 20% 0;">
                        <div class="card">
                            <div class="card-body">
                                <p class="card-description text-center"> Update Details</p>
			                                   <div class="form-group">
                                        <label>Account Holder Name<span style="color:red">*</span></label>
                                        <div class="input-group">
                                            <input class="form-control form-control-lg border-left-0" data-val="true" data-val-required="FirmName Required!" id="AccountHolderName" name="AccountHolderName" placeholder="Account Holder Name" type="text" value="${user.AccountHolderName}" />
                                        </div>
                                    
                                    </div>
                                    <div class="form-group">
                                        <label>Acccount Number<span style="color:red">*</span></label>
                                        <div class="input-group">
                                            <input class="form-control form-control-lg border-left-0" id="AccountNumber" name="AccountNumber" placeholder="AccountNumber" type="text" value="${user.AccountNumber}" />
                                        </div>
                                       
                                    </div>
                                   
                                    <div class="form-group">
                                        <label>Bank Name<span style="color:red">*</span></label>
                                        <div class="input-group">
                                            <div class="input-group-prepend bg-transparent">
                                                <span class="input-group-text bg-transparent border-right-0">
                                                    <i class="mdi mdi-email-outline text-primary"></i>
                                                </span>
                                            </div>
                                            <input class="form-control form-control-lg border-left-0" data-val="true"  id="BankName" name="BankName" placeholder="BankName" type="text" value="${user.BankName}" />
                                        </div>
                                     
                                    </div>
                                    
                                      <div class="form-group">
                                        <label>IFSCCode<span style="color:red">*</span></label>
                                        <div class="input-group">
                                            <div class="input-group-prepend bg-transparent">
                                                <span class="input-group-text bg-transparent border-right-0">
                                                    <i class="mdi mdi-email-outline text-primary"></i>
                                                </span>
                                            </div>
                                            <input class="form-control form-control-lg border-left-0"  id="IFSCCode" name="IFSCCode" placeholder="IFSCCode" type="text" value="${user.IFSCCode}" />
                                        </div>
                                       
                                    </div>
                                    <div class="mt-3">
                                        <button type="submit" onclick="profile.UpdateVendorBankDetails();" class="btn btn-block btn-gradient-primary mr-2">Update</button>
                                    </div>

                          </div>
                        </div>
                    </div>`;
            common.HideLoader();
            $('#Modal').children('div').children('div').html(form);
            $('#Modal').show();
        });
    },

    UpdateVendorBankDetails: function () {
        if (profile.ValidateBankDetail()) {
            data = {
                "AccountHolderName": $('#AccountHolderName').val(),
                "AccountNumber": $('#AccountNumber').val(),
                "BankName": $('#BankName').val(),
                "IFSCCode": $('#IFSCCode').val()
            }
            ajax.doPostAjax(`/${homeController}/UpdateVendorBankDetails`, data, function (result) {
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
        form = `<div class="col-md-12 grid-margin stretch-card" style="padding: 0 20% 0;">
                        <div class="card">
                            <div class="card-body">
                                <p class="card-description text-center"> Change Password</p>
			                                   <div class="form-group">
                                        <label>Password<span style="color:red">*</span></label>
                                        <div class="input-group">
                                            <input class="form-control form-control-lg border-left-0" data-val="true"  id="Password" name="Password" placeholder="Enter Password" type="password" />
                                        </div>
                                    
                                    </div>
                                    <div class="form-group">
                                        <label>Confirm Password<span style="color:red">*</span></label>
                                        <div class="input-group">
                                            <input class="form-control form-control-lg border-left-0" id="ConfirmPassword" name="ConfirmPassword" placeholder="Confirm Password" type="password"  />
                                        </div>
                                       
                                    </div>
                                    </div>
                                    <div class="mt-3">
                                        <button type="submit" onclick="profile.UpdateUserPasswordDetails();" class="btn btn-block btn-gradient-primary mr-2">Update</button>
                                    </div>
                          </div>
                        </div>
                    </div>`;
        common.HideLoader();
        $('#Modal').children('div').children('div').html(form);
        $('#Modal').show();

    },

    UpdateUserPasswordDetails: function () {
        if (profile.ValidateUserPasswordDetail()) {
            data = {
                "NewPassword": $('#Password').val(),
                "ConfirmPassword": $('#ConfirmPassword').val()
            }
            alert(data);
            ajax.doPostAjax(`/${accountController}/UpdateCurrentUserPassword`, data, function (result) {
                common.ShowMessage(result);
                if (result.ResultFlag) {
                    commonFunction.HideModel('#Modal');
                }
            });
        }

    },

    ValidateBasicDetail: function () {
        if ($('#FirstName').val() == null || $('#FirstName').val() == 'undefined') {
            toastr.error('Enter First Name!');
            return false;
        } else if ($('#LastName').val() == null || $('#LastName').val() == 'undefined') {
            toastr.error('Enter Last Name!');
            return false;
        } else if ($('#UserAddress').val() == null || $('#UserAddress').val() == 'undefined') {
            toastr.error('Enter Last Name!');
            return false;
        }

        return true;
    },

    ValidateVendorDetail: function () {
        if ($('#FirmName').val() == null || $('#FirmName').val() == 'undefined') {
            toastr.error('Enter Last Name!');
            return false;
        } else if ($('#GST').val() == null || $('#GST').val() == 'undefined') {
            toastr.error('Enter GST !');
            return false;
        } else if ($('#PAN').val() == null || $('#PAN').val() == 'undefined') {
            toastr.error('Enter PAN !');
            return false;
        } else if ($('#FullAddress').val() == null || $('#FullAddress').val() == 'undefined') {
            toastr.error('Enter Address !');
            return false;
        } else if ($('#City').val() == null || $('#City').val() == 'undefined') {
            toastr.error('Enter City !');
            return false;
        } else if ($('#State').val() == null || $('#State').val() == 'undefined') {
            toastr.error('Enter State !');
            return false;
        } else if ($('#PIN').val() == null || $('#PIN').val() == 'undefined') {
            toastr.error('Enter PIN !');
            return false;
        } else if ($('#imagePreview').attr('src') == null || ($('#imagePreview').attr('src') == 'undefined')) {
            toastr.error('Upload Address Proof!');
        }
        return true;
    },

     ValidateBankDetail: function () {
         if ($('#AccountHolderName').val() == null || $('#AccountHolderName').val() == 'undefined') {
             toastr.error('Enter Account Holder Name!');
            return false;
         } else if ($('#AccountNumber').val() == null || $('#AccountNumber').val() == 'undefined') {
             toastr.error('Enter Account Number!');
            return false;
         } else if ($('#BankName').val() == null || $('#IFSCCode').val() == 'undefined') {
             toastr.error('Enter Bank Name !');
            return false;
        }

        return true;
    },

    ValidateUserPasswordDetail: function () {
        if ($('#Password').val() == null || $('#Password').val() == 'undefined') {
            toastr.error('Enter Password!');
            return false;
        } else if ($('#ConfirmPassword').val() == null || $('#ConfirmPassword').val() == 'undefined') {
            toastr.error('Enter Confirm Password !');
            return false;
        } else if ($('#Password').val() != $('#ConfirmPassword').val()) {
            toastr.error('Password and Confirm Password Mismatch !');
            return false;
        }
        return true;

    },

    readfile: function (input, w) {
        if (input.files && input.files[0]) {
            var fileReader = new FileReader(),
                files = input.files,
                file;
            if (!files.length) {
                return;
            }
            file = files[0];
            //var size = parseFloat(file.size).toFixed(2);
            if (/^image\/\w+$/.test(file.type)) {
                fileReader.onload = function (e) {
                    $('#' + w + '').attr('src', e.target.result);
                    $('#' + w + '').attr('style', 'display:block');
                    var image = new Image();
                    image.src = $('#' + w + '').attr("src");
                    image.onload = function () {
                        //alert('width: ' + this.width + ' and height: ' + this.height);
                    };
                }
                fileReader.readAsDataURL(input.files[0]);
            } else {
                $('#' + w + '').attr('src', '');
                $('#' + w + '').attr('style', 'display:none');
                alert("Please choose an image file.");
                //$("#PostedFile").val('');
            }
        }
    }



}
