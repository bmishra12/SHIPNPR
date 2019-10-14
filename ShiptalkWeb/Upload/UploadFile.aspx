<%@ Page Language="C#" MasterPageFile="../ShiptalkWebWide.Master" AutoEventWireup="true" CodeBehind="UploadFile.aspx.cs" Inherits="ShiptalkWeb.Upload.UploadFile" %>

<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <title>File Upload</title>


    <link href="../../../css/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />


    <script type="text/javascript">
    
          $(document).ready(function() {


          })


          function disableButtons()
          {
              //alert("trying to disable");
              document.getElementById('<%= btnValidate.ClientID %>').style.visibility = 'hidden'
              document.getElementById('<%= btnValidate.ClientID %>').style.display = 'none'

             // document.getElementById('<%= btnValidate.ClientID %>').disabled = true;
              if (document.getElementById('<%= btnProcess.ClientID %>') != null) {
                  document.getElementById('<%= btnProcess.ClientID %>').style.visibility = 'hidden'
                  document.getElementById('<%= btnProcess.ClientID %>').style.display = 'none'
     
              }
              if (document.getElementById('<%= btnViewRecentUploads.ClientID %>') != null) {
                  document.getElementById('<%= btnViewRecentUploads.ClientID %>').style.visibility = 'hidden'
                  document.getElementById('<%= btnViewRecentUploads.ClientID %>').style.display = 'none'
              }

          }
          
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">


    <div class="dv5col" >
     <div>
        <h1>Upload CC or Pam File</h1>    
    </div>
    <span class="smaller">
             <asp:Literal ID="dv3colMessage" runat="server" Text="Upload CC or Pam data file for processing. Files will be uploaded, validated and processed."
                    EnableViewState="false"></asp:Literal>
            </span>
    <div>&nbsp;</div>        
    <div>&nbsp;</div>
    <div style="float:right">
        <asp:LinkButton ID="btnViewRecentUploads" runat="server" 
                     CssClass="formbutton1"  PostBackUrl="#"   
                     Width="125px"  Text="&nbsp;View recent activities" 
                     onclick="btnViewRecentUploads_Click" BorderStyle="None" BackColor="Transparent"  
                       /> 
        
    </div>
    

    <table style="padding:0px; width:100%; height: 30px;">
        <tr>
            <td><asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Select the file to upload" />
            
        </td>
        <td>
            <div runat="server" id="divbtn" style="position:relative; float:left; right:125px">
                <asp:Button ID="btnValidate" runat="server" Width="100px" CssClass="formbutton1"  Height="25px" 
                onclick="btnValidate_Click" Text="Process"   />
                             
            </div>
        </td>
        <td>
            <div>
                <asp:Button ID="btnProcess" runat="server"  Width="100px" CssClass="formbutton1"   
                onclick="btnProcess_Click"  Text="Process"  /> 
            </div>            
        </td>
        <td>
                 <asp:LinkButton ID="btnDownload" runat="server" Visible="false" CssClass="formbutton1"  PostBackUrl="#"   
                     Width="145px"  Text="Download invalid records" onclick="btnDownload_Click"  /> 
                     
                      
        </td>
        </tr>
        <tr>
            <td><span id="lblFeedBack" style="color:Red"  runat="server" ></span></td>
        </tr>
    </table>

    <div style="position:relative; float:left;  top:5px">
        <span id="lblStatus" style="color:Red" runat="server"></span>
            <asp:Panel ID="pnlContent" Width="100%" runat="server" BorderStyle="Solid" BorderWidth="1px" 
                BorderColor="Blue">
                <span id="lblValidation" runat="server" visible="false"></span>
                <asp:GridView ID="grdUploadStatus" runat="server" Visible="False" 
                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                    GridLines="None"  Width="100%" 
                    onrowdatabound="grdUploadStatus_RowDataBound" 
                    onrowcommand="grdUploadStatus_RowCommand" AllowPaging="True" 
                    onpageindexchanging="grdUploadStatus_PageIndexChanging" >
                    <RowStyle BackColor="#EFF3FB" />
                    <HeaderStyle HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="Filename">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbtnFileName" runat="server" 
                                    CommandName="SelectOriginal" >LinkButton</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Error file">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbtnErrorFileName" runat="server" 
                                    CommandName="SelectError" >LinkButton</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="State" HeaderText="State" HeaderStyle-Width="50px" >
                        <HeaderStyle Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FileType" HeaderText="FileType" />
                        <asp:BoundField DataField="ProcessDate" DataFormatString="{0:dd/MM/yyyy}" 
                            HeaderText="Date Processed" HtmlEncode="False" />
                        <asp:BoundField DataField="UserName" HeaderText="Username" />
                        <asp:BoundField DataField="Comments" HeaderText="Comments" />
                        <asp:BoundField DataField="UploadFileName"/>
                        <asp:BoundField DataField="FileUrl"/>
                        <asp:BoundField DataField="FileError"/>
                        <asp:BoundField DataField="InvalidRecords" />
                        <asp:BoundField DataField="Status" />
                        
                        

                        
                    </Columns>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" Font-Size="X-Small" ForeColor="White" 
                        HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                
            </asp:Panel>
            
            </div>
     
        </div>
    
    
</asp:Content>
