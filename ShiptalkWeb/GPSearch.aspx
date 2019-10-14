<%@ Page Language="C#" MasterPageFile="~/ShiptalkWeb.Master"  AutoEventWireup="true" CodeBehind="GPSearch.aspx.cs" Inherits="ShiptalkWeb.GPSearch" Title="Shiptalk" %>

<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="content-type" content="text/html;charset=ISO-8859-1" />
                <meta name="description"
                  content="The State Health Insurance Assistance Program, or SHIP, is a national program that offers one-on-one counseling and assistance to people with Medicare and their families" />


  <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?v=3.exp&amp;sensor=false"></script>
  <script type="text/javascript">

   var infoWindow;
   var textAcctChk;
   var Long;
   var Lat;
   var State; 
   
   var profProgName;
   var profAddr;
   var profPhone;
   var profWebsite;
   var profPDF;
   var profProgDirector;
   var profEmail;
   var profFax;
   var profAvailableLang;
                              
   $(document).ready(function () {

      // alert("Ready");
       textAcctChk = $("[id$='hdnAcctChk']").val();

       Long = $("[id$='_hdnLong']");
       Lat = $("[id$='_hdnLat']");
       State = $("[id$='_hdnState']"); 

       profProgName = $("[id$='_lblProgramName']");
       profAddr = $("[id$='_lblAddress']");
       profPhone = $("[id$='_lblPhone']");
       profWebsite = $("[id$='_lblWebsite']");
       profPDF = $("[id$='_lblPdf']");
       profProgDirector = $("[id$='_lblProgramDirector']");
       profEmail = $("[id$='_lblEmail']");
       profFax = $("[id$='_lblFax']");
       profAvailableLang = $("[id$='_lblAvailableLanguages']");
       
                
        initialize();      
   });

    function initialize() {

        

        button = $("[id$=_btnSearch]");
        
        button.click(function (e) {
        
            e.preventDefault();
            findPrograms();
        
        });
  }

  function findPrograms() {
      
      clearInfo();
      var textBoxAddress = $("[id$='textBoxAddress']");       
        
        //alert(textBoxAddress.val());      
        var requiredValidatorAddress = $("[id$='requiredValidatorAddress']")[0];
        ValidatorEnable(requiredValidatorAddress, true);
       
        if (requiredValidatorAddress.isvalid)
        {
            var viewData = new Object();
            viewData.Address = textBoxAddress.val();
            textBoxAddress = $("[id$='textBoxAddress']").val();               
            var geocoder = new google.maps.Geocoder();
            var point = new Object();



            geocoder.geocode({ 'address': textBoxAddress },
                    function (results, status) {
                        if (status == google.maps.GeocoderStatus.OK) {
                            point.Lat = results[0].geometry.location.lat();
                            point.Lng = results[0].geometry.location.lng();



                            $(results[0].address_components).each(function (i, component) {
                                if (component.types[0] == 'administrative_area_level_1')
                                    point.State = component.short_name;



                            });


                            $.ajax({
                                type: "POST",
                                url: "GeoProgramSearch/svc/SHIPprofileAgencies.axd",
                                data: "{'viewData':" + JSON.stringify(getFindProgramsByAddressViewData(point)) + "}",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (ProfileAgencies) {
                                    markMapCoordinates(ProfileAgencies)
                                },
                                error: function (xhr, ajaxOptions, thrownError)
                                { }
                            });



                        } else {
                            
                            updateInfo("<span style='color:#A90901;'>Unable to locate the address provided.</span>");
                            textBoxAddress.focus();
                        }
                    });
        }
        else
        {
            textBoxAddress.focus();
        }
    }

    function getFindProgramsByAddressViewData(point) {
        

        var viewData = new Object();
        viewData.Latitude = point.Lat;
        viewData.Longitude = point.Lng;
        viewData.State = point.State;
        viewData.Radius = $("[id$='ddlRadius'] :selected").val();   
        return viewData;
    }

   
     function markMapCoordinates(ProfileAgencies) { 
        
        var profiles = ProfileAgencies.Table;
        var agencies = ProfileAgencies.Table1;
        //alert(agencies.length);
        
        //alert(profiles);
        //alert(agencies);             
         
        var geocoder = new google.maps.Geocoder();
        var profile = null;

        if (profiles.length <= 0) {
            updateInfo("<span style='color:#A90901;'>Unable to locate the address provided.</br></br></span>");
            textBoxAddress.focus();
        }
        else {

            var pnlProfile = $("[id$='pnlProfile']");
            pnlProfile.show();

            var profileDisplay = "";

            profileDisplay = "<table width='950px' class='dataTable'><thead><tr><th width='200px' scope='col' >Program Name</th><th width='200px' scope='col' >Address</th><th width='200px' scope='col'>Phone</th><th width='250px' scope='col' >Website</th><th width='100px' scope='col' >Pdf</th><th width='5px' scope='col'  style='display:none;'>Program Director</th><th width='5px' scope='col' style='display:none;'>Email</th><th width='5px' scope='col' style='display:none;'>Fax</th><th width='5px' scope='col' style='display:none;' >Available Languages</th></tr></thead><tbody>";

            $(profiles).each(function (i, profileInstance) {
                profile = profileInstance;
                profileDisplay = profileDisplay + "<tr><td>" + profile.ProgramName + "</td><td> " + profile.Address + "</td><td>" + profile.Phone + "</td><td><a href='" + profile.ProgramWebsite + " target = 'blank'>" + profile.ProgramWebsite + "</a></td><td> <a href='Ship/GeneratePDF.aspx?type=sp&state=" + profile.StateFIPS + "'>Download</a> </td><td style='display:none;'>" + profile.ProgramDirector + "</td><td style='display:none;'> " + profile.Email + "</td><td style='display:none;'> " + profile.Fax + " </td><td style='display:none;'> " + profile.AvailableLanguages + " </td><td></tr>";
            });

            profileDisplay = profileDisplay + "</tbody></table>";
            var lblProfile = $("[id$='lblProfile']");
            lblProfile.html(profileDisplay);

            var profileAgency = "";

            if (agencies.length > 0) {

                profileAgency = "<table width='950px' class='dataTable'><thead><tr><th width='200px' scope='col' >Program Name</th><th width='200px' scope='col' >Address</th><th width='200px' scope='col'>Phone</th><th width='250px' scope='col' >Website</th><th width='100px' scope='col' >Pdf</th><th width='5px' scope='col'  style='display:none;'>Program Director</th><th width='5px' scope='col' style='display:none;'>Email</th><th width='5px' scope='col' style='display:none;'>Fax</th><th width='5px' scope='col' style='display:none;' >Available Languages</th></tr></thead><tbody>";
                $(agencies).each(function (i, agency) {
                    var agencyWebsite = "";
                    if (agency.URL == "Not Available")
                        agencyWebsite = "Not Available";
                    else
                        agencyWebsite = "<a href='" + agency.URL + " target = 'blank'>" + agency.URL + "</a>";

                    profileAgency = profileAgency + "<tr><td>" + agency.ProgramName + "</td><td> " + agency.agencyAddress + "</td><td>" + agency.agencyPhone + "</td><td>" + agencyWebsite + "</td><td><a href='Ship/GeneratePDF.aspx?type=cl&id=" + agency.agencyLocationID + "'>Download</a> </td><td style='display:none;'>" + agency.ProgramDirector + "</td><td style='display:none;'> " + agency.agencyEmail + "</td><td style='display:none;'> " + agency.Fax + " </td><td style='display:none;'> " + agency.AvailableLanguages + " </td><td></tr>";
                });
                profileAgency = profileAgency + "</tbody></table>";
            }
            else {
                profileAgency = "No Agencies were found for given address.";
            }
            var lblAgency = $("[id$='lblAgency']");
            lblAgency.html(profileAgency);
        }
    }

    
   

    function updateInfo(info) {
        $("#info").html(info).scrollTo(500).effect("highlight", {}, 2000);
    }

    function clearInfo() {
        $("#info").html("");
    }

   </script>
 

</asp:Content> 



<asp:Content id="Content2" contentplaceholderid="body1" runat="server">


    <div id="maincontent">
        <div class="dv3col">
            <h1>What is SHIPtalk?</h1>
            <p>The State Health Insurance Assistance Program, or SHIP, is a 
            national program that offers one-on-one counseling and assistance 
            to people with Medicare and their families.</p>
        </div>
        <div class="clear"></div>
        
        
      
            <h2>Find a State SHIP</h2>
            <p>Looking for a State SHIP?  Select your state below to find your local SHIP branch.</p>
          
             <asp:Label ID="labelAddress" runat="server" AssociatedControlID="textBoxAddress" CssClass="tag">Address or ZIP Code:</asp:Label>
                              
                <asp:TextBox ID="textBoxAddress" runat="server" Width="450px" />
                <asp:RequiredFieldValidator ID="requiredValidatorAddress" runat="server" EnableClientScript="true" ErrorMessage="<br />An Address or ZIP Code is required.<br />" ControlToValidate="textBoxAddress" Display="Dynamic"></asp:RequiredFieldValidator>
                <br /><br />
                <asp:Label ID="labelAddressRadius" runat="server" AssociatedControlID="ddlRadius" CssClass="tag">Radius:</asp:Label>
                <asp:DropDownList ID="ddlRadius" runat="server">
                    <asp:ListItem Text="5 Miles" Value="5" />
                    <asp:ListItem Text="10 Miles" Value="10" />
                    <asp:ListItem Text="15 Miles" Value="15" />
                    <asp:ListItem Text="25 Miles" Value="25" Selected="True" />
                    <asp:ListItem Text="50 Miles" Value="50" />
                </asp:DropDownList>
           <asp:Button ID="btnSearch" runat="server" Text="Search"    CssClass="formbutton3"  />          
            
            
            <br /><br />
             <div id="info">&nbsp;</div>
              
           <asp:Label ID="lblErrorMessage" runat="server" Visible="false"></asp:Label>
           
           <asp:HiddenField ID="hdnLong" runat="server" />
           <asp:HiddenField ID="hdnLat" runat="server" />
           <asp:HiddenField ID="hdnState" runat="server" />


          <asp:HyperLink ID="lnkViewData" runat="server" Text="Map Version" NavigateUrl="~/GeoProgramSearch.aspx"></asp:HyperLink><br /><br /><br /><br />
           <asp:Panel ID="pnlProfile" runat="server" style="display: none;">             
              <div id="dv3colFormContent" class="dv3col">
               <h4>SHIP Profile</h4><br/>
               <asp:Label ID="lblProfile" runat="server" />
                <br /><h4>Agencies</h4><br />
                <asp:Label ID="lblAgency" runat="server" />
                <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
                 </div>
            </asp:Panel>
       
        <div class="clear"></div>
        <div>
         <asp:LinkButton ID="lnkBtn" Text="Back to SHIPtalk" ToolTip="Back to SHIPtalk" runat="server" PostBackUrl="http://nprpreprocessor.shiptalk.org/" Font-Bold="true"></asp:LinkButton><br />
           <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/ShpTlkbtn.jpg" PostBackUrl="http://nprpreprocessor.shiptalk.org/" runat="server" ToolTip="Back to SHIPtalk" />
       
        </div>
      
    </div>

    
</asp:Content>
