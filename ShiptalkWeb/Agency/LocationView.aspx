<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ShiptalkWeb.Master" CodeBehind="LocationView.aspx.cs" Inherits="ShiptalkWeb.Agency.LocationView" %>

<%@ Import Namespace="ShiptalkWeb.Routing" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects.UI" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects" %>
<%@ Import Namespace="ShiptalkWeb" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>
<asp:content id="contentHead" contentplaceholderid="head" runat="server">
    <title>View Agency Location</title>



    <script type="text/javascript">

            $(document).ready(function() {
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
                            __doPostBack($("[id$='_buttonDeleteAgencyLocation']").attr("id").replace(/_/g, "$"), "");
                        }
                    }
                });

                $("[id$='_buttonDeleteAgencyLocation']").click(function(e) {
                    e.preventDefault();
                    $('#confirmDelete').dialog('open');
                });
            });

    </script>
    
</asp:content>
<asp:content id="contentBody" contentplaceholderid="body1" runat="server">
    <div id="maincontent">
        <div class="dv3col">
        <asp:FormView runat="server" ID="formView" DataSourceID="dataSourceViewAgencyLocation" Width="100%">
                <ItemTemplate>
                    <h1>
                        <%# Eval("LocationName").EncodeHtml() %></h1>
                    <div>
                    &nbsp;
                            <asp:Panel runat="server" ID ="panelCommands" Visible='<%# !Convert.ToBoolean(Eval("IsMainOffice")) %>' style="text-align: right; width= 100%;">
                                <asp:Button runat="server" Visible='<%# IsAdmin  %>'  ID="buttonEditAgencyLocation" Text="Edit" ToolTip="Edit this Agency Location." OnClick="buttonEditAgencyLocation_Click" CssClass="formbutton1" />
                                &nbsp;
                                <asp:Button runat="server" Visible='<%# IsAdmin  %>'  ID="buttonDeleteAgencyLocation" Text="Delete" ToolTip="Delete this Agency Location." OnClick="buttonDeleteAgencyLocation_Click" CssClass="formbutton1" UseSubmitBehavior="true" />
                            </asp:Panel>
                            
                            <div id="dv3colFormContent" style="margin-top: 10px; width: 615px; border-top: solid 2px #eee;">
                            <table>
                                <tbody>
                                    <tr>
                                        <td nowrap style="width: 125px;">
                                            <strong>Location Name:</strong>
                                        </td>
                                        <td>
                                            <%# Eval("LocationName").EncodeHtml() %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Is Main Office:</strong>
                                        </td>
                                        <td>
                                            <%# ((bool)Eval("IsMainOffice")) ? "Yes" : "No" %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Is Active:</strong>
                                        </td>
                                        <td>
                                            <%# ((bool)Eval("IsActive")) ? "Yes" : "No" %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap style="width: 125px;">
                                            <strong>County:</strong>
                                        </td>
                                        <td>
                                           <%# Eval("PhysicalCounty").EncodeHtml()%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap style="width: 125px;">
                                            <strong>Physical Address:</strong>
                                        </td>
                                        <td>
                                            <%# ((ViewAgencyLocationViewData)Container.DataItem).GetPhysicalAddress().FormattedAddress() %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap style="width: 125px;">
                                            <strong>Mailing Address:</strong>
                                        </td>
                                        <td>
                                            <%# ((ViewAgencyLocationViewData)Container.DataItem).GetMailingAddress().FormattedAddress()%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap style="width: 125px;">
                                            <strong>Contact:</strong>
                                        </td>
                                        <td>
                                            <%# Eval("ContactFirstName").EncodeHtml()%>
                                            <%# Eval("ContactMiddleName").EncodeHtml()%>
                                            <%# Eval("ContactLastName").EncodeHtml()%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap style="width: 125px;">
                                            <strong>Contact Title:</strong>
                                        </td>
                                        <td>
                                            <%# Eval("ContactTitle").EncodeHtml()%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap style="width: 125px;">
                                            <strong>Hours of Operation:</strong>
                                        </td>
                                        <td>
                                            <%# Eval("HoursOfOperation").EncodeHtml() %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap style="width: 125px;">
                                            <strong>Primary Phone:</strong>
                                        </td>
                                        <td>
                                            <%# Eval("PrimaryPhone").EncodeHtml() %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap style="width: 125px;">
                                            <strong>Secondary Phone:</strong>
                                        </td>
                                        <td>
                                            <%# Eval("SecondaryPhone").EncodeHtml() %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap style="width: 125px;">
                                            <strong>Primary Email:</strong>
                                        </td>
                                        <td>
                                            <span class="lineBreak"><%# Eval("PrimaryEmail").EncodeHtml() %></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap style="width: 125px;">
                                            <strong>Secondary Email:</strong>
                                        </td>
                                        <td>
                                            <%# Eval("SecondaryEmail").EncodeHtml() %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap style="width: 125px;">
                                            <strong>Toll Free Phone:</strong>
                                        </td>
                                        <td>
                                            <%# Eval("TollFreePhone").EncodeHtml()%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap style="width: 125px;">
                                            <strong>TDD:</strong>
                                        </td>
                                        <td>
                                            <%# Eval("TDD").EncodeHtml()%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap style="width: 125px;">
                                            <strong>Toll Free TDD:</strong>
                                        </td>
                                        <td>
                                            <%# Eval("TollFreeTDD").EncodeHtml()%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap style="width: 125px;">
                                            <strong>Fax:</strong>
                                        </td>
                                        <td>
                                            <%# Eval("Fax").EncodeHtml()%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap style="width: 125px;">
                                            <strong>Comments:</strong>
                                        </td>
                                        <td>
                                            <%#Eval("Comments").EncodeHtml()%>
                                        </td>
                                    </tr>
                                    <%--Added by Lavanya--%>
                                    <tr>
                                        <td nowrap style="width: 125px;">
                                            <strong>Available Languages:</strong>
                                        </td>
                                        <td>                                       
                                                <%# Eval("AvailableLanguages").EncodeHtml()%></a>
                                        </td>
                                    </tr>
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
                            </div>
                    </div>
                </ItemTemplate>
            </asp:FormView>
        </div>
    </div>
    <div id="confirmDelete" title="Confirm Delete" style="display:none;">
        <p>
            Are you sure you would like to delete this agency location?
        </p>
    </div>
     <pp:ObjectContainerDataSource ID="dataSourceViewAgencyLocation" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.ViewAgencyLocationViewData" OnSelecting="dataSourceViewAgencyLocation_Selecting" />
</asp:content>
