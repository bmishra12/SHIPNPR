using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ShiptalkLogic.BusinessObjects;
using System.Web.Security;

using ShiptalkLogic.BusinessLayer;

namespace ShiptalkWeb
{

    public partial class ShiptalkWeb : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            TopMenu.DataBind();
                ShowHideLinks();
        }

        protected void SetControls()
        {
        }

        protected void LoginStatus1_LoggedOut(object sender, System.EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
        }

        //protected bool ShowUserList
        //{
        //    get
        //    {
        //        if (ShiptalkPrincipal.Identity.IsAuthenticated && !HttpContext.Current.Session.IsNull() && HttpContext.Current.Session.Count > 0)
        //        {
        //            if (ShiptalkPrincipal.IsShipDirector | ((ShiptalkPrincipal.StateFIPS == State.GetStateFIPSForCMS()) && ShiptalkPrincipal.IsAdmin))
        //            {
        //                return true;
        //            }
        //        }
        //        return false;
        //    }
        //}


        UserAccount _AccountInfo = null;
        protected UserAccount AccountInfo
        {
            get
            {
                if (_AccountInfo == null)
                    _AccountInfo = GetAccountInfo();

                return _AccountInfo;
            }
        }

        private void ShowHideLinks()
        {
            if (IsAuthenticated())
            {
                //var navBarControl = TopMenu.FindControl("navbar");

                //var agencyLink = navBarControl.FindControl("AgencyLink");
                //if (agencyLink != null)
                //    agencyLink.Visible = CanViewAgency();

                //var userLink = navBarControl.FindControl("UserLink");
                //if (userLink != null)
                //    userLink.Visible = CanViewUser();

                //var uploadLink = navBarControl.FindControl("UploadLink");
                //if (uploadLink != null)
                //    uploadLink.Visible = CanViewUpload();

                //var shipProfileLink = navBarControl.FindControl("ShipProfileLink");
                //if (shipProfileLink != null)
                //    shipProfileLink.Visible = CanViewShipProfile();
                
            }
        }

        protected bool IsAuthenticated()
        {
            return (ShiptalkPrincipal.Identity.IsAuthenticated && !HttpContext.Current.Session.IsNull());
        }

        private UserAccount GetAccountInfo()
        {
            if (ShiptalkPrincipal.Identity.IsAuthenticated && !HttpContext.Current.Session.IsNull())
            {
                return UserBLL.GetUserAccount(ShiptalkPrincipal.UserId);
            }

            return null;
        }

        protected bool CanViewAgency()
        {
            if (IsAuthenticated())
            {
                if (DefaultListOfLinks.Contains(UserLinks.Agency))
                {
                    //return !IsSHIPReadOnly;
                    return true;
                }
            }
            return false;
        }
        protected bool CanViewUser()
        {
            if (IsAuthenticated())
            {
                if (DefaultListOfLinks.Contains(UserLinks.User))
                {
                    //return !IsSHIPReadOnly;
                    return true;
                }
            }
            return false;
        }
        //protected bool CanViewUpload()
        //{
        //    if (IsAuthenticated())
        //    {
        //        if (DefaultListOfLinks.Contains(UserLinks.Upload))
        //        {
        //            return !IsNPRReadOnly;
        //        }
        //    }
        //    return false;
        //}
        //protected bool CanViewRR()
        //{
        //    if (IsAuthenticated())
        //    {
        //    if (DefaultListOfLinks.Contains(UserLinks.ResourceReport))
        //    {
        //        return !IsNPRReadOnly;
        //    }
        //}
        //    return false;
        //}
        //protected bool CanViewShipProfile()
        //{
        //    if (IsAuthenticated())
        //    {
        //        if (DefaultListOfLinks.Contains(UserLinks.ShipProfile))
        //        {
        //            return !IsSHIPReadOnly;
        //        }
        //    }
        //    return false;
        //}

        #region "Dynamic population of tablinks"
        public enum UserLinks
        {
            Agency = 1,
            User,
            Upload,
            ResourceReport,
            ShipProfile
        }

       
        private bool IsNPRReadOnly
        {
            get
            {
                return Descriptors.Contains(Descriptor.OtherStaff_NPR.EnumValue<int>());
            }
        }
        private bool IsSHIPReadOnly
        {
            get
            {
                return Descriptors.Contains(Descriptor.OtherStaff_SHIP.EnumValue<int>());
            }
        }


        private List<UserLinks> _DefaultListOfLinks = null;
        public List<UserLinks> DefaultListOfLinks
        {
            get
            {
                if (_DefaultListOfLinks == null)
                {
                    ListOfUserLinks linksObj = new ListOfUserLinks(AccountInfo);
                    _DefaultListOfLinks = linksObj.ListOfLinks;
                }

                return _DefaultListOfLinks;
            }
        }

        private IEnumerable<int> _Descriptors = null;
        private IEnumerable<int> Descriptors
        {
            get
            {
                if (_Descriptors == null)
                    _Descriptors = UserBLL.GetDescriptorsForUser(AccountInfo.UserId, null);

                return _Descriptors;
            }
        }

        private class ListOfUserLinks
        {
            private List<UserLinks> listOfLinks = new List<UserLinks>();
            private UserAccount AccountInfo;

            public ListOfUserLinks(UserAccount _AccountInfo)
            {
                this.AccountInfo = _AccountInfo;
                PopulateListOfLinksForUser();
            }
            public List<UserLinks> ListOfLinks
            {
                get
                {
                    return listOfLinks;
                }
            }
            private void PopulateListOfLinksForUser()
            {
                //First populate default links
                switch (AccountInfo.Scope)
                {
                    case Scope.CMS:
                    case Scope.CMSRegional:
                        PopulateDefaultLinkListForCMSUser();
                        break;
                    case Scope.State:
                        PopulateDefaultLinkListForStateUser();
                        break;
                    case Scope.SubStateRegion:
                        PopulateDefaultLinkListForSubStateUser();
                        break;
                    case Scope.Agency:
                        PopulateDefaultLinkListForAgencyUser();
                        break;
                    default:
                        throw new NotSupportedException("Unknown Scope. Requested scope is not supported.");
                }
            }
            private void PopulateDefaultLinkListForCMSUser()
            {
                if (AccountInfo.IsCMSRegionalScope)
                {
                    listOfLinks.Add(UserLinks.Agency);
                    listOfLinks.Add(UserLinks.User);
                }
                else
                {
                    //CMS Admin
                    if (AccountInfo.IsAdmin)
                    {
                        listOfLinks.Add(UserLinks.Agency);
                        listOfLinks.Add(UserLinks.User);
                        listOfLinks.Add(UserLinks.Upload);
                        listOfLinks.Add(UserLinks.ResourceReport);
                        listOfLinks.Add(UserLinks.ShipProfile);
                    }
                    //CMS User
                    else
                    {
                        listOfLinks.Add(UserLinks.Agency);
                        listOfLinks.Add(UserLinks.User);
                        listOfLinks.Add(UserLinks.ResourceReport);
                    }
                }
            }
            private void PopulateDefaultLinkListForStateUser()
            {
                //State Admin
                if (AccountInfo.IsStateAdmin)
                {
                    listOfLinks.Add(UserLinks.Agency);
                    listOfLinks.Add(UserLinks.User);
                    listOfLinks.Add(UserLinks.Upload);
                    listOfLinks.Add(UserLinks.ResourceReport);
                    listOfLinks.Add(UserLinks.ShipProfile);

                    //Ship D Special 
                    if (AccountInfo.IsShipDirector)
                    {
                        //Nothing special as of now.
                    }
                }
                //State User
                else
                {
                    listOfLinks.Add(UserLinks.Agency);
                    listOfLinks.Add(UserLinks.User);
                }
            }
            private void PopulateDefaultLinkListForSubStateUser()
            {
                //SubState Admin and SubStateUser
                listOfLinks.Add(UserLinks.Agency);
                listOfLinks.Add(UserLinks.User);
            }
            private void PopulateDefaultLinkListForAgencyUser()
            {
                //listOfLinks.Add(UserLinks.Agency);
                listOfLinks.Add(UserLinks.User);

                //Agency Admins
                if (AccountInfo.IsAdmin)
                {
                    listOfLinks.Add(UserLinks.Upload);
                    listOfLinks.Add(UserLinks.ResourceReport);
                }
            }
        }
        #endregion





    }
}
