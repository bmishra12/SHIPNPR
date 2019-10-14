using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkWeb.Routing;
using System.Web.Routing;
using System.IO;
using TallComponents.Licensing;
using TallComponents.PDF;
using TallComponents.PDF.Forms.Fields;

namespace ShiptalkWeb.SHIP
{
    public partial class SHIPProfileView : System.Web.UI.Page, IRouteDataPage 
    {
        private SHIPProfileBLL _logic;
        
        #region Properties


        public SHIPProfileBLL Logic
        {
            get
            {
                if (_logic == null) _logic = new SHIPProfileBLL();

                return _logic;
            }
        }

        private ViewSHIPProfileViewData _viewData;

        private ViewSHIPProfileViewData ViewData
        {
            get
            {
                string state;
                if (AccountInfo == null)
                    state = Request.QueryString["state"];
                else
                    state = ddlStates.SelectedValue;

                if (_viewData == null)
                    _viewData = Logic.GetSHIPProfile(state);

                return _viewData;
            }
        }

        public UserAccount AccountInfo { get; set; }
        public RouteData RouteData { get; set; }


        protected void PopulateStates()
        {
            ddlStates.DataSource = State.GetStatesWithFIPSKey();
            ddlStates.DataTextField = "Value";
            ddlStates.DataValueField = "Key";
            ddlStates.DataBind();

            ////if (AccountInfo.IsStateAdmin)
            ////{
            ////    ddlStates.Items.FindByValue(AccountInfo.StateFIPS).Selected = true;
            ////    ddlStates.Enabled = false;

            ////    if (ViewData != null)
            ////    {
            ////        dataSourceViewSHIP.DataSource = ViewData;
            ////    }
            ////}

        }

        private void showEditButtonLogic()
        {
            this.buttonEdit.Visible = false;
            if (AccountInfo == null)
                return;

            if (AccountInfo.Scope == Scope.CMS && AccountInfo.IsAdmin == true)
                this.buttonEdit.Visible = true;

            if (AccountInfo.Scope == Scope.State && AccountInfo.IsShipDirector == true
                    && AccountInfo.StateFIPS == ddlStates.SelectedValue)
                this.buttonEdit.Visible = true;

            
        }

        protected void ddlStates_SelectedChanged(object sender, EventArgs e)
        {
            MessageBox.Visible = false;
            if (ddlStates.SelectedValue != "0")
            {
                if (ViewData != null)
                {
                    dataSourceViewSHIP.DataSource = ViewData;

                    //determine whether to show the edit button depending on the scope
                    showEditButtonLogic();
                }
                else
                {
                    dataSourceViewSHIP.DataSource = null;
                }
            }
            else
            {
                 dataSourceViewSHIP.DataSource = null;
            }
        }

        #endregion

        protected void formView_ItemCreated(System.Object sender, EventArgs e)
        {
                if (AccountInfo == null)
                {
                    Panel PanelSecure = (Panel)formView.Row.FindControl("pnlSecure");
                    PanelSecure.Visible = false;
                }

                else
                {
                    if (!panelState.Visible)
                    {
                        Panel PanelSecure = (Panel)formView.Row.FindControl("pnlSecure");
                        PanelSecure.Visible = true;
                    }
                }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OnViewInitialized();

                if (AccountInfo == null)
                {                    
                    panelState.Visible = false;                   
                    dataSourceViewSHIP.DataSource = ViewData;
                }
                else
                    PopulateStates();


            }
        }
        /// <summary>
        /// Called when page is initialized for the first time.
        /// </summary>
        protected void OnViewInitialized()
        {
           // DefaultState = new State(AccountInfo.StateFIPS);
           // IsAdmin = AccountInfo.IsAdmin;
        }


        protected void buttonEdit_Click(object sender, EventArgs e)
        {
            RouteController.RouteTo(RouteController.ShipProfileEdit(this.ddlStates.SelectedValue));
        }

        //Added by Lavanya
        protected void lnkPDF_Click(object sender, EventArgs e)
        {
            string FilePath = string.Empty;
            string PDFTemplatePath = string.Empty;

            PDFTemplatePath = System.Configuration.ConfigurationManager.AppSettings["PDFTemplatePath"].ToString();

            if (AccountInfo == null)
            {
                FilePath = Path.Combine(PDFTemplatePath, "Pdf\\State_SHIP_Profile_Template_External.pdf");

                FillPDF(FilePath, "External");
            }
            else
            {
                if (AccountInfo.Scope == Scope.CMS && AccountInfo.IsAdmin == true)
                {
                    FilePath = Path.Combine(PDFTemplatePath, "Pdf\\State_SHIP_Profile_Template_Internal.pdf");
                    FillPDF(FilePath, "internal");
                }

                if (AccountInfo.Scope == Scope.State && AccountInfo.IsShipDirector == true
                        && AccountInfo.StateFIPS == ddlStates.SelectedValue)
                {
                    FilePath = Path.Combine(PDFTemplatePath, "Pdf\\State_SHIP_Profile_Template_Internal.pdf");
                    FillPDF(FilePath, "internal");
                }
            }

            
        }
        //Added by Lavanya
        protected void FillPDF(string FilePath, string FormType)
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
                    ((TextField)NumberofEligibleBeneficiaries).Value = (ViewData.NumberOfEligibleBeneficiaries).ToString();
                    ((TextField)NumberofBeneficiaryContacts).Value = (ViewData.NumberOfBeneficiaryContacts).ToString();
                    ((TextField)LocalAgencies).Value = ViewData.LocalAgencies;

                    #endregion
                }

                form.Write(Response);
                Response.Close();


            }
        }

    }
}
