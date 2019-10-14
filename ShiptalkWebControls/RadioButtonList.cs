using System.Web.UI;

namespace ShiptalkWebControls
{
    [ToolboxData("<{0}:RadioButtonList runat=server></{0}:RadioButtonList>")]
    [ValidationProperty("SelectedNullableEnumValue")]
    public class RadioButtonList : System.Web.UI.WebControls.RadioButtonList
    {
        #region Properties

        public int SelectedEnumValue 
        {
            get
            {
                var returnValue = 0;
                int.TryParse(base.SelectedValue, out returnValue);
                    
                return returnValue;
            }
            set { base.SelectedValue = value.ToString(); }
        }

        public int? SelectedNullableEnumValue
        {
            get
            {
                var returnValue = 0;
                if(int.TryParse(base.SelectedValue, out returnValue))
                    return returnValue;

                return null;
            }
            set { base.SelectedValue = (value.HasValue) ? value.ToString() : null; }
        }

        #endregion
    }
}
