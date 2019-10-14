<%@ Page Language="C#" MasterPageFile="~/ShiptalkWebWideNoTab.Master" AutoEventWireup="true"
    CodeBehind="ResetPassword.aspx.cs" Inherits="ShiptalkWeb.ResetPassword" Title="Reset SHIPtalk password"  %>
<%@ Import Namespace="ShiptalkCommon" %>
<%@ Register Assembly="Lanap.BotDetect" Namespace="Lanap.BotDetect" TagPrefix="BotDetect" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontent">
        <asp:Panel ID="ResetPasswordPanel" runat="server" DefaultButton="btnResetPassword">
            <div class="dv3col" id="test12" runat="server">
                <h1>
                    <asp:Literal ID="colTitle" runat="server" Text="Forgot password?" EnableViewState="false" ></asp:Literal>
                </h1>
                <asp:Literal ID="dv3colSuccessMessage" runat="server" Visible="true"  Text="If you have a SHIPtalk account and forgot your password, you can reset your password by providing the requested information below:"></asp:Literal>
            </div>
            <div class="dv3col">
                <div style="width: 275px; float: left" runat="server">
                    <div style="float: left; margin-top: 9px" runat="server" id="MessageArea" enableviewstate="false"
                        visible="false">
                        <div style="width: 272px; float: left; padding-right: 4px">
                            <asp:Label ID="Message" runat="server" Text="" EnableViewState="false" CssClass="required"></asp:Label>
                        </div>
                    </div>
                    <div  runat="server" id = "email1"  enableviewstate="false">
                   
                    <div style="float: left; margin-top: 9px"  >
                        <div style="width: 272px; float: left; padding-right: 4px">
                            <asp:Label ID="Label123" runat="server" Text="Please enter the email address you used when you registered for SHIPtalk" AssociatedControlID="Email"></asp:Label>
                        </div>
                        <div style="width: 272px; float: left; padding-right: 4px">
                            <asp:TextBox ID="Email" runat="server" Width="264px" ValidationGroup="ResetPassword"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ErrorMessage="<br />Email address is required"
                                Display="Dynamic" SetFocusOnError="True" ControlToValidate="Email" ValidationGroup="ResetPassword"></asp:RequiredFieldValidator>
                           <%-- <asp:RegularExpressionValidator ID="EmailFmtValidate" runat="server" ErrorMessage="<br />The email is not in a valid format"
                                ValidationExpression='<%# ShiptalkCommon.ConfigUtil.EmailValidationRegex %>'
                                SetFocusOnError="True" ControlToValidate="Email" Display="Dynamic" ValidationGroup="ResetPassword" EnableViewState="false"></asp:RegularExpressionValidator>--%>
                        </div>
                    </div>
                    
                        <div style="width: 272px; float: left; padding-right: 4px">
                            <asp:Label ID="EmailConfirmText" runat="server" Text="Please re-enter your email address" AssociatedControlID="EmailConfirm"></asp:Label>
                        </div>
                        <div style="width: 272px; float: left; padding-right: 4px">
                            <asp:TextBox ID="EmailConfirm" runat="server" Width="264px" ValidationGroup="ResetPassword"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="EmailConfirmRequired" runat="server" ErrorMessage="<br />Email address must be retyped"
                                Display="Dynamic" SetFocusOnError="True" ControlToValidate="EmailConfirm" ValidationGroup="ResetPassword"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareEmail" runat="server" ErrorMessage="<br />Email and Retype email must match"
                                SetFocusOnError="True" ControlToValidate="EmailConfirm" ControlToCompare="Email"
                                Display="Dynamic" ValidationGroup="ResetPassword"></asp:CompareValidator>
                        </div>
                   
                    <div style="float: left; margin-top: 19px">
                        <div style="width: 272px; float: left; padding-right: 4px">
                            <BotDetect:Captcha ID="ctrlCaptcha" runat="server" SoundEnabled="true" SoundIconAltText="Listen to image text" />
                            <asp:Label ID="CaptchaLabel" runat="server" AssociatedControlID="CaptchaText">Enter text from the image or sound above</asp:Label>
                            <asp:CustomValidator ID="cvCustomValidator" runat="server" ErrorMessage="" Display="Dynamic"
                                ValidationGroup="ResetPassword"></asp:CustomValidator>
                            <br />
                            <asp:TextBox ID="CaptchaText" runat="server" EnableViewState="false" ValidationGroup="ResetPassword"
                                Width="264px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CaptchaTextRequired" runat="server" SetFocusOnError="True"
                                ControlToValidate="CaptchaText" Display="Dynamic" ErrorMessage="Image text is required"
                                ToolTip="Image text is required." ValidationGroup="ResetPassword"><BR />Enter the text from image or sound above</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div style="float: left; margin-top: 9px">
                        <div style="width: 270px; padding-right: 4px">
                            <asp:Button ID="btnResetPassword" runat="server" Text="Submit >>" CssClass="formbutton3"
                                OnClick="ResetPasswordCommand" ValidationGroup="ResetPassword" />
                        </div>
                   
                </div>
                </div>
            </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
