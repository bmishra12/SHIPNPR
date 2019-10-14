<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ShiptalkWeb.Master" CodeBehind="AgencyView.aspx.cs" Inherits="ShiptalkWeb.Agency.AgencyView" %>

<%@ Import Namespace="ShiptalkWeb.Routing" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects.UI" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects" %>
<%@ Import Namespace="ShiptalkWeb" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>
<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <title>View Agency</title>
    


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
                        __doPostBack($("[id$='_buttonDeleteAgency']").attr("id").replace(/_/g, "$"), "");
                    }
                }
            });

            $("[id$='_buttonDeleteAgency']").click(function(e) {
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
            <a runat="server" href='<%# RouteController.AgencyRegister() %>'>Add an Agency</a>
            <br />
            <a runat="server" href='<%# RouteController.AgencySearch() %>'>Find Agencies</a>
            <br />
            <br />
        </asp:Panel>
        <div class="dv3col">
            <asp:FormView runat="server" ID="formView" DataSourceID="dataSourceViewAgency" Width="100%">
                <ItemTemplate>
                    <h1>
                        <%# Eval("Name").EncodeHtml() %> (<%# Eval("Code").EncodeHtml().TrimEnd() %>)</h1>
                    <div class="commands">
                        <asp:Button runat="server" ID="buttonEditAgency" Text="Edit"   Visible='<%# IsAdmin  %>' ToolTip="Edit this Agency." OnClick="buttonEditAgency_Click" CssClass="formbutton1" />
                        &nbsp;
                        <asp:Button runat="server" ID="buttonDeleteAgency" Text="Delete" Visible='<%# IsAdmin %>' ToolTip="Delete this Agency." OnClick="buttonDeleteAgency_Click" CssClass="formbutton1" UseSubmitBehavior="true" />
                    </div>
                    <div id="dv3colFormContent" class="section">
                        <table width="100%" class="dataTable">
                            <tbody>
                                <tr>
                                    <td nowrap style="width: 125px;">
                                        Agency Name:
                                    </td>
                                    <td>
                                        <%# Eval("Name").EncodeHtml() %> (<%# Eval("Code").EncodeHtml().TrimEnd() %>)
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Agency Type:
                                    </td>
                                    <td>
                                        <%# ((AgencyType)Enum.Parse(typeof(AgencyType), (Eval("Type").EncodeHtml()))).Description()%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Is Active:
                                    </td>
                                    <td>
                                        <%# ((Convert.ToBoolean(Eval("IsActive").EncodeHtml()))) ? "Yes" : "No"%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Service Areas:
                                    </td>
                                    <td>
                                        <asp:Repeater runat="server" ID="repeaterServiceAreas" DataSource='<%# Eval("ServiceAreas") %>'>
                                            <HeaderTemplate>
                                                <table width="100%" style="margin: 0; padding: 0;">
                                                    <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("Value").EncodeHtml() %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody></table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap style="width: 125px;">
                                        County:
                                    </td>
                                    <td>
                                        <%# Eval("PhysicalCounty").EncodeHtml()%>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap style="width: 125px;">
                                        Physical Address:
                                    </td>
                                    <td>
                                        <%# ((ViewAgencyViewData)Container.DataItem).GetPhysicalAddress().FormattedAddress() %>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap style="width: 125px;">
                                        Mailing Address:
                                    </td>
                                    <td>
                                        <%# ((ViewAgencyViewData)Container.DataItem).GetMailingAddress().FormattedAddress() %>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap style="width: 125px;">
                                        Contact:
                                    </td>
                                    <td>
                                        <%# Eval("SponsorFirstName").EncodeHtml()%>
                                        <%# Eval("SponsorMiddleName").EncodeHtml()%>
                                        <%# Eval("SponsorLastName").EncodeHtml()%>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap style="width: 125px;">
                                        Contact Title:
                                    </td>
                                    <td>
                                        <%# Eval("SponsorTitle").EncodeHtml()%>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap style="width: 125px;">
                                        Hours of Operation:
                                    </td>
                                    <td>
                                        <%# Eval("HoursOfOperation").EncodeHtml() %>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap style="width: 125px;">
                                        Primary Phone:
                                    </td>
                                    <td>
                                        <%# Eval("PrimaryPhone").EncodeHtml() %>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap style="width: 125px;">
                                        Secondary Phone:
                                    </td>
                                    <td>
                                        <%# Eval("SecondaryPhone").EncodeHtml() %>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap style="width: 125px;">
                                        Primary Email:
                                    </td>
                                    <td>
                                        <%# Eval("PrimaryEmail").EncodeHtml() %>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap style="width: 125px;">
                                        Secondary Email:
                                    </td>
                                    <td>
                                        <%# Eval("SecondaryEmail").EncodeHtml() %>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap style="width: 125px;">
                                        Toll Free Phone:
                                    </td>
                                    <td>
                                        <%# Eval("TollFreePhone").EncodeHtml()%>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap style="width: 125px;">
                                        TDD:
                                    </td>
                                    <td>
                                        <%# Eval("TDD").EncodeHtml()%>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap style="width: 125px;">
                                        Toll Free TDD:
                                    </td>
                                    <td>
                                        <%# Eval("TollFreeTDD").EncodeHtml()%>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap style="width: 125px;">
                                        Fax:
                                    </td>
                                    <td>
                                        <%# Eval("Fax").EncodeHtml()%>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap style="width: 125px;">
                                        Website URL:
                                    </td>
                                    <td>
                                        <a href='<%# Eval("URL").EncodeHtmlAttr() %>' title="Website URL">
                                            <%# Eval("URL").EncodeHtml() %></a>
                                    </td>
                                </tr>
                                <%--Added by Lavanya--%>
                                 <tr>
                                    <td nowrap style="width: 125px;">
                                        Available Languages:
                                    </td>
                                    <td>                                       
                                            <%# Eval("AvailableLanguages").EncodeHtml()%></a>
                                    </td>
                                </tr>
                                <%--end--%>
                                <tr>
                                    <td nowrap style="width: 125px;">
                                        Comments:
                                    </td>
                                    <td>
                                        <%#Eval("Comments").EncodeHtml()%>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap style="width: 125px;">
                                        <strong>Hide Agency From Public:</strong>
                                    </td>
                                    <td>
                                        <%#   ((Convert.ToBoolean(Eval("HideAgencyFromPublic").EncodeHtml()))) ? "<strong>Yes" : "No"%>
                                    </td>
                                </tr>
                                <%--Added by Lavanya--%>
                                <tr>
                                    <td nowrap style="width: 125px;">
                                        <strong>Hide Agency From Search:</strong>
                                    </td>
                                    <td>
                                        <%# ((Convert.ToBoolean(Eval("HideAgencyFromSearch").EncodeHtml()))) ? "<strong>Yes" : "No"%>
                                    </td>
                                </tr>
                                 <%--end--%>
                            </tbody>
                        </table>
                        <br />
                        <br />
                        <div class="section">
                        <h4>
                            Locations
                        </h4>
                        <div class="commands">
                            <asp:Button runat="server" ID="buttonAddLocation"  Text="Add a Location" ToolTip="Add a Location to this Agency." CssClass="formbutton1" OnClick="buttonAddLocation_Click" />
                        </div>
                        <br />
                        <asp:Repeater runat="server" ID="repeaterLocations" DataSource='<%# Eval("Locations") %>'>
                            <HeaderTemplate>
                                <table class="dataTable">
                                    <thead>
                                        <tr>
                                            <th scope="col" nowrap>
                                                Location
                                            </th>
                                            <th scope="col" nowrap>
                                                Details
                                            </th>
                                            <th>
                                                &nbsp;
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="vertical-align: top;">
                                        <a runat="server" href='<%# RouteController.AgencyLocationView(Convert.ToInt32(Eval("Id"))) %>' title='<%# Eval("LocationName") %>'>
                                            <%# Eval("LocationName").EncodeHtml() %></a>
                                            <%# ((bool)(Eval("IsMainOffice"))) ? "<br />(Main Office)" : ""%>
                                    </td>
                                    <td style="vertical-align: top;">
                                        <%# ((ViewAgencyLocationViewData)Container.DataItem).GetPhysicalAddress().FormattedAddress() %>
                                        <br />   
                                        <%# Eval("PrimaryPhone").EncodeHtml()%>
                                        <br />
                                        Hours: <%# Eval("HoursOfOperation").EncodeHtml() %>
                                    </td>
                                    <td style="vertical-align: top;">
                                        <a id="A1" runat="server" href='<%# RouteController.AgencyLocationView(Convert.ToInt32(Eval("Id"))) %>' title="View this Agency Location.">View</a>
                                        |
                                        <a runat="server"  href='<%# RouteController.AgencyLocationEdit(Convert.ToInt32(Eval("Id"))) %>' visible='<%#  !(bool)Eval("IsMainOffice") %>' title="Edit this Agency Location.">Edit</a>
                                        
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody> </table>
                            </FooterTemplate>
                        </asp:Repeater>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:FormView>
        </div>
    </div>
    <div id="confirmDelete" title="Confirm Delete" style="display: none;">
        <p>
            Are you sure you would like to delete this agency and all of its locations?
        </p>
    </div>
    <pp:ObjectContainerDataSource ID="dataSourceViewAgency" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.ViewAgencyViewData" OnSelecting="dataSourceViewAgency_Selecting" />
</asp:Content>
