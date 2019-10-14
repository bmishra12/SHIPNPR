<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ShiptalkWeb.Master" CodeBehind="SubStateRegionSearchForReport.aspx.cs" Inherits="NPRRebuild.ShiptalkWeb.NPRReports.SubStateRegionSearchForReport" %>

<%@ Import Namespace="ShiptalkWeb" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects" %>
<%@ Import Namespace="ShiptalkWeb.Routing" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>

<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <title>List of Sub State Region For Report</title>

    <link href="../../css/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />


    <script type="text/javascript" src="../../scripts/jquery-ui-mask-1.7.2.js"></script>

    <script type="text/javascript" src="../../scripts/jquery.charcounter.js"></script>

    
    <script type="text/javascript">

        $(document).ready(function() {

            //initialize alt row table styles.
            $(".dataTable tr:even").addClass("even");

            
        });
        
    </script>

</asp:Content>
<asp:Content ID="contentBody" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontent">
        <div class="dv3col">
            <h1>
                List of Sub State Region For Report</h1>

            <div>
                <div class="commands">

                    <a runat="server" id="aAddReport" Visible='<%#  IsAdmin && (Scope == Scope.CMS || Scope == Scope.State)  %>' href='<%# RouteController.ReportSubstateAdd() %>'>Add a Sub State Region for Report</a>
                    <br />
                </div>
                <br />
               
                
                
                <div id="dv3colFormContent" class="section">
                    <asp:Panel runat="server" ID="panelNoResults" Visible="false">
                    <p style="text-align: center; color: Red;">
                            No Sub State Region For Report were found.
                        </p>
                    </asp:Panel>
                    <asp:ListView runat="server" ID="listViewSubStates" 
                        DataSourceID="dataSourceSubStateRegion" ItemPlaceholderID="placeHolder" 
                         OnSorting="listViewSubStates_Sorting"
                        DataKeyNames="ID" >
                        <LayoutTemplate>
                            <table id="searchResults" class="dataTable">
                                <thead>
                                    <tr>
                                        <th scope="col" style="width: 210px;">
                                            <asp:LinkButton runat="server" ID="SortByEventNameButton"
                                              CommandName="Sort" Text="Name" 
                                              CommandArgument="SubStateRegionName"/>
                                            <asp:Image runat="server" ID="SortImage1" 
                                              ImageUrl="~/images/ascending.gif" Visible="false" />
                                        </th>
                                        <th scope="col">
                                            <asp:LinkButton runat="server" ID="LinkButton1"
                                              CommandName="Sort" Text="Type" 
                                              CommandArgument="Type"/>
                                            <asp:Image runat="server" ID="SortImage2" 
                                              ImageUrl="~/images/ascending.gif" Visible="false" />
                                            
                                        </th>

                                        <th scope="col" style="width: 75px;">
                                            &nbsp;
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
                                <td valign="top">
                                     <a id="A1" runat="server" href='<%# RouteController.ReportSubstateView((int)Eval("ID")) %>' title='<%# Eval("SubStateRegionName").EncodeHtml().TrimEnd()  %>'>
                                        <%# Eval("SubStateRegionName").EncodeHtml()%> </a>
                                    <br />
                                </td>
                                <td scope="row" valign="top">
                                       <%# ((SubStateReportType)Enum.Parse(typeof(SubStateReportType), (Eval("Type").EncodeHtml()))).Description()%>
                                </td>

                                <td valign="top">
                                    <a id="A2" runat="server" href='<%# RouteController.ReportSubstateView((int)Eval("ID")) %>' title="View this Sub State.">View</a>
                                     | 
                                    <a id="A3" runat="server" Visible='<%#  IsAdmin && (Scope == Scope.CMS || Scope == Scope.State)  %>' href='<%# RouteController.ReportSubstateEdit((int)Eval("ID")) %>' title="Edit this Sub State.">Edit</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                     <div style="text-align: center;">
                        <asp:DataPager ID="pager" runat="server" PageSize="5" PagedControlID="listViewSubStates">
                            <Fields>
                                <asp:NumericPagerField />
                            </Fields>
                        </asp:DataPager>
                    </div>
            </div>
        </div>
    </div>
    </div>
    <pp:ObjectContainerDataSource ID="dataSourceSubStateRegion" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.SearchSubStateRegionForReportViewData" OnSelecting="dataSourceSubStateRegion_Selecting" />
</asp:Content>
