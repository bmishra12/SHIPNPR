<%@ Page Title="" Language="C#" MasterPageFile="~/ShiptalkWeb.Master" AutoEventWireup="true" CodeBehind="SubStateRegionView.aspx.cs" Inherits="ShiptalkWeb.Agency.SubStateRegionView" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects"%>

<%@ Import Namespace="ShiptalkWeb" %>
<%@ Import Namespace="ShiptalkWeb.Routing" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>
<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <title>View Sub-State Region</title>


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
            <a id="aAgencyRegionAdd" runat="server" href='<%# RouteController.AgencyRegionAdd() %>'>Add a Sub-State Region</a>
            <br />
            <a id="aAgencySearch" runat="server" href='<%# RouteController.AgencySearch() %>'>Find Agencies</a>
            <br />
            <br />
        </asp:Panel>
        <div class="dv3col">
            <asp:FormView runat="server" ID="formView" DataSourceID="dataSourceViewSubStateRegion" Width="100%">
                <ItemTemplate>
                    <h1>
                        <%# Eval("Name").EncodeHtml() %>
                        </h1>
                    <div id="dv3colFormContent" class="section">
                        <table>
                            <tbody>
                                <tr>
                                    <td nowrap>
                                        State:
                                    </td>
                                    <td>
                                        <%# ((State)Eval("State")).StateName %>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap>
                                        Sub-State Region Name:
                                    </td>
                                    <td>
                                        <%# Eval("Name").EncodeHtml() %>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        Is Active:
                                    </td>
                                    <td>
                                        <%# ((bool)Eval("IsActive")) ? "Yes" : "No" %>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        Sub-State Region Agencies:
                                    </td>
                                    <td>
                                        <asp:Repeater runat="server" ID="repeaterAgencies" DataSource='<%# Eval("Agencies") %>'>
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Container.DataItem.EncodeHtml() %>
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
            
                    <asp:Panel runat="server" ID="panelCommands" CssClass="commands" Visible='<%# ((Scope == Scope.CMS || Scope == Scope.State) && IsAdmin) %>'>
                        <asp:Button runat="server" ID="buttonEditSubStateRegion" Text="Edit" ToolTip="Edit this Sub-State Region." OnClick="buttonEditSubStateRegion_Click" CssClass="formbutton1" />
                        &nbsp;
                        <asp:Button runat="server" ID="buttonDeleteSubStateRegion" Text="Delete" ToolTip="Delete this Sub-State Region." OnClick="buttonDeleteSubStateRegion_Click" CssClass="formbutton1" UseSubmitBehavior="true" />
                    </asp:Panel>
        </div>
        <div id="confirmDelete" title="Confirm Delete" style="display: none;">
        <p>
            Are you sure you would like to delete this sub-state region?
        </p>
    </div>
    <pp:ObjectContainerDataSource ID="dataSourceViewSubStateRegion" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.ViewSubStateRegionViewData" OnSelecting="dataSourceViewSubStateRegion_Selecting" />
</asp:Content>
