<%@ Page Title="" Language="C#" MasterPageFile="~/ShiptalkWebWide.Master" AutoEventWireup="true" CodeBehind="ResourceReportSearch.aspx.cs" Inherits="ShiptalkWeb.ResourceReportSearch" %>
<%@ Import Namespace="ShiptalkWeb.Routing" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects.UI" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects" %>
<%@ Import Namespace="ShiptalkWeb" %>
<%@ Import Namespace="System.Data" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">
<SCRIPT language=Javascript>
      <!--
      function isNumberKey(evt)
      {
         var charCode = (evt.which) ? evt.which : event.keyCode
         if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
         return true;
      }
      //-->
   </SCRIPT>

 <div class="dv5col">
     <div>
        <h1>Search Resource Report</h1>  
        <div class="commands">
         <a runat="server" id="a1" href="~/Npr/Docs/Chapter_4_NPR_Resource_508.pdf"  target='_blank' > <b>NPR User Manual: Resource Report Form</b></a>
         <br/>
         <br/>
        </div>  
    </div>


<table width="100%">
    <tr>
        <td colspan="4">
            <div style="position:relative; left:260px" id="StateContentDiv" runat="server">
                <table>
                    <tr>
                        <td align="right"><span class="smaller">Enter Year</span>
                            <asp:DropDownList ID="cmbYear" runat="server" ToolTip="Enter Year" ></asp:DropDownList>
                        </td>
                        <td align="right">
                            <table style="width:100%">
                                <tr>
                                    <td id="StateContentCell" runat="server" >
                                        <span class="smaller">State</span>
                                         <asp:DropDownList ID="ddlStates" Width="200px" runat="server" 
                                            AutoPostBack="True" ToolTip="State" ></asp:DropDownList>
                                        <asp:Label ID="lblDefaultState" runat="server" Width="200px" Visible="false" ></asp:Label>
                                  </td>
                                </tr>
                            </table>
                            
                        </td>
                        <td  align="right">
                                <div><asp:Button ID="btnSearch"  CssClass="formbutton1" Width="100px" runat="server" Text="Search" 
                                    onclick="btnSearch_Click" />
                                </div>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
        
    </tr>
    <tr>
        <td colspan="4" align="right">
            <asp:Button ID="btnNew" runat="server" CssClass="formbutton1"  Width="160px" 
                Text="Add New Resource Report" onclick="btnNew_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <div><asp:Label ID="lblRecordCount" runat="server" Visible="false"></asp:Label></div>
            <asp:GridView ID="grdReports" runat="server"  
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                    GridLines="None"  Width="100%" OnRowCommand="grdReports_RowCommand"  DataKeyNames="ResourceReportId">
                    <RowStyle BackColor="#EFF3FB"  />
                    <HeaderStyle HorizontalAlign="Left" />
                <Columns>
                   
                    <asp:TemplateField HeaderText="Report Year">
                            <ItemTemplate>
                                <a runat="server" href= '<%# RouteController.ResourceReportView((int)Eval("ResourceReportID")) %>' >
                                    <%# (int)Eval("Month") + "/" + (int)Eval("Year")%>
                                </a> 
                            </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Grantee Agency Reporting">
                            <ItemTemplate>
                               <asp:Label ID="lblGranteeName" runat="server" Text='<%#  Eval("StateGranteeName").EncodeHtml() %>' ></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton Visible='<%# IsAdmin && (Scope == Scope.CMS || Scope == Scope.State) %>' ID="lnkbtnDelete" runat="server" 
                                    CommandName="DeleteReport" >Delete</asp:LinkButton>
                            </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:LinkButton Visible='<%# IsAdmin && (Scope == Scope.CMS || Scope == Scope.State) %>' ID="lnkbtnEdit" runat="server" 
                                    CommandName="EditReport" >Edit</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>                        
                </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" Font-Size="X-Small" ForeColor="White" 
                        HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </td>
        <asp:Label ID="msgFeedBack" runat="server" Text="No uploaded reports found." Visible="false"  ForeColor="Red"></asp:Label>
    </tr>
    
</table>
</div> 
 <pp:ObjectContainerDataSource ID="dataSourceViewResourceReports" runat="server" DataObjectTypeName="System.Data.DataTable" />          
</asp:Content>
