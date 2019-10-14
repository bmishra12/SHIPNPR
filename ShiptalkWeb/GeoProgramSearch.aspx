<%@ Page Language="C#" MasterPageFile="~/ShiptalkWeb.Master"  AutoEventWireup="true" CodeBehind="GeoProgramSearch.aspx.cs" Inherits="ShiptalkWeb.GeoProgramSearch" Title="Shiptalk" %>

<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="content-type" content="text/html;charset=ISO-8859-1" />
                <meta name="description"
                  content="The State Health Insurance Assistance Program, or SHIP, is a national program that offers one-on-one counseling and assistance to people with Medicare and their families" />


  <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?v=3.exp&amp;sensor=false"></script>
  <script type="text/javascript">

   var infoWindow;      
   var textAcctChk;                              
   $(document).ready(function ()
   {          
        textAcctChk = $("[id$='hdnAcctChk']").val();           
        initialize();      
   });

   function initialize() {      
        var latlng = new google.maps.LatLng(40, -98);
        var myOptions = {
          zoom: 4,
          center: latlng,
          mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);

         button = $("[id$=_btnSearch]");

        button.click(function (e)
        {
            e.preventDefault();                 
            findPrograms();
        });
  }

  function findPrograms() {  
        clearInfo();
        var textBoxAddress = $("[id$='textBoxAddress']");       
        var requiredValidatorAddress = $("[id$='requiredValidatorAddress']")[0];
        ValidatorEnable(requiredValidatorAddress, true);
       
        if (requiredValidatorAddress.isvalid)
        {
            var viewData = new Object();
            viewData.Address = textBoxAddress.val();
            textBoxAddress = $("[id$='textBoxAddress']").val();               
            var geocoder = new google.maps.Geocoder();
            var point = new Object();

            geocoder.geocode({'address': textBoxAddress},
                    function(results, status) {
                         if (status == google.maps.GeocoderStatus.OK) {
                             point.Lat = results[0].geometry.location.lat();
                             point.Lng = results[0].geometry.location.lng();

                             $(results[0].address_components).each(function(i, component) {
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
         
        var geocoder = new google.maps.Geocoder();
        var profile = null; 
               
        $(profiles).each(function(i, profileInstance) {                         
           profile = profileInstance;            
           //alert(profile.StateName);          
        });

        var location = new google.maps.LatLng(profile.Latitude, profile.Longitude);        
                            
        var mapOptions = {           
            center: location,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        

       var bounds = new google.maps.LatLngBounds ();
       markers = new Array();         
       map = new google.maps.Map(document.getElementById('map_canvas'), mapOptions);

       $(agencies).each(function(i, agency) {
       var newLatLng = new google.maps.LatLng(agency.agencyLat,agency.agencyLng );
                     var marker = new google.maps.Marker({
                      position: new google.maps.LatLng(agency.agencyLat, agency.agencyLng),
                      map: map,
                      animation: google.maps.Animation.DROP,
                      clickable: true,
                      title: agency.ProgramName,
                      agency: agency
                  });        
                  
                  markers.push(marker);       
                  bounds.extend(newLatLng);

                  google.maps.event.addListener(marker, 'click', 
                     function () {                                         
                         openAgencyInfoWindow(marker);
                        });
                });    

          var layer = new google.maps.FusionTablesLayer({
            query: {
                select: 'TOP 1 geometry',
                from: '0IMZAFCwR-t7jZnVzaW9udGFibGVzOjIxMDIxNw',
                where: "'County Name' = '" + profile.agencyCounty +"'"             
            },
            styles: [{
                polygonOptions: {
                    'fillColor': "#5D92C1",
                    'strokeColor': "#0D5A9B",
                    'strokeWeight': 1,
                    'fillOpacity': .15
                },
                polylineOptions: {
                    'strokeColor': "#0D5A9B",
                    'strokeWeight': 1
                }
            }],
            'suppressInfoWindows': true,
        });

       layer.setMap(map);               

         var marker = new google.maps.Marker({
              position: location,
              map: map,
              icon:  "../Images/bluedot.png",
              animation: google.maps.Animation.DROP,
              clickable: true,
              title: profile.ProgramName,
              profile: profile
          });

          var newLatLng = location;
          bounds.extend(newLatLng);

          map.fitBounds(bounds);  
                
          var zoomLevel = map.getZoom();
          if (zoomLevel > 10)
                 { map.setZoom(10); }           

          google.maps.event.addListener(marker, 'click', 
             function () {
                 
                 marker.position = location;
                 openProfileInfoWindow(marker);
                });
    }

    function openProfileInfoWindow(marker) {

       var content = formatProfileInfo(marker);
//        var content =   "<strong>State: </strong>StateName<br/>" + "<strong>Profile Name: </strong>" + marker.profile.ProgramName +
//                        "<br /><strong>Website: </strong><a href='"+ marker.profile.ProgramWebsite + "' target='_blank'>"+ marker.profile.ProgramWebsite + "</a>"; 


        if (infoWindow) infoWindow.close();
        infoWindow = new google.maps.InfoWindow();

          infowindow = new google.maps.InfoWindow({maxWidth: 50});
        infoWindow.setContent(content);
        infoWindow.setPosition(marker.position);
        infoWindow.open(map);
    }  

    function fnDownloadProfilePDF(stateFIPS)
    {           
        window.open("Ship/GeneratePDF.aspx?type=sp&state=" + stateFIPS);
        return false;
        
      /*  alert(stateFIPS);
        $.ajax({
            type: "POST",                
            url: "GeoProgramSearch/svc/FillPdf.axd",
            data: JSON.stringify({StateId:stateFIPS,
            IsLoggedIn: $("[id$='hdnAcctChk']").val()
            }),

            //data: '{ \'stateFIPS\': \'' + stateFIPS + '\' }',                 
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (FillPdf)
                        { alert("test"); },               
                        error: function (xhr, ajaxOptions, thrownError)
                        {  }
            })*/
    }

    function fnDownloadAgencyPDF(AgencyLocationId)
    {
        window.open("Ship/GeneratePDF.aspx?type=cl&id=" + AgencyLocationId);
        return false;
    }

    function formatProfileInfo(marker)
    {
        var formatString = "";
         formatString += "<p style='font-size:9pt;'>";
         formatString += "<strong>Program Name</strong> (the name of this SHIP office): <a href='Ship/SHIPProfileView.aspx?state="+ marker.profile.StateFIPS + "' ' target='_blank'>" + marker.profile.ProgramName + "</a>";
         formatString += "<br/><strong>Program Director</strong> (the name of the director for this SHIP office): " + marker.profile.ProgramDirector;
         formatString += "<br/><strong>Address</strong> (the street address of this SHIP office): " + marker.profile.Address;
         formatString += "<br/><strong>Email</strong> (the primary email address to reach this SHIP office): " + marker.profile.Email;
         formatString += "<br/><strong>Phone</strong> (the primary phone number to reach this SHIP office): " + marker.profile.Phone;
         formatString += "<br/><strong>Fax</strong> (the primary fax number to reach this SHIP office): " + marker.profile.Fax;
         formatString += "<br /><strong>Website</strong> (the Web site URL for this SHIP office): <a href='"+ marker.profile.ProgramWebsite + "' target='_blank'>"+ marker.profile.ProgramWebsite + "</a>";         
         formatString += "<br/><strong>Available Languages</strong> (the different languages that counselors may speak in this SHIP office; however, there is no guarantee that such languages will always be available): " + marker.profile.AvailableLanguages ;         

         if (textAcctChk == "Yes")
         {
             formatString += "<br/><strong>State Oversight Agency</strong> (the state agency that oversees this state’s SHIP office): " + marker.profile.StateOversightAgency ;
             formatString += "<br/><strong>Number of Paid Staff</strong> (the number of paid staff this state’s SHIP employs statewide): " + marker.profile.NumberOfPaidStaff ;
             formatString += "<br/><strong>Number of Volunteers</strong> (the number of SHIP volunteers statewide): " + marker.profile.NumberOfVolunteerCounselors ;
             formatString += "<br/><strong>Number of Coordinators</strong> (the number of staff statewide who coordinate SHIP activities and staff): " + marker.profile.NumberOfCoordinators;
             formatString += "<br/><strong>Number of Certified Counselors</strong> (the number of persons statewide who are certified to counsel Medicare beneficiaries): " + marker.profile.NumberOfCertifiedCounselors;
             formatString += "<br/><strong>Number of Eligible Beneficiaries</strong> (the number of persons in this state who were Medicare-eligible in 2012): " + marker.profile.NumberOfEligibleBeneficiaries;
             formatString += "<br/><strong>Number of Beneficiary Contacts</strong> (the number of contacts this SHIP made with beneficiaries statewide in 2012): " + marker.profile.NumberOfBeneficiaryContacts;
             formatString += "<br/><strong>Local Agencies</strong> (a listing of the local SHIP offices in this state): " + marker.profile.LocalAgencies;
         }
         
         formatString += "</p>";

         formatString += "<a href='#' onClick='fnDownloadProfilePDF("+ marker.profile.StateFIPS + ") '>Download to PDF</a>";

          return formatString;
    }

    function formatAgencyInfo(marker)
    {
        var formatString = "";
         formatString += "<p style='font-size:8pt;'>";
         formatString += "<strong>Program Name</strong> (the name of this SHIP office): " + marker.agency.ProgramName + "</a>";
         formatString += "<br/><strong>Program Director</strong> (the name of the director for this SHIP office): " + marker.agency.ProgramDirector;
         formatString += "<br/><strong>Address</strong> (the street address of this SHIP office): " + marker.agency.agencyAddress;
         formatString += "<br/><strong>Email</strong> (the primary email address to reach this SHIP office): " + marker.agency.agencyEmail;
         formatString += "<br/><strong>Phone</strong> (the primary phone number to reach this SHIP office): " + marker.agency.agencyPhone;
         formatString += "<br/><strong>Fax</strong> (the primary fax number to reach this SHIP office): " + marker.agency.Fax;
         formatString += "<br/><strong>Web site</strong> (the Web site URL for this SHIP office): " + marker.agency.URL;
         formatString += "<br/><strong>Available Languages</strong> (the different languages that counselors may speak in this SHIP office; however, there is no guarantee that such languages will always be available): " + marker.agency.AvailableLanguages;
                 
         formatString += "</p>";

          formatString += "<a href='#' onClick='fnDownloadAgencyPDF("+ marker.agency.agencyLocationID + ") '>Download  to PDF</a>";

         return formatString;
    }

    function openAgencyInfoWindow(marker) {

        var content =  formatAgencyInfo(marker);

        if (infoWindow) infoWindow.close();
        infoWindow = new google.maps.InfoWindow();
        infoWindow.setContent(content);
        infoWindow.setPosition(marker.position);
        infoWindow.open(map);
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
           <%-- <asp:DropDownList runat="server" ID="ddlStates" DataTextField="Value" DataValueField="Key" OnSelectedIndexChanged="ddlStates_SelectedIndexChanged" 
                                                            AppendDataBoundItems="true" Width="170px" ToolTip="Select State" >
                                                            <asp:ListItem Text="Select a State" Value="0" />
                                                        </asp:DropDownList><br />--%>
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
           <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="formbutton3" /><br /><br />
             <div id="info">&nbsp;</div>             
          
           <asp:HyperLink ID="lnkViewData" runat="server" Text="Text-Only Version" NavigateUrl="~/GPSearch.aspx"></asp:HyperLink><br /><br />
           <asp:HiddenField ID="hdnAcctChk" runat="server" Value="No" ></asp:HiddenField>
           <asp:HiddenField ID="hdnProfile" runat="server" ></asp:HiddenField>
           <asp:HiddenField ID="hdnAgencies" runat="server" Visible="true" ></asp:HiddenField>
      
         <div id="map_canvas" style="width: 650px; height: 450px">
        </div>
         <div class="clear"></div>
        <div style="padding-top:20px">
        <asp:Image ID="imgBlueDot" runat="server" ImageUrl="~/images/bluedot.png" ImageAlign="Middle" />&nbsp;Blue pin represents state SHIP office<br /><br />
        <asp:Image ID="imgRedDot" runat="server" ImageUrl="~/images/Reddot.jpg" ImageAlign="Middle"/>Red pins represent local SHIP counseling sites<br /><br />
        <strong>Note:</strong>&nbsp;Click on a SHIP program name to view that SHIP’s summary page.<br /><br />
       
         <asp:LinkButton ID="lnkBtn" Text="Back to SHIPtalk" ToolTip="Back to SHIPtalk" runat="server" PostBackUrl="http://nprpreprocessor.shiptalk.org/" Font-Bold="true"></asp:LinkButton><br />
           <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/ShpTlkbtn.jpg" PostBackUrl="http://nprpreprocessor.shiptalk.org/" runat="server" ToolTip="Back to SHIPtalk" />
        </div>
      
    </div>

    
</asp:Content>
