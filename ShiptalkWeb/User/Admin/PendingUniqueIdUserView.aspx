<%@ Page Language="C#" MasterPageFile="~/ShiptalkWeb.Master" AutoEventWireup="true"
    CodeBehind="PendingUniqueIdUserView.aspx.cs" Inherits="ShiptalkWeb.PendingUniqueIdUserView"
    Title="Pending Registration - User Information" %>

<%@ Import Namespace="ShiptalkWeb" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects" %>
<%@ Import Namespace="ShiptalkWeb.Routing" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls"
    TagPrefix="pp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .InfoValueFont
        {
            font-size: 12px;
            font-family: Arial;
            font-weight: bold;
            word-wrap: break-word;
        }
        .Descriptors
        {
            padding-left: 0px;
            margin-left: 2px;
            list-style-type: circle;
            font-weight: bold;
            font-family: Arial;
            font-size: 12px;
        }
    </style>

    <script language="javascript" type="text/javascript">
    function HandleDescriptorlist(DescriptorRootID, SelectedDescriptorID)
    {

    	var descClientId = DescriptorRootID;
        
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontent">
        <div class="dv3col" runat="server" id="MainPanel">
            <div style="text-align: left">
                <h1>
                     <asp:Label ID="lblTitle" runat="server" Text="Generate CMS SHIP Unique ID for User"
                        EnableViewState="false"></asp:Label>
                     
                </h1>
            </div>
            <span class="smaller">
                <asp:Literal ID="dv3colMessage" runat="server" Text="" EnableViewState="true"></asp:Literal>
            </span>
            <br />
            <br />
            <div style="text-align: right">
                <a id="A1" runat="server" href="~/user/admin/pendinguniqueids">Back to pending CMS SHIP Unique ID Requests</a>
            </div>
            <asp:FormView runat="server" ID="formView" DataSourceID="dataSourceUserView" Width="100%"
                DefaultMode="Edit" DataKeyNames="UserId" OnPreRender="formView_PreRender">
                <EditItemTemplate>
                    <asp:Panel ID="RegistrationDataPanel" runat="server" DefaultButton="btnSubmit">
                        <div id="dv3colFormContent" runat="server" style="margin-top: 10px; width: 615px;
                            border-top: solid 2px #eee;">
                            <table>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td colspan="3" style="margin-bottom: 2px; margin-bottom: 0px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <asp:PlaceHolder ID="plhMessage" runat="server" Visible="false" EnableViewState="false">
                                    <tr>
                                        <td colspan="4">
                                            <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false" CssClass="required"></asp:Label>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="RoleLabel" runat="server" Text="Role" AssociatedControlID="RoleSelected"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px; padding: 2px;">
                                        <asp:Label ID="RoleSelected" runat="server" Text='<%#  Eval("RoleTitle").EncodeHtml() %>' CssClass="InfoValueFont"
                                            Width="286px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="Label1" runat="server" Text="Role description" AssociatedControlID="lblRoleDescription"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <div style="width: 280px;">
                                            <span style="font-weight: normal; font-family: Arial; font-size: 12px;">
                                                <asp:Label ID="lblRoleDescription" runat="server" Text='<%# Eval("RoleDescription").EncodeHtml() %>'></asp:Label></span>
                                        </div>
                                    </td>
                                </tr>
                                <asp:PlaceHolder ID="plhIsShipDirector" runat="server" Visible='<%# Eval("IsShipDirector") %>'>
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 305px">
                                            <div style="border: solid 0px #000; padding: 4px; background-color: #FFCC00; width: 232px">
                                                <asp:Label ID="ShipDirectorAccessRequested" runat="server" Text="SHIP Director"
                                                    Font-Bold="true"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="plhCMSRegion" runat="server" Visible='<%# Eval("IsUserCMSRegionalScope") %>'>
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            <asp:Label ID="CMSRegionLabel" runat="server" Text="CMS Region" AssociatedControlID="CMSRegionSelected"></asp:Label>
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 305px">
                                            <asp:Label ID="CMSRegionSelected" runat="server" Text='<%# Eval("CMSRegionName").EncodeHtml().ToCamelCasing() %>'
                                                CssClass="InfoValueFont" Width="286px"></asp:Label>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="divStates" runat="server" Visible='<%# !((bool)Eval("IsUserCMSRegionalScope") || (bool)Eval("IsUserCMSScope")) %>'>
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            <asp:Label ID="StatesLabel" runat="server" Text="State" AssociatedControlID="StateSelected"></asp:Label>
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 305px">
                                            <asp:Label ID="StateSelected" runat="server" Text='<%# Eval("StateName").EncodeHtml() %>' CssClass="InfoValueFont"
                                                Width="286px"></asp:Label>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="plhSubStateRegion" runat="server" Visible='<%# Eval("IsUserSubStateRegionalScope") %>'>
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            <asp:Label ID="SubStateRegionLabel" runat="server" Text="Sub State Region" AssociatedControlID="SubStateSelected"></asp:Label>
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 305px">
                                            <asp:Label ID="SubStateSelected" runat="server" Text='<%# Eval("DefaultSubStateName").EncodeHtml().ToCamelCasing() %>'
                                                CssClass="InfoValueFont" Width="286px"></asp:Label>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="plhAgencies" runat="server" Visible='<%# Eval("IsUserAgencyScope") %>'>
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            <asp:Label ID="AgencyLabel" runat="server" Text="Agency" AssociatedControlID="AgencySelected"></asp:Label>
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 305px">
                                            <asp:Label ID="AgencySelected" runat="server" Text='<%# Eval("DefaultAgencyName").EncodeHtml().ToCamelCasing() %>'
                                                CssClass="InfoValueFont" Width="286px"></asp:Label>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="FirstNameLabel" runat="server" Text="First Name" AssociatedControlID="FirstName"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:Label ID="FirstName" runat="server" Text='<%# Eval("FirstName").EncodeHtml().ToCamelCasing() %>'
                                            CssClass="InfoValueFont" Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                        &nbsp;
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="MiddleNameLabel" runat="server" Text="Middle Name" AssociatedControlID="MiddleName"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:Label ID="MiddleName" runat="server" Text='<%# Eval("MiddleName").EncodeHtml().ToCamelCasing() %>'
                                            CssClass="InfoValueFont" Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="LastNameLabel" runat="server" Text="Last Name" AssociatedControlID="LastName"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:Label ID="LastName" runat="server" Text='<%# Eval("LastName").EncodeHtml().ToCamelCasing() %>'
                                            CssClass="InfoValueFont" Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="NickNameLabel" runat="server" Text="Nick Name" AssociatedControlID="NickName"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:Label ID="NickName" runat="server" Text='<%# Eval("NickName").EncodeHtml().ToCamelCasing() %>'
                                            CssClass="InfoValueFont" Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="SuffixLabel" runat="server" Text="Suffix" AssociatedControlID="Suffix"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:Label ID="Suffix" runat="server" Text='<%# Eval("Suffix").EncodeHtml().ToCamelCasing() %>'
                                            CssClass="InfoValueFont" Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="HonorificsLabel" runat="server" Text="Honorifics" AssociatedControlID="Honorifics"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:Label ID="Honorifics" runat="server" Text='<%# Eval("Honorifics").EncodeHtml().ToCamelCasing() %>'
                                            CssClass="InfoValueFont" Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="PrimaryEmailLabel" runat="server" Text="Primary Email" AssociatedControlID="Email"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:Label ID="Email" runat="server" Text='<%# Eval("PrimaryEmail").EncodeHtml() %>'
                                            CssClass="InfoValueFont" Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="SecondaryEmailLabel" runat="server" Text="Secondary Email" AssociatedControlID="SecondaryEmail"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:Label ID="SecondaryEmail" runat="server" Text='<%# Eval("SecondaryEmail").EncodeHtml() %>'
                                            CssClass="InfoValueFont" Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="PrimaryPhoneLabel" runat="server" Text="Primary Phone" AssociatedControlID="PrimaryPhone"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:Label ID="PrimaryPhone" runat="server" Text='<%# Eval("PrimaryPhone").EncodeHtml() %>'
                                            CssClass="InfoValueFont" Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="SecondaryPhoneLabel" runat="server" Text="Secondary Phone" AssociatedControlID="SecondaryPhone"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:Label ID="SecondaryPhone" runat="server" Text='<%# Eval("SecondaryPhone").EncodeHtml() %>'
                                            CssClass="InfoValueFont" Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <div visible='<%# Eval("HasDescriptors").EncodeHtml() %>'>
                                            Task Functions and Access Authorizations
                                        </div>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <div visible='<%# Eval("HasDescriptors").EncodeHtml() %>'>
                                            <div style="width: 256px; background: #fff; color: #000; padding: 3px 0 8px 4px;">
                                                <asp:CheckBoxList ID="Descriptors" runat="server" CssClass="Descriptors" Enabled="false">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                
                                <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            CMS SHIP Unique ID
                                        </td>
                                     <td style="width: 4px">
                                        &nbsp;
                                    </td>
                              <td style="width: 305px">
                                            <asp:Label ID="Label9" runat="server" Text='[Pending Approval]' Visible='<%# UniqueIDData.IsPendingApproval %>' CssClass="InfoValueFont" Width="256px"></asp:Label>
                                            <asp:Label ID="Label7" runat="server" Text='<%# UniqueIDData.UniqueID %>' Visible='<%# UniqueIDData.IsApproved %>' CssClass="InfoValueFont" Width="256px"></asp:Label>
                                        </td>
                                    </tr>
                                <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            Is CMS SHIP Unique ID Revoked
                                        </td>
                                     <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                            <asp:Label ID="Label2" runat="server" Text='<%# UniqueIDData.IsMedicareUniqueIdRevoked %>'  CssClass="InfoValueFont" Width="256px"></asp:Label>
                                        </td>
                                 </tr>
                                    
                                <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            CMS SHIP Unique ID Generated Date
                                        </td>
                                     <td style="width: 4px">
                                        &nbsp;
                                    </td>
                              <td style="width: 305px">
                                            <asp:Label ID="Label3" runat="server" Text='<%# UniqueIDData.ApprovedDate %>' Visible='<%# UniqueIDData.IsApproved %>' CssClass="InfoValueFont" Width="256px"></asp:Label>
                                        </td>
                                  </tr>
                                <asp:PlaceHolder ID="plhApproveDeny" runat="server" Visible="true">
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;&nbsp;
                                        </td>
                                        <td style="width: 305px;">
                                            <table style="width: 256px;">
                                                <tr>
                                                    <td style="width: 160px;" align="left">
                                                        <asp:Button ID="btnSubmit" runat="server" Text="Approve and Generate &#10; CMS SHIP Unique ID" CssClass="formbutton4" Width="160px"  visible='<%# !UniqueIDData.IsApproved %>'
                                                            OnCommand="PostBackUserCommand" UseSubmitBehavior="true" CommandName='<%# PostBackCommands.APPROVE.Description() %>' />
                                                    </td>
                                                    
                                                     <td style="width: 160px">
                                                        <asp:Button ID="btnDeny" runat="server" Text="Deny Request for &#10; CMS SHIP Unique ID" CssClass="formbutton4" Width="160px" visible='<%# !UniqueIDData.IsApproved %>'
                                                            OnCommand="PostBackUserCommand" UseSubmitBehavior="true" CommandName='<%# PostBackCommands.DENY.Description() %>' />
                                                    </td>

                                                  <td style="width: 160px;" align="left">
                                                        <asp:Button ID="btnRevoke" runat="server" Text="Revoke &#10; CMS SHIP Unique ID" CssClass="formbutton4" Width="160px"  visible='<%# UniqueIDData.IsApproved && !UniqueIDData.IsMedicareUniqueIdRevoked%>'
                                                            OnCommand="PostBackUserCommand" UseSubmitBehavior="true" CommandName='<%# PostBackCommands.REVOKE.Description() %>' />
                                                   </td>
 
                                                  <td style="width: 160px;" align="left">
                                                        <asp:Button ID="btnInvoke" runat="server" Text="Reinstate &#10; CMS SHIP Unique ID" CssClass="formbutton4" Width="160px"  visible='<%# UniqueIDData.IsApproved && UniqueIDData.IsMedicareUniqueIdRevoked%>'
                                                            OnCommand="PostBackUserCommand" UseSubmitBehavior="true" CommandName='<%# PostBackCommands.INVOKE.Description() %>' />
                                                   </td>


                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>

                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;&nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <div style="float: left; margin-left: 0px; padding: 4px">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;&nbsp;
                                    </td>
                                    <td style="width: 305px">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                </EditItemTemplate>
            </asp:FormView>
        </div>
    </div>
    <pp:ObjectContainerDataSource ID="dataSourceUserView" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.UserViewData"
        OnSelecting="dataSourceUserView_Selecting" />
</asp:Content>
