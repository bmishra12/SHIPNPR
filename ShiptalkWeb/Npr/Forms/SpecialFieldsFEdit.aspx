<%@ Page Title="" Language="C#" MasterPageFile="~/ShiptalkWebWide.Master" AutoEventWireup="true" CodeBehind="NPR.Forms.SpecialFieldsFEdit.aspx.cs" Inherits="ShiptalkWeb.SpecialFieldsFEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">


    <script type="text/javascript" src="../../../scripts/jquery-ui-mask-1.7.2.js"></script>

    <script type="text/javascript" src="../../../scripts/jquery.charcounter.js"></script>

    <script type="text/javascript">

        $(document).ready(function() {
            //Apply a masks for input controls.
            $('.datePicker').datepicker({ showButtonPanel: true,
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
     <div>
        <h1>Special Fields Manager</h1>    
    </div>
</div>

    
   
    <table width="100%" cellspacing="10px" >
        <tr>
            <td><strong>Name:</strong></td>
            <td><asp:TextBox ID="TxtName" Enabled="false" runat="server" ToolTip="Name"></asp:TextBox></td>
            <td><strong>State:</strong></td>
            <td><asp:Label ID="lblState" runat="server" ></asp:Label></td>
        </tr>
        <tr>
            <td valign="top"><strong>Start Date:</strong></td>
            <td valign="top">
                <asp:TextBox id="StartTextDate" CssClass="datePicker"  MaxLength="10" width="200px" runat="server" ToolTip="Start Date" /> 
                <asp:Panel ID="pnlInvalidStartDate" runat="server" Visible="false">
                    <asp:Label ID="lblInvalidStartDate" ForeColor="red" runat="server" Text="Start date cannot be past date in past."></asp:Label>
                </asp:Panel> 
             </td>
            
                
                
            <td valign="top"><strong>End Date:</strong></td>
            <td valign="top">
                <asp:TextBox id="EndTextDate" CssClass="datePicker" MaxLength="10" width="200px" runat="server"  ToolTip="End Date"/> 
                
                <asp:Panel ID="pnlInvalidEndDate"  runat="server" Visible="false">
                    <asp:Label ID="lblInvalidENdDate" ForeColor="red" runat="server" Text="End date must come after today's date." ></asp:Label>
                </asp:Panel>
             
             </td>
            
        </tr>
        <tr>
            <td><strong>Description:</strong></td>
            <td colspan="3"><asp:Textbox ID="TxtDescription"   MaxLength="100" runat="server" Width="620px"  ToolTip="Description"></asp:Textbox></td>
            <td></td>
            <td></td>

        </tr>
        <tr>
                
             <td><strong>Validation Type:</strong></td>
            <td><asp:DropDownList ID="ddlValidationType" Width="200px" runat="server" ToolTip="Validation Type" >
                <asp:ListItem Text="None"/>
                <asp:ListItem Text="Numeric"/>
                <asp:ListItem Text="AlphaNumeric"/>
                <asp:ListItem Text="Range"/>
                <asp:ListItem Text="Option" />
            </asp:DropDownList></td>
            
                    <td><strong>Is required:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong></td>
                    <td><asp:RadioButtonList ID="rdIsRequired" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Yes" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>

            </td>

        </tr>
         <%--Added by Lavanya Maram: 06/28/2013 --%>
        <tr>
            <td><strong>Validaton Range:</strong></td>
            <td colspan="3">
                <asp:Textbox ID="txtValidationRange" runat="server"  MaxLength="20" Width="100px" ToolTip="Validation Range" ></asp:Textbox><br />                
            </td>
        </tr> 
       <%-- end--%>
        <tr>
            
        </tr>
        <tr>
            <td><strong>Field Type:</strong></td>
            <td>
                <asp:DropDownList ID="ddlFieldType" runat="server" Width="200px" AutoPostBack="true" ToolTip="Field Type">
                    <asp:ListItem Text="--Select Field Type--" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="NATIONWIDE"></asp:ListItem>
                    <asp:ListItem Text="STATE"></asp:ListItem>
                </asp:DropDownList>
            </td>
            
            <td><strong>Form Type:</strong></td>
            <td><asp:Label ID="lblFormType" runat="server" Width="200px" /></td>
            
        </tr>
        <tr>
            <td align="right" colspan="4">
            <asp:Button ID="btnSave" runat="server" CssClass="formbutton1" Text="Save"  
                    Width="100px" onclick="btnSave_Click"/>
            </td>
            
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="lblFeedBack" runat="server" ForeColor="Red" ></asp:Label>                    
            </td>
        </tr>
    </table>
                        
   
   
    

</asp:Content>
