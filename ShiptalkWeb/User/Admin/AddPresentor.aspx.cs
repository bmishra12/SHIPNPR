using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ShiptalkWeb.Routing;
using System.Web.Routing;
using System.Transactions;

using Microsoft.Practices.Web.UI.WebControls;

using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkLogic.BusinessLayer;
using ShiptalkCommon;

using RegistrationObject = ShiptalkLogic.BusinessObjects.UserRegistration;


namespace ShiptalkWeb
{
    public partial class AddPresentor : System.Web.UI.Page, IRouteDataPage, IAuthorize
    {

        private const string PresentorStateViewKey = "PresentorState";
        private const string PresentorAgencyIdViewKey = "PresentorAgencyId";

        private const string PresentorStateRouteKey = "StateFIPS";

        protected State PresentorState { get; set; }
        protected int PresentorAgencyId { get; set; }


        private UserViewData UserViewData { get; set; }

        protected int AddPresentorStatus = 0;

       
        #region Page wired events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsAuthorized())
                ShiptalkException.ThrowSecurityException(string.Format("Access denied. User :{0} cannot create Presenter.", this.AccountInfo.UserId), "You are not authorized to create a Presentor.");

            InitializeView();
        }
        protected void dataSourceUserView_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            dataSourceUserView.DataSource = UserViewData;
        }
        protected void dataSourceUserView_Updating(object sender, ObjectContainerDataSourceUpdatingEventArgs e)
        {
            UserViewData.FirstName = e.NewValues["FirstName"].ToString();
            UserViewData.MiddleName = e.NewValues["MiddleName"].IsNull() ? e.NewValues["MiddleName"].ToString() : string.Empty;
            UserViewData.LastName = e.NewValues["LastName"].ToString();

            UserViewData.NickName = e.NewValues["NickName"].IsNull() ? e.NewValues["NickName"].ToString() : string.Empty;
            UserViewData.PrimaryEmail = e.NewValues["PrimaryEmail"].ToString();
            UserViewData.PrimaryPhone = e.NewValues["PrimaryPhone"].ToString();
        }
        protected void dataSourceUserView_Updated(object sender, ObjectContainerDataSourceStatusEventArgs e)
        {
            if (UserViewData != null)
            {
                if (RegisterUserBLL.DoesUserNameExist(UserViewData.PrimaryEmail))
                {
                    DisplayMessage("The Primary Email address is already registered. Duplicates are not allowed.", true);
                    //formView.ChangeMode(FormViewMode.Edit);
                }
                else
                {
                    IRegisterUser regBLL = RegisterUserBLL.CreateRegistrationProviderObject(CreateRegistrationObject());
                    regBLL.ValidateData();
                    if (regBLL.IsValid)
                    {
                        if (regBLL.Save())
                        {
                            AddPresentorStatus = 1;
                            DisplayMessage("The Presenter has been added successfully. You may add another presenter or close this window.", false);
                            UserViewData = new UserViewData();
                        }
                        else
                        {
                            DisplayMessage("Unable to add presenter. Please contact support if the issue persists. Error: " + regBLL.ErrorMessage, true);
                        }
                    }
                    else
                        DisplayMessage("Validation error occured while adding new User. Error: " + regBLL.ErrorMessage, true);
                }
            }
       

           Page.DataBind();

        }
        #endregion


        private RegistrationObject CreateRegistrationObject()
        {
            //Fill personal profile info here...
            RegistrationObject regObj = new RegistrationObject();
            regObj.FirstName = UserViewData.FirstName;
            regObj.MiddleName = UserViewData.MiddleName;
            regObj.LastName = UserViewData.LastName;
            regObj.NickName = UserViewData.NickName;
            regObj.PrimaryEmail = UserViewData.PrimaryEmail;
            regObj.PrimaryPhone = UserViewData.PrimaryPhone;
            
            
            regObj.Password = GenerateTempPassword();

            regObj.ClearPassword = regObj.Password; //sammit

           // regObj.RoleRequested = LookupBLL.Roles.Where(r => (!r.IsAdmin && r.scope == Scope.State)).First();

            regObj.RoleRequested = LookupBLL.Roles.Where(r => (!r.IsAdmin && r.scope == Scope.Agency)).First();

            regObj.StateFIPS = PresentorState.Code;
            regObj.UserRegionalAccessProfile.RegionId = PresentorAgencyId;

            regObj.UserRegionalAccessProfile.DescriptorIDList.Add(Descriptor.PresentationAndMediaStaff.EnumValue<int>());
            regObj.IsRegistrationRequest = false;
            regObj.RegisteredByUserId = this.AccountInfo.UserId;
            return regObj;
        }


        private string GenerateTempPassword()
        {
            return PasswordUtil.GenerateRandomPassword();
        }


        //private string GetStateFIPSForNewUser()
        //{
        //    string StateFIPS = string.Empty;

        //    if (this.AccountInfo.IsStateLevel)
        //        StateFIPS = this.AccountInfo.StateFIPS;
        //    else if(this.AccountInfo.IsCMSLevel)
        //    {
        //        var ddlStatesObj = formView.FindControl("ddlStates") as DropDownList;
        //        if (ShowStateSelection && (ddlStatesObj.SelectedValue.Trim() != string.Empty))
        //            StateFIPS = ddlStatesObj.SelectedValue.Trim();
        //    }

        //    return StateFIPS;
        //}


        private void InitializeView()
        {
            if (!IsPostBack)
                PopulatePresentorState();
            
            UserViewData = new UserViewData();
        }
        private void DisplayMessage(string message, bool IsError)
        {
            //Set up title area
            plhMessage.Visible = true;
            lblTitleMessage.Text = (IsError ? "Error" : "Success!");
            lblMessage.Text = message;
            lblMessage.CssClass = (IsError ? "required" : "info");

            if (!IsError || !IsPostBack)
            {
                //MainPanel.Visible = false;
                //formView.Visible = false;
            }
        }

        
        private void PopulatePresentorState()
        {
            if (RouteData.Values[PresentorStateRouteKey] == null) 
                throw new ShiptalkException("State is required in the Route data for new Presenter.", false, new ArgumentNullException("RouteData.st"));

            if (this.AccountInfo.IsStateLevel)
                PresentorState = new State(this.AccountInfo.StateFIPS);
            else
            {
                //For CMS Level Users
                PresentorState = new State(RouteData.Values[PresentorStateRouteKey].ToString().Split(',')[0]);
            }

            PresentorAgencyId = Convert.ToInt32(RouteData.Values[PresentorStateRouteKey].ToString().Split(',')[1]);

        }
        private UserViewData _AdminViewData { get; set; }
        private UserViewData AdminViewData
        {
            get
            {
                if (_AdminViewData.IsNull())
                    _AdminViewData = UserBLL.GetUser(this.AccountInfo.UserId);

                return _AdminViewData;
            }
        }

       
        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion


        #region "View state events"
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            PresentorState = (State)ViewState[PresentorStateViewKey];
            PresentorAgencyId = (int)ViewState[PresentorAgencyIdViewKey];

        }

        protected override object SaveViewState()
        {
            ViewState[PresentorStateViewKey] = PresentorState;
            ViewState[PresentorAgencyIdViewKey] = PresentorAgencyId;

            return base.SaveViewState();
        }
        #endregion


        #region IAuthorize Members

        public bool IsAuthorized()
        {
            //For adding a Presentor, the User must be a Counselor or Data Submitter or anything else *BUT* ReadOnly Ship Director, ReadOnly NPR.
            if (AdminViewData.IsCMSLevel)
                return AdminViewData.IsAdmin;
            else
            {
                if (this.AccountInfo.StateFIPS == AdminViewData.StateFIPS)
                {
                    if (AdminViewData.IsShipDirector)
                        return true;
                    else
                    {
                        //To be authorized, Users MUST NOT have the ReadOnly descriptors. Check if they have or not.
                        var readonlyMatches = AdminViewData.GetAllDescriptorsForUser.Where(p => (p.Key.ToEnumObject<Descriptor>() == Descriptor.OtherStaff_NPR || p.Key.ToEnumObject<Descriptor>() == Descriptor.OtherStaff_SHIP));
                        if (readonlyMatches != null && readonlyMatches.Count() > 0)
                        {
                            //If null or the readonlyMatches count = 0, then IsAuthorized = true;
                            return readonlyMatches.First().Value.IsNull();
                        }
                        else
                            return true;
                    }
                }
                else
                    return false;
            }
        }

        #endregion
    }
}
