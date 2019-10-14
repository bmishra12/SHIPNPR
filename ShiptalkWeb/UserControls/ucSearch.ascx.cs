using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShiptalkWeb
{
    public partial class ucSearch : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageControls();
        }

        private void SetPageControls()
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                //loggedInSearch.Visible = true;
            }
            else
            {
                //loggedInSearch.Visible = false;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(
                    string.Format("~/InfoLib/InfoLibSearch.aspx?{0}={1}",
                        QueryStringHelper.QueryStringParamNames.INFOLIB_SEARCH_TEXT.Description(), txtSearch.Text.Trim())
                    );
        }
    }
}