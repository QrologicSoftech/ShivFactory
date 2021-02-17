using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.IO;
using Common;
using System.Data.SqlClient;


public partial class Administrator_Default : System.Web.UI.Page
{
    
    Cls_Connection cls = new Cls_Connection();
    DataTable dt = new DataTable();
    DataTable dtMenuAdmin = new DataTable();
    DataTable dtCompanyAdmin = new DataTable();
    SMSFUN smsfn = new SMSFUN();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            cls.BindDropDownList(ddlPackage, "Select * from tblMLM_Package Where IsActive=1", "PackageName", "PackageID");
            cls.BindDropDownList(ddlcity, "select CityID,CityName From tbl_CityMaster where StateID=26 order by CityName asc", "CityName", "CityID");
            ddlcity.Items.Insert(0, new ListItem("Select City", "0"));
            if (Request.QueryString["MemberID"] != null && Request.QueryString["position"] != null)
            {
                txtSponsor.Text = Encryption.Decrypt(Request.QueryString["MemberID"].ToString());
                txtSponsor_TextChanged(null, null);
                ddlcareof.SelectedIndex = -1;
                txtSponsor.Enabled = false;
                ddlposition.SelectedValue = Request.QueryString["position"].ToString();
                ddlposition.Enabled = false;
                signup.Attributes["class"] = "tab-pane active";
                login.Attributes["class"] = "tab-pane";
            }

            if (Request.QueryString["MemberID"] != null && Request.QueryString["position"] == null)
            {
                txtSponsor.Text = Encryption.Decrypt(Request.QueryString["MemberID"].ToString());
                txtSponsor_TextChanged(null, null);
                txtSponsor.Enabled = false;
                //ddlposition.SelectedValue = Request.QueryString["position"].ToString();
                //ddlposition.Enabled = false;
                signup.Attributes["class"] = "tab-pane active";
                login.Attributes["class"] = "tab-pane";
            }
            if (Request.QueryString["SP"] != null)
            {
                signup.Attributes["class"] = "tab-pane active";
                login.Attributes["class"] = "tab-pane";
            }
        }
    }
    protected void txtChangeddl(object sender, EventArgs e)
    {
        if (Convert.ToString(ddlMemberPlaceType.SelectedItem) != "General")
        {          
            txtDATE_OF_BIRTH.Visible = true;
            signup.Attributes["class"] = "tab-pane active";
            login.Attributes["class"] = "tab-pane";
            RequiredFieldValidator9.Enabled = true;
        }
        else
        {
            txtDATE_OF_BIRTH.Visible = false;
            signup.Attributes["class"] = "tab-pane active";
            login.Attributes["class"] = "tab-pane";
            RequiredFieldValidator9.Enabled = false;
        }
    }
    protected void txtSponsor_TextChanged(object sender, EventArgs e)
    {
        DataTable dtsponsor = new DataTable();
        dtsponsor = cls.selectDataTable("select *From tblmlm_membermaster as a inner join tblmlm_membertree as b on a.MsrNo=b.MsrNo where a.MemberID='" + txtSponsor.Text.Trim() + "' and b.isactive=1");
        if (dtsponsor.Rows.Count > 0)
        {
            lblsponsorName.Text = dtsponsor.Rows[0]["FirstName"].ToString();
            lblsponsorName.Visible = true;
        }
        else
        {
            lblsponsorName.Text = "";
            txtSponsor.Text = "";
            lblsponsorName.Visible = false;
        }
        signup.Attributes["class"] = "tab-pane active";
        login.Attributes["class"] = "tab-pane";
    }
    //protected void txtMobile_TextChanged(object sender, EventArgs e)
    //{
    //    if (cls.ExecuteIntScalar("Select COUNT(*) from tblMLM_MemberMaster Where Mobile='" + txtMobile.Text.Trim() + "'") > 0)
    //    {
    //        // txtAadhar.Text = "";
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Mobile Number Already Used !!');", true);
    //        txtMobile.Text = "";
    //        return;
    //    }
    //}
    //protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    cls.BindDropDownList(ddlDistrict, "Select CityID,CityName from [dbo].[tbl_CityMaster] Where IsActive=1 AND CountryID='76' and stateId='" + ddlState.SelectedValue.ToString() + "'", "CityName", "CityID");
    //    ddlDistrict.Items.Insert(0, new ListItem("Select District", "0"));
    //    ddlDistrict.Enabled = true;
    //}
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (chkremember.Checked == true)
        {
            Response.Cookies["userid"].Value = txtUserID.Text.Trim();
            Response.Cookies["pass"].Value = txtPassword.Text.Trim();
            Response.Cookies["userid"].Expires = DateTime.Now.AddDays(1);
            Response.Cookies["pass"].Expires = DateTime.Now.AddDays(1);
        }
        else
        {
            Response.Cookies["userid"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["pass"].Expires = DateTime.Now.AddDays(-1);
        }

        cls.loginname = txtUserID.Text.Trim();
        cls.password = txtPassword.Text.Trim();
        cls.action = "MemberPanel";
        cls.IpAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();

        dt = cls.AdminLoginAuthentication();
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["Status"].ToString() == "1")
            {
                string str = "select *,case MenuLink when '#' then '$' else MenuName end as PageName,substring(cssClass,0,CHARINDEX('$', cssClass)) as cssClass0,substring(cssClass,CHARINDEX('$', cssClass)+1,LEN(cssClass)-CHARINDEX('$', cssClass)) as cssClass1 from tblMLM_MenuMember where IsActive<>0and MenuID in(Select Word from dbo.SplitPra('" + dt.Rows[0]["MenuStr"].ToString() + "')) order by parentid,position";
                dtMenuAdmin = cls.GetDataTable(str);
                Session.Add("dtMenuMember", dtMenuAdmin);
                Session.Add("MemberSession", dt);
                Response.Redirect("Dashboard.aspx");
            }
            else
            {
                Function.MessageBox(dt.Rows[0]["Message"].ToString());
                return;
            }
        }
        else
        {
            Function.MessageBox("Invalid UserID/password !!");
            return;
        }
    }
    protected void btnforgetpass_Click(object sender, EventArgs e)
    {

        if (cls.ExecuteIntScalar(" select count(*) from tblMLM_MemberMaster where (loginid='" + txtforgetuseid.Text + "' OR MemberID='" + txtforgetuseid.Text + "')") > 0)
        {
            smsfn.sms_recoverpasswordcus(2, txtforgetuseid.Text);
            Function.MessageBox("Your Password send to your Register Mobile No");
            txtforgetuseid.Text = "";
            txtforgetpass.Text = "";
            return;
        }
        else
        {
            txtforgetuseid.Text = "";
            txtforgetpass.Text = "";
            Function.MessageBox("Invalid UserID/ Mobile no !!");
            return;
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        dateofbirth();
        string serialNo = "0";
            DataTable dtresult = new DataTable();
            #region
            //if ((cls.ExecuteIntScalar("Select count(*) from tblMLM_MemberMaster Where Mobile='" + txtMobile.Text.ToString() + "'")) >= 3)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('This Mobile Number Already Used');", true);
            //    return;
            //}


            //DataTable dtpin = cls.selectDataTable("Select * from tblMLM_PinMaster Where PinNumber='" + txtpin.Text.Trim() + "' and PinStatus='Available' AND IsActive=1 and PackageID=" + Convert.ToInt32(ddlPackage.SelectedValue.ToString()) + "");
            //if (dtpin.Rows.Count > 0)
            //{
            //    if (dtpin.Rows[0]["PinStatus"].ToString() == "Used")
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PinNumber Already Used');", true);
            //        return;
            //    }
            //    serialNo = dtpin.Rows[0]["SerialNumber"].ToString();
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Pin number');", true);
            //    return;
            //}

            //Raju:
            //Random random = new Random();
            //int SixDigit = random.Next(100000, 999999);
            #endregion
            string MemberID = "WW" + txtloginid.Text.Trim().ToString();
            if ((cls.ExecuteIntScalar("Select COUNT(*) from tblMLM_MemberMaster Where MemberID='" + MemberID + "'")) > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Id Already Exists!');", true);
                return;
            }

            string ipaddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string IntroMsrno = cls.ExecuteStringScalar("Select Msrno from tblMLM_membermaster Where MemberID='" + txtSponsor.Text.Trim() + "'");
            string ParentMsrno = IntroMsrno;
            if (MemberID == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Id Can't be Empty!');", true);
                return;
            }

            DataSet dtset = cls.GetDataSet("Exec [PROCMLM_MemberMaster] '0','" + MemberID + "','" + ddlcareof.SelectedItem.Text.ToString() + "','" + txtname.Text.Trim() + "','','','','','" + txtEmailID.Text.Trim().ToString() + "','" + txtDATE_OF_BIRTH.Text.Trim().ToString() + "','','" + txtMobile.Text.Trim() + "','','76',26," + ddlcity.SelectedValue.ToString() + ",'" + ipaddress + "','Admin Panel','" + IntroMsrno + "','" + ParentMsrno + "','" + ddlposition.SelectedValue + "','1','0','','" + serialNo + "','" + txtPasswordReg.Text.Trim() + "','','','','0','0','','','','','','0','" + ddlMemberPlaceType.SelectedValue.ToString() + "'");

            if (dtset.Tables.Count > 0)
            {
                if (dtset.Tables.Count > 1)
                {
                    dtresult = dtset.Tables[1];
                }
                else
                {
                    dtresult = dtset.Tables[0];
                }
                string message = dtresult.Rows[0]["Message"].ToString();
                if (dtresult.Rows[0]["Status"].ToString() == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + message + "');", true);
                }
                else
                {
                    string smsmessage = "Welcome to WWFoundation.Your Registration is successfully.Your I'd " + dtresult.Rows[0]["MemberID"].ToString() + " pw " + dtresult.Rows[0]["Password"].ToString() + " http://www.WWFoundation.in";
                    Send(dtresult.Rows[0]["Mobile"].ToString(), smsmessage);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Member Successfully Registerd');location.replace('Default.aspx');", true);
                }
            }
        
    }

    public void Send(string Mobile, string smsMessage)
    {
        try
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            WebClient client = new WebClient();
            string baseurl = "http://sms.qrologic.com/api/smsapi?key=8aac594bfa9eff734cac6df232d5b0c3&route=1&sender=SMSATL&number=" + Mobile + "&sms=" + smsMessage + "";        
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }
        catch (Exception ex)
        {

        }
    }

    //protected void ddlPackage_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (Convert.ToInt32(ddlPackage.SelectedValue.ToString()) > 1)
    //    {
    //        txtpin.Visible = true;
    //    }
    //    else
    //    {
    //        txtpin.Visible = false;
    //    }
    //}
    #region agecalculation
   
    #endregion
    private static int CalculateAge(DateTime dateOfBirth)
    {
        int age = 0;
        age = DateTime.Now.Year - dateOfBirth.Year;
        if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
            age = age - 1;

        return age;
    }
    protected void txtDATE_OF_BIRTH_TextChanged(object sender, EventArgs e)
    {
        dateofbirth();
    }

    private void dateofbirth()
    {
        int membertypevalue = Convert.ToInt32(ddlMemberPlaceType.SelectedValue.ToString());
        if (membertypevalue != 1)
        {
            int age = CalculateAge(Convert.ToDateTime(txtDATE_OF_BIRTH.Text));
            if (age >= 18 && age <= 40)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Age Can be between 18 to 40 !');location.replace('Default.aspx');", true);
                return;
            }
        }
    }
}