﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShiptalkWeb
{
    public partial class ShiptalkWebWide : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("X-FRAME-OPTIONS", "DENY");
        }
    }
}
