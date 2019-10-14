<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ShiptalkWeb.Master" CodeBehind="SubStateRegionEditForReport.aspx.cs" Inherits="NPRRebuild.ShiptalkWeb.NPRReports.SubStateRegionEditForReport" %>

<%@ Import Namespace="ShiptalkWeb" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet" Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet" TagPrefix="pp" %>
<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <title>Edit Sub State Region For Report</title>


    <script type="text/javascript" src="../../../scripts/jquery-ui-mask-1.7.2.js"></script>

    <script type="text/javascript" src="../../../scripts/jquery.charcounter.js"></script>

    <script type="text/javascript">
        
        $(document).ready(function() {
            //Initialize the Service Area select lists.
            //$("#serviceAreaSelection").selectLists();

            var srcListBox = $("#serviceAreaSelection .src").find()
            var src = $("#serviceAreaSelection .src");
            var dst = $("#serviceAreaSelection .dst");
            var add = $("#serviceAreaSelection .add");
            var remove = $("#serviceAreaSelection .remove");
            var inputValues = $("#serviceAreaSelection input[type='hidden']");

            add.click(function() {
                src.find("option:selected").appendTo(dst);
                //keep the listvalues updated.
                updateValues();
            });

            remove.click(function() {
                dst.find("option:selected").appendTo(src);
                //keep the listvalues updated.
                updateValues();
            });

            function updateValues() {
                var values = new Array();

                dst.find("option").each(function() {
                    values.push($(this).val());
                })

                inputValues.val(values);
            }

            function upateDstValues() {
                var values = inputValues.val().split(',');

                for (i = 0; i <= values.length; i++)
                    src.find("option[value='" + values[i] + "']").appendTo(dst);
            }

            //rmove all the items in dst first.
            dst.find("option").remove();
            upateDstValues();
            //set the width.
            dst.width(src.width());
            src.width(dst.width());

            //Apply a masks for input controls.
            $('.phone').mask({ mask: '(999) 999-9999 x9999', allowPartials: true });
            $('.zip').mask({ mask: '99999-9999', allowPartials: true });
            //Apply a char restrictor to comments.
            $('.comment').charCounter(300,
            {
                container: "<div></div>",
                classname: "counter"
            });
        });
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontent">
        <div class="dv3col">
            <h1>
                Edit Sub State Region For Report
                        <%# Eval("SubStateRegionName").EncodeHtml()%> </h1>
            <div>
                <asp:FormView runat="server" ID="formViewSubStateRegionForReport" 
                DataSourceID="dataSourceSubStateRegionForReport" DataKeyNames="Id" DefaultMode="Edit" Width="100%">
                    <EditItemTemplate>
                        <p>
                            (Items marked in <span class="required">*</span> indicate required fields.)</p>
                            <div id="dv3colFormContent" class="section">
                        <table class="formTable">
                            <tbody>
                                <tr>
                                    <td>
                                        <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelState" AssociatedControlID="dropDownListState">State:</asp:Label>
                                    </td>
                                    <td>
                                      <pp:PropertyProxyValidator ID="proxyValidatorState" runat="server" Display="Dynamic"
                                        DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditSubStateRegionForReportViewData"
                                        PropertyName="State" ControlToValidate="dropDownListState" RulesetName="Data"
                                        CssClass="validationMessage" OnValueConvert="proxyValidatorState_ValueConvert"/> 
                                                    
                                        <asp:DropDownList runat="server" ID="dropDownListState" CssClass="dropdown1wm" DataTextField="Value" DataValueField="Key" DataSource='<%# States %>' 
                                        SelectedValue='<%# (DefaultState.StateAbbr == "CM") ? string.Empty : DefaultState.StateAbbr %>' 
                                        AutoPostBack="true" OnSelectedIndexChanged="dropDownListState_SelectedIndexChanged" Enabled='<%# (Scope == Scope.CMS && IsAdmin) %>' 
                                        AppendDataBoundItems="true">
                                         <asp:ListItem Text="-- Select State --" Value="" />
                                        </asp:DropDownList>
                                        <br />
                                    </td>
                                </tr>
                                
                                 <tr>
                                    <td>
                                        <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="label1" AssociatedControlID="dropDownListFormType">Form Type:</asp:Label>
                                    </td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="PropertyProxyValidatorReprotFormType" runat="server" Display="Dynamic"
                                        DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditSubStateRegionForReportViewData"
                                        PropertyName="ReprotFormType" ControlToValidate="dropDownListFormType" RulesetName="Data"
                                        CssClass="validationMessage" OnValueConvert="PropertyProxyValidatorReprotType_ValueConvert"/> 
                                        
                                        
                                        <asp:DropDownList runat="server" ID="dropDownListFormType" DataTextField="Value" CssClass="dropdown1wm" DataValueField="Key" 
                                        
                                         AutoPostBack="true" OnSelectedIndexChanged="dropDownListFormType_SelectedIndexChanged">
                                            <asp:ListItem Text="-- Select Form Type --" Value="0" />

                                        </asp:DropDownList>                                   
                                     </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="label2" AssociatedControlID="dropDownListGroupype">Group Type:</asp:Label>
                                    </td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="PropertyProxyValidator1" runat="server" Display="Dynamic"
                                        DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditSubStateRegionForReportViewData"
                                        PropertyName="Type" ControlToValidate="dropDownListGroupype" RulesetName="Data"
                                        CssClass="validationMessage" OnValueConvert="PropertyProxyValidatorReprotType_ValueConvert"/> 
                                            
                                    <asp:DropDownList runat="server" ID="dropDownListGroupype" DataTextField="Value" CssClass="dropdown1wm" DataValueField="Key" 
                                     
                                    AutoPostBack="true" OnSelectedIndexChanged="dropDownListGroupType_SelectedIndexChanged">
                                            <asp:ListItem Text="-- Select Report Group Type --" Value="0" />
                                          
                                        </asp:DropDownList>                                   
                                     </td>
                                </tr>                                
                                <tr>
                                    <td>
                                        <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelName" AssociatedControlID="textBoxName">Sub State Region Report Name:</asp:Label>
                                    </td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="proxyValidatorName" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditSubStateRegionForReportViewData" PropertyName="SubStateRegionName" ControlToValidate="textBoxName" RulesetName="Data" CssClass="validationMessage" />
                                        <asp:TextBox runat="server" ID="textBoxName" MaxLength="100" Text='<%# Bind("SubStateRegionName") %>' CssClass="textfield3"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="labelServiceEntityCode" AssociatedControlID="textBoxServiceEntityCode">Sub State Region Service Entity Code:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxServiceEntityCode" MaxLength="20" Text='<%# Bind("SubStateRegionServiceEntityCode") %>' CssClass="textfield3"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span class="required">*</span>&nbsp;Service Areas:
                                    </td>
                                    <td>
                                        <div id="serviceAreaSelection">
                                            <asp:CustomValidator runat="server" ID="validatorServiceAreas" ControlToValidate="dropDownListState" ErrorMessage="Service Areas are required." OnServerValidate="validatorServiceAreas_ServerValidate" Display="Dynamic" CssClass="validationMessage" />
                                            <table id="serviceAreas">
                                                    <tr>
                                                        <td>
                                                            <asp:Label runat="server" ID="labelCounties" AssociatedControlID="listBoxCounties" Text="Counties/Zip Codes/Agencies"></asp:Label>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="labelServiceAreas" AssociatedControlID="listBoxServiceAreas" Text="Service Areas"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td rowspan="2">
                                                            <%--Bind the datasource to the control on the fly. DataSource='<%# DynamicSubStateRegions %>' DataSource='<%# DynamicSubStateRegions %>'--%>
                                                            <asp:ListBox ID="listBoxCounties" runat="server" SelectionMode="Multiple" DataTextField="Value" DataValueField="Key"  CssClass="src dropdown1wm" Height="150px" Width="150px"></asp:ListBox>
                                                        </td>
                                                        <td style="text-align: center; vertical-align: bottom;">
                                                            <input id="buttonAddServiceArea" type="button" value=">>" class="add formbutton1" />
                                                            
                                                        </td>
                                                        <td rowspan="2">
                                                            <asp:ListBox ID="listBoxServiceAreas" runat="server" SelectionMode="Multiple" CssClass="dst dropdown1wm" Height="150px" Width="150px" DataTextField="Value" DataValueField="Key" ></asp:ListBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center; vertical-align: top;">
                                                        <input id="buttonRemoveServiceArea" type="button" value="<<" class="remove formbutton1" />
                                                        </td>
                                                    </tr>
                                            </table>
                                            <asp:HiddenField runat="server" ID="hiddenServiceAreas" Value='<%# Bind("ServiceAreas") %>' />
                                        </div>
                                    </td>
                                </tr>
                                
                                 <tr>
                                    <td></td>
                                    <td></td>
                                    <td>
                                        <asp:Button runat="server" ID="buttonSubmit" Text="Submit" CssClass="formbutton1a" OnClick="buttonSubmit_Click" CausesValidation="false" />   
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        
                        </div>
                    </EditItemTemplate>
                </asp:FormView>
            </div>
        </div>
    </div>
    <pp:ObjectContainerDataSource ID="dataSourceSubStateRegionForReport" runat="server" 
    DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.EditSubStateRegionForReportViewData" 
     OnSelecting="dataSourceEditSubStateRegionForReport_Selecting" 
     OnUpdating="dataSourceEditSubStateRegionForReport_Updating" 
    OnUpdated="dataSourceEditSubStateRegionForReport_Updated" />
</asp:Content>
