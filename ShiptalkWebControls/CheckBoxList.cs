using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace ShiptalkWebControls
{
    [ToolboxData("<{0}:CheckBoxList runat=server></{0}:CheckBoxList>")]
    [ValidationPropertyAttribute("SelectedItemCount")]
    public class CheckBoxList : System.Web.UI.WebControls.CheckBoxList
    {
        private IEnumerable<KeyValuePair<string, string>> _selectedItems;

        #region Constructors

        public CheckBoxList()
        {
           DataBound += CheckBoxList_DataBound;
        }

        #endregion

        #region Properties

        public IEnumerable<KeyValuePair<string, string>> SelectedItems 
        {
            get { return GetSelectedItems(); }
            set
            {
                _selectedItems = value;

                if (value == null || base.Items.Count == 0) return;

                foreach (ListItem listItem in base.Items)
                {
                        foreach (var pair in value)
                        {
                            if (listItem.Value != pair.Key) continue;

                            listItem.Selected = true;
                            break;
                        }
                }
            }
        }

        public int SelectedItemCount
        {
            get { return GetSelectedItems().Count; }
        }

        #endregion 

        #region Methods

        private IList<KeyValuePair<string, string>> GetSelectedItems()
        {
            if (base.Items.Count == 0)
            return null;

            var selectedItems = new List<KeyValuePair<string, string>>();

            foreach (ListItem listItem in base.Items)
                if (listItem.Selected)
                    selectedItems.Add(new KeyValuePair<string, string>(listItem.Value, listItem.Text));

            return selectedItems;
        }

        #endregion

        #region Event Handlers

        private void CheckBoxList_DataBound(object sender, EventArgs e)
        {
            ((CheckBoxList)sender).SelectedItems = _selectedItems;
        }

        #endregion
    }
}
