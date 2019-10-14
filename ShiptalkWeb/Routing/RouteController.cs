using System.Web;

namespace ShiptalkWeb.Routing
{
    public static class RouteController
    {
        public static string AgencySearch() { return "~/agency/search"; }
        public static string AgencyRegister() { return "~/agency/register"; }
        public static string AgencyRegisterSuccess(int id) { return string.Format("~/agency/register/success/{0}", id); }
        public static string AgencyEdit(int id) { return string.Format("~/agency/edit/{0}", id); }
        public static string AgencyView(int id) { return string.Format("~/agency/view/{0}", id); }
        public static string AgencyLocationView(int id) { return string.Format("~/agency/location/view/{0}", id); }
        public static string AgencyLocationEdit(int id) { return string.Format("~/agency/location/edit/{0}", id); }
        public static string AgencyLocationAdd(int agencyId) { return string.Format("~/agency/location/add/{0}", agencyId); }
        public static string AgencyRegionAdd() { return string.Format("~/agency/region/add"); } 
        public static string AgencyRegionView(int id) { return string.Format("~/agency/region/view/{0}", id); }
        public static string AgencyRegionSuccess(int id) { return string.Format("~/agency/region/success/{0}", id); }
        public static string AgencyRegionEdit(int id) { return string.Format("~/agency/region/edit/{0}", id); }

        //Form routes.
        public static string CcfAdd() { return string.Format("~/forms/ccf/add"); }
        public static string CcfAddInAgency(int agencyId) { return string.Format("~/forms/ccf/add-a-{0}", agencyId); }
        public static string CcfAddSimilarContact(int id) { return string.Format("~/forms/ccf/add-c-{0}", id); }
        public static string CcfView(int id) { return string.Format("~/forms/ccf/view/{0}", id); }
        public static string CcfEdit(int id) { return string.Format("~/forms/ccf/edit/{0}", id); }
        public static string CcfSearch() { return "~/forms/CCf/search"; }

        public static string PamAdd() { return string.Format("~/forms/pam/add"); }
        public static string PamAddInAgency(int agencyId) { return string.Format("~/forms/pam/add-a-{0}", agencyId); }
        public static string PamView(int id) { return string.Format("~/forms/pam/view/{0}", id); }
        public static string PamEdit(int id) { return string.Format("~/forms/pam/edit/{0}", id); }
        public static string PamSearch() { return "~/forms/pam/search"; }


        //User based routes.
		public static string UserHome() { return "~/user/home"; }
        public static string ChangePassword() { return "~/user/changepassword"; }
        public static string UserAdd() { return "~/user/admin/useradd"; }
        public static string AddPresentor(string StateFIPS) { return string.Format("~/user/admin/addpresentor/{0}", StateFIPS); }
        public static string EditMyProfile() { return "~/user/editmyprof"; }
        public static string EditMyEmail() { return "~/user/editmyemail"; }
       
        public static string UserSearch() { return "~/user/usersearch"; }
        public static string UserEdit(int id) { return string.Format("~/user/admin/edit/{0}", id); }
        public static string UserView(int id) { return string.Format("~/user/admin/view/{0}", id); }

        public static string Inactivity180() { return "~/user/inactivity180"; }

        public static string InActiveUserList() { return string.Format("~/user/admin/InActiveUserList"); }


        public static string UserRegionalProfileView(int id) { return string.Format("~/user/admin/view/regprofile/{0}", id); }
        public static string UserRegionalProfileEdit(int id) { return string.Format("~/user/admin/edit/regprofile/{0}", id); }

        
        //User Agency routes
        public static string UserAgencyProfileView(int UserId, int AgencyId) { return string.Format("~/user/admin/agencyprofile/view/{0}", UserId.ToString() + "-" + AgencyId.ToString()); }
        public static string UserAgencyProfileEdit(int UserId, int AgencyId) { return string.Format("~/user/admin/agencyprofile/edit/{0}", UserId.ToString() + "-" + AgencyId.ToString()); }
        public static string UserAgencyProfileAdd(int UserId) { return string.Format("~/user/admin/agencyprofile/add/{0}", UserId.ToString());}


        //User Sub State Region routes
        public static string UserSubStateProfileView(int UserId, int SubStateRegionId) { return string.Format("~/user/admin/substprofile/view/{0}", UserId.ToString() + "-" + SubStateRegionId.ToString()); }
        public static string UserSubStateProfileEdit(int UserId, int SubStateRegionId) { return string.Format("~/user/admin/substprofile/edit/{0}", UserId.ToString() + "-" + SubStateRegionId.ToString()); }
        public static string UserSubStateProfileAdd(int UserId) { return string.Format("~/user/admin/substprofile/add/{0}", UserId.ToString()); }


        //User Admin based routes
        public static string UserRegistrationsPending() { return "~/user/admin/pendingusers"; }
        public static string UserRegistrationsPendingSelect(int id) { return string.Format("~/user/admin/pendingusers/{0}", id); }
        public static string UserRegistrationPendingEmails() { return "~/user/admin/pendingemails";}

        public static string UserPendingUniqueIds() { return "~/user/admin/pendinguniqueids"; }
        public static string UserPendingUniqueIdSelect(int id) { return string.Format("~/user/admin/pendinguniqueids/{0}", id); }
        
        //added on 07/18/2012 : Lavanya
        public static string EmailChangeVerificationsPending() { return "~/user/admin/PendingEmailChange"; }
        public static string EmailChangeVerificationsPendingSelect(int id) { return string.Format("~/user/admin/PendingEmailChangeView/{0}", id); }
        public static string PendingEmailChangeEdit(int id) { return string.Format("~/user/admin/PendingEmailChangeEdit/{0}", id); }



        //View/Download list of Users the User has access to - including their UserID
        public static string UserList() { return string.Format("~/user/admin/userlist"); }

        //View/Download list of InActive Users
       // public static string InActiveUserList() { return string.Format("~/user/admin/InActiveUserList"); }
        
        //Upload status routes
        public static string UploadStatus() { return "~/upload/uploadstatus"; }
        public static string UploadFile() { return "~/upload/uploadfile"; }

        //User Agency Preview
        public static string AgencyPreview(int id) { return string.Format("~/SHIP/view/{0}", id); }
        //Ship Profile Edit
        public static string ShipProfileEdit(string id) { return string.Format("~/SHIP/edit/{0}", id); }
        public static string ShipProfileView() { return "~/SHIP/view"; }

        //UserManual

        public static string UserManual() { return "~/Npr/Docs/UserManual.aspx"; }

        //NPRReports - CCSummaryReport
        public static string NprReports() { return "~/NPRReports/view"; }
        public static string NprSummaryReport() { return "~/NPRReports/view/summary"; }
        public static string NprSummaryReportPreview() { return "~/NPRReports/view/preview"; }

                             
       //reports
        public static string ReportSubstateSearch() { return "~/reports/substate/search"; }
        public static string ReportSubstateAdd() { return "~/reports/substate/add"; }
        public static string ReportSubstateView(int id) { return string.Format("~/reports/substate/view/{0}", id); }
        public static string ReportSubstateEdit(int id) { return string.Format("~/reports/substate/edit/{0}", id); }


        //NPRReports - PAMSummaryReport                     
        public static string NprPAMReports() { return "~/NPRReports/pam/view"; }
        public static string NprPAMSummaryReport() { return "~/NPRReports/pam/view/summary"; }
        public static string NprPAMSummaryReportPreview() { return "~/NPRReports/pam/view/preview"; }
        
        //Resource Reports routes
        public static string ResourceReportSearch() { return "~/Resource/Forms/search"; }
        public static string ResourceReportView(int id) { return string.Format("~/resource/forms/view/{0}", id); }
        public static string ResourceReportEdit(int id) { return string.Format("~/resource/forms/edit/{0}", id); }
        public static string ResourceReportAdd(int id) { return string.Format("~/resource/forms/add/{0}", id); }

        //Special Fields
        public static string SpeciaFieldsSearch(string DataForm) { return string.Format("~/admin/specialfields/search/{0}",DataForm); }
        public static string SpeciaFieldsView(int SpecialFieldID) { return string.Format("~/admin/specialfields/view/{0}", SpecialFieldID); }
        public static string SpeciaFieldsEdit(int SpecialFieldID) { return string.Format("~/admin/specialfields/edit/{0}", SpecialFieldID); }
        public static string SpeciaFieldsAdd(int FormType, string StateCode) { return string.Format("~/admin/specialfields/add/{0},{1}", FormType, StateCode); }

        //InfoLib Routes
        public static string ViewInfoLibItems(int ParentId) { return string.Format("~/infolib/viewitems/{0}", ParentId); }
        //public static string ViewInfoLibItem(int ParentId) { return string.Format("~/infolib/viewitem/{0}", ParentId); }
        public static string ViewInfoLibItemsTopLevel() { return "~/infolib/viewitemstoplevel/"; }
        public static string AddInfoLibItem(int ParentId) { return string.Format("~/infolib/addinfolibitem/{0}", ParentId); }
        public static string EditInfoLibItem(int ParentId, int InfoLibItemId) { return string.Format("~/infolib/editinfolibitem/{0}/{1}", ParentId, InfoLibItemId); }
        public static string DeleteInfoLibItem(int ParentId, int InfoLibItemId) { return string.Format("~/infolib/delinfolibitem/{0}/{1}", ParentId, InfoLibItemId); }

        public static void RouteTo(string route)
        {
            RouteTo(route, true);
        }

        public static void RouteTo(string route, bool endResponse)
        {
            HttpContext.Current.Response.Redirect(route, endResponse);

            //getting exception here need to resolve this first
            //var NewEmail = document.getElementById('ctl00_body1_formView_Email').value;
            //in the above statement element id is not same for all edit profiles due to this JS is not working fine
        }
    }
}
