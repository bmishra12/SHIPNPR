<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeEmail.aspx.cs" MasterPageFile="~/ShiptalkWeb.Master" Inherits="ShiptalkWeb.ChangeEmail" %>

<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls"
    TagPrefix="pp" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    TagPrefix="pp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Change email</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontent">
        <asp:Panel runat="server" ID="ChangeEmailPanel" DefaultButton="btnChangeEmail">
            <div class="dv4colleft">
                <h2>
                    Change your email</h2>
                
                <p>
                    Please enter your new email below:</p>
                <div>
                    <div style="float: left;">
                        <div style="width: 122px; float: left; border: solid 0px #000;">
                            <asp:Label ID="lblNewEmail" runat="server" Text="New email:" AssociatedControlID="Email"></asp:Label> </div>
                        <div style="width: 279px; float: left; border: solid 0px #000;">
                            <asp:RequiredFieldValidator ID="EmailRequired" CssClass="validationMessage" runat="server"
                                        ErrorMessage="Primary email address is required<br/>" Display="Dynamic" SetFocusOnError="True"
                                        ControlToValidate="Email" ValidationGroup="ChangeEmail"></asp:RequiredFieldValidator>
                                    <%--<asp:RegularExpressionValidator ID="EmailFmtValidate" CssClass="validationMessage"
                                        runat="server" ErrorMessage="Primary email is not in a valid format<br/>" ValidationExpression='<%# ShiptalkCommon.ConfigUtil.EmailValidationRegex %>'
                                        SetFocusOnError="True" ControlToValidate="Email" Display="Dynamic" ValidationGroup="RegisterForm"></asp:RegularExpressionValidator>--%>
                                    <asp:TextBox ID="Email" CssClass="textfield3" runat="server" ValidationGroup="ChangeEmail"></asp:TextBox>
                                    
                        </div>
                    </div>
                    <div style="float: left; margin-top: 9px">
                        <div style="width: 122px; float: left; border: solid 0px #000;">
                             <asp:Label ID="lblRetypeEmail" runat="server" Text=" Re-type email:" AssociatedControlID="Email1"></asp:Label></div>
                        <div style="width: 279px; float: left; border: solid 0px #000;">
                            <asp:RequiredFieldValidator ID="EmailRequired1" CssClass="validationMessage" runat="server"
                                        ErrorMessage="Primary email address is required<br/>" Display="Dynamic" SetFocusOnError="True"
                                        ControlToValidate="Email1" ValidationGroup="ChangeEmail"></asp:RequiredFieldValidator>
                                    <%--<asp:RegularExpressionValidator ID="EmailFmtValidate" CssClass="validationMessage"
                                        runat="server" ErrorMessage="Primary email is not in a valid format<br/>" ValidationExpression='<%# ShiptalkCommon.ConfigUtil.EmailValidationRegex %>'
                                        SetFocusOnError="True" ControlToValidate="Email" Display="Dynamic" ValidationGroup="RegisterForm"></asp:RegularExpressionValidator>--%>
                                    <asp:TextBox ID="Email1" CssClass="textfield3" runat="server" ValidationGroup="RegisterForm"></asp:TextBox>
                                     <asp:CompareValidator ID="CompareEmail" runat="server" ErrorMessage="&lt;br /&gt;Email and Re-type Email must match"
                                ControlToValidate="Email1" ControlToCompare="Email" Display="Dynamic"
                                ValidationGroup="ChangeEmail" EnableClientScript="false"></asp:CompareValidator>
                              
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
                            <asp:Button ID="btnChangeEmail" runat="server" Text="Submit >>" CssClass="formbutton3"
                                OnClick="ChangeEmailCommand" ValidationGroup="ChangeEmail" />
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
