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
using ShiptalkWeb.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkCommon;
using System.Text; 

namespace ShiptalkWeb
{
    public partial class GeoProgramSearch : Page, IRouteDataPage
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
                    hdnAcctChk.Value = "Yes";
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

    }
}
