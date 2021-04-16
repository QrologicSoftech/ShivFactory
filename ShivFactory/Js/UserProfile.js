var userprofile = {

    BindBasicDetail: function () {
        common.ShowLoader();
        var form;
        ajax.doGetAjax(`/Home/GetCurrentUserDetails`, function (result) {
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
            $('#Modal').addClass('modal-profile');
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
            ajax.doPostAjax(`/Home/SaveCurrentUserBasicDetails`, data, function (result) {
                common.ShowMessage(result);
                if (result.ResultFlag) {
                    commonFunction.HideModel('#Modal');
                }
            });
        }
    },

    BindMobileDetail: function () {
        common.ShowLoader();
        ajax.doGetAjax(`/Home/GetCurrentUserDetails`, function (result) {
            if (result.ResultFlag == false) { common.ShowMessage(result); }
            let user = result.Data;
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
  <label class="control-label required" for="display_name">Mobile</label>    <input class="form-control" data-val="true"  id="Mobile" name="Mobile" placeholder="Enter Mobile" type="mobile" value = "${user.Mobile == null ? "" : user.Mobile}" />
                                      
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
            $('#Modal').addClass('modal-profile');
            $('#Modal').children('div').children('div').html(form);
            $('#Modal').show();
        });
    },
    UpdateMobileDetail: function () {
        if (userprofile.ValidateUserMobileDetail()) {
            data = {
                "Mobile": $('#Mobile').val(),
            }
            ajax.doPostAjax(`/Account/UpdateCurrentUserMobile`, data, function (result) {
                common.ShowMessage(result);
                if (result.ResultFlag) {
                    commonFunction.HideModel('#Modal');
                }
            });
        }

    },

    BindEmailDetail: function () {
        common.ShowLoader();
        ajax.doGetAjax(`/Home/GetCurrentUserDetails`, function (result) {
            if (result.ResultFlag == false) { common.ShowMessage(result); }
            let user = result.Data;
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
  <label class="control-label required" for="display_name">Email</label>    <input required class="form-control"   id="Email" name="Email" placeholder="Enter Email" type ="email" value = "${user.Email == null ? "" : user.Email}"  />
                                      
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
            $('#Modal').addClass('modal-profile');
            $('#Modal').children('div').children('div').html(form);
            $('#Modal').show();
        });

    },
    UpdateEmailDetail: function () {
        if (userprofile.ValidateEmailDetail()) {
            data = {
                "Email": $('#Email').val(),
            }
            ajax.doPostAjax(`/Account/UpdateCurrentUserEmail`, data, function (result) {
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
        $('#Modal').addClass('modal-profile');
        $('#Modal').children('div').children('div').html(form);
        $('#Modal').show();

    },
    UpdateUserPasswordDetails: function () {
        if (userprofile.ValidateUserPasswordDetail()) {
            data = {
                "NewPassword": $('#Password').val(),
                "ConfirmPassword": $('#ConfirmPassword').val()
            }
            ajax.doPostAjax(`/Account/UpdateCurrentUserPassword`, data, function (result) {
                common.ShowMessage(result);
                if (result.ResultFlag) {
                    commonFunction.HideModel('#Modal');
                }
            });
        }

    },

    ValidateMobileDetail: function () {
        if (confirm("Are you sure want to change mobile Number.")) {
            if ($('#Mobile').val() == null || $('#Mobile').val() == 'undefined' || $('#Mobile').val().length == '0') {
                toastr.error('Enter Mobile!');
                return false;
            }

            return true;
        }
    },

    ValidateEmailDetail: function () {
        if ($('#Email').val() == null || $('#Email').val() == 'undefined' || $('#Email').val().length == '0') {
            toastr.error('Enter Email!');
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
    },

    ValidateAddressDetail: function () {
        debugger;
        if ($('#Name').val() == null || $('#Name').val() == 'undefined' || $('#Name').val().length == '0') {
            toastr.error('Enter Name!');
            return false;
        } else if ($('#Phone').val() == null || $('#Phone').val() == 'undefined' || $('#Phone').val().length == '0') {
            toastr.error('Enter Phone Number!');
            return false;
        } else if ($('#Pincode').val() == null || $('#Pincode').val() == 'undefined' || $('#Pincode').val().length == '0') {
            toastr.error('Enter Pincode');
            return false;
        } else if ($('#AddressLine1').val() == null || $('#AddressLine1').val() == 'undefined' || $('#AddressLine1').val().length == '0') {
            toastr.error('Enter address line 1');
            return false;
        } else if ($('#AddressLine2').val() == null || $('#AddressLine2').val() == 'undefined' || $('#AddressLine2').val().length == '0') {
            toastr.error('Enter address line 2');
            return false;
        } else if ($('#Address3').val() == null || $('#Address3').val() == 'undefined' || $('#Address3').val().length == '0') {
            toastr.error('Enter address line 3');
            return false;
        } else if ($('#City').val() == null || $('#City').val() == 'undefined' || $('#City').val().length == '0') {
            toastr.error('Enter City !');
            return false;
        } else if ($('#state option:selected').val() == null || $('#state option:selected').val() == 'undefined' || $('#state option:selected').val().length == '0') {
            toastr.error('Select state');
            return false;
        } else if ($('input[name="locationTypeTag"]:checked').val() == null || $('input[name="locationTypeTag"]:checked').val() == 'undefined' || $('input[name="locationTypeTag"]:checked').val().length == '0') {
            toastr.error('Select Address type');
            return false;
        }

        return true;
    },

    ValidateAddressModalDetail: function () {
        debugger;
        if ($('#m_Name').val() == null || $('#m_Name').val() == 'undefined' || $('#m_Name').val().length == '0') {
            toastr.error('Enter Name!');
            return false;
        } else if ($('#m_Phone').val() == null || $('#m_Phone').val() == 'undefined' || $('#m_Phone').val().length == '0') {
            toastr.error('Enter Phone Number!');
            return false;
        } else if ($('#m_Pincode').val() == null || $('#m_Pincode').val() == 'undefined' || $('#m_Pincode').val().length == '0') {
            toastr.error('Enter Pincode');
            return false;
        } else if ($('#m_AddressLine1').val() == null || $('#m_AddressLine1').val() == 'undefined' || $('#m_AddressLine1').val().length == '0') {
            toastr.error('Enter address line 1');
            return false;
        } else if ($('#m_AddressLine2').val() == null || $('#m_AddressLine2').val() == 'undefined' || $('#m_AddressLine2').val().length == '0') {
            toastr.error('Enter address line 2');
            return false;
        } else if ($('#m_Address3').val() == null || $('#m_Address3').val() == 'undefined' || $('#m_Address3').val().length == '0') {
            toastr.error('Enter address line 3');
            return false;
        } else if ($('#m_City').val() == null || $('#m_City').val() == 'undefined' || $('#m_City').val().length == '0') {
            toastr.error('Enter City !');
            return false;
        } else if ($('#m_state option:selected').val() == null || $('#m_state option:selected').val() == 'undefined' || $('#m_state option:selected').val().length == '0') {
            toastr.error('Select state');
            return false;
        } else if ($('input[name="m_locationTypeTag"]:checked').val() == null || $('input[name="m_locationTypeTag"]:checked').val() == 'undefined' || $('input[name="m_locationTypeTag"]:checked').val().length == '0') {
            toastr.error('Select Address type');
            return false;
        }

        return true;
    },


    BindAddressDetails: function (addressid) {
        common.ShowLoader();
        ajax.doGetAjax(`/Customer/GetAddressByAddressID?ID=`+ addressid, function (result) {
            if (result.ResultFlag = false) { common.ShowMessage(result); }
                let address = result.Data;
                var form = `  <div class="N5Ijry">
                                    <div class="yLyjMK">
                                        <div class="_1hGj33">
                                            <div class="_1lRtwc _1Jqgld">
                                                <input type="text" class="_1w3ZZo _2mFmU7" id="m_Name" required="" autocomplete="name" tabindex="1" value="${address.UserName == null ? "" : address.UserName}"><label for="name" class="_1osQq7 -FxG57">Name</label>
                                            </div>
                                            <div class="_1lRtwc _1Jqgld">
                                                <input type="text" class="_1w3ZZo _2mFmU7" id="m_Phone" required="" maxlength="10" autocomplete="tel" tabindex="2" value="${address.Phone == null ? "" : address.Phone}">
                                                <label for="phone" class="_1osQq7 -FxG57">10-digit mobile number</label>
                                            </div>
                                        </div>
                                        <div class="_1hGj33">
                                            <div class="_1lRtwc _1Jqgld">
                                                <input type="text" class="_1w3ZZo _2mFmU7" id="m_Pincode"  maxlength="6" autocomplete="postal-code" tabindex="3" value="${address.Pincode == null ? "" : address.Pincode}">
                                                <label for="pincode" class="_1osQq7 -FxG57">Pincode</label>
                                            </div>
                                            <div class="_1lRtwc _1Jqgld">
                                                <input type="text" class="_1w3ZZo _2mFmU7"  id="m_AddressLine2" tabindex="4" value="${address.Address2 == null ? "" : address.Address2}">
                                                <label for="addressLine2" class="_1osQq7 -FxG57">Locality</label>
                                            </div>
                                        </div>
                                        <div class="_1hGj33 _3kco7L">
                                            <div class="GTbXbG _2kJObl">
                                                <div class="_1Y2dIb _1Jqgld">
                                                    <textarea class="_1sQQBU _1w3ZZo _1TO48q" id="m_AddressLine1" rows="2" cols="10" tabindex="5" value ="${address.Address1 == null ? "" : address.Address1}" required="" autocomplete="street-address"></textarea>
                                                    <label for="addressLine1" class="_1osQq7 -FxG57">Address (Area and Street)</label>
                                                </div>
                                                <div class="_1fa_Yn _18Y7Fu"></div>
                                            </div>
                                        </div>
                                        <div class="_1hGj33">
                                            <div class="GTbXbG _1lRtwc _3kco7L">
                                                <div class="_1Y2dIb _1Jqgld">
                                                    <input type="text" class="_1w3ZZo _2mFmU7" id="m_City" required="" autocomplete="City" tabindex="6"  value ="${address.City == null ? "" : address.City}">
                                                    <label for="city" class="_1osQq7 -FxG57">City/District/Town</label>
                                                </div>
                                                <div class="_1fa_Yn _18Y7Fu"></div>
                                            </div>
                                            <div>
                                                <div class="_1fB71V _3kco7L">
                                                    <div class="MnyFPx ">State</div>
                                                    <div class="_1cpOwe _1lRtwc jE2jGc">
                                                        <select class="_1EDlbo _1lRtwc jE2jGc _2gIrb5" id="m_state" required="" tabindex="7">
                                                            <option value="" selected="">--Select State--</option>
                                                            <option value="Andaman &amp; Nicobar Islands">Andaman &amp; Nicobar Islands</option>
                                                            <option value="Andhra Pradesh">Andhra Pradesh</option>
                                                            <option value="Arunachal Pradesh">Arunachal Pradesh</option>
                                                            <option value="Assam">Assam</option>
                                                            <option value="Bihar">Bihar</option>
                                                            <option value="Chandigarh">Chandigarh</option>
                                                            <option value="Chhattisgarh">Chhattisgarh</option>
                                                            <option value="Dadra &amp; Nagar Haveli &amp; Daman &amp; Diu">Dadra &amp; Nagar Haveli &amp; Daman &amp; Diu</option>
                                                            <option value="Delhi">Delhi</option>
                                                            <option value="Goa">Goa</option>
                                                            <option value="Gujarat">Gujarat</option>
                                                            <option value="Haryana">Haryana</option>
                                                            <option value="Himachal Pradesh">Himachal Pradesh</option>
                                                            <option value="Jammu &amp; Kashmir">Jammu &amp; Kashmir</option>
                                                            <option value="Jharkhand">Jharkhand</option>
                                                            <option value="Karnataka">Karnataka</option>
                                                            <option value="Kerala">Kerala</option>
                                                            <option value="Ladakh">Ladakh</option>
                                                            <option value="Lakshadweep">Lakshadweep</option>
                                                            <option value="Madhya Pradesh">Madhya Pradesh</option>
                                                            <option value="Maharashtra">Maharashtra</option>
                                                            <option value="Manipur">Manipur</option>
                                                            <option value="Meghalaya">Meghalaya</option>
                                                            <option value="Mizoram">Mizoram</option>
                                                            <option value="Nagaland">Nagaland</option>
                                                            <option value="Odisha">Odisha</option>
                                                            <option value="Puducherry">Puducherry</option>
                                                            <option value="Punjab">Punjab</option>
                                                            <option value="Rajasthan">Rajasthan</option>
                                                            <option value="Sikkim">Sikkim</option>
                                                            <option value="Tamil Nadu">Tamil Nadu</option>
                                                            <option value="Telangana">Telangana</option>
                                                            <option value="Tripura">Tripura</option>
                                                            <option value="Uttarakhand">Uttarakhand</option>
                                                            <option value="Uttar Pradesh">Uttar Pradesh</option>
                                                            <option value="West Bengal">West Bengal</option>
                                                        </select>
                                                        <span class="_2NY3xT kYuoGH"></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="_1hGj33">
                                            <div class="_1lRtwc _1Jqgld">
                                                <input type="text" class="_1w3ZZo _2mFmU7" id="m_Address3" autocomplete="off" tabindex="8" maxlength="200"  value ="${address.Address3 == null ? "" : address.Address3}">
                                                <label for="landmark" class="_1osQq7 -FxG57">Landmark (Optional)</label>
                                            </div>
                                            
                                        </div>
                                        <div class="yI40P1">
                                            <p class="_2tiHgk">Address Type</p>
                                            <div class="_3TMnFu">
                                                <div>
                                                    <label for="HOME" class="_2Fn-Ln _3iI7Qn _3L7Pww">
                                                        <input type="radio" class="_3DAmyP" name="m_locationTypeTag" value="HOME">
                                                        <div class="_1XFPmK"></div>
                                                        <div class="_2jIO64">
                                                            <span>Home (All day delivery)</span>
                                                        </div>
                                                    </label>
                                                    <label for="WORK" class="_2Fn-Ln _3iI7Qn">
                                                        <input type="radio" class="_3DAmyP" name="locationTypeTag"  value="HOME">
                                                        <div class="_1XFPmK"></div>
                                                        <div class="_2jIO64">
                                                            <span>Work (Delivery between 10 AM - 5 PM)</span>
                                                        </div>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="l5QiYB _1hGj33">
                                            <button class="_2KpZ6l _1JDhFS _3AWRsL" type="button" onclick="userprofile.UpdateAddress('`+addressid+`')" tabindex="10">Save</button>
                                        </div>
                                    </div>
                                </div>`;
            $('#Modal').addClass('modal-profile');
            $("input[name='m_locationTypeTag'][value='" + address.Addresstype + "']").prop('checked', true);
            $("#m_state").val(address.State == null ? "" : address.State).change();
            common.HideLoader();
            $('#Modal').children('div').children('div').html(form);
            $('#Modal').show();
        });
    },

    SaveAddress: function () {
        if (userprofile.ValidateAddressDetail()) {
            var data = {
                "UserName": $('#Name').val(),
                "Phone": $('#Phone').val(),
                "Pincode": $('#Pincode').val(),
                "Address1": $('#AddressLine1').val(),
                "Address2": $('#AddressLine2').val(),
                "Address3": $('#Address3').val(),
                "City": $('#City').val(),
                "State": $('#state option:selected').text(),
                "Addresstype": $('input[name="locationTypeTag"]:checked').val()
            }
            ajax.doPostAjax(`/Customer/Address`, data, function (result) {
                common.ShowMessage(result);
                if (result.ResultFlag) {
                    commonFunction.HideModel('#Modal');
                    common.ShowMessage(result);
                }
            });
        }
    },
    UpdateAddress: function (addressid) {
        if (userprofile.ValidateAddressModalDetail()) {
            var data = {
                "UserName": $('#m_Name').val(),
                "Phone": $('#m_Phone').val(),
                "Pincode": $('#m_Pincode').val(),
                "Address1": $('#m_AddressLine1').val(),
                "Address2": $('#m_AddressLine2').val(),
                "Address3": $('#m_Address3').val(),
                "City": $('#m_City').val(),
                "State": $('#m_state option:selected').text(),
                "Addresstype": $('input[name="m_locationTypeTag"]:checked').val(),
                "ID": addressid
            }
            ajax.doPostAjax(`/Customer/Address`, data, function (result) {
                common.ShowMessage(result);
                if (result.ResultFlag) {
                    commonFunction.HideModel('#Modal');
                    common.ShowMessage(result);
                }
            });
        }
    },

    ShowAddressForm: function () {
        $('#addressList').css("display", "none");
        $('#addressform').css("display", "block");
    },

    ShowAddressList: function () {
        $('#addressList').css("display", "block");
        $('#addressform').css("display", "none");
    }
}
