<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ShiptalkWeb.Master" CodeBehind="SubStateRegionViewForReport.aspx.cs" Inherits="NPRRebuild.ShiptalkWeb.NPRReports.SubStateRegionViewForReport" %>

<%@ Import Namespace="ShiptalkWeb.Routing" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects.UI" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects" %>
<%@ Import Namespace="ShiptalkWeb" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>
<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <title>View Sub State Region For Report</title>
    


    <script type="text/javascript">

        $(document).ready(function() {
            //initialize alt row table styles.
            $(".dataTable tr:even").addClass("even");

            //Initialize the dialog to be used for delete confirmations.
            $("#confirmDelete").dialog({
                resizable: false,
                height: 200,
                autoOpen: false,
                modal: true,
                buttons: {
                    "No": function() {
                        $(this).dialog('close');
                    },
                    "Yes": function() {
                        $(this).dialog('close');
                        __doPostBack($("[id$='_buttonDeleteSubStateRegion']").attr("id").replace(/_/g, "$"), "");
                    }
                }
            });

            $("[id$='_buttonDeleteSubStateRegion']").click(function(e) {
                e.preventDefault();
                $('#confirmDelete').dialog('open');
            });

        });
        
    </script>

</asp:Content>
<asp:Content ID="contentBody" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontent">
        <asp:Panel ID="panelSuccess" runat="server" Visible='<%# DisplaySuccessMessage() %>'>
            <h1>
                Success!
            </h1>
            <p>
                <asp:Literal ID="literalSuccessMessage" runat="server" Text='<%# GetSuccessMessage() %>' />                         
            </p>
            <a runat="server" href='<%# RouteController.ReportSubstateAdd() %>'>Add a Sub State Region For Report</a>
            <br />
            <a runat="server" href='<%# RouteController.ReportSubstateSearch() %>'>Sub State Region For Report</a>
            <br />
            <br />
        </asp:Panel>
        <div class="dv3col">
            <asp:FormView runat="server" ID="formView" DataSourceID="dataSourceViewSubStateRegion" Width="100%">
                <ItemTemplate>
                    <h1>
                        <%# Eval("SubStateRegionName").EncodeHtml()%> </h1>
                    <div class="commands">
                        <asp:Button runat="server" ID="buttonEditSubStateRegion" Visible='<%# IsAdmin && (Scope == Scope.CMS || Scope == Scope.State)  %>' Text="Edit" ToolTip="Edit this SubStateRegion." OnClick="buttonEditSubStateRegion_Click" CssClass="formbutton1" />
                        &nbsp;
                        <asp:Button runat="server" ID="buttonDeleteSubStateRegion" Visible='<%# IsAdmin && (Scope == Scope.CMS || Scope == Scope.State)  %>' Text="Delete" ToolTip="Delete this SubStateRegion." OnClick="buttonDeleteSubStateRegion_Click" CssClass="formbutton1" UseSubmitBehavior="true" />
                    </div>
                    <div id="dv3colFormContent" class="section">
                        <table width="100%" class="dataTable">
                            <tbody>
                                <tr>
                                    <td nowrap style="width: 125px;">
                                        SubStateRegion Name:
                                    </td>
                                    <td>
                                        <%# Eval("SubStateRegionName").EncodeHtml()%> 
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap style="width: 125px;">
                                        Report State:
                                    </td>
                                    <td>
                                        <%# ((State)Eval("State")).StateName%> 
                                    </td>
                                </tr> 
                                <tr>
                                    <td nowrap style="width: 125px;">
                                        Report Form Type:
                                    </td>
                                    <td>
                                        <%# ((FormType)Eval("ReprotFormType")).Description()%> 
                                    </td>
                                </tr>  
                                <tr>
                                    <td>
                                        SubStateRegion Type:
                                    </td>
                                    <td>
                                        <%# ((SubStateReportType)Enum.Parse(typeof(SubStateReportType), (Eval("Type").EncodeHtml()))).Description()%>                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap style="width: 125px;">
                                        SubStateRegion Entity Code:
                                    </td>
                                    <td>
                                        <%# Eval("SubStateRegionServiceEntityCode").EncodeHtml()%> 
                                    </td>
                                </tr> 
                                <tr>
                                    <td valign="top">
                                        Service Areas:
                                    </td>
                                    <td>

                                            <asp:Repeater runat="server" ID="repeaterServiceAreas" DataSource='<%# Eval("ServiceAreas") %>'>
                                                <HeaderTemplate>
                                                    <table width="100%"  class="dataTable">
                                                        <tbody>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%# Eval("Value").EncodeHtml()%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </tbody></table>
                                                </FooterTemplate>
                                            </asp:Repeater>

                                    </td>
                                </tr>




                            </tbody>
                        </table>

                    </div>
                </ItemTemplate>
            </asp:FormView>
        </div>
    </div>
    <div id="confirmDelete" title="Confirm Delete" style="display: none;">
        <p>
            Are you sure you would like to delete this Substate Region for Report?
        </p>
    </div>
    <pp:ObjectContainerDataSource ID="dataSourceViewSubStateRegion" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.ViewSubStateRegionForReportViewData" OnSelecting="dataSourceViewSubStateRegion_Selecting" />
</asp:Content>
