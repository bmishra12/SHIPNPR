<%@ Page Language="C#" MasterPageFile="~/ShiptalkWeb.Master" AutoEventWireup="true"
    CodeBehind="UserSubStateProfileView.aspx.cs" Inherits="ShiptalkWeb.UserSubStateProfileView"
    Title="User Profile : Sub State Regional Profile : View" %>

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
        .Descriptors
        {
            padding-left: 10px;
            margin-left: 10px;
            list-style-type: circle;
            font-weight: bold;
            font-family: Arial;
            font-size: 12px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontent">
        <div class="dv3col" runat="server" id="MainPanel">
            <asp:FormView runat="server" ID="formView" DataSourceID="dataSourceSubStateUserView"
                Width="100%">
                <ItemTemplate>
                    <div style="text-align: left">
                        <h2>
                            <asp:Label ID="lblTitle" runat="server" Text='<%# "Sub State Regional profile for " + GetUserFullName %>'
                                EnableViewState="false"></asp:Label>
                        </h2>
                        <span class="smaller">
                            <asp:Literal ID="dv3colMessage" runat="server" Text='<%# "View admin rights, default settings, task functions and access authorizations, supervisors for " + GetUserFullName + " at " + GetSubStateRegionName + "." %>'
                                EnableViewState="false"></asp:Literal>
                        </span>
                    </div>
                    <asp:Panel ID="SubStateDataPanel" runat="server" EnableViewState="false" DefaultButton="btnEdit">
                        <asp:PlaceHolder ID="plhEditDeleteCommands" runat="server" Visible='<%# IsEditAccessAllowed %>'>
                            <div class="commands">
                                <asp:Button runat="server" ID="btnEdit" Text="Edit" ToolTip="Edit this sub state profile."
                                    CssClass="formbutton1" OnCommand="UserCommand" UseSubmitBehavior="true" CommandName="Edit" />
                            </div>
                        </asp:PlaceHolder>
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
                                    <td style="width: 305px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="Label1" runat="server" Text="State" AssociatedControlID="StateName"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:Label ID="StateName" runat="server" Text='<%# GetStateName %>' CssClass="InfoValueFont"
                                            Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="Label5" runat="server" Text="Name" AssociatedControlID="FullNameOfUser"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:Label ID="FullNameOfUser" runat="server" Text='<%# GetUserFullName %>' CssClass="InfoValueFont"
                                            Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="SubStateNameLabel" runat="server" Text="Sub State Region" AssociatedControlID="SubStateName"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:Label ID="SubStateName" runat="server" Text='<%# GetSubStateRegionName %>' CssClass="InfoValueFont"
                                            Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="Label8" runat="server" Text="Grant Access to this Region" AssociatedControlID="AccessStatus"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <span class="InfoValueFont">
                                            <img runat="server" id="AccessStatus" src='<%# this.ResolveUrl("~/images/" + (((bool)Eval("IsActive")) ? "activestatus.png" : "inactivestatus.png"))  %>'>&nbsp;<%# ((bool)Eval("IsActive"))? "Active" : "Inactive" %></img>
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="Label2" runat="server" Text="Role" AssociatedControlID="RoleName"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:Label ID="RoleName" runat="server" Text='<%# GetRoleName %>' CssClass="InfoValueFont"
                                            Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                <asp:PlaceHolder ID="plhIsMultiSubStateUser" runat="server" Visible='<%# (bool)IsMultiSubStateUser %>'>
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            <asp:Label ID="Label3" runat="server" Text="Is Default Sub State" AssociatedControlID="DefaultRegion"></asp:Label>
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 305px">
                                            <asp:Label ID="DefaultRegion" runat="server" Text='<%# IsDefaultRegionText %>' CssClass="InfoValueFont"
                                                Width="256px"></asp:Label>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="plhCanApproveUsers" runat="server" Visible='<%# (bool)Eval("IsAdmin") %>'>
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            <asp:Label ID="Label6" runat="server" Text="Can approve user registrations" AssociatedControlID="RoleName"></asp:Label>
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 305px">
                                            <asp:Label ID="Label7" runat="server" Text='<%# CanApproveUserRegistrations ? "Yes" : "No" %>'
                                                CssClass="InfoValueFont" Width="256px"></asp:Label>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                  <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            <asp:Label ID="Label9" runat="server" Text="Is super data editor?" AssociatedControlID="lblIsSuperDataEditor"></asp:Label>
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 305px">
                                            <asp:Label ID="lblIsSuperDataEditor" runat="server" Text='<%# (bool)Eval("IsSuperDataEditor") ? "Yes" : "No" %>'
                                                CssClass="InfoValueFont" Width="256px"></asp:Label>
                                        </td>
                                    </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="Label4" runat="server" Text="Task Functions and Access Authorizations"
                                            AssociatedControlID="Descriptors"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <div style="width: 256px; background: #F2F2F2; color: #000; padding: 3px 0 8px 4px;
                                            border-top: solid 1px #E6E6E6;">
                                            <asp:BulletedList ID="Descriptors" runat="server" DataSource='<%# GetDescriptorNames %>'
                                                CssClass="Descriptors" BulletStyle="Disc">
                                            </asp:BulletedList>
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
                </ItemTemplate>
            </asp:FormView>
        </div>
    </div>
    <pp:ObjectContainerDataSource ID="dataSourceSubStateUserView" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UserRegionalAccessProfile"
        OnSelecting="dataSourceSubStateUserView_Selecting" />
</asp:Content>
