using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkWeb;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration;
using Microsoft.Practices.Web.UI.WebControls;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkWeb.Routing;
using System.Linq;
using System.Drawing;

namespace ShiptalkWeb.Npr.Forms
{
    public partial class SpecialFieldsFView : Page, IRouteDataPage
    {
        private const string ID = "SpecialFieldID";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                OnViewInitialize();
             
        }

        /// <summary>
        /// Initalize the view of data.
        /// </summary>
        private void OnViewInitialize()
        {
          ViewSpecialFieldsViewData  Recs =  SpecialFieldsBLL.GetSpecialFieldsView(int.Parse(SpecialFieldID));
            dataSourceViewSpecialField.DataSource = Recs;
            dataSourceViewSpecialField.DataBind();
        }

       
        
       
        #region Properties
       

        private string SpecialFieldID
        {
            get
            {
                if (RouteData.Values[ID] == null) return null;
                return RouteData.Values[ID].ToString();
            }

        }
        #endregion
        #region Implementation of IRouteDataPage


        public RouteData RouteData { get; set; }
        public UserAccount AccountInfo { get; set; }


        #endregion


        #region Implementation of IAuthorize
        public bool IsAuthorized()
        {
            return (AccountInfo.Scope == Scope.CMS || AccountInfo.Scope == Scope.State || AccountInfo.IsAdmin);

        }
        #endregion

        #region Event Methods
        


        #endregion

       
      
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            RouteController.RouteTo(RouteController.SpeciaFieldsEdit(int.Parse(SpecialFieldID)));
        }

    }
}
