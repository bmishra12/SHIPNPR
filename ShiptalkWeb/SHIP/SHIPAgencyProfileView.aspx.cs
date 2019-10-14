using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Web.Routing;
using Microsoft.Practices.Web.UI.WebControls;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkWeb.Routing;

using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkCommon;

namespace ShiptalkWeb.SHIP
{
    public partial class SHIPAgencyProfileView : Page
    {
     
        private AgencyBLL _logic;

        public string stateFIPS { get; set; }
        public string countyFIPS { get; set; }
        public string stateName { get; set; }
        public string countyName { get; set; }
        public string zip { get; set; }
        
        public IEnumerable<KeyValuePair<int, string>> Agencies { get; set; }
            
        public AgencyBLL Logic
        {
            get
            {
                if (_logic == null) _logic = new AgencyBLL();

                return _logic;
            }
        }
    
        protected void Page_Load(object sender, EventArgs e)
        {
           
            NoSearchResultsMessage.Visible = false;
        }
        
        protected void dataSourceViewAgency_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            //stateFIPS = "37";
            //CountyFIPS = "37037";

            //stateFIPS = "01";
            //CountyFIPS = "01041";

            //stateFIPS = "01";
            //CountyFIPS = "01001";

            //stateFIPS = "99";
            //CountyFIPS = "99999";
            ContentPlaceHolder cp = PreviousPage.Master.FindControl("body1") as ContentPlaceHolder;
            DropDownList ddlStates = cp.FindControl("ddlStates") as DropDownList;
            DropDownList ddlCounties = cp.FindControl("ddlCounties") as DropDownList;

            stateFIPS = ddlStates.SelectedValue;
            countyFIPS = ddlCounties.SelectedValue;

            stateName = ddlStates.SelectedItem.Text.Trim();
            countyName = ddlCounties.SelectedItem.Text.Trim();
            zip = null;

            IList<ViewAgencyProfileView> agencies = null;

            agencies = new List<ViewAgencyProfileView>(Logic.GetViewAgency(stateFIPS, countyFIPS, zip));
            
            if (agencies.Count == 0)
            {
                NoSearchResultsMessage.Visible = true;
            }
            else
            {
                NoSearchResultsMessage.Visible = false;
                dataSourceViewAgency.DataSource = agencies;
            }
        }
    }
}
