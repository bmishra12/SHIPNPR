<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SHIPProfileView.aspx.cs" Inherits="ShiptalkWeb.SHIP.SHIPProfileView" MasterPageFile="~/ShiptalkWeb.Master" Title="State SHIP" %>
<%@ Import Namespace="ShiptalkWeb.Routing" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects.UI" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects" %>
<%@ Import Namespace="ShiptalkWeb" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>
<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="content-type" content="text/html;charset=ISO-8859-1" />
                <meta name="description"
                  content="Find a State SHIP" />
</asp:Content>
<asp:Content ID="contentBody" ContentPlaceHolderID="body1" runat="server">
  
    <div id="maincontent">
        
        <div class="dv3col">
        
           <asp:Panel runat="server" ID="panelState">
                    
            <table width="100%" class="formTable">
                <tbody>

                    <tr>
                        <td colspan="2">
                            <div class="commands">
                                <asp:Button runat="server" ID="buttonEdit" Text="Edit"  Visible ="false" CssClass="formbutton3" OnClick="buttonEdit_Click"   />
                            </div>
                        </td>
                    </tr>                    
                    <tr>
                        <td>
                            <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelName" AssociatedControlID="ddlStates">State Name:</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlStates" DataTextField="Value" DataValueField="Key"
                                OnSelectedIndexChanged="ddlStates_SelectedChanged" AutoPostBack="true" AppendDataBoundItems="true"
                                Width="170px">
                                <asp:ListItem Text="Select a State" Value="0" />
                            </asp:DropDownList>
                            <br />
                        </td>
                    </tr>

                    <tr>
                        <td colspan="2">
                            <asp:Label runat="server" ID="MessageBox" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
          </asp:Panel>

            <asp:FormView runat="server" ID="formView" DataSourceID="dataSourceViewSHIP" Width="100%" OnItemCreated="formView_ItemCreated">
                <ItemTemplate>
                    
                    <div id="dv3colFormContent" class="section">
                        <table width="100%">
                        <%--    <tbody>--%>
                                <tr>
                                    <td colspan="2" align="right">
                                        <asp:LinkButton ID="lnkPDF" runat="server" onclick="lnkPDF_Click" >Download to PDF</asp:LinkButton> 
                                    </td>
                                </tr>
                                <tr>
                                    <td  width="150px">
                                        <strong>State Name:</strong>
                                    </td>
                                    <td width="450px">
                                        <%# Eval("StateName").EncodeHtml() %> 
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Program Name:</strong>
                                    </td>
                                    <td>
                                        <%# Eval("ProgramName").EncodeHtml()%>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td>
                                        <strong>Program Website:</strong>
                                    </td>
                                    <td>
                                        <%# Eval("Programwebsite").EncodeHtml()%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Program Summary:</strong>
                                    </td>
                                    <td>
                                        <%# Eval("ProgramSummary").EncodeHtml()%>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <strong>Program Director:</strong>
                                    </td>
                                    <td>
                                        <%# Eval("ProgramCoordinatorName").EncodeHtml()%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Admin Agency Address:</strong>
                                    </td>
                                    <td>
                                        <%# Eval("AdminAgencyAddress").EncodeHtml()%>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <strong>Admin Agency Name:</strong>
                                    </td>
                                    <td>
                                        <%# Eval("AdminAgencyName").EncodeHtml()%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Admin Agency Phone:</strong>
                                    </td>
                                    <td>
                                        <%# Eval("AdminAgencyPhone").EncodeHtml()%>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <strong>Admin Agency Zipcode:</strong>
                                    </td>
                                    <td>
                                        <%# Eval("AdminAgencyZipcode").EncodeHtml()%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Admin Agency Fax:</strong>
                                    </td>
                                    <td>
                                        <%# Eval("AdminAgencyFax").EncodeHtml()%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Admin Agency Email:</strong>
                                    </td>
                                    <td>
                                        <%# Eval("AdminAgencyEmail").EncodeHtml()%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Available Languages:</strong>
                                    </td>
                                    <td>
                                        <%# Eval("AvailableLanguages").EncodeHtml()%>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <strong>Beneficiary Contact Email:</strong>
                                    </td>
                                    <td>
                                        <%# Eval("BeneficiaryContactEmail").EncodeHtml()%>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <strong>Beneficiary Contact Hours:</strong>
                                    </td>
                                    <td>
                                        <%# Eval("BeneficiaryContactHours").EncodeHtml()%>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <strong>Beneficiary Contact Phone Toll Free:</strong>
                                    </td>
                                    <td>
                                        <%# Eval("BeneficiaryContactPhoneTollFree").EncodeHtml()%>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <strong>Beneficiary Contact Phone Toll Free In State Only:</strong>
                                    </td>
                                    <td>
                                        <%# Eval("BeneficiaryContactPhoneTollFreeInStateOnly").EncodeHtml()%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Beneficiary Contact Phone Toll Line:</strong>
                                    </td>
                                    <td>
                                        <%# Eval("BeneficiaryContactPhoneTollLine").EncodeHtml()%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Beneficiary Contact TDD Line:</strong>
                                    </td>
                                    <td>
                                        <%# Eval("BeneficiaryContactTDDLine").EncodeHtml()%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Beneficiary Contact Website:</strong>
                                    </td>
                                    <td>
                                        <%# Eval("BeneficiaryContactWebsite").EncodeHtml()%>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <strong>Number Of Counties Served:</strong>
                                    </td>
                                    <td>
                                        <%# Eval("NumberOfCountiesServed").EncodeHtml()%>
                                    </td>
                                </tr>
                                  <tr>
                                    <td>
                                        <strong>Number Of Sponsors:</strong>
                                    </td>
                                    <td>
                                        <%# Eval("NumberOfSponsors").EncodeHtml()%>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <strong>Number Of Volunteer Counselors:</strong>
                                    </td>
                                    <td>
                                        <%# Eval("NumberOfVolunteerCounselors").EncodeHtml()%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Program Coordinator Address:</strong>
                                    </td>
                                    <td>
                                        <%# Eval("ProgramCoordinatorAddress").EncodeHtml()%>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <strong>Program Coordinator City:</strong>
                                    </td>
                                    <td>
                                        <%# Eval("ProgramCoordinatorCity").EncodeHtml()%>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <strong>Program Coordinator Email:</strong>
                                    </td>
                                    <td>
                                        <%# Eval("ProgramCoordinatorEmail").EncodeHtml()%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Program Coordinator Fax:</strong>
                                    </td>
                                    <td>
                                        <%# Eval("ProgramCoordinatorFax").EncodeHtml()%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Program Coordinator Zipcode:</strong>
                                    </td>
                                    <td>
                                        <%# Eval("ProgramCoordinatorZipcode").EncodeHtml()%>
                                    </td>
                                </tr>
                                <%-- New fields--%>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="pnlSecure" runat="server">
                                            <table width="100%" border="1">
                                                <tr>
                                                    <td width="150px">
                                                        <strong>State Oversight Agency:</strong>
                                                    </td>
                                                    <td width="450px">
                                                        <%# Eval("StateOversightAgency").EncodeHtml()%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <strong>Number Of Paid Staff:</strong>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NumberOfPaidStaff").EncodeHtml()%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <strong>Number Of Coordinators:</strong>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NumberOfCoordinators").EncodeHtml()%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <strong>Number Of Certified Counselors:</strong>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NumberOfCertifiedCounselors").EncodeHtml()%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <strong>Number Of Eligible Beneficiaries:</strong>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NumberOfEligibleBeneficiaries").EncodeHtml()%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <strong>Number Of Beneficiary Contacts:</strong>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NumberOfBeneficiaryContacts").EncodeHtml()%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <strong>Local Agencies:</strong>
                                                    </td>
                                                    <td>
                                                        <%# Eval("LocalAgencies").EncodeHtml()%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            <%-- </tbody>--%>
                            </table>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:FormView>
        </div>
    </div>
 
    <pp:ObjectContainerDataSource ID="dataSourceViewSHIP" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.ViewSHIPProfileViewData" />
</asp:Content>
