<%@ Page Title="" Language="C#" MasterPageFile="~/ShiptalkWeb.Master" AutoEventWireup="true" CodeBehind="SubStateRegionAdd.aspx.cs" Inherits="ShiptalkWeb.Agency.SubStateRegionAdd" %>

<%@ Import Namespace="ShiptalkWeb.Routing" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects.UI" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects" %>
<%@ Import Namespace="ShiptalkWeb" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet" Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet" TagPrefix="pp" %>
<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <title>Add a Sub-State Region</title>



    <script type="text/javascript" src="../../../scripts/jquery-ui-mask-1.7.2.js"></script>

    <script type="text/javascript" src="../../../scripts/jquery.charcounter.js"></script>

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
            <h1>
                Add a Sub-State Region</h1>
            <div>
                <asp:FormView runat="server" ID="formViewSubStateRegion" DataSourceID="dataSourceSubStateRegion" DefaultMode="Insert" Width="100%">
                    <InsertItemTemplate>
                        <p>
                            (Items marked in <span class="required">*</span> indicate required fields.)</p>
                        <div id="dv3colFormContent" class="section">
                            <table class="formTable">
                                <tbody>
                                    <tr>
                                        <td class="tdFormRequiredLabel"></td>
                                        <td class="tdFormLabel">
                                            State:
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="dropDownListState" CssClass="dropdown1wm" DataTextField="Value" DataValueField="Key"
                                             ToolTip="State" DataSource='<%# States %>' SelectedValue='<%# (DefaultState.StateAbbr == "CM") ? string.Empty : DefaultState.StateAbbr %>' AutoPostBack="true" OnSelectedIndexChanged="dropDownListState_SelectedIndexChanged" Enabled='<%# (Scope == Scope.CMS && IsAdmin) %>' AppendDataBoundItems="true">
                                                <asp:ListItem Text="-- Select State --" Value="" />
                                            </asp:DropDownList>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><span class="required">*</span></td>
                                        <td>
                                            <asp:Label runat="server" ID="labelName" AssociatedControlID="textBoxName">Sub-State Region Name:</asp:Label>
                                        </td>
                                        <td>
                                            <pp:PropertyProxyValidator ID="proxyValidatorName" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddSubStateRegionViewData" PropertyName="Name" ControlToValidate="textBoxName" RulesetName="Data" CssClass="validationMessage" />
                                            <asp:TextBox runat="server" ID="textBoxName" CssClass="textfield3" MaxLength="100" Text='<%# Bind("Name") %>'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><span class="required">*</span></td>
                                        <td>
                                            Sub-State Region Agencies:
                                        </td>
                                        <td>
                                            <div id="agenciesSelection">
                                            <asp:CustomValidator runat="server" ID="validatorRegionAgencies" ControlToValidate="dropDownListState" ErrorMessage="Sub-State Region Agencies are required." OnServerValidate="validatorRegionAgencies_ServerValidate" Display="Dynamic" CssClass="validationMessage" />
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
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:Button runat="server" ID="buttonSubmit" Text="Submit" CssClass="formbutton1a" OnClick="buttonSubmit_Click" CausesValidation="false" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </InsertItemTemplate>
                </asp:FormView>
                <div id="dv3colFormContent" class="section">
                    <h4>
                        Sub-State Regions</h4>
                        <br />
                    <asp:Panel runat="server" ID="panelNoResults" Visible="false">
                        <p style="text-align: center;">
                            No Sub-State Regions were found.
                        </p>
                    </asp:Panel>
                    <asp:ListView runat="server" ID="listViewSubStateRegions" DataSourceID="dataSourceSubStateRegions" ItemPlaceholderID="placeHolder" DataKeyNames="Id">
                        <LayoutTemplate>
                            <table class="dataTable">
                                <thead>
                                    <tr>
                                        <th scope="col" style="width: 200px;">
                                            Name
                                        </th>
                                        <th scope="col" nowrap>
                                            State
                                        </th>
                                        <th scope="col" nowrap>
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr runat="server" id="placeHolder">
                                    </tr>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td scope="row" valign="top">
                                    <%# Eval("Name").EncodeHtml() %>
                                </td>
                                <td style="width: 125px; vertical-align: top;">
                                    <%# ((State)Eval("State")).StateName %>
                                </td>
                                <td style="width: 75px; vertical-align: top;">
                                    <a runat="server" href='<%# RouteController.AgencyRegionView(Convert.ToInt32(Eval("Id"))) %>' title="View this Sub-State Region.">View</a>
                                    |
                                    <a runat="server" href='<%# RouteController.AgencyRegionEdit(Convert.ToInt32(Eval("Id"))) %>' title="Edit this Sub-State Region.">Edit</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>
    </div>
    <pp:ObjectContainerDataSource ID="dataSourceSubStateRegion" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.AddSubStateRegionViewData" OnInserting="dataSourceSubStateRegion_Inserting" OnInserted="dataSourceSubStateRegion_Inserted" />
    <pp:ObjectContainerDataSource ID="dataSourceSubStateRegions" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.SearchSubStateRegionsViewData" OnSelecting="dataSourceSubStateRegions_Selecting" />
</asp:Content>
