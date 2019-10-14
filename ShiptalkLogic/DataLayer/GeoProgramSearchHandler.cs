using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TallComponents.Licensing;
using TallComponents.PDF;
using TallComponents.PDF.Forms.Fields;

using ShiptalkLogic.DataLayer;
using ShiptalkLogic.BusinessObjects;

namespace ShiptalkLogic.DataLayer
{
    public class GeoProgramSearchHandler : IHttpHandler
    {
        string FilePath = string.Empty;
        string PDFTemplatePath = string.Empty;
        bool IsLoggedIn = false;

        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {


            var dataservice = new SHIPProfileDAL();

            var ds = new Agency();

            //SetPortalId(context.Request);
            HttpResponse response = context.Response;
            response.ContentType = "application/json";
            var localPath = context.Request.Url.LocalPath;
            

            //if (localPath.Contains("/GeoProgramSearch/svc/SHIPprofileAgencies"))
            //{
            //    JToken root;
            //    using (var reader = new StreamReader(context.Request.InputStream))
            //        root = JObject.Parse(reader.ReadToEnd());

            //    var State = root.SelectToken("ddlStates").Value<string>();
            //    string ProfileSet = JsonConvert.SerializeObject(dataservice.GetSHIPProfileAgencyDetails(State));

            //    response.Write(ProfileSet);
            //}

            if (localPath.Contains("/GeoProgramSearch/svc/SHIPprofileAgencies"))
            {
                JToken root;
                using (var reader = new StreamReader(context.Request.InputStream))
                    root = JObject.Parse(reader.ReadToEnd());

                var Latitude = Convert.ToDouble((root.SelectToken("viewData").ToList()[0].First).ToString());
                var Longitude = Convert.ToDouble((root.SelectToken("viewData").ToList()[1].First).ToString());
                var state = (root.SelectToken("viewData").ToList()[2].First).ToString();
                var radius = int.Parse((root.SelectToken("viewData").ToList()[3].First).ToString());

                string ProfileSet = JsonConvert.SerializeObject(dataservice.GetSHIPProfileAgencyDetailsByAddress(Latitude, Longitude, state, radius));

                response.Write(ProfileSet);
            }

            else
                if (localPath.Contains("/GeoProgramSearch/svc/ResolveAddress"))
                {
                    JToken root;
                    using (var reader = new StreamReader(context.Request.InputStream))
                        root = JObject.Parse(reader.ReadToEnd());

                    var Address = root.SelectToken("textBoxAddress").Value<string>();

                    var googleMapsService = new GoogleMapsService(ConfigurationManager.AppSettings["GoogleApiKey"]);
                    var latLng = googleMapsService.GetLocation(Address);                   
                     
                    string latLngVal = JsonConvert.SerializeObject(latLng);
                    response.Write(latLngVal);
                }
                else
                    if (localPath.Contains("/GeoProgramSearch/svc/FillPdf"))
                    {
                        JToken root;
                        using (var reader = new StreamReader(context.Request.InputStream))
                            root = JObject.Parse(reader.ReadToEnd());

                        var StateFIPS = root.SelectToken("StateId").Value<string>();
                        var IsLoggedIn = root.SelectToken("IsLoggedIn").Value<string>();
                        GeneratePDF(context);

                       // var FillPDFDataService = new

                        
                    }

                else
                    response.Write(JsonConvert.SerializeObject(string.Empty));

        }
        #endregion

        public void GeneratePDF(HttpContext context)
        {
            PDFTemplatePath = System.Configuration.ConfigurationManager.AppSettings["PDFTemplatePath"].ToString();

            if (!IsLoggedIn)
            {
                FilePath = Path.Combine(PDFTemplatePath, "Pdf\\State_SHIP_Profile_Template_External.pdf");

                FillPDF(context, FilePath, "External");
            }
            else
            {

                FilePath = Path.Combine(PDFTemplatePath, "Pdf\\State_SHIP_Profile_Template_Internal.pdf");
                FillPDF(context, FilePath, "internal");
            }
        }

        protected void FillPDF(HttpContext context, string FilePath, string FormType)
        {
            byte[] buffer;            

            using (FileStream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            {
                using (System.IO.BinaryReader fileIn = new System.IO.BinaryReader(fileStream))
                {
                   /* Document form = new Document(fileIn);
                    Field ProgramName = form.Fields[0];
                    Field AvailableLanguages = form.Fields[1];
                    Field ProgramDirector = form.Fields[2];*/
                    buffer = fileIn.ReadBytes((int)fileIn.BaseStream.Length);
                }
            }

                context.Response.ContentType = "application/pdf";
                context.Response.AddHeader("Content-Length", buffer.Length.ToString());
                context.Response.AppendHeader("content-disposition", "inline; filename=test.pdf");
                context.Response.BinaryWrite(buffer);
                context.Response.End(); 
#region "Test"
                /*Document form = new Document(fileIn);
                //External form fields
                Field ProgramName = form.Fields[0];
                Field AvailableLanguages = form.Fields[1];
                Field ProgramDirector = form.Fields[2];
                Field Email = form.Fields[3];
                Field Address = form.Fields[4];
                Field Phone = form.Fields[5];
                Field Fax = form.Fields[6];
                Field Website = form.Fields[7];

                #region Fill PDF external fields

                //make form fields empty initially
                ((TextField)ProgramName).Value = "";
                ((TextField)ProgramDirector).Value = "";
                ((TextField)Address).Value = "";
                ((TextField)Email).Value = "";
                ((TextField)Phone).Value = "";
                ((TextField)Fax).Value = "";
                ((TextField)Website).Value = "";
                ((TextField)AvailableLanguages).Value = "";

                //set the values
                //((TextField)ProgramName).Value = ViewData.ProgramName;
                //((TextField)ProgramDirector).Value = ViewData.AdminAgencyContactName;
                //((TextField)Address).Value = ViewData.AdminAgencyAddress;
                //((TextField)Email).Value = ViewData.AdminAgencyEmail;
                //((TextField)Phone).Value = ViewData.AdminAgencyPhone;
                //((TextField)Fax).Value = ViewData.AdminAgencyFax;
                //((TextField)Website).Value = ViewData.ProgramWebsite;
                //((TextField)AvailableLanguages).Value = ViewData.AvailableLanguages;

                #endregion

                if (FormType == "internal")
                {
                    Field StateOversightAgency = form.Fields[8];
                    Field NumberofPaidStaff = form.Fields[9];
                    Field NumberofVolunteers = form.Fields[10];
                    Field NumberofCoordinators = form.Fields[11];
                    Field NumberofCertifiedCounselors = form.Fields[12];
                    Field NumberofEligibleBeneficiaries = form.Fields[13];
                    Field NumberofBeneficiaryContacts = form.Fields[14];
                    Field LocalAgencies = form.Fields[15];

                    #region Fill PDF external fields

                    //make form fields empty initially
                    ((TextField)StateOversightAgency).Value = "";
                    ((TextField)NumberofPaidStaff).Value = "";
                    ((TextField)NumberofVolunteers).Value = "";
                    ((TextField)NumberofCoordinators).Value = "";
                    ((TextField)NumberofCertifiedCounselors).Value = "";
                    ((TextField)NumberofEligibleBeneficiaries).Value = "";
                    ((TextField)NumberofBeneficiaryContacts).Value = "";
                    ((TextField)LocalAgencies).Value = "";

                    //set values
                    //((TextField)StateOversightAgency).Value = ViewData.StateOversightAgency;
                    //((TextField)NumberofPaidStaff).Value = (ViewData.NumberOfPaidStaff).ToString();
                    //((TextField)NumberofVolunteers).Value = (ViewData.NumberOfVolunteerCounselors).ToString();
                    //((TextField)NumberofCoordinators).Value = (ViewData.NumberOfCoordinators).ToString();
                    //((TextField)NumberofCertifiedCounselors).Value = (ViewData.NumberOfCertifiedCounselors).ToString();
                    //((TextField)NumberofEligibleBeneficiaries).Value = (ViewData.NumberOfEligibleBeneficiaries).ToString();
                    //((TextField)NumberofBeneficiaryContacts).Value = (ViewData.NumberOfBeneficiaryContacts).ToString();
                    //((TextField)LocalAgencies).Value = ViewData.LocalAgencies;

                    #endregion
                    
                    buffer = fileIn.ReadBytes((int)fileIn.BaseStream.Length);
                }*/
                
                
               // form.Write(Response);
                //Response.Close();
#endregion

           // }
        }
    }
}
