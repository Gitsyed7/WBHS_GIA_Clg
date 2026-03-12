using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Microsoft.Reporting.WebForms;
using System.Net;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using NSDataAccessLayer;
using System.Drawing.Imaging;
using System.Net.Mail;

public partial class Higher_Education_OnlineRegistration_Clg_online_reg_emp : System.Web.UI.Page
{
    #region Global Declaration

    string tmplate_dlt;
	BussinessLogicLayer bll = new BussinessLogicLayer();
    MBUCT_BussinessLogicLayer bll1 = new MBUCT_BussinessLogicLayer();
    MBUCT_Clg_User_bll MBUCT_clg_user_bll = new MBUCT_Clg_User_bll();
    DataProcessing dp = new DataProcessing();

	#endregion

	#region Page Load
	protected void Page_Load(object sender, EventArgs e)
    {
        Response.AddHeader("pragma", "no-cache");
        Response.AddHeader("cache-control", "private");
        Response.CacheControl = "no-cache";
        Response.Cache.SetExpires(DateTime.Now.AddMinutes(-1));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        if (Request.UrlReferrer == null)
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            Session["rollValue"] = "";
            Session["value"] = "";
            Response.Redirect("~/First_page.aspx");
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");

        if (!IsPostBack)
        {
            //Session.Clear();
            //Session.Abandon();
            //Session.RemoveAll();
            txt_ben_DOB.Attributes.Add("readonly", "readonly");
            txt_serv_dt.Attributes.Add("readonly", "readonly");
			txt_ac_no.Attributes["type"] = "password";
			
			#region Drop down bind @ Personal Details tab load

			#region for Gender
			sex_ddlist.DataSource = bll.returnGender();
            sex_ddlist.DataTextField = "GendrDes";
            sex_ddlist.DataValueField = "genCd";
            sex_ddlist.DataBind();
            sex_ddlist.Items.Insert(0, "Select Gender");
            #endregion

            #region for marital status
            mrt_ddlist.DataSource = bll.returnMaritalStatus();
            mrt_ddlist.DataTextField = "stsDes";
            mrt_ddlist.DataValueField = "stsCd";
            mrt_ddlist.DataBind();
            mrt_ddlist.Items.Insert(0, "Select Marital Status");
            #endregion

			#region for district

			dist_ddlist.DataSource = bll1.returndistrict();
            dist_ddlist.DataTextField = "dist_nm";
            dist_ddlist.DataValueField = "dist_cd";
            dist_ddlist.DataBind();
            dist_ddlist.Items.Insert(0, "Select District");
            dist_ddlist.Enabled = true;
            #endregion

            #endregion

            #region Session Labels binding & Existing data fetch of personal info
            //lbl_appid.Text = Session["app_id"].ToString();
            lbl_DOB.Text = Session["DOB"].ToString();
            lbl_hrmsid.Text = Session["hrms_id"].ToString();
            string query = "select app_id, slr_no from MBUCT_AppId_Clg_ONLINE where hrms_id='" + lbl_hrmsid.Text + "'and invalid_time is null and is_exists='0'";
            DataSet ds = NSDataAccessLayer.ServiceLocatorIDataAccessLayer.GetDataAccessLayer().GetDataSetByQuery(query);
            lbl_appid.Text = ds.Tables[0].Rows[0][0].ToString();
            HiddenField_SLR_NO.Value = ds.Tables[0].Rows[0]["slr_no"].ToString();
            TabContainer1.ActiveTab = per_info_tab;
            DataSet ds_fetch = bll1.GET_MBUCT_College_BasicInfo_Online_Existing(lbl_appid.Text, lbl_hrmsid.Text);

            btn_nxt_fml.Visible = false;

            #region for Basic info fetch
            if (ds_fetch.Tables[0].Rows.Count != 0)
            {
				if(ds_fetch.Tables[0].Rows[0]["Retirement_age"].ToString() != "")
				{
					rad_ret_yr.SelectedValue = ds_fetch.Tables[0].Rows[0]["Retirement_age"].ToString();
				}
				else
				{
					rad_ret_yr.SelectedIndex = -1;
				}
				txt_fname.Text = ds_fetch.Tables[0].Rows[0]["CLG_FirstName"].ToString();
                txt_lname.Text = ds_fetch.Tables[0].Rows[0]["CLG_LastName"].ToString();
                sex_ddlist.DataSource = bll.returnGender();
                sex_ddlist.DataTextField = "GendrDes";
                sex_ddlist.DataValueField = "genCd";
                sex_ddlist.DataBind();
                sex_ddlist.Items.Insert(0, "Select Gender");
                sex_ddlist.SelectedValue = ds_fetch.Tables[0].Rows[0]["sex"].ToString();
                mrt_ddlist.DataSource = bll.returnMaritalStatus();
                mrt_ddlist.DataTextField = "stsDes";
                mrt_ddlist.DataValueField = "stsCd";
                mrt_ddlist.DataBind();
                mrt_ddlist.Items.Insert(0, "Select Marital Status");
                mrt_ddlist.SelectedValue = ds_fetch.Tables[0].Rows[0]["MTS_STS_CD"].ToString();
                dist_ddlist.DataSource = bll1.returndistrict();
                dist_ddlist.DataTextField = "dist_nm";
                dist_ddlist.DataValueField = "dist_cd";
                dist_ddlist.DataBind();
                dist_ddlist.Items.Insert(0, "Select District");
                dist_ddlist.Enabled = true;
                dist_ddlist.SelectedValue = ds_fetch.Tables[0].Rows[0]["EMP_DIST_CD"].ToString();
                txt_addr.Text = ds_fetch.Tables[0].Rows[0]["empAddr"].ToString();
                txt_aadhaar_no.Text = ds_fetch.Tables[0].Rows[0]["Aadhaar_Card_No"].ToString();
                txt_mob.Text = ds_fetch.Tables[0].Rows[0]["MOBILE_NO"].ToString();
                txt_email.Text = ds_fetch.Tables[0].Rows[0]["EMAIL_ID"].ToString();
                txt_ph_no.Text = ds_fetch.Tables[0].Rows[0]["RESIDENCE_PH_NO"].ToString();
                string id_type = ds_fetch.Tables[0].Rows[0]["id_type"].ToString();
                if (id_type == "Voter Card")
                {
                    lbl_pan_voter.Text = id_type + " No.";
                    rdlist_id_prf.SelectedValue = "01";
                    txt_id_prf.Text = ds_fetch.Tables[0].Rows[0]["PAN_VOTER_NO"].ToString();
                }
                else if (id_type == "Pan Card")
                {
                    lbl_pan_voter.Text = id_type + " No.";
                    rdlist_id_prf.SelectedValue = "02";
                    txt_id_prf.Text = ds_fetch.Tables[0].Rows[0]["PAN_VOTER_NO"].ToString();
                }
                else
                {
                    lbl_pan_voter.Text = "";
                    rdlist_id_prf.SelectedIndex = -1;
                    txt_id_prf.Text = "";
                }

                txt_ifsc_cd.Text = ds_fetch.Tables[0].Rows[0]["bnk_ifsc"].ToString();
				Lbl_bank_name.Text = ds_fetch.Tables[0].Rows[0]["bnk_nm"].ToString();
				Lbl_branch_name.Text = ds_fetch.Tables[0].Rows[0]["bnk_br_nm"].ToString();
				Lbl_MICR.Text = ds_fetch.Tables[0].Rows[0]["bnk_micr"].ToString();
				txt_ac_no.Text = ds_fetch.Tables[0].Rows[0]["bnk_ac-no"].ToString();
				txt_cnfm_ac_no.Text = ds_fetch.Tables[0].Rows[0]["bnk_ac-no"].ToString();
			}
            #endregion

            #endregion

        }
    }
	#endregion

	#region For relationships
	private void relationship()
    {
        relation_ddlist.DataSource = bll.returnRetionship_member();
        relation_ddlist.DataTextField = "mem_des";
        relation_ddlist.DataValueField = "mem_cd";
        relation_ddlist.DataBind();
        relation_ddlist.Items.Insert(0, "SELECT RELATIONSHIP");
    }
    #endregion

    #region For Blood group
    private void blood_group()
    {
        string query = "SELECT [WBG_ID],[WBG_NAME] FROM [WBHS_BLOOD_GROUP]";
        DataSet ds = NSDataAccessLayer.ServiceLocatorIDataAccessLayer.GetDataAccessLayer().GetDataSetByQuery(query);
        bld_grp_ddlist.DataSource = ds;
        bld_grp_ddlist.DataTextField = "WBG_NAME";
        bld_grp_ddlist.DataValueField = "WBG_ID";
        bld_grp_ddlist.DataBind();
        bld_grp_ddlist.Items.Insert(0, "SELECT BLOOD GROUP");
    }
    #endregion

    #region Personal Details Tab | Radio List Selected index changed 
    protected void rdlist_id_prf_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbl_pan_voter.Text = "Please enter " + rdlist_id_prf.SelectedItem + " No.";
        txt_id_prf.Enabled = true;
        txt_id_prf.Text = string.Empty;
    }
	#endregion

	#region Personal Details Tab | Bank IFSC text changed
	protected void txt_ifsc_cd_TextChanged(object sender, EventArgs e)
	{
		DataSet ds_bnk = NSDataAccessLayer.ServiceLocatorIDataAccessLayer.GetDataAccessLayer().GetDataSetByQuery("select BANK,BRANCH,MICR_CODE from WBHS_RBI_BANK_DETAILS where IFSC='" + txt_ifsc_cd.Text + "'");
		if (ds_bnk.Tables[0].Rows.Count != 0)
		{
			Lbl_bank_name.Text = ds_bnk.Tables[0].Rows[0]["BANK"].ToString();
			Lbl_branch_name.Text = ds_bnk.Tables[0].Rows[0]["BRANCH"].ToString();
			Lbl_MICR.Text = ds_bnk.Tables[0].Rows[0]["MICR_CODE"].ToString();
		}
		else
		{
			Lbl_bank_name.Text = "";
			Lbl_branch_name.Text = "";
			Lbl_MICR.Text = "";
			DisplayMessage("No details found.",1);
            txt_ifsc_cd.Text = "";
            return;
		}
	}

	#endregion

	#region Personal Details Tab | Bank account no. text changed
	protected void txt_ac_no_TextChanged(object sender, EventArgs e)
	{
		if (txt_cnfm_ac_no.Text != "")
		{
			if (txt_cnfm_ac_no.Text != txt_ac_no.Text)
			{
				txt_ac_no.Text = "";
				DisplayMessage("Kindly enter same account number in both places.", 1);
				return;
			}
		}
	}
	protected void txt_cnfm_ac_no_TextChanged(object sender, EventArgs e)
	{
		if(txt_ac_no.Text != "")
		{
			if (txt_cnfm_ac_no.Text != txt_ac_no.Text)
			{
				txt_cnfm_ac_no.Text = "";
				DisplayMessage("Account number did not match.", 1);
				return;
			}
		}
		else
		{
			txt_cnfm_ac_no.Text = "";
			DisplayMessage("Please enter account no. above first.", 1);
			return;
		}
	}
	
	#endregion

	#region Next button pressed @Personal Info Tab
	protected void btn_next_per_Click(object sender, ImageClickEventArgs e)
    {
        lbl_appid_ofc.Text = lbl_appid.Text;
        lbl_hrms_ofc.Text = lbl_hrmsid.Text;
        Lbl_DOB_ofc.Text = lbl_DOB.Text;

        #region Validation @ Personal Details Tab

		if(rad_ret_yr.SelectedIndex == -1)
		{
			DisplayMessage("Please select retirement age.", 1);
			return;
		}
        if (txt_fname.Text == "")
		{
			DisplayMessage("Please enter first name.", 1);
			return;
		}
		//if (txt_lname.Text == "")
		//{
		//	DisplayMessage("Please enter last name.");
		//	return;
		//}
		if (sex_ddlist.SelectedItem.Text == "Select Gender")
		{
			DisplayMessage("Please select genger.", 1);
			return;
		}
		if (mrt_ddlist.SelectedItem.Text == "Select Marital Status")
		{
			DisplayMessage("Please select marital status.", 1);
			return;
		}
		if (dist_ddlist.SelectedItem.Text == "Select District")
		{
			DisplayMessage("Please select district.", 1);
			return;
		}
		if (txt_addr.Text == "")
		{
			DisplayMessage("Please enter postal address.", 1);
			return;
		}
		if (txt_mob.Text == "")
		{
			DisplayMessage("Please enter mobile no.", 1);
			return;
		}
		if (txt_email.Text == "")
		{
			DisplayMessage("Please enter email id.", 1);
			return;
		}
		if (rdlist_id_prf.SelectedValue == "")
		{
			DisplayMessage("Please select ID proof type.", 1);
			return;
		}
		if (txt_id_prf.Text == "")
		{
			DisplayMessage("Please enter ID proof details.", 1);
			return;
		}
        if (txt_aadhaar_no.Text == "")
        {
            DisplayMessage("Please enter Aadhaar card No.", 1);
            return;
        }
        if (txt_ifsc_cd.Text == "")
		{
			DisplayMessage("Please enter IFSC of bank.", 1);
			return;
		}
		if (Lbl_bank_name.Text == "" || Lbl_branch_name.Text == "")  /*|| Lbl_MICR.Text == ""*/
		{
			DisplayMessage("No bank is selected. Kindly enter proper IFSC.", 1);
			return;
		}
		if (txt_ac_no.Text == "")
		{
			DisplayMessage("Please enter bank account number.", 1);
			return;
		}
		if (txt_ac_no.Text.Length < 9)
		{
			DisplayMessage("Account number cannot be less than 9 digits.", 1);
			return;
		}
		if (txt_cnfm_ac_no.Text == "")
		{
			DisplayMessage("Please confirm bank account number.", 1);
			return;
		}
		#endregion

		#region Drop down bind @ Office details tab load

		#region District

		clg_dist_ddlist.DataSource = bll1.returndistrict();
        clg_dist_ddlist.DataTextField = "dist_nm";
        clg_dist_ddlist.DataValueField = "dist_cd";
        clg_dist_ddlist.DataBind();
        clg_dist_ddlist.Items.Insert(0, "Select College District");
        clg_dist_ddlist.Enabled = true;

        #endregion

        #region Designation
        desig_ddlist.DataSource = bll1.GET_MBUCT_DESIGNATION_LIST();
        desig_ddlist.DataTextField = "design_nm";
        desig_ddlist.DataValueField = "desig_cd";
        desig_ddlist.DataBind();
        desig_ddlist.Items.Insert(0, "Select Your Designation");
        #endregion

        #region Pay band
        pay_band_ddlist.DataSource = bll1.GET_MBUCT_PAYBND();
        pay_band_ddlist.DataTextField = "PaybndRng";
        pay_band_ddlist.DataValueField = "paybndCd";
        pay_band_ddlist.DataBind();
        pay_band_ddlist.Items.Insert(0, "Select Pay Band");
		#endregion

		#region Pay level

		string query = "SELECT LEVEL_CD,LEVEL_DESCRIP FROM WBHS_SALARY_LEVEL where LEVEL_CD in(46,47,48,49,50,51,52,53,54,55,56)";
		DataSet ds = NSDataAccessLayer.ServiceLocatorIDataAccessLayer.GetDataAccessLayer().GetDataSetByQuery(query);
		ddl_pay_level.DataSource = ds;
		ddl_pay_level.DataTextField = "LEVEL_DESCRIP";
		ddl_pay_level.DataValueField = "LEVEL_CD";
		ddl_pay_level.DataBind();
		ddl_pay_level.Items.Insert(0, "Select Pay Level");

		#endregion

		#endregion

		#region Data Collection @ Personal Details

		string slr_no = HiddenField_SLR_NO.Value;
        string app_id = lbl_appid.Text;
        string hrms_id = lbl_hrmsid.Text;
        string fname = txt_fname.Text.Trim().ToUpper();
        string lname = txt_lname.Text.Trim().ToUpper();
        string dob = lbl_DOB.Text.Trim();
        string gender = sex_ddlist.SelectedValue;
        string mrt_stat = mrt_ddlist.SelectedValue;
        string dist = dist_ddlist.SelectedValue;
        string address = txt_addr.Text.Trim().ToUpper();
        string id_prf = txt_id_prf.Text.Trim().ToUpper();
        string aadhaar_no = txt_aadhaar_no.Text.Trim();
        string mob = txt_mob.Text.Trim();
        string email = txt_email.Text.Trim().ToLower();
        string phNo = txt_ph_no.Text.Trim();
		string retire_age_yr = rad_ret_yr.SelectedValue;
		int ret_age = Convert.ToInt32(rad_ret_yr.SelectedValue);
        string redate = Cal_SuperAnnu(dob, ret_age);

		string ifsc_cd = txt_ifsc_cd.Text.Trim();
		//if (ifsc_cd.Length != 0)
		//{
		//	string substring = ifsc_cd.Substring(4, 1);
		//	if (substring != "0")
		//	{
		//		DisplayMessage("Please Insert Correct IFS Code");
		//		txt_ifsc_cd.Text = "";
		//		return;
		//	}
		//}
		string bnk_name = Lbl_bank_name.Text;
        string bnk_branch = Lbl_branch_name.Text;
		string bnk_micr = Lbl_MICR.Text;
        string bnk_acno = txt_ac_no.Text.Trim();

		string is_exists = "0";
        string id_type = rdlist_id_prf.SelectedItem.ToString();

		#endregion

		#region Data Insertion

		bool status = INSERT_MBUCT_ClgbasicInfo_Online(slr_no, app_id, hrms_id, fname, lname, dob, mrt_stat, gender, dist, address, id_prf, aadhaar_no, mob, email, phNo, 
			redate, ifsc_cd, bnk_name, bnk_branch, bnk_micr, bnk_acno, is_exists, id_type, retire_age_yr);
        if (status == true)
        {
            DisplayMessage("Personal details saved successfully. Please fill up Office Details next.", 0);
            ofc_info_tab.Visible = true;
            ofc_info_tab.Enabled = true;
            TabContainer1.ActiveTab = ofc_info_tab;
            //per_info_tab.Enabled = false;
        }
        else
        {
            DisplayMessage("Please fill all informations correctly..", 1);
            //btn_next1.Visible = false;
        }
        #endregion

        fatch_ofc_details(); /*For Office details existing data fetch*/
    }

	#endregion

	#region Redate calc(Retirement date calculator)
	private string Cal_SuperAnnu(string dob, int ret_age)
	{
		DataSet ds_selectdata = new DataSet();
		int dd, dd1, mm, mm1, yy1;
		string cal_sp = "select datepart(yyyy,convert(datetime,'" + dob + "',103))[Year], datepart(mm,convert(datetime,'" + dob + "',103))[Month],datepart(dd,convert(datetime,'" + dob + "',103))[Date]";
		ds_selectdata = dp.selectData(cal_sp);

		dd = Convert.ToInt32(ds_selectdata.Tables[0].Rows[0][2].ToString());
		mm = Convert.ToInt32(ds_selectdata.Tables[0].Rows[0][1].ToString());
		yy1 = Convert.ToInt32(ds_selectdata.Tables[0].Rows[0][0].ToString()) + ret_age;


		if (mm == 1 | mm == 3 | mm == 5 | mm == 7 | mm == 8 | mm == 10 | mm == 12)
		{
			dd1 = 31;
			if (dd > 1)
			{
				mm1 = mm;
			}
			else
			{
				mm1 = mm - 1;
				if (mm1 == 0)
				{
					mm1 = 12;
					yy1 = yy1 - 1;
				}

				if (mm1 == 1 | mm1 == 3 | mm1 == 5 | mm1 == 7 | mm1 == 8 | mm1 == 10 | mm1 == 12)
				{
					dd1 = 31;
				}
				else
				{
					dd1 = 30;
				}
			}
		}
		else
		{
			dd1 = 30;
			if (mm == 2)
			{
				dd1 = 28;
			}
			if (dd > 1)
			{
				mm1 = mm;
			}
			else
			{
				mm1 = mm - 1;

				if (mm1 == 1 | mm1 == 3 | mm1 == 5 | mm1 == 7 | mm1 == 8 | mm1 == 10 | mm1 == 12)
				{
					dd1 = 31;
				}
				else
				{
					dd1 = 30;
				}
			}

		}

		if (mm1 == 2)
		{
			if (yy1 % 100 == 0)
			{
				if (yy1 % 400 == 0)
				{
					dd1 = 29;
				}
				else
				{
					dd1 = 28;
				}
			}
			else
			{
				if (yy1 % 4 == 0)
				{
					dd1 = 29;
				}
				else
				{
					dd1 = 28;
				}
			}
		}


		string kk;
		if (mm1 < 10)
		{
			string mnt = '0' + mm1.ToString();
			kk = dd1.ToString() + "/" + mnt + "/" + yy1.ToString();
		}
		else
		{
			kk = dd1.ToString() + "/" + mm1.ToString() + "/" + yy1.ToString();
		}

		return kk;
	}
	#endregion

	#region data fetch function declaration for Office Details
	private void fatch_ofc_details()
    {
        #region for office details fetch

        DataSet ds_ofc_details_fatch = bll1.GET_MBUCT_College_Office_Details_Existing(lbl_appid.Text, lbl_hrmsid.Text);

        if (ds_ofc_details_fatch.Tables[0].Rows.Count != 0)
        {
            lbl_appid_ofc.Text = ds_ofc_details_fatch.Tables[0].Rows[0]["app_id"].ToString();
            lbl_hrms_ofc.Text = ds_ofc_details_fatch.Tables[0].Rows[0]["HRMS_ID"].ToString();

            //#region college name bind
            //clg_list_ddlist.DataSource = bll1.GET_MBUCT_College_list(dist1);
            //clg_list_ddlist.DataTextField = "college_nm";
            //clg_list_ddlist.DataValueField = "college_cd";
            //clg_list_ddlist.DataBind();
            //clg_list_ddlist.Items.Insert(0, "Select College Name");
            //clg_list_ddlist.SelectedValue = ds_ofc_details_fatch.Tables[0].Rows[0]["clg_nm"].ToString();
            //#endregion

            #region college district bind
            clg_dist_ddlist.DataSource = bll1.returndistrict();
            clg_dist_ddlist.DataTextField = "dist_nm";
            clg_dist_ddlist.DataValueField = "dist_cd";
            clg_dist_ddlist.DataBind();
            clg_dist_ddlist.Items.Insert(0, "Select District");
            clg_dist_ddlist.SelectedValue = ds_ofc_details_fatch.Tables[0].Rows[0]["clg_dist_cd"].ToString();
            string dist1 = ds_ofc_details_fatch.Tables[0].Rows[0]["clg_dist_cd"].ToString();
            #endregion

            #region college name bind                                                                                                 
            clg_list_ddlist.DataSource = bll1.GET_MBUCT_College_list(dist1);
            clg_list_ddlist.DataTextField = "CLG_NM";
            clg_list_ddlist.DataValueField = "CLG_CD";
            clg_list_ddlist.DataBind();
            clg_list_ddlist.Items.Insert(0, "Select College Name");
            clg_list_ddlist.SelectedValue = ds_ofc_details_fatch.Tables[0].Rows[0]["clg_nm"].ToString();
            #endregion

            #region subdivision bind
            
            subdiv_ddlist.DataSource = bll1.GET_MBUCT_SUBDIVISION_BY_DIST_CD(dist1);
            subdiv_ddlist.DataTextField = "subdiv_nm";
            subdiv_ddlist.DataValueField = "subdiv_cd";
            subdiv_ddlist.DataBind();
            subdiv_ddlist.Items.Insert(0, "Select Subdivision");
            //subdiv_ddlist.Enabled = true;
            subdiv_ddlist.SelectedValue = ds_ofc_details_fatch.Tables[0].Rows[0]["sub_div_cd"].ToString();
            #endregion

            #region block bind
            string dist2 = clg_dist_ddlist.SelectedValue.ToString();
            string subdvsn = subdiv_ddlist.SelectedValue.ToString();
            block_ddlist.DataSource = bll1.GET_MBUCT_BLOCK_BY_dist_SUBDIV_CD(dist2, subdvsn);
            block_ddlist.DataTextField = "block_nm";
            block_ddlist.DataValueField = "block_cd";
            block_ddlist.DataBind();
            block_ddlist.Items.Insert(0, "Select Block");
            block_ddlist.SelectedValue = ds_ofc_details_fatch.Tables[0].Rows[0]["block_cd"].ToString();
            #endregion

            txt_clg_addr.Text = ds_ofc_details_fatch.Tables[0].Rows[0]["clg_addr"].ToString();
            txt_serv_dt.Text = ds_ofc_details_fatch.Tables[0].Rows[0]["DOJ"].ToString();

            #region designation fetch
            desig_ddlist.DataSource = bll1.GET_MBUCT_DESIGNATION_LIST();
            desig_ddlist.DataTextField = "design_nm";
            desig_ddlist.DataValueField = "desig_cd";
            desig_ddlist.DataBind();
            desig_ddlist.Items.Insert(0, "SELECT");
            desig_ddlist.SelectedValue = ds_ofc_details_fatch.Tables[0].Rows[0]["desig"].ToString();
			#endregion

			#region ROPA details visibility

			if (ds_ofc_details_fatch.Tables[0].Rows[0]["ropa"].ToString() != null)
			{
				rbl_ropa.SelectedValue = ds_ofc_details_fatch.Tables[0].Rows[0]["ropa"].ToString();

				if (ds_ofc_details_fatch.Tables[0].Rows[0]["ropa"].ToString() == "01")
				{
					tab_ropa09.Visible = true;
				}
				if (ds_ofc_details_fatch.Tables[0].Rows[0]["ropa"].ToString() == "02")
				{
					tab_ropa19.Visible = true;
				}
			}
			else
			{
				rbl_ropa.SelectedValue = "01";
				tab_ropa09.Visible = true;
			}

			//rbl_ropa.SelectedValue = ds_ofc_details_fatch.Tables[0].Rows[0]["ropa"].ToString();

			//if (ds_ofc_details_fatch.Tables[0].Rows[0]["ropa"].ToString() == "01")
			//{
			//	tab_ropa09.Visible = true;
			//}
			//if (ds_ofc_details_fatch.Tables[0].Rows[0]["ropa"].ToString() == "02")
			//{
			//	tab_ropa19.Visible = true;
			//}
			#endregion

			#region pay band fetch

			if (ds_ofc_details_fatch.Tables[0].Rows[0]["ropa"].ToString() == "01")
			{
				pay_band_ddlist.DataSource = bll1.GET_MBUCT_PAYBND();
				pay_band_ddlist.DataTextField = "PaybndRng";
				pay_band_ddlist.DataValueField = "paybndCd";
				pay_band_ddlist.DataBind();
				pay_band_ddlist.Items.Insert(0, "SELECT PAY BAND");
				if (ds_ofc_details_fatch.Tables[0].Rows[0]["ropa"].ToString() == "01")
				{
					pay_band_ddlist.SelectedValue = ds_ofc_details_fatch.Tables[0].Rows[0]["pay_band"].ToString();
				}
			}
			

			#endregion

			#region grade pay fetch
			if (ds_ofc_details_fatch.Tables[0].Rows[0]["ropa"].ToString() == "01")
			{
				string pay_band = ds_ofc_details_fatch.Tables[0].Rows[0]["pay_band"].ToString();
				grade_ddlist.DataSource = bll1.GET_MBUCT_GRADEPAY(pay_band);
				grade_ddlist.DataTextField = "GrdPay";
				grade_ddlist.DataValueField = "GrdPayCd";
				grade_ddlist.DataBind();
				grade_ddlist.Items.Insert(0, "SELCT GRADE PAY");
				grade_ddlist.Enabled = true;
				if (ds_ofc_details_fatch.Tables[0].Rows[0]["ropa"].ToString() == "01")
				{
					grade_ddlist.SelectedValue = ds_ofc_details_fatch.Tables[0].Rows[0]["gd_pay"].ToString();
				}
			}
			#endregion

			if (ds_ofc_details_fatch.Tables[0].Rows[0]["ropa"].ToString() == "01")
			{
				txt_band.Text = ds_ofc_details_fatch.Tables[0].Rows[0]["band_pay"].ToString();
				lbl_basic.Text = ds_ofc_details_fatch.Tables[0].Rows[0]["basic_pay"].ToString();
			}

			#region Pay level

			if (ds_ofc_details_fatch.Tables[0].Rows[0]["ropa"].ToString() == "02")
			{
				string query = "SELECT LEVEL_CD,LEVEL_DESCRIP FROM WBHS_SALARY_LEVEL";
				DataSet ds = NSDataAccessLayer.ServiceLocatorIDataAccessLayer.GetDataAccessLayer().GetDataSetByQuery(query);
				ddl_pay_level.DataSource = ds;
				ddl_pay_level.DataTextField = "LEVEL_DESCRIP";
				ddl_pay_level.DataValueField = "LEVEL_CD";
				ddl_pay_level.DataBind();
				ddl_pay_level.Items.Insert(0, "Select Pay Level");
				if (ds_ofc_details_fatch.Tables[0].Rows[0]["ropa"].ToString() == "02")
				{
					ddl_pay_level.SelectedValue = ds_ofc_details_fatch.Tables[0].Rows[0]["pay_level"].ToString();
				}
			}

			#endregion

			#region Basic Salary

			if (ds_ofc_details_fatch.Tables[0].Rows[0]["ropa"].ToString() == "02")
			{
				if (ddl_pay_level.SelectedItem.Text != "Select Pay Level")
				{
					string query_basic_sal = "SELECT BASIC_CD,BASIC_DESCRIPTION FROM WBHS_SALARY_BASIC where level_cd='" + ddl_pay_level.SelectedItem.Value + "'";
					DataSet ds_basic_sal = NSDataAccessLayer.ServiceLocatorIDataAccessLayer.GetDataAccessLayer().GetDataSetByQuery(query_basic_sal);
					ddl_basic_sal.DataSource = ds_basic_sal;
					ddl_basic_sal.DataTextField = "BASIC_DESCRIPTION";
					ddl_basic_sal.DataValueField = "BASIC_CD";
					ddl_basic_sal.DataBind();
					ddl_basic_sal.Items.Insert(0, "Select Basic Salary");
					ddl_basic_sal.Enabled = true;
					if (ds_ofc_details_fatch.Tables[0].Rows[0]["ropa"].ToString() == "02")
					{
						ddl_basic_sal.SelectedValue = ds_ofc_details_fatch.Tables[0].Rows[0]["basic_sal"].ToString();
					}
				}
			}

			#endregion
		}

		#endregion
	}

	#endregion

	#region Office details Tab | drop-down bind @ Selected index changed

	#region College district change | College name & Subdivision bind
	protected void clg_dist_ddlist_SelectedIndexChanged(object sender, EventArgs e)
	{
		#region college name bind
		string dist = clg_dist_ddlist.SelectedValue.ToString();
		clg_list_ddlist.DataSource = bll1.GET_MBUCT_College_list(dist);
		clg_list_ddlist.DataTextField = "CLG_NM";
		clg_list_ddlist.DataValueField = "CLG_CD";
		clg_list_ddlist.DataBind();
		clg_list_ddlist.Items.Insert(0, "Select College Name");
		#endregion

		#region subdiv bind
		subdiv_ddlist.DataSource = bll1.GET_MBUCT_SUBDIVISION_BY_DIST_CD(dist);
		subdiv_ddlist.DataTextField = "subdiv_nm";
		subdiv_ddlist.DataValueField = "subdiv_cd";
		subdiv_ddlist.DataBind();
		subdiv_ddlist.Items.Insert(0, "Select College Subdivision");
		subdiv_ddlist.Enabled = true;
		#endregion

	}

	#endregion

	#region College name change | College address fill
	protected void clg_list_ddlist_SelectedIndexChanged(object sender, EventArgs e)
	{
		string query = "select clg_addr from MBUCT_Clg_Name_List where clg_cd='" + clg_list_ddlist.SelectedValue + "'";
		DataSet ds = NSDataAccessLayer.ServiceLocatorIDataAccessLayer.GetDataAccessLayer().GetDataSetByQuery(query);
		if (ds.Tables[0].Rows.Count != 0)
		{
			txt_clg_addr.Text = ds.Tables[0].Rows[0]["clg_addr"].ToString();
		}
		else
		{
			txt_clg_addr.Text = "";
		}
	}
	#endregion

	#region Subdivision change | Block bind
	protected void subdiv_ddlist_SelectedIndexChanged(object sender, EventArgs e)
	{
		string dist = clg_dist_ddlist.SelectedValue.ToString();
		string subdvsn = subdiv_ddlist.SelectedValue.ToString();
		block_ddlist.DataSource = bll1.GET_MBUCT_BLOCK_BY_dist_SUBDIV_CD(dist, subdvsn);
		block_ddlist.DataTextField = "block_nm";
		block_ddlist.DataValueField = "block_cd";
		block_ddlist.DataBind();
		block_ddlist.Items.Insert(0, "Select College Block");
		block_ddlist.Enabled = true;
	}
	#endregion

	#endregion

	#region ROPA selection
	protected void rbl_ropa_SelectedIndexChanged(object sender, EventArgs e)
	{
		ddl_pay_level.SelectedIndex = -1;
		ddl_basic_sal.Items.Clear();
		pay_band_ddlist.SelectedIndex = -1;
		grade_ddlist.Items.Clear();
		txt_band.Text = "";
		lbl_basic.Text = "Grade Pay + Band Pay";
		if (rbl_ropa.SelectedItem.Value == "01")
		{
			tab_ropa09.Visible = true;
			tab_ropa19.Visible = false;
		}
		if (rbl_ropa.SelectedItem.Value == "02")
		{
			tab_ropa19.Visible = true;
			tab_ropa09.Visible = false;
		}
	}

	#endregion

	#region ROPA_09
	protected void pay_band_ddlist_SelectedIndexChanged(object sender, EventArgs e)
	{
		string pay_band = pay_band_ddlist.SelectedValue.ToString();
		grade_ddlist.DataSource = bll1.GET_MBUCT_GRADEPAY(pay_band);
		grade_ddlist.DataTextField = "GrdPay";
		grade_ddlist.DataValueField = "GrdPayCd";
		grade_ddlist.DataBind();
		grade_ddlist.Items.Insert(0, "Select Grade Pay");
		grade_ddlist.Enabled = true;
	}
	protected void grade_ddlist_SelectedIndexChanged(object sender, EventArgs e)
	{
		txt_band.Enabled = true;
		txt_band.Text = "";
		lbl_basic.Text = "Grade Pay + Band Pay";
	}
	protected void txt_band_TextChanged(object sender, EventArgs e)
	{
		if (grade_ddlist.SelectedItem.Value != "0")
		{
			Int32 basic_pay = Convert.ToInt32(grade_ddlist.SelectedItem.Text) + Convert.ToInt32(txt_band.Text);
			//string s_basic_pay = Convert.ToString(basic_pay);
			lbl_basic.Text = Convert.ToString(basic_pay);
		}
		else
		{
			DisplayMessage("Please Select Grade Pay", 1);
		}
	}

	#endregion

	#region ROPA_19
	protected void ddl_pay_level_SelectedIndexChanged(object sender, EventArgs e)
	{
		string query = "SELECT BASIC_CD,BASIC_DESCRIPTION FROM WBHS_SALARY_BASIC where level_cd='" + ddl_pay_level.SelectedItem.Value + "'";
		DataSet ds = NSDataAccessLayer.ServiceLocatorIDataAccessLayer.GetDataAccessLayer().GetDataSetByQuery(query);
		ddl_basic_sal.DataSource = ds;
		ddl_basic_sal.DataTextField = "BASIC_DESCRIPTION";
		ddl_basic_sal.DataValueField = "BASIC_CD";
		ddl_basic_sal.DataBind();
		ddl_basic_sal.Items.Insert(0, "Select Basic Salary");
		ddl_basic_sal.Enabled = true;
	}

	#endregion

	#region Next button pressed @Office Details Tab
	protected void btn_nxt_ofc_Click(object sender, ImageClickEventArgs e)
    {
        relationship();
        blood_group();
        fml_info_tab.Visible = true;
        lbl_appid_fml.Text = lbl_appid.Text;
        lbl_hrms_fml.Text = lbl_hrmsid.Text;
        Lbl_DOB_fml.Text = lbl_DOB.Text;
        string name = txt_fname.Text + " " + txt_lname.Text;
        lbl_applicant_nm.Text = name.ToUpper();

        #region Validation

        if(clg_dist_ddlist.SelectedItem.Text == "Select College Name")
		{
			DisplayMessage("Please select college name.", 1);
			return;
		}
		if (clg_dist_ddlist.SelectedItem.Text == "Select College District")
		{
			DisplayMessage("Please select district of college.", 1);
			return;
		}
		if (subdiv_ddlist.SelectedItem.Text == "Select College Subdivision")
		{
			DisplayMessage("Please select sub-division of college.", 1);
			return;
		}
		if (block_ddlist.SelectedItem.Text == "Select College Block")
		{
			DisplayMessage("Please select block of college.", 1);
			return;
		}
		if (txt_clg_addr.Text == "")
		{
			DisplayMessage("Please enter college address.", 1);
			return;
		}
		if (txt_serv_dt.Text == "")
		{
			DisplayMessage("Please enter date of entry into college service.", 1);
			return;
		}
		if (desig_ddlist.SelectedItem.Text == "Select Your Designation")
		{
			DisplayMessage("Please select your designation.", 1);
			return;
		}
        if (rbl_ropa.SelectedIndex == -1)
        {
            DisplayMessage("Please select Revision of Pay & Allowance.", 1);
            return;
        }
        if (rbl_ropa.SelectedValue == "01" && pay_band_ddlist.SelectedItem.Text == "Select Pay Band")
		{
			DisplayMessage("Please select pay band.", 1);
			return;
		}
		if (rbl_ropa.SelectedValue == "01" && grade_ddlist.SelectedItem.Text == "Select Grade Pay")
		{
			DisplayMessage("Please select grade pay.", 1);
			return;
		}
		if (rbl_ropa.SelectedValue == "01" && txt_band.Text == "")
		{
			DisplayMessage("Please enter band pay.", 1);
			return;
		}
		if (rbl_ropa.SelectedValue == "02" && ddl_pay_level.SelectedItem.Text == "Select Pay Level")
		{
			DisplayMessage("Please select pay level.", 1);
			return;
		}
		if (rbl_ropa.SelectedValue == "02" && ddl_basic_sal.SelectedItem.Text == "Select Basic Salary")
		{
			DisplayMessage("Please select basic salary.", 1);
			return;
		}
		
		#endregion

		#region Data Collection

		string slr_no = HiddenField_SLR_NO.Value;
        string app_id = lbl_appid.Text;
        string hrms_id = lbl_hrmsid.Text;
        string clg_nm = clg_list_ddlist.SelectedValue;
        string clg_dist = clg_dist_ddlist.SelectedValue;
        string clg_sub_div = subdiv_ddlist.SelectedValue;
        string clg_blk = block_ddlist.SelectedValue;
        string clg_addr = txt_clg_addr.Text.ToUpper();
        string dt_join = txt_serv_dt.Text;
        string desig = desig_ddlist.SelectedValue;
		string ropa = rbl_ropa.SelectedValue;

		string pay_bnd = "";
		string gd_pay = "";
		string bnd_pay = "";
		string s_basic_pay = "";
		string pay_level = "";
		string basic_sal = "";
		string ward_name = "";
		string ward_govt = "";
		string ward_tmc = "";

		#region if ROPA-09
		if (rbl_ropa.SelectedValue == "01")
		{
			pay_bnd = pay_band_ddlist.SelectedValue;
			gd_pay = grade_ddlist.SelectedValue;
			bnd_pay = txt_band.Text;
			Int32 basic_pay = Convert.ToInt32(grade_ddlist.SelectedItem.Text) + Convert.ToInt32(bnd_pay);
			s_basic_pay = Convert.ToString(basic_pay);

			pay_level = "";
			basic_sal = "";

			if (Convert.ToInt32(basic_pay) < 18000)
			{
				ward_name = "GENERAL";
				ward_govt = "DOUBLE OCCUPANCY LARGE CABIN";
				ward_tmc = "GENERAL";
			}
			else if (Convert.ToInt32(basic_pay) >= 18000 && Convert.ToInt32(basic_pay) < 27000)
			{
				ward_name = "SEMI-PRIVATE";
				ward_govt = "DOUBLE OCCUPANCY LARGE CABIN";
				ward_tmc = "GENERAL";
			}
			else if (Convert.ToInt32(basic_pay) >= 27000 && Convert.ToInt32(basic_pay) < 75000)
			{
				ward_name = "PRIVATE";
				ward_govt = "SINGLE OCCUPANCY SMALL CABIN";
				ward_tmc = "GENERAL";
			}
			if (Convert.ToInt32(basic_pay) >= 75000)
			{
				ward_name = "PRIVATE";
				ward_govt = "SINGLE OCCUPANCY SMALL CABIN";
				ward_tmc = "PRIVATE";
			}
		}
		#endregion
		#region if ROPA-19
		if (rbl_ropa.SelectedValue == "02")
		{
			pay_level = ddl_pay_level.SelectedValue;
			basic_sal = ddl_basic_sal.SelectedValue;
			int basic = Convert.ToInt32(ddl_basic_sal.SelectedItem.Text);

			if (basic > 0 && basic < 46320)
			{
				ward_name = "GENERAL";
				ward_govt = "DOUBLE OCCUPANCY LARGE CABIN";
				ward_tmc = "GENERAL";
			}
			else if (basic < 69390 && basic >= 46320)
			{
				ward_name = "SEMI-PRIVATE";
				ward_govt = "DOUBLE OCCUPANCY LARGE CABIN";
				ward_tmc = "GENERAL";
			}
			else if (basic < 150000 && basic >= 69390)
			{
				ward_name = "PRIVATE";
				ward_govt = "SINGLE OCCUPANCY SMALL CABIN";
				ward_tmc = "GENERAL";
			}
			if (basic >= 150000)
			{
				ward_name = "SUITE";
				ward_govt = "SINGLE OCCUPANCY LARGE CABIN";
				ward_tmc = "PRIVATE";
			}

			pay_bnd = "";
			gd_pay = "";
			bnd_pay = "";
			s_basic_pay = "";
		}
		#endregion

		string IS_EXISTS = "0";

		#endregion

		#region Data Insertion

		bool status = INSERT_MBUCT_Clg_office_Details(slr_no, app_id, hrms_id, clg_nm, clg_dist, clg_sub_div, clg_blk, clg_addr, dt_join, desig,
			ropa, pay_bnd, gd_pay, bnd_pay, s_basic_pay, pay_level, basic_sal, ward_name, ward_govt, ward_tmc, IS_EXISTS);
        if (status == true)
        {
            DisplayMessage("Office details saved successfully. Please fill up family Details next.", 0);
            fml_info_tab.Visible = true;
            fml_info_tab.Enabled = true;
            TabContainer1.ActiveTab = fml_info_tab;
            //per_info_tab.Enabled = false;

        }
        else
        {
            DisplayMessage("Please try again", 1);
            //btn_next1.Visible = false;
        }
        #endregion
        
        DataSet ds = bll1.GET_MBUCT_Clg_Family_Details_Bind_SLR(lbl_appid_fml.Text, lbl_hrms_fml.Text,HiddenField_SLR_NO.Value);
        if (ds.Tables[0].Rows.Count != 0)
        {
            #region Fetch previously entered Family Details

            //string Photofilepath1 = ReadFileFromServer(ds.Tables[0].Rows[0]["PhotoPath"].ToString(), "Writereaddata", "CLGEMP", "Photo", "", true);
            //string Photofilepath2 = "~/Tempfile/Writereaddata/CLGEMP/Photo/" + ds.Tables[0].Rows[0]["PhotoPath"].ToString();

            //string Signfilepath1 = ReadFileFromServer(ds.Tables[0].Rows[0]["SignPath"].ToString(), "Writereaddata", "CLGEMP", "Signature", "", true);
            //string Signfilepath2 = "~/Tempfile/Writereaddata/CLGEMP/Signature/" + ds.Tables[0].Rows[0]["SignPath"].ToString();

            family_grid.DataSource = ds;
            family_grid.DataBind();
            btn_nxt_fml.Visible = true;
            //txt_ben_id_prf.Enabled = false;
            txt_ben_DOB.Enabled = true;
            txt_Ben_Name.Enabled = true;
            relation_ddlist.Enabled = true;
            txt_ben_aadhaar_no.Text = "";
            txt_ben_aadhaar_no.Enabled = true;
            txt_ben_mob_no.Text = "";
            txt_ben_mob_no.Enabled = true;
            txt_ben_email.Text = "";
            txt_ben_email.Enabled = true;

            #endregion
        }
		else
        {
            #region family details self data fill
            TabContainer1.ActiveTab = fml_info_tab;
            lbl_applicant_nm.Text = name.ToUpper();
            txt_Ben_Name.Text = lbl_applicant_nm.Text;
            txt_ben_DOB.Text = Lbl_DOB_fml.Text;

            string dob = txt_ben_DOB.Text;
            txt_age.Text = calAge(dob);
            relation_ddlist.SelectedValue = Convert.ToString("10");
            //rdlist_ben_id.SelectedValue = rdlist_id_prf.SelectedValue;
            //lbl_ben_id_prf.Text = rdlist_ben_id.SelectedItem.Text + " No.";
            //txt_ben_id_prf.Text = txt_id_prf.Text;
            //txt_ben_id_prf.Enabled = false;
            
            txt_ben_aadhaar_no.Text = txt_aadhaar_no.Text;
            txt_ben_mob_no.Text = txt_mob.Text;
            txt_ben_email.Text = txt_email.Text;
            txt_ben_aadhaar_no.Enabled = false;
            txt_ben_mob_no.Enabled = false;
            txt_ben_email.Enabled = false;
            txt_ben_DOB.Enabled = false;
            txt_Ben_Name.Enabled = false;
            relation_ddlist.Enabled = false;

			#endregion
		}
	}

    #endregion

	#region Age Calculator
	private string calAge(string dob)
	{
		string yr, umnt, smnt;
		//DateTime dt;
		int usr_mnt, sys_mnt;
		//dt = DateTime.Parse(txtDOB.Text);
		DataSet ds = new DataSet();
		umnt = "select datepart(mm,convert(datetime,'" + dob + "',103))";
		ds = dp.selectData(umnt);
		usr_mnt = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());

		smnt = "select datepart(mm,getdate())";
		ds = dp.selectData(smnt);
		sys_mnt = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
		string age = "";

		if (sys_mnt < usr_mnt | sys_mnt == usr_mnt & sys_mnt < usr_mnt)
		{

			yr = "select datediff(yy,convert(datetime,'" + txt_ben_DOB.Text + "',103),getdate())-1";
			ds = dp.selectData(yr);
			age = ds.Tables[0].Rows[0][0].ToString();
			return age;
		}
		else
		{
			yr = "select datediff(yy,convert(datetime,'" + txt_ben_DOB.Text + "',103),getdate())";
			ds = dp.selectData(yr);
			age = ds.Tables[0].Rows[0][0].ToString();
			return age;
		}
	}
	#endregion

	#region Save button pressed @ Family details Tab

	//protected void rdlist_ben_id_SelectedIndexChanged(object sender, EventArgs e)
 //   {
 //       lbl_ben_id_prf.Text = "Please enter " + rdlist_ben_id.SelectedItem + " No.";
 //       txt_ben_id_prf.Enabled = true;
 //       txt_ben_id_prf.Text = string.Empty;
 //   }
    protected void btn_save_fml_Click(object sender, ImageClickEventArgs e)
    {
        #region validation

        if (txt_Ben_Name.Text == "")
		{
			DisplayMessage("Please enter beneficiary name", 1);
			return;
		}
		if (txt_ben_DOB.Text == "")
		{
			DisplayMessage("Please enter date of birth of beneficiary", 1);
			return;
		}
        if(relation_ddlist.SelectedItem.Text == "SELECT RELATIONSHIP")
		{
			DisplayMessage("Please select relation with beneficiary.", 1);
			return;
		}
		
		//if (ddlBenCat.SelectedValue == "Critical" && cblBenDisease.SelectedIndex ==-1)
		//{
		//	DisplayMessage("Please select the name of the critical disease/illness.");
		//	return;
		//}

		#region Critical illness
		int q = 0;
		for (int i = 0; i < cblBenDisease.Items.Count; i++)
		{
			if (cblBenDisease.Items[i].Selected)
			{
				q = q + 1;
			}
		}

		if (q == 0 && ddlBenCat.SelectedValue == "Critical")
		{
			DisplayMessage("Please select the name of the critical disease/illness.", 1);
			return;
		}
		#endregion

		if (txt_ben_income.Text == "")
		{
			DisplayMessage("Please enter monthly income of beneficiary.", 1);
			return;
		}
		if (bld_grp_ddlist.SelectedItem.Text == "SELECT BLOOD GROUP")
		{
			DisplayMessage("Please select blood group of beneficiary.", 1);
			return;
		}
		//if(rdlist_ben_id.SelectedIndex == -1)
		//{
		//	DisplayMessage("Please select identification proof type.");
		//	return;
		//}
        if(Convert.ToInt32(txt_age.Text) >= 5)
        {
            if (txt_ben_aadhaar_no.Text == "")
            {
                DisplayMessage("Please enter aadhaar no.", 1);
                return;
            }
        }
		if (txt_ben_mob_no.Text == "")
		{
			DisplayMessage("Please enter beneficiary mobile number.", 1);
			return;
		}
		if (txt_ben_email.Text == "")
		{
			DisplayMessage("Please enter beneficiary email id.", 1);
			return;
		}
		if (fup_photo.FileName == "")
		{
			DisplayMessage("Please upload photo.", 1);
			return;
		}
		if (fup_sign.FileName == "")
		{
			DisplayMessage("Please upload signature.", 1);
			return;
		}

        if (fup_photo.HasFile)
        {
            if (fup_photo.PostedFile.ContentLength <= 51700 && fup_photo.PostedFile.ContentLength >= 10281)
            {
                try
                {
                    string file = "File name: " +
                    fup_photo.PostedFile.FileName + "<br>" +
                    fup_photo.PostedFile.ContentLength + " kb<br>" +
                    "Content type: " +
                    fup_photo.PostedFile.ContentType;
                }
                catch (Exception ex)
                {
                    DisplayMessage("ERROR: " + ex.Message.ToString(), 1);
                }
            }
            else
            {
                DisplayMessage("FILE SIZE OF PHOTO MUST BE WITHIN 10KB-50KB", 1);
                return;
            }
        }
        if (fup_sign.HasFile)
        {
            if (fup_sign.PostedFile.ContentLength <= 51700 && fup_sign.PostedFile.ContentLength >= 5197)
            {
                try
                {
                    string file = "File name: " +
                    fup_sign.PostedFile.FileName + "<br>" +
                    fup_sign.PostedFile.ContentLength + " kb<br>" +
                    "Content type: " +
                    fup_sign.PostedFile.ContentType;
                }
                catch (Exception ex)
                {
                    DisplayMessage("ERROR: " + ex.Message.ToString(), 1);
                }
            }
            else
            {
                DisplayMessage("FILE SIZE OF SIGNATURE MUST BE WITHIN 8KB TO 50KB", 1);
                return;
            }
        }
        #endregion

        #region datacollection_fam
        string slr_no = HiddenField_SLR_NO.Value;
        string app_id = lbl_appid_fml.Text;
        string HRMS_ID = lbl_hrms_fml.Text;
        string BEN_ID = "";
        if (family_grid.Rows.Count == 0)
        {
            BEN_ID = HRMS_ID + "/" + "1";
            slr_no = slr_no + "/" + "1";
        }
        else
        {
            BEN_ID = HRMS_ID + "/" + Convert.ToString(family_grid.Rows.Count + 1);
            slr_no = slr_no + "/" + Convert.ToString(family_grid.Rows.Count + 1);
        }
        string BEN_NM = txt_Ben_Name.Text.ToUpper();
        string BEN_DOB = txt_ben_DOB.Text;
        string AGE = txt_age.Text;
        string INCOME = txt_ben_income.Text;
        string RELATION = relation_ddlist.SelectedValue;
        string BLD_GRP = bld_grp_ddlist.SelectedItem.Text;
        //string ID_TYPE = rdlist_ben_id.SelectedItem.Text;
        string BEN_PHOTO_FILE_NAME = BEN_ID.Replace("/", "") + fup_photo.FileName; // Application server physical file name and DB file name equalized by adding BEN_ID.Replace("/", "") 13.06.24
        System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(fup_photo.PostedFile.InputStream);
        //System.Drawing.Image objImage = scaleimage(bmpPostedImage, 300);
        Stream str1 = ToStream(bmpPostedImage, ImageFormat.Jpeg);
        str1.Position = 0;

        // save image into binary format//
        BinaryReader br = new BinaryReader(str1);
        Byte[] BEN_IMG_PHOTO = br.ReadBytes((int)str1.Length);

        string BEN_SIG_FILE_NAME = BEN_ID.Replace("/", "") + fup_sign.FileName; // Application server physical file name and DB file name equalized BEN_ID.Replace("/", "") 13.06.24

        System.Drawing.Bitmap bmpPostedImage1 = new System.Drawing.Bitmap(fup_sign.PostedFile.InputStream);
        //System.Drawing.Image objImage1 = scaleimage(bmpPostedImage1, 40);
        Stream str11 = ToStream(bmpPostedImage1, ImageFormat.Jpeg);
        str11.Position = 0;

        // save signature into binary format//
        BinaryReader br1 = new BinaryReader(str11);
        Byte[] BEN_IMG_SIG = br1.ReadBytes((int)str11.Length);
        string PHOTO_FTP = "http://wbhealthscheme.gov.in/Writereaddata/CLGEMP/Photo/" + BEN_ID.Replace("/", "") + fup_photo.FileName;
        string SIGN_FTP = "http://wbhealthscheme.gov.in/Writereaddata/CLGEMP/Signature/" + BEN_ID.Replace("/", "") + fup_sign.FileName;
        string UPLOADING_ID = app_id;
        string UPLOADING_IP = (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? Request.ServerVariables["REMOTE_ADDR"]).Split(',')[0].Trim();
        string AADHAAR_NO = txt_ben_aadhaar_no.Text;
        string BEN_MOB = txt_ben_mob_no.Text;
		string BEN_EMAIL = txt_ben_email.Text;
		string BEN_CAT = ddlBenCat.SelectedValue;
		string IS_EXISTS = "0";
        #endregion

        #region datainsertion_family
        bool status = INSERT_MBUCT_clg_familyDetails(slr_no, app_id, HRMS_ID, BEN_ID, BEN_NM, BEN_DOB, AGE, INCOME, RELATION, 
            BLD_GRP, AADHAAR_NO, BEN_PHOTO_FILE_NAME, BEN_IMG_PHOTO, BEN_SIG_FILE_NAME, 
            BEN_IMG_SIG, PHOTO_FTP, SIGN_FTP, UPLOADING_ID, UPLOADING_IP, BEN_MOB, BEN_EMAIL,BEN_CAT, IS_EXISTS);

		if (status == true)
        {
			#region BeneficiaryDeseaseDetails
			if (BEN_CAT == "Critical")
			{
				string concatVar = "$";
				string DeseaseCode = "";
				int j = 0;
				for (int i = 0; i < cblBenDisease.Items.Count; i++)
				{
					if (cblBenDisease.Items[i].Selected)
					{
						DeseaseCode = DeseaseCode + cblBenDisease.Items[i].Value + concatVar;
						j = j + 1;
					}
				}
				//wbhs_deathcase_dal objBD = new wbhs_deathcase_dal();

				if (j > 0)
				{
					bool p = BeneficiaryDisease_Insert(slr_no, app_id, BEN_ID, j, DeseaseCode);
					ddlBenCat.SelectedIndex = -1;
					cblBenDisease.Items.Clear();
					trBenDisease.Visible = false;
				}
			}

			#endregion

			DisplayMessage("DETAILS INSERTED SUCCESSFULLY FOR-" + txt_Ben_Name.Text.ToUpper(), 0);

            #region Save Photo in SDC File Server

            //string save_photo_path = "~/Writereaddata/CLGEMP/Photo/" + BEN_ID.Replace("/", "") + fup_photo.FileName;
            //bmpPostedImage.Save(Server.MapPath(save_photo_path), ImageFormat.Jpeg);

            string filepath = "~/TempFile/" + Path.GetFileName(BEN_ID.Replace("/", "") + fup_photo.FileName);
            fup_photo.SaveAs(Server.MapPath(filepath));

            string photo = Path.Combine(Server.MapPath("~/TempFile/"),BEN_ID.Replace("/", "") + fup_photo.FileName);
            FileUpload news_up = new FileUpload();
            news_up.SaveAs(photo);

            //byte[] retrun = (new WebClient()).UploadFile(new Uri("http://192.168.1.69/Upload.ashx?Key=abcd1234" + "&Folder=" + "Writereaddata" + "&subFolder=" + "CLGEMP" + "&subSubFolder=" + "Photo" + "&subSubSubFolder=" + ""), photo); // mirror uploaded file to FileServer server

            byte[] retrun = (new WebClient()).UploadFile(new Uri("http://172.25.146.178/Upload.ashx?Key=abcd1234" + "&Folder=" + "Writereaddata" + "&subFolder=" + "CLGEMP" + "&subSubFolder=" + "Photo" + "&subSubSubFolder=" + ""), photo); // mirror uploaded file to FileServer server

            if (File.Exists(photo))
            {
                File.Delete(photo);
            }
            #endregion

            #region Save Sign in SDC File Server

            //string save_sign_path = "~/Writereaddata/CLGEMP/Signature/" + BEN_ID.Replace("/", "") + fup_sign.FileName;
            //bmpPostedImage1.Save(Server.MapPath(save_sign_path), ImageFormat.Jpeg);

            string filepath1 = "~/TempFile/" + Path.GetFileName(BEN_ID.Replace("/", "") + fup_sign.FileName);
            fup_sign.SaveAs(Server.MapPath(filepath1));

            string sig = Path.Combine(Server.MapPath("~/TempFile/"), BEN_ID.Replace("/", "") + fup_sign.FileName);
            FileUpload news_up1 = new FileUpload();
            news_up1.SaveAs(sig);

            //byte[] retrun1 = (new WebClient()).UploadFile(new Uri("http://192.168.1.69/Upload.ashx?Key=abcd1234" + "&Folder=" + "Writereaddata" + "&subFolder=" + "CLGEMP" + "&subSubFolder=" + "Signature" + "&subSubSubFolder=" + ""), sig); // mirror uploaded file to FileServer server

            byte[] retrun1 = (new WebClient()).UploadFile(new Uri("http://172.25.146.178/Upload.ashx?Key=abcd1234" + "&Folder=" + "Writereaddata" + "&subFolder=" + "CLGEMP" + "&subSubFolder=" + "Signature" + "&subSubSubFolder=" + ""), sig); // mirror uploaded file to FileServer server
            if (File.Exists(sig))
            {
                File.Delete(sig);
            }
            #endregion

            fill_grid_family();
            save_btn_refresh_control();
            //lbl_ben_id_prf.Text = string.Empty;
            btn_nxt_fml.Visible = true;
            txt_ben_DOB.Enabled = true;
            txt_Ben_Name.Enabled = true;
            relation_ddlist.Enabled = true;
            txt_ben_aadhaar_no.Text = "";
            txt_ben_aadhaar_no.Enabled = true;
            txt_ben_mob_no.Text = "";
            txt_ben_mob_no.Enabled = true;
            txt_ben_email.Text = "";
            txt_ben_email.Enabled = true;
        }
        else
        {
            DisplayMessage("PLEASE TRY AGAIN", 1);
        }
        #endregion
    }
    #endregion

    #region Utility function declaration for Family details
    private void fill_grid_family()
    {
        DataSet ds = bll1.GET_MBUCT_Clg_Family_Details_Bind(lbl_appid_fml.Text, lbl_hrms_fml.Text,HiddenField_SLR_NO.Value);
        //string Photofilepath1 = ReadFileFromServer(ds.Tables[0].Rows[0]["PhotoPath"].ToString(), "Writereaddata", "CLGEMP", "Photo", "", true);
        //string Photofilepath2 = "~/Tempfile/Writereaddata/CLGEMP/Photo/" + ds.Tables[0].Rows[0]["PhotoPath"].ToString();

        //string Signfilepath1 = ReadFileFromServer(ds.Tables[0].Rows[0]["SignPath"].ToString(), "Writereaddata", "CLGEMP", "Signature", "", true);
        //string Signfilepath2 = "~/Tempfile/Writereaddata/CLGEMP/Signature/" + ds.Tables[0].Rows[0]["SignPath"].ToString();

        family_grid.DataSource = ds;
        family_grid.DataBind();
    }
    private static Stream ToStream(System.Drawing.Image image, ImageFormat formaw)
    {
        var stream = new System.IO.MemoryStream();
        image.Save(stream, formaw);
        stream.Position = 0;
        return stream;
    }
    protected void txt_ben_DOB_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string dob = txt_ben_DOB.Text;
            txt_age.Text = calAge(dob);

        }
        catch (System.Exception excep)
        {
            DisError(excep.Message);
        }
    }
	protected void ddlBenCat_SelectedIndexChanged(object sender, EventArgs e)
	{
		trBenDisease.Visible = false;
		if(relation_ddlist.SelectedItem.Text == "SELECT RELATIONSHIP")
		{
			DisplayMessage("Please select relation first.", 1);
			ddlBenCat.SelectedValue = "General";
			trBenDisease.Visible = false;
			return;
		}
		if (relation_ddlist.SelectedItem.Text == "SELF")
		{
			DisplayMessage("This category is only applicable for beneficiaries of applicant.", 1);
			ddlBenCat.SelectedValue = "General";
			trBenDisease.Visible = false;
			return;
		}
		if (relation_ddlist.SelectedItem.Text != "SELF" && ddlBenCat.SelectedValue == "Critical")
		{
			trBenDisease.Visible = true;
			//wbhs_deathcase_dal BenDisease = new wbhs_deathcase_dal();
			DataSet ds = BeneficiaryDisease_GetDetails();
			cblBenDisease.DataSource = ds.Tables[0];
			cblBenDisease.DataTextField = "DiseaseName";
			cblBenDisease.DataValueField = "SlNo";
			cblBenDisease.DataBind();

		}
	}

	#endregion

	#region Next button pressed @ Family Details Tab | Dropdown bind @ HOI Tab load
	protected void btn_nxt_fml_Click(object sender, ImageClickEventArgs e)
    {
        hoi_info_tab.Visible = true;
        TabContainer1.ActiveTab = hoi_info_tab;
        #region Head of institution bind
        hoi_ddlist.DataSource = bll1.GET_MBUCT_Hoi_list();
        hoi_ddlist.DataTextField = "HoiNm";
        hoi_ddlist.DataValueField = "HoiCode";
        hoi_ddlist.DataBind();
        hoi_ddlist.Items.Insert(0, "Select Head of the Institution");

        #endregion
        #region DDO Designation Bind
        //ddo_desig_ddlist.DataSource = bll1.GET_MBUCT_DDO_Designation();
        //ddo_desig_ddlist.DataTextField = "desig_nm";
        //ddo_desig_ddlist.DataValueField = "ddo_desig";
        //ddo_desig_ddlist.DataBind();
        //ddo_desig_ddlist.Items.Insert(0, "Select DDO Designation");
        #endregion
        #region district
        ddo_dist_ddlist.DataSource = bll1.returndistrict();
        ddo_dist_ddlist.DataTextField = "dist_nm";
        ddo_dist_ddlist.DataValueField = "dist_cd";
        ddo_dist_ddlist.DataBind();
        ddo_dist_ddlist.Items.Insert(0, "Select DDO District");

        #endregion
    }
    #endregion

    #region HOI Tab | Drop down bind @ Selected index changed
    protected void ddo_dist_ddlist_SelectedIndexChanged1(object sender, EventArgs e)
    {
        string ddo_dist = ddo_dist_ddlist.SelectedValue;
        trsy_ddlist.DataSource = bll1.GET_MBUCT_Treasury_list(ddo_dist);
        trsy_ddlist.DataTextField = "tr_nm";
        trsy_ddlist.DataValueField = "srl_no";
        trsy_ddlist.DataBind();
        trsy_ddlist.Items.Insert(0, "Select Treasury");
    }
	protected void ddo_cd_ddlist_SelectedIndexChanged(object sender, EventArgs e)
	{
		string query = "SELECT DDO_DESIGNATION FROM wbhealthscheme.dbo.WBHS_DDO_DETAILS where DDO_CD = '" + ddo_cd_ddlist.SelectedItem.Text + "'";
		DataSet ds_ddo_desig = NSDataAccessLayer.ServiceLocatorIDataAccessLayer.GetDataAccessLayer().GetDataSetByQuery(query);
		if (ds_ddo_desig.Tables[0].Rows.Count != 0)
		{
			tr_ddo_desig1.Visible = true;
			lbl_ddo_desig.Text = ds_ddo_desig.Tables[0].Rows[0]["DDO_DESIGNATION"].ToString();
		}
	}
	protected void trsy_ddlist_SelectedIndexChanged1(object sender, EventArgs e)
    {
        string tr_cd = trsy_ddlist.SelectedValue;
        ddo_cd_ddlist.DataSource = bll1.GET_MBUCT_DDO_Code_list(tr_cd);
        ddo_cd_ddlist.DataTextField = "ddo_cd";
        ddo_cd_ddlist.DataValueField = "ddo_sl_no";
        ddo_cd_ddlist.DataBind();
        ddo_cd_ddlist.Items.Insert(0, "Select DDO Code");
    }

	#endregion

	#region Final Save button pressed @ HOI Tab
	protected void btnSave_final_Click(object sender, ImageClickEventArgs e)
    {

        #region validation
        if (
            hoi_ddlist.SelectedItem.Text == "Select Head of the Institution"
          //|| ddo_desig_ddlist.SelectedItem.Text == "Select DDO Designation"
          || ddo_dist_ddlist.SelectedItem.Text == "Select DDO District"
          || trsy_ddlist.SelectedItem.Text == "Select Treasury"
          || ddo_cd_ddlist.SelectedItem.Text == "Select DDO Code"
          )
        {
            DisplayMessage("Please Fill / Select mandatory fields", 1);
            return;
        }
		if (CheckBox1.Checked != true)
		{
			DisplayMessage("Please Recheck and Accept The Declaration", 1);
			return;
		}
		#endregion

		#region Data Allocation

		string slr_no = HiddenField_SLR_NO.Value;
        string APP_ID = lbl_appid_fml.Text;
        string HRMS_ID = lbl_hrms_fml.Text;
        string HOI_DESIG = hoi_ddlist.SelectedValue;
        //string DDO_DESIG = ddo_desig_ddlist.SelectedValue;
        string DDO_DESIG = "";
        string DDO_DIST = ddo_dist_ddlist.SelectedValue;
        string TR_CODE = trsy_ddlist.SelectedValue;
        string DDO_CODE = ddo_cd_ddlist.SelectedItem.Text;
        string IS_EXISTS = "0";

		#endregion

		#region Data Insertion

		bool status = INSERT_MBUCT_CLG_HOI_DETAILS(slr_no, APP_ID, HRMS_ID, HOI_DESIG, DDO_DESIG, DDO_DIST, TR_CODE, DDO_CODE, IS_EXISTS);
        if (status == true)
        {
            DisplayMessage("Thank you for completing Enrolment process.Please Click On The Report Button To Generate & Download The Application Form", 0);
            string query = "update MBUCT_AppId_Clg_ONLINE set is_exists='1' where APP_ID='" + APP_ID + "' and hrms_id='" + HRMS_ID + "' and is_exists='0'";
            NSDataAccessLayer.ServiceLocatorIDataAccessLayer.GetDataAccessLayer().ExecuteNonQueryByQueryWithoutParam(query);
            btnSave_final.Visible = false;
            btn_rpt.Visible = true;
            final_fml_nxt.Visible = true;
            final_nxt_per.Visible = true;
            final_ofc_nxt.Visible = true;
            btn_next_per.Visible = false;
            btn_nxt_fml.Visible = false;
            btn_nxt_ofc.Visible = false;
            btn_save_fml.Visible = false;
            sendmail();
            SendSms();
            SendSmsDDO();
        }
        else
        {
            DisplayMessage("Please Try Again...", 1);
        }
		#endregion
	}
	#endregion

	#region family details control refresh
	private void save_btn_refresh_control()
    {
        txt_Ben_Name.Text = "";
        txt_ben_DOB.Text = "";
        txt_age.Text = "";
        relation_ddlist.SelectedIndex = 0;
        txt_ben_income.Text = "";
        bld_grp_ddlist.SelectedIndex = 0;
        //rdlist_ben_id.SelectedIndex = -1;
        txt_ben_aadhaar_no.Text = string.Empty;
        btn_nxt_fml.Visible = true;

    }
	#endregion

	#region HOME button click
	protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
	{
		Response.Redirect("~/First_page.aspx");
	}

	#endregion

	#region Next button clicks of Tabpanels
	protected void final_nxt_per_Click(object sender, ImageClickEventArgs e)
	{
		ofc_info_tab.Visible = true;
		TabContainer1.ActiveTab = ofc_info_tab;
	}
	protected void final_ofc_nxt_Click(object sender, ImageClickEventArgs e)
	{
		fml_info_tab.Visible = true;
		TabContainer1.ActiveTab = fml_info_tab;
	}
	protected void final_fml_nxt_Click(object sender, ImageClickEventArgs e)
	{
		hoi_info_tab.Visible = true;
		TabContainer1.ActiveTab = hoi_info_tab;
	}

	#endregion

	#region Previous button clicks of Tabpanels
	protected void btn_prv_ofc_Click(object sender, ImageClickEventArgs e)
    {
        per_info_tab.Visible = true;
        TabContainer1.ActiveTab = per_info_tab;
    }
    protected void btn_prv_fml_Click(object sender, ImageClickEventArgs e)
    {
        ofc_info_tab.Visible = true;
        TabContainer1.ActiveTab = ofc_info_tab;
    }
    protected void btn_prv_hoi_Click1(object sender, ImageClickEventArgs e)
    {
        fml_info_tab.Visible = true;
        TabContainer1.ActiveTab = fml_info_tab;
    }

	#endregion

	#region SMS Sending
	private void SendSms()
	{
        //Push_SMS msg = new Push_SMS();

        //string MobileNo = txt_mob.Text.ToString();
        //string Message = "Online WBHS Application-" + lbl_appid.Text + " generated. Please submit duly signed application to DDO for verification";
        //string exp = "";
        //int i = msg.push_msg(Message, MobileNo, exp);

        //if (i.ToString().Length > 0)
        //{
        //    DisError(exp.ToString());
        //    return;
        //}
        string MobileNo = txt_mob.Text.ToString();
        string dlt_entity_id = "1001987019757004358";
        string Message = "Online WBHS Application-" + lbl_appid.Text + " generated. Please submit duly signed application to DDO for verification.";
        //Online WBHS Application-{#var#} generated. Please submit duly signed application to DDO for verification.
        string exp = "";
        tmplate_dlt = "1007917094986932569";
        int i = push_msg(Message, MobileNo, dlt_entity_id, tmplate_dlt, exp);

        if (i.ToString().Length > 0)
        {
            //DisError(exp.ToString());
            //return;
        }
	}
	private void SendSmsDDO()
	{
		Push_SMS msg = new Push_SMS();
		string ddo_cd = ddo_cd_ddlist.SelectedItem.Text.ToString();
		string query = "select DDO_MOB_NO from WBHS_DDO_DETAILS where DDO_CD='" + ddo_cd + "'";
		DataSet ddo_mob_no = NSDataAccessLayer.ServiceLocatorIDataAccessLayer.GetDataAccessLayer().GetDataSetByQuery(query);
		string mob_no = ddo_mob_no.Tables[0].Rows[0]["DDO_MOB_NO"].ToString();
        string dlt_entity_id = "1001987019757004358";
		if (string.IsNullOrEmpty(mob_no))
		{

		}
		else
		{
            //string MobileNo = mob_no;
            //string Message = "Verification is pending for WBHS Application-" + lbl_appid.Text + ". Please verify duly signed application within 3 days after receiving";
            //string exp = "";
            //int i = msg.push_msg(Message, MobileNo, exp);

            //if (i.ToString().Length > 0)
            //{
            //    DisError(exp.ToString());
            //    return;
            //}
            string MobileNo = mob_no;
            string Message = "Verification is pending for WBHS Application " + lbl_appid.Text + ". Please verify duly signed application within 3 days after receiving.";
            //Verification is pending for WBHS Application {#var#}. Please verify duly signed application within 3 days after receiving.
            string exp = "";
            tmplate_dlt = "1007473161755633707";
            int i = push_msg(Message, MobileNo, dlt_entity_id, tmplate_dlt, exp);

            if (i.ToString().Length > 0)
            {
                //DisError(exp.ToString());
                //return;
            }
		}
	}

    private Int16 push_msg(string msg, string mobile_no, string dlt_entity_id, string tmplate_dlt, string exp)
    {
        Int16 flag = 0;
        try
        {
            if (flag == 0)
            {
                HttpWebRequest request;
                HttpWebResponse rsponse;
                string username = ConfigurationManager.AppSettings["username"].ToString();
                string pin = ConfigurationManager.AppSettings["pin"].ToString();
                string senderid = ConfigurationManager.AppSettings["sender"].ToString();
                //string dlt_template_id = tmplate_dlt;
                //string API = "https://smsgw.sms.gov.in/failsafe/HttpLink?username=" + username + "&pin=" + Server.UrlEncode(pin) + "&message=" + msg + "&mnumber=" + mobile_no + "&signature=" + senderid + "";
                string API = "https://smsgw.sms.gov.in/failsafe/HttpLink?username=" + username + "&pin=" + Server.UrlEncode(pin) + "&message=" + msg + "&mnumber= " + mobile_no + "&signature= " + senderid + "&dlt_entity_id=" + dlt_entity_id + "&dlt_template_id=" + tmplate_dlt;
                request = (HttpWebRequest)WebRequest.Create(API);
                rsponse = (HttpWebResponse)request.GetResponse();
                request.Method = "GET";
                exp = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
                flag = 1;
            }
        }
        catch (Exception ex)
        {
            exp = ex.Message;
        }
        return flag;
    }
	#endregion

	#region Mail sending
	private void sendmail()
    {
        using (MailMessage msgMail = new MailMessage())
        {
            SmtpClient SmtpServer = new SmtpClient("smtpsgwhyd.nic.in");
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            string query = "SELECT CONVERT(nvarchar(20),CR_TIME,103) [EMP_ENROLEMENT_TIME], (select DDO_DESIGNATION from WBHS_DDO_DETAILS where DDO_CD=CHOI.DDO_CODE)[DDO_DESIGNATION]FROM MBUCT_AppId_Clg_ONLINE MAPP JOIN MBUCT_Clg_HOI_Details CHOI ON CHOI.APP_ID= MAPP.APP_ID AND CHOI.HRMS_ID=MAPP.HRMS_ID AND CHOI.SLR_NO=MAPP.SLR_NO WHERE MAPP.APP_ID='" + lbl_appid.Text + "' and MAPP.HRMS_ID='" + lbl_hrmsid.Text + "' and MAPP.SLR_NO='" + HiddenField_SLR_NO.Value + "' and MAPP.IS_EXISTS='1'";
            DataSet ds = NSDataAccessLayer.ServiceLocatorIDataAccessLayer.GetDataAccessLayer().GetDataSetByQuery(query);
            msgMail.To.Add(txt_email.Text.ToLower().Replace(";", ","));
            msgMail.From = new MailAddress("support.hshed-wb@gov.in", "WBHS HED Support");
            string mail_sub = "Employee / Beneficiary information for new online registration";
            msgMail.Subject = mail_sub.ToUpper();
            msgMail.Body = "Dear Sir/Madam," + System.Environment.NewLine + System.Environment.NewLine + "Welcome to the West Bengal Health Scheme For Grant-in-Aid College"
                 + System.Environment.NewLine + System.Environment.NewLine + "The WBHS Application with Application Id " + lbl_appid.Text + " & HRMS Id " + lbl_hrmsid.Text + " for " + System.Environment.NewLine +
                 "Sri/Smt. " + txt_fname.Text.ToUpper() + " " + txt_lname.Text.ToUpper() + " have been successfully submitted to DDO " + ddo_cd_ddlist.SelectedItem.Text.ToUpper() + System.Environment.NewLine +
                 "(" + ds.Tables[0].Rows[0]["DDO_DESIGNATION"].ToString() + ")" + " on " + ds.Tables[0].Rows[0]["EMP_ENROLEMENT_TIME"].ToString() + " for verification. You are therefore requested to contact" +
                 System.Environment.NewLine + "your concerned ddo for further process. " + System.Environment.NewLine
                + System.Environment.NewLine + "Regards," + System.Environment.NewLine +
                System.Environment.NewLine + "Department of Higher Education." + System.Environment.NewLine + System.Environment.NewLine +
                "Statutory warning: Do not share your login ID & password with anyone." + System.Environment.NewLine +
                "This is an automatic system generated e-mail, please do not respond or reply to the sender." + System.Environment.NewLine +
                "Please do not print this e-mail unless you urgently require it, Save environment." +
                System.Environment.NewLine + System.Environment.NewLine +
                "Disclaimer:" + System.Environment.NewLine +
                "The information contained herein (including any accompanying documents) is confidential and is" + System.Environment.NewLine +
                "intended solely for the addressee(s). If you have erroneously received this message, please" + System.Environment.NewLine +
                "immediately delete it and notify support.hshed-wb@nic.in. Also, if you are not the intended" + System.Environment.NewLine +
                "recipient, you are hereby notified that any disclosure, copying, distribution or taking any action in" + System.Environment.NewLine +
                "reliance on the contents of this message or any accompanying document is strictly prohibited and" + System.Environment.NewLine +
                "is unlawful. Govt. of West Bengal is not responsible for any damage caused by a virus or alteration" + System.Environment.NewLine +
                "of the e-mail by a third party or otherwise. The contents of this message may not necessarily" + System.Environment.NewLine +
                "represent the views or policies of Govt. of West Bengal."; ;
            SmtpServer.Port = 465;
            SmtpServer.Credentials = new System.Net.NetworkCredential("support.hshed-wb@gov.in", "Sdwaks@2025");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(msgMail);
        }
    }

    #endregion

    #region Download Form A
    protected void btn_rpt_Click(object sender, ImageClickEventArgs e)
	{
        //mb_rate.Show();
        //string empId = lbl_hrmsid.Text;
        MBUCT_clg_user_bll.hrms_id = lbl_hrmsid.Text;
        //  string appid = lbl_appid.Text;
        MBUCT_clg_user_bll.APP_ID = lbl_appid.Text;
        //  string slrno = HiddenField_SLR_NO.Value;
        MBUCT_clg_user_bll.slr_no = HiddenField_SLR_NO.Value;

        DataSet ds_1 = GET_ONLINE_CLG_APPLICATION_FORM(MBUCT_clg_user_bll);
        DataSet ds_2 = GET_MBUCT_Clg_Family_Details_Bind(MBUCT_clg_user_bll);

        LocalReport localReport = new LocalReport();
        localReport.ReportPath = Server.MapPath("~/Report/College/MBUCT_CLG_EMP_APP_FORM.rdlc");
        ReportDataSource rds = new ReportDataSource("DataSet1", ds_1.Tables[0]);
        ReportDataSource rds1 = new ReportDataSource("DataSet2", ds_2.Tables[0]);
        localReport.DataSources.Add(rds);
        localReport.DataSources.Add(rds1);
        // Render report to byte array
        byte[] bytes = localReport.Render("PDF");
        // Send the PDF to the client browser for download
        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AddHeader(
            "content-disposition",
            "attachment; filename=\"FORM - A for " + MBUCT_clg_user_bll.hrms_id + ".pdf\"");
        Response.Buffer = true;
        Response.BinaryWrite(bytes);
        Response.End();
      //  string empId = lbl_hrmsid.Text;
      //  string appid = lbl_appid.Text;
      //  string slrno = HiddenField_SLR_NO.Value;
      //  string sTargetURL = "";
      //  sTargetURL = "http://10.249.122.97/ReportServer_mssqlreport?" + "/wbhealthscheme/MBUCT_CLG_EMP_APP_FORM&rs:Command=Render&rs:format=PDF&APP_ID=" + appid + "&SLR_NO=" + slrno + "&HRMS_ID=" + empId;
      //  Response.AddHeader("content-disposition", "attachment; filename=\"FORM - A for " + empId + ".pdf\"");
      //  HttpWebRequest req =
      //(HttpWebRequest)WebRequest.Create(sTargetURL);
      //  req.PreAuthenticate = true;
      //  req.Credentials = new System.Net.NetworkCredential(
      //      "sr_wbhealthscheme_r",
      //      "Dzx*#mn75V9876#",
      //      "SP-WEB-REP");

      //  HttpWebResponse HttpWResp = (HttpWebResponse)req.GetResponse();
      //  Stream fStream = HttpWResp.GetResponseStream();
      //  byte[] fileBytes = ReadFully(fStream);
      //  HttpWResp.Close();
      //  Response.Clear();
      //  Response.ContentType = "application/pdf";
      //  Response.BinaryWrite(fileBytes);
    }

    #endregion

    #region Utility Function
    private static byte[] ReadFully(Stream input)
    {

        byte[] buffer = new byte[16 * 1024];
        using (MemoryStream ms = new MemoryStream())
        {
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }
            return ms.ToArray();
        }
    }
    private void DisplayMessage(string msg, int fail)
    {
        try
        {
            btnYes.Style.Add("display", "none");
            if (fail == 0)
            {
                lblReason.ForeColor = System.Drawing.Color.Green;
                lblMsgHeading.Text = "Success !";
                imgMessage.ImageUrl = "../../images/successMessage.png";
                btncancel.Text = "OK";
                btncancel.CssClass = "button_save";
            }
            else if (fail == 1)
            {
                lblMsgHeading.Text = "Error !";
                imgMessage.ImageUrl = "../../images/imgClosePopup.png";
                lblReason.ForeColor = System.Drawing.Color.Red;
                btncancel.Text = "OK";
                btncancel.CssClass = "button_close";
            }
            else if (fail == 2)
            {
                lblMsgHeading.Text = "";
                imgMessage.ImageUrl = "../../images/warningMessage.png";
                lblReason.ForeColor = System.Drawing.Color.Red;
                btnYes.Style.Add("display", "block");
                btncancel.Text = "No";
                //btncancel.CssClass = "button_save";
                btncancel.CssClass = "button_close";
            }
            modalPE.Show();
            lblReason.Text = msg;
            lblReason.Visible = true;
        }
        catch (Exception ex)
        {
        }
    }
    private void DisError(string errorDesc)
    {
        string script = "alert('" + errorDesc + "');";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "UserSecurity", script, true);
    }
    public class ReportServerCredentials : IReportServerCredentials
    {
        private string _userName;
        private string _password;
        private string _domain;
        public ReportServerCredentials(string userName, string password, string domain)
        {
            _userName = userName;
            _password = password;
            _domain = domain;
        }

        public WindowsIdentity ImpersonationUser
        {
            get
            {
                // Use default identity.
                return null;
            }
        }
        public ICredentials NetworkCredentials
        {
            get
            {
                // Use default identity.
                return new NetworkCredential(_userName, _password, _domain);
            }
        }
        public bool GetFormsCredentials(out Cookie authCookie, out string user, out string password, out string authority)
        {
            // Do not use forms credentials to authenticate.
            authCookie = null;
            user = password = authority = null;
            return false;
        }
    }
    public string ReadFileFromServer(string fileName, string folder, string subFolder, string subSubFolder, string subSubSubFolder, bool isUrl)
    {
        string outputpath = string.Empty;

        try
        {

            //string Fileserverpath = "http://172.25.146.178/SDCFILESERVER/"; // Primary Path 
            string Fileserverpath = "http://192.168.1.69/SDCFILESERVER/"; // Primary Path 

            string Fileserverpath_F = "http://172.25.xxx.xxx/sdcfileserverread_F/sdcfileserver/"; // Secendery Path E Drive 


            String LocalServerpath = Path.Combine(HttpContext.Current.Server.MapPath("~/Tempfile/"));


            if (fileName == "")
            {
                return "File Name is Missing";
            }


            if (folder == "")
            {
                return "Folder Name is Missing";
            }

            //if (CheckfileFoldername(fileName) == true)
            //{
            //    return "File Name Contains Special Character ";
            //}




            //if (CheckfileFoldername(folder) == true)
            //{
            //    return "folder Name Contains Special Character ";
            //}
            String Directorypath = Path.Combine(folder.Trim('/').Trim('\\'));
            //Directorypath = Fileserverpath + Directorypath;


            if (subFolder != "")
            {
                //if (CheckfileFoldername(subFolder) == true)
                //{
                //    return "subFolder Name Contains Special Character ";
                //}


                Directorypath = Path.Combine(folder.Trim('/').Trim('\\'), subFolder.Trim('/').Trim('\\'));
                //Directorypath = Fileserverpath + Directorypath;

            }

            if (subSubFolder != "")
            {
                //if (CheckfileFoldername(subSubFolder) == true)
                //{
                //    return "subSubFolder Name Contains Special Character ";
                //}
                Directorypath = Path.Combine(folder.Trim('/').Trim('\\'), subFolder.Trim('/').Trim('\\'), subSubFolder.Trim('/').Trim('\\'));
                // Directorypath = Fileserverpath + Directorypath;

            }
            if (subSubSubFolder != "")
            {
                Directorypath = Path.Combine(folder.Trim('/').Trim('\\'), subFolder.Trim('/').Trim('\\'), subSubFolder.Trim('/').Trim('\\'), subSubSubFolder.Trim('/').Trim('\\'));
            }

            string ActualFilePath = "";

            string dbfilename = fileName;


            string path = Path.Combine(Fileserverpath + Directorypath, fileName); // Primay Pathe 

            path = Path.Combine(Fileserverpath + Directorypath, fileName);
            path = path.Replace('\\', '/').Trim('/');

            HttpStatusCode result = default(HttpStatusCode);
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path);
                request.Method = "GET";

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if (response != null)
                    {
                        result = response.StatusCode;
                        response.Close();
                    }
                }
            }
            catch (WebException ex)
            {
                result = ((HttpWebResponse)ex.Response).StatusCode;
            }

            if (result == HttpStatusCode.NotFound) // Primay Pathe 
            {
                // ==========================================
                if (Directorypath == "FarmerDocument\\Voter")
                {
                    fileName = fileName.Replace("_2.pdf", "_1.pdf");
                }
                if (Directorypath == "FarmerDocument\\Land")
                {
                    fileName = fileName.Replace("_3.pdf", "_1.pdf");
                }
                if (Directorypath == "FarmerDocument\\Bank")
                {
                    fileName = fileName.Replace("_4.pdf", "_1.pdf");
                }
                if (Directorypath == "FarmerDocument\\Aadhar")
                {
                    fileName = fileName.Replace("_5.pdf", "_1.pdf");
                }

                path = Path.Combine(Fileserverpath + Directorypath, fileName); // Primay Pathe 

                path = Path.Combine(Fileserverpath + Directorypath, fileName);
                path = path.Replace('\\', '/').Trim('/');

                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path);
                    request.Method = "GET";

                    using (var response = request.GetResponse() as HttpWebResponse)
                    {
                        if (response != null)
                        {
                            result = response.StatusCode;
                            response.Close();
                        }
                    }
                }
                catch (WebException ex)
                {
                    result = ((HttpWebResponse)ex.Response).StatusCode;
                }

            }


            //==========================================



            if (result == HttpStatusCode.NotFound) // Primay Pathe 
            {
                // ==========================================
                if (Directorypath == "FarmerDocument\\Voter")
                {
                    fileName = fileName.Replace("_2.jpg", "_1.jpg");
                }
                if (Directorypath == "FarmerDocument\\Land")
                {
                    fileName = fileName.Replace("_3.jpg", "_1.jpg");
                }
                if (Directorypath == "FarmerDocument\\Bank")
                {
                    fileName = fileName.Replace("_4.jpg", "_1.jpg");
                }
                if (Directorypath == "FarmerDocument\\Aadhar")
                {
                    fileName = fileName.Replace("_5.jpg", "_1.jpg");
                }

                path = Path.Combine(Fileserverpath + Directorypath, fileName); // Primay Pathe 

                path = Path.Combine(Fileserverpath + Directorypath, fileName);
                path = path.Replace('\\', '/').Trim('/');

                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path);
                    request.Method = "GET";

                    using (var response = request.GetResponse() as HttpWebResponse)
                    {
                        if (response != null)
                        {
                            result = response.StatusCode;
                            response.Close();
                        }
                    }
                }
                catch (WebException ex)
                {
                    result = ((HttpWebResponse)ex.Response).StatusCode;
                }

            }

            //==========================================

            if (result == HttpStatusCode.NotFound) // Primay Pathe 
            {
                // ==========================================
                if (Directorypath == "FarmerDocument\\Voter")
                {
                    fileName = fileName.Replace("_2.jpeg", "_1.jpeg");
                }
                if (Directorypath == "FarmerDocument\\Land")
                {
                    fileName = fileName.Replace("_3.jpeg", "_1.jpeg");
                }
                if (Directorypath == "FarmerDocument\\Bank")
                {
                    fileName = fileName.Replace("_4.jpeg", "_1.jpeg");
                }
                if (Directorypath == "FarmerDocument\\Aadhar")
                {
                    fileName = fileName.Replace("_5.jpeg", "_1.jpeg");
                }

                path = Path.Combine(Fileserverpath + Directorypath, fileName); // Primay Pathe 

                path = Path.Combine(Fileserverpath + Directorypath, fileName);
                path = path.Replace('\\', '/').Trim('/');

                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path);
                    request.Method = "GET";

                    using (var response = request.GetResponse() as HttpWebResponse)
                    {
                        if (response != null)
                        {
                            result = response.StatusCode;
                            response.Close();
                        }
                    }
                }
                catch (WebException ex)
                {
                    result = ((HttpWebResponse)ex.Response).StatusCode;
                }

            }


            //======PRIMARY PATH END======================================================================================================================

            #region ANOTHERDRIVE_CODE
            //string path_F = Path.Combine(Fileserverpath_F + Directorypath, dbfilename); // Secendery Path E: Drive Path
            //path_F = Path.Combine(Fileserverpath_F + Directorypath, dbfilename);
            //path_F = path_F.Replace('\\', '/').Trim('/');

            //HttpStatusCode result_F = default(HttpStatusCode);
            //try
            //{
            //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path_F);
            //    request.Method = "GET";

            //    using (var response = request.GetResponse() as HttpWebResponse)
            //    {
            //        if (response != null)
            //        {
            //            result_F = response.StatusCode;
            //            response.Close();
            //        }
            //    }
            //}
            //catch (WebException ex)
            //{
            //    result_F = ((HttpWebResponse)ex.Response).StatusCode;
            //}


            //if (result_F == HttpStatusCode.NotFound) // Primay Pathe 
            //{

            //    if (Directorypath == "FarmerDocument\\Voter")
            //    {
            //        fileName = fileName.Replace("_2.pdf", "_1.pdf");
            //    }
            //    if (Directorypath == "FarmerDocument\\Land")
            //    {
            //        fileName = fileName.Replace("_3.pdf", "_1.pdf");
            //    }
            //    if (Directorypath == "FarmerDocument\\Bank")
            //    {
            //        fileName = fileName.Replace("_4.pdf", "_1.pdf");
            //    }
            //    if (Directorypath == "FarmerDocument\\Aadhar")
            //    {
            //        fileName = fileName.Replace("_5.pdf", "_1.pdf");
            //    }


            //    path_F = Path.Combine(Fileserverpath_F + Directorypath, fileName); // Secendery Path E: Drive Path
            //    path_F = Path.Combine(Fileserverpath_F + Directorypath, fileName);
            //    path_F = path_F.Replace('\\', '/').Trim('/');

            //    try
            //    {
            //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path_F);
            //        request.Method = "GET";

            //        using (var response = request.GetResponse() as HttpWebResponse)
            //        {
            //            if (response != null)
            //            {
            //                result_F = response.StatusCode;
            //                response.Close();
            //            }
            //        }
            //    }
            //    catch (WebException ex)
            //    {
            //        result_F = ((HttpWebResponse)ex.Response).StatusCode;
            //    }



            //}

            //if (result_F == HttpStatusCode.NotFound) // Primay Pathe 
            //{
            //    // ==========================================
            //    if (Directorypath == "FarmerDocument\\Voter")
            //    {
            //        fileName = fileName.Replace("_2.jpg", "_1.jpg");
            //    }
            //    if (Directorypath == "FarmerDocument\\Land")
            //    {
            //        fileName = fileName.Replace("_3.jpg", "_1.jpg");
            //    }
            //    if (Directorypath == "FarmerDocument\\Bank")
            //    {
            //        fileName = fileName.Replace("_4.jpg", "_1.jpg");
            //    }
            //    if (Directorypath == "FarmerDocument\\Aadhar")
            //    {
            //        fileName = fileName.Replace("_5.jpg", "_1.jpg");
            //    }

            //    path_F = Path.Combine(Fileserverpath_F + Directorypath, fileName); // Primay Pathe 

            //    path_F = Path.Combine(Fileserverpath_F + Directorypath, fileName);
            //    path_F = path_F.Replace('\\', '/').Trim('/');

            //    try
            //    {
            //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path_F);
            //        request.Method = "GET";

            //        using (var response = request.GetResponse() as HttpWebResponse)
            //        {
            //            if (response != null)
            //            {
            //                result_F = response.StatusCode;
            //                response.Close();
            //            }
            //        }
            //    }
            //    catch (WebException ex)
            //    {
            //        result_F = ((HttpWebResponse)ex.Response).StatusCode;
            //    }
            //}

            //if (result_F == HttpStatusCode.NotFound) // Primay Pathe 
            //{
            //    // ==========================================
            //    if (Directorypath == "FarmerDocument\\Voter")
            //    {
            //        fileName = fileName.Replace("_2.jpeg", "_1.jpeg");
            //    }
            //    if (Directorypath == "FarmerDocument\\Land")
            //    {
            //        fileName = fileName.Replace("_3.jpeg", "_1.jpeg");
            //    }
            //    if (Directorypath == "FarmerDocument\\Bank")
            //    {
            //        fileName = fileName.Replace("_4.jpeg", "_1.jpeg");
            //    }
            //    if (Directorypath == "FarmerDocument\\Aadhar")
            //    {
            //        fileName = fileName.Replace("_5.jpeg", "_1.jpeg");
            //    }

            //    path_F = Path.Combine(Fileserverpath_F + Directorypath, fileName); // Primay Pathe 

            //    path_F = Path.Combine(Fileserverpath_F + Directorypath, fileName);
            //    path_F = path_F.Replace('\\', '/').Trim('/');

            //    try
            //    {
            //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path_F);
            //        request.Method = "GET";

            //        using (var response = request.GetResponse() as HttpWebResponse)
            //        {
            //            if (response != null)
            //            {
            //                result_F = response.StatusCode;
            //                response.Close();
            //            }
            //        }
            //    }
            //    catch (WebException ex)
            //    {
            //        result_F = ((HttpWebResponse)ex.Response).StatusCode;
            //    }



            //    //==========================================


            //}
            #endregion
            //XXXXXXXX=========END===========XXXXXXXXXXXXXXXXXXXX==========END=============XXXXXXXXXXXXXXX


            if (result == HttpStatusCode.OK) // Primay Pathe 
            {
                ActualFilePath = path;
            }
            //if (result_F == HttpStatusCode.OK) // Primay Pathe 
            //{
            //    ActualFilePath = path_F;
            //}



            string Downloadpath = Path.Combine(LocalServerpath, Directorypath);

            if (!Directory.Exists(Downloadpath))
            {
                Directory.CreateDirectory(Downloadpath);
            }

            string domainName = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);

            Downloadpath = Path.Combine(LocalServerpath, Directorypath, fileName.Trim('/').Trim('\\'));



            (new WebClient()).DownloadFile(ActualFilePath, Downloadpath); // mirror uploaded file to another server

            string Retrufilepath = string.Empty;

            if (isUrl == true)
            {
                // Retrufilepath = HttpContext.Current.Server.MapPath("~/Tempfile") + "\\" + Directorypath + "\\" + fileName.Trim('/').Trim('/');
                Retrufilepath = domainName + "/Tempfile" + "\\" + Directorypath + "\\" + fileName.Trim('/').Trim('/');
                Retrufilepath = Retrufilepath.Replace('\\', '/').Trim('/');
            }
            if (isUrl == false)
            {
                Retrufilepath = domainName + "/Tempfile" + "\\" + Directorypath + "\\" + fileName.Trim('/').Trim('/');

                //Create Signature 
                //String Sig = GetFileReadSignature();
                HttpCookie filesignatureCookie = new HttpCookie("filesignature");
                filesignatureCookie.Value = Retrufilepath;
                filesignatureCookie.Expires = DateTime.Now.AddSeconds(180);
                HttpContext.Current.Response.Cookies.Add(filesignatureCookie);

                Retrufilepath = domainName + "/SDCFileViewer.aspx";

            }

            return Retrufilepath;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    }
    #endregion

    #region Data Access Layer

    #region Insert
    private bool INSERT_MBUCT_ClgbasicInfo_Online(string slr_no, string app_id, string hrms_id, string fname, string lname, string dob, string mrt_stat,
        string gender, string dist, string address, string id_prf, string aadhaar_no, string mob, string email, string phNo, string redate,
        string ifsc_cd, string bnk_name, string bnk_branch, string bnk_micr, string bnk_acno, string is_exists, string id_type, string retire_age_yr)
    {
        try
        {
            SqlParameterCollection spm = new SqlCommand().Parameters;
            spm.AddWithValue("@slr_no", slr_no);
            spm.AddWithValue("@app_id", app_id);
            spm.AddWithValue("@hrms_id", hrms_id);
            spm.AddWithValue("@clg_fnm", fname);
            spm.AddWithValue("@clg_lnm", lname);
            spm.AddWithValue("@clg_dob", dob);
            spm.AddWithValue("@mt_stat", mrt_stat);
            spm.AddWithValue("@gen", gender);
            if (dist == "")
            {
                spm.AddWithValue("@dist_cd", DBNull.Value);
            }
            else
            {
                spm.AddWithValue("@dist_cd", dist);
            }
            spm.AddWithValue("@addr", address);
            spm.AddWithValue("@id_prf", id_prf);
            spm.AddWithValue("@aadhaar_no", aadhaar_no);
            spm.AddWithValue("@mob_no", mob);
            spm.AddWithValue("@email_id", email);
            if (phNo == "")
            {
                spm.AddWithValue("@ph_no", DBNull.Value);
            }
            else
            {
                spm.AddWithValue("@ph_no", phNo);
            }

            spm.AddWithValue("@redate", redate);
            spm.AddWithValue("@bnk_ifsc", ifsc_cd);
            spm.AddWithValue("@bnk_nm", bnk_name);
            spm.AddWithValue("@bnk_br_nm", bnk_branch);
            spm.AddWithValue("@bnk_micr", bnk_micr);
            spm.AddWithValue("@bnk_acno", bnk_acno);

            spm.AddWithValue("@is_exists", is_exists);
            spm.AddWithValue("@id_type", id_type);
            spm.AddWithValue("@retire_age_yr", retire_age_yr);
            NSDataAccessLayer.ServiceLocatorIDataAccessLayer.GetDataAccessLayer().ExecuteNonQueryBYStoredProcedure("INSERT_mbuct_clgBasicInfo_online", spm);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private bool INSERT_MBUCT_Clg_office_Details(string slr_no, string app_id, string hrms_id, string clg_nm, string clg_dist, string cld_sub, string clg_blk,
        string clg_addr, string doj, string desig, string ropa, string pay_bnd, string gd_pay, string bnd_pay, string basic_pay, string pay_level, string basic_sal,
        string wrd_nm, string wrd_govt, string wrd_tmc, string is_exists)
    {
        SqlParameterCollection spm = new SqlCommand().Parameters;
        spm.AddWithValue("@slr_no", slr_no);
        spm.AddWithValue("@app_id", app_id);
        spm.AddWithValue("@hrms_id", hrms_id);
        spm.AddWithValue("@clg_nm", clg_nm);
        spm.AddWithValue("@clg_dist", clg_dist);
        spm.AddWithValue("@clg_sub_dv", cld_sub);
        spm.AddWithValue("@clg_blk", clg_blk);
        spm.AddWithValue("@clg_addr", clg_addr);
        spm.AddWithValue("@doj", doj);
        spm.AddWithValue("@desig", desig);
        spm.AddWithValue("@ropa", ropa);

        if (pay_bnd == "")
        {
            spm.AddWithValue("@pay_band", DBNull.Value);
        }
        else
        {
            spm.AddWithValue("@pay_band", pay_bnd);
        }
        if (gd_pay == "")
        {
            spm.AddWithValue("@gd_pay", DBNull.Value);
        }
        else
        {
            spm.AddWithValue("@gd_pay", gd_pay);
        }
        if (bnd_pay == "")
        {
            spm.AddWithValue("@band_pay", DBNull.Value);
        }
        else
        {
            spm.AddWithValue("@band_pay", bnd_pay);
        }
        if (basic_pay == "")
        {
            spm.AddWithValue("@basic_pay", DBNull.Value);
        }
        else
        {
            spm.AddWithValue("@basic_pay", basic_pay);
        }
        if (pay_level == "")
        {
            spm.AddWithValue("@pay_level", DBNull.Value);
        }
        else
        {
            spm.AddWithValue("@pay_level", pay_level);
        }
        if (basic_sal == "")
        {
            spm.AddWithValue("@basic_sal", DBNull.Value);
        }
        else
        {
            spm.AddWithValue("@basic_sal", basic_sal);
        }
        spm.AddWithValue("@ward_name", wrd_nm);
        spm.AddWithValue("@ward_govt", wrd_govt);
        spm.AddWithValue("@ward_tata", wrd_tmc);
        spm.AddWithValue("@IS_EXISTS", is_exists);
        NSDataAccessLayer.ServiceLocatorIDataAccessLayer.GetDataAccessLayer().ExecuteNonQueryBYStoredProcedure("INSERT_mbuct_Ofice_Details", spm);
        return true;
    }

    private bool INSERT_MBUCT_clg_familyDetails(string slr_no, string APP_ID, string HRMS_ID, string BEN_ID, string BEN_NM, string BEN_DOB, string AGE, string INCOME, string RELATION, string BLD_GRP, string AADHAAR_NO, string BEN_PHOTO_FILE_NAME, byte[] BEN_IMG_PHOTO,
        string BEN_SIG_FILE_NAME, byte[] BEN_IMG_SIG, string PHOTO_FTP, string SIGN_FTP, string UPLOADING_ID, string UPLOADING_IP, string BEN_MOB, string BEN_EMAIL, string BEN_CAT, string IS_EXISTS)
    {
        SqlParameterCollection spm = new SqlCommand().Parameters;
        spm.AddWithValue("@slr_no", slr_no);
        spm.AddWithValue("@APP_ID", APP_ID);
        spm.AddWithValue("@HRMS_ID", HRMS_ID);
        spm.AddWithValue("@BEN_ID", BEN_ID);
        spm.AddWithValue("@BEN_NM", BEN_NM);
        spm.AddWithValue("@BEN_DOB", BEN_DOB);
        spm.AddWithValue("@AGE", AGE);
        spm.AddWithValue("@INCOME", INCOME);
        spm.AddWithValue("@RELATION", RELATION);
        spm.AddWithValue("@BLD_GRP", BLD_GRP);
        //spm.AddWithValue("@ID_TYPE", ID_TYPE);
        spm.AddWithValue("@AADHAAR_NO", AADHAAR_NO);
        spm.AddWithValue("@BEN_PHOTO_FILE_NAME", BEN_PHOTO_FILE_NAME);
        spm.AddWithValue("@BEN_IMG_PHOTO", BEN_IMG_PHOTO); //Binary data saving in DB has been stopped 13.06.24 Done in storeprocedure
        spm.AddWithValue("@BEN_SIG_FILE_NAME", BEN_SIG_FILE_NAME);
        spm.AddWithValue("@BEN_IMG_SIG", BEN_IMG_SIG); //Binary data saving in DB has been stopped 13.06.24 Done in storeprocedure
        spm.AddWithValue("@PHOTO_FTP", PHOTO_FTP);
        spm.AddWithValue("@SIGN_FTP", SIGN_FTP);
        spm.AddWithValue("@UPLOADING_ID", UPLOADING_ID);
        spm.AddWithValue("@UPLOADING_IP", UPLOADING_IP);
        spm.AddWithValue("@BEN_MOB", BEN_MOB);
        spm.AddWithValue("@BEN_EMAIL", BEN_EMAIL);
        spm.AddWithValue("@BEN_CAT", BEN_CAT);
        spm.AddWithValue("@IS_EXISTS", IS_EXISTS);
        NSDataAccessLayer.ServiceLocatorIDataAccessLayer.GetDataAccessLayer().ExecuteNonQueryBYStoredProcedure("INSERT_MBUCT_clg_familyDetails", spm);
        return true;
    }

    private bool INSERT_MBUCT_CLG_HOI_DETAILS(string slr_no, string APP_ID, string HRMS_ID, string HOI_DESIG, string DDO_DESIG, string DDO_DIST, string TR_CODE, string DDO_CODE, string IS_EXISTS)
    {
        try
        {
            SqlParameterCollection spm = new SqlCommand().Parameters;
            spm.AddWithValue("@slr_no", slr_no);
            spm.AddWithValue("@app_id", APP_ID);
            spm.AddWithValue("@hrms_id", HRMS_ID);
            spm.AddWithValue("@hoi_desig", HOI_DESIG);
            if (DDO_DESIG == "")
            {
                spm.AddWithValue("@ddo_desig", DBNull.Value);
            }
            else
            {
                spm.AddWithValue("@ddo_desig", DDO_DESIG);
            }

            spm.AddWithValue("@ddo_dist", DDO_DIST);
            spm.AddWithValue("@tr_cd", TR_CODE);
            spm.AddWithValue("@ddo_cd", DDO_CODE);
            spm.AddWithValue("@is_exists", IS_EXISTS);
            NSDataAccessLayer.ServiceLocatorIDataAccessLayer.GetDataAccessLayer().ExecuteNonQueryBYStoredProcedure("INSERT_mbuct_clg_hoi_details", spm);
            return true;
        }
        catch
        {
            return false;
        }
    }

    #endregion

    #region beneficiary_critical_disease
    public DataSet BeneficiaryDisease_GetDetails()
	{
		DataSet ds = new DataSet();
		ds = ServiceLocatorIDataAccessLayer.GetDataAccessLayer().GetDataSetByStoredProcedure("WBHS_Master_BeneficiaryDisease_GetDetails");
		return ds;
	}
	public bool BeneficiaryDisease_Insert(string SLR_NO, string APP_ID, string BEN_ID, int flag, string DiseaseCode)
	{
		try
		{
			SqlParameterCollection spm = new SqlCommand().Parameters;//

			spm.AddWithValue("@slr_no", SLR_NO);
			spm.AddWithValue("@AppId", APP_ID);
			spm.AddWithValue("@BenId", BEN_ID);
			spm.AddWithValue("@CountFlag", flag);
			spm.AddWithValue("@DiseaseId", DiseaseCode);

			NSDataAccessLayer.ServiceLocatorIDataAccessLayer.GetDataAccessLayer().ExecuteNonQueryBYStoredProcedure("INSERT_MBUCT_Clg_BeneficiaryDisease", spm);
			return true;
		}
		catch (Exception ex)
		{

			return false;
		}
	}
	public DataSet CheckBenificiaryValidFor_CriticalDisease(string IdNo)
	{
		DataSet ds = new DataSet();
		SqlParameterCollection spm = new SqlCommand().Parameters;
		spm.AddWithValue("@IdNo", IdNo);
		ds = ServiceLocatorIDataAccessLayer.GetDataAccessLayer().GetdataSetByStoredProcedureWithParameter("MBUCT_CheckBenificiaryValidFor_CriticalDisease", spm);
		return ds;
	}
	public DataSet BeneficiaryDisease_GenerateAppId(string Appid)
	{
		DataSet ds = new DataSet();
		SqlParameterCollection spm = new SqlCommand().Parameters;
		spm.AddWithValue("@Appid", Appid);
		ds = ServiceLocatorIDataAccessLayer.GetDataAccessLayer().GetdataSetByStoredProcedureWithParameter("WBHS_BeneficiaryDisease_GenerateAppId", spm);
		return ds;
	}
	public DataSet BenificiaryDiseaseLisByIdNo(string IdNo)
	{
		DataSet ds = new DataSet();
		SqlParameterCollection spm = new SqlCommand().Parameters;
		spm.AddWithValue("@IdNo", IdNo);
		ds = ServiceLocatorIDataAccessLayer.GetDataAccessLayer().GetdataSetByStoredProcedureWithParameter("WBHS_BenificiaryDiseaseLisByIdNo", spm);
		return ds;
	}

	public bool BeneficiaryDisease_UPDATE(string AppCode, string IdNo, int flag, string DiseaseCode)
	{

		try
		{
			SqlParameterCollection spm = new SqlCommand().Parameters;//

			spm.AddWithValue("@AppId", AppCode);
			spm.AddWithValue("@IdNo", IdNo);
			spm.AddWithValue("@CountFlag", flag);
			spm.AddWithValue("@DiseaseId", DiseaseCode);

			ServiceLocatorIDataAccessLayer.GetDataAccessLayer().ExecuteNonQueryBYStoredProcedure("WBHS_BeneficiaryDisease_UPDATE", spm);
			return true;
		}
		catch (Exception ex)
		{

			return false;
		}
	}
	public bool BeneficiaryDisease_DELETE(string IdNo)
	{

		try
		{
			SqlParameterCollection spm = new SqlCommand().Parameters;
			spm.AddWithValue("@IdNo", IdNo);
			ServiceLocatorIDataAccessLayer.GetDataAccessLayer().ExecuteNonQueryBYStoredProcedure("WBHS_BeneficiaryDisease_DELETE", spm);
			return true;
		}
		catch (Exception ex)
		{

			return false;
		}
	}
	public bool BeneficiaryDisease_FinalddoVarification(string AppId, Int32 Status)
	{

		try
		{
			SqlParameterCollection spm = new SqlCommand().Parameters;
			spm.AddWithValue("@AppId", AppId);
			spm.AddWithValue("@STS", Status);
			ServiceLocatorIDataAccessLayer.GetDataAccessLayer().ExecuteNonQueryBYStoredProcedure("WBHS_BeneficiaryDisease_FinalddoVarification", spm);
			return true;
		}
		catch (Exception ex)
		{

			return false;
		}
	}
	#endregion

    #region Get

    private DataSet GET_ONLINE_CLG_APPLICATION_FORM(MBUCT_Clg_User_bll getAppForm)
    {
        SqlParameterCollection spm = new SqlCommand().Parameters;
        spm.AddWithValue("@APP_ID", getAppForm.APP_ID);
        spm.AddWithValue("@HRMS_ID", getAppForm.hrms_id);
        spm.AddWithValue("@SLR_NO", getAppForm.slr_no);
        return NSDataAccessLayer.ServiceLocatorIDataAccessLayer.GetDataAccessLayer().GetdataSetByStoredProcedureWithParameter("GET_ONLINE_CLG_APPLICATION_FORM", spm);
    }
    private DataSet GET_MBUCT_Clg_Family_Details_Bind(MBUCT_Clg_User_bll getAppFam)
    {
        SqlParameterCollection spm = new SqlCommand().Parameters;
        spm.AddWithValue("@APP_ID", getAppFam.APP_ID);
        spm.AddWithValue("@HRMS_ID", getAppFam.hrms_id);
        spm.AddWithValue("@SLR_NO", getAppFam.slr_no);
        return NSDataAccessLayer.ServiceLocatorIDataAccessLayer.GetDataAccessLayer().GetdataSetByStoredProcedureWithParameter("GET_MBUCT_Clg_Family_Details_Bind", spm);
    }
    #endregion

	#endregion
}
