<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Administrator_Default" %>

<!DOCTYPE html>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %> 

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Member Panel</title>
       <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <link rel="shortcut icon" type="image/x-icon" href="assets/images/favicon.png">

    <link rel="stylesheet" href="../public/assets/lib/bootstrap/css/bootstrap.css" />

    <link rel="stylesheet" href="../public/assets/lib/font-awesome/css/font-awesome.css" />

    <link rel="stylesheet" href="../public/assets/css/main.css" />

    <link rel="stylesheet" href="../public/assets/lib/metismenu/metisMenu.css" />

    <link rel="stylesheet" href="../public/assets/lib/onoffcanvas/onoffcanvas.css" />

    <link rel="stylesheet" href="../public/assets/lib/animate.css/animate.css" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" integrity="sha384-wvfXpqpZZVQGK6TAh5PVlGOfQNHSoD2xbE+QkPxCAFlNEevoEH3Sl0sibVcOQVnN" crossorigin="anonymous">
    <style>
.login {
	background-size: cover;
	padding: 15px;
	background-position: bottom;
}
.login .form-signin {
	box-shadow: inherit;
	max-width: 400px;
}
hr {
	border: 0;
}
		#login input {
	margin-bottom: 12px;
	padding-left:50px;
}
.fa-icon .fa {
	position: absolute;
	z-index: 99;
	left: 1px;
	font-size: 22px;
	color: #333;
	top: 1px;
	border-right: 1px solid #bbb;
	padding-right: 4px;
	height: 43px;
	background: #f1f1f1;
	width: 41px;
	text-align: center;
	line-height: 46px;
}

    </style>
    <script type="text/javascript">  
        function SelectDate(e) {
            var PresentDay = new Date();
            var dateOfBirth = e.get_selectedDate();
            var months = (PresentDay.getMonth() - dateOfBirth.getMonth() + (12 * (PresentDay.getFullYear() - dateOfBirth.getFullYear())));
            document.getElementById("txtCONSULTANT_AGE").value = Math.round(months / 12);
        }
    </script> 
   
</head>
<body class="login">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
        <div class="form-signin" >
            <div class="text-center singin-top"  >
                <img src="../../images/logo.png" style="height:105px;width:218px;" alt="" class="img-responsive"  />
            </div>
                  
            <div class="tab-content">
                <div id="login" class="tab-pane active" style="margin-top: -2px;" runat="server" ClientIdMode="Static">

                    <p class="text-muted text-center">
                        Enter your username and passwords
               
                    </p>
                   <div style="position:relative" class="fa-icon"><i class="fa fa-user" aria-hidden="true"></i>
 <asp:TextBox ID="txtUserID" runat="server" placeholder="MemberID" class="form-control top"></asp:TextBox></div>
                   <div style="position:relative" class="fa-icon"> <i class="fa fa-lock" aria-hidden="true"></i>
<asp:TextBox ID="txtPassword" TextMode="Password" runat="server" placeholder="Password" class="form-control bottom"></asp:TextBox></div>
                    <%--<asp:TextBox ID="txtCode" runat="server" placeholder="Security Code" class="form-control bottom"></asp:TextBox>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Administrator/captcha.aspx" Style="width: 25%;" />--%>
                    <div class="checkbox">
                        <label>
                            <asp:CheckBox ID="chkremember" runat="server" />
                            Remember Me
                        </label>

                    </div>
                    <br />
                    <asp:Button ID="btnLogin" runat="server" Text="Sign in" class="btn btn-lg btn-primary btn-block" OnClick="btnLogin_Click" />

                </div>
                <div id="forgot" class="tab-pane">

                    <p class="text-muted text-center">Enter your valid Mobile</p>
                    <asp:TextBox ID="txtforgetuseid" runat="server" placeholder="MemberID" class="form-control top"></asp:TextBox>
                    <asp:TextBox ID="txtforgetpass" runat="server" placeholder="Register Mobile" class="form-control bottom"></asp:TextBox>
                    <br/>
                    <asp:Button ID="btnforgetpass" runat="server" Text="Recover Password" class="btn btn-lg btn-danger btn-block" OnClick="btnforgetpass_Click" />

                </div>
                <div id="signup" class="tab-pane" runat="server" ClientIdMode="Static">
                    
                      <asp:DropDownList ID="ddlMemberPlaceType"  OnSelectedIndexChanged="txtChangeddl" AutoPostBack="true" runat="server" class="form-control dropHeight">
                                        <asp:ListItem Value="0">Select MemberType*</asp:ListItem>
                                        <asp:ListItem Value="1">General</asp:ListItem>
                                        <asp:ListItem Value="2">widow</asp:ListItem>
                                        <asp:ListItem Value="3">Handicapped</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlMemberPlaceType"
                                        ErrorMessage="Please Select MemberType" ValidationGroup="V1" InitialValue="0" SetFocusOnError="True" Display="None">*</asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtDATE_OF_BIRTH" AutoPostBack="true" OnTextChanged="txtDATE_OF_BIRTH_TextChanged" Visible="false" runat="server" Font-Size="12px" class="form-control input-sm" placeholder="DOB"></asp:TextBox>  
                    <%-- <asp:Label ID="Label35" runat="server" CssClass="lbl1" Text="Age"></asp:Label>   
                    <asp:TextBox ID="txtCONSULTANT_AGE" runat="server" ReadOnly="true" AutoPostBack="true"  OnTextChanged="txtCONSULTANT_AGE_TextChanged"></asp:TextBox>--%>
                      <cc1:CalendarExtender runat="server" TargetControlID="txtDATE_OF_BIRTH" Format="yyyy-MM-dd" OnClientDateSelectionChanged="SelectDate" ID="CalendarExtender1"></cc1:CalendarExtender>  
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDATE_OF_BIRTH"
                                        ErrorMessage="Please Select  Dob" ValidationGroup="V1" SetFocusOnError="True" Display="None">*</asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtSponsor" class="form-control input-sm" placeholder="SponsorID*" runat="server" AutoPostBack="True" OnTextChanged="txtSponsor_TextChanged"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSponsor"
                                        ErrorMessage="Please Enter SponsorID" ValidationGroup="V1" SetFocusOnError="True" Display="None">*</asp:RequiredFieldValidator>
                     <asp:Label ID="lblsponsorName" Style="color: #000" runat="server"></asp:Label>

                    <asp:TextBox ID="txtloginid" class="form-control input-sm" placeholder="LoginID*" runat="server" AutoPostBack="True" ></asp:TextBox>                            
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtloginid"
                                        ErrorMessage="Please Enter LoginID" ValidationGroup="V1" SetFocusOnError="True" Display="None">*</asp:RequiredFieldValidator>
                  
                    <asp:TextBox ID="txtname" placeholder="Full Name*" runat="server" class="form-control input-sm"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtname"
                                        ErrorMessage="Please Enter FullName" ValidationGroup="V1" SetFocusOnError="True" Display="None">*</asp:RequiredFieldValidator>

                    <asp:DropDownList ID="ddlcareof" runat="server" class="form-control dropHeight" placeholder="Care *">
                        <asp:ListItem Value="0">Select Title*</asp:ListItem>
                                        <asp:ListItem Value="1">S/O</asp:ListItem>
                                        <asp:ListItem Value="2">W/O</asp:ListItem>
                                        <asp:ListItem Value="3">D/O</asp:ListItem>
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlcareof"
                                        ErrorMessage="Please Select Position" ValidationGroup="V1" InitialValue="0" SetFocusOnError="True" Display="None">*</asp:RequiredFieldValidator>
                  --%>

                     <asp:TextBox ID="txtcareofname" placeholder="Care of name*" runat="server" class="form-control input-sm"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtcareofname"
                                        ErrorMessage="Please Enter FatherName" ValidationGroup="V1" SetFocusOnError="True" Display="None">*</asp:RequiredFieldValidator>

                <asp:TextBox ID="txtEmailID" placeholder="EmaiID" runat="server" class="validate[<%--required,--%>custom[email]] form-control input-sm"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmailID" ErrorMessage="Invalid Email Format" Display="None">*</asp:RegularExpressionValidator>
                     <asp:TextBox ID="txtMobile" placeholder="MobileNo*" runat="server" MaxLength="10" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMobile"
                                        ErrorMessage="Please Enter MobileNo" ValidationGroup="V1" SetFocusOnError="True" Display="None">*</asp:RequiredFieldValidator>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True"
                                        TargetControlID="txtMobile" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtMobile" ID="RegularExpressionValidator1" ValidationExpression="^[\s\S]{10,}$" runat="server" ErrorMessage="Minimum 10 characters allowed."></asp:RegularExpressionValidator>

                    <asp:DropDownList ID="ddlPackage" runat="server" Visible="false" class="form-control dropHeight"></asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlPackage"
                                                    ErrorMessage="Please Select Package" ValidationGroup="V" InitialValue="0" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>--%>
                    <%--<asp:TextBox ID="txtpin" class="form-control input-sm" Visible="false" placeholder="Pin*" runat="server" AutoPostBack="True" ></asp:TextBox>             
                   <asp:RequiredFieldValidator ID="RequiredFieldValidatorpin" runat="server" ControlToValidate="txtpin"
                                        ErrorMessage="Please Enter Pin" ValidationGroup="V1" SetFocusOnError="True" Display="None">*</asp:RequiredFieldValidator>--%>

                 <%--<asp:DropDownList ID="ddlState" runat="server" class="form-control dropHeight"></asp:DropDownList>--%>
                    <asp:DropDownList ID="ddlcity" runat="server" class="form-control dropHeight"></asp:DropDownList>
                    <asp:DropDownList ID="ddlposition" runat="server" class="form-control dropHeight">
                                        <asp:ListItem Value="0">Select Position*</asp:ListItem>
                                        <asp:ListItem Value="L">Left</asp:ListItem>
                                        <asp:ListItem Value="R">Right</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlposition"
                                        ErrorMessage="Please Select Position" ValidationGroup="V1" InitialValue="0" SetFocusOnError="True" Display="None">*</asp:RequiredFieldValidator>
                  
                    <asp:TextBox ID="txtPasswordReg" MaxLength="6" placeholder="password"  TextMode="Password" runat="server" class="form-control input-sm"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPasswordReg"
                                        ErrorMessage="Please Enter Password" ValidationGroup="V1" SetFocusOnError="True" Display="None">*</asp:RequiredFieldValidator>
                                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                                TargetControlID="txtPasswordReg" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtPasswordReg" ID="RegularExpressionValidator83" ValidationExpression="^[\s\S]{6,}$" runat="server" ErrorMessage="Minimum 6 characters allowed."></asp:RegularExpressionValidator>
                     <asp:TextBox ID="txtconfirmpass" MaxLength="6" placeholder="Confirm password"  TextMode="Password" runat="server" class="form-control input-sm"></asp:TextBox>
                                    <asp:CompareValidator runat="server" ID="cmpNumbers" ControlToValidate="txtconfirmpass" ControlToCompare="txtPasswordReg" Operator="Equal" Display="Dynamic" Type="String" ErrorMessage="Password Mismatch" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtconfirmpass"
                                        ErrorMessage="Please Enter Confirm Password" ValidationGroup="V1" SetFocusOnError="True" Display="None">*</asp:RequiredFieldValidator>
                   
                <asp:Button ID="btnSubmit" runat="server" Text="Register" ValidationGroup="V1" class="btn btn-lg btn-success btn-block" OnClick="btnSubmit_Click" />
                  
                </div>
            </div>
            <hr>
            <div class="text-center">
                <ul class="list-inline">
                    <li><a style="color:#2C0075;font-size:16px;" class="text-muted" href="#login" data-toggle="tab">Login</a></li>                  
                    <li style="position:relative"><a style="color:#2C0075;font-size:16px;" class="text-muted" href="#signup" data-toggle="tab">Register Now!</a></li>
                    <li><a style="color:#2C0075;font-size:16px;" class="text-muted" href="#forgot" data-toggle="tab">Forgot Password?</a></li>
                </ul>
            </div>
        </div>
        
        
           
        <!--jQuery -->
        <script src="../Public/assets/lib/jquery/jquery.js"></script>

        <!--Bootstrap -->
        <script src="../Public/assets/lib/bootstrap/js/bootstrap.js"></script>


        <script type="text/javascript">
            (function ($) {
                $(document).ready(function () {
                    $('.list-inline li > a').click(function () {
                        var activeForm = $(this).attr('href') + ' > form';
                        //console.log(activeForm);
                        $(activeForm).addClass('animated fadeIn');
                        //set timer to 1 seconds, after that, unload the animate animation
                        setTimeout(function () {
                            $(activeForm).removeClass('animated fadeIn');
                        }, 1000);
                    });
                });
            })(jQuery);
        </script>
    </form>
</body>
</html>
