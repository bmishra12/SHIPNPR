<%@ Page Language="C#" MasterPageFile="~/ShiptalkWebWideNoTab.Master" AutoEventWireup="true"
    CodeBehind="NewPass.aspx.cs" Inherits="ShiptalkWeb.NewPass" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Shiptalk Account Helper </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">

<div class="dvPad">
    <script type="text/javascript">
        // Amar Beeharry replaced Javascript with JQuery for x-browser compatibility
        //This line also eliminates prompts to remember passwords in firefox.
        // 7/2/2012
        $(document).ready(function () { $("input").attr("autocomplete", "off"); }); 
    </script>
</div>
    <div id="maincontent">
      
        <asp:Panel ID="ResetResultPanel" runat="server" EnableViewState="false" Visible="false">
            <div class="dv3col">
                <h1>
                    <asp:Literal ID="ResetResultTitle" runat="server" Text="" EnableViewState="false"></asp:Literal>
                </h1>
                <asp:Literal ID="ResetResultDescription" runat="server" EnableViewState="false" Text=""></asp:Literal>
            </div>
        </asp:Panel>
        <asp:Panel ID="PasswordChangePanel" runat="server" Visible="false" DefaultButton="btnChangePassword">
            <div style="width: 403px">
                <div class="dv4colleft">
                    <div style="margin-top: 8px; padding: 4px; background-color: #ffcc00;">
                        <span style="font-weight: bold;">Minimum security requirements</span>:
                        <br />
                       
                        <ul>
                        <li>
                        Your password must be between 8 and 30 characters.
                        </li>
                        <li>
                        Your password must contain at least one uppercase (capital) letter (e.g., A, B, etc.).
                        </li>
                        <li>
                        Your password must contain at least one digit (e.g., 1, 2, 3, etc.).
                        </li>
                        <li>
                       Your password must contain at least one special character (e.g. !, @, #, %, etc.).
                        </li>
                        <li>
                        <u>Your password may not include any actual words </u> (referred to as “dictionary words”). <br />
                        This includes certain common names as well.
                        
                        </li>
                        
                        </ul>
            
                    </div>
                    <p>
                        Please enter your new password below:</p>
                    <div>
                        <div style="float: left;">
                            <div style="width: 122px; float: left; border: solid 0px #000;">
                                New Password:</div>
                            <div style="width: 150px; float: right; border: solid 0px #000;">
                                <asp:TextBox ID="Password" runat="server" Width="142px" ValidationGroup="ChangePasswordGroup"
                                    TextMode="Password" AutoCompleteType="Disabled"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ErrorMessage="Password is required."
                                    Display="Dynamic" ControlToValidate="Password" ValidationGroup="ChangePasswordGroup"
                                    EnableClientScript="true" Enabled="true"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="PasswordValidate" runat="server" ErrorMessage="&lt;br /&gt;Password entered does not meet minimum requirements. Please see the yellow box above for a description of what your password needs to include."
  ValidationExpression="^.*(?=^.{8,30}$)(?=.*\d)(?=.*[A-Z])(?=.*[\&quot;!#$%&'()*+,-./:;<=>?@[\]^_`{|}~]).*$"  
                                    ControlToValidate="Password" Display="Dynamic" ValidationGroup="ChangePasswordGroup"
                                    EnableClientScript="true" Enabled="true"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div style="float: left; margin-top: 9px">
                            <div style="width: 122px; float: left; border: solid 0px #000;">
                                Re-type Password:</div>
                            <div style="width: 150px; float: right; border: solid 0px #000;">
                                <asp:TextBox ID="ConfirmPassword" runat="server" Width="142px" ValidationGroup="ChangePasswordGroup"
                                    TextMode="Password" AutoCompleteType="Disabled"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ErrorMessage="Please retype your password"
                                    Display="Dynamic" ControlToValidate="ConfirmPassword" ValidationGroup="ChangePasswordGroup"
                                    EnableClientScript="true" Enabled="true"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="ComparePassword" runat="server" ErrorMessage="&lt;br /&gt;Password and Re-type password must match"
                                    ControlToValidate="ConfirmPassword" ControlToCompare="Password" Display="Dynamic"
                                    ValidationGroup="ChangePasswordGroup" EnableClientScript="true"></asp:CompareValidator>
                            </div>
                        </div>
                        <div style="float: left; margin-top: 9px" runat="server" id="MessageArea" enableviewstate="false"
                            visible="false">
                            <div style="width: 272px; float: left; padding-right: 4px">
                                <asp:Label ID="Message" runat="server" Text="" EnableViewState="false" CssClass="required"></asp:Label>
                            </div>
                        </div>
                        <div style="float: left; margin-top: 9px">
                            <div style="width: 122px; float: left;">
                                &nbsp;
                            </div>
                            <div style="width: 150px; float: right; padding-right: 4px">
                                <asp:Button ID="btnChangePassword" runat="server" Text="Submit >>" CssClass="formbutton3"
                                    OnClick="ChangePasswordCommand" ValidationGroup="ChangePasswordGroup" />

                                    
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
