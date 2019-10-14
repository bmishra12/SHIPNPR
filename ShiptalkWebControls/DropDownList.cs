using System.Web.UI;

namespace ShiptalkWebControls
{
    [ToolboxData("<{0}:DropDownList runat=server></{0}:DropDownList>")]
    [ValidationProperty("SelectedEnumValue")]
    public class DropDownList : System.Web.UI.WebControls.DropDownList
    {
        #region Properties

        public int SelectedEnumValue
        {
            get { return int.Parse(base.SelectedValue); }
            set { base.SelectedValue = value.ToString(); }
        }

        #endregion
    }
}
