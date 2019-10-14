using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace ShiptalkCommon
{
    public sealed class ShiptalkMailMessage
    {
        public enum MailFrom
        {
            ShiptalkResourceCenter = 1,
            ShiptalkTechSupport
        }

        public ShiptalkMailMessage(bool IsHtml, MailFrom fromAddr)
            : this(IsHtml)
        {
            this.From = GetEmailAddressForMailFrom(fromAddr);

        }

        public ShiptalkMailMessage(bool IsHtml, string fromAddress)
            : this(IsHtml)
        {
            this.From = fromAddress;
        }

        private ShiptalkMailMessage(bool IsHtml)
        {
            this.IsBodyHtml = IsHtml;
        }


        private List<string> _ToList = new List<string>();
        public List<string> ToList
        {
            get
            {
                return _ToList;
            }
            set { _ToList = value; }
        }

        private List<string> _CCList = new List<string>();
        public List<string> CCList
        {
            get
            {
                return _CCList;
            }
            set { _CCList = value; }
        }


        private List<string> _BCCList = new List<string>();
        public List<string> BCCList
        {
            get
            {
                return _BCCList;
            }
            set { _BCCList = value; }
        }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string From { get; set; }

        public bool IsBodyHtml { get; private set; }

        private string GetEmailAddressForMailFrom(MailFrom fromAddr)
        {
            string email = string.Empty;
            switch (fromAddr)
            {
                case MailFrom.ShiptalkResourceCenter:
                    email = ConfigUtil.EmailOfResourceCenter;
                    break;
                case MailFrom.ShiptalkTechSupport:
                    email = ConfigUtil.EmailOfTechSupport;
                    break;
            }
            return email;
        }
    }


    public sealed class ShiptalkMail
    {

        public ShiptalkMail(ShiptalkMailMessage shipMailMessage)
        {
            CreateMailMessage(shipMailMessage);
        }

        private MailMessage _MailMessage;

        private MailMessage MailMessageObject
        {
            get
            {
                if (_MailMessage == null)
                    _MailMessage = new MailMessage();

                return _MailMessage;
            }
        }

        /// <summary>
        /// Sends mail using the Mail message object
        /// </summary>
        /// <param name="IsBodyHtml"></param>
        /// <returns></returns>
        public bool SendMail()
        {
            try
            {

                //use CDO to send if the hostingplace is "HP"
                if (ConfigUtil.HostingPlace.ToLower() == "hp")
                {

                    CDO.Message oMsg;
                    
                    ////CDO.IBodyPart iBp;

                    // Create a new message.
                    oMsg = new CDO.Message();
                    //oMsg.From = "shipnpr@air.org";
                    //oMsg.To = "bmishra@air.org";

                    CDO.IConfiguration iConfg;
                    iConfg = oMsg.Configuration;

                    ADODB.Fields oFields = iConfg.Fields;

                     oFields["http://schemas.microsoft.com/cdo/configuration/sendusing"].Value = 2;
                    
                     oFields["http://schemas.microsoft.com/cdo/configuration/smtpserverport"].Value = 25;
                   
                     oFields["http://schemas.microsoft.com/cdo/configuration/smtpserver"].Value = "mailhost.prod";

                  //  oFields["http://schemas.microsoft.com/cdo/configuration/smtpserver"].Value = "localhost";
                 

                    oMsg.Configuration.Fields.Update();


                    oMsg.From = MailMessageObject.From.Address;
                    oMsg.To = string.Join(",", new List<string>(from e in MailMessageObject.To select e.Address).ToArray());
                    oMsg.CC = string.Join(",", new List<string>(from e in MailMessageObject.CC select e.Address).ToArray());
                    oMsg.BCC = string.Join(",", new List<string>(from e in MailMessageObject.Bcc select e.Address).ToArray());
                    oMsg.Subject = MailMessageObject.Subject;
                    //oMsg.TextBody = MailMessageObject.Body;
                    oMsg.HTMLBody = MailMessageObject.Body;


                    // Send mail.
                
                    oMsg.Send();


                    oMsg = null;
                   
                    return true;
                }
                else
                {
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(ConfigUtil.EmailServer);

                    if (ConfigUtil.EmailServerRequiresAuthentication == true)
                    {
                        System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential(ConfigUtil.EmailServerUserName, ConfigUtil.EmailServerPassword);
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = basicAuthenticationInfo;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                    }


                    ApplyBusinessRules();
                    smtp.Send(this.MailMessageObject);
                    return true;

                }
            }
            catch (Exception ex)
            {
                //TODO : Logout
                
                return false;
            }
        }

        private void CreateMailMessage(ShiptalkMailMessage shipMailMessage)
        {
            //Transfer data to our Mail Message object
            shipMailMessage.ToList.ForEach(addr => MailMessageObject.To.Add(new MailAddress(addr)));
            shipMailMessage.CCList.ForEach(addr => MailMessageObject.CC.Add(new MailAddress(addr)));
            shipMailMessage.BCCList.ForEach(addr => MailMessageObject.Bcc.Add(new MailAddress(addr)));

            //Set all values required for our Mail Message object
            MailMessageObject.From = new MailAddress(shipMailMessage.From);
            MailMessageObject.Subject = shipMailMessage.Subject;
            MailMessageObject.Body = shipMailMessage.Body;
            MailMessageObject.IsBodyHtml = shipMailMessage.IsBodyHtml;
        }


        private void ApplyBusinessRules()
        {
            if (this.MailMessageObject == null)
                throw new ShiptalkException("The Mail Message object was not initialized", false, new ArgumentNullException("MailMessage"));

            if (string.IsNullOrEmpty(MailMessageObject.From.Address))
                throw new ShiptalkException("From address is required for email", false);

            if (MailMessageObject.To.Count == 0)
                throw new ShiptalkException("To address is required for email", false);

            if (string.IsNullOrEmpty(MailMessageObject.Subject))
                throw new ShiptalkException("Subject is required for email", false);

            if (MustOverrideEmail)
            {
                //Apply Override Email to ToList/CCList/BCCList
                MailMessageObject.To.ToList<MailAddress>().ForEach(addr => addr = new MailAddress(OverrideEmailAddress));
                MailMessageObject.CC.ToList<MailAddress>().ForEach(addr => addr = new MailAddress(OverrideEmailAddress));
                MailMessageObject.Bcc.ToList<MailAddress>().ForEach(addr => addr = new MailAddress(OverrideEmailAddress));
            }
        }

        //private string Host
        //{
        //    get
        //    {
        //        return ConfigUtil.EmailServer;
        //    }
        //}

        //private string HostingPlace
        //{
        //    get
        //    {
        //        return ConfigUtil.HostingPlace;
        //    }
        //}

        //private string EmailServerUserName
        //{
        //    get
        //    {
        //        return ConfigUtil.EmailServerUserName;
        //    }
        //}

        //private string EmailServerPassword
        //{
        //    get
        //    {
        //        return ConfigUtil.EmailServerPassword;
        //    }
        //}

        //private bool EmailServerRequiresAuthentication
        //{
        //    get
        //    {
        //        return ConfigUtil.EmailServerRequiresAuthentication;
        //    }

        //}

        private string OverrideEmailAddress
        {
            get
            {
                return ConfigUtil.OverrideEmailAddress;
            }
        }

        private bool MustOverrideEmail
        {
            get
            {
                return ConfigUtil.MustOverrideEmail;
            }
        }

    }
}

