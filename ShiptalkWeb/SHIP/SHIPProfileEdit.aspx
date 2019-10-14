<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ShiptalkWeb.Master" CodeBehind="SHIPProfileEdit.aspx.cs" Inherits="ShiptalkWeb.SHIP.SHIPProfileEdit" %>
<%@ Import Namespace="ShiptalkWeb.Routing" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects.UI" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects" %>
<%@ Import Namespace="ShiptalkWeb" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet" Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet" TagPrefix="pp" %>
<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <title>Edit SHIP Profile</title>      

    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?v=3.exp&amp;sensor=false"></script>

    <script type="text/javascript">

        var geocoder;
        var progAddress;
        var progCity;
        var progState;
        var progZip;
        var progLat;
        var progLng;

        function geocodeAddress() {            
            var Lat;
            var Lng;
            var geocoder = new google.maps.Geocoder();

            var address = $.trim(progAddress.val());
            var city = $.trim(progCity.val());
            var state = $.trim(progState.val());
            var zip = $.trim(progZip.val());

            setTimeout(function () {

                geocoder.geocode({
                    'address': address + ' ' + city + ' ' + state + ' ' + zip
                },
                     function (results, status) {
                         if (status == google.maps.GeocoderStatus.OK) {
                             Lat = results[0].geometry.location.lat();
                             Lng = results[0].geometry.location.lng();

                             progLng.val(Lng);
                             progLat.val(Lat);                           

                         } else {
                             alert("Geocode was not successful for the following reason: " + status);
                         }
                     });

            });

        }

        $(document).ready(function () {       
            progAddress = $("[id$='_textBoxProgramCoordinatorAddress']");
            progCity = $("[id$='_textBoxProgramCoordinatorCity']");
            progState = $("[id$='_hdnState']");
            progZip = $("[id$='textBoxProgramCoordinatorZipcode']");
            progLat = $("[id$='_hdnLat']");
            progLng = $("[id$='_hdnLng']");

            progAddress.blur(geocodeAddress);
            progCity.blur(geocodeAddress);
            progState.blur(geocodeAddress);
            progZip.blur(geocodeAddress);
        })
          
        
    </script>

</asp:Content>
<asp:Content ID="contentBody" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontent">
        <div class="dv3col">
        <table width="100%"  class="formTable">
            <tbody>
                <tr style="display:none">
                    <td>
                        <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelName" AssociatedControlID="ddlStates">State Name:</asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlStates" DataTextField="Value" DataValueField="Key" OnSelectedIndexChanged="ddlStates_SelectedChanged" AutoPostBack="true" AppendDataBoundItems="true" Width="170px" >
                            <asp:ListItem Text="Select a State" Value="0" />
                        </asp:DropDownList>
                        <br />
                    </td>
                </tr>
                 <tr>
                    <td colspan="2">
                        <asp:Label runat="server" ID="MessageBox" Visible="false"></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
            <asp:FormView runat="server"  ID="formView" DefaultMode="Edit" DataKeyNames="ID" DataSourceID="dataSourceEditSHIP" Width="100%">
                <EditItemTemplate>
                 <h1>
                    Edit</h1>
                     <p>
                        (Items marked in <span class="required">*</span> indicate required fields.)</p>
                    <div id="dv3colFormContent" class="section">
                        <table width="100%"  class="formTable">
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label1" AssociatedControlID="textBoxProgramName">Program Name:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxProgramName" MaxLength="250" Text='<%# Bind("ProgramName")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label2" AssociatedControlID="textBoxProgramWebsite">Program Website:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxProgramWebsite" MaxLength="250" Text='<%# Bind("ProgramWebsite")%>'></asp:TextBox><br />
                                       <%-- <asp:RegularExpressionValidator runat="server" ValidationGroup="ShipProfileEdit" SetFocusOnError="true" ControlToValidate="textBoxProgramWebsite" ValidationExpression="((http|https):\/\/w{3}[\d]*.|(http|ftp|https):\/\/|w{3}[\d]*.)([\w\d\._\-#\(\)\[\]\\,;:]+@[\w\d\._\-#\(\)\[\]\\,;:])?([a-z0-9]+.)*[a-z\-0-9]+.([a-z]{2,3})?[a-z]{2,6}(:[0-9]+)?(\/[\/a-z0-9\._\-,]+)*[a-z0-9\-_\.\s\%]+(\?[a-z0-9=%&amp;\.\-,#]+)?" ErrorMessage="Program Website URL is not in a valid format." ID="RevProgramWebsite">
                                        </asp:RegularExpressionValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label3" AssociatedControlID="textBoxProgramSummary">Program Summary:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxProgramSummary" MaxLength="7000" Height="200px" TextMode="MultiLine" Text='<%# Bind("ProgramSummary")%>'></asp:TextBox>
                                    </td>
                                </tr>                                
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label24" AssociatedControlID="textBoxAdminAgencyContactName">Admin Agency Contact Name:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxAdminAgencyContactName" MaxLength="50" Text='<%# Bind("AdminAgencyContactName")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label5" AssociatedControlID="textBoxAdminAgencyName">Admin Agency Name:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxAdminAgencyName" MaxLength="150" Text='<%# Bind("AdminAgencyName")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label25" AssociatedControlID="textBoxAdminAgencyContactTitle">Admin Agency Contact Title:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxAdminAgencyContactTitle" MaxLength="50" Text='<%# Bind("AdminAgencyContactTitle")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label4" AssociatedControlID="textBoxAdminAgencyAddress">Admin Agency Address:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxAdminAgencyAddress" MaxLength="150" Text='<%# Bind("AdminAgencyAddress")%>'></asp:TextBox>
                                        
                                    </td>
                                </tr>
                               <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label26" AssociatedControlID="textBoxAdminAgencyCity">Admin Agency City:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxAdminAgencyCity" MaxLength="80" Text='<%# Bind("AdminAgencyCity")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label7" AssociatedControlID="textBoxAdminAgencyZipcode">Admin Agency Zipcode:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxAdminAgencyZipcode" MaxLength="10" Text='<%# Bind("AdminAgencyZipcode")%>' ></asp:TextBox><br />
                                          <asp:RegularExpressionValidator runat="server" ValidationGroup="ShipProfileEdit" SetFocusOnError="true" ValidationExpression="(^\d{5}$)|(^\d{5}-\d{4}$)" ControlToValidate="textBoxAdminAgencyZipcode" ErrorMessage="Admin Agency Zipcode entered must be in the format 99999-9999" ID="RevAdminAgencyZipcode">
                                        </asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label6" AssociatedControlID="textBoxAdminAgencyPhone">Admin Agency Phone:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxAdminAgencyPhone" MaxLength="20" Text='<%#  Bind("AdminAgencyPhone")%>'></asp:TextBox><br />
                                         <asp:RegularExpressionValidator runat="server" ValidationGroup="ShipProfileEdit" SetFocusOnError="true" ControlToValidate="textBoxAdminAgencyPhone" ValidationExpression="^\(?[\d]{3}\)?[\s-]?[\d]{3}[\s-]?[\d]{4}?(([\s-]?(x)?[\d]{1,4})?)$" ErrorMessage="Admin Agency Phone entered must be in the format 999-999-9999" ID="RevAdminAgencyPhone">
                                        </asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label27" AssociatedControlID="textBoxAdminAgencyFax">Admin Agency Fax:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxAdminAgencyFax" MaxLength="20" Text='<%#  Bind("AdminAgencyFax")%>'></asp:TextBox><br />
                                         <asp:RegularExpressionValidator runat="server" ValidationGroup="ShipProfileEdit" SetFocusOnError="true" ControlToValidate="textBoxAdminAgencyFax" ValidationExpression="^\(?[\d]{3}\)?[\s-]?[\d]{3}[\s-]?[\d]{4}?(([\s-]?(x)?[\d]{1,4})?)$" ErrorMessage="Admin Agency Fax entered must be in the format 999-999-9999" ID="RevAdminAgencyFax">
                                        </asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label28" AssociatedControlID="textBoxAdminAgencyEmail">Admin Agency Email:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxAdminAgencyEmail" MaxLength="150" Text='<%#  Bind("AdminAgencyEmail")%>'></asp:TextBox><br />
                                        <asp:RegularExpressionValidator runat="server" ValidationGroup="ShipProfileEdit" SetFocusOnError="true" ControlToValidate="textBoxAdminAgencyEmail" ValidationExpression="^[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.(([0-9]{1,3})|([a-zA-Z]{2,3})|(aero|coop|info|museum|name))$" ErrorMessage="Admin Agency Email is not in a valid format." ID="RevAdminAgencyEmail">
                                        </asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label8" AssociatedControlID="textBoxAvailableLanguages">Available Languages:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxAvailableLanguages" MaxLength="256" Text='<%# Bind("AvailableLanguages")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label9" AssociatedControlID="textBoxBeneficiaryContactEmail">Beneficiary Contact Email:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxBeneficiaryContactEmail" MaxLength="150" Text='<%#  Bind("BeneficiaryContactEmail")%>'></asp:TextBox><br />
                                         <asp:RegularExpressionValidator runat="server" ValidationGroup="ShipProfileEdit" SetFocusOnError="true" ControlToValidate="textBoxBeneficiaryContactEmail" ValidationExpression="^[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.(([0-9]{1,3})|([a-zA-Z]{2,3})|(aero|coop|info|museum|name))$" ErrorMessage="Beneficiary Contact Email is not in a valid format." ID="revBeneficiaryContactEmail">
                                        </asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label10" AssociatedControlID="textBoxBeneficiaryContactHours">Beneficiary Contact Hours:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxBeneficiaryContactHours" MaxLength="300" Text='<%# Bind("BeneficiaryContactHours")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label11" AssociatedControlID="textBoxBeneficiaryContactPhoneTollFree">Beneficiary Contact Phone Toll Free:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxBeneficiaryContactPhoneTollFree" MaxLength="20" Text='<%# Bind("BeneficiaryContactPhoneTollFree")%>'></asp:TextBox><br />
                                         <asp:RegularExpressionValidator runat="server" ValidationGroup="ShipProfileEdit" SetFocusOnError="true" ControlToValidate="textBoxBeneficiaryContactPhoneTollFree" ValidationExpression="^\(?[\d]{3}\)?[\s-]?[\d]{3}[\s-]?[\d]{4}?(([\s-]?(x)?[\d]{1,4})?)$" ErrorMessage="Beneficiary Contact Phone TollFree entered must be in the format 999-999-9999" ID="RevBeneficiaryContactPhoneTollFree">
                                        </asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label12" >Beneficiary Contact Phone Toll Free In State Only:</asp:Label>
                                    </td>
                                    <td>
                                      <%--  <asp:DropDownList runat="server" ID="ddlBeneficiaryContactPhoneTollFreeInStateOnly" SelectedValue='<%# Bind("BeneficiaryContactPhoneTollFreeInStateOnly") %>'>
                                        <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                        </asp:DropDownList>--%>
                                        
                                        <asp:RadioButtonList runat="server" ID="rblBeneficiaryContactPhoneTollFreeInStateOnly" RepeatDirection="Horizontal" SelectedValue='<%# Bind("BeneficiaryContactPhoneTollFreeInStateOnly") %>' >
                                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label13" AssociatedControlID="textBoxBeneficiaryContactPhoneTollLine">Beneficiary Contact Phone Toll Line:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxBeneficiaryContactPhoneTollLine" MaxLength="20" Text='<%# Bind("BeneficiaryContactPhoneTollLine")%>'></asp:TextBox><br />
                                        <asp:RegularExpressionValidator runat="server" ValidationGroup="ShipProfileEdit" SetFocusOnError="true" ControlToValidate="textBoxBeneficiaryContactPhoneTollLine" ValidationExpression="^\(?[\d]{3}\)?[\s-]?[\d]{3}[\s-]?[\d]{4}?(([\s-]?(x)?[\d]{1,4})?)$" ErrorMessage="Beneficiary Contact Phone TollLine entered must be in the format 999-999-9999" ID="RevBeneficiaryContactPhoneTollLine">
                                        </asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label14" AssociatedControlID="textBoxBeneficiaryContactTDDLine">Beneficiary Contact TDD Line:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxBeneficiaryContactTDDLine" MaxLength="20" Text='<%# Bind("BeneficiaryContactTDDLine")%>'></asp:TextBox><br />
                                         <asp:RegularExpressionValidator runat="server" ValidationGroup="ShipProfileEdit" SetFocusOnError="true" ControlToValidate="textBoxBeneficiaryContactTDDLine" ValidationExpression="^\(?[\d]{3}\)?[\s-]?[\d]{3}[\s-]?[\d]{4}?(([\s-]?(x)?[\d]{1,4})?)$" ErrorMessage="Beneficiary Contact TDDLine entered must be in the format 999-999-9999" ID="RevBeneficiaryContactTDDLine">
                                        </asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label15" AssociatedControlID="textBoxBeneficiaryContactWebsite">Beneficiary Contact Website:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxBeneficiaryContactWebsite" MaxLength="150" Text='<%# Bind("BeneficiaryContactWebsite")%>'></asp:TextBox><br />
                                      <%--   <asp:RegularExpressionValidator runat="server" ValidationGroup="ShipProfileEdit" SetFocusOnError="true" ControlToValidate="textBoxBeneficiaryContactWebsite" ValidationExpression="((http|https):\/\/w{3}[\d]*.|(http|ftp|https):\/\/|w{3}[\d]*.)([\w\d\._\-#\(\)\[\]\\,;:]+@[\w\d\._\-#\(\)\[\]\\,;:])?([a-z0-9]+.)*[a-z\-0-9]+.([a-z]{2,3})?[a-z]{2,6}(:[0-9]+)?(\/[\/a-z0-9\._\-,]+)*[a-z0-9\-_\.\s\%]+(\?[a-z0-9=%&amp;\.\-,#]+)?" ErrorMessage="Beneficiary Contact Website URL is not in a valid format." ID="RevBeneficiaryContactWebsite">
                                        </asp:RegularExpressionValidator>--%>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label16" AssociatedControlID="textBoxNumberOfCountiesServed">Number Of Counties Served:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxNumberOfCountiesServed" MaxLength="5" Text='<%# Bind("NumberOfCountiesServed")%>'></asp:TextBox><br />
                                        <asp:RangeValidator id="RangeValidator3" runat="server" SetFocusOnError="true" ValidationGroup="ShipProfileEdit" ErrorMessage="You must enter a number between 0 to 32000" ControlToValidate="textBoxNumberOfCountiesServed" MinimumValue="0" MaximumValue="32000" Type="Integer" ></asp:RangeValidator>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label17" AssociatedControlID="textBoxNumberOfSponsors">Number Of Sponsors:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxNumberOfSponsors" ValidationGroup="ShipProfileEdit" MaxLength="5" Text='<%# Bind("NumberOfSponsors")%>'></asp:TextBox><br />
                                        <asp:RangeValidator id="RangeValidator2" runat="server" ValidationGroup="ShipProfileEdit" SetFocusOnError="true" ErrorMessage="You must enter a number between 0 to 32000" ControlToValidate="textBoxNumberOfSponsors" MinimumValue="0" MaximumValue="32000" Type="Integer" ></asp:RangeValidator>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label18" AssociatedControlID="textBoxNumberOfVolunteerCounselors">Number Of Volunteer Counselors:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxNumberOfVolunteerCounselors" ValidationGroup="ShipProfileEdit" MaxLength="5" Text='<%# Bind("NumberOfVolunteerCounselors")%>'></asp:TextBox><br />
                                        <asp:RangeValidator id="RangeValidator1" runat="server" SetFocusOnError="true" ValidationGroup="ShipProfileEdit" ErrorMessage="You must enter a number between 0 to 32000" ControlToValidate="textBoxNumberOfVolunteerCounselors" MinimumValue="0" MaximumValue="32000" Type="Integer" ></asp:RangeValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label29" AssociatedControlID="textBoxNumberOfStateStaff">Number Of State Staff:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxNumberOfStateStaff" ValidationGroup="ShipProfileEdit" MaxLength="5" Text='<%# Bind("NumberOfStateStaff")%>'></asp:TextBox><br />
                                        <asp:RangeValidator id="RangeValidator4" runat="server" ValidationGroup="ShipProfileEdit" SetFocusOnError="true" ErrorMessage="You must enter a number between 0 to 32000" ControlToValidate="textBoxNumberOfStateStaff" MinimumValue="0" MaximumValue="32000" Type="Integer" ></asp:RangeValidator>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label30" AssociatedControlID="textBoxTotalCounties">Total Counties:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxTotalCounties" ValidationGroup="ShipProfileEdit" MaxLength="5" Text='<%# Bind("TotalCounties")%>'></asp:TextBox><br />
                                        <asp:RangeValidator id="RangeValidator5" runat="server" SetFocusOnError="true" ValidationGroup="ShipProfileEdit" ErrorMessage="You must enter a number between 0 to 32000" ControlToValidate="textBoxTotalCounties" MinimumValue="0" MaximumValue="32000" Type="Integer" ></asp:RangeValidator>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label31" AssociatedControlID="textBoxProgramCoordinatorName">Program Coordinator Name:</asp:Label>
                                    </td>
                                    <td>
                                        
                                        <asp:TextBox runat="server" ID="textBoxProgramCoordinatorName" MaxLength="50" Text='<%# Bind("ProgramCoordinatorName")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label19" AssociatedControlID="textBoxProgramCoordinatorAddress">Program Coordinator Address:</asp:Label>
                                    </td>
                                    <td>
                                        
                                        <asp:TextBox runat="server" ID="textBoxProgramCoordinatorAddress" MaxLength="150" Text='<%# Bind("ProgramCoordinatorAddress")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label20" AssociatedControlID="textBoxProgramCoordinatorCity">Program Coordinator City:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxProgramCoordinatorCity" MaxLength="80" Text='<%# Bind("ProgramCoordinatorCity")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label21" AssociatedControlID="textBoxProgramCoordinatorEmail">Program Coordinator Email:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxProgramCoordinatorEmail" MaxLength="150" Text='<%# Bind("ProgramCoordinatorEmail")%>'></asp:TextBox><br />
                                        <asp:RegularExpressionValidator runat="server" ValidationGroup="ShipProfileEdit" SetFocusOnError="true" ControlToValidate="textBoxProgramCoordinatorEmail" ValidationExpression="^[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.(([0-9]{1,3})|([a-zA-Z]{2,3})|(aero|coop|info|museum|name))$" ErrorMessage="Program Cordinator Email is not in a valid format." ID="RevProgramCordinatorEmail">
                                        </asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label32" AssociatedControlID="textBoxProgramCoordinatorPhone">Program Coordinator Phone:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxProgramCoordinatorPhone" MaxLength="20" Text='<%# Bind("ProgramCoordinatorPhone")%>'></asp:TextBox><br />
                                         <asp:RegularExpressionValidator runat="server" ValidationGroup="ShipProfileEdit" SetFocusOnError="true" ControlToValidate="textBoxProgramCoordinatorPhone" ValidationExpression="^\(?[\d]{3}\)?[\s-]?[\d]{3}[\s-]?[\d]{4}?(([\s-]?(x)?[\d]{1,4})?)$" ErrorMessage="Program Cordinator Phone entered must be in the format 999-999-9999" ID="RevProgramCordinatorPhone">
                                        </asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label22" AssociatedControlID="textBoxProgramCoordinatorFax">Program Coordinator Fax:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxProgramCoordinatorFax" MaxLength="20" Text='<%# Bind("ProgramCoordinatorFax")%>'></asp:TextBox><br />
                                        <asp:RegularExpressionValidator runat="server" ValidationGroup="ShipProfileEdit" SetFocusOnError="true" ControlToValidate="textBoxProgramCoordinatorFax" ValidationExpression="^\(?[\d]{3}\)?[\s-]?[\d]{3}[\s-]?[\d]{4}?(([\s-]?(x)?[\d]{1,4})?)$" ErrorMessage="Program Cordinator Fax entered must be in the format 999-999-9999" ID="RevProgramCordinatorFax">
                                        </asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label23" AssociatedControlID="textBoxProgramCoordinatorZipcode">Program Coordinator Zipcode:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxProgramCoordinatorZipcode" MaxLength="10" Text='<%# Bind("ProgramCoordinatorZipcode")%>'></asp:TextBox><br />
                                          <asp:RegularExpressionValidator runat="server" ValidationGroup="ShipProfileEdit" SetFocusOnError="true" ValidationExpression="(^\d{5}$)|(^\d{5}-\d{4}$)" ControlToValidate="textBoxProgramCoordinatorZipcode" ErrorMessage="Program Coordinator Zipcode entered must be in the format 99999-9999" ID="RevProgramCoordinatorZipcode">
                                        </asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                               <%-- New fields added By Lavanya--%>
                                <tr style="visibility:hidden;" >
                                    <td colspan="2">                                        
                                        <asp:HiddenField ID="hdnState" runat="server" Value='<%# Bind("StateName") %>'  />
                                        <asp:HiddenField ID="hdnLng" runat="server" Value='<%# Bind("Longitude") %>' />
                                        <asp:HiddenField ID="hdnLat" runat="server" Value='<%# Bind("Latitude") %>' />
                                    </td>
                                </tr>                              
                               <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label38" AssociatedControlID="textBoxStateOversightAgency">State Oversight Agency:</asp:Label>
                                    </td>
                                    <td>
                                        
                                        <asp:TextBox runat="server" ID="textBoxStateOversightAgency" MaxLength="150" Text='<%# Bind("StateOversightAgency")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label33" AssociatedControlID="textBoxNumberOfPaidStaff">Number Of Paid Staff:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxNumberOfPaidStaff" ValidationGroup="ShipProfileEdit" MaxLength="5" Text='<%# Bind("NumberOfPaidStaff")%>'></asp:TextBox><br />
                                        <asp:RangeValidator id="RvNumberOfPaidStaff" runat="server" SetFocusOnError="true" ValidationGroup="ShipProfileEdit" ErrorMessage="You must enter a number between 0 to 32000" ControlToValidate="textBoxNumberOfPaidStaff" MinimumValue="0" MaximumValue="32000" Type="Integer" ></asp:RangeValidator>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label34" AssociatedControlID="textBoxNumberOfCoordinators">Number Of Coordinators:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxNumberOfCoordinators" ValidationGroup="ShipProfileEdit" MaxLength="5" Text='<%# Bind("NumberOfCoordinators")%>'></asp:TextBox><br />
                                        <asp:RangeValidator id="RvNumberOfCoordinators" runat="server" SetFocusOnError="true" ValidationGroup="ShipProfileEdit" ErrorMessage="You must enter a number between 0 to 32000" ControlToValidate="textBoxNumberOfCoordinators" MinimumValue="0" MaximumValue="32000" Type="Integer" ></asp:RangeValidator>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label35" AssociatedControlID="textBoxNumberOfCertifiedCounselors">Number Of Certified Counselors:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxNumberOfCertifiedCounselors" ValidationGroup="ShipProfileEdit" MaxLength="5" Text='<%# Bind("NumberOfCertifiedCounselors")%>'></asp:TextBox><br />
                                        <asp:RangeValidator id="RvNumberOfCertifiedCounselors" runat="server" SetFocusOnError="true" ValidationGroup="ShipProfileEdit" ErrorMessage="You must enter a number between 0 to 32000" ControlToValidate="textBoxNumberOfCertifiedCounselors" MinimumValue="0" MaximumValue="32000" Type="Integer" ></asp:RangeValidator>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label36" AssociatedControlID="textBoxNumberOfEligibleBeneficiaries">Number Of Eligible Beneficiaries:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxNumberOfEligibleBeneficiaries" ValidationGroup="ShipProfileEdit" Text='<%# Bind("NumberOfEligibleBeneficiaries")%>'></asp:TextBox><br />
                                        <%--<asp:RangeValidator id="RvNumberOfEligibleBeneficiaries" runat="server" SetFocusOnError="true" ValidationGroup="ShipProfileEdit" ErrorMessage="You must enter a number between 0 to 32000" ControlToValidate="textBoxNumberOfEligibleBeneficiaries" MinimumValue="0" MaximumValue="32000" Type="Integer" ></asp:RangeValidator>--%>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label37" AssociatedControlID="textBoxNumberOfBeneficiaryContacts">Number Of Beneficiary Contacts:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxNumberOfBeneficiaryContacts" ValidationGroup="ShipProfileEdit" Text='<%# Bind("NumberOfBeneficiaryContacts")%>'></asp:TextBox><br />
                                        <%--<asp:RangeValidator id="RvNumberOfBeneficiaryContacts" runat="server" SetFocusOnError="true" ValidationGroup="ShipProfileEdit" ErrorMessage="You must enter a number between 0 to 32000" ControlToValidate="textBoxNumberOfBeneficiaryContacts" MinimumValue="0" MaximumValue="32000" Type="Integer" ></asp:RangeValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label39" AssociatedControlID="textBoxLocalAgencies">Local Agencies:</asp:Label>
                                    </td>
                                    <td>
                                        
                                        <asp:TextBox runat="server" ID="textBoxLocalAgencies" MaxLength="150" Text='<%# Bind("LocalAgencies")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                <%--End Adding new fields--%>
                                 <tr>
                                    <td colspan="2">
                                        <div class="commands">
                                            <asp:Button runat="server" ID="buttonSubmit" Text="Submit" CssClass="formbutton3" ValidationGroup="ShipProfileEdit" OnClick="buttonSubmit_Click"  CausesValidation="true" />
                                        </div>
                                    </td>
                                </tr>
                        </div>
                    </div>
                </EditItemTemplate>
            </asp:FormView>
        </div>
    </div>
    <pp:ObjectContainerDataSource ID="dataSourceEditSHIP" runat="server" 
    DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.EditShipProfileViewData" 
    OnSelecting="dataSourceEditSHIP_Selecting"
    OnUpdated="dataSourceEditSHIP_Updated" 
    />
</asp:Content>
