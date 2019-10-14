<%@ Page Language="C#" MasterPageFile="~/ShiptalkWeb.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="ShiptalkWeb.AddUser" Title="Add a User" %>
<%@ Register Assembly="Lanap.BotDetect" Namespace="Lanap.BotDetect" TagPrefix="BotDetect" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">
  <div id="maincontent">
        <div class="dv3col">
            <h1>
                <asp:Literal ID="colTitle" runat="server" Text="Add a new user" EnableViewState="false"></asp:Literal>
            </h1>
            <div id="dv3colDefaultDescription" runat="server">
                (Items marked in <span class="required">*</span> indicate required fields.)
            </div>
            <asp:Literal ID="dv3colSuccessMessage" runat="server" Visible="false" EnableViewState="false"
                Text="You have successfully added a new user. Please note that an email with instructions have been sent to the User to verify the email address. Once the email is verified, the User will be able to login with the password you had set while creating the User. The User will be able to change the password anytime after logging into the system."></asp:Literal>
            <br />
            <asp:Panel ID="RegisterFormPanel" runat="server" DefaultButton="btnSubmit">
            <div id="dv3colFormContent" runat="server" style="margin-top: 10px; width: 615px;
                border-top: solid 2px #eee;">
                <table>
                    <tr>
                        <td style="width: 8px;">
                        </td>
                        <td colspan="2" style="margin-bottom: 2px; margin-bottom: 0px">
                            &nbsp;
                        </td>
                    </tr>
                    <asp:PlaceHolder ID="plhMessage" runat="server" Visible="false" EnableViewState="false">
                        <tr>
                            <td style="width: 8px;">
                                &nbsp;
                            </td>
                            <td style="width: 270px">
                            </td>
                            <td style="width: 310px">
                                <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false"></asp:Label>
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <tr>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 270px; text-align: right;">
                            <asp:Label ID="RoleLabel" runat="server" Text="Role" AssociatedControlID="ddlRoles"></asp:Label>
                            <asp:Label ID="Label4" runat="server" CssClass="required" Text="*" Style="padding: 0px;
                                margin: 0px"></asp:Label>
                        </td>
                        <td style="width: 310px; padding: border: solid 1px #000">
                            <asp:DropDownList ID="ddlRoles" runat="server" Width="286px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged" CssClass="dropdown2wm"
                                ValidationGroup="RegisterForm">
                            </asp:DropDownList>
                            <asp:RangeValidator ID="RoleRequired" runat="server" ErrorMessage="<br />The Role must be selected"
                                MinimumValue="01" MaximumValue="999" SetFocusOnError="True" ControlToValidate="ddlRoles" ValidationGroup="RegisterForm"
                                Display="Dynamic"></asp:RangeValidator>
                        </td>
                    </tr>
                    <asp:PlaceHolder ID="plhRoleDescription" runat="server" EnableViewState="true" Visible="false">
                        <tr>
                            <td style="width: 8px;">
                            </td>
                            <td style="width: 270px; text-align: right;">
                                &nbsp;
                            </td>
                            <td style="width: 310px">
                                <div style="width: 280px; background: #F2F2F2; color: #000; padding: 3px 0 8px 4px;">
                                    <span style="font-weight: normal; font-family: Arial; font-size: 12px;">Role description:
                                        <asp:Label ID="lblRoleDescription" runat="server" Text=""></asp:Label></span>
                                </div>
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="plhIsShipDirector" runat="server" EnableViewState="true" Visible="false">
                        <tr>
                            <td style="width: 8px;">
                            </td>
                            <td style="width: 270px; text-align: right;">
                                &nbsp;
                            </td>
                            <td style="width: 310px">
                                <div style="border: solid 0px #000; width: 286px;">
                                    <asp:CheckBox ID="chBoxIsShipDirector" runat="server" Text="Check this box, if State SHIP Director" 
                                        AutoPostBack="true" OnCheckedChanged="OnShipDirectorCheckboxAction" ValidationGroup="RegisterForm" />
                                </div>
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="plhCMSRegion" runat="server">
                        <tr>
                            <td style="width: 8px;">
                            </td>
                            <td style="width: 270px; text-align: right;">
                                <asp:Label ID="CMSRegionLabel" runat="server" Text="CMS Region" AssociatedControlID="ddlCMSRegion"></asp:Label>
                                <asp:Label ID="Label7" runat="server" CssClass="required" Text="*" Style="padding: 0px;
                                    margin: 0px"></asp:Label>
                            </td>
                            <td style="width: 310px">
                                <asp:DropDownList ID="ddlCMSRegion" runat="server" Width="286px" CssClass="dropdown2wm"
                                    ValidationGroup="RegisterForm">
                                </asp:DropDownList>
                                <asp:RangeValidator ID="CMSRegionSelection" runat="server" ErrorMessage="<br />The CMS Region must be selected"
                                    MinimumValue="01" MaximumValue="999" SetFocusOnError="True" ControlToValidate="ddlCMSRegion" ValidationGroup="RegisterForm"
                                    Display="Dynamic"></asp:RangeValidator>
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="divStates" runat="server">
                        <tr>
                            <td style="width: 8px;">
                            </td>
                            <td style="width: 270px; text-align: right;">
                                <asp:Label ID="StatesLabel" runat="server" Text="State" AssociatedControlID="ddlStates"></asp:Label>
                                <asp:Label ID="Label44" runat="server" CssClass="required" Text="*" Style="padding: 0px;
                                    margin: 0px"></asp:Label>
                            </td>
                            <td style="width: 310px">
                                <asp:DropDownList ID="ddlStates" runat="server" Width="286px" AutoPostBack="true"
                                    CssClass="dropdown2wm" OnSelectedIndexChanged="ddlStates_SelectedIndexChanged"
                                    ValidationGroup="RegisterForm">
                                </asp:DropDownList>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="<br />The State must be selected"
                                    MinimumValue="01" MaximumValue="999" SetFocusOnError="True" ControlToValidate="ddlStates" ValidationGroup="RegisterForm"
                                    Display="Dynamic"></asp:RangeValidator>
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="plhSubStateRegion" runat="server">
                        <tr>
                            <td style="width: 8px;">
                            </td>
                            <td style="width: 270px; text-align: right;">
                                <asp:Label ID="SubStateRegionLabel" runat="server" Text="Sub State Region" AssociatedControlID="ddlSubStateRegion"></asp:Label>
                                <asp:Label ID="Label5" runat="server" CssClass="required" Text="*" Style="padding: 0px;
                                    margin: 0px"></asp:Label>
                            </td>
                            <td style="width: 310px">
                                <asp:DropDownList ID="ddlSubStateRegion" runat="server" Width="286px" CssClass="dropdown2wm"
                                    ValidationGroup="RegisterForm">
                                </asp:DropDownList>
                                <asp:RangeValidator ID="SubStateRegionRequired" runat="server" ErrorMessage="<br />The Sub State Region must be selected"
                                    MinimumValue="01" MaximumValue="9999" SetFocusOnError="True" ControlToValidate="ddlSubStateRegion" ValidationGroup="RegisterForm"
                                    Display="Dynamic"></asp:RangeValidator>
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="plhAgencies" runat="server">
                        <tr>
                            <td style="width: 8px;">
                            </td>
                            <td style="width: 270px; text-align: right;">
                                <asp:Label ID="AgencyLabel" runat="server" Text="Agency" AssociatedControlID="ddlAgency"></asp:Label>
                                <asp:Label ID="Label3" runat="server" CssClass="required" Text="*" Style="padding: 0px;
                                    margin: 0px"></asp:Label>
                            </td>
                            <td style="width: 310px">
                                <asp:DropDownList ID="ddlAgency" runat="server" Width="286px" CssClass="dropdown2wm"
                                    ValidationGroup="RegisterForm">
                                </asp:DropDownList>
                                <asp:RangeValidator ID="AgencyRequired" runat="server" ErrorMessage="<br />The Agency must be selected"
                                    MinimumValue="01" MaximumValue="99999" SetFocusOnError="True" ControlToValidate="ddlAgency" ValidationGroup="RegisterForm"
                                    Display="Dynamic"></asp:RangeValidator>
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <tr>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 270px; text-align: right;">
                            <asp:Label ID="FirstNameLabel" runat="server" Text="First Name" AssociatedControlID="FirstName"></asp:Label>
                            <asp:Label ID="Label10" runat="server" CssClass="required" Text="*" Style="padding: 0px;
                                margin: 0px"></asp:Label>
                        </td>
                        <td style="width: 310px">
                            <asp:TextBox ID="FirstName" runat="server" Width="280px" ValidationGroup="RegisterForm"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="FirstNameRequired" runat="server" ErrorMessage="<br/>First name is required"
                                Display="Dynamic" SetFocusOnError="True" ControlToValidate="FirstName" ValidationGroup="RegisterForm"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="FirstNameValidator" runat="server" 
                                ErrorMessage="Only alphabets, apostrophe, hyphens allowed; maximum 50 characters." 
                                Display="Dynamic" ValidationGroup="RegisterForm" SetFocusOnError="True" ControlToValidate="FirstName" 
                                ValidationExpression="^[a-zA-Z'\-.\s]{1,50}$"></asp:RegularExpressionValidator>  
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px;">
                            &nbsp;
                        </td>
                        <td style="width: 270px; text-align: right;">
                            <asp:Label ID="MiddleNameLabel" runat="server" Text="Middle Name" AssociatedControlID="MiddleName"></asp:Label>
                        </td>
                        <td style="width: 310px">
                            <asp:TextBox ID="MiddleName" runat="server" Width="280px" ValidationGroup="RegisterForm"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="MiddleNameValidator" runat="server" 
                                ErrorMessage="Only alphabets, apostrophe, hyphens allowed; maximum 50 characters." 
                                Display="Dynamic" ValidationGroup="RegisterForm" SetFocusOnError="True" ControlToValidate="MiddleName" 
                                ValidationExpression="^[a-zA-Z'\-.\s]{1,50}$"></asp:RegularExpressionValidator>  
                            
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 270px; text-align: right;">
                            <asp:Label ID="LastNameLabel" runat="server" Text="Last Name" AssociatedControlID="LastName"></asp:Label>
                            <asp:Label ID="Label1" runat="server" CssClass="required" Text="*" Style="padding: 0px;
                                margin: 0px"></asp:Label>
                        </td>
                        <td style="width: 310px">
                            <asp:TextBox ID="LastName" runat="server" Width="280px" ValidationGroup="RegisterForm"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="LastNameRequired" runat="server" ErrorMessage="&lt;br /&gt;Last name is required"
                                Display="Dynamic" SetFocusOnError="True" ControlToValidate="LastName" ValidationGroup="RegisterForm"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="LastNameValidator" runat="server" 
                                ErrorMessage="Only alphabets, apostrophe, hyphens allowed; maximum 50 characters." 
                                Display="Dynamic" ValidationGroup="RegisterForm" SetFocusOnError="True" ControlToValidate="LastName" 
                                ValidationExpression="^[a-zA-Z'\-.\s]{1,50}$"></asp:RegularExpressionValidator>  
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 270px; text-align: right;">
                            <asp:Label ID="NickNameLabel" runat="server" Text="Nick Name" AssociatedControlID="NickName"></asp:Label>
                        </td>
                        <td style="width: 310px">
                            <asp:TextBox ID="NickName" runat="server" Width="280px" ValidationGroup="RegisterForm"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="NickNameValidator" runat="server" 
                                ErrorMessage="Only alphabets, apostrophe, hyphens allowed; maximum 50 characters." 
                                Display="Dynamic" ValidationGroup="RegisterForm" SetFocusOnError="True" ControlToValidate="NickName" 
                                ValidationExpression="^[a-zA-Z'\-.\s]{1,50}$"></asp:RegularExpressionValidator>  
                            
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 270px; text-align: right;">
                            <asp:Label ID="SuffixLabel" runat="server" Text="Suffix" AssociatedControlID="Suffix"></asp:Label>
                        </td>
                        <td style="width: 310px">
                            <asp:TextBox ID="Suffix" runat="server" ValidationGroup="RegisterForm"></asp:TextBox>&nbsp;<span class="smaller"><span class="gray">(e.g., Jr.)</span></span>
                            <asp:RegularExpressionValidator ID="SuffixValidator" runat="server" 
                                ErrorMessage="<br />Only alphabets, numbers, hyphens allowed; maximum 20 characters." 
                                Display="Dynamic" ValidationGroup="RegisterForm" SetFocusOnError="True" ControlToValidate="Suffix" 
                                ValidationExpression="^[a-zA-Z\-.\s]{1,20}$"></asp:RegularExpressionValidator>  
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 270px; text-align: right;">
                            <asp:Label ID="HonorificsLabel" runat="server" Text="Honorifics" AssociatedControlID="Honorifics"></asp:Label>
                        </td>
                        <td style="width: 310px">
                            <asp:TextBox ID="Honorifics" runat="server" ValidationGroup="RegisterForm"></asp:TextBox>&nbsp;<span class="smaller"><span class="gray">(e.g., MSW, MD)</span></span>
                            <asp:RegularExpressionValidator ID="HonorificsValidator" runat="server" 
                                ErrorMessage="<br />Only alphabets, numbers, hyphens allowed; maximum 20 characters." 
                                Display="Dynamic" ValidationGroup="RegisterForm" SetFocusOnError="True" ControlToValidate="Honorifics" 
                                ValidationExpression="^[a-zA-Z\-.\s]{1,20}$"></asp:RegularExpressionValidator>  
                            
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 270px; text-align: right;">
                            <asp:Label ID="PrimaryEmailLabel" runat="server" Text="Primary Email" AssociatedControlID="Email"></asp:Label>
                            <asp:Label ID="Label2" runat="server" CssClass="required" Text="*" Style="padding: 0px;
                                margin: 0px"></asp:Label>
                        </td>
                        <td style="width: 310px">
                            <asp:TextBox ID="Email" runat="server" Width="280px" ValidationGroup="RegisterForm"></asp:TextBox>
                            <asp:Label ID="Label9" runat="server" Text="(The Primary email will be your Login User name)"
                                Width="284px" Font-Size="Small"></asp:Label>
                            <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ErrorMessage="<br />The Primary email address is required"
                                Display="Dynamic" SetFocusOnError="True" ControlToValidate="Email" ValidationGroup="RegisterForm"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="EmailFmtValidate" runat="server" ErrorMessage="<br />Primary email is not in a valid format"
                                ValidationExpression="^[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.(([0-9]{1,3})|([a-zA-Z]{2,3})|(aero|coop|info|museum|name))$"
                                SetFocusOnError="True" ControlToValidate="Email" Display="Dynamic" ValidationGroup="RegisterForm"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 270px; text-align: right;">
                            &nbsp;
                        </td>
                        <td style="width: 310px;">
                            <div style=" width: 286px; padding: 4px; background-color: #ffcc00;">
                            <span style="font-weight: bold;">Minimum security requirements</span>:
                            <br />
                            <span style="font-size: 12px;">Your password must be between 6 to 15 characters and must contain at least 1 numeric.</span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 270px; text-align: right;">
                            <asp:Label ID="PasswordLabel" runat="server" Text="Password" AssociatedControlID="Password"></asp:Label>
                            <asp:Label ID="Label8" runat="server" CssClass="required" Text="*" Style="padding: 0px;
                                margin: 0px"></asp:Label>
                        </td>
                        <td style="width: 310px">
                            <asp:TextBox ID="Password" runat="server" Width="280px" TextMode="Password" ValidationGroup="RegisterForm"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ErrorMessage="Password is required."
                                Display="Dynamic" SetFocusOnError="True" ControlToValidate="Password" ValidationGroup="RegisterForm"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="PasswordValidate" runat="server" ErrorMessage="&lt;br /&gt;Password entered does not meet minimum requirements."
                                ValidationExpression="^.*(?=^.{6,15}$)(?=.*[a-zA-Z])(?=.*[0-9])((?=.*\\d)||(.*[\\W])).*$"
                                SetFocusOnError="True" ControlToValidate="Password" Display="Dynamic" ValidationGroup="RegisterForm"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 270px; text-align: right;">
                            <asp:Label ID="ConfirmPasswordLabel" runat="server" Text="Confirm Password" AssociatedControlID="ConfirmPassword"></asp:Label>
                            <asp:Label ID="Label12" runat="server" CssClass="required" Text="*" Style="padding: 0px;
                                margin: 0px"></asp:Label>
                        </td>
                        <td style="width: 310px">
                            <asp:TextBox ID="ConfirmPassword" runat="server" Width="280px" TextMode="Password"
                                ValidationGroup="RegisterForm"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ErrorMessage="Please retype your password"
                                Display="Dynamic" SetFocusOnError="True" ControlToValidate="ConfirmPassword" ValidationGroup="RegisterForm"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="ComparePassword" runat="server" ErrorMessage="&lt;br /&gt;Password and Re-type password must match"
                                SetFocusOnError="True" ControlToValidate="ConfirmPassword" ControlToCompare="Password" Display="Dynamic"
                                ValidationGroup="RegisterForm"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 270px; text-align: right;">
                            <asp:Label ID="SecondaryEmailLabel" runat="server" Text="Secondary Email" AssociatedControlID="SecondaryEmail"></asp:Label>
                        </td>
                        <td style="width: 310px">
                            <asp:TextBox ID="SecondaryEmail" runat="server" Width="280px" ValidationGroup="RegisterForm"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="SecondaryEmailFmtValidate" runat="server" ErrorMessage="<br/ >Secondary email is not in a valid format"
                                ValidationExpression="^[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.(([0-9]{1,3})|([a-zA-Z]{2,3})|(aero|coop|info|museum|name))$"
                                SetFocusOnError="True" ControlToValidate="SecondaryEmail" Display="Dynamic" ValidationGroup="RegisterForm"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 270px; text-align: right;">
                            <asp:Label ID="PrimaryPhoneLabel" runat="server" Text="Primary Phone" AssociatedControlID="PrimaryPhone"></asp:Label>
                            <asp:Label ID="Label6" runat="server" CssClass="required" Text="*" Style="padding: 0px;
                                margin: 0px"></asp:Label>
                        </td>
                        <td style="width: 310px">
                            <asp:TextBox ID="PrimaryPhone" runat="server" ValidationGroup="RegisterForm"></asp:TextBox><br /><span class="gray">(e.g., 999-999-9999 x9999.)</span>
                            <asp:RequiredFieldValidator ID="PrimaryPhoneRequired" runat="server" ErrorMessage="<br />Primary phone is required"
                                Display="Dynamic" SetFocusOnError="True" ControlToValidate="PrimaryPhone" ValidationGroup="RegisterForm"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="PrimaryPhoneValidate" runat="server" ErrorMessage="<br/>Primary phone is not in a valid format"
                                ValidationExpression="\(?\d{3}\)?[-\s.]?\d{3}[-.]\d{4}( x\d{0,5})?" SetFocusOnError="True" ControlToValidate="PrimaryPhone"
                                Display="Dynamic" ValidationGroup="RegisterForm"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 270px; text-align: right;">
                            <asp:Label ID="SecondaryPhoneLabel" runat="server" Text="Secondary Phone" AssociatedControlID="SecondaryPhone"></asp:Label>
                        </td>
                        <td style="width: 310px">
                            <asp:TextBox ID="SecondaryPhone" runat="server" Width="280px" ValidationGroup="RegisterForm"></asp:TextBox><br /><span class="gray">(e.g., 999-999-9999 x9999.)</span>
                            <asp:RegularExpressionValidator ID="SecondaryPhoneFmtValidate" runat="server" ErrorMessage="Secondary phone is not in a valid format"
                                ValidationExpression="\(?\d{3}\)?[-\s.]?\d{3}[-.]\d{4}( x\d{0,5})?" SetFocusOnError="True" ControlToValidate="SecondaryPhone"
                                Display="Dynamic" ValidationGroup="RegisterForm"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 270px; text-align: right;">
                            &nbsp;
                        </td>
                        <td style="width: 310px">
                            <asp:CheckBoxList ID="cblDescriptors" runat="server" ValidationGroup="RegisterForm">
                                <asp:ListItem Value="1">Counselor</asp:ListItem>
                                <asp:ListItem Value="2">Data Submitter</asp:ListItem>
                                <asp:ListItem Value="3">Presentation and Media Staff</asp:ListItem>
                                <asp:ListItem Value="4">Data Editor/Reviewer</asp:ListItem>
                                <asp:ListItem Value="5">Other Staff (NPR Read Only)</asp:ListItem>
                                <asp:ListItem Value="6">Other Staff (SHIP Read Only)</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 270px; text-align: right;">
                        </td>
                        <td style="width: 310px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 270px; text-align: right;">
                        </td>
                        <td style="width: 310px">
                            <BotDetect:Captcha ID="ctrlCaptcha" runat="server" SoundEnabled="true" SoundIconAltText="Listen to image text" />
                            <%--onprerender="ctrlCaptcha_PreRender" />--%>
                            <asp:Panel ID="Panel1" runat="server">
                            </asp:Panel>
                            <br />
                            <asp:Label ID="CaptchaLabel" runat="server" AssociatedControlID="CaptchaText">Enter the text from the image or sound above.</asp:Label>
                            <br />
                            <asp:TextBox ID="CaptchaText" runat="server" EnableViewState="false" ValidationGroup="RegisterForm"></asp:TextBox>
                            <asp:CustomValidator ID="cvCustomValidator" runat="server" ErrorMessage="" Display="Dynamic"
                                ValidationGroup="RegisterForm"></asp:CustomValidator>
                            <asp:RequiredFieldValidator ID="CaptchaTextRequired" runat="server" SetFocusOnError="True" ControlToValidate="CaptchaText"
                                Display="Dynamic" ErrorMessage="Image text is required" ToolTip="Image text is required."
                                ValidationGroup="RegisterForm"><BR />Please enter the text from image or sound.</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 270px; text-align: right;">
                        </td>
                        <td style="width: 310px;">
                            <div style="float: right; margin-right: 24px;">
                                <asp:Label ID="Label11" runat="server" Text="Before submission, please ensure that your password was entered."></asp:Label>
                            </div>
                            <div style="float: left; margin-left: 0px">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="formbutton3" Width="130px"
                                    OnClick="RegisterUserCommand" ValidationGroup="RegisterForm" UseSubmitBehavior="true" 
                                     CommandName="Submit" CommandArgument="RegisterForm" /></div>
                            
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 270px; text-align: right;">
                        </td>
                        <td style="width: 310px">
                        </td>
                    </tr>
                </table>
            </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
