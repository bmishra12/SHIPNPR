<%@ Page Title="" Language="C#" MasterPageFile="~/ShiptalkWebWide.Master" AutoEventWireup="true" CodeBehind="SpecialFieldsFView.aspx.cs" Inherits="ShiptalkWeb.Npr.Forms.SpecialFieldsFView" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet" Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet" TagPrefix="pp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">
<div class="dv5col">
     <div>
        <h1>Special Fields Manager</h1>    
    </div>

    <table width="100%">
        <tr>
            <td valign="top" align="left">
                        <asp:Label ID="lblFeedBack"  ForeColor="Red" runat="server" Visible="false"></asp:Label>
                        <asp:Panel ID="pnlFieldInfo" runat="server"  GroupingText="Special Field Data"  Width="100%">
                        <asp:FormView runat="server" ID="formViewSpecialFields" DefaultMode="ReadOnly" 
                            DataSourceID="dataSourceViewSpecialField"  Width="100%" >
                            <ItemTemplate>
                            
   
                            <table width="100%"  cellspacing = "10px">
                                <tr>
                                    <td><strong>Name:</strong></td>
                                    <td><asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label></td>
                                    <td><strong>State:</strong></td>
                                    <td><asp:Label ID="lblState" runat="server" Text='<%# Bind("StateName") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><strong>Start Date:</strong></td>
                                    <td><asp:Label ID="lblStartDate" runat="server" Text='<%# Bind("StartDate") %>'></asp:Label></td>
                                    <td><strong>End Date:</strong></td>
                                    <td><asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("EndDate") %>'></asp:Label></td>
                                    
                                </tr>
                                <tr>
                                    <td><strong>Description:</strong></td>
                                    <td colspan="3"><asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label></td>
                                    <td></td>
                                    <td></td>

                                </tr>
                                <tr>
                                    <td><strong>Validation Type:</strong></td>
                                    <td><asp:Label ID="lblValidationType" runat="server" Text='<%# Bind("ValidationName") %>'></asp:Label></td>

                                    <td><strong>IsRequired:</strong></td>
                                    <td><asp:Label ID="lblRequired" runat="server" Text='<%# Bind("IsRequired") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><strong>Validation Range:</strong></td>
                                    <td><asp:Label ID="lblValidationRange" runat="server" Text='<%# Bind("Range") %>'></asp:Label></td>                                 

                                    <td><strong>Form Type:</strong></td>
                                    <td><asp:Label ID="lblFormType" runat="server" Text='<%# Bind("FormType") %>'></asp:Label></td>
                                    
                                </tr>
                                <tr>
                                    <td><strong>Ordinal:</strong></td>
                                    <td><asp:Label ID="lblOrdinal" runat="server" Text='<%# Bind("Ordinal") %>'></asp:Label></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                
                            </table>
                            
                            
                            
                            </ItemTemplate>
                        </asp:FormView>
                        <table width="100%">
                         <tr>
                                    <td align="right">
                                    <asp:Button ID="btnEdit" runat="server" CssClass="formbutton1" Text="Edit"  
                                            Width="100px" onclick="btnEdit_Click"/>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                        </table>
                        </asp:Panel>
                
            </td>
        </tr>
    </table>
    <pp:ObjectContainerDataSource ID="dataSourceViewSpecialField" runat="server" 
        DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.ViewSpecialFieldsViewData" >
        </pp:ObjectContainerDataSource>
</asp:Content>
