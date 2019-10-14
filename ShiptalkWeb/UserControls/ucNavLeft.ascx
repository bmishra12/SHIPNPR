<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucNavLeft.ascx.cs" Inherits="ShiptalkWeb.ucNavLeft" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Import Namespace="ShiptalkCommon" %>





<div class="dvPad" style="display: none">
    &nbsp;</div>
    
    
<!-- logged in nav section : begin -->
<div id="publicNav" runat="server">
    <div class="leftNavBox1">

        <div id="AnonymousTabs" runat="server">     
                <div class="leftNavHeader">
                    <asp:Label ID="LoginHereLabel" runat="server" Text="Enter Email and Password"></asp:Label>
                    <span class="offscreen">
                        <asp:Label ID="lblUsername" runat="server" Text="Username" AssociatedControlID="txtUser"></asp:Label>
                        <asp:Label ID="lblPassword" runat="server" Text="Password" AssociatedControlID="txtPassword"></asp:Label>
                    </span>
                </div>
                <div class="leftNavContent"  >
                    <asp:Panel ID="LoginPanel" runat="server" DefaultButton="btnLogin">
                    <p>
                        <asp:Label ID="LoginError" runat="server" Text="" BackColor="#ffcc00" EnableViewState="false"></asp:Label>
                        <asp:Label ID="Label1" runat="server" Text="Your email address:"  Font-Bold=true EnableViewState="false"></asp:Label>
                      
                        <asp:TextBox ID="txtUser" runat="server" CssClass="textfield1" ValidationGroup="LoginForm" AutoCompleteType="Disabled"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ErrorMessage="Please use the email address that was registered as your username."
                            Display="Dynamic" ControlToValidate="txtUser" ValidationGroup="LoginForm"></asp:RequiredFieldValidator>
                              <asp:Label ID="Label2" runat="server" Text="Password:" Font-Bold=true EnableViewState="false"></asp:Label>
                      
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="textfield1" TextMode="Password"
                            ValidationGroup="LoginForm" AutoCompleteType="Disabled"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ErrorMessage="Password is required."
                            Display="Dynamic" ControlToValidate="txtPassword" ValidationGroup="LoginForm"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="PasswordValidate" runat="server" ErrorMessage="To comply with new CMS security regulations, all passwords must now be between 8 to 30 characters and must contain at least one upper case letter, at least one digit and at least one special character. Please click on the 'Forgot Password' link to reset your password."
                            ValidationExpression="^.*(?=.{8,30})(?=.*\d)(?=.*[A-Z])(?=.*[\&quot;!#$%&'()*+,-./:;<=>?@[\]^_`{|}~]).*$"
                            ControlToValidate="txtPassword" Display="Dynamic" ValidationGroup="LoginForm"></asp:RegularExpressionValidator>
                        <asp:Button ID="btnLogin" runat="server" Text="GO >>" CssClass="formbutton3" OnClick="btnLogin_Click"
                            ValidationGroup="LoginForm" />
                      <%--  <cc1:TextBoxWatermarkExtender ID="tbwe2" runat="server" TargetControlID="txtUser"
                            WatermarkText="Your email" WatermarkCssClass="textfield1wm" />--%>
                               
                        <asp:HyperLink ID="LinkToResetPassword" runat="server" ToolTip="Reset your password" NavigateUrl="~/ResetPassword.aspx">Forgot password?</asp:HyperLink><br />
                      <%--  <asp:HyperLink ID="OldShipUserLink" runat="server" NavigateUrl="~/OldShipUserLogin.aspx"><br />Old www.SHIPtalk.org User Login</asp:HyperLink>--%>
                    </p>
                   
                    
                    </asp:Panel>
                    <div class="clear">
                    </div>
                </div>
            </div>
            

        
    </div>
        <br />
    <br />
    <div class="leftNavBox1"  >
    <div class="leftNavHeader">
        New password policy
    </div>

    <div class="leftNavContent">
        <p>
                
                    <asp:Button ID="btnResetPassword" runat="server" Text="Reset Password" CssClass="formbutton3" OnClick="btnResetPassword_Click" />
                    </p>
        <p>Password requirements have been revised to comply with new security regulations
            <a runat="server" id="asd" href="~/npr/docs/SHIP_NPR_Password_Inst_508.pdf" target="_blank">Please click here</a> for detailed information on how to reset your password.</p>
    </div>
</div>
</div>
 <br />
  <br />


<div class="dvPad">
    <script type="text/javascript">
        // Amar Beeharry replaced Javascript with JQuery for x-browser compatibility
        //This line also eliminates prompts to remember passwords in firefox.
        // 7/2/2012
           $(document).ready(function () { $("input").attr("autocomplete", "off"); }); 
    </script>
</div>
<!-- logged in nav section : end -->
<!-- public nav section : begin -->
<div id="loggedInNav" runat="server" >
    <div class="leftNavBox1">
        <div class="leftNavHeader"  style="display: none">
            Test public nav box
        </div>
        <div class="leftNavContent">
            <div class="welcomeBanner">
                <asp:Literal ID="welcomeText" runat="server"></asp:Literal>
            </div>
            <br />
            <br />
            <br />

            <div class="commands" style="text-align:left">

                    <a runat="server" id="aAddCCF" href='../forms/ccf/add'>Add a Contact for a New Client With No Prior Service at This Agency</a>
                    <br />
                    <br />
                    <a runat="server" id="aAddPam" href='../forms/pam/add'>Add a PAM</a>
            </div>
        </div>
    </div>
    <div class="dvPad"> 
        &nbsp;</div>
</div>
<!-- public nav section :
end -->
<!-- only for sub pages -->

<div class="dvPad" style="display: block">
    &nbsp;</div>
    
<div style="display: block">
   
</div>
<div class="dvPad" style="display: none">
    &nbsp;</div>

<div class="leftNavBox1" style="display: none">
    <div class="leftNavHeader">
       <%-- <asp:Label ID="FindAShipStatesLabel" runat="server" Text="Find a SHIP" AssociatedControlID="ddlFindAShipStates"></asp:Label>--%>
    </div>
    <div class="leftNavContent" style="display: none">
        <p>
            <asp:DropDownList ID="ddlFindAShipStates" runat="server" CssClass="dropdown2wm" onchange="ddl2Change(this)" ToolTip="Select  a State">
                <asp:ListItem Text="Select a State" Value=""></asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="Button4" runat="server" Text="GO >>" CssClass="formbutton3" />
        </p>
        <div class="clear">
        </div>
    </div>
    
</div>
<div class="dvPad">
    &nbsp;</div>
<div class="leftNavBox1" style="display: none">
    <div class="leftNavHeader">
        <asp:Label ID="FindCounselorLabel" runat="server" Text="Find a Counselor"></asp:Label>
    </div>
     <span class="offscreen">
        <asp:Label ID="FindCounselorStatesLabel" runat="server" Text="Select a State" AssociatedControlID="ddlFindCounselorStates"></asp:Label>
        <asp:Label ID="FindCounselorCounty" runat="server" Text="Select a Counselor" AssociatedControlID="ddlFindCounselorCounties"></asp:Label>
     </span>
    <div class="leftNavContent">
        <p>
            <asp:DropDownList ID="ddlFindCounselorStates" runat="server" CssClass="dropdown2wm" onchange="ddl2Change(this)">
                <asp:ListItem Text="Select a State" Value=""></asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="ddlFindCounselorCounties" runat="server" CssClass="dropdown2wm" onchange="ddl2Change(this)">
                <asp:ListItem Text="Select a County" Value=""></asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="Button5" runat="server" Text="GO >>" CssClass="formbutton3" />
        </p>
        <div class="clear">
        </div>
    </div>
</div>
<div class="dvPad">
    &nbsp;</div>
<!-- end
only for sub pages -->
