<%@ Page Language="C#" MasterPageFile="~/ShiptalkWeb.Master" AutoEventWireup="True"
    CodeBehind="PendingEmailChangeVerifications.aspx.cs" Inherits="ShiptalkWeb.PendingEmailChangeVerifications"
    Title="Pending Email Change Verifications" %>

<%@ Import Namespace="ShiptalkWeb" %>
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontent">
        <div class="dv3col">
            <h1>
                Pending Email Change Verifications</h1>
            <span class="smaller">
                <asp:Literal ID="dv3colMessage" runat="server" Text="Displays the list of Users who have changed their Email address, but not yet verified their Email address."
                    EnableViewState="false"></asp:Literal>
            </span>
            
            <div>
                <div id="dv3colFormContent" class="section">
                    <asp:PlaceHolder ID="plhMessage" runat="server" Visible="false" EnableViewState="false">
                        <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false" CssClass="required"></asp:Label>
                    </asp:PlaceHolder>
                    
                    <div style="text-align: center; margin-top: 20px; padding-bottom: 240px;" visible='<%# !ViewDataHasRows %>' runat="server" id="divNoResultsMesage">
                        <asp:Label ID="NoSearchResultsMessage" runat="server" EnableViewState="false"  Visible="false"
                            CssClass="required" Text="At this time, there are no pending email verifications."></asp:Label>
                    </div>
                    <asp:ListView runat="server" ID="listViewPendingEmailVerifications" DataSourceID="dataSourcePendingEmailVerifications"
                        ItemPlaceholderID="placeHolder" DataKeyNames="UserId">
                        <LayoutTemplate>
                            <table class="dataTable">
                                <thead>
                                    <tr>                                         
                                        <th scope="col" style="width: 100px; text-align: left; padding-left: 4px; ">
                                            First Name
                                        </th>
                                         <th scope="col" style="width: 100px; text-align: left; padding-left: 4px; ">
                                            Last Name
                                        </th>
                                        <th scope="col" style="width: 80px; text-align: left; padding-left: 4px; ">
                                            State
                                        </th>
                                        <th scope="col" style="width: 100px; text-align: left; padding-left: 4px;">
                                            Scope
                                        </th>
                                        <th scope="col" style="text-align: left; padding-left: 4px; width: 160px;">
                                            Primary Email
                                        </th>                                       
                                        <th scope="col" style="text-align: left; padding-left: 4px; width: 55px; ">
                                            Select
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr runat="server" id="placeHolder">
                                    </tr>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td scope="row" style="vertical-align: top; ">
                                    <div style="width: 100px; word-wrap: break-word">
                                        <span class="smaller">
                                            <%# Eval("FirstName").EncodeHtml().ToCamelCasing()%>
                                        </span>
                                    </div>
                                </td>
                                <td scope="row" style="vertical-align: top; ">
                                    <div style="width: 100px; word-wrap: break-word">
                                        <span class="smaller">
                                            <%#Eval("LastName").EncodeHtml().ToCamelCasing() %>
                                        </span>
                                    </div>
                                </td>
                                <td scope="row" style="vertical-align: top;">
                                    <div style="width: 80px; word-wrap: break-word">
                                        <span class="smaller">
                                            <%#Eval("StateName").EncodeHtml().ToCamelCasing()%>
                                        </span>
                                    </div>
                                </td>
                                <td scope="row" style="vertical-align: top; ">
                                    <div style="width: 100px; word-wrap: break-word">
                                        <span class="smaller">
                                            <%#Eval("Scope").EncodeHtml().ToCamelCasing()%>
                                        </span>
                                    </div>
                                </td>
                                <td style="vertical-align: top">
                                    <div style="width: 160px; word-wrap: break-word">
                                        <span class="smaller">
                                            <%# Eval("PrimaryEmail").EncodeHtml() %>
                                        </span>
                                    </div>
                                </td> 
                                <td valign="top">
                                    <a id="A3" runat="server" href='<%# RouteController.EmailChangeVerificationsPendingSelect((int)Eval("UserId")) %>'
                                        title="Select">Select</a>
                                </td>                              
                                
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                    <div style="text-align: center;">
                        <asp:DataPager ID="pager" runat="server" PageSize="15" PagedControlID="listViewPendingEmailVerifications">
                            <Fields>
                                <asp:NumericPagerField />
                            </Fields>
                        </asp:DataPager>
                    </div>
                </div>
            </div>
        </div>
        <pp:ObjectContainerDataSource ID="dataSourcePendingEmailVerifications" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.UserSummaryViewData"
            OnSelecting="dataSourcePendingEmailVerifications_Selecting" />
    </div>
</asp:Content>
