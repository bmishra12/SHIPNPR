using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShiptalkCommon;
using ShiptalkLogic.DataLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessLayer;
namespace ShiptalkWeb
{
    public partial class Encryptor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btndecrypt_Click(object sender, EventArgs e)
        {
            string encryptedPwd = txtDecrypt.Text.Trim();
           // SystemLayer.Encryption encryptor = new SystemLayer.Encryption();
            UserBLL tt = new UserBLL();
            //lblDecrypt.Text = tt.GetDecryptedPassword(encryptedPwd);
                
        }
    }
}
