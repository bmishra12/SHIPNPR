<%@ Page Language="C#" MasterPageFile="~/ShiptalkWeb.Master" AutoEventWireup="true"
    CodeBehind="UserRegistration.aspx.cs" Inherits="ShiptalkWeb.UserRegistration"
    Title="SHIPtalk User Registration" %>
<%@ Import Namespace="ShiptalkCommon" %>
<%@ Register Assembly="Lanap.BotDetect" Namespace="Lanap.BotDetect" TagPrefix="BotDetect" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
    function HandleDescriptorlist(SelectedDescriptorID)
    {

    	var descClientId = '<%= cblDescriptors.ClientID %>';
        
  		var NPRReadOnlyChecked;
      	var SHIPReadOnlyChecked;

      	var SelectedObject;

      	if ( document.getElementById(descClientId) != null && document.getElementById(descClientId).childNodes != null)
        {

      	   	for (var i=0; i < document.getElementById(descClientId).getElementsByTagName("span").length; i++)
      	   	{
         		var CustomValue = document.getElementById(descClientId).getElementsByTagName("span")[i].getAttribute("CustomValue");
                
				if(CustomValue != null)
				{

					switch(CustomValue)
					{
						//NPR Read Only
						case "6":
							NPRReadOnlyChecked = document.getElementById(descClientId).getElementsByTagName("span")[i].firstChild;
							break;
						//SHIP Read Only
						case "7":
							SHIPReadOnlyChecked = document.getElementById(descClientId).getElementsByTagName("span")[i].firstChild;
							break;
						default:
							continue;

					}

					if(SelectedDescriptorID == CustomValue)
						SelectedObject = document.getElementById(descClientId).getElementsByTagName("span")[i].firstChild;
         		}

			}


			switch(SelectedDescriptorID)
			{
				case 1:
				case 2:
				case 3:
				case 4:
					if(SelectedObject != null)
					{
					    var ItemChecked = SelectedObject.checked;
					    if(ItemChecked)
					    {
						    NPRReadOnlyChecked.checked = false;
						    SHIPReadOnlyChecked.enabled = false;
					    }
					    else
					    {
						    //NPRReadOnlyChecked.checked = false;
						    SHIPReadOnlyChecked.enabled = true;
    				    }
    				}
    				else
    				{
    				    if(NPRReadOnlyChecked != null)
    				        NPRReadOnlyChecked.checked = false;
    				    
    				    if(SHIPReadOnlyChecked != null)
    				        SHIPReadOnlyChecked.checked = false;
    				}
					break;
				case 6:
					for (var i=0; i < document.getElementById(descClientId).getElementsByTagName("span").length; i++)
					{
         				var CurrentValue = document.getElementById(descClientId).getElementsByTagName("span")[i].getAttribute("CustomValue");
         				if((CurrentValue != 6) && (CurrentValue != 7))
         				{
         					document.getElementById(descClientId).getElementsByTagName("span")[i].firstChild.checked = false;
         					if(NPRReadOnlyChecked.checked)
         					{
         						document.getElementById(descClientId).getElementsByTagName("span")[i].firstChild.disabled = true;
         					}
         					else
         					{
         						document.getElementById(descClientId).getElementsByTagName("span")[i].firstChild.disabled = false;
         					}

         				}
         			}
         			break;
				case 7:
					for (var i=0; i < document.getElementById(descClientId).getElementsByTagName("span").length; i++)
					{
						var CurrentValue = document.getElementById(descClientId).getElementsByTagName("span")[i].getAttribute("CustomValue");
         				if(CurrentValue != 7)
         				{
         					document.getElementById(descClientId).getElementsByTagName("span")[i].firstChild.checked = false;
         					if(SHIPReadOnlyChecked.checked)
         					{
         						document.getElementById(descClientId).getElementsByTagName("span")[i].firstChild.disabled = true;
         					}
         					else
         					{
         						document.getElementById(descClientId).getElementsByTagName("span")[i].firstChild.disabled = false;
         					}

         				}
         			}
         			break;
			}

        }
    }

    </script>
      <script language="javascript"  type="text/javascript">
           var UserNameFieldName = '<%= Email.ClientID %>'
           var PasswordFieldName = '<%= Password.ClientID %>'
           var cPasswordFieldName = '<%= ConfirmPassword.ClientID %>'
           var elem1 = document.getElementById(UserNameFieldName);
           var elem2 = document.getElementById(PasswordFieldName);
           var elem3 = document.getElementById(cPasswordFieldName);
           if (elem1 != null) elem1.setAttribute("autocomplete", "off");
           if (elem2 != null) elem2.setAttribute("autocomplete", "off");
           if (elem3 != null) elem3.setAttribute("autocomplete", "off"); 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontent">
        <div class="dv3col">
            <%--<asp:Panel ID="OldShipUserLoginPanel" runat="server" Visible="false">
                <div style="float: right">
                    <h5>
                        <asp:HyperLink ID="OldShipUserLink" runat="server" NavigateUrl="~/OldShipUserLogin.aspx">Old www.SHIPtalk.org User? Click here to login</asp:HyperLink></h5>
                    <br />
                </div>
            </asp:Panel>--%>
            <h1>
                <asp:Literal ID="colTitle" runat="server" Text="Register!" EnableViewState="false"></asp:Literal>
            </h1>
            <div id="dv3colDefaultDescription" runat="server">
                User registration for new NPR users. Reference Dennis Nalty User Registration PPs
                and related documents for guidance.<br />
                <br />
                (<span class="required">*</span> = required field)
            </div>
            <asp:Literal ID="dv3colSuccessMessage" runat="server" Visible="false" EnableViewState="false"
                Text="You have successfully submitted your registration form. We will be contacting you at the email address you provided during registration. Please follow the instructions in the email to verify your registration request and submit it for approval
.">         </asp:Literal>
            <asp:Literal ID="OldShipInfoAvailableMesg" runat="server" Visible="false" Text="Your old SHIPtalk.org information has been prepopulated for certain fields.">         
            </asp:Literal>
            <br />
            <asp:Panel ID="RegisterFormPanel" runat="server" DefaultButton="btnSubmit">
                <div id="dv3colFormContent" runat="server">
                    <table class="formTable">
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <div id="LoadingImageDiv" style="display: none">
                                    Please wait...
                                    <asp:Image ID="loadingimage" runat="server" AlternateText="Loading data image" ImageUrl="~/images/ajax-loader.gif" />
                                </div>
                            </td>
                        </tr>
                        <asp:PlaceHolder ID="plhMessage" runat="server" Visible="false" EnableViewState="false">
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false"></asp:Label>
                                </td>
                            </tr>
                        </asp:PlaceHolder>
                        <tr>
                            <td class="tdFormRequiredLabel">
                                <asp:Label ID="Label4" runat="server" CssClass="required" Text="*"></asp:Label>
                            </td>
                            <td class="tdFormLabel">
                                <asp:Label ID="RoleLabel" runat="server" Text="Role" AssociatedControlID="ddlRoles"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRoles" runat="server" Width="286px" CssClass="dropdown1wm"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged" ValidationGroup="RegisterForm">
                                </asp:DropDownList>
                                <%--<asp:RangeValidator ID="RoleRequired" runat="server" ErrorMessage="<br /> Role must be selected"
				                                    MinimumValue="01" MaximumValue="999" SetFocusOnError="True" ControlToValidate="ddlRoles"
				                                    ValidationGroup="RegisterForm" Display="Dynamic"></asp:RangeValidator>--%>
                                <asp:RequiredFieldValidator ID="RoleRequired" runat="server" ErrorMessage="<br />The Role must be selected"
                                    Display="Dynamic" ControlToValidate="ddlRoles" SetFocusOnError="true" ValidationGroup="RegisterForm"
                                    InitialValue="Select a Role"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <asp:PlaceHolder ID="plhRoleDescription" runat="server" EnableViewState="true" Visible="false">
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <div style="width: 280px; background: #F2F2F2; color: #000; padding: 3px 0 8px 4px;">
                                        <span style="font-weight: normal; font-family: Arial; font-size: 12px;">Role description:
                                            <asp:Label ID="lblRoleDescription" runat="server" Text=""></asp:Label></span>
                                    </div>
                                </td>
                            </tr>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="plhIsShipDirector" runat="server" EnableViewState="true" Visible="false">
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <div style="border: solid 0px #000; width: 286px;">
                                        <asp:CheckBox ID="chBoxIsShipDirector" runat="server" Text="Check this box, if State SHIP Director"
                                            AutoPostBack="true" OnCheckedChanged="OnShipDirectorCheckboxAction" ValidationGroup="RegisterForm" />
                                    </div>
                                </td>
                            </tr>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="plhCMSRegion" runat="server">
                            <tr>
                                <td>
                                    <asp:Label ID="Label7" runat="server" CssClass="required" Text="*"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="CMSRegionLabel" runat="server" Text="CMS Region" AssociatedControlID="ddlCMSRegion"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCMSRegion" runat="server" Width="286px" CssClass="dropdown2wm"
                                        ValidationGroup="RegisterForm">
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="CMSRegionSelection" runat="server" ErrorMessage="<br />The CMS Region must be selected"
                                        MinimumValue="01" MaximumValue="999" SetFocusOnError="True" ControlToValidate="ddlCMSRegion"
                                        ValidationGroup="RegisterForm" Display="Dynamic"></asp:RangeValidator>
                                </td>
                            </tr>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="divStates" runat="server">
                            <tr>
                                <td>
                                    <asp:Label ID="Label44" runat="server" CssClass="required" Text="*"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="StatesLabel" runat="server" Text="State" AssociatedControlID="ddlStates"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStates" runat="server" Width="286px" CssClass="dropdown2wm"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlStates_SelectedIndexChanged" ValidationGroup="RegisterForm">
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="<br />The State must be selected"
                                        MinimumValue="01" MaximumValue="999" SetFocusOnError="True" ControlToValidate="ddlStates"
                                        ValidationGroup="RegisterForm" Display="Dynamic"></asp:RangeValidator>
                                </td>
                            </tr>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="plhSubStateRegion" runat="server">
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" CssClass="required" Text="*"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="SubStateRegionLabel" runat="server" Text="Sub State Region" AssociatedControlID="ddlSubStateRegion"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSubStateRegion" runat="server" Width="286px" CssClass="dropdown2wm"
                                        ValidationGroup="RegisterForm">
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="SubStateRegionRequired" runat="server" ErrorMessage="<br />The Sub State Region must be selected"
                                        MinimumValue="01" MaximumValue="9999" SetFocusOnError="True" ControlToValidate="ddlSubStateRegion"
                                        ValidationGroup="RegisterForm" Display="Dynamic"></asp:RangeValidator>
                                </td>
                            </tr>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="plhAgencies" runat="server">
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" CssClass="required" Text="*"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="AgencyLabel" runat="server" Text="Agency" AssociatedControlID="ddlAgency"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlAgency" runat="server" Width="286px" CssClass="dropdown2wm"
                                        ValidationGroup="RegisterForm">
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="AgencyRequired" runat="server" ErrorMessage="<br />The Agency must be selected"
                                        MinimumValue="01" MaximumValue="99999" SetFocusOnError="True" ControlToValidate="ddlAgency"
                                        ValidationGroup="RegisterForm" Display="Dynamic"></asp:RangeValidator>
                                </td>
                            </tr>
                        </asp:PlaceHolder>
                        <!-- begin text fields -->
                        <tr>
                            <td>
                                <asp:Label ID="Label10" runat="server" CssClass="required" Text="*"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="FirstNameLabel" runat="server" Text="First Name" AssociatedControlID="FirstName"></asp:Label>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="FirstNameRequired" CssClass="validationMessage" runat="server"
                                    ErrorMessage="First name is required<br/>" Display="Dynamic" SetFocusOnError="True"
                                    ControlToValidate="FirstName" ValidationGroup="RegisterForm"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="FirstNameValidator" CssClass="validationMessage"
                                    runat="server" ErrorMessage="Only alphabets, apostrophe, hyphens allowed; maximum 50 characters.<br/>"
                                    Display="Dynamic" ValidationGroup="RegisterForm" SetFocusOnError="True" ControlToValidate="FirstName"
                                    ValidationExpression="^[a-zA-Z'\-.\s]{1,50}$"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="FirstName" CssClass="textfield3" runat="server" ValidationGroup="RegisterForm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="MiddleNameLabel" runat="server" Text="Middle Name" AssociatedControlID="MiddleName"></asp:Label>
                            </td>
                            <td>
                                <asp:RegularExpressionValidator ID="MiddleNameValidator" CssClass="validationMessage"
                                    runat="server" ErrorMessage="Only alphabets, apostrophe, hyphens allowed; maximum 50 characters.<br/>"
                                    Display="Dynamic" ValidationGroup="RegisterForm" SetFocusOnError="True" ControlToValidate="MiddleName"
                                    ValidationExpression="^[a-zA-Z'\-.\s]{1,50}$"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="MiddleName" CssClass="textfield3" runat="server" ValidationGroup="RegisterForm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" CssClass="required" Text="*"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LastNameLabel" runat="server" Text="Last Name" AssociatedControlID="LastName"></asp:Label>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="LastNameRequired" CssClass="validationMessage" runat="server"
                                    ErrorMessage="Last name is required&lt;br /&gt;" Display="Dynamic" SetFocusOnError="True"
                                    ControlToValidate="LastName" ValidationGroup="RegisterForm"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="LastNameValidator" CssClass="validationMessage"
                                    runat="server" ErrorMessage="Only alphabets, apostrophe, hyphens allowed; maximum 50 characters.<br/>"
                                    Display="Dynamic" ValidationGroup="RegisterForm" SetFocusOnError="True" ControlToValidate="LastName"
                                    ValidationExpression="^[a-zA-Z'\-.\s]{1,50}$"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="LastName" CssClass="textfield3" runat="server" ValidationGroup="RegisterForm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="NickNameLabel" runat="server" Text="Nickname" AssociatedControlID="NickName"></asp:Label>
                            </td>
                            <td>
                                <asp:RegularExpressionValidator ID="NickNameValidator" CssClass="validationMessage"
                                    runat="server" ErrorMessage="Only alphabets, apostrophe, hyphens allowed; maximum 50 characters.<br/>"
                                    Display="Dynamic" ValidationGroup="RegisterForm" SetFocusOnError="True" ControlToValidate="NickName"
                                    ValidationExpression="^[a-zA-Z'\-.\s]{1,50}$"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="NickName" CssClass="textfield3" runat="server" ValidationGroup="RegisterForm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="SuffixLabel" runat="server" Text="Suffix" AssociatedControlID="Suffix"></asp:Label>
                            </td>
                            <td>
                                <asp:RegularExpressionValidator ID="SuffixValidator" CssClass="validationMessage"
                                    runat="server" ErrorMessage="<br />Only alphabets, numbers, hyphens allowed; maximum 20 characters.<br/>"
                                    Display="Dynamic" ValidationGroup="RegisterForm" SetFocusOnError="True" ControlToValidate="Suffix"
                                    ValidationExpression="^[a-zA-Z\-.\s]{1,20}$"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="Suffix" CssClass="textfield3" runat="server" ValidationGroup="RegisterForm"></asp:TextBox>
                                <cc1:TextBoxWatermarkExtender ID="tbwe1" runat="server" TargetControlID="Suffix"
                                    WatermarkText="e.g., Jr." WatermarkCssClass="textfield3wm" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="HonorificsLabel" runat="server" Text="Honorifics" AssociatedControlID="Honorifics"></asp:Label>
                            </td>
                            <td>
                                <asp:RegularExpressionValidator ID="HonorificsValidator" CssClass="validationMessage"
                                    runat="server" ErrorMessage="<br />Only alphabets, numbers, hyphens allowed; maximum 20 characters.<br/>"
                                    Display="Dynamic" ValidationGroup="RegisterForm" SetFocusOnError="True" ControlToValidate="Honorifics"
                                    ValidationExpression="^[a-zA-Z\-.\s]{1,20}$"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="Honorifics" CssClass="textfield3" runat="server" ValidationGroup="RegisterForm"></asp:TextBox>
                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="Honorifics"
                                    WatermarkText="e.g., MSW, MD" WatermarkCssClass="textfield3wm" />
                            </td>
                        </tr>
                    </table>
                    <!-- LOGIN ROWS -->
                    <div class="formAltRow">
                        <table class="formTable">
                            <tr>
                                <td class="tdFormRequiredLabel">
                                    <asp:Label ID="Label2" runat="server" CssClass="required" Text="*"></asp:Label>
                                </td>
                                <td class="tdFormLabel">
                                    <asp:Label ID="PrimaryEmailLabel" runat="server" Text="Primary Email" AssociatedControlID="Email"></asp:Label>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="EmailRequired" CssClass="validationMessage" runat="server"
                                        ErrorMessage="Primary email address is required<br/>" Display="Dynamic" SetFocusOnError="True"
                                        ControlToValidate="Email" ValidationGroup="RegisterForm"></asp:RequiredFieldValidator>
                                    <%--<asp:RegularExpressionValidator ID="EmailFmtValidate" CssClass="validationMessage"
                                        runat="server" ErrorMessage="Primary email is not in a valid format<br/>" ValidationExpression='<%# ShiptalkCommon.ConfigUtil.EmailValidationRegex %>'
                                        SetFocusOnError="True" ControlToValidate="Email" Display="Dynamic" ValidationGroup="RegisterForm"></asp:RegularExpressionValidator>
                                    --%><asp:TextBox ID="Email" CssClass="textfield3" runat="server" ValidationGroup="RegisterForm"></asp:TextBox>
                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="Email"
                                        WatermarkText="Primary Email will be your login Username" WatermarkCssClass="textfield3wm" />
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top;">
                                    <asp:Label ID="Label8" runat="server" CssClass="required" Text="*"></asp:Label>
                                </td>
                                <td style="vertical-align: top;">
                                    <asp:Label ID="PasswordLabel" runat="server" Text="Password" AssociatedControlID="Password"></asp:Label>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" CssClass="validationMessage" runat="server"
                                        ErrorMessage="Password is required<br/>" Display="Dynamic" SetFocusOnError="True"
                                        ControlToValidate="Password" ValidationGroup="RegisterForm" ></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="PasswordValidate" CssClass="validationMessage"
                                        runat="server" ErrorMessage="Password entered does not meet minimum requirements&lt;br /&gt;"
    ValidationExpression="^.*(?=^.{8,30}$)(?=.*\d)(?=.*[A-Z])(?=.*[\&quot;!#$%&'()*+,-./:;<=>?@[\]^_`{|}~]).*$"                                        SetFocusOnError="True" ControlToValidate="Password" Display="Dynamic" ValidationGroup="RegisterForm"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="Password" CssClass="textfield3" runat="server" TextMode="Password"
                                        ValidationGroup="RegisterForm" AutoCompleteType="Disabled"></asp:TextBox>
                                    <div style="font-size: 11px;">
                                        Your password must be between 8 to 30 characters and must contain at least one upper case letter, at least one digit and at least one special character.                                    
                                       </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label12" runat="server" CssClass="required" Text="*"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="ConfirmPasswordLabel" runat="server" Text="Confirm Password" AssociatedControlID="ConfirmPassword"></asp:Label>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" CssClass="validationMessage"
                                        runat="server" ErrorMessage="Please retype your password<br/>" Display="Dynamic"
                                        SetFocusOnError="True" ControlToValidate="ConfirmPassword" ValidationGroup="RegisterForm"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="ComparePassword" CssClass="validationMessage" runat="server"
                                        ErrorMessage="Password and Re-type password must match&lt;br /&gt;" SetFocusOnError="True"
                                        ControlToValidate="ConfirmPassword" ControlToCompare="Password" Display="Dynamic"
                                        ValidationGroup="RegisterForm"></asp:CompareValidator>
                                    <asp:TextBox ID="ConfirmPassword" CssClass="textfield3" runat="server" TextMode="Password"
                                        ValidationGroup="RegisterForm" AutoCompleteType="Disabled"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table class="formTable">
                        <tr>
                            <td class="tdFormRequiredLabel">
                            </td>
                            <td class="tdFormLabel">
                                <asp:Label ID="SecondaryEmailLabel" runat="server" Text="Secondary Email" AssociatedControlID="SecondaryEmail"></asp:Label>
                            </td>
                            <td>
                               <%-- <asp:RegularExpressionValidator ID="SecondaryEmailFmtValidate" CssClass="validationMessage"
                                    runat="server" ErrorMessage="Secondary email is not in a valid format<br/>" ValidationExpression='<%# ShiptalkCommon.ConfigUtil.EmailValidationRegex %>'
                                    SetFocusOnError="True" ControlToValidate="SecondaryEmail" Display="Dynamic" ValidationGroup="RegisterForm" EnableViewState="false"></asp:RegularExpressionValidator>
                              --%>  <asp:TextBox ID="SecondaryEmail" CssClass="textfield3" runat="server" ValidationGroup="RegisterForm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" CssClass="required" Text="*"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="PrimaryPhoneLabel" runat="server" Text="Primary Phone" AssociatedControlID="PrimaryPhone"></asp:Label>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="PrimaryPhoneRequired" CssClass="validationMessage"
                                    runat="server" ErrorMessage="Primary phone is required<br/>" Display="Dynamic"
                                    SetFocusOnError="True" ControlToValidate="PrimaryPhone" ValidationGroup="RegisterForm"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="PrimaryPhoneValidate" CssClass="validationMessage"
                                    runat="server" ErrorMessage="Primary phone is not in a valid format<br/>" ValidationExpression="\(?\d{3}\)?[-\s.]?\d{3}[-.]\d{4}( x\d{0,5})?"
                                    SetFocusOnError="True" ControlToValidate="PrimaryPhone" Display="Dynamic" ValidationGroup="RegisterForm"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="PrimaryPhone" CssClass="textfield3" runat="server" ValidationGroup="RegisterForm"></asp:TextBox>
                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="PrimaryPhone"
                                    WatermarkText="e.g., 202-555-1234 x1234" WatermarkCssClass="textfield3wm" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="SecondaryPhoneLabel" runat="server" Text="Secondary Phone" AssociatedControlID="SecondaryPhone"></asp:Label>
                            </td>
                            <td>
                                <asp:RegularExpressionValidator ID="SecondaryPhoneFmtValidate" CssClass="validationMessage"
                                    runat="server" ErrorMessage="Secondary phone is not in a valid format<br/>" ValidationExpression="\(?\d{3}\)?[-\s.]?\d{3}[-.]\d{4}( x\d{0,5})?"
                                    SetFocusOnError="True" ControlToValidate="SecondaryPhone" Display="Dynamic" ValidationGroup="RegisterForm"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="SecondaryPhone" CssClass="textfield3" runat="server" ValidationGroup="RegisterForm"></asp:TextBox>
                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="SecondaryPhone"
                                    WatermarkText="e.g., 202-555-1234 x1234" WatermarkCssClass="textfield3wm" />
                            </td>
                        </tr>
                    </table>
                    <!-- Need to use the following div to SHOW/HIDE cblDescriptors, not the checkbox control -->
                    <asp:Panel ID="dvCblDescriptors" runat="server" CssClass="formAltRow">
                        <table class="formTable">
                            <tr>
                                <td class="tdFormRequiredLabel">
                                </td>
                                <td class="tdFormLabel">
                                    <asp:Label ID="Label13" runat="server" Text="Task Functions and Access- Authorizations (Check all that apply)"
                                        AssociatedControlID="cblDescriptors"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBoxList ID="cblDescriptors" runat="server" ValidationGroup="RegisterForm">
                                        <asp:ListItem Value="1" CustomValue="1" onClick="HandleDescriptorlist(1)">Counselor</asp:ListItem>
                                        <asp:ListItem Value="2" CustomValue="2" onClick="HandleDescriptorlist(2)">Data Submitter</asp:ListItem>
                                        <asp:ListItem Value="3" CustomValue="3" onClick="HandleDescriptorlist(3)">Public and Media Staff</asp:ListItem>
                                        <asp:ListItem Value="4" CustomValue="4" onClick="HandleDescriptorlist(4)">Data Editor/Reviewer</asp:ListItem>
                                        <asp:ListItem Value="6" CustomValue="6" onClick="HandleDescriptorlist(6)">Other Staff (NPR Read Only)</asp:ListItem>
                                        <asp:ListItem Value="7" CustomValue="7" onClick="HandleDescriptorlist(7)">Other Staff (SHIP Read Only)</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table class="formTable">
                        <tr>
                            <td>
                            </td>
                            <td class="tdFormLabel">
                            </td>
                            <td>
                                <BotDetect:Captcha ID="ctrlCaptcha" runat="server" SoundEnabled="true" SoundIconAltText="Listen to image text" />
                                <%--onprerender="ctrlCaptcha_PreRender" />--%>
                                <asp:Panel ID="Panel1" runat="server">
                                </asp:Panel>
                                <br />
                                <asp:Label ID="CaptchaLabel" runat="server" AssociatedControlID="CaptchaText">Enter the text from the image or sound above</asp:Label>
                                <br />
                                <asp:CustomValidator ID="cvCustomValidator" CssClass="validationMessage" runat="server"
                                    ErrorMessage="" Display="Dynamic" ValidationGroup="RegisterForm"></asp:CustomValidator>
                                <asp:RequiredFieldValidator ID="CaptchaTextRequired" CssClass="validationMessage"
                                    runat="server" SetFocusOnError="True" ControlToValidate="CaptchaText" Display="Dynamic"
                                    ErrorMessage="Image text is required<br/>" ToolTip="Image text is required."
                                    ValidationGroup="RegisterForm">Enter the text from the image or sound above<br /></asp:RequiredFieldValidator>
                                <asp:TextBox ID="CaptchaText" CssClass="textfield3" runat="server" EnableViewState="false"
                                    ValidationGroup="RegisterForm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="btnSubmit" runat="server" Text="Register!" CssClass="formbutton1a"
                                    ValidationGroup="RegisterForm" UseSubmitBehavior="true" OnClick="RegisterUserCommand"
                                    CommandName="Submit" CommandArgument="RegisterForm" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
