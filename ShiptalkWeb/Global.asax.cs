using System;
using System.Web;
using System.Web.Security;
using System.Web.Routing;
using ShiptalkWeb.Routing;
using ShiptalkCommon;
using System.Web.Management;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects.UI;
using System.Text;

namespace ShiptalkWeb
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
            MakeCount(CountType.Reset);

         //sammit
            WordCache.LoadStaticCache();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            MakeCount(CountType.Add);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
         
            if (HttpContext.Current.Request.IsSecureConnection.Equals(false) && ConfigUtil.SecureAllPages)
            {
                Response.Redirect("https://" + Request.ServerVariables["HTTP_HOST"] + HttpContext.Current.Request.RawUrl);
            }
        }


        protected void Application_EndRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
           
            bool RedirectToCustomErrorPage = true;

            string ErrorPagesVirPath = "~/ErrorPages/";
            string Error404Page = ErrorPagesVirPath + "404.aspx";
            string CustomErrorPage = ErrorPagesVirPath + "CustomError.aspx";
            string SessionUnavailablePage = ErrorPagesVirPath + "SessionExpired.aspx";
            string UnAuthorizedAccessPage = ErrorPagesVirPath + "UnAuthorizedAccess.aspx";
           
            try
            {
                Exception Ex = HttpContext.Current.Error;
                var request = HttpContext.Current.Request;
                string mesg = Ex.Message.ToLower();
                string pagePath = Request.Url.PathAndQuery.ToString();
                string WEB_EVENT_NAME = "WebEventRequestInformation";
                bool IsAuthenticated = (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated);
           
                bool IsUserLoggedIn = IsAuthenticated && ShiptalkPrincipal.IsSessionActive;
                string UserInfo = IsUserLoggedIn ? GetUserInfo(ShiptalkPrincipal.UserId) : "Unhandled error. Caught in global.asax.";
                ErrorHandlerUtil.HandleError(Ex, HttpContext.Current, UserInfo );

                if (Ex is HttpUnhandledException)
                {
                    //The default action: Must redirect to custom page. The event must be logged before that.
                    RedirectToCustomErrorPage = true;
                    
                    Application.Add(WEB_EVENT_NAME, CreateWebEventRequestInfo(request));
                }
                else if (Ex is HttpException)
                {
                    Server.ClearError();

                    HttpException httpEx = Ex as HttpException;
                    if (httpEx.GetHttpCode() == 404)
                    {
                        if (pagePath.ToLower().EndsWith(".aspx"))
                        {
                            //We need to redirect User to 404 page.
                            RedirectToCustomErrorPage = false;
                            //For now, we are not going to log this event.
                            //Application.Add(WEB_EVENT_NAME, CreateWebEventRequestInfo(request));
                            Response.Redirect(Error404Page);
                            return;
                        }
                        else
                        {
                            //Here we do not take any action. We do not want to keep logging for some cached image request that does not exist.

                            //404 for Gif, JPG and other requests handled here. fakeimg.jpg 
                            //If we try to redirect user to another page, user will get 
                            //undesired results, just because of a missing insignificant image. 
                            Response.ClearContent();
                            RedirectToCustomErrorPage = false;
                            return;
                        }
                    }
                }
                else if (Ex is ShiptalkException)
                {
                    ShiptalkException shipEx = Ex as ShiptalkException;
                    if (shipEx != null && shipEx.ExceptionType.HasValue)
                    {
                        if (shipEx.ExceptionType.Value == ShiptalkException.ShiptalkExceptionTypes.UN_AUTHORIZED_EXCEPTION)
                        {
                            //Redirect if session expired.
                            //We're not going to log this event.
                            Server.ClearError();
                            RedirectToCustomErrorPage = false;
                            Response.Redirect(UnAuthorizedAccessPage, true);
                        }
                        else if (shipEx.ExceptionType.Value == ShiptalkException.ShiptalkExceptionTypes.SESSION_EXPIRED_OR_UNAVAILABLE)
                        {
                            //Redirect if session expired.
                            //We're not going to log this event.
                            Server.ClearError();
                            RedirectToCustomErrorPage = false;
                            Response.Redirect(SessionUnavailablePage, true);
                        }
                    }
                }
                else if(Ex is  System.Security.SecurityException)
                {
                    //Redirect if session expired.
                    //We're not going to log this event.
                    Server.ClearError();
                    RedirectToCustomErrorPage = false;
                    Response.Redirect(UnAuthorizedAccessPage, true);
                }
            }
            catch (Exception OuterEx) { RedirectToCustomErrorPage = true; }

            if (RedirectToCustomErrorPage) Server.Transfer(CustomErrorPage);


            

            //Response.Write(string.Format("<h1>{0}</h1>", Server.GetLastError().StackTrace.ToString()));
            //Response.Flush();
            //Response.End();

        }

        
        private WebEventRequestInformation CreateWebEventRequestInfo(HttpRequest request)
        {
            string _Url = request.Url.ToString();
            string WSUserAccount = request.LogonUserIdentity.Name;
            string RequestingUserAgent = request.UserAgent;
            string RequestingHostAddress = request.UserHostAddress;
            bool IsAuthenticated = (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated);
            bool IsUserLoggedIn = IsAuthenticated && ShiptalkPrincipal.IsSessionActive;
            return (
                       new WebEventRequestInformation
                       {
                           IsLoggedIn = IsUserLoggedIn,
                           Url = _Url,
                           User = WSUserAccount,
                           UserAgent = RequestingUserAgent,
                           UserHostAddress = RequestingHostAddress,
                           UserInfo = IsUserLoggedIn ? GetUserInfo(ShiptalkPrincipal.UserId) : string.Empty
                       }
                  );
        }

        private string GetUserInfo(int UserId)
        {
            UserViewData UserData = UserBLL.GetUser(UserId);


            string fmt = "{0} : {1}</BR>";

            StringBuilder sb = new StringBuilder();
            //sb.AppendFormat(fmt, "UserId", UserData.UserId);
            sb.AppendFormat(fmt, "First Name", UserData.FirstName);
            sb.AppendFormat(fmt, "Middle Name", UserData.MiddleName);
            sb.AppendFormat(fmt, "Last Name", UserData.LastName);
            sb.AppendFormat(fmt, "Email", UserData.PrimaryEmail);
            sb.AppendFormat(fmt, "Primary Phone", UserData.PrimaryPhone);
            sb.AppendFormat(fmt, "Scope", UserData.Scope.Description());
            sb.AppendFormat(fmt, "IsAdmin", UserData.IsAdmin.ToString());
            sb.AppendFormat(fmt, "State", UserData.StateName);


            return sb.ToString();
        }

        protected void Session_End(object sender, EventArgs e)
        {
            MakeCount(CountType.Dec);
        }

        protected void Application_End(object sender, EventArgs e)
        {
            //throw new ApplicationException("This is an unhandled exception. Application was shut down");


            //Exception Ex = HttpContext.Current.Error;
            //ErrorHandlerUtil.HandleError(Ex, HttpContext.Current, "****Application ended abruptly***");

        }

        private void ApplicationException(string p)
        {
            throw new NotImplementedException();
        }

        private enum CountType
        {
            Add,
            Dec,
            Reset
        }

        private void MakeCount(CountType ct)
        {
            switch (ct)
            {
                    //Prakash 11/26/12 - Added Code to Handle the null Refernce exceptions
                
                case CountType.Add:
                    Application.Lock();
                    if (Application["UserCount"] == null)
                    {
                        Application["UserCount"] = 1;
                    }
                    else
                    {
                        Application["UserCount"] = (int)Application["UserCount"] + 1;
                    }
                    Application.UnLock();
                    break;

                //case CountType.Add:
                //    Application.Lock();
                //    Application["UserCount"] = (int)Application["UserCount"] + 1;
                //    Application.UnLock();
                //    break;

                case CountType.Dec:
                    Application.Lock();
                    Application["UserCount"] = (int)Application["UserCount"] - 1;
                    Application.UnLock();
                    break;
                case CountType.Reset:
                    Application.Lock();
                    Application["UserCount"] = 0;
                    Application.UnLock();
                    break;
            }
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            //#if (!DEBUG)

            //Agency Routes.
            routes.Add("agency.register", new AuthorizeRoute("agency.register", "agency/register", "~/Agency/AgencyRegister.aspx", true));
            routes.Add("agency.register.success", new AuthorizeRoute("agency.register.success", "agency/register/success/{id}", "~/Agency/AgencyView.aspx", true));
            routes.Add("agency.view", new AuthorizeRoute("agency.view", "agency/view/{id}", "~/Agency/AgencyView.aspx", true));
            routes.Add("agency.edit", new AuthorizeRoute("agency.edit", "agency/edit/{id}", "~/Agency/AgencyEdit.aspx", true));
            routes.Add("agency.search", new AuthorizeRoute("agency.search", "agency/search", "~/Agency/AgencySearch.aspx", true));

            routes.Add("agency.location.add", new AuthorizeRoute("agency.location.add", "agency/location/add/{agencyId}", "~/Agency/LocationAdd.aspx", true));
            routes.Add("agency.location.view", new AuthorizeRoute("agency.location.view", "agency/location/view/{id}", "~/Agency/LocationView.aspx", true));
            routes.Add("agency.location.edit", new AuthorizeRoute("agency.location.edit", "agency/location/edit/{id}", "~/Agency/LocationEdit.aspx", true));
            routes.Add("agency.region.add", new AuthorizeRoute("agency.region.add", "agency/region/add", "~/Agency/SubStateRegionAdd.aspx", true));
            routes.Add("agency.region.view", new AuthorizeRoute("agency.region.view", "agency/region/view/{id}", "~/Agency/SubStateRegionView.aspx", true));
            routes.Add("agency.region.success", new AuthorizeRoute("agency.region.success", "agency/region/success/{id}", "~/Agency/SubStateRegionView.aspx", true));
            routes.Add("agency.region.edit", new AuthorizeRoute("agency.region.edit", "agency/region/edit/{id}", "~/Agency/SubStateRegionEdit.aspx", true));

            //User Agency Preview
            routes.Add("agency.profileview", new AuthorizeRoute("agency.profileview", "ship/agencyprofileview", "~/Ship/SHIPAgencyProfileView.aspx", true));

           //User Routes.
            routes.Add("user.home", new AuthorizeRoute("user.home", "user/userhome", "~/User/UserHome.aspx", true));
            routes.Add("user.changepassword", new AuthorizeRoute("user.changepassword", "user/changepassword", "~/User/ChangePassword.aspx", true));
            routes.Add("user.usersearch", new AuthorizeRoute("user.usersearch", "user/usersearch", "~/User/UserSearch.aspx", true));
            routes.Add("user.editmyprof", new AuthorizeRoute("user.editmyprof", "user/editmyprof", "~/User/EditMyProfile.aspx", true));
            routes.Add("user.editmyemail", new AuthorizeRoute("user.editmyemail", "user/editmyemail", "~/User/ChangeEmail.aspx", true));

            routes.Add("user.inactivity180", new AuthorizeRoute("user.inactivity180", "user/inactivity180", "~/User/Inactivity180.aspx", true));


            //Forms
            routes.Add("forms.ccf.add", new AuthorizeRoute("forms.ccf.add", "forms/ccf/add", "~/Npr/Forms/CCF.aspx", false));
            routes.Add("forms.ccf.add-a", new AuthorizeRoute("forms.ccf.add-a", "forms/ccf/add-a-{id}", "~/Npr/Forms/CCF.aspx", false));
            routes.Add("forms.ccf.add-c", new AuthorizeRoute("forms.ccf.add-c", "forms/ccf/add-c-{id}", "~/Npr/Forms/CCF.aspx", false));
            routes.Add("forms.ccf.view", new AuthorizeRoute("forms.ccf.view", "forms/ccf/view/{id}", "~/Npr/Forms/CCFView.aspx", false));
            routes.Add("forms.ccf.edit", new AuthorizeRoute("forms.ccf.edit", "forms/ccf/edit/{id}", "~/Npr/Forms/CCFEdit.aspx", false));
            routes.Add("forms.ccf.search", new AuthorizeRoute("forms.ccf.search", "forms/ccf/search", "~/Npr/Forms/CCFSearch.aspx", false));

            routes.Add("forms.pam.add", new AuthorizeRoute("forms.pam.add", "forms/pam/add", "~/Npr/Forms/PAMF.aspx", false));
            routes.Add("forms.pam.add-a", new AuthorizeRoute("forms.pam.add-a", "forms/pam/add-a-{id}", "~/Npr/Forms/PAMF.aspx", false));
            routes.Add("forms.pam.view", new AuthorizeRoute("forms.pam.view", "forms/pam/view/{id}", "~/Npr/Forms/PAMFView.aspx", false));
            routes.Add("forms.pam.edit", new AuthorizeRoute("forms.pam.edit", "forms/pam/edit/{id}", "~/Npr/Forms/PAMFEdit.aspx", false));
            routes.Add("forms.pam.search", new AuthorizeRoute("forms.pam.search", "forms/pam/search", "~/Npr/Forms/PAMFSearch.aspx", false));

            //Reports 
            routes.Add("reports.substate.search", new AuthorizeRoute("reports.substate.search", "reports/substate/search", "~/Npr/Reports/SubStateRegionSearchForReport.aspx", false));
            routes.Add("reports.substate.add", new AuthorizeRoute("reports.substate.add", "reports/substate/add", "~/Npr/Reports/SubStateRegionAddForReport.aspx", false));
            routes.Add("reports.substate.view", new AuthorizeRoute("reports.substate.view", "reports/substate/view/{id}", "~/Npr/Reports/SubStateRegionViewForReport.aspx", false));
            routes.Add("reports.substate.edit", new AuthorizeRoute("reports.substate.edit", "reports/substate/edit/{id}", "~/Npr/Reports/SubStateRegionEditForReport.aspx", false));

            routes.Add("reports.substate.success", new AuthorizeRoute("reports.substate.success", "reports/substate/success/{id}", "~/Npr/Reports/SubStateRegionViewForReport.aspx", true));

            
            //-----User Admin Routes
            routes.Add("user.admin.userview", new AuthorizeRoute("user.admin.userview", "user/admin/view/{id}", "~/User/Admin/UserView.aspx", true));
            routes.Add("user.admin.useradd", new AuthorizeRoute("user.admin.useradd", "user/admin/useradd", "~/User/Admin/UserAdd.aspx", true));
            routes.Add("user.admin.addpresentor", new AuthorizeRoute("user.admin.addpresentor", "user/admin/addpresentor/{StateFIPS}", "~/User/Admin/AddPresentor.aspx", true));
            routes.Add("user.admin.useredit", new AuthorizeRoute("user.admin.useredit", "user/admin/edit/{id}", "~/User/Admin/UserEdit.aspx", true));

            routes.Add("user.admin.userlist", new AuthorizeRoute("user.admin.userlist", "user/admin/userlist", "~/User/Admin/UserList.aspx", true));
            routes.Add("user.admin.InactiveUserList", new AuthorizeRoute("user.admin.InActiveUserList", "user/admin/InActiveUserList", "~/User/Admin/InActiveUserList.aspx", true));

            //User Agency Profile Routes
            routes.Add("user.admin.agencyprofview", new AuthorizeRoute("user.admin.agencyprofview", "user/admin/agencyprofile/view/{params}", "~/User/Admin/UserAgencyProfileView.aspx", true));
            routes.Add("user.admin.agencyprofedit", new AuthorizeRoute("user.admin.agencyprofedit", "user/admin/agencyprofile/edit/{params}", "~/User/Admin/UserAgencyProfileEdit.aspx", true));
            routes.Add("user.admin.agencyprofadd", new AuthorizeRoute("user.admin.agencyprofadd", "user/admin/agencyprofile/add/{id}", "~/User/Admin/UserAgencyProfileAdd.aspx", true));

            //User Sub State Regional Profile Routes
            routes.Add("user.admin.substprofview", new AuthorizeRoute("user.admin.substprofview", "user/admin/substprofile/view/{params}", "~/User/Admin/UserSubStateProfileView.aspx", true));
            routes.Add("user.admin.substprofedit", new AuthorizeRoute("user.admin.substprofedit", "user/admin/substprofile/edit/{params}", "~/User/Admin/UserSubStateProfileEdit.aspx", true));
            routes.Add("user.admin.substprofadd", new AuthorizeRoute("user.admin.substprofadd", "user/admin/substprofile/add/{id}", "~/User/Admin/UserSubStateProfileAdd.aspx", true));

            routes.Add("user.admin.pendingusers", new AuthorizeRoute("user.admin.pendingusers", "user/admin/pendingusers", "~/User/Admin/PendingUserRegistrations.aspx", true));
            routes.Add("user.admin.pendingemails", new AuthorizeRoute("user.admin.pendingemails", "user/admin/pendingemails", "~/User/Admin/PendingUserEmailVerification.aspx", true));
            routes.Add("user.admin.pendingusers.select", new AuthorizeRoute("user.admin.pendingusers.select", "user/admin/pendingusers/{id}", "~/User/Admin/PendingRegistrationUserView.aspx", true));

            routes.Add("user.admin.pendinguniqueids", new AuthorizeRoute("user.admin.pendinguniqueids", "user/admin/pendinguniqueids", "~/User/Admin/PendingUniqueIdRequests.aspx", true));
            routes.Add("user.admin.pendinguniqueids.select", new AuthorizeRoute("user.admin.pendinguniqueids.select", "user/admin/pendinguniqueids/{id}", "~/User/Admin/PendingUniqueIdUserView.aspx", true));

            //added on 07/18/2012 : Lavanya
            routes.Add("user.admin.PendingEmailChange", new AuthorizeRoute("user.admin.PendingEmailChange", "user/admin/PendingEmailChange", "~/User/Admin/PendingEmailChangeVerifications.aspx", true));
            routes.Add("user.admin.PendingEmailChange.select", new AuthorizeRoute("user.admin.PendingEmailChange.select", "user/admin/PendingEmailChangeView/{id}", "~/User/Admin/PendingEmailChangeUserView.aspx", true));
            routes.Add("user.admin.PendingEmailChangeEdit", new AuthorizeRoute("user.admin.PendingEmailChangeEdit", "user/admin/PendingEmailChangeEdit/{id}", "~/User/Admin/PendingEmailChangeUserEdit.aspx", true));

            //------ Upload Routes
            routes.Add("upload.uploadfile", new AuthorizeRoute("upload.uploadfile", "upload/uploadfile", "~/Upload/UploadFile.aspx", true));
            routes.Add("upload.uploadstatus", new AuthorizeRoute("upload.uploadstatus", "upload/uploadstatus", "~/Upload/UploadStatus.aspx", true));

            //Ship Profile
            routes.Add("ship.edit", new AuthorizeRoute("ship.edit", "ship/edit/{id}", "~/Ship/SHIPProfileEdit.aspx", true));
            routes.Add("ship.view", new AuthorizeRoute("ship.view", "ship/view", "~/Ship/SHIPProfileView.aspx", true));

            //Npr Docs

            routes.Add("npr.docs.usermanual", new AuthorizeRoute("npr.docs.usermanual", "npr/docs/usermanual", "~/npr/docs/usermanual.aspx", true));

            //NprReports 
            routes.Add("nprreports.view", new AuthorizeRoute("nprreports.view", "nprreports/view", "~/ShipNPR_Reports/NprReports.aspx", true));
            routes.Add("nprreports.view.summary", new AuthorizeRoute("nprreports.view.summary", "nprreports/view/summary", "~/ShipNPR_Reports/CCSummaryReport.aspx", true));
            routes.Add("nprreports.view.preview", new AuthorizeRoute("nprreports.view.preview", "nprreports/view/preview", "~/ShipNPR_Reports/CCSummaryReportPreview.aspx", true));
            
            routes.Add("nprreports.pam.view", new AuthorizeRoute("nprreports.pam.view", "nprreports/pam/view", "~/ShipNPR_Reports/NprPAMReportSearch.aspx", true));
            routes.Add("nprreports.pam.view.summary", new AuthorizeRoute("nprreports.pam.view.summary", "nprreports/pam/view/summary", "~/ShipNPR_Reports/PAMSummaryReport.aspx", true));
            routes.Add("nprreports.pam.view.preview", new AuthorizeRoute("nprreports.pam.view.preview", "nprreports/pam/view/preview", "~/ShipNPR_Reports/PAMSummaryReportPreview.aspx", true));

            //-- Resource Reports Routes
            routes.Add("Resource.Forms.search", new AuthorizeRoute("Resource.Forms.search", "Resource/Forms/search", "~/Npr/Forms/ResourceReportSearch.aspx", true));
            routes.Add("resource.forms.view", new AuthorizeRoute("resource.forms.view", "resource/forms/view/{id}", "~/Npr/Forms/ResourceReportFView.aspx", true));
            routes.Add("resource.forms.edit", new AuthorizeRoute("resource.forms.edit", "resource/forms/edit/{id}", "~/Npr/Forms/ResourceReportFEdit.aspx", true));
            routes.Add("resource.forms.add", new AuthorizeRoute("resource.forms.add", "resource/forms/add/{id}", "~/Npr/Forms/ResourceReportFAdd.aspx", true));

            //-- Special Field Routes
            routes.Add("admin.specialfields.search", new AuthorizeRoute("admin.specialfields.search", "admin/specialfields/search/{DataForm}", "~/Npr/Forms/SpecialFieldsFSearch.aspx", true));
            routes.Add("admin.specialfields.view", new AuthorizeRoute("admin.specialfields.view", "admin/specialfields/view/{SpecialFieldID}", "~/Npr/Forms/SpecialFieldsFView.aspx", true));
            routes.Add("admin.specialfields.edit", new AuthorizeRoute("admin.specialfields.edit", "admin/specialfields/edit/{SpecialFieldID}", "~/Npr/Forms/SpecialFieldsFEdit.aspx", true));
            routes.Add("admin.specialfields.add", new AuthorizeRoute("admin.specialfields.add", "admin/specialfields/add/{FormType},{StateCode}", "~/Npr/Forms/SpecialFieldsFAdd.aspx", true));


            //InfoLib routes
            routes.Add("infolib.viewitems", new AuthorizeRoute("infolib.viewitems", "infolib/viewitems/{ParentId}", "~/InfoLib/InfoLibItems.aspx", true));
            //routes.Add("infolib.viewitem", new AuthorizeRoute("infolib.viewitem", "infolib/viewitem/{ParentId}", "~/InfoLib/InfoLibItems.aspx", true));
            routes.Add("infolib.viewitemstoplevel", new AuthorizeRoute("infolib.viewitemstoplevel", "infolib/viewitemstoplevel", "~/InfoLib/InfoLibItemsTopLevel.aspx", true));
            routes.Add("infolib.addinfolibitem", new AuthorizeRoute("infolib.addinfolibitem", "infolib/addinfolibitem/{ParentId}", "~/InfoLib/AddInfoLibItem.aspx", true));
            routes.Add("infolib.editinfolibitem", new AuthorizeRoute("infolib.editinfolibitem", "infolib/editinfolibitem/{ParentId}/{InfoLibItemId}", "~/InfoLib/EditInfoLibItem.aspx", true));
            routes.Add("infolib.delinfolibitem", new AuthorizeRoute("infolib.delinfolibitem", "infolib/delinfolibitem/{ParentId}/{InfoLibItemId}", "~/InfoLib/DeleteInfoLibItem.aspx", true));
            //routes.Add("infolib.addinfolibitem", new AuthorizeRoute("infolib.addinfolibitem", "infolib/addinfolibitem", "~/InfoLib/AddInfoLibItem.aspx", true));
            
            //#endif        
        }
    }
}