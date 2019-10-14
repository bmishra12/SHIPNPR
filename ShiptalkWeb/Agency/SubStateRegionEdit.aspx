<%@ Page Title="" Language="C#" MasterPageFile="~/ShiptalkWeb.Master" AutoEventWireup="true" CodeBehind="SubStateRegionEdit.aspx.cs" Inherits="ShiptalkWeb.Agency.SubStateRegionEdit" %>

<%@ Import Namespace="ShiptalkWeb.Routing" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects.UI" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects" %>
<%@ Import Namespace="ShiptalkWeb" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet" Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet" TagPrefix="pp" %>
<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <title>Add a Sub-State Region</title>

    
    <script type="text/javascript">

        $(document).ready(function() {

            var srcListBox = $("#agenciesSelection .src").find()
            var src = $("#agenciesSelection .src");
            var dst = $("#agenciesSelection .dst");
            var add = $("#agenciesSelection .add");
            var remove = $("#agenciesSelection .remove");
            var inputValues = $("#agenciesSelection input[type='hidden']");

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
        });

    </script>

</asp:Content>
<asp:Content ID="contentBody" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontent">
        <div class="dv3col">
                <asp:FormView runat="server" ID="formViewSubStateRegion" DataSourceID="dataSourceSubStateRegion" DataKeyNames="Id, State" DefaultMode="Edit" Width="100%">
                    <EditItemTemplate>
                        <h1>
                        Edit
                        <%# Eval("Name").EncodeHtml() %></h1>
                         <p>
                            (Items marked in <span class="required">*</span> indicate required fields.)</p>
                            <div id="dv3colFormContent" class="section">
                            <table class="formTable" style="width:100%;">
                                <tbody>
                                    <tr>
                                        <td><span class="required">*</span></td>
                                        <td>
                                            <asp:Label runat="server" ID="labelName" AssociatedControlID="textBoxName">Sub-State Region Name:</asp:Label>
                                        </td>
                                        <td>
                                            <pp:PropertyProxyValidator ID="proxyValidatorName" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditSubStateRegionViewData" PropertyName="Name" ControlToValidate="textBoxName" RulesetName="Data" CssClass="validationMessage" />
                                            <asp:TextBox runat="server" ID="textBoxName" MaxLength="100" Text='<%# Bind("Name") %>' CssClass="textfield3"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Label runat="server" ID="labelIsActive" AssociatedControlID="checkBoxIsActive">Is Active:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:CheckBox runat="server" ID="checkBoxIsActive" Checked='<%# Bind("IsActive") %>' Text="" CssClass="checkbox2"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><span class="required">*</span></td>
                                        <td nowrap>
                                            Sub-State Region Agencies:
                                        </td>
                                        <td>
                                            <div id="agenciesSelection">
                                                <table id="agencies">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:Label runat="server" ID="labelAgencies" AssociatedControlID="listBoxAgencies" Text="Agencies"></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="labelRegionAgencies" AssociatedControlID="listBoxRegionAgencies" Text="Sub-State Region Agencies"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td rowspan="2">
                                                                <%--Bind the datasource to the control on the fly.--%>
                                                                <asp:ListBox ID="listBoxAgencies" runat="server" SelectionMode="Multiple" DataTextField="Value" DataValueField="Key" DataSource='<%# Agencies %>' CssClass="src dropdown1wm" Height="150px" Width="150px"></asp:ListBox>
                                                            </td>
                                                            <td style="text-align: center; vertical-align: bottom;">
                                                                <input id="buttonAddAgency" type="button" value=">>" class="add formbutton1" />
                                                            </td>
                                                            <td rowspan="2">
                                                                <asp:ListBox ID="listBoxRegionAgencies" runat="server" SelectionMode="Multiple" CssClass="dst dropdown1wm" Height="150px" Width="150px" DataTextField="Value" DataValueField="Key" DataSource='<%# Agencies %>'></asp:ListBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: center; vertical-align: top;">
                                                                <input id="buttonRemoveAgency" type="button" value="<<" class="remove formbutton1" />
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <asp:HiddenField runat="server" ID="hiddenRegionAgencies" Value='<%# Bind("Agencies") %>' />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:CustomValidator runat="server" ID="validatorRegionAgencies" ControlToValidate="textBoxName" ErrorMessage="Sub-State Region Agencies are required." OnServerValidate="validatorRegionAgencies_ServerValidate" Display="Dynamic" CssClass="required" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2"></td>
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
    <pp:ObjectContainerDataSource ID="dataSourceSubStateRegion" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.EditSubStateRegionViewData" OnUpdated="dataSourceSubStateRegion_Updated" OnSelecting="dataSourceSubStateRegion_Selecting" />
</asp:Content>
