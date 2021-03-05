var profile = {

    BindBasicDetail: function () {
        common.ShowLoader();
        // Create table
        var form = `<div class="col-md-12 grid-margin stretch-card" style="padding: 0 20% 0;">
                        <div class="card">
                            <div class="card-body">
                                <p class="card-description text-center"> Update Details</p>
			                                   <div class="form-group">
                                        <label>FirstName<span style="color:red">*</span></label>
                                        <div class="input-group">
                                            <input class="form-control form-control-lg border-left-0" data-val="true" data-val-required="FirstName Required!" id="FirstName" name="FirstName" placeholder="FirstName" type="text" value="" />
                                        </div>
                                        <span class="field-validation-valid text-danger" data-valmsg-for="FirstName" data-valmsg-replace="true"></span>
                                    </div>
                                    <div class="form-group">
                                        <label>LastName<span style="color:red">*</span></label>
                                        <div class="input-group">
                                            <input class="form-control form-control-lg border-left-0" id="LastName" name="LastName" placeholder="LastName" type="text" value="" />
                                        </div>
                                        <span class="field-validation-valid text-danger" data-valmsg-for="LastName" data-valmsg-replace="true"></span>
                                    </div>
                                    <div class="form-group">
                                        <label>UserName<span style="color:red">*</span></label>
                                        <div class="input-group">
                                            <div class="input-group-prepend bg-transparent">
                                                <span class="input-group-text bg-transparent border-right-0">
                                                    <i class="mdi mdi-account-outline text-primary"></i>
                                                </span>
                                            </div>
                                            <input class="form-control form-control-lg border-left-0" data-val="true" data-val-regex="Invalid format." data-val-regex-pattern="^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$" data-val-required="Phone Number Required!" id="PhoneNumber" name="PhoneNumber" placeholder="Username" type="text" value="" />

                                        </div>
                                        <span class="field-validation-valid text-danger" data-valmsg-for="PhoneNumber" data-valmsg-replace="true"></span>
                                    </div>
                                    <div class="form-group">
                                        <label>Email<span style="color:red">*</span></label>
                                        <div class="input-group">
                                            <div class="input-group-prepend bg-transparent">
                                                <span class="input-group-text bg-transparent border-right-0">
                                                    <i class="mdi mdi-email-outline text-primary"></i>
                                                </span>
                                            </div>
                                            <input class="form-control form-control-lg border-left-0" data-val="true" data-val-email="Invalid Email Address" data-val-required="Email Required!" id="EmailId" name="EmailId" placeholder="Email" type="text" value="" />
                                        </div>
                                        <span class="field-validation-valid text-danger" data-valmsg-for="EmailId" data-valmsg-replace="true"></span>
                                    </div>
                                    <div class="form-group">
                                        <label>Password<span style="color:red">*</span></label>
                                        <div class="input-group">
                                            <div class="input-group-prepend bg-transparent">
                                                <span class="input-group-text bg-transparent border-right-0">
                                                    <i class="mdi mdi-lock-outline text-primary"></i>
                                                </span>
                                            </div>
                                            <input class="form-control form-control-lg border-left-0" data-val="true" data-val-length="The Password must be at least 6 characters long." data-val-length-max="20" data-val-length-min="6" data-val-required="The Password field is required." id="Password" name="Password" placeholder="Password" type="text" value="" />
                                        </div>
                                        <span class="field-validation-valid text-danger" data-valmsg-for="Password" data-valmsg-replace="true"></span>
                                    </div>
                                    <div class="form-group">
                                        <label>Confirm Password<span style="color:red">*</span></label>
                                        <div class="input-group">
                                            <div class="input-group-prepend bg-transparent">
                                                <span class="input-group-text bg-transparent border-right-0">
                                                    <i class="mdi mdi-lock-outline text-primary"></i>
                                                </span>
                                            </div>
                                            <input class="form-control form-control-lg border-left-0" data-val="true" data-val-equalto="The password and confirmation password do not match." data-val-equalto-other="*.Password" id="ConfirmPassword" name="ConfirmPassword" placeholder="ConfirmPassword" type="text" value="" />
                                        </div>
                                        <span class="field-validation-valid text-danger" data-valmsg-for="ConfirmPassword" data-valmsg-replace="true"></span>
                                    </div>
                                    
                                    <div class="mt-3">
                                        <button type="submit" class="btn btn-block btn-gradient-primary mr-2">Update</button>
                                    </div>

                          </div>
                        </div>
                    </div>
               
`;

        debugger;
        $('#Modal').children('div').children('div').html(form);
        $('#Model').show();
        common.HideLoader(); 
    }
    

   
}


