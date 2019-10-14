using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;

using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;

using ShiptalkWeb.Routing;
using System.Web.Routing;

namespace ShiptalkWeb
{
    public partial class ucInfoLibParent : System.Web.UI.UserControl
    {

        private string INFOLIBITEM_PARENT_ID_ROUTE_KEY = "ParentId";
        bool RETREIVE_LINKED_FILE_BINARY = false;
        
        private const string InfoLib_File_Server_Url = "~/InfoLib/InfoLibFileServer.aspx";
        private string InfoLib_File_Server_Url_QS_Params_Fmt =
                           QueryStringHelper.QueryStringParamNames.INFOLIB_ITEMID_INT.Description()
                           + "={0}&"
                           + QueryStringHelper.QueryStringParamNames.INFOLIB_REQ_FILE_IDEN_INT.Description() + "={1}";


        private int? ParentId = null;
        private InfoLibItem data = null;

        protected void Page_Load(object sender, EventArgs e)
        {

            InitializeData();
            if (data != null)
                InitializeView();
            else
            {
                if (ParentId.HasValue && ParentId.Value == 0)
                {
                    Label lblHeaderText = new Label();
                    lblHeaderText.Text = "Top level item.";
                    pnlParent.Controls.Add(lblHeaderText);
                }
                return;
            }

        }

        private void InitializeView()
        {
            if (data.ItemHeader.HeaderType == InfoLibHeaderType.TextOnly ||
                data.ItemHeader.HeaderType == InfoLibHeaderType.HtmlText)
            {
                Label lblHeaderText = new Label();
                lblHeaderText.Text = data.ItemHeader.HeaderText;
                pnlParent.Controls.Add(lblHeaderText);
            }
            else
            {
                Image headerImage = new Image();
                headerImage.ImageUrl = ConstructUrlForInfoLibItemFile(ParentId.Value, InfoLibFileServerTypes.HeaderImageFile);
                headerImage.BorderWidth = new Unit(0);
                pnlParent.Controls.Add(headerImage);
            }
        }


        private void InitializeData()
        {
            IHttpHandler handler = this.Page as IHttpHandler;
            IRouteDataPage routeDataPage = null;
            if (handler != null) routeDataPage = (handler as IRouteDataPage);
            if (routeDataPage != null)
            {
                if (routeDataPage.RouteData.Values[INFOLIBITEM_PARENT_ID_ROUTE_KEY] + string.Empty != string.Empty)
                    ParentId = Convert.ToInt32(routeDataPage.RouteData.Values[INFOLIBITEM_PARENT_ID_ROUTE_KEY]);
            }

            if (!ParentId.HasValue || ParentId.Value == 0)
                return;

            data = InfoLibBLL.GetInfoLibItem(ParentId.Value, RETREIVE_LINKED_FILE_BINARY);
        }


        private string ConstructUrlForInfoLibItemFile(int InfoLibItemId, InfoLibFileServerTypes type)
        {
            NameValueCollection nvColl = new NameValueCollection();
            nvColl.Add(QueryStringHelper.QueryStringParamNames.INFOLIB_ITEMID_INT.Description(), InfoLibItemId.ToString());
            nvColl.Add(QueryStringHelper.QueryStringParamNames.INFOLIB_REQ_FILE_IDEN_INT.Description(), type.EnumValue<int>().ToString());
            return InfoLibUtil.ConstructInfoLibFileServerUrl(nvColl);
            //return string.Format(InfoLib_File_Server_Url + "?" + InfoLib_File_Server_Url_QS_Params_Fmt, InfoLibItemId, type.EnumValue<int>());
        }
    }
}