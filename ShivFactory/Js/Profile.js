let homeArea = 'Home', homeController = 'Home' ,PartialView ='GetCurrentUserDetails';
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
                                        <label>FirstName<span style="color:red">*</span></label>
                                        <div class="input-group">
                                            <input class="form-control form-control-lg border-left-0" data-val="true" data-val-required="FirstName Required!" id="FirstName" name="FirstName" placeholder="FirstName" type="text" value="${user.FirstName}" />
                                        </div>
                                        <span class="field-validation-valid text-danger" data-valmsg-for="FirstName" data-valmsg-replace="true"></span>
                                    </div>
                                    <div class="form-group">
                                        <label>LastName<span style="color:red">*</span></label>
                                        <div class="input-group">
                                            <input class="form-control form-control-lg border-left-0" id="LastName" name="LastName" placeholder="LastName" type="text" value="${user.LastName}" />
                                        </div>
                                        <span class="field-validation-valid text-danger" data-valmsg-for="LastName" data-valmsg-replace="true"></span>
                                    </div>
                                   
                                    <div class="form-group">
                                        <label>Email<span style="color:red">*</span></label>
                                        <div class="input-group">
                                            <div class="input-group-prepend bg-transparent">
                                                <span class="input-group-text bg-transparent border-right-0">
                                                    <i class="mdi mdi-email-outline text-primary"></i>
                                                </span>
                                            </div>
                                            <input class="form-control form-control-lg border-left-0" data-val="true" data-val-email="Invalid Email Address" data-val-required="Email Required!" id="EmailId" name="EmailId" placeholder="Email" type="text" value="${user.Email}" />
                                        </div>
                                        <span class="field-validation-valid text-danger" data-valmsg-for="EmailId" data-valmsg-replace="true"></span>
                                    </div>
                                    
                                      <div class="form-group">
                                        <label>Mobile<span style="color:red">*</span></label>
                                        <div class="input-group">
                                            <div class="input-group-prepend bg-transparent">
                                                <span class="input-group-text bg-transparent border-right-0">
                                                    <i class="mdi mdi-email-outline text-primary"></i>
                                                </span>
                                            </div>
                                            <input class="form-control form-control-lg border-left-0"  id="EmailId" name="Mobile" placeholder="Mobile" type="text" value="${user.Mobile}" />
                                        </div>
                                        <span class="field-validation-valid text-danger" data-valmsg-for="EmailId" data-valmsg-replace="true"></span>
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
       
        //if (!$('FirstName').val()){
        //    $('FirstName').addClass("error");
        //} else{
        //    $('FirstName').removeClass("error");
        //}
        //then call api to save 


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
                                        <span class="field-validation-valid text-danger" data-valmsg-for="FirstName" data-valmsg-replace="true"></span>
                                    </div>
                                    <div class="form-group">
                                        <label>GST<span style="color:red">*</span></label>
                                        <div class="input-group">
                                            <input class="form-control form-control-lg border-left-0" id="GST" name="GST" placeholder="GST" type="text" value="${user.GSTIN}" />
                                        </div>
                                        <span class="field-validation-valid text-danger" data-valmsg-for="GST" data-valmsg-replace="true"></span>
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
                                            <input class="form-control form-control-lg border-left-0"  id="Address" name="Address" placeholder="Address" type="text" value="${user.FullAddress}" />
                                        </div>
                                        <span class="field-validation-valid text-danger" data-valmsg-for="Address" data-valmsg-replace="true"> Address Required</span>
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
                                        <span class="field-validation-valid text-danger">City Required</span>
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
                                        <span class="field-validation-valid text-danger" ></span>
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

        //if (!$('FirstName').val()){
        //    $('FirstName').addClass("error");
        //} else{
        //    $('FirstName').removeClass("error");
        //}
        //then call api to save 


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
                                        <span class="field-validation-valid text-danger" data-valmsg-for="FirstName" data-valmsg-replace="true"></span>
                                    </div>
                                    <div class="form-group">
                                        <label>Acccount Number<span style="color:red">*</span></label>
                                        <div class="input-group">
                                            <input class="form-control form-control-lg border-left-0" id="AccountNumber" name="AccountNumber" placeholder="AccountNumber" type="text" value="${user.AccountNumber}" />
                                        </div>
                                        <span class="field-validation-valid text-danger" data-valmsg-for="GST" data-valmsg-replace="true"></span>
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
                                        <span class="field-validation-valid text-danger" data-valmsg-for="PAN" data-valmsg-replace="true"></span>
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
                                        <span class="field-validation-valid text-danger" data-valmsg-for="Address" data-valmsg-replace="true"> Address Required</span>
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

    }

   
}


