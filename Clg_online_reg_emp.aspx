<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Higher_Education/Master_pages/Admin_Master.master" AutoEventWireup="true" CodeFile="Clg_online_reg_emp.aspx.cs" Inherits="Higher_Education_OnlineRegistration_Clg_online_reg_emp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <%--<link href="../css/style.css" rel="stylesheet" type="text/css" />--%>
    <script src="../js/jquery-1.9.1.js" type="text/javascript"></script>
    <link href="../css/jquery-ui-1.10.3.custom.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-ui-1.10.3.custom.js" type="text/javascript"></script>
    <link href="../css/uploadfile.css" rel="stylesheet" type="text/css" />
    <link href="../css/all_StyleSheet.css" rel="stylesheet" />
    
    <style type="text/css">
        body {
            margin: 0;
            padding: 0;
            font-family: Arial;
        }

        /*.modal1 {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0px;
            left: 0px;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }*/

        .center {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 130px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }

            .center img {
                height: 128px;
                width: 128px;
            }
            /*AutoComplete flyout */
        .completionListElement
        {
            visibility: hidden;
            margin: 0px !important;
            background-color: inherit;
            color: #0a64b4;
            border: solid 1px #0a64b4;
            cursor: pointer;
            text-align: left;
            list-style-type: none;
            font-family: cambria;
            font-weight: bold;
            font-size: large;
            padding: 0;
        }
        
        .listItem
        {
            background-color: #ccffcc;
            padding: 1px;
        }
        
        .highlightedListItem
        {
            background-color: #0a64b4;
            padding: 1px;
            color: #ffffff;
        }
        
        .close
        {
            position: absolute;
            top: 0;
            right: 0;
        }
        
        .hiddencol
        {
            display: none;
        }
        .auto-style3 {
            text-align: left;
            height: 10px;
            padding-left: 0px;
            font-family: Comic Sans MS;
            font-size: 9px;
            color: Navy;
            font-weight: bold;
            width: 50%;
        }
        .auto-style4 {
            text-align: left;
            height: 10px; /*font-family: Comic Sans MS;*/ /*font-size: 9px;*/;
            color: Black;
            width: 50%;
        }
        .divWaiting {
            position: fixed;
            background-color: #000;
            z-index: 2147483647 !important;
            opacity: 0.6;
            overflow: hidden;
            text-align: center;
            top: 0;
            left: 0;
            height: 100%;
            width: 100%;
            padding-top: 20%;
        }

        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .button_save {
            border-top: 1px solid #97f7ad;
            background: #65d67f;
            background: -webkit-gradient(linear, left top, left bottom, from(#3e9c45), to(#65d67f));
            background: -webkit-linear-gradient(top, #3e9c45, #65d67f);
            background: -moz-linear-gradient(top, #3e9c45, #65d67f);
            background: -ms-linear-gradient(top, #3e9c45, #65d67f);
            background: -o-linear-gradient(top, #3e9c45, #65d67f);
            padding: 5px 10px;
            -webkit-border-radius: 8px;
            -moz-border-radius: 8px;
            border-radius: 8px;
            -webkit-box-shadow: rgba(0,0,0,1) 0 1px 0;
            -moz-box-shadow: rgba(0,0,0,1) 0 1px 0;
            box-shadow: rgba(0,0,0,1) 0 1px 0;
            text-shadow: rgba(0,0,0,.4) 0 1px 0;
            color: white;
            font-size: 14px;
            font-family: Georgia, serif;
            text-decoration: none;
            vertical-align: middle;
        }

            .button_save:hover {
                border-top-color: #307829;
                background: #307829;
                color: #ffffff;
            }

            .button_save:active {
                border-top-color: #385c1b;
                background: #385c1b;
            }
        /* Close Button */
        .button_close {
            border-top: 1px solid #f59898;
            background: #d46565;
            background: -webkit-gradient(linear, left top, left bottom, from(#9c3e3e), to(#d46565));
            background: -webkit-linear-gradient(top, #9c3e3e, #d46565);
            background: -moz-linear-gradient(top, #9c3e3e, #d46565);
            background: -ms-linear-gradient(top, #9c3e3e, #d46565);
            background: -o-linear-gradient(top, #9c3e3e, #d46565);
            padding: 5px 10px;
            -webkit-border-radius: 8px;
            -moz-border-radius: 8px;
            border-radius: 8px;
            -webkit-box-shadow: rgba(0,0,0,1) 0 1px 0;
            -moz-box-shadow: rgba(0,0,0,1) 0 1px 0;
            box-shadow: rgba(0,0,0,1) 0 1px 0;
            text-shadow: rgba(0,0,0,.4) 0 1px 0;
            color: white;
            font-size: 14px;
            font-family: Georgia, serif;
            text-decoration: none;
            vertical-align: middle;
        }

            .button_close:hover {
                border-top-color: #782a2a;
                background: #782a2a;
                color: #ffffff;
            }

            .button_close:active {
                border-top-color: #5c1b1b;
                background: #5c1b1b;
            }
    </style>

    <script type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            $(".datepicker").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd/mm/yy'
            });
        });
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            $(".datepicker1").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy'
            });
        });

        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("You cannot select future date");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value("")

            }
        }
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#img_photo').attr('src', e.target.result);
                }

                reader.readAsDataURL(input.files[0]);
            }
        }
        $("#fup_photo").change(function () {
            readURL(this);
        });
        function readURL_sign(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#img_sign').attr('src', e.target.result);
                }

                reader.readAsDataURL(input.files[0]);
            }
        }
        $("#fup_sign").change(function () {
            readURL_sign(this);
        });
    
        function lettersOnly(evt) {
            //debugger;
            evt = (evt) ? evt : event;
            var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode :
               ((evt.which) ? evt.which : 0));
            if (charCode > 31 && (charCode < 65 || charCode > 90) &&
               (charCode < 97 || charCode > 122)) {
                //alert("Enter letters only.");
                return false;
            }
            return true;
        }
        function letterspace(evt) {
            //debugger;
            evt = (evt) ? evt : event;
            var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode :
               ((evt.which) ? evt.which : 0));
            if (charCode > 32 && (charCode < 65 || charCode > 90) &&
               (charCode < 97 || charCode > 122)) {
                //alert("Enter letters only.");
                return false;
            }
            return true;
        }
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

        function blockSpecialChar(e) {
            var k = e.keyCode;
            return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || (k >= 48 && k <= 57));
        }
        function blockSpecialCharExceptSpace(e) {
            var k = e.keyCode;
            return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || (k >= 48 && k <= 57) || k == 32);
        }
        function validateemail() {
            var x = document.myform.email.value;
            var atposition = x.indexOf("@");
            var dotposition = x.lastIndexOf(".");
            if (atposition < 1 || dotposition < atposition + 2 || dotposition + 2 >= x.length) {
                alert("Please enter a valid e-mail address \n atpostion:" + atposition + "\n dotposition:" + dotposition);
                return false;
            }
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%; background-color:ghostwhite">
                <tr>
                    <td align="right" colspan="3" style="padding-right: 10px; padding-top: 5px;">
                        <asp:ImageButton ID="ImageButton6" runat="server" Height="32px" ImageUrl="~/Higher_Education/images1/button_home.png" Width="100px" OnClick="ImageButton6_Click" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td align="center">

                        <table  style="width: 900px; background-image:url('../../images/bg-back-emp-pen.jpg'); border-radius: 8px">
                            <tr>
                                <td align="center" style="height: 19px"></td>

                            </tr>
                            <tr>
                                <td align="center">
                                    <span style="font-size: X-large; font-weight: bold">O</span><span style="font-size: medium">NLINE</span>
                                    <span style="font-size: X-large; font-weight: bold">R</span><span style="font-size: medium">EGISTRATION</span>
                                    <span style="font-size: X-large; font-weight: bold">F</span><span style="font-size: medium">OR</span>
                                    <span style="font-size: X-large; font-weight: bold">G</span><span style="font-size: medium">RANT-in-AID</span>
                                    <span style="font-size: X-large; font-weight: bold">C</span><span style="font-size: medium">OLLEGES</span>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <table align="left" width="658px" cellpadding="3">
                                        <tr>
                                            <td align="left">
                                                <AjaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="2" Font-Names="verdana"
                                                    AutoPostBack="false" Width="900px" Font-Size="Small" CssClass="MyTabStyle">
                                                    
                                                    <AjaxToolkit:TabPanel HeaderText="PERSONAL DETAILS" runat="server" ID="per_info_tab"
                                                        Width="900px">
                                                        <HeaderTemplate>PERSONAL DETAILS</HeaderTemplate>
                                                        <ContentTemplate>
                                                            <div>
                                                                <table width="900px" style="background-color:white;">
                                                                    <tr>
                                                                        <td colspan="2" style="padding-top: 10px"></td>
                                                                    </tr>
                                                                    <tr class="form-level" style="padding-left: 20px;">
                                                                        <td class="form-level" style="padding-left: 190px; width: 50%;">
                                                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Application Id Number:"></asp:Label>
                                                                        </td>
                                                                        <td class="form-level" style="padding-left: 45px;">
                                                                            <asp:Label ID="lbl_appid" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Small" ForeColor="#EE3535" Width="200px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 190px; width: 50%;">
                                                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="HRMS/Unique Id:"></asp:Label>
                                                                        </td>
                                                                        <td class="form-level" style="padding-left: 45px;">
                                                                            <asp:Label ID="lbl_hrmsid" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Small" ForeColor="#EE3535" Width="200px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 190px; width: 50%;">
                                                                            <asp:Label ID="Label33" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Date Of Birth:"></asp:Label>
                                                                        </td>
                                                                        <td class="form-level" style="padding-left: 45px;">
                                                                            <asp:Label ID="lbl_DOB" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Small" ForeColor="#EE3535" Width="200px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                            <tr>
                                                            <td class="form-level" style="padding-left: 190px; width: 50%;">
                                                                <asp:Label ID="Label81" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Select retirement age&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"></asp:Label>
                                                            </td>
                                                            <td class="form-level" style="padding-left: 35px;">
                                                                <asp:RadioButtonList ID="rad_ret_yr" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif"
                                                                                Font-Size="15px" font-weight="bold" ForeColor="#094F7C" Height="16px" 
                                                                    Width="350px" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="60" Text="60 Years"></asp:ListItem>
                                                                    <asp:ListItem Value="62" Text="62 Years"></asp:ListItem>
                                                                    <asp:ListItem Value="65" Text="65 Years"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 190px; width: 50%;">
                                                                            <asp:Label ID="Label44" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="First Name:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"></asp:Label>
                                                                        </td>
                                                                        <td class="form-level" style="padding-left: 45px;">
                                                                            <asp:TextBox ID="txt_fname" runat="server" Font-Names="Verdana" Font-Size="Small" Width="200px" Style="text-transform: uppercase" CssClass="qtr2" onkeypress="return letterspace(event)" AutoComplete="off"></asp:TextBox>
                                                                            <asp:Label ID="Lblfname" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="XX-Small"
                                                                    ForeColor="#094F7C" Text="(First Name + Middle Name)" Width="180px"></asp:Label>
                                                                            </td>
                                                                    </tr>
                                                                    <tr style="height: 10px;">
                                                                        <td class="form-level" style="padding-left: 190px; width: 50%;">
                                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Last Name:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"></asp:Label>
                                                                        </td>
                                                                        <td class="form-level" style="padding-left: 45px;">
                                                                            <asp:TextBox ID="txt_lname" CssClass="qtr2" runat="server" Font-Names="Verdana" Font-Size="Small" Width="200px" Style="text-transform: uppercase" AutoComplete="off" onkeypress="return lettersOnly(event)"></asp:TextBox>
                                                                            <asp:Label ID="Label68" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="XX-Small"
                                                                    ForeColor="#094F7C" Text="(Surname)" Width="150px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 190px; width: 50%; height: 30px;">
                                                                            <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Gender:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"></asp:Label>
                                                                        </td>
                                                                        <td class="form-level" style="padding-left: 45px; height: 30px;">
                                                                            <asp:DropDownList ID="sex_ddlist" runat="server" Font-Names="Verdana" Font-Size="Small" Width="200px"></asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 190px; width: 50%; height: 45px;">
                                                                            <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Marital Status:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"></asp:Label>
                                                                        </td>
                                                                        <td class="form-level" style="padding-left: 45px; height: 50px;">
                                                                            <asp:DropDownList ID="mrt_ddlist" runat="server" Font-Names="Verdana" Font-Size="Small" Width="200px"></asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 190px; width: 246px; height: 45px;">
                                                                            <asp:Label ID="Label38" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Residing District:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"></asp:Label>
                                                                        </td>
                                                                        <td class="form-level" style="padding-left: 45px; height: 50px;">
                                                                            <asp:DropDownList ID="dist_ddlist" runat="server" Font-Names="Verdana" Font-Size="Small" Width="200px"></asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 190px; width: 246px; height: 60px;">
                                                                            <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Permanent Address:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"></asp:Label>
                                                                        </td>
                                                                        <td class="form-level" style="padding-left: 45px; height: 60px;">
                                                                            <asp:TextBox ID="txt_addr" runat="server" Font-Names="Verdana" Font-Size="Small" TextMode="MultiLine" Width="195px" Style="text-transform: uppercase" AutoComplete="off"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 190px; width: 246px; height: 50px;">
                                                                            <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Mobile No.:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"></asp:Label>
                                                                        </td>
                                                                        <td class="form-level" style="padding-left: 45px; height: 50px;">
                                                                            <asp:TextBox ID="txt_mob" CssClass="qtr2" runat="server" Font-Names="Verdana" Font-Size="Small" Width="200px" MaxLength="10" onkeypress="return isNumberKey(event)" AutoComplete="off"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="<br>Please enter correct mobile number</br>" ControlToValidate="txt_mob"
                                                                                ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 190px; width: 246px;">
                                                                            <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Email Address:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"></asp:Label></td>
                                                                        <td class="form-level" style="padding-left: 45px; text-transform:lowercase;">
                                                                            <asp:TextBox ID="txt_email" CssClass="qtr2" runat="server" Font-Names="Verdana" Font-Size="Small" Width="200px" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<br>Please enter valid Email Id</br>" ControlToValidate="txt_email"
                                                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 190px; width: 246px;">
                                                                            <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Residence Phone Number:"></asp:Label>
                                                                        </td>
                                                                        <td class="form-level" style="padding-left: 45px;">
                                                                            <asp:TextBox ID="txt_ph_no" CssClass="qtr2" runat="server" Font-Names="Verdana" Font-Size="Small" Width="200px" onkeypress="return isNumberKey(event)" AutoComplete="off"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 190px; width: 246px;">
                                                                            <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Select Identity Proof:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"></asp:Label>
                                                                        </td>
                                                                        <td class="form-level" style="padding-left: 37px;">
                                                                            <asp:RadioButtonList ID="rdlist_id_prf" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="11px" font-weight="bold" ForeColor="#094F7C" RepeatDirection="Horizontal" Width="180px" OnSelectedIndexChanged="rdlist_id_prf_SelectedIndexChanged" AutoPostBack="True">
                                                                                <asp:ListItem Value="01">Voter Card</asp:ListItem>
                                                                                <asp:ListItem Value="02">Pan Card</asp:ListItem>
                                                                                <%--<asp:ListItem Value="03">Aadhar Card</asp:ListItem>--%>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 190px; width: 246px;">
                                                                            <asp:Label ID="lbl_pan_voter" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Identity Proof No.:"></asp:Label>
                                                                        </td>
                                                                        <td class="form-level" style="padding-left: 45px;">
                                                                            <asp:TextBox ID="txt_id_prf" CssClass="qtr2" runat="server" Font-Names="Verdana" Font-Size="Small" Width="200px" Enabled="False" Style="text-transform: uppercase" onkeypress="return blockSpecialChar(event)" AutoComplete="off"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 190px; width: 246px;">
                                                                            <asp:Label ID="Label51" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Aadhaar No.:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"></asp:Label>
                                                                        </td>
                                                                        <td class="form-level" style="padding-left: 45px;">
                                                                            <asp:TextBox ID="txt_aadhaar_no" CssClass="qtr2" runat="server" Font-Names="Verdana" Font-Size="Small" Width="200px" onkeypress="return isNumberKey(event)" AutoComplete="off"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" style="font-family: Verdana; font-size: small; font-weight: bold; color: #EE3535; text-decoration: underline; padding-left: 190px;">
                                                                            <p align="left">Bank Details </p>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                            <td class="form-level" style="padding-left: 190px; width: 246px;">
                                                                                <asp:Label ID="Label30" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Bank IFS Code (IFSC):&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"></asp:Label>
                                                                            </td>
                                                                            <td class="form-level" style="padding-left: 45px;">
                                                                                <asp:TextBox ID="txt_ifsc_cd" runat="server" Font-Names="Verdana" AutoPostBack="true" 
                                                                                    AutoComplete="Off" onpaste="return false" AutoCompleteType="Disabled" Font-Size="Small" Width="200px" MaxLength="11" Style="text-transform: uppercase" CssClass="qtr2"
                                                                                      PlaceHolder="Please enter bank IFSC" text-Transform="Uppercase"
                                                                                    OnTextChanged="txt_ifsc_cd_TextChanged"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server"
                                                                Enabled="True" FilterType="Numbers,UppercaseLetters,lowercaseLetters" TargetControlID="txt_ifsc_cd"
                                                                ValidChars="">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <AjaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" CompletionListCssClass="completionListElement"
                                            CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="highlightedListItem"
                                            ServiceMethod="AutoCompleteBank" ServicePath="~/wbhs_view_sanction_details.asmx"
                                            MinimumPrefixLength="5" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                            TargetControlID="txt_ifsc_cd" FirstRowSelected="false" runat="server" CompletionListElementID="bank_ifsc">
                                        </AjaxToolkit:AutoCompleteExtender>
                                                                            </td>
                                                                        <div id="bank_ifsc"></div>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 190px; width: 246px; height: 45px;">
                                                                            <asp:Label ID="Label28" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Bank Name:"></asp:Label>
                                                                        </td>
                                                                        <td class="form-level" style="padding-left: 45px; height: 45px;">
                                                                            <asp:Label ID="Lbl_bank_name" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px"></asp:Label>
                                                                            <%--<asp:DropDownList ID="bnk_nm_ddlist" runat="server" Font-Names="Verdana" Font-Size="Small" Width="200px" Height="20px" OnSelectedIndexChanged="bnk_nm_ddlist_SelectedIndexChanged"></asp:DropDownList>--%>
                                                                        </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="form-level" style="padding-left: 190px; width: 246px; height: 45px;">
                                                                                <asp:Label ID="Label29" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Branch Name:"></asp:Label>
                                                                            </td>
                                                                            <td class="form-level" style="padding-left: 45px;">
                                                                                <asp:Label ID="Lbl_branch_name" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px"></asp:Label>
                                                                                <%--<asp:DropDownList ID="brnch_nm_ddlist" runat="server" Font-Names="Verdana" Font-Size="Small" Width="200px" Height="20px" OnSelectedIndexChanged="brnch_nm_ddlist_SelectedIndexChanged"></asp:DropDownList>--%>
                                                                                <%--<asp:TextBox ID="txt_brnc_nm" runat="server" CssClass="qtr2" Font-Names="Verdana" Font-Size="Small" Width="200px" Style="text-transform: uppercase" onkeypress="return blockSpecialCharExceptSpace(event)" AutoComplete="off"></asp:TextBox>--%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="form-level" style="padding-left: 190px; width: 246px; height: 45px;">
                                                                                <asp:Label ID="Label48" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="MICR Code:"></asp:Label>
                                                                            </td>
                                                                            <td class="form-level" style="padding-left: 45px;">
                                                                                <asp:Label ID="Lbl_MICR" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px"></asp:Label>
                                                                                <%--<asp:DropDownList ID="brnch_nm_ddlist" runat="server" Font-Names="Verdana" Font-Size="Small" Width="200px" Height="20px" OnSelectedIndexChanged="brnch_nm_ddlist_SelectedIndexChanged"></asp:DropDownList>--%>
                                                                                <%--<asp:TextBox ID="txt_brnc_nm" runat="server" CssClass="qtr2" Font-Names="Verdana" Font-Size="Small" Width="200px" Style="text-transform: uppercase" onkeypress="return blockSpecialCharExceptSpace(event)" AutoComplete="off"></asp:TextBox>--%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="form-level" style="padding-left: 190px; width: 246px;">
                                                                                <asp:Label ID="Label31" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" 
                                                                                    ForeColor="#094F7C" Height="16px" Text="Account No.:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;" placeholder="Enter your account no."></asp:Label>
                                                                            </td>
                                                                            <td class="form-level" style="padding-left: 45px;">
                                                                                <asp:TextBox ID="txt_ac_no" runat="server" CssClass="qtr2" Font-Names="Verdana" Font-Size="Small" Width="200px" AutoComplete="off" 
                                                                                    MaxLength="17" OnTextChanged="txt_ac_no_TextChanged"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txt_ac_no" runat="server" Enabled="True" FilterType="Numbers"
                                                                                    TargetControlID="txt_ac_no"></cc1:FilteredTextBoxExtender>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="form-level" style="padding-left: 190px; width: 246px;">
                                                                                <asp:Label ID="Label35" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" 
                                                                                    ForeColor="#094F7C" Height="16px" Text="Confirm account No.:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;" placeholder="Confirm your account no."></asp:Label>
                                                                            </td>
                                                                            <td class="form-level" style="padding-left: 45px;">
                                                                                <asp:TextBox ID="txt_cnfm_ac_no" runat="server" CssClass="qtr2" Font-Names="Verdana" Font-Size="Small" Width="200px" AutoComplete="off" 
                                                                                    MaxLength="17" OnTextChanged="txt_cnfm_ac_no_TextChanged"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" FilterType="Numbers"
                                                                                    TargetControlID="txt_cnfm_ac_no"></cc1:FilteredTextBoxExtender>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="form-level" style="padding-left: 190px; width: 246px;">&nbsp;</td>
                                                                            <td class="form-level" style="padding-left: 45px;">
                                                                                <asp:Label ID="Label14" runat="server" Font-Size="Small" font-weight="bold" ForeColor="red" Text="Account No should be 9-17 digits."></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 246px">&nbsp;</td>
                                                                            <td>&nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" style="align-content: center;" colspan="2">
                                                                                <asp:ImageButton ID="btn_next_per" runat="server" Height="25px" ImageUrl="~/Higher_Education/images1/button_save-continue.png" Width="135px" OnClick="btn_next_per_Click" /><asp:ImageButton ID="final_nxt_per" runat="server" Height="25px" ImageUrl="~/Higher_Education/images1/button_next.png" Width="60px" Visible="False" OnClick="final_nxt_per_Click" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 246px">&nbsp;</td>
                                                                            <td>&nbsp;</td>
                                                                        </tr>
                                                                        <tr bgcolor="">
                                                                            <td style="font-family: Verdana; font-size: x-small; font-weight: bold; color: #EE3535; text-decoration: underline"
                                                                                colspan="2">
                                                                                <p align="center">THE INPUTS WITH &#39;*&#39; MARKS ARE MANDATORY DATA. YOU HAVE TO ENTER THOSE DATA FOR YOUR ONLINE REGISTRATION TO BE SUCCESSFUL AND THESE ARE VERY ESSENTIAL FOR YOR ONLINE REGISTRATION AND ALSO FOR AVAILING THE REIMBURSEMENT BENEFITS. </p>
                                                                            </td>
                                                                        </tr>
                                                                </table>
                                                            </div>
                                                        </ContentTemplate>
                                                    </AjaxToolkit:TabPanel>
                                                    <AjaxToolkit:TabPanel HeaderText="OFFICE LOCATION DETAILS" runat="server" ID="ofc_info_tab"
                                                        Width="900px" Visible="False" Enabled="False">
                                                        <HeaderTemplate>OFFICE LOCATION</HeaderTemplate>
                                                        <ContentTemplate>
                                                            <div>
                                                                <table width="900px" style="background-color: white;">
                                                                    <tr>
                                                                        <td class="qtr1" style="padding-left: 130px; width: 50%;">&nbsp;&nbsp;&nbsp; </td>
                                                                        <td class="qtr2">&nbsp;&nbsp;&nbsp; </td>
                                                                    </tr>
                                                                    <tr class="form-level" style="padding-left: 20px;">
                                                                        <td class="form-level" style="padding-left: 150px; width: 50%;">
                                                                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Application Id Number:" style="margin-bottom: 0px"></asp:Label></td>
                                                                        <td class="form-level" style="padding-left: 60px;">
                                                                            <asp:Label ID="lbl_appid_ofc" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Small" ForeColor="#EE3535" Width="200px"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 150px; width: 246px;">
                                                                            <asp:Label ID="Label45" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="HRMS/Unique Id:"></asp:Label></td>
                                                                        <td class="form-level" style="padding-left: 60px;">
                                                                            <asp:Label ID="lbl_hrms_ofc" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Small" ForeColor="#EE3535" Width="200px"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 150px; width: 246px;">
                                                                            <asp:Label ID="Label46" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Date Of Birth:"></asp:Label></td>
                                                                        <td class="form-level" style="padding-left: 60px;">
                                                                            <asp:Label ID="Lbl_DOB_ofc" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Small" ForeColor="#EE3535" Width="200px"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 150px; width: 246px;"></td>
                                                                        <td class="form-level" style="padding-left: 45px;">&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 150px;">
                                                                            <asp:Label ID="Label75" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="College District:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"></asp:Label>
                                                                        </td>
                                                                        <td class="form-level" style="padding-left: 60px; height: 30px;">
                                                                            <asp:DropDownList ID="clg_dist_ddlist" runat="server" Font-Names="Verdana" Font-Size="Small" Width="200px" OnSelectedIndexChanged="clg_dist_ddlist_SelectedIndexChanged" AutoPostBack="True" text-transform="uppercase"></asp:DropDownList></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 150px;">
                                                                            <asp:Label ID="Label76" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Name of College:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"></asp:Label>
                                                                        </td>
                                                                        <td class="form-level" style="padding-left: 60px; height: 30px;">
                                                                            <asp:DropDownList ID="clg_list_ddlist" runat="server" Font-Names="Verdana" 
                                                                                Font-Size="Small" Width="200px" text-transform="uppercase" AutoPostBack="True" 
                                                                                onselectedindexchanged="clg_list_ddlist_SelectedIndexChanged" ></asp:DropDownList></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 150px;">
                                                                            <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Sub-Division:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"></asp:Label></td>
                                                                        <td class="form-level" style="padding-left: 60px; height: 30px;">
                                                                            <asp:DropDownList ID="subdiv_ddlist" runat="server" Font-Names="Verdana" Font-Size="Small" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="subdiv_ddlist_SelectedIndexChanged" text-transform="uppercase"></asp:DropDownList></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 150px;">
                                                                            <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Block:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"></asp:Label></td>
                                                                        <td class="form-level" style="padding-left: 60px; height: 40px;">
                                                                            <asp:DropDownList ID="block_ddlist" runat="server" Font-Names="Verdana" Font-Size="Small" Width="200px" text-transform="uppercase"></asp:DropDownList></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 150px; ">
                                                                            <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Full Address(College):&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"></asp:Label></td>
                                                                        <td class="form-level" style="padding-left: 60px; ">
                                                                            <asp:TextBox ID="txt_clg_addr" runat="server" TextMode="MultiLine" Width="195px" Style="text-transform: uppercase" AutoComplete="off"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 150px; width: ">
                                                                            <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Date of Entry Into College Service:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"></asp:Label></td>
                                                                        <td class="form-level" style="padding-left: 60px;">
                                                                            <asp:TextBox ID="txt_serv_dt" runat="server" Font-Names="Verdana" AutoPostBack="True" Height="10px" Font-Size="Small" Width="200px" AutoComplete="off"></asp:TextBox>
                                                                            <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" Enabled="True"
                                                                                FilterType="Custom, Numbers" TargetControlID="txt_serv_dt" ValidChars="/-:">
                                                                            </AjaxToolkit:FilteredTextBoxExtender>
                                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate"
                                                                                TargetControlID="txt_serv_dt" Enabled="True">
                                                                            </cc1:CalendarExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 150px;">
                                                                            <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Designation:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"></asp:Label></td>
                                                                        <td class="form-level" style="padding-left: 60px; height: 30px;">
                                                                            <asp:DropDownList ID="desig_ddlist" runat="server" Font-Names="Verdana" Font-Size="Small" Width="200px" text-transform="uppercase"></asp:DropDownList></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 150px;">
                                                                            <asp:Label ID="Label54" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif"
                                                                                Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Revision of Pay and Allowance:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"></asp:Label>
                                                                        </td>
                                                                        <td class="form-level" style="padding-left: 52px; height: 30px;">
                                                                            <asp:RadioButtonList ID="rbl_ropa" runat="server" Font-Bold="True"
                                                                                Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold"
                                                                                ForeColor="#094F7C" RepeatDirection="Horizontal" Width="250px" AutoPostBack="True"
                                                                                OnSelectedIndexChanged="rbl_ropa_SelectedIndexChanged">
                                                                                <asp:ListItem Value="01" Text="ROPA 2009"></asp:ListItem>
                                                                                <asp:ListItem Value="02" Text="ROPA 2019"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td runat="server" colspan="2">
                                                                            <table id="tab_ropa09" runat="server" width="100%" visible="false">
                                                                                <tr>
                                                                        <td class="form-level" style="padding-left: 150px; height: 30px; width:51%">
                                                                            <asp:Label ID="Label23" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Pay Band:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"></asp:Label></td>
                                                                        <td class="form-level" style="padding-left: 60px; height: 30px;">
                                                                            <asp:DropDownList ID="pay_band_ddlist" runat="server" Font-Names="Verdana" Font-Size="Small" Width="200px" OnSelectedIndexChanged="pay_band_ddlist_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 150px;">
                                                                            <asp:Label ID="Label24" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Grade Pay:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"></asp:Label></td>
                                                                        <td class="form-level" style="padding-left: 60px; height: 30px;">
                                                                            <asp:DropDownList ID="grade_ddlist" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="Small" Width="200px" OnSelectedIndexChanged="grade_ddlist_SelectedIndexChanged"></asp:DropDownList></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 150px;">
                                                                            <asp:Label ID="Label25" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Band Pay:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"></asp:Label></td>
                                                                        <td class="form-level" style="padding-left: 60px; height: 30px;">
                                                                            <asp:TextBox ID="txt_band" runat="server" Font-Names="Verdana" AutoPostBack="True" Height="10px" Font-Size="8pt" Width="200px" AutoComplete="off" OnTextChanged="txt_band_TextChanged" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 150px;">
                                                                            <asp:Label ID="Label27" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Basic Pay:"></asp:Label></td>
                                                                        <td class="form-level" style="padding-left: 60px;">
                                                                            <asp:Label ID="lbl_basic" runat="server" Font-Names="Verdana" Text="Grade Pay + Band Pay" ForeColor="Red"></asp:Label></td>
                                                                    </tr>
                                                                            </table>
                                                                            <table id="tab_ropa19" runat="server" width="100%" visible="false">
                                                                                <tr>
                                                                        <td class="form-level" style="padding-left: 150px; height: 30px; width:51%">
                                                                            <asp:Label ID="Label55" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif"
                                                                                Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Pay Level:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"></asp:Label>
                                                                        </td>
                                                                        <td class="form-level" style="padding-left: 60px; height: 30px;">
                                                                            <asp:DropDownList ID="ddl_pay_level" runat="server" Font-Names="Verdana" Font-Size="Small"
                                                                                Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddl_pay_level_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                        <tr>
                                                                        <td class="form-level" style="padding-left: 150px; height: 30px; width:51%">
                                                                            <asp:Label ID="Label56" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif"
                                                                                Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Basic Salary:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"></asp:Label>
                                                                        </td>
                                                                        <td class="form-level" style="padding-left: 60px; height: 30px;">
                                                                            <asp:DropDownList ID="ddl_basic_sal" runat="server"  Font-Names="Verdana"
                                                                                Font-Size="Small" Width="200px" Enabled="False">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="height: 19px;">&nbsp;&nbsp;</td>
                                                                        <td style="height: 19px">&nbsp;&nbsp;</td>
                                                                        <td class="small" style="height: 19px">&nbsp;&nbsp;</td>
                                                                        <tr>
                                                                            <td align="center" colspan="2" style="padding-left: 30px; height: 19px; align-content: center;">
                                                                                <asp:ImageButton ID="btn_prv_ofc" runat="server" Height="25px" ImageUrl="~/Higher_Education/images1/button_previous.png" OnClick="btn_prv_ofc_Click" Width="90px" />
                                                                                <asp:ImageButton ID="btn_nxt_ofc" runat="server" Height="25px" ImageUrl="~/Higher_Education/images1/button_save-continue.png" OnClick="btn_nxt_ofc_Click" Width="130px" />
                                                                                <asp:ImageButton ID="final_ofc_nxt" runat="server" Height="25px" ImageUrl="~/Higher_Education/images1/button_next.png" OnClick="final_ofc_nxt_Click" Visible="False" Width="60px" />
                                                                            </td>
                                                                            <td class="small" style="height: 19px;">&nbsp;&nbsp;</td>
                                                                        </tr>
                                                                        <tr bgcolor="">
                                                                            <td colspan="3" style="font-family: Verdana; font-size: x-small; font-weight: bold; color: #EE3535; text-decoration: underline;">
                                                                                <p align="center">
                                                                                    THE INPUTS WITH '*' MARKS ARE MANDATORY DATA. YOU HAVE TO ENTER THOSE DATA FOR YOUR ONLINE REGISTRATION TO BE SUCCESSFUL AND THESE ARE VERY ESSENTIAL FOR YOR ONLINE REGISTRATION AND ALSO FOR AVAILING THE REIMBURSEMENT BENEFITS.
                                                                                </p>
                                                                            </td>
                                                                        </tr>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </ContentTemplate>
                                                    </AjaxToolkit:TabPanel>
                                                    <AjaxToolkit:TabPanel HeaderText="FAMILY DETAILS" runat="server" ID="fml_info_tab"
                                                        Width="900px" Visible="False" Enabled="False">
                                                        <HeaderTemplate>FAMILY DETAILS</HeaderTemplate>
                                                        <ContentTemplate>
                                                            <div>
                                                                <table width="900px" style="background-color: white;">
                                                                    <tr>
                                                                        <td class="qtr1" style="padding-left: 45px; width:50%">&nbsp;&nbsp;&nbsp; </td>
                                                                        <td class="qtr2">&nbsp;&nbsp;&nbsp; </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 100px; width: 50%;">
                                                                            <asp:Label ID="Label26" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Application Id Number:"></asp:Label></td>
                                                                        <td class="form-level" style="padding-left: 50px;">
                                                                            <asp:Label ID="lbl_appid_fml" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Small" ForeColor="#EE3535" Width="200px"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 100px; width: 50%;">
                                                                            <asp:Label ID="Label40" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="HRMS/Unique Id:"></asp:Label></td>
                                                                        <td class="form-level" style="padding-left: 50px;">
                                                                            <asp:Label ID="lbl_hrms_fml" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Small" ForeColor="#EE3535" Width="200px"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 100px; width: 50%;">
                                                                            <asp:Label ID="Label47" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Date Of Birth:"></asp:Label></td>
                                                                        <td class="form-level" style="padding-left: 50px;">
                                                                            <asp:Label ID="Lbl_DOB_fml" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Small" ForeColor="#EE3535" Width="200px"></asp:Label></td>
                                                                    </tr>
                                                                    <tr bgcolor="">
                                                                        <td class="form-level" style="padding-left: 100px; width: 50%;">
                                                                            <asp:Label ID="Label34" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Text="Applicant Name:" Width="140px"></asp:Label></td>
                                                                        <td class="form-level" style="padding-left: 50px;" colspan="2">
                                                                            <asp:Label ID="lbl_applicant_nm" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Small" ForeColor="#EE3535" font-weight="bold" Width="200px"></asp:Label></td>
                                                                    </tr>
                                                                    <tr bgcolor="">
                                                                        <td class="form-level" style="padding-left: 100px; width: 50%; height: 39px;">
                                                                            <asp:Label ID="Label41" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Text="Name of Beneficiary:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;" Width="250px"></asp:Label></td>
                                                                        <td class="form-level" style="padding-left: 50px; height: 10px;" colspan="2">
                                                                            <asp:TextBox ID="txt_Ben_Name" CssClass="qtr2" runat="server" Font-Names="Verdana" Font-Size="Small" Width="250px" Style="text-transform: uppercase" onkeypress="return letterspace(event)" AutoComplete="off"></asp:TextBox><AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender25" runat="server" Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txt_Ben_Name" ValidChars=" ()"></AjaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 100px; width: 50%;">
                                                                            <asp:Label ID="Label39" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Text="Date of Birth of Beneficiary:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;" Width="250px"></asp:Label></td>
                                                                        <td class="form-level" style="padding-left: 50px; width: 436px;">
                                                                            <asp:TextBox ID="txt_ben_DOB" runat="server" AutoPostBack="True" Height="10px" Font-Names="Verdana" Font-Size="Small" AutoComplete="off" ForeColor="Black" Width="150px" OnTextChanged="txt_ben_DOB_TextChanged"></asp:TextBox>
                                                                            <AjaxToolkit:FilteredTextBoxExtender ID="dob_filter" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txt_ben_DOB" ValidChars="-/"></AjaxToolkit:FilteredTextBoxExtender>
                                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate"
                                                                                TargetControlID="txt_ben_DOB" Enabled="True">
                                                                            </cc1:CalendarExtender>
                                                                            <asp:Label ID="LblAge" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Text="Age:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;" Width="30px" Height="16px"></asp:Label><asp:TextBox ID="txt_age" runat="server" Font-Names="Verdana" Height="10px" Font-Size="Small" ForeColor="Black" ReadOnly="True" Width="38px" AutoComplete="off"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding-left: 100px; width: 50%;">
                                                                            <asp:Label ID="Label42" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Text="Relation with Applicant:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;" Width="250px"></asp:Label></td>
                                                                        <td style="padding-left: 50px; width: 50%; height: 30px;">
                                                                            <asp:DropDownList ID="relation_ddlist" runat="server" Font-Names="Verdana" Font-Size="Small" Width="200px"></asp:DropDownList></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding-left: 100px; width: 50%;">
                                                                            <asp:Label ID="Label79" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Text="Beneficiary Category &lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;" Width="250px"></asp:Label>
                                                                        </td>
                                                                        <td style="padding-left: 50px; width: 50%; height: 30px;">
                                                                            <asp:DropDownList ID="ddlBenCat" runat="server" Font-Names="Verdana" Font-Size="Small" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlBenCat_SelectedIndexChanged">
                                                                                <asp:ListItem Value="General">General Beneficiary</asp:ListItem>
                                                                                <asp:ListItem Value="Critical">Beneficiary under Critical illness</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="trBenDisease" runat="server" visible="False">
                                                                        <td style="padding-left: 100px; width: 50%;" runat="server">
                                                                            <asp:Label ID="Label80" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Text="Select name of disease/ illness&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;" Width="250px"></asp:Label>
                                                                        </td>
                                                                        <td style="padding-left: 43px; width: 50%; height: 30px;" runat="server">
                                                                            <asp:CheckBoxList ID="cblBenDisease" runat="server" Width="80%" Font-Bold="False" Font-Names="Cambria" Font-Size="Medium"></asp:CheckBoxList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 100px; width: 50%;">
                                                                            <asp:Label ID="Label43" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Text="Monthly Income of Beneficiary:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;" Width="250px"></asp:Label></td>
                                                                        <td style="padding-left: 50px; width: 50%;">
                                                                            <asp:TextBox ID="txt_ben_income" CssClass="qtr2" runat="server" Font-Names="Verdana" Font-Size="Small" Width="200px" AutoComplete="off" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding-left: 100px; width: 50%;">
                                                                            <asp:Label ID="Label74" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Blood Group:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;" Width="138px"></asp:Label></td>
                                                                        <td align="left" colspan="2" style="padding-left: 50px; width: 50%; height: 30px;">
                                                                            <asp:DropDownList ID="bld_grp_ddlist" runat="server" Font-Names="Verdana" Font-Size="Small" Width="200px"></asp:DropDownList></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style3" style="padding-left: 100px; ">
                                                                            <asp:Label ID="Label52" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Aadhaar Card No:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;* (Not mandatory for children below 5 yrs)&lt;/strong&gt;&lt;/font&gt;" Width="330px"></asp:Label></td>
                                                                        <td class="auto-style4" style="padding-left: 50px; ">
                                                                            <asp:TextBox ID="txt_ben_aadhaar_no" CssClass="qtr2" runat="server" Font-Names="Verdana" Font-Size="Small" Width="200px" onkeypress="return isNumberKey(event)" AutoComplete="off"></asp:TextBox></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td class="qtr1" style="padding-left: 100px; width: 50%;">
                                                                            <asp:Label ID="Label49" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Mobile No. of Beneficiary:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;* (Provide parent's mobile number in case of infant)&lt;/strong&gt;&lt;/font&gt;" Width="300px"></asp:Label>
                                                                        <td class="qtr2" style="padding-left: 50px; width: 50%;">
                                                                            <asp:TextBox ID="txt_ben_mob_no" CssClass="qtr2" runat="server" Font-Names="Verdana" Font-Size="Small" Width="200px" onkeypress="return isNumberKey(event)" AutoComplete="off" MaxLength="10"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="qtr1" style="padding-left: 100px; width: 50%;">
                                                                            <asp:Label ID="Label50" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Height="16px" Text="Email Id of Beneficiary:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;* (Provide parent's email ID in case of infant)&lt;/strong&gt;&lt;/font&gt;" Width="300px"></asp:Label>
                                                                        <td class="qtr2" style="padding-left: 50px; width: 50%;">
                                                                            <asp:TextBox ID="txt_ben_email" CssClass="qtr2" runat="server" Font-Names="Verdana" Font-Size="Small" Width="200px" AutoComplete="off"></asp:TextBox>
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="<br>Please enter valid Email Id</br>" ControlToValidate="txt_ben_email"
                                                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td style="padding-left: 100px; width: 50%;">
                                                                            <asp:Label ID="Label72" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Text="Upload Photo:
                                                                                        &lt;font color=&quot;#EE3535&quot;size=&quot;2px&quot;&gt;(upload only .JPEG/.JPG file only and size of photo should be within 10KB to 50KB*)&lt;/font&gt;"
                                                                                Width="250px"></asp:Label></td>
                                                                        <td style="padding-left: 50px; width: 50%;">
                                                                            <asp:FileUpload ID="fup_photo" runat="server" CssClass="fup_photo" Font-Bold="True" ForeColor="#175581" onchange="readURL(this)" Width="256px" /></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 50%; padding-left: 100px">&nbsp;</td>
                                                                        <td align="left" colspan="2" style="padding-left: 50px;">
                                                                            <img id="img_photo" src="../images1/NOPHOTO.jpg" alt="No image" height="100px" /></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding-left: 100px; width: 50%;">
                                                                            <asp:Label ID="Label73" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small" font-weight="bold" ForeColor="#094F7C" Text="Upload Signature:
                                                                                &lt;font color=&quot;#EE3535&quot;size=&quot;2px&quot;&gt;(upload only .JPEG/.JPG file only and size of photo should be within 10KB to 50KB*)&lt;/font&gt;"
                                                                                Width="250px"></asp:Label></td>
                                                                        <td style="padding-left: 50px; width: 50%;">
                                                                            <asp:FileUpload ID="fup_sign" runat="server" CssClass="fup_photo1" Font-Bold="True" ForeColor="#175581" onchange="readURL_sign(this)" Width="256px" /></td>
                                                                        <td align="left"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 50%;"></td>
                                                                        <td align="left" colspan="2" style="padding-left: 50px">
                                                                            <img id="img_sign" src="../images1/NOSIGN.jpg" alt="No image" height="70px" /></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" colspan="3" style="padding-left: 20px;">&nbsp;&nbsp;</td>
                                                                        <tr>
                                                                            <td align="center" class="auto-style2" colspan="3">
                                                                                <asp:ImageButton ID="btn_prv_fml" runat="server" Height="25px" ImageUrl="~/Higher_Education/images1/button_previous.png" OnClick="btn_prv_fml_Click" Width="70px" />
                                                                                <asp:ImageButton ID="btn_save_fml" runat="server" Height="25px" ImageUrl="~/Higher_Education/images1/button_save.png" OnClick="btn_save_fml_Click" Width="60px" />
                                                                                <asp:ImageButton ID="btn_nxt_fml" runat="server" Height="25px" ImageUrl="~/Higher_Education/images1/button_next.png" OnClick="btn_nxt_fml_Click" Width="60px" />
                                                                                <asp:ImageButton ID="final_fml_nxt" runat="server" Height="25px" ImageUrl="~/Higher_Education/images1/button_next.png" OnClick="final_fml_nxt_Click" Visible="False" Width="60px" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" colspan="3" style="padding-left: 20px; ">&nbsp;&nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" colspan="3">
                                                                                <asp:GridView ID="family_grid" runat="server" AutoGenerateColumns="False" 
                                                                                    CellPadding="4" Font-Names="Verdana" Font-Size="Small" ForeColor="#094F7C" 
                                                                                    Width="360px">
                                                                                    <AlternatingRowStyle BackColor="#EBEBFF" ForeColor="#383A3B" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="APPLICATION ID" Visible="False">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label10" runat="server" Font-Bold="true" Text='<%# Eval("APP_ID") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField Visible="False">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label19" runat="server" Font-Bold="true" Text='<%# Eval("HRMS_ID") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="ID NO.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label11" runat="server" Font-Bold="true" Text='<%# Eval("idno") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="NAME">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label12" runat="server" Font-Bold="true" Text='<%# Eval("bName") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="DATE OF BIRTH">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label17" runat="server" Font-Bold="true" Text='<%# Eval("dob") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="AGE">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label13" runat="server" Font-Bold="true" Text='<%# Eval("age") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="RELATION">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label15" runat="server" Font-Bold="true" Text='<%# Eval("RELATIONSHIP") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="MONTHLY INCOME">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label16" runat="server" Font-Bold="true" Text='<%# Eval("monthIncome") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField Visible="False">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label18" runat="server" Font-Bold="true" Text='<%# Eval("IS_EXISTS") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="PHOTO">
                                                                                            <ItemTemplate>
                                                                                                <%--<asp:Image ID="photo" runat="server" Height="50px" ImageUrl='<%# "data:image/jpg;base64," + Convert.ToBase64String((byte[])Eval("photo")) %>' Width="50px" />--%>
                                                                                                <%--<asp:Image ID="photo" runat="server" Height="50px" ImageUrl='<%# Eval("photo") %>' Width="50px" />--%>
                                                                                                <asp:Image ID="photo" runat="server" Height="50px" ImageUrl='<%# "../../Handler_CLGPhoto.ashx?id="+ Eval("SLR_NO") %>' Width="50px" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="SIGN">
                                                                                            <ItemTemplate>
                                                                                                <%--<asp:Image ID="signature" runat="server" Height="50px" ImageUrl='<%# "data:image/jpg;base64," + Convert.ToBase64String((byte[])Eval("sign")) %>' Width="50px" />--%>
                                                                                                <%--<asp:Image ID="signature" runat="server" Height="50px" ImageUrl='<%# Eval("sign") %>' Width="50px" />--%>
                                                                                                <asp:Image ID="signature" runat="server" Height="50px" ImageUrl='<%# "../../Handler_CLGSign.ashx?id="+ Eval("SLR_NO") %>' Width="50px" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="BLOOD GROUP">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_bld" runat="server" Font-Bold="true" Text='<%# Eval("blood_group") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <EditRowStyle BackColor="#999999" />
                                                                                    <FooterStyle BackColor="#70C3F9" Font-Bold="True" ForeColor="White" />
                                                                                    <HeaderStyle BackColor="#70C3F9" Font-Bold="True" ForeColor="#094F7C" />
                                                                                    <PagerStyle BackColor="#70C3F9" ForeColor="White" HorizontalAlign="Center" />
                                                                                    <RowStyle BackColor="White" ForeColor="#383A3B" />
                                                                                    <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="#4FACEA" />
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr bgcolor="">
                                                                            <td colspan="3" style="font-family: Verdana; font-size: x-small; font-weight: bold; color: #EE3535; text-decoration: underline">
                                                                                <p align="center">
                                                                                    THE INPUTS WITH '*' MARKS ARE MANDATORY DATA. YOU HAVE TO ENTER THOSE DATA FOR YOUR ONLINE REGISTRATION TO BE SUCCESSFUL AND THESE ARE VERY ESSENTIAL FOR YOR ONLINE REGISTRATION AND ALSO FOR AVAILING THE REIMBURSEMENT BENEFITS.
                                                                                </p>
                                                                            </td>
                                                                        </tr>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </ContentTemplate>
                                                    </AjaxToolkit:TabPanel>
                                                    <AjaxToolkit:TabPanel HeaderText="CCA/HEAD OF OFFICE" runat="server" ID="hoi_info_tab"
                                                        Width="900px" Visible="False">
                                                        <HeaderTemplate>HEAD OF INSTITUTION</HeaderTemplate>
                                                        <ContentTemplate>
                                                            <div>
                                                                <table width="900px" style="background-color: white;">
                                                                    <tr bgcolor="">
                                                                        <td colspan="2" style="padding-top: 10px;"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 130px; vertical-align: middle; height:40px; width: 50%;">
                                                                            <asp:Label ID="Label57" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" font-weight="bold" Font-Size="Small"
                                                                                ForeColor="#094F7C" Text="Designation of Head of Institution:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"></asp:Label></td>
                                                                        <td style="padding-left:40px">
                                                                            <asp:DropDownList ID="hoi_ddlist" runat="server" AutoPostBack="True" Font-Names="Verdana"
                                                                                Font-Size="Small" Width="200px">
                                                                            </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="hoi_ddlist"
                                                                                ErrorMessage="Select Head of the Institution" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small"
                                                                                ValidationGroup="pp"></asp:RequiredFieldValidator></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 130px; vertical-align: middle; height:40px; width: 50%;">
                                                                            <asp:Label ID="Label67" runat="server" Text="District Where DDO is Located:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"
                                                                                Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" font-weight="bold" Font-Size="Small"
                                                                                ForeColor="#094F7C"></asp:Label></td>
                                                                        <td style="padding-left:40px">
                                                                            <asp:DropDownList ID="ddo_dist_ddlist" runat="server" Font-Size="Small" Width="200px" Font-Names="Verdana"
                                                                                AutoPostBack="True" OnSelectedIndexChanged="ddo_dist_ddlist_SelectedIndexChanged1">
                                                                            </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddo_dist_ddlist"
                                                                                ErrorMessage="Select DDO District" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small"
                                                                                ValidationGroup="pp"></asp:RequiredFieldValidator></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 130px; vertical-align: middle; height:40px; width: 50%;">
                                                                            <asp:Label ID="Label32" runat="server" Text="Select Treasury:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"
                                                                                Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" font-weight="bold" Font-Size="Small"
                                                                                ForeColor="#094F7C"></asp:Label></td>
                                                                        <td style="padding-left:40px">
                                                                            <asp:DropDownList ID="trsy_ddlist" runat="server" Font-Size="Small" Width="200px" Font-Names="Verdana"
                                                                                AutoPostBack="True" OnSelectedIndexChanged="trsy_ddlist_SelectedIndexChanged1">
                                                                            </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="trsy_ddlist"
                                                                                ErrorMessage="Select Treasury" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small"
                                                                                ValidationGroup="pp"></asp:RequiredFieldValidator></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 130px; vertical-align: middle; height:40px; width: 50%;">
                                                                            <asp:Label ID="Label36" runat="server" Text="Drawing &amp; Disbursing Officer(DDO) Code:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"
                                                                                Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" font-weight="bold" Font-Size="Small"
                                                                                ForeColor="#094F7C"></asp:Label></td>
                                                                        <td style="padding-left:40px">
                                                                            <asp:DropDownList ID="ddo_cd_ddlist" runat="server" Font-Size="Small" Width="200px" Font-Names="Verdana"
                                                                                AutoPostBack="True" OnSelectedIndexChanged="ddo_cd_ddlist_SelectedIndexChanged">
                                                                            </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddo_cd_ddlist"
                                                                                ErrorMessage="Select DDO Code" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" Font-Size="Small"
                                                                                ValidationGroup="pp"></asp:RequiredFieldValidator></td>
                                                                    </tr>
                                                                      <tr id="tr_ddo_desig1" runat="server" visible="False">
                                                                        <td class="form-level" style="padding-left: 130px; vertical-align: middle; height:40px; width: 50%;" runat="server">
                                                                            <asp:Label ID="Label65" runat="server" Text="Designation of DDO:&lt;font color=&quot;red&quot;&gt;&lt;strong&gt;*&lt;/strong&gt;&lt;/font&gt;"
                                                                                Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" font-weight="bold" Font-Size="Small"
                                                                                ForeColor="#094F7C"></asp:Label></td>
                                                                        <td style="padding-left:40px">
                                                                            <asp:Label ID="lbl_ddo_desig" runat="server" Font-Names="Verdana"></asp:Label> 
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="form-level" style="padding-left: 130px; width: 50%;">&nbsp;&nbsp;</td>
                                                                        <td>&nbsp;&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" colspan="2" style="padding-left: 25px;">
                                                                            <asp:CheckBox ID="CheckBox1" runat="server" Font-Bold="True" Font-Names="Helvetica Neue,Arial,Sans-Serif" font-weight="bold" Font-Size="Small"
                                                                                ForeColor="#EE3535" Text="I accept the declaration written below"
                                                                                Width="100%" /></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" style="font-weight: bold; font-size: smaller; padding-left: 30px; text-decoration: underline; font-family: Verdana; width: 500px; color: #EE3535;">Declaration: </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" colspan="2" style="padding-left: 30px; padding-right: 30px; font-family: Verdana; font-size: smaller; color: #EE3535; font-weight: bold;">
                                                                            <p>I, hereby declare that the statements made in the Application are true to the best of my knowledge and belief. </p>
                                                                            <p>I, do hereby declare that upon Enrolment under the Scheme I shall Forego/Continue to forego my regular monthly Medical Allowance/Medical Relief from my salary. </p>
                                                                            <p>I, further declare that I shall abide by the provisions of the Scheme as may be in force from time to time. </p>
                                                                            <p>I, further declare that I have not Opted out from the Scheme in any previous occasion. </p>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" colspan="2" style="padding-left: 30px;"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" colspan="2" style="padding-left: 0px;">
                                                                            <asp:ImageButton ID="btn_prv_hoi" runat="server" Height="25px" ImageUrl="~/Higher_Education/images1/button_previous.png" Width="70px" OnClick="btn_prv_hoi_Click1" />
                                                                            <asp:ImageButton ID="btnSave_final" runat="server" Height="25px" ImageUrl="~/Higher_Education/images1/button_save.png" Width="60px" ValidationGroup="pp" OnClick="btnSave_final_Click" />
                                                                            <asp:ImageButton ID="btn_rpt" runat="server" Height="25px" ImageUrl="~/Higher_Education/images1/button_report.png" Width="60px" Visible="False" OnClick="btn_rpt_Click" /></td>
                                                                    </tr>
                                                                    <tr bgcolor="">
                                                                        <td style="font-family: Verdana; font-size: x-small; font-weight: bold; color: #EE3535; text-decoration: underline"
                                                                            colspan="2">
                                                                            <p align="center">THE INPUTS WITH &#39;*&#39; MARKS ARE MANDATORY DATA. YOU HAVE TO ENTER THOSE DATA FOR YOUR ONLINE REGISTRATION TO BE SUCCESSFUL AND THESE ARE VERY ESSENTIAL FOR YOR ONLINE REGISTRATION AND ALSO FOR AVAILING THE REIMBURSEMENT BENEFITS. </p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </ContentTemplate>
                                                    </AjaxToolkit:TabPanel>
                                                </AjaxToolkit:TabContainer>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="HiddenField_SLR_NO" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%;" align="center">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="width: 100%;">&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="TabContainer1$fml_info_tab$btn_save_fml" />
            <asp:PostBackTrigger ControlID="TabContainer1$hoi_info_tab$btn_rpt" />
        </Triggers>
    </asp:UpdatePanel>
    <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modal1">
                <div class="center">
                    <img alt="" src="../images1/loader.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <asp:UpdatePanel ID="upMsg" runat="server">
        <ContentTemplate>
            <div>
                <asp:Panel ID="pnlMessage" runat="server" Width="650px">
                    <div>
                        <AjaxToolkit:ModalPopupExtender ID="modalPE" runat="server" BehaviorID="modelPE"
                            TargetControlID="btnTarget" PopupControlID="popUpPanel" DropShadow="false" OkControlID="btnCancel"
                            BackgroundCssClass="modalBackground">
                        </AjaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="popUpPanel" Width="350px" Height="215px" DefaultButton="btnCancel"
                            runat="server" BackColor="Transparent" BackImageUrl="~/images/modalBack.png" style="display:none">
                            <div>
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 10%; height: 32px; vertical-align: bottom">
                                            <asp:Image ID="imgMessage" Height="30px" Width="30px" runat="server" ImageAlign="Top" />
                                        </td>
                                        <td style="width: 90%; height: 32px;" align="left">
                                            <asp:Label ID="lblMsgHeading" runat="server" Font-Size="14pt" Text="Warning" Font-Bold="true"
                                                ForeColor="White"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2" style="height: 100pt;">
                                            <div>
                                                <asp:Label ID="lblReason" runat="server" Width="90%" Font-Bold="True" Font-Names="Cambria"
                                                    Font-Size="Medium"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td width="50%" style="height: 25pt; text-align: right; padding-left: 100px;">
                                                        <asp:Button ID="btnYes" Width="60pt" runat="server" Text="Yes" CssClass="button_save"
                                                            Style="display: block;" />
                                                    </td>
                                                    <td width="50%" align="left">
                                                        <asp:Button ID="btncancel" OnClientClick="$find('modelPE').hide(); return false;"
                                                            Width="60pt" runat="server" Text="No" CssClass="button_close" Style="display: block;" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </div>
                    <div style="visibility: hidden">
                        <asp:Button ID="btnTarget" runat="server" />
                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="updProgress" runat="server" DisplayAfter="1">
        <ProgressTemplate>
            <div class="divWaiting">
                <div style="width: 330px; margin: auto; color: #fff; font-weight: bold; border-radius: 10px; box-shadow: 0 0 5px #000;">
                    <img alt="progress" src="../../images/hourglass.gif" style="width: 50px" /><br />
                    <span>Processing... Please Wait. </span>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <%--<asp:HiddenField ID="hideformodel" runat="server" />
    <AjaxToolkit:ModalPopupExtender ID="mb_rate" TargetControlID="hideformodel" BackgroundCssClass="modalBackground"
        Enabled="true" PopupControlID="PanelRpt" CancelControlID="hideformodel" runat="server">
    </AjaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelRpt" runat="server" CssClass="modalPopup" Style="width: 760px; z-index: 1;">
        <table id="Table6" width="760px" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="background-color: #ffffff">
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="760px" Height="500px"
                        BorderColor="Transparent" InternalBorderWidth="" ShowFindControls="False" ShowPrintButton="True"
                        ShowParameterPrompts="False" ShowRefreshButton="True" SizeToReportContent="false"
                        BorderWidth="1px" InternalBorderStyle="NotSet">
                    </rsweb:ReportViewer>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:ImageButton ID="ImageButton2" runat="server" Height="25px" ImageUrl="~/Higher_Education/images1/button_ok.png"
                        Width="60px" />
                </td>
            </tr>
            <tr>
                <td style="text-align: center"></td>
            </tr>
        </table>
    </asp:Panel>--%>

</asp:Content>

