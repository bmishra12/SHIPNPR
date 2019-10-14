using System;
using System.Web.Management;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessLayer;
using ShiptalkCommon;
using System.Web;
using ShiptalkLogic.BusinessObjects.UI;
using System.Text;
using System.Web.UI;
namespace ShiptalkWeb
{
    public partial class WebEventMailFormatter : System.Web.UI.Page
    {

        public WebBaseErrorEvent RaisedEvent { get; set; }

        private WebEventRequestInformation Request
        {
            get { return (WebEventRequestInformation)Application["WebEventRequestInformation"]; }
        }
        public string RequestUrl
        {
            get
            {
                if (Request != null) return Request.Url;
                else return "N/A";
            }
        }
        public string RequestUserAgent
        {
            get
            {
                if (Request != null) return Request.UserAgent;
                else return "N/A";
            }
        }
        public string RequestUserHostAddress
        {
            get
            {
                if (Request != null) return Request.UserHostAddress;
                else return "N/A";
            }
        }
        public string RequestUser
        {
            get
            {
                if (Request != null) return Request.User;
                else return "N/A";
            }
        }
        public string RequestIsLoggedIn
        {
            get
            {
                if (Request != null) return Request.IsLoggedIn.ToString();
                else return "N/A";
            }
        }
        public string RequestUserInfo
        {
            get
            {
                if (Request != null) return Request.UserInfo;
                else return "N/A";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RaisedEvent = TemplatedMailWebEventProvider.CurrentNotification.Events[0] as WebBaseErrorEvent;

            if (RaisedEvent != null)
            {
                Page.DataBind();
                //if (ConfigUtil.HostingPlace.ToLower() == "orcsweb")
                //{
                    ShiptalkMailMessage mailMessage = new ShiptalkMailMessage(true, ShiptalkMailMessage.MailFrom.ShiptalkResourceCenter);

                    mailMessage.ToList.Add(ConfigUtil.EmailOfTechSupport);
                   // mailMessage.ToList.Add(ConfigUtil.EmailOfCriticalErrorCC);
                    mailMessage.Subject = "Event Notification";
                    mailMessage.Body = errordiv.InnerHtml;


                    //Send Mail here
                    ShiptalkMail mail = new ShiptalkMail(mailMessage);
                    mail.SendMail();
                //}
            }
            
           
          
      
             Application.Remove("WebEventRequestInformation");
        }
        //protected bool IsUserLoggedIn
        //{
        //    get
        //    {
        //        return Request.IsLoggedIn;
        //    }
        //}
    }

    public class WebEventRequestInformation
    {
        public string Url { get; set; }
        public string UserAgent { get; set; }
        public string UserHostAddress { get; set; }
        public string User { get; set; }
        public bool IsLoggedIn { get; set; }
        public string UserInfo { get; set; }

        //public WebEventRequestInformation()
        //{
        //    IsLoggedIn = false;
        //}
    }
}
