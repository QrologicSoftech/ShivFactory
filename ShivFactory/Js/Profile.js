let homeArea = 'Home', homeController = 'Home',accountController='Account' ,PartialView ='GetCurrentUserDetails';
var profile = {

    BindBasicDetail: function () {
        common.ShowLoader();
        var form;
        ajax.doGetAjax(`/${homeController}/${PartialView}`, function (result) {
            if (result.ResultFlag == false) { common.ShowMessage(result); }
            let user = result.Data;
            // Create table
            form = `  <h4>Update Basic Details</h4>  <div class="form-text-acount">
                <div class="form-horizontal" name="personal-details" autocomplete="off">
                    <div class="form-fields">
                        <div class="form-group ">
                            <div class="row">
                                <div class="col-sm-5">
                                    <label class="control-label required" for="display_name">First Name</label>
                                </div>

                                <div class="col-sm-7">
                                    <div>
      <input class="form-control"  id="FirstName" name="FirstName" placeholder="FirstName" type="text" value="${user.FirstName == null ? '' : user.FirstName}" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group ">
                            <div class="row">
                                <div class="col-sm-5">
                                    <label class="control-label required" for="display_name">Enter Last Name</label>
                                </div>

                                <div class="col-sm-7">
                                    <div>
                                   <input class="form-control" id="LastName" name="LastName" placeholder="LastName" type="text" value="${user.LastName == null ? '' : user.LastName}" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group ">
                            <div class="row">
                                <div class="col-sm-5">
                                    <label class="control-label required" for="display_name">Gender</label>
                                </div>

                                <div class="col-sm-7">
                                    <div>
                                 <input type="radio" class="form-radio-input" id="male" name="gender" value="Male" >Male</input>  &nbsp; &nbsp;&nbsp; <input type="radio" id="female" name="gender" value="Female">Female</input> 
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

 <div class="form-group ">
                            <div class="row">
                                <div class="col-sm-5">
                                    <label class="control-label required" for="display_name">Address</label>
                                </div>

                                <div class="col-sm-7">
                                    <div>
                                 <input class="form-control" id="UserAddress" name="UserAddress" placeholder="UserAddress" type="text" value="${user.Address == null ? '' : user.Address}" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer form-submit col-md-12 col-sm-12">
                        <div class="action-btn">
                            <span uif-append="submit" class="inline-block">
                                <div class="" uif-fbtype="wrapper" uif-uqid="6462377d-bf38-793c-d2cf-820cae24a976">

                                    <button type="submit" name="submit" onclick="profile.UpdateBasicDetails();" class="btn submit-btn btn-primary" id="save" >SAVE</button>

                                </div>
                            </span>
                        </div>
                    </div>
                </div>
            </div>`;
            common.HideLoader();
            debugger;
            $('#Modal').children('div').children('div').html(form);
          
            $('#Modal').addClass('modal-profile');
            $("input[name='gender'][value='" + user.Gender + "']").prop('checked', true);
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
            form = `  <h4>Update Vendor Details</h4> <div class="form-text-acount">
                <div class="form-horizontal" name="vendor-details" autocomplete="off">
                    <div class="form-fields">
                        <div class="form-group ">
                            <div class="row">
                                <div class="col-sm-5">
                                    <label class="control-label required" for="display_name">Firm Name</label>
                                </div>

                                <div class="col-sm-7">
                                    <div>
      <input class="form-control"  id="FirmName" name="FirmName" placeholder="FirmName" type="text" value="${user.FirmName == null ? '' : user.FirmName}" />
                                      
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group ">
                            <div class="row">
                                <div class="col-sm-5">
                                    <label class="control-label required" for="display_name">Enter GST</label>
                                </div>

                                <div class="col-sm-7">
                                    <div>
                                    <input class="form-control" id="GST" name="GST" placeholder="GST" type="text" value="${user.GSTIN == null ? '' : user.GSTIN}" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group ">
                            <div class="row">
                                <div class="col-sm-5">
                                    <label class="control-label required" for="display_name"> Enter PAN</label>
                                </div>

                                <div class="col-sm-7">
                                    <div>
                                <input class="form-control" id="PAN" name="PAN" placeholder="PAN" type="text" value="${user.PanNo == null ? '' : user.PanNo}" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

 <div class="form-group ">
                            <div class="row">
                                <div class="col-sm-5">
                                    <label class="control-label required" for="display_name">Business Address</label>
                                </div>

                                <div class="col-sm-7">
                                    <div>
                              <input class="form-control"  id="FullAddress" name="FullAddress" placeholder="Vendor Address" type="text" value="${user.FullAddress == null ? '' : user.FullAddress}" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

 <div class="form-group ">
                            <div class="row">
                                <div class="col-sm-5">
                                    <label class="control-label required" for="display_name">Pincode</label>
                                </div>

                                <div class="col-sm-7">
                                    <div>
                             <input class="form-control"  id="PIN" name="PIN" placeholder="PIN" type="text" value="${user.PIN == null ? '' : user.PIN}" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

<div class="form-group ">
                            <div class="row">
                                <div class="col-sm-5">
                                    <label class="control-label required" for="display_name">City</label>
                                </div>

                                <div class="col-sm-7">
                                    <div>
                              <input class="form-control"  id="City" name="City" placeholder="City" type="text" value="${user.City == null ? '' : user.City}" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

<div class="form-group ">
                            <div class="row">
                                <div class="col-sm-5">
                                    <label class="control-label required" for="display_name">State</label>
                                </div>

                                <div class="col-sm-7">
                                    <div>
                           <input class="form-control"  id="State" name="State" placeholder="State" type="text" value="${user.State == null ? '' : user.State}" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

<div class="form-group ">
                            <div class="row">
                                <div class="col-sm-5">
                                    <label class="control-label required" for="display_name">Address</label>
                                </div>

                                <div class="col-sm-7">
                                    <div>
                          <input type="file" name="img[]" id="fileupload" onchange = "common.readfile(this,'imagePreview')" />
 <img id="imagePreview"   runat="server" style="border: 1px solid #eee3e3; @display;" height="100" width="100" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer form-submit col-md-12 col-sm-12">
                        <div class="action-btn">
                            <span uif-append="submit" class="inline-block">
                                <div>

                                    <button type="submit" name="submit" onclick="profile.UpdateVendorDetails();" class="btn submit-btn btn-primary" id="save" validation-on-click="true" analytics-on="click" analytics-category="Manage_Profile" analytics-action="save_modal_store_detail_click" analytics-label="save_modal_store_detail" analytics-state="tracked">SAVE</button>

                                </div>
                            </span>
                        </div>
                    </div>
                </div>
            </div>`;
            common.HideLoader();
            $('#Modal').children('div').children('div').html(form);
            $('#imagePreview').attr('src', user.AddressProofImg);
            $('#Modal').show();
        });
    },
    UpdateVendorDetails: function () {
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
            form = `<div class="form-text-acount">
                <form class="form-horizontal" name="bank-details" autocomplete="off">
                    <div class="form-fields">
                        <div class="form-group ">
                            <div class="row">
                                <div class="col-sm-5">
                                    <label class="control-label required" for="display_name">Account Holder Name</label>
                                </div>

                                <div class="col-sm-7">
                                    <div>
      <input class="form-control" id="AccountHolderName" name="AccountHolderName" placeholder="Account Holder Name" type="text" value="${user.AccountHolderName == null ? '' : user.AccountHolderName}" />
                                      
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group ">
                            <div class="row">
                                <div class="col-sm-5">
                                    <label class="control-label required" for="display_name">Account Number</label>
                                </div>

                                <div class="col-sm-7">
                                    <div>
                                 <input class="form-control" id="AccountNumber" name="AccountNumber" placeholder="AccountNumber" type="text" value="${user.AccountNumber == null ? '' : user.AccountNumber}" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group ">
                            <div class="row">
                                <div class="col-sm-5">
                                    <label class="control-label required" for="display_name">IFSC Code</label>
                                </div>

                                <div class="col-sm-7">
                                    <div>
                                 <input class="form-control"  id="IFSCCode" name="IFSCCode" placeholder="IFSCCode" type="text" value="${user.IFSCCode == null ? '' : user.IFSCCode}" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

 <div class="form-group ">
                            <div class="row">
                                <div class="col-sm-5">
                                    <label class="control-label required" for="display_name">Bank Name</label>
                                </div>

                                <div class="col-sm-7">
                                    <div>
                                <input class="form-control"  id="BankName" name="BankName" placeholder="BankName" type="text" value="${user.BankName == null ? '' : user.BankName}" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
<div class="form-group ">
                            <div class="row">
                                <div class="col-sm-5">
                                    <label class="control-label required" for="display_name">Bank Proof </label>
                                </div>

                                <div class="col-sm-7">
                                    <div>
                          <input type="file" name="img_bank" id="fileupload_bank" onchange = "common.readfile(this,'imagePreviewbank')" />
 <img id="imagePreviewbank" style="border: 1px solid #eee3e3; @display;" height="100" width="100" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal-footer form-submit col-md-12 col-sm-12">
                        <div class="action-btn">
                            <span uif-append="submit" class="inline-block">
                                <div >

                                    <button type="submit" name="submit" onclick="profile.UpdateVendorBankDetails();" class="btn submit-btn btn-primary" id="save" >SAVE</button>

                                </div>
                            </span>
                        </div>
                    </div>
                </form>
            </div>`;
            common.HideLoader();
            $('#Modal').children('div').children('div').html(form);
            $('#imagePreviewbank').attr('src', user.BankProofImg);
            $('#Modal').show();
        });
    },
    UpdateVendorBankDetails: function () {
                if (profile.ValidateBankDetail()) {
            data = {
                "AccountHolderName": $('#AccountHolderName').val(),
                "AccountNumber": $('#AccountNumber').val(),
                "BankName": $('#BankName').val(),
                "IFSCCode": $('#IFSCCode').val(),
                "BankProof": document.getElementById("imagePreviewbank").src //$('#imagePreviewbank').attr('src')
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
        form = `   <h4>Change Password</h4><div class="form-text-acount">
                <div class="form-horizontal" name="bank-details" autocomplete="off">
                    <div class="form-fields">
                        <div class="form-group ">
                            <div class="row">
                                <div class="col-sm-5">
                                    <label class="control-label required" for="display_name">Password</label>
                                </div>

                                <div class="col-sm-7">
                                    <div>
      <input class="form-control" data-val="true"  id="Password" name="Password" placeholder="Enter Password" type="password" />
                                      
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group ">
                            <div class="row">
                                <div class="col-sm-5">
                                    <label class="control-label required" for="display_name">Confirm Password</label>
                                </div>

                                <div class="col-sm-7">
                                    <div>
                                <input class="form-control" id="ConfirmPassword" name="ConfirmPassword" placeholder="Confirm Password" type="password"  />
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                    <div class="modal-footer form-submit col-md-12 col-sm-12">
                        <div class="action-btn">
                            <span uif-append="submit" class="inline-block">
                                <div >
                                    <button type="submit" name="submit" onclick="profile.UpdateUserPasswordDetails();" class="btn submit-btn btn-primary" id="save" >SAVE</button>
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
        if (profile.ValidateUserPasswordDetail()) {
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

    ValidateBasicDetail: function () {
        if ($('#FirstName').val() == null || $('#FirstName').val() == 'undefined' || $('#FirstName').val().length == '0') {
            toastr.error('Enter First Name!');
            return false;
        } else if ($('#LastName').val() == null || $('#LastName').val() == 'undefined' || $('#LastName').val().length == '0') {
            toastr.error('Enter Last Name!');
            return false;
        } else if ($('#UserAddress').val() == null || $('#UserAddress').val() == 'undefined' || $('#UserAddress').val.length =='0') {
            toastr.error('Enter Address!');
            return false;
        }

        return true;
    },

    ValidateVendorDetail: function () {
        if ($('#FirmName').val() == null || $('#FirmName').val() == 'undefined' || $('#FirmName').val().length == '0') {
            toastr.error('Enter Last Name!');
            return false;
        } else if ($('#GST').val() == null || $('#GST').val() == 'undefined' ||  $('#GST').val().length == '0') {
            toastr.error('Enter GST !');
            return false;
        } else if ($('#PAN').val() == null || $('#PAN').val() == 'undefined' || $('#PAN').val().length == '0') {
            toastr.error('Enter PAN !');
            return false;
        } else if ($('#FullAddress').val() == null || $('#FullAddress').val() == 'undefined' || $('#FullAddress').val().length == '0') {
            toastr.error('Enter Address !');
            return false;
        } else if ($('#City').val() == null || $('#City').val() == 'undefined' || $('#City').val().length == '0') {
            toastr.error('Enter City !');
            return false;
        } else if ($('#State').val() == null || $('#State').val() == 'undefined' || $('#State').val().length == '0') {
            toastr.error('Enter State !');
            return false;
        } else if ($('#PIN').val() == null || $('#PIN').val() == 'undefined' || $('#PIN').val().length == '0') {
            toastr.error('Enter PIN !');
            return false;
        } else if ($('#imagePreview').attr('src') == null || $('#imagePreview').attr('src') == 'undefined' || $('#imagePreview').attr('src').length == '0') {
            toastr.error('Upload Address Proof!');
        }
        return true;
    },

    ValidateBankDetail: function () {
        if ($('#AccountHolderName').val() == null || $('#AccountHolderName').val() == 'undefined' || $('#AccountHolderName').val().length == '0') {
             toastr.error('Enter Account Holder Name!');
            return false;
        } else if ($('#AccountNumber').val() == null || $('#AccountNumber').val() == 'undefined' || $('#AccountNumber').val().length == '0') {
             toastr.error('Enter Account Number!');
            return false;
        } else if ($('#IFSCCode').val() == null || $('#IFSCCode').val() == 'undefined' || $('#IFSCCode').val().length == '0') {
             toastr.error('Enter IFSC !');
            return false;
        }
        else if ($('#BankName').val() == null || $('#BankName').val() == 'undefined' || $('#BankName').val().length == '0') {
            toastr.error('Enter Bank Name !');
            return false;
        } else if (document.getElementById("imagePreviewbank").src == null || document.getElementById("imagePreviewbank").src == 'undefined' || document.getElementById("imagePreviewbank").src == '0') {
            toastr.error('Upload  Bank Proof !');
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
