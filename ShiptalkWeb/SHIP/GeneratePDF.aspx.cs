using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using ShiptalkWeb.Routing;
using System.Web.Routing;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using TallComponents.Licensing;
using TallComponents.PDF;
using TallComponents.PDF.Forms.Fields;


namespace ShiptalkWeb.SHIP
{
    public partial class GeneratePDF : System.Web.UI.Page
    {
        private SHIPProfileBLL _logic;
        private AgencyBLL _agencyLogic;

        bool IsLoggedIn = false;
        string StateFIPS = string.Empty;
        int AgencyLocationId = 0;
       
        #region Properties


        public SHIPProfileBLL Logic
        {
            get
            {
                if (_logic == null) _logic = new SHIPProfileBLL();

                return _logic;
            }
        }

        public AgencyBLL AgencyLogic
        {
            get
            {
                if (_agencyLogic == null) _agencyLogic = new AgencyBLL();

                return _agencyLogic;
            }
            
        }

        private ViewSHIPProfileViewData _viewData;

        private ViewSHIPProfileViewData ViewData
        {
            get
            {
                if (Request.QueryString["state"] != null && Request.QueryString["state"].ToString() != "")
                {
                    StateFIPS = Request.QueryString["state"].ToString();
                    if (StateFIPS.Length == 1)
                        StateFIPS = "0" + StateFIPS;
                }

                if (_viewData == null)
                    _viewData = Logic.GetSHIPProfile(StateFIPS);

                return _viewData;
            }
        }

       // public RouteData RouteData { get; set; }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            string FilePath = string.Empty;
            string PDFTemplatePath = string.Empty;
            string ProfileType = string.Empty;

            PDFTemplatePath = System.Configuration.ConfigurationManager.AppSettings["PDFTemplatePath"].ToString();

            if (Request.QueryString["type"] != null && Request.QueryString["type"].ToString() != "")
                ProfileType = Request.QueryString["type"].ToString();

            if (ProfileType == "sp")
            {

                if (ShiptalkPrincipal.IsSessionActive)
                {
                    IsLoggedIn = true;
                    dataSourceViewSHIPProfile.DataSource = ViewData;
                }                

                if (!IsLoggedIn)
                {
                    FilePath = Path.Combine(PDFTemplatePath, "Pdf\\State_SHIP_Profile_Template_External.pdf");

                    FillProfilePDF(FilePath, "External");
                }
                else
                {
                    FilePath = Path.Combine(PDFTemplatePath, "Pdf\\State_SHIP_Profile_Template_Internal.pdf");
                    FillProfilePDF(FilePath, "internal");
                }
            }

            else
                if (ProfileType == "cl")
                {
                    if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
                        {
                            AgencyLocationId = int.Parse(Request.QueryString["id"].ToString());                           
                        }

                    FilePath = Path.Combine(PDFTemplatePath, "Pdf\\Local_SHIP_Site_Profile_Template_External.pdf");
                    
                    DataTable AgencyLocation = GetAgencyLocationDetails(AgencyLocationId);

                    FillAgencyPDF(FilePath, AgencyLocation);
                }

        }

        protected void FillProfilePDF(string FilePath, string FormType)
        {
            using (FileStream fileIn = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            {
                Document form = new Document(fileIn);

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
                ((TextField)ProgramName).Value = ViewData.ProgramName;
                ((TextField)ProgramDirector).Value = ViewData.ProgramCoordinatorName;
                ((TextField)Address).Value = ViewData.AdminAgencyAddress;
                ((TextField)Email).Value = ViewData.AdminAgencyEmail;
                ((TextField)Phone).Value = ViewData.AdminAgencyPhone;
                ((TextField)Fax).Value = ViewData.AdminAgencyFax;
                ((TextField)Website).Value = ViewData.ProgramWebsite;
                ((TextField)AvailableLanguages).Value = ViewData.AvailableLanguages;

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
                    ((TextField)StateOversightAgency).Value = ViewData.StateOversightAgency;
                    ((TextField)NumberofPaidStaff).Value = (ViewData.NumberOfPaidStaff).ToString();
                    ((TextField)NumberofVolunteers).Value = (ViewData.NumberOfVolunteerCounselors).ToString();
                    ((TextField)NumberofCoordinators).Value = (ViewData.NumberOfCoordinators).ToString();
                    ((TextField)NumberofCertifiedCounselors).Value = (ViewData.NumberOfCertifiedCounselors).ToString();

                    if (ViewData.NumberOfEligibleBeneficiaries != null)
                        ((TextField)NumberofEligibleBeneficiaries).Value = (ViewData.NumberOfEligibleBeneficiaries).ToString();
                    if (ViewData.NumberOfBeneficiaryContacts != null)
                        ((TextField)NumberofBeneficiaryContacts).Value = (ViewData.NumberOfBeneficiaryContacts).ToString();

                    ((TextField)LocalAgencies).Value = ViewData.LocalAgencies;
                    

                    #endregion
                }

                form.Write(Response);
                Response.Close();


            }
        }

        protected DataTable GetAgencyLocationDetails(int AgencyLocationId)
        {
            DataTable dtAgencyLocation = new DataTable();

            dtAgencyLocation = AgencyLogic.GetAgencyLocationForGeoSearch(AgencyLocationId);
         
                return dtAgencyLocation;
           
        }

        protected void FillAgencyPDF(string FilePath, DataTable AgencyLocation)
        {
            using (FileStream fileIn = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            {
                Document form = new Document(fileIn);

                //External form fields
                Field ProgramName = form.Fields[0];
                Field AvailableLanguages = form.Fields[7];
                Field ProgramDirector = form.Fields[2];
                Field Email = form.Fields[3];
                Field Address = form.Fields[1];
                Field Phone = form.Fields[6];
                Field Fax = form.Fields[4];
                Field Website = form.Fields[5];              

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
                if(AgencyLocation.Rows.Count > 0 )
                {
                    ((TextField)ProgramName).Value = AgencyLocation.Rows[0].Field<string>("ProgramName");
                    ((TextField)ProgramDirector).Value = AgencyLocation.Rows[0].Field<string>("ProgramDirector");
                    ((TextField)Address).Value = AgencyLocation.Rows[0].Field<string>("agencyAddress");
                    ((TextField)Email).Value = AgencyLocation.Rows[0].Field<string>("agencyEmail");
                    ((TextField)Phone).Value = AgencyLocation.Rows[0].Field<string>("agencyPhone");
                    ((TextField)Fax).Value = AgencyLocation.Rows[0].Field<string>("Fax");
                    ((TextField)Website).Value = AgencyLocation.Rows[0].Field<string>("URL");
                    ((TextField)AvailableLanguages).Value = AgencyLocation.Rows[0].Field<string>("AvailableLanguages");
                }

                      

                form.Write(Response);
                Response.Close();


            }
        }
    }
}