using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShiptalkLogic;
using ShiptalkLogic.BusinessObjects;
using System.Web.Security;

using ShiptalkLogic.BusinessLayer;

namespace ShiptalkWeb
{

    public partial class ShiptalkWeb : System.Web.UI.MasterPage
      
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //TopMenu.DataBind();
            // display number of users currently logged to CMS admin only at the bottom of the page.
   
            Response.AppendHeader("X-FRAME-OPTIONS", "DENY");
       

            int CurrentUserCount = (int)Application["usercount"];

            if (CurrentUserCount == 1)
                lblUserCount.Text = "There is 1 user currently logged in.<br>";
            else
                lblUserCount.Text = "There are " + CurrentUserCount + " users currently logged in.<br>";
            //when session is active and a valid user has logged in to see the count
            if (ShiptalkPrincipal.IsSessionActive && ShiptalkPrincipal.UserId != 0)
            {
                if (AccountInfo.Scope == Scope.CMS && AccountInfo.IsAdmin)
                {
                    lblUserCount.Visible = true;
                }
                else
                {
                    lblUserCount.Visible = false;
                }
            }
        }
       

        //protected void LoginStatus1_LoggedOut(object sender, System.EventArgs e)
        //{
        //    FormsAuthentication.SignOut();
        //    Session.Abandon();
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


        protected bool IsAuthenticated()
        {
            return (ShiptalkPrincipal.Identity.IsAuthenticated && !HttpContext.Current.Session.IsNull() && ShiptalkPrincipal.IsSessionActive);
        }

        private UserAccount GetAccountInfo()
        {
            if (IsAuthenticated())
            {
                return UserBLL.GetUserAccount(ShiptalkPrincipal.UserId);
            }

            return null;
        }



        protected bool CanViewLink(UserLinks LinkItem)
        {
            bool result = false;

            if (IsAuthenticated())
            {

                //Verify access
                var LinkAccessObject = LinkAccessBuilder.Instance.GetLinkAccessConfiguration(LinkItem);

                //Role identifier for User.
                var RoleIdentityForUser = RoleIdentifierList.Instance.GetRoleIdentifier(this.AccountInfo);

                //Get Descriptors for User
                var DescriptorsForUser = UserBLL.GetDescriptorsForUser(this.AccountInfo.UserId, null);


                //Check if the RoleIdentifiers for the Link contains RoleIdentity of the User
                var RoleIdentifersForLink = LinkAccessObject.RoleIdentifiers;
                var DescriptorsNOTAllowed = LinkAccessObject.DescriptorExceptions;
                var DescriptorsAllowed = LinkAccessObject.DescriptorsAllowed;
                if (RoleIdentifersForLink.Contains(RoleIdentityForUser))
                {
                    //Initially, granted access because the RoleIdentity of the User
                    //DOES match the authorized RoleIdentifiers for the requested link.
                    result = true;

                    //If Descriptors that are NOT ALLOWED contains one of the the User's descriptor 
                    //do not show the link
                    if (DescriptorsNOTAllowed != null)
                    {
                        foreach (var descriptor in DescriptorsNOTAllowed)
                        {
                            if (DescriptorsForUser.Contains(descriptor.EnumValue<int>())) return false;
                        }
                    }

                    //If User Descriptors 
                    if (DescriptorsAllowed != null)
                    {
                        foreach (var descriptor in DescriptorsAllowed)
                        {
                            if (DescriptorsForUser.Contains(descriptor.EnumValue<int>())) return true;
                        }
                        result = false;
                    }

                    
                }
            }
            return result;
        }


        #region "Dynamic population of tablinks"
        public enum UserLinks
        {
            Agency = 1,
            User,
            ResourceReport,
            ClientContactForm,
            PAMForm,
            Upload,
            ShipProfile,
            NPRReports
        }




        public sealed class LinkAccess
        {
            public UserLinks Link { get; set; }
            public List<RoleIdentifier> RoleIdentifiers { get; set; }
            public List<Descriptor> DescriptorExceptions { get; set; }
            public List<Descriptor> DescriptorsAllowed { get; set; }
        }


        public class LinkAccessBuilder
        {
            private List<LinkAccess> LinkAccessList { get; set; }
            private static LinkAccessBuilder instance;
            private LinkAccessBuilder() { }
            private void Build()
            {
                LinkAccessList = new List<LinkAccess>();
                instance.AddRoleIDsToAgencyLink();
                instance.AddRoleIDsToUserLink();
                instance.AddRoleIDsToResourceReportLink();
                instance.AddRoleIDsToCCFormLink();
                instance.AddRoleIDsToPAMFormLink();
                instance.AddRoleIDsToUploadLink();
                instance.AddRoleIDsToShipProfileLink();
                instance.AddRoleIDsToNPRReportsLink();
            }
            public static LinkAccessBuilder Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = new LinkAccessBuilder();
                        instance.Build();
                    }
                    return instance;
                }
            }

            protected void AddRoleIDsToAgencyLink()
            {
                UserLinks linkToAdd = UserLinks.Agency;
                List<RoleIdentifier> RoleIDs = new List<RoleIdentifier> { 
                   RoleIdentifierList.Instance.GetRoleIdentifier(Scope.Agency, false), 
                   RoleIdentifierList.Instance.GetRoleIdentifier(Scope.Agency, true),
                   RoleIdentifierList.Instance.GetRoleIdentifier(Scope.SubStateRegion, false),
                   RoleIdentifierList.Instance.GetRoleIdentifier(Scope.SubStateRegion, true),
                   RoleIdentifierList.Instance.GetRoleIdentifier(Scope.State, false),
                   RoleIdentifierList.Instance.GetRoleIdentifier(Scope.State, true),
                   RoleIdentifierList.Instance.GetRoleIdentifier(Scope.State, true, true),
                   RoleIdentifierList.Instance.GetRoleIdentifier(Scope.CMSRegional, false),
                   RoleIdentifierList.Instance.GetRoleIdentifier(Scope.CMS, false),
                   RoleIdentifierList.Instance.GetRoleIdentifier(Scope.CMS, true)
                };

                LinkAccessList.Add(new LinkAccess
                {
                    RoleIdentifiers = RoleIDs,
                    DescriptorExceptions =
                        new List<Descriptor>
                        {
                            Descriptor.OtherStaff_SHIP
                        }
                    ,
                    Link = linkToAdd
                });
            }
            protected void AddRoleIDsToUserLink()
            {
                UserLinks linkToAdd = UserLinks.User;
                List<RoleIdentifier> RoleIDs = new List<RoleIdentifier> { 
                    RoleIdentifierList.Instance.GetRoleIdentifier(Scope.Agency, false), 
                    RoleIdentifierList.Instance.GetRoleIdentifier(Scope.Agency, true),
                    RoleIdentifierList.Instance.GetRoleIdentifier(Scope.SubStateRegion, false),
                    RoleIdentifierList.Instance.GetRoleIdentifier(Scope.SubStateRegion, true),
                    RoleIdentifierList.Instance.GetRoleIdentifier(Scope.State, false),
                    RoleIdentifierList.Instance.GetRoleIdentifier(Scope.State, true),
                    RoleIdentifierList.Instance.GetRoleIdentifier(Scope.State, true, true),
                    RoleIdentifierList.Instance.GetRoleIdentifier(Scope.CMSRegional, false),
                    RoleIdentifierList.Instance.GetRoleIdentifier(Scope.CMS, false),
                    RoleIdentifierList.Instance.GetRoleIdentifier(Scope.CMS, true)
                };

                LinkAccessList.Add(new LinkAccess
                {
                    RoleIdentifiers = RoleIDs,
                    DescriptorExceptions =
                        new List<Descriptor>
                        {
                            Descriptor.OtherStaff_SHIP,
                            Descriptor.OtherStaff_NPR
                        },
                    DescriptorsAllowed = null,
                    Link = linkToAdd
                });
            }
            protected void AddRoleIDsToResourceReportLink()
            {
                UserLinks linkToAdd = UserLinks.ResourceReport;
                List<RoleIdentifier> RoleIDs = new List<RoleIdentifier> { 
                        RoleIdentifierList.Instance.GetRoleIdentifier(Scope.State, true),
                        RoleIdentifierList.Instance.GetRoleIdentifier(Scope.State, true, true),
                        RoleIdentifierList.Instance.GetRoleIdentifier(Scope.CMS, true)
                };

                LinkAccessList.Add(new LinkAccess
                {
                    RoleIdentifiers = RoleIDs,
                    DescriptorExceptions = null,
                    DescriptorsAllowed = null,
                    Link = linkToAdd
                });
            }
            protected void AddRoleIDsToCCFormLink()
            {
                UserLinks linkToAdd = UserLinks.ClientContactForm;
                List<RoleIdentifier> RoleIDs = new List<RoleIdentifier> { 
                       RoleIdentifierList.Instance.GetRoleIdentifier(Scope.Agency, false), 
                       RoleIdentifierList.Instance.GetRoleIdentifier(Scope.Agency, true),
                       RoleIdentifierList.Instance.GetRoleIdentifier(Scope.SubStateRegion, false),
                        RoleIdentifierList.Instance.GetRoleIdentifier(Scope.SubStateRegion, true),
                        RoleIdentifierList.Instance.GetRoleIdentifier(Scope.State, false),
                        RoleIdentifierList.Instance.GetRoleIdentifier(Scope.State, true),
                        RoleIdentifierList.Instance.GetRoleIdentifier(Scope.State, true, true),
                        RoleIdentifierList.Instance.GetRoleIdentifier(Scope.CMS, true)
                };

                LinkAccessList.Add(new LinkAccess
                {
                    RoleIdentifiers = RoleIDs,
                    DescriptorsAllowed = new List<Descriptor>
                    {
                        Descriptor.Counselor,
                        Descriptor.DataSubmitter,
                        Descriptor.DataEditor_Reviewer
                    },
                    DescriptorExceptions = null,
                    Link = linkToAdd
                });
            }
            protected void AddRoleIDsToPAMFormLink()
            {
                UserLinks linkToAdd = UserLinks.PAMForm;
                List<RoleIdentifier> RoleIDs = new List<RoleIdentifier> { 
                       RoleIdentifierList.Instance.GetRoleIdentifier(Scope.Agency, false), 
                       RoleIdentifierList.Instance.GetRoleIdentifier(Scope.Agency, true),
                       RoleIdentifierList.Instance.GetRoleIdentifier(Scope.SubStateRegion, false),
                        RoleIdentifierList.Instance.GetRoleIdentifier(Scope.SubStateRegion, true),
                        RoleIdentifierList.Instance.GetRoleIdentifier(Scope.State, false),
                        RoleIdentifierList.Instance.GetRoleIdentifier(Scope.State, true),
                        RoleIdentifierList.Instance.GetRoleIdentifier(Scope.State, true, true),
                        RoleIdentifierList.Instance.GetRoleIdentifier(Scope.CMS, true)
                };

                LinkAccessList.Add(new LinkAccess
                {
                    RoleIdentifiers = RoleIDs,
                    DescriptorsAllowed = new List<Descriptor>
                    {
                        Descriptor.PresentationAndMediaStaff,
                        Descriptor.DataSubmitter,
                        Descriptor.DataEditor_Reviewer
                    },
                    DescriptorExceptions = null,
                    Link = linkToAdd
                });
            }
            protected void AddRoleIDsToUploadLink()
            {
                UserLinks linkToAdd = UserLinks.Upload;
                List<RoleIdentifier> RoleIDs = new List<RoleIdentifier> { 
                       RoleIdentifierList.Instance.GetRoleIdentifier(Scope.Agency, true),
                        RoleIdentifierList.Instance.GetRoleIdentifier(Scope.SubStateRegion, true),
                        RoleIdentifierList.Instance.GetRoleIdentifier(Scope.State, true),
                        RoleIdentifierList.Instance.GetRoleIdentifier(Scope.State, true, true),
                        RoleIdentifierList.Instance.GetRoleIdentifier(Scope.CMS, true)
                };

                LinkAccessList.Add(new LinkAccess
                {
                    RoleIdentifiers = RoleIDs,
                    DescriptorsAllowed = null,
                    DescriptorExceptions = null,
                    Link = linkToAdd
                });
            }
            protected void AddRoleIDsToShipProfileLink()
            {
                UserLinks linkToAdd = UserLinks.ShipProfile;
                List<RoleIdentifier> RoleIDs = new List<RoleIdentifier> { 
                        RoleIdentifierList.Instance.GetRoleIdentifier(Scope.State, true),
                        RoleIdentifierList.Instance.GetRoleIdentifier(Scope.State, true, true),
                        RoleIdentifierList.Instance.GetRoleIdentifier(Scope.CMS, true)
                };

                LinkAccessList.Add(new LinkAccess { RoleIdentifiers = RoleIDs, DescriptorExceptions = null, DescriptorsAllowed = null, Link = linkToAdd });
            }
            protected void AddRoleIDsToNPRReportsLink()
            {
                UserLinks linkToAdd = UserLinks.NPRReports;
                List<RoleIdentifier> RoleIDs = new List<RoleIdentifier> { 
                       RoleIdentifierList.Instance.GetRoleIdentifier(Scope.Agency, true), 
                       RoleIdentifierList.Instance.GetRoleIdentifier(Scope.Agency, false),
                       RoleIdentifierList.Instance.GetRoleIdentifier(Scope.SubStateRegion, true),
                        RoleIdentifierList.Instance.GetRoleIdentifier(Scope.SubStateRegion, false),
                        RoleIdentifierList.Instance.GetRoleIdentifier(Scope.State, true, true),
                        RoleIdentifierList.Instance.GetRoleIdentifier(Scope.State, true),
                        RoleIdentifierList.Instance.GetRoleIdentifier(Scope.State, false),
                        RoleIdentifierList.Instance.GetRoleIdentifier(Scope.CMS, true),
                        RoleIdentifierList.Instance.GetRoleIdentifier(Scope.CMS, false)
                };

                LinkAccessList.Add(new LinkAccess { RoleIdentifiers = RoleIDs, DescriptorExceptions = null, DescriptorsAllowed = null, Link = linkToAdd });
            }
            public LinkAccess GetLinkAccessConfiguration(UserLinks linkToFind)
            {
                return LinkAccessList.Where(l => l.Link == linkToFind).FirstOrDefault();
            }

        }


        public class RoleIdentifierList
        {
            private List<RoleIdentifier> RoleIdentifiers { get; set; }
            private static RoleIdentifierList instance;
            private RoleIdentifierList() { }
            private void StartBuilder()
            {
                RoleIdentifiers = new List<RoleIdentifier>();
                instance.BuildAgencyRoleIDs();
                instance.BuildSubStateRoleIDs();
                instance.BuildStateRoleIDs();
                instance.BuildCMSRegionRoleIDs();
                instance.BuildCMSRoleIDs();
            }
            public static RoleIdentifierList Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = new RoleIdentifierList();
                        instance.StartBuilder();
                    }
                    return instance;
                }
            }

            protected void BuildAgencyRoleIDs()
            {
                RoleIdentifiers.Add(new RoleIdentifier(Scope.Agency, false));
                RoleIdentifiers.Add(new RoleIdentifier(Scope.Agency, true));
            }
            protected void BuildSubStateRoleIDs()
            {
                RoleIdentifiers.Add(new RoleIdentifier(Scope.SubStateRegion, false));
                RoleIdentifiers.Add(new RoleIdentifier(Scope.SubStateRegion, true));
            }
            protected void BuildStateRoleIDs()
            {
                RoleIdentifiers.Add(new RoleIdentifier(Scope.State, false));
                RoleIdentifiers.Add(new RoleIdentifier(Scope.State, true));
                RoleIdentifiers.Add(new RoleIdentifier(Scope.State, true, true));
            }
            protected void BuildCMSRegionRoleIDs()
            {
                RoleIdentifiers.Add(new RoleIdentifier(Scope.CMSRegional, false));
            }
            protected void BuildCMSRoleIDs()
            {
                RoleIdentifiers.Add(new RoleIdentifier(Scope.CMS, false));
                RoleIdentifiers.Add(new RoleIdentifier(Scope.CMS, true));
            }
            public RoleIdentifier GetRoleIdentifier(Scope scope, bool IsAdmin)
            {
                return GetRoleIdentifier(scope, IsAdmin, false);
            }
            public RoleIdentifier GetRoleIdentifier(Scope scope, bool IsAdmin, bool IsShipDirector)
            {
                return RoleIdentifiers.Where(r => r.IsAdmin == IsAdmin && r.Scope == scope && r.IsShipDirector == IsShipDirector).FirstOrDefault();
            }
            public RoleIdentifier GetRoleIdentifier(UserAccount accountInfo)
            {
                return GetRoleIdentifier(accountInfo.Scope, accountInfo.IsAdmin, accountInfo.IsShipDirector);
            }
        }





        public class RoleIdentifier
        {
            private static int _IdCtr = 1;
            private int _Id = 0;
            private Scope _scope;
            private bool _IsAdmin = false;
            private bool _IsShipDirector = false;

            public RoleIdentifier(Scope scope, bool IsAdmin, bool IsShipDirector)
            {
                _Id = _IdCtr++;
                _scope = scope;
                _IsAdmin = IsAdmin;
                _IsShipDirector = IsShipDirector;
            }
            public RoleIdentifier(Scope scope, bool IsAdmin)
                : this(scope, IsAdmin, false)
            {
            }

            public int Id
            {
                get
                {
                    return _Id;
                }
            }
            public bool IsAdmin
            {
                get
                {
                    return _IsAdmin;
                }
            }
            public bool IsShipDirector
            {
                get
                {
                    return _IsShipDirector;
                }
            }
            public Scope Scope
            {
                get
                {
                    return _scope;
                }
            }
        }











        //private class ListOfUserLinks
        //{
        //    private List<UserLinks> listOfLinks = new List<UserLinks>();
        //    private UserAccount AccountInfo;

        //    public ListOfUserLinks(UserAccount _AccountInfo)
        //    {
        //        this.AccountInfo = _AccountInfo;
        //        PopulateListOfLinksForUser();
        //    }
        //    public List<UserLinks> ListOfLinks
        //    {
        //        get
        //        {
        //            return listOfLinks;
        //        }
        //    }
        //    private void PopulateListOfLinksForUser()
        //    {
        //        //First populate default links
        //        switch (AccountInfo.Scope)
        //        {
        //            case Scope.CMS:
        //            case Scope.CMSRegional:
        //                PopulateDefaultLinkListForCMSUser();
        //                break;
        //            case Scope.State:
        //                PopulateDefaultLinkListForStateUser();
        //                break;
        //            case Scope.SubStateRegion:
        //                PopulateDefaultLinkListForSubStateUser();
        //                break;
        //            case Scope.Agency:
        //                PopulateDefaultLinkListForAgencyUser();
        //                break;
        //            default:
        //                throw new NotSupportedException("Unknown Scope. Requested scope is not supported.");
        //        }
        //    }
        //    private void PopulateDefaultLinkListForCMSUser()
        //    {
        //        listOfLinks.Add(UserLinks.Agency);
        //        listOfLinks.Add(UserLinks.User);

        //        //CMS Admin
        //        if (AccountInfo.IsCMSScope && AccountInfo.IsAdmin)
        //        {
        //            listOfLinks.Add(UserLinks.Upload);
        //            listOfLinks.Add(UserLinks.ResourceReport);
        //            listOfLinks.Add(UserLinks.ShipProfile);
        //            listOfLinks.Add(UserLinks.PAMForm);
        //            listOfLinks.Add(UserLinks.ClientContactForm);
        //        }
        //    }
        //    private void PopulateDefaultLinkListForStateUser()
        //    {
        //        listOfLinks.Add(UserLinks.Agency);
        //        listOfLinks.Add(UserLinks.User);

        //        //State Admin
        //        if (AccountInfo.IsStateAdmin)
        //        {
        //            listOfLinks.Add(UserLinks.Upload);
        //            listOfLinks.Add(UserLinks.ResourceReport);
        //            listOfLinks.Add(UserLinks.ShipProfile);
        //            listOfLinks.Add(

        //            //Ship D Special 
        //            if (AccountInfo.IsShipDirector)
        //            {
        //                //Nothing special as of now.
        //            }
        //        }
        //    }
        //    private void PopulateDefaultLinkListForSubStateUser()
        //    {
        //        //SubState Admin and SubStateUser
        //        listOfLinks.Add(UserLinks.Agency);
        //        listOfLinks.Add(UserLinks.User);
        //    }
        //    private void PopulateDefaultLinkListForAgencyUser()
        //    {
        //        //listOfLinks.Add(UserLinks.Agency);
        //        listOfLinks.Add(UserLinks.User);

        //        //Agency Admins
        //        if (AccountInfo.IsAdmin)
        //        {
        //            listOfLinks.Add(UserLinks.Upload);
        //            listOfLinks.Add(UserLinks.ResourceReport);
        //        }
        //    }
        //}
        #endregion





    }
}
