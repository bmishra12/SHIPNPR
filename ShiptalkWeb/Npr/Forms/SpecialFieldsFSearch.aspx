<%@ Page Title="" Language="C#" MasterPageFile="~/ShiptalkWebWide.Master" AutoEventWireup="true" CodeBehind="SpecialFieldsFSearch.aspx.cs" Inherits="ShiptalkWeb.Npr.Forms.SpecialFieldsFSearch" %>
<%@ Import Namespace="ShiptalkWeb.Routing" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects.UI" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects" %>
<%@ Import Namespace="ShiptalkWeb" %>
<%@ Import Namespace="System.Data" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">



    <script type="text/javascript" src="../../../scripts/jquery-ui-mask-1.7.2.js"></script>

    <script type="text/javascript" src="../../../scripts/jquery.charcounter.js"></script>

   <script type="text/javascript">
   
        $(document).ready(function() {
            //Apply a masks for input controls.
        $("[id$='_TxtStartDT']").datepicker({ showButtonPanel: true,
                onClose: function(dateText, inst) { $(this).focus(); },
                changeMonth: true,
                changeYear: true,
                showOn: 'button',
                buttonImage: '../../../css/images/calendar.gif',
                buttonImageOnly: true
            }).mask({ mask: '19/99/9999', allowPartials: true }).width(175);


            $("[id$='_TxtEndDT']").datepicker({ showButtonPanel: true,
                onClose: function(dateText, inst) { $(this).focus(); },
                changeMonth: true,
                changeYear: true,
                showOn: 'button',
                buttonImage: '../../../css/images/calendar.gif',
                buttonImageOnly: true
            }).mask({ mask: '19/99/9999', allowPartials: true }).width(175);

        });
       
    
</script>



<div class="dv5col">
        <h1>Special Fields Manager</h1>    
    <asp:Label ID="lblFeedBack" runat="server" ForeColor="Red"></asp:Label>
    <table width="100%">
        <tr>
            
            <td  align="right" colspan="5">
                   <asp:Button ID="btnSearch" runat="server"  CssClass="formbutton1" 
                       Width="100px"  Text="Search" onclick="btnSearch_Click"/> <br/><br/>
                   <asp:Button ID="btnAdd" runat="server"  CssClass="formbutton1" 
                       Width="100px"  Text="Add" onclick="btnAdd_Click" /> 
            </td>
        </tr>
        <tr>
            <td  valign="top" id="cStates" runat="server" visible="false">
                <br />State&nbsp;&nbsp;&nbsp;<asp:Label ID="lblState" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlStates" runat="server" Width="140px" ToolTip="State"></asp:DropDownList>
           </td>
            <td valign="top">
                <strong>Search Filter Start Date:</strong><br/>
                <asp:TextBox ID="TxtStartDT" runat="server" Width="200px" ToolTip="Search Filter Start Date"></asp:TextBox>
                
            </td>
            <td>
                <strong>Search Filter End Date:</strong><br/>
                <asp:TextBox ID="TxtEndDT" runat="server" Width="200px" ToolTip="Search Filter End Date"></asp:TextBox>
            
             </td>
             <td>
                    <div style="position:relative; top:12%">
                  <asp:Button ID="btnClear" runat="server" Text="Clear Filter" 
                    CssClass="formbutton1" onclick="btnClear_Click" />
                    </div>
            </td>
            
            
        </tr>
        <tr>
            <td colspan="5">
                <asp:GridView ID="grdFields" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" 
                    onrowdatabound="grdFields_RowDataBound" Width="100%" 
                    onrowcommand="grdFields_RowCommand">
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID">
                        <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Name" HeaderText="Field Name">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="StartDate" HeaderText="Start Date">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EndDate" HeaderText="End Date">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Ordinal" HeaderText="Special Field Type">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:ButtonField HeaderText="Edit" Text="Edit" HeaderStyle-HorizontalAlign="Left" CommandName="FIELD_EDIT"/>
                        
                        <asp:ButtonField HeaderText="Delete" Text="Delete" HeaderStyle-HorizontalAlign="Left" CommandName="FIELD_DELETE" />
                    </Columns>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
              </td>
        </tr>
    </table>
</div>
</asp:Content>
