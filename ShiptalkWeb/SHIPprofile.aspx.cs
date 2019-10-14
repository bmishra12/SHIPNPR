using System;
using System.Security;
using System.Web.Routing;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkWeb.Routing;

using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkCommon;
using System.Text; 

namespace ShiptalkWeb
{
    public partial class SHIPprofile : Page, IRouteDataPage
    {
        private const string IdKey = "Id";
        private AgencyBLL _logic;

        public int? Id
        {
            get
            {
                if (RouteData.Values[IdKey] == null) return null;

                int id;

                if (int.TryParse(RouteData.Values[IdKey].ToString(), out id))
                    return id;

                return null;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //throw new ApplicationException("Testing Exception Handling");
            PopulateStates();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }

        protected void PopulateStates()
        {
            ddlStates1.DataSource = State.GetStatesWithFIPSKey();
            ddlStates1.DataTextField = "Value";
            ddlStates1.DataValueField = "Key";
            ddlStates1.DataBind();         
        }
        public AgencyBLL Logic
        {
            get
            {
                if (_logic == null) _logic = new AgencyBLL();

                return _logic;
            }
        }
       

        protected void ddlStates1_SelectedIndexChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("~/Ship/SHIPProfileView.aspx?state=");
            sb.Append(ddlStates1.SelectedItem.Value);
            Response.Redirect(sb.ToString());
        }

        

        
        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion

    }
}
