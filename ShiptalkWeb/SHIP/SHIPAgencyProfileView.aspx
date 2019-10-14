<%@ Page Language="C#"  MasterPageFile="~/ShiptalkWeb.Master" AutoEventWireup="true"
    CodeBehind="SHIPAgencyProfileView.aspx.cs" Inherits="ShiptalkWeb.SHIP.SHIPAgencyProfileView" Title="SHIP Counselor" %>
<%@ Import Namespace="ShiptalkWeb.Routing" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects.UI" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects" %>
<%@ Import Namespace="ShiptalkWeb" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls"
    TagPrefix="pp" %>
<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="content-type" content="text/html;charset=ISO-8859-1" />
                <meta name="description"
                  content="Find a SHIP Counselor" />
</asp:Content>
<asp:Content ID="contentBody" ContentPlaceHolderID="body1" runat="server">
    <div id="NoSearchResultsMessage" runat="server" enableviewstate="false" visible="false">
    
        <tr>
            <td colspan="2">
                <div style="margin-top: 20px; text-align: center; margin-bottom: 10">
                <br />
                <br />
                    <span style="background-color: #fff">Your search for state 
                       <strong><%=stateName%></strong> and county <strong><%=countyName%></strong> did not yield any result.</span>
                </div>
            </td>
        </tr>
    
    </div>
    <div id="maincontent">
    <div>
        <asp:Repeater runat="server"  DataSourceID="dataSourceViewAgency" ID="RepeaterViewAgencies">
        <ItemTemplate>
            <div id="dv3colFormContent" class="dv3col">
                 
                        <table width="100%" border="1">
                            <tbody>
                                <tr>
                                    <td nowrap="nowrap" style="width: 30%;">
                                        <strong>Agency Name:</strong>
                                    </td>
                                    <td>
                                        <%# Eval("AgencyName").EncodeHtml() %>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <div >
                            <h4>
                                &nbsp;Locations
                            </h4>
                            <br />
                            <asp:Repeater runat="server" ID="repeaterLocations" DataSource='<%# Eval("Locations") %>'>
                                <HeaderTemplate>
                                    <table class="dataTable">
                                        <thead>
                                            <tr>
                                                <th scope="col" nowrap>
                                                    Address
                                                </th>
                                                <th scope="col" nowrap>
                                                    Phone Number / Email
                                                </th>
                                                <th scope="col" nowrap>
                                                    Hours of Operation
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <div>
                                                <%# ((bool)(Eval("IsMainOffice"))) ? "<strong>Main Office:</strong><br />" : "<br /><strong>Other Locations:</strong><br />"%>
                                                <%# ((ViewAgencyProfileLocationViewData)Container.DataItem).GetPhysicalAddress().FormattedAddress()%>
                                            </div>
                                        </td>
                                        <td>
                                            <%# Eval("PrimaryPhone").EncodeHtml() %> <br /> <%# Eval("PrimaryEmail").EncodeHtml() %>
                                        </td>
                                        <td>
                                            <%# Eval("HoursOfOperation").EncodeHtml() %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody> </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                        <br />
                        
                         </div>
                         </ItemTemplate>
        </asp:Repeater>
    </div>
      
        </div>
    <pp:ObjectContainerDataSource ID="dataSourceViewAgency" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.ViewAgencyProfileView"
        OnSelecting="dataSourceViewAgency_Selecting" />
</asp:Content>
