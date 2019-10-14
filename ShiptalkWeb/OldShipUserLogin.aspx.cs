using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Transactions;

using ShiptalkWeb.Routing;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;

using ShiptalkCommon;

namespace ShiptalkWeb
{
    public partial class OldShipUserLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.DataBind();
        }

        private string UserName { get { return ((TextBox)LoginView1.FindControl("txtUsername")).Text.Trim(); } }
        private string Password { get { return ((TextBox)LoginView1.FindControl("txtPassword")).Text.Trim(); } }
        private Label LoginError { get { return ((Label)LoginView1.FindControl("LoginError")); } }


        private void DisplayMessage(string ErrorMessage, bool IsError)
        {
            LoginError.Text = ErrorMessage;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //TODO: Validation to be replaced with ProxyValidator
            Page.Validate("ChangePasswordGroup");
            if (Page.IsValid)
            {
                OldShipUserInfo oldinfo = RegisterUserBLL.GetOldShipUserInfo(UserName, Password);
                if (oldinfo != null)
                {
                    Session["OldShipUserInfo"] = oldinfo;
                    Response.Redirect("~/UserRegistration.aspx");
                }
                else
                    DisplayMessage("The Username and Password does not match a valid account.", true);
            }
        }
    }
}
