<%@ Page Language="C#" MasterPageFile="~/ShiptalkWeb.Master" AutoEventWireup="true" CodeBehind="DesignateUserApprover.aspx.cs" Inherits="ShiptalkWeb.User.Admin.DesignateUserApprover" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<title>Designate a User as Approver</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">


 <div id="maincontent">
        <div class="dv3col">
            <h1>Designate an approver for User Registrations</h1>
            The designated User will recieve email notifications upon user registrations for the jursidiction.
            It is possible to have multiple approvers for a jurisdiction.
        </div>
         <div id="dv3colFormContent" runat="server" style="margin-top: 10px; width: 615px;
                border-top: solid 2px #eee;">
                
                <div>
                    Table A.<br />
                    Here are the existing approvers for --jurisdiction--<br />
                    --table for users in jursdiction who could be removed as designate--
                </div>
                <div>
                    <br />
                    --table for users in jursdiction who could be selected as the designate--<br />
                    --The list contains users who are not in table A.
                </div>
                
         </div>
</div>        

</asp:Content>
