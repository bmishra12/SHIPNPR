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
using System.Collections.Specialized;

using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessLayer;

namespace ShiptalkWeb
{
    public partial class ucInfoLibForumCallSummaryView : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                InitializeView();
        }

        private void InitializeView()
        {
            InfoLibForumCallItem forumCallItem = InfoLibBLL.GetInfoLibForumCallItem();
            if (forumCallItem != null)
            {
                divforumCallSummaryView.Visible = true;
                InfoLibItem summaryItem = forumCallItem.SummaryItem;
                litSummaryViewContent.Text = summaryItem.ItemHeader.HeaderText;

                if (forumCallItem.DetailedItem != null)
                {
                    hlMoreLink.Visible = true;
                    NameValueCollection nvColl = new NameValueCollection();
                    nvColl.Add(QueryStringHelper.QueryStringParamNames.INFOLIB_ITEMID_INT.Description(),
                        forumCallItem.DetailedItem.InfoLibItemId.ToString());
                    nvColl.Add(QueryStringHelper.QueryStringParamNames.INFOLIB_PARENTID_INT.Description(),
                                            forumCallItem.DetailedItem.ParentId.ToString());
                    nvColl.Add(QueryStringHelper.QueryStringParamNames.INFOLIB_SPECIAL_IDENTIFIER.Description(),
                                                                InfoLibSpecialIdentifiers.Forum_call.EnumValue<int>().ToString());
                    hlMoreLink.NavigateUrl = InfoLibUtil.ConstructInfoLibItemNavigationUrl(nvColl);
                }
            }
        }
    }
}