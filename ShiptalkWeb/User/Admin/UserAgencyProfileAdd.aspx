<%@ Page Language="C#" MasterPageFile="~/ShiptalkWeb.Master" AutoEventWireup="true"
    CodeBehind="UserAgencyProfileAdd.aspx.cs" Inherits="ShiptalkWeb.UserAgencyProfileAdd"
    Title="User Agency Profile - Add" %>

<%@ Import Namespace="ShiptalkWeb" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects" %>
<%@ Import Namespace="ShiptalkLogic.BusinessLayer" %>
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
        td .c305
        {
            width: 305px;
            padding-left: 10px;
        }
        .Descriptors
        {
            padding-left: 4px;
            margin-left: 2px;
            list-style-type: circle;
            font-weight: bold;
            font-family: Arial;
            font-size: 12px;
        }
    </style>

    <script language="javascript" type="text/javascript">
		    //This function will toggle display of 'Can Approve user registratoins'.
		    //Also note that the code behind will call this on post back to maintain view.
		    function HandleIsAdminClick(IsAdminClientId, ApproverPanelClientId)
            {
                var chkIsAdmin = document.getElementById(IsAdminClientId);
                if(chkIsAdmin != null)
                {
                        var val = 'none';
                        
                        if(chkIsAdmin.checked == true)
                            val = 'block';
                        
                        var Elem = document.getElementById('divApprover');
                        if(Elem != null)
                        {
                            //set value here                
                            Elem.style.display = val;
                        }
                }
                //cbIsApprover
            }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontent">
        <div class="dv3col" runat="server" id="plhMessage" visible="false" enableviewstate="false">
            <h1>
                <asp:Label ID="lblTitleMessage" runat="server" Text="" EnableViewState="false"></asp:Label></h1>
            <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false" CssClass="required"></asp:Label>
            <br />
            <asp:HyperLink ID="hlBackToEdit" runat="server" Visible="true" EnableViewState="true"
                NavigateUrl='<%# RouteController.UserEdit(UserProfileUserId) %>'>Edit User Profile main page</asp:HyperLink>
        </div>
        <div class="dv3col" runat="server" id="MainPanel">
            <div style="text-align: left">
                <div class="commands">
                    <asp:HyperLink ID="HyperLink2" runat="server" Visible="true" EnableViewState="false"
                        NavigateUrl='<%# RouteController.UserEdit(UserProfileUserId) %>'>Edit User Profile main page</asp:HyperLink>
                    <br />
                    <asp:HyperLink ID="HyperLink1" runat="server" Visible="true" EnableViewState="false"
                        NavigateUrl='<%# RouteController.UserView(UserProfileUserId) %>'>View User Profile main page</asp:HyperLink></div>
                <h2>
                    <asp:Label ID="lblTitle" runat="server" Text='Add User Agency Profile' EnableViewState="false"></asp:Label>
                </h2>
                <span class="smaller">
                    <asp:Literal ID="dv3colMessage" runat="server" Text='<%# "Add admin rights, default settings, task functions and access authorizations, supervisors for " + GetUserFullName.EncodeHtml().ToCamelCasing() + "." %>'
                        EnableViewState="false"></asp:Literal>
                </span>
            </div>
            <asp:FormView runat="server" ID="formView" DataSourceID="dataSourceAgencyUserAdd"
                Width="100%" DefaultMode="Edit" DataKeyNames="Id" OnPreRender="formView_PreRender">
                <EditItemTemplate>
                    <asp:Panel ID="AgencyDataPanel" runat="server" DefaultButton="btnSubmit">
                        <div id="dv3colFormContent" runat="server" style="margin-top: 10px; width: 615px;
                            border-top: solid 2px #eee;">
                            <table>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td class="c305">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="Label1" runat="server" Text="State" ></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td class="c305">
                                        <asp:Label ID="StateName" runat="server" Text='<%# GetStateName %>' CssClass="InfoValueFont"
                                            Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="Label5" runat="server" Text="Name" ></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td class="c305">
                                        <asp:Label ID="FullNameOfUser" runat="server" Text='<%# GetUserFullName.EncodeHtml().ToCamelCasing() %>'
                                            CssClass="InfoValueFont" Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="AgencyNameLabel" runat="server" Text="Agency" AssociatedControlID="ddlAgencies"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td class="c305">
                                        <asp:DropDownList ID="ddlAgencies" runat="server">
                                        </asp:DropDownList>
                                        <asp:RangeValidator ID="AgencyRequired" runat="server" ErrorMessage="<br />An Agency must be selected"
                                            MinimumValue="01" MaximumValue="99999" SetFocusOnError="True" ControlToValidate="ddlAgencies"
                                            Display="Dynamic"></asp:RangeValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="Label8" runat="server" Text="Is Agency Administrator?" AssociatedControlID="cbIsAdmin"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td class="c305">
                                        <asp:CheckBox ID="cbIsAdmin" runat="server" Checked='<%# Bind("IsAdmin") %>' ValidationGroup="UserAgencyProfile"
                                            OnClick='<%# "HandleIsAdminClick(" + IsAdminCheckboxClientId + "," + ApproverPanelClientId + ")" %>' />
                                        <asp:CustomValidator ID="cvIsAdminError" runat="server" ErrorMessage="<br/>You are not authorized to create an admin User account in the chosen agency."
                                            ValidationGroup="UserAgencyProfile" Display="Dynamic" SetFocusOnError="true"></asp:CustomValidator>
                                    </td>
                                </tr>
                                <asp:PlaceHolder ID="plhIsMultiAgencyUser" runat="server" Visible='<%# !IsSingleProfileUser %>'>
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            <asp:Label ID="Label3" runat="server" Text="Is Default Agency?" AssociatedControlID="cbIsDefaultRegion"></asp:Label>
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td class="c305">
                                            <asp:CheckBox ID="cbIsDefaultRegion" runat="server" Checked='<%# Bind("IsDefaultRegion") %>'
                                                ValidationGroup="UserAgencyProfile" />
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <div id="pnlCanApproveUsers" runat="server" visible="true">
                                    <div id="divApproverPanel" style="display: block">
                                        <tr>
                                            <td style="width: 8px;">
                                            </td>
                                            <td style="width: 200px; text-align: right;">
                                                <asp:Label ID="Label6" runat="server" Text="Can approve user registrations?" AssociatedControlID="cbIsApprover"></asp:Label>
                                            </td>
                                            <td style="width: 4px">
                                                &nbsp;
                                            </td>
                                            <td class="c305">
                                                <asp:CheckBox ID="cbIsApprover" runat="server" Checked='<%# Bind("IsApproverDesignate") %>'
                                                    ValidationGroup="UserAgencyProfile" />
                                                <asp:CustomValidator ID="cvIsApproverError" runat="server" ErrorMessage="<br/>The selected role does not qualify the User to be an approver. Approvers must be Administrators."
                                                    ValidationGroup="UserAgencyProfile" Display="Dynamic" SetFocusOnError="true"></asp:CustomValidator>
                                            </td>
                                        </tr>
                                    </div>
                                </div>
                                 <div id="pnlIsSuperDataEditor" runat="server" visible="true">
                                    <div id="div2" style="display: block">
                                        <tr>
                                            <td style="width: 8px;">
                                            </td>
                                            <td style="width: 200px; text-align: right;">
                                                <asp:Label ID="Label4" runat="server" Text="Is super data editor?" AssociatedControlID="cbIsDataSuperEditor"></asp:Label>
                                            </td>
                                            <td style="width: 4px">
                                                &nbsp;
                                            </td>
                                            <td class="c305">
                                                <asp:CheckBox ID="cbIsDataSuperEditor" runat="server" Checked='<%# Bind("IsSuperDataEditor") %>'
                                                    ValidationGroup="UserAgencyProfile" />
                                            </td>
                                        </tr>
                                    </div>
                                </div>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="Label2" runat="server" Text="Task Functions and Access Authorizations"
                                           ></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <div style="width: 256px; background: #F2F2F2; color: #000; padding: 3px 0 8px 0px;
                                            border-top: solid 1px #E6E6E6;">
                                            <asp:CheckBoxList ID="Descriptors" runat="server" CssClass="Descriptors">
                                            </asp:CheckBoxList>
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
                                    <td class="c305">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="formbutton3" Width="110px"
                                            UseSubmitBehavior="true" CommandName="Update" ValidationGroup="UserProfile" OnCommand="UserCommand" />
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
                                    <td class="c305">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                </EditItemTemplate>
            </asp:FormView>
        </div>
    </div>
    <pp:ObjectContainerDataSource ID="dataSourceAgencyUserAdd" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UserRegionalAccessProfile"
        OnSelecting="dataSourceAgencyUserAdd_Selecting" OnUpdated="dataSourceAgencyUserAdd_Updated"
        OnUpdating="dataSourceAgencyUserAdd_Updating" />
</asp:Content>
