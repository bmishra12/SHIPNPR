<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ShiptalkWeb.Master" CodeBehind="AgencySearch.aspx.cs" Inherits="ShiptalkWeb.Agency.AgencySearch" %>

<%@ Import Namespace="ShiptalkWeb" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects" %>
<%@ Import Namespace="ShiptalkWeb.Routing" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>
<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <title>Agency Search</title>


    
<%--    <script type="text/javascript" src="../../scripts/jquery.tablesorter.js"></script>
    
    <script type="text/javascript" src="../../scripts/jquery.tablesorter.pager.js"></script>--%>
    
    <script type="text/javascript">

        $(document).ready(function() {

            //initialize alt row table styles.
            $(".dataTable tr:even").addClass("even");

            //Attach enter key event to the keyword textbox.
            $("[id$='textBoxSearchKeywords']").keypress(function(e) {
                keyCode = e.which ? e.which : e.keyCode;

                if (keyCode == 13) {
                    $(this).blur();
                    e.preventDefault();
                    __doPostBack($("[id$='buttonSearch']").attr("name"), "");
                }
            });

            //Attach a slide animation to display available filters.
            $("#filterResults").click(function(e) {
                e.preventDefault();
                $("#filters").slideToggle(300);
            });
        });
        
    </script>

</asp:Content>
<asp:Content ID="contentBody" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontent">
        <div class="dv3col">
            <h1>
                Find an Agency</h1>
            <div>
                <div class="commands">
                    <a runat="server" id="aAddAgency" Visible='<%# IsAdmin %>' href='<%# RouteController.AgencyRegister() %>'>Add an Agency</a>
                    <br />
                    <a runat="server" id="aAddSubStateRegion" Visible='<%# IsAdmin %>'  href='<%# RouteController.AgencyRegionAdd() %>'>Add a Sub-State Region</a>
                    <br />
                    <asp:LinkButton runat="server" ID="linkButtonListAllAgencies" OnClick="linkButtonListAllAgencies_Click">List all Agencies</asp:LinkButton>
                </div>
                <br />
                <table class="formTable">
                    <tbody>
                        <tr>
                            <td class="tdFormLabel">
                                <asp:Label runat="server" ID="labelSearchKeywords" Text="Search Keywords:" ToolTip="Search Keywords" AssociatedControlID="textBoxSearchKeywords" />
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="textBoxSearchKeywords" Style="width: 400px;" CssClass="textfield3" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdFormLabel">
                                <asp:Label runat="server" ID="label1" Text="Filter by State:" ToolTip="Filter by State" AssociatedControlID="dropDownListState" />
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="dropDownListState" CssClass="dropdown1wm" DataTextField="Value" DataValueField="Key" AppendDataBoundItems="true" Width="200px" SelectedValue='<%# DefaultState.StateAbbr %>' Enabled='<%# Scope == Scope.CMS || Scope == Scope.CMSRegional %>'>
                                    <asp:ListItem Text="Include All States" Value="CM" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button runat="server" ID="buttonSearch" Text="Search" ToolTip="Search" OnClick="buttonSearch_Click" CssClass="formbutton1a" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                
                
                
                <div id="dv3colFormContent" class="section">
                    <asp:Panel runat="server" ID="panelNoResults" Visible="false">
                    <p style="text-align: center; color: Red;">
                            No Agencies were found for the search criteria you provided.
                        </p>
                    </asp:Panel>
                    <asp:ListView runat="server" ID="listViewAgencies" DataSourceID="dataSourceAgencies" ItemPlaceholderID="placeHolder" DataKeyNames="Id">
                        <LayoutTemplate>
                            <table id="searchResults" class="dataTable">
                                <thead>
                                    <tr>
                                        <th scope="col" style="width: 210px;">
                                            Agency
                                        </th>
                                        <th scope="col">
                                            Details
                                        </th>
                                        <th scope="col" style="width: 75px;">
                                            &nbsp;
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr runat="server" id="placeHolder">
                                    </tr>
                                </tbody>
                                <%--<tfoot>
                                    <tr>
                                        <td colspan="3" style="border-top:1px solid #94A8CF;text-align: center;">
                                            <div id="pager" class="pager">
                                                <form>
		                                          
		                                        <a href="#" class="first" title="First">First</a>&nbsp;
		                                        &nbsp;<a href="#" class="prev" title="Previous">Previous</a>&nbsp;
		                                        &nbsp;<input type="text" class="pagedisplay" style="border:0 none;text-align:center;width:30px;"/>&nbsp; 
		                                        &nbsp;<a href="#" class="next" title="Next">Next</a>&nbsp;
		                                        &nbsp;<a href="#" class="last" title="Last">Last</a>
		                                        &nbsp;&nbsp;|&nbsp;&nbsp;
		                                        <label for="selectPageSize">Page Size:</label>
                                                <select id="selectPageSize" class="pagesize"> 
			                                        <option selected="selected"  value="10">5</option> 
			                                        <option value="20">10</option> 
			                                        <option value="30">15</option> 
			                                        <option  value="40">20</option> 
		                                        </select>
		                                        </form>
                                            </div>
                                        </td>
                                    </tr>
                                </tfoot>--%>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td valign="top">
                                    <a runat="server" href='<%# RouteController.AgencyView((int)Eval("Id")) %>' title='<%# string.Format("{0} ({1})", Eval("Name").EncodeHtml(), Eval("Code").EncodeHtml().TrimEnd())  %>'>
                                        <%# Eval("Name").EncodeHtml() %> (<%# Eval("Code").EncodeHtml().TrimEnd() %>)</a>
                                    <br />
                                        <%# ((AgencyType)Eval("Type")).Description() %>
                                </td>
                                <td scope="row" valign="top">
                                    <%# Eval("FormattedAddress") %>
                                    <br />
                                    <%# Eval("PrimaryPhone").EncodeHtml() %>
                                    <br />
                                    Hours:<%# Eval("HoursOfOperation").EncodeHtml() %>
                                    <br /><br />
                                    
                                </td>
                                <td valign="top">
                                    <a id="A1" runat="server" href='<%# RouteController.AgencyView((int)Eval("Id")) %>' title="View this Agency.">View</a>
                                     | 
                                    <a runat="server"  href='<%# RouteController.AgencyEdit((int)Eval("Id")) %>' title="Edit this Agency.">Edit</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                     <div style="text-align: center;">
                        <asp:DataPager ID="pager" runat="server" PageSize="5" PagedControlID="listViewAgencies">
                            <Fields>
                                <asp:NumericPagerField />
                            </Fields>
                        </asp:DataPager>
                    </div>
            </div>
        </div>
    </div>
    </div>
    <pp:ObjectContainerDataSource ID="dataSourceAgencies" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.SearchAgenciesViewData" OnSelecting="dataSourceAgencies_Selecting" />
</asp:Content>
