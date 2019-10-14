using System;
using System.Security;
using System.Web.Routing;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.DataLayer;
using ShiptalkWeb.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkCommon;
using System.Text;
using System.Data;

namespace ShiptalkWeb
{
    public partial class GPSearch : Page, IRouteDataPage
    {
        private const string IdKey = "Id";
        private AgencyBLL _logic;


        public string GoogleApiKey
        {
            get
            {
                string googleApiKey = null;
                googleApiKey = ConfigurationManager.AppSettings["PRODUploadFilepathCC"];
                return googleApiKey;
            }
        }
        //public int? Id
        //{
        //    get
        //    {
        //        if (RouteData.Values[IdKey] == null) return null;

        //        int id;

        //        if (int.TryParse(RouteData.Values[IdKey].ToString(), out id))
        //            return id;

        //        return null;
        //    }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (ShiptalkPrincipal.IsSessionActive)
                {
                   // hdnAcctChk.Value = "Yes";
                }
            }
            //string servername = getRuntimeAttribute();
            //if (!IsPostBack)
            //{
                //throw new ApplicationException("Testing Exception Handling");
                //PopulateStates();
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //}

        }

        //protected void PopulateStates()
        //{
        //    ddlStates.DataSource = State.GetStatesWithFIPSKey();
        //    ddlStates.DataTextField = "Value";
        //    ddlStates.DataValueField = "Key";
        //    ddlStates.DataBind();         
        //}
        public AgencyBLL Logic
        {
            get
            {
                if (_logic == null) _logic = new AgencyBLL();

                return _logic;
            }
        }

        //protected void ddlStates_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var dataservice = new SHIPProfileBLL();
        //    hdnProfile.Value = JsonConvert.SerializeObject(dataservice.GetSHIPProfileAgencyDetails(ddlStates.SelectedValue));

        //    //hdnProfile.Value = GetSHIPProfileForState();
        //    //hdnAgencies.Value = GetAgenciesForState();
        //}      

        //public string GetSHIPProfileForState()
        //{
        //    var dataservice = new SHIPProfileBLL();

        //    var ProfileData = JsonConvert.SerializeObject(dataservice.GetSHIPProfileAgencyDetails(ddlStates.SelectedValue).Tables[0]);

        //    return ProfileData;
        //}

        //public string GetAgenciesForState()
        //{
        //    var dataservice = new SHIPProfileBLL();
        //    var Agencies = JsonConvert.SerializeObject(dataservice.GetSHIPProfileAgencyDetails(ddlStates.SelectedValue).Tables[1]);

        //    return Agencies;

        //}
        
        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion



        //protected LatLng ResolveAddress(string Address)
        //{
        //    var googleMapsService = new GoogleMapsService(ConfigurationManager.AppSettings["GoogleApiKey"]);
        //    var latLng = googleMapsService.GetLocation(Address);

        //    return latLng;
        //}

        //protected void  FindPrograms(LatLng LatLngresults)
        //{
        //       var dataservice = new SHIPProfileBLL();

        //    DataSet dsProfileAgency = new DataSet();
        //    DataTable dtProfile = new DataTable();
        //    DataTable dtAgencies = new DataTable();

        //    dsProfileAgency = dataservice.GetSHIPProfileAgencyDetailsByAddress(Convert.ToDouble(LatLngresults.Lat), Convert.ToDouble(LatLngresults.Lng), LatLngresults.State, int.Parse(ddlRadius.SelectedValue));

        //     if (dsProfileAgency != null)
        //     {
        //         dtProfile = dsProfileAgency.Tables[0];
        //         dtAgencies = dsProfileAgency.Tables[1];
        //     }

        //     if (dtProfile.Rows.Count > 0)
        //     {
        //         pnlProfile.Visible = true;

        //         lblProgramName.Text = dtProfile.Rows[0].Field<string>("ProgramName");
        //         lblAddress.Text = dtProfile.Rows[0].Field<string>("Address");
        //         lblWebsite.Text = "<a href='" + dtProfile.Rows[0].Field<string>("ProgramWebsite") + "' target=_blank'>" + dtProfile.Rows[0].Field<string>("ProgramWebsite") + "</a>";
        //         lblPdf.Text = "<a href='Ship/GeneratePDF.aspx?type=sp&state=" + dtProfile.Rows[0].Field<string>("StateFIPS") + "'>Download</a>";

        //         lblProgramDirector.Text = dtProfile.Rows[0].Field<string>("ProgramDirector");
        //         lblEmail.Text = dtProfile.Rows[0].Field<string>("Email");
        //         lblPhone.Text = dtProfile.Rows[0].Field<string>("Phone");
        //         lblFax.Text = dtProfile.Rows[0].Field<string>("Fax");
        //         lblAvailableLanguages.Text = dtProfile.Rows[0].Field<string>("AvailableLanguages");                
        //     }

        //     if (dtAgencies.Rows.Count > 0)
        //     {
        //         lblMessage.Visible = false;
        //         repeaterAgencies.DataSource = dtAgencies;
        //         repeaterAgencies.DataBind();
        //     }
        //     else
        //     {
        //         repeaterAgencies.DataSource = null;
        //         repeaterAgencies.DataBind();
        //         lblMessage.Visible = true;
        //         lblMessage.Text = "No Agencies were found for given address.";
                 
        //     }

        //}

        
      
    
    }
}
