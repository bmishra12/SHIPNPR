<%@ Page Title="" Language="C#" MasterPageFile="~/ShiptalkWebWide.Master" AutoEventWireup="true" CodeBehind="CCFSearch.aspx.cs" Inherits="ShiptalkWeb.Npr.Forms.CCFSearch" %>

<%@ Import Namespace="ShiptalkWeb" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects" %>
<%@ Import Namespace="ShiptalkWeb.Routing" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <%-- <base href="<%= BaseUrl %>" />--%>
    <title>Client Contact</title>
    <link href="../../../css/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../../../scripts/jquery-ui-mask-1.7.2.js"></script>

    <script type="text/javascript" src="../../../../scripts/jquery.charcounter.js"></script>

    <%--<script type="text/javascript">

        function initialize(isPageLoad) {

            $('.datePicker').datepicker({ showButtonPanel: true,
                onSelect: function() { },
                onClose: function(dateText, inst) { $(this).focus(); },
                changeMonth: true,
                changeYear: true,
                showOn: 'button',
                buttonImage: '../../../css/images/calendar.gif',
                buttonImageOnly: true
            }).mask({ mask: '19/99/9999', allowPartials: true }).width(175);

        }

    
        function endRequest(sender, args) {
            $(document).ready(function() { initialize(false); });
        }

        function pageLoad() {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);
            $(document).ready(function() { initialize(true); });
        }
        
    </script>--%>

</asp:Content>
<asp:Content ID="contentBody" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontentwide">
        <div class="dv5col">
            <h1>
                Search Client Contacts</h1>
            <div>
                <div class="commands">
                    <a runat="server" id="aAddCCF" href='<%# RouteController.CcfAdd() %>'>Add a Contact for a New Client With No Prior Service at This Agency</a>
                    <br />
                    <a runat="server" id="aAddSpecialField"  visible="false" href='<%# RouteController.SpeciaFieldsSearch("1") %>'>Add a Special Field</a>
                    <br />
                    <asp:LinkButton runat="server"  ID="linkButtonListRecentClientContacts" OnClick="linkButtonListRecentClientContacts_Click">List Recent Client Contacts</asp:LinkButton>
                    <br />
                     <a runat="server" id="a1" href="~/Npr/Docs/Chapter_2_NPR_Client_Form_508.pdf"  target='_blank' > <b>NPR User Manual: Client Contact Form</b></a>
                </div>
                <br />
                <asp:Panel runat="server" ID="panelSearchCriteria">
                    <table width="100%">
                        <tr>
                        <td class="tdFormLabel" style="width: 140px;">
                            <asp:Label runat="server" ID="labelSearchKeywords" Text="Search Keywords:" ToolTip="Search Keywords" AssociatedControlID="textBoxSearchKeywords" />
                        </td>
                        <td colspan="2">
                            <asp:TextBox runat="server" ID="textBoxSearchKeywords" Style="width: 550px;"  CssClass="textfield3wm" />
                            <asp:TextBoxWatermarkExtender ID="textBoxSearchKeywordsWatermarkExtender" runat="server" TargetControlID="textBoxSearchKeywords" WatermarkText="Enter Client’s Name, NPR Client ID, Agency-State-Specific Client ID, or Representative Name" WatermarkCssClass="textfield3wm" />
                        </td>
                        </tr>
                        <tr>
                            <td class="tdFormLabel">
                                Contact Date Range:
                            </td>
                            <td>
                                <asp:Label runat="server" ID="labelFromDateOfContact" AssociatedControlID="textBoxFromDateOfContact" Text="From:" ToolTip="From Contact Date." />
                                <br />
                                <asp:TextBox runat="server" ID="textBoxFromDateOfContact" CssClass="datePicker textfield3wm" Style="width: 100px;" />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="labelToDateOfContact" AssociatedControlID="textBoxToDateOfContact" Text="To:" ToolTip="To Contact Date." />
                                <br />
                                <asp:TextBox runat="server" ID="textBoxToDateOfContact" CssClass="datePicker textfield3wm" Style="width: 100px;" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdFormLabel">
                                <asp:Label runat="server" ID="labelState" AssociatedControlID="dropDownListState">State:</asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:DropDownList runat="server" ID="dropDownListState" DataTextField="Value" DataValueField="Key" AppendDataBoundItems="true" SelectedValue='<%# DefaultState.StateAbbr %>' Enabled='<%# Scope == Scope.CMS || Scope == Scope.CMSRegional %>' DataSource='<%# States %>' CssClass="dropdown1wm" AutoPostBack="True" OnSelectedIndexChanged="dropDownListState_SelectedIndexChanged">
                                    <asp:ListItem Text="-- Select a State --" Value="CM" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdFormLabel">
                                <asp:Label runat="server" ID="labelAgency" AssociatedControlID="dropDownListAgency">Agency:</asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:DropDownList runat="server" ID="dropDownListAgency" DataTextField="Value" DataValueField="Key" Style="width: 475px;" CssClass="dropdown1wm" AutoPostBack="true" OnSelectedIndexChanged="dropDownListAgency_SelectedIndexChanged" Enabled='<%# Scope.IsLowerOrEqualTo(Scope.State) %>'>
                                        <asp:ListItem Text='-- All of my agencies --' Value="0" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdFormLabel">
                                <asp:Label runat="server" ID="labelCounselor" Text="Counselor:" ToolTip="Counselor" AssociatedControlID="dropDownListCounselor" />
                            </td>
                            <td colspan="2">
                                <asp:DropDownList runat="server" ID="dropDownlistCounselor" DataSource='<%# Counselors %>' DataTextField="Value" DataValueField="Key" Style="width: 550px;" CssClass="dropdown1wm" AppendDataBoundItems="true" OnDataBound="dropDownlistCounselor_DataBound" Enabled='<%# Scope.IsLowerOrEqualTo(Scope.State) %>' >
                                    <asp:ListItem Text="-- All counselors --" Value="0" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdFormLabel">
                                <asp:Label runat="server" ID="labelSubmitter" Text="Submitter:" ToolTip="Submitter" AssociatedControlID="dropDownlistSubmitter" />
                            </td>
                            <td colspan="2">
                                <asp:DropDownList runat="server" ID="dropDownlistSubmitter" DataSource='<%# Submitters %>' DataTextField="Value" DataValueField="Key" Style="width: 550px;" CssClass="dropdown1wm" AppendDataBoundItems="true" OnDataBound="dropDownlistSubmitter_DataBound" Enabled='<%# Scope.IsLowerOrEqualTo(Scope.State) %>'>
                                    <asp:ListItem Text="-- All submitters --" Value="0" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="2">
                                <asp:Button runat="server" ID="buttonSearch" Text="Search" ToolTip="Search" OnClick="buttonSearch_Click" CssClass="formbutton1a" />
                            </td>
                        </tr>

                    </table>
                </asp:Panel>
                <div id="dv5col">
                    <asp:Panel runat="server" ID="panelNoResults" Visible="false">
                       
                        <%
                            if (!IsPostBack)
                            {
                        %>
                          <p style="text-align: center; color: Blue; font-weight:  bold ; ">
                           The Search tool will display the ten records that best match your search criteria. If you have not selected a date range, your search will be limited to last three months.
                                 </p>
                         <p style="text-align: center; color: Blue;">
                           To view your recent Client Contact entries, please click on the “List Recent Client Contacts” link located at the top of this page on the right-hand side.
                                 </p>
                        <%
                            }
                            else
                            {  %>
                             <p style="text-align: center; color: Red;">
                            No Client Contacts were found for the search criteria you provided.
                              </p>
                        <%  } %>
                            
                   
                    </asp:Panel>
                    <asp:ListView runat="server" ID="listViewSearchClientContacts" DataSourceID="dataSourceSearchClientContacts" ItemPlaceholderID="placeHolder" DataKeyNames="Id">
                        <LayoutTemplate>
                            <table id="searchResults" class="dataTable">
                                <thead>
                                    <tr>
                                        <th scope="col" style="width: 160px;">
                                            Client Name
                                        </th>
                                        <th scope="col" style="width: 160px;">
                                            State-Agency-Specific Client ID
                                        </th>
                                        <th scope="col" style="width: 100px;">
                                            Contact Date
                                        </th>
                                        <th scope="col" style="width: 180px;">
                                            &nbsp;
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
                                <td valign="top">
                                    <%# Eval("ClientLastName").EncodeHtml() %>,
                                    <%# Eval("ClientFirstName").EncodeHtml() %>
                                </td>
                                <td scope="row" valign="top">
                                    <%# Eval("StateSpecificClientId").EncodeHtml().TrimEnd()%>
                                </td>
                                <td scope="row" valign="top">
                                    <%# Eval("DateOfContact", "{0:MM/dd/yyyy}").EncodeHtml()%>
                                </td>
                                <td valign="top">
                                    <a runat="server" href='<%# RouteController.CcfView(Convert.ToInt32(Eval("Id"))) %>' title="View this Client Contact.">View</a> | <a runat="server" href='<%# RouteController.CcfEdit(Convert.ToInt32(Eval("Id"))) %>' title="Edit this Client Contact.">Edit</a> | <a runat="server" href='<%# RouteController.CcfAddSimilarContact(Convert.ToInt32(Eval("Id"))) %>' title="Add another Contact using this Client.">New contact for this client</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                    <div style="text-align: center;">
                        <asp:DataPager ID="pager" runat="server" PageSize="25" PagedControlID="listViewSearchClientContacts">
                            <Fields>
                                <asp:NumericPagerField NextPageText="Next" PreviousPageText="Previous" />
                            </Fields>
                        </asp:DataPager>
                    </div>
                </div>
            </div>
        </div>
    </div>
        <pp:ObjectContainerDataSource ID="dataSourceSearchClientContacts" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.SearchClientContactsViewData" OnSelecting="dataSourceSearchClientContacts_Selecting" />
</asp:Content>
