using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShiptalkWebControls
{
    [ControlValueProperty("Text")]
    [ControlBuilder(typeof(TextBoxControlBuilder))]
    [SupportsEventValidation]
    [DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")] 
    [DefaultProperty("Text")] 
    [ValidationProperty("Text")] 
    [DefaultEvent("TextChanged")] 
    [Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")] 
    [ParseChildren(true, "Text")]
    public class TextBox : System.Web.UI.WebControls.TextBox
    {
        private static readonly object EventBlur;

        protected override bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
        {
            ValidateEvent(postDataKey, string.Empty);
            var newText = postCollection[postDataKey];

            if (!ReadOnly && !Text.Equals(newText, StringComparison.Ordinal))
            {
                Text = newText;
            
                return true;
            }

            return false;
        }

        protected override void RaisePostDataChangedEvent()
        {
            if (AutoPostBack && !Page.IsPostBackEventControlRegistered)
            {
                Page.AutoPostBackControl = this;
                if (CausesValidation)
                    Page.Validate(ValidationGroup);
            }

            OnBlur(EventArgs.Empty);
            OnTextChanged(EventArgs.Empty);
        }

        internal void ValidateEvent(string uniqueID, string eventArgument)
        {
            if (Page != null)
                Page.ClientScript.ValidateEvent(uniqueID, eventArgument);
        }

        public event EventHandler Blur
        {
            add
            {
                Events.AddHandler(EventBlur, value);
            }
            remove
            {
                Events.RemoveHandler(EventBlur, value);
            }
        }

        protected virtual void OnBlur(EventArgs e)
        {
            var handler = (EventHandler)Events[EventBlur];
            if (handler != null)
                handler(this, e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            RenderBeginTag(writer);

            if (TextMode == TextBoxMode.MultiLine)
                HttpUtility.HtmlEncode(Text, writer);
            
            RenderEndTag(writer);
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (AutoPostBack && (Page != null))
            {
                string onblurValue = null;
                if (HasAttributes)
                {
                    onblurValue = Attributes["onblur"];

                    if (onblurValue != null)
                        Attributes.Remove("onblur");
                }

                var options = new PostBackOptions(this, string.Empty);

                if (CausesValidation)
                {
                    options.PerformValidation = true;
                    options.ValidationGroup = ValidationGroup;
                }

                if (Page.Form != null)
                {
                    options.AutoPostBack = true;
                }

                writer.AddAttribute("onblur", Page.ClientScript.GetPostBackEventReference(this, String.Empty));
            }

            base.AddAttributesToRender(writer);
        }
    }
}
