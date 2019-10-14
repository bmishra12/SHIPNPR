using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Web.UI.WebControls;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkWeb.Routing;

namespace ShiptalkWeb.Npr.Forms
{
    public partial class CCFSearch : Page, IRouteDataPage, IAuthorize
    {
        private const string ActiveAgenciesKey = "ActiveAgencies";
        private const string ActiveAgencyDescriptorsKey = "ActiveAgencyDescriptors";
        private const string CcfSearchTypeKey = "CcfSearchType";
        private const string DefaultStateKey = "DefaultState";
        private const string IsAdminKey = "IsAdmin";
        private const string ScopeKey = "Scope";
        private const string UserIdKey = "UserId";
        //private const string IsCounselorOnlyKey = "IsCounselorOnly";
        //private const string IsSubmitterOnlyKey = "IsSubmitterOnly";
        //private const string IsReviewerOnlyKey = "IsReviewerOnly";

        protected string AGENCY_DROPDOWN_DEFAULT_TEXT = "-- All of my agencies --";

        private AgencyBLL _agencyLogic;
        private CCFBLL _logic;

        #region Properties

        /// <summary>
        /// Gets or sets the active agencies id's for the current user.
        /// </summary>
        //protected IList<int> ActiveAgencies { get { return (IList<int>)ViewState[ActiveAgenciesKey]; } set { ViewState[ActiveAgenciesKey] = value; } }

        /// <summary>
        /// Gets or sets the active agency ids and their descriptors for the current user.
        /// </summary>
        //protected IDictionary<int, IList<int>> ActiveAgencyDescriptors { get { return (IDictionary<int, IList<int>>)ViewState[ActiveAgencyDescriptorsKey]; } set { ViewState[ActiveAgencyDescriptorsKey] = value; } }

        protected SearchType CcfSearchType { get { return (SearchType)ViewState[CcfSearchTypeKey]; } set { ViewState[CcfSearchTypeKey] = value; } }

        protected State DefaultState { get { return new State(((IRouteDataPage)this).AccountInfo.StateFIPS); } }

        protected bool IsCMSAdmin { get { return this.AccountInfo.IsAdmin && this.AccountInfo.IsCMSScope; } }

        protected bool ISCMSLevel { get { return this.AccountInfo.IsCMSScope; } }

        //protected bool IsCounselorOnly { get { return (ViewState[IsCounselorOnlyKey] != null) ? (bool)ViewState[IsCounselorOnlyKey] : false; } set { ViewState[IsCounselorOnlyKey] = value; } }

        //protected bool IsSubmitterOnly { get { return (ViewState[IsSubmitterOnlyKey] != null) ? (bool)ViewState[IsSubmitterOnlyKey] : false; } set { ViewState[IsSubmitterOnlyKey] = value; } }

        //protected bool IsReviewerOnly { get { return (ViewState[IsReviewerOnlyKey] != null) ? (bool)ViewState[IsReviewerOnlyKey] : false; } set { ViewState[IsReviewerOnlyKey] = value; } }

        protected Scope Scope { get { return this.AccountInfo.Scope; } }

        protected int UserId { get { return this.AccountInfo.UserId; }  }

        protected AgencyBLL AgencyLogic
        {
            get
            {
                if (_agencyLogic == null) _agencyLogic = new AgencyBLL();

                return _agencyLogic;
            }
        }

        protected CCFBLL Logic
        {
            get
            {
                if (_logic == null) _logic = new CCFBLL();

                return _logic;
            }
        }

        #region Search Form Dependencies
        protected IEnumerable<KeyValuePair<string, string>> States { get; set; }

        #endregion

        #region Search Form Properties

        protected int? CounselorId
        {
            get
            {
                return (Convert.ToInt32(dropDownlistCounselor.SelectedValue) > 0) ? (int?)Convert.ToInt32(dropDownlistCounselor.SelectedValue) : null;
            }
        }

        protected DateTime? FromContactDate
        {
            get
            {
                return (!string.IsNullOrEmpty(textBoxFromDateOfContact.Text)) ? (DateTime?)DateTime.Parse(textBoxFromDateOfContact.Text) : null;
            }
        }

        protected string SearchKeywords
        {
            get
            {
                return textBoxSearchKeywords.Text.Replace("\"", string.Empty).Replace(",", string.Empty);
            }
        }

        protected State State
        {
            get
            {
                return new State(dropDownListState.SelectedValue);
            }
        }

        protected int? SubmitterId
        {
            get
            {
                return (Convert.ToInt32(dropDownlistSubmitter.SelectedValue) > 0) ? (int?)Convert.ToInt32(dropDownlistSubmitter.SelectedValue) : null;
            }
        }

        protected DateTime? ToContactDate
        {
            get
            {
                return (!string.IsNullOrEmpty(textBoxToDateOfContact.Text)) ? (DateTime?)DateTime.Parse(textBoxToDateOfContact.Text) : null;
            }
        }

        protected int? SelectedAgencyId
        {
            get
            {
                return dropDownListAgency.SelectedIndex != 0 ? Convert.ToInt32(dropDownListAgency.SelectedValue) : (int?)null;
            }
        }
        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Binds the dependent data for the form.
        /// </summary>
        protected void BindDependentData()
        {

            #region drop down population logic.
            //switch (Scope)
            //{
            //    case Scope.CMS:
            //        {
            //            Counselors = Logic.GetClientContactCounselors();
            //            Submitters = Logic.GetClientContactSubmitters();

            //            break;
            //        }
            //    case Scope.State:
            //        {

            //            Counselors = Logic.GetClientContactCounselors(DefaultState);
            //            Submitters = Logic.GetClientContactSubmitters(DefaultState);

            //            break;
            //        }
            //    default :
            //        {
            //            //Determine if the current user is a super data editor.
            //            if (AgencyLogic.IsSuperDataEditor(UserId))
            //            {
            //                Counselors = Logic.GetCounselorsForSuperDataEditor(UserId);
            //                Submitters = Logic.GetSubmittersForSuperDataEditor(UserId);
            //            }
            //            else
            //            {
            //                //Set the Descriptors needed to determine form population and options.
            //                var isCounselor = IsDescriptor(Descriptor.Counselor);
            //                var isSubmitter = IsDescriptor(Descriptor.DataSubmitter);
            //                var isReviewer = IsDescriptor(Descriptor.DataEditor_Reviewer);

            //                if (isCounselor && isSubmitter)
            //                {
            //                    IsCounselorOnly = false;
            //                    IsSubmitterOnly = false;

            //                    //Get the Counselors for where the current user is a DataSubmitter.
            //                    Counselors = Logic.GetCounselors(Scope, (from id in GetAgencies(Descriptor.DataSubmitter) select id).ToList());

            //                    //Check if the current user is in the counselors list; otherwise add them manually.
            //                    if (
            //                        (from counselor in Counselors where counselor.Key == UserId select counselor.Key).Count() == 0)
            //                    {
            //                        var profile = UserBLL.GetUserProfile(UserId);
            //                        Counselors = new List<KeyValuePair<int, string>>
            //                                         {
            //                                             (new KeyValuePair<int, string>(UserId,
            //                                                 string.Format("{0} {1}", profile.FirstName, profile.LastName)))
            //                                         };
            //                    }

            //                    //Get the Submitters for where the current user is a Counselor.
            //                    Submitters = Logic.GetSubmitters(Scope, (from id in GetAgencies(Descriptor.Counselor) select id).ToList());

            //                    //Check if the current user is in the submitter list; otherwise add them manually.
            //                    if ((from submitter in Submitters where submitter.Key == UserId select submitter.Key).Count() == 0)
            //                    {
            //                        var profile = UserBLL.GetUserProfile(UserId);
            //                        Submitters = new List<KeyValuePair<int, string>>
            //                                         {
            //                                             (new KeyValuePair<int, string>(UserId,
            //                                                 string.Format("{0} {1}", profile.FirstName, profile.LastName)))
            //                                         };
            //                    }
            //                }
            //                else if (isCounselor)
            //                {
            //                    //check if the current user is a reviewer too.
            //                    if (isReviewer)
            //                    {
            //                        //Get submitters from agencies in which the current user is a counselor.
            //                        //No Counselors are to be retrieved.
            //                        Counselors = null;
            //                        //Get the submitters for whom the current user is a counselor.
            //                        Submitters = Logic.GetSubmitters(Scope, (from id in GetAgencies(Descriptor.Counselor) select id).ToList());
            //                    }
            //                    else
            //                    {
            //                        IsCounselorOnly = true;
            //                        //Get the Counselors for where the current user is a DataSubmitter.
            //                        Counselors = Logic.GetCounselors(Scope, (from id in GetAgencies(Descriptor.DataSubmitter) select id).ToList());

            //                        //Check if the current user is in the counselors list; otherwise add them manually.
            //                        if (
            //                            (from counselor in Counselors where counselor.Key == UserId select counselor.Key).Count() == 0)
            //                        {
            //                            var profile = UserBLL.GetUserProfile(UserId);
            //                            Counselors = new List<KeyValuePair<int, string>>
            //                                             {
            //                                                 (new KeyValuePair<int, string>(UserId,
            //                                                     string.Format("{0} {1}", profile.FirstName, profile.LastName)))
            //                                             };
            //                        }

            //                        //Get the Submitters for the current user's agencies.
            //                        Submitters = Logic.GetSubmitters(Scope, (from id in ActiveAgencyDescriptors.Keys select id).ToList());
            //                    }
            //                }
            //                else if (isSubmitter)
            //                {
            //                    //check if the current user is a reviewer too.
            //                    if (isReviewer)
            //                    {
            //                        //Get the counselors for whom the curent user is a submitter
            //                        Counselors = Logic.GetCounselors(Scope, (from id in GetAgencies(Descriptor.DataSubmitter) select id).ToList());
            //                        //Get the submitters for whom the current user is a supervisor.
            //                        Submitters = Logic.GetReviewerSubmitters(Scope, (from id in GetAgencies(Descriptor.DataEditor_Reviewer) select id).ToList(), UserId);
            //                    }
            //                    else
            //                    {
            //                        IsSubmitterOnly = true;
            //                        //Get the Counselors for the current user's agencies.
            //                        Counselors = Logic.GetCounselors(Scope, (from id in ActiveAgencyDescriptors.Keys select id).ToList());
            //                        //Get the Submitters for where the current user is a Counselor.
            //                        Submitters = Logic.GetSubmitters(Scope, (from id in GetAgencies(Descriptor.Counselor) select id).ToList());

            //                        //Check if the current user is in the submitter list; otherwise add them manually.
            //                        if (
            //                            (from submitter in Submitters where submitter.Key == UserId select submitter.Key).Count() == 0)
            //                        {
            //                            var profile = UserBLL.GetUserProfile(UserId);
            //                            Submitters = new List<KeyValuePair<int, string>>
            //                                             {
            //                                                 (new KeyValuePair<int, string>(UserId, string.Format("{0} {1}", profile.FirstName, profile.LastName)))
            //                                             };
            //                        }
            //                    }
            //                }
            //                else if (isReviewer) //not a counselor or submitter.
            //                {
            //                    IsReviewerOnly = true;
            //                    //No Counselors are to be retrieved.
            //                    Counselors = null;
            //                    //Get the submitters for whom the current user is a supervisor.
            //                    Submitters = Logic.GetReviewerSubmitters(Scope, (from id in GetAgencies(Descriptor.DataEditor_Reviewer) select id).ToList(), UserId);
            //                }
            //            }

            //            break;
            //        }
            //}

            #endregion

            //PopulateCounselorDropDown();
            //PopulateSubmitterDropDown();


            //if user is Cms Regional user populate only his states
            if (AccountInfo.Scope == Scope.CMSRegional)
            {


                IEnumerable<KeyValuePair<string, string>> StatesData = AgencyLogic.GetStates();

                IEnumerable<KeyValuePair<string, string>> StatesForUser = null;

                List<string> StateFIPSForCMSRegions = new List<string>();
                IEnumerable<UserRegionalAccessProfile> profiles = UserCMSBLL.GetUserCMSRegionalProfiles(AccountInfo.UserId, false);
                foreach (UserRegionalAccessProfile profile in profiles)
                {
                    IEnumerable<string> CMSStateFIPS = LookupBLL.GetStatesForCMSRegion(profile.RegionId);
                    if (CMSStateFIPS != null)
                        StateFIPSForCMSRegions.AddRange(CMSStateFIPS);
                }
                if (StateFIPSForCMSRegions.Count > 0)
                {
                    StatesForUser = (
                        from stFIPS in StatesData
                        from cmsStFIPS in StateFIPSForCMSRegions
                        where stFIPS.Key == State.GetState(cmsStFIPS).Key
                        select stFIPS
                        );
                }
                States = StatesForUser;

            }
            else
            {
                States = AgencyLogic.GetStates();
            }
            
            
            aAddCCF.DataBind();


            //check the correct authorization
            if (AccountInfo.IsStateAdmin
                    || (AccountInfo.IsCMSScope && AccountInfo.IsAdmin)
                || AccountInfo.IsShipDirector)
            {
                aAddSpecialField.Visible = true;
                aAddSpecialField.DataBind();
            }

            //9/11/2013- commented the below line-  Bimal noticed that the NPR code is searching for all a user’s recent records the moment that the CC tab is clicked.
            //Since many users may not actually need a prior client search (i.e. they may click the “Add a new client never seen here before “link), Bimal will move the search from a default to a specific request if the user makes any of the three search selections (by agency counselor submitter).
            //It appears that the database has reached a size that even with indexing the default background search is consuming too many system resources and risks not only the spinning and slow load but also a timeout and error page as well.


            panelSearchCriteria.DataBind();
            if(Scope.IsLowerOrEqualTo(Scope.State)) BindStateDependentData(new State(dropDownListState.SelectedValue));
        }

        // <summary>
        /// Called when page is initialized for the first time.
        /// </summary>
        protected void OnViewInitialized()
        {
           CcfSearchType = SearchType.Init;
            //DefaultState = new State(((IRouteDataPage)this).AccountInfo.StateFIPS);
            //IsCMSAdmin = this.AccountInfo.IsAdmin && this.AccountInfo.IsCMSScope;
            //ActiveAgencies = AgencyLogic.GetActiveAgencies(UserId);
            //ActiveAgencyDescriptors = AgencyLogic.GetActiveAgencyDescriptors(UserId);

          
            BindDependentData();
        }

        /// <summary>
        /// Called when the page is loaded.
        /// </summary>
        protected void OnViewLoaded()
        {
        }

        private void PopulateCounselorDropDown()
        {
            dropDownlistCounselor.DataSource = Counselors;
            dropDownlistCounselor.DataTextField = "Value";
            dropDownlistCounselor.DataValueField = "Key";
            dropDownlistCounselor.DataBind();
            dropDownlistCounselor.Items.Insert(0, new ListItem("-- Select a Counselor --", "0"));
        }

        private void PopulateSubmitterDropDown()
        {
            dropDownlistSubmitter.DataSource = Submitters;
            dropDownlistSubmitter.DataTextField = "Value";
            dropDownlistSubmitter.DataValueField = "Key";
            dropDownlistSubmitter.DataBind();
            dropDownlistSubmitter.Items.Insert(0, new ListItem("-- Select a Data Submitter --", "0"));
        }


        //protected bool IsDescriptor(Descriptor type)
        //{
        //    if (ActiveAgencyDescriptors == null) return false;

        //    foreach (var activeAgencyDescriptor in ActiveAgencyDescriptors)
        //    {
        //        foreach (var descriptor in activeAgencyDescriptor.Value)
        //        {
        //            if (descriptor != 0 && (Descriptor)descriptor == type)
        //                return true;
        //        }
        //    }

        //    return false;
        //}

        //protected IList<int> GetAgencies(Descriptor type)
        //{
        //    var agencies = new List<int>();

        //    if (ActiveAgencyDescriptors == null) return agencies;

        //    foreach (var activeAgencyDescriptor in ActiveAgencyDescriptors)
        //    {
        //        foreach (var descriptor in activeAgencyDescriptor.Value)
        //        {
        //            if (descriptor != 0 && (Descriptor)descriptor == type)
        //                agencies.Add(activeAgencyDescriptor.Key);
        //        }
        //    }

        //    return agencies;
        //}

        private IEnumerable<KeyValuePair<int, string>> GetCounselors(int? AgencyId)
        {
            //Populate Counselors from DB
            CCFBLL.CCCounselorsFilterCriteria criteria = new CCFBLL.CCCounselorsFilterCriteria();
            criteria.StateFIPS = AccountInfo.IsCMSLevel ? State.GetCode(dropDownListState.SelectedValue) : AccountInfo.StateFIPS;
            criteria.scope = this.AccountInfo.Scope;
            criteria.UserId = this.AccountInfo.UserId;
            criteria.AgencyId = AgencyId;
            return Logic.GetClientContactCounselors(criteria);
        }

        private IEnumerable<KeyValuePair<int, string>> GetSubmitters(int? AgencyId)
        {
            //Populate Submitters from DB
            CCFBLL.CCSubmittersFilterCriteria criteria = new CCFBLL.CCSubmittersFilterCriteria();
            criteria.StateFIPS = AccountInfo.IsCMSLevel ? State.GetCode(dropDownListState.SelectedValue) : AccountInfo.StateFIPS;
            criteria.scope = this.AccountInfo.Scope;
            criteria.UserId = this.AccountInfo.UserId;
           criteria.AgencyId = AgencyId;
            return Logic.GetClientContactSubmitters(criteria);
        }

        private IEnumerable<KeyValuePair<int, string>> _Counselors { get; set; }
        protected IEnumerable<KeyValuePair<int, string>> Counselors
        {
            get
            {
                if (_Counselors == null)
                {
                    int? AgencyId = null;
                    if (IsPostBack && dropDownListAgency.SelectedIndex != 0)
                        AgencyId =  Convert.ToInt32(dropDownListAgency.SelectedValue);

                    _Counselors = GetCounselors(AgencyId);
                }

                return _Counselors;
            }
            set
            {
                _Counselors = value;
            }
        }

        private IEnumerable<KeyValuePair<int, string>> _Submitters { get; set; }
        protected IEnumerable<KeyValuePair<int, string>> Submitters
        {
            get
            {
                if (_Submitters == null)
                {
                    int? AgencyId = null;
                    if (IsPostBack && dropDownListAgency.SelectedIndex != 0)
                        AgencyId = Convert.ToInt32(dropDownListAgency.SelectedValue);

                    _Submitters = GetSubmitters(AgencyId);
                }

                return _Submitters;
            }
            set
            {
                _Submitters = value;
            }
        }
        //public bool IsSingleAgencyUser(out int? AgencyId)
        //{

        //    if (AccountInfo.Scope.IsEqual(Scope.Agency))
        //    {
        //        var profiles = UserAgencyBLL.GetUserAgencyProfiles(AccountInfo.UserId, false);
        //        if (profiles != null && profiles.Count() == 1)
        //        {
        //            AgencyId = profiles.First().RegionId;
        //            return true;
        //        }
        //    }
        //    AgencyId = null;
        //    return false;
        //}

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) OnViewInitialized();

            OnViewLoaded();
        }

        protected void dropDownlistCounselor_DataBound(object sender, EventArgs e)
        {
            //if (!IsCounselorOnly) return;

            //var item = dropDownlistCounselor.Items.FindByValue(UserId.ToString());

            //if (item != null)
            //    dropDownlistCounselor.SelectedValue = UserId.ToString();

            //dropDownlistCounselor.Enabled = false;
        }

        protected void dropDownlistSubmitter_DataBound(object sender, EventArgs e)
        {
            //if (!IsSubmitterOnly) return;

            //var item = dropDownlistSubmitter.Items.FindByValue(UserId.ToString());

            //if (item != null)
            //    dropDownlistSubmitter.SelectedValue = UserId.ToString();

            //dropDownlistSubmitter.Enabled = false;
        }

        protected void linkButtonListRecentClientContacts_Click(object sender, EventArgs e)
        {
            CcfSearchType = SearchType.Recent;
            listViewSearchClientContacts.DataBind();
        }

        protected void buttonSearch_Click(object sender, EventArgs e)
        {
            CcfSearchType = SearchType.Keyword;
            pager.SetPageProperties(0, pager.PageSize, false);

            listViewSearchClientContacts.DataBind();
        }

        protected void dataSourceSearchClientContacts_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            IList<SearchClientContactsViewData> clientContacts = null;

            switch (CcfSearchType)
            {
                case SearchType.Keyword:
                    {
                        //if (AccountInfo.IsStateLevel)
                        //{
                            //clientContacts = new List<SearchClientContactsViewData>(Logic.SearchClientContacts(SearchKeywords, FromContactDate, ToContactDate, State, (IsCounselorOnly) ? UserId : CounselorId, SubmitterId, (int)Scope, UserId));
                            clientContacts = new List<SearchClientContactsViewData>( Logic.SearchClientContacts 
                                                                                    (
                                                                                    new CCFBLL.CCSearchCriteria 
                                                                                        {   UserId = AccountInfo.UserId,
                                                                                            scope = AccountInfo.Scope,
                                                                                            AgencyId=SelectedAgencyId,
                                                                                            Keyword = SearchKeywords,
                                                                                            StateFIPS = State.Code,
                                                                                            CounselorId = CounselorId,
                                                                                            SubmitterId = SubmitterId,
                                                                                            FromDate = FromContactDate,
                                                                                            ToDate = ToContactDate,
                                                                                        }
                                                                                   )
                                                                            );
                        //}
                        dataSourceSearchClientContacts.DataSource = clientContacts;

                        break;
                    }
                case SearchType.Recent:
                    {
                        clientContacts = new List<SearchClientContactsViewData>(Logic.GetRecentClientContacts(UserId));
                        dataSourceSearchClientContacts.DataSource = clientContacts;

                        break;
                    }
                case SearchType.Init:
                    {
                       clientContacts = new List<SearchClientContactsViewData>();
                        dataSourceSearchClientContacts.DataSource = clientContacts;

                        break;
                    }
            }

            panelNoResults.Visible = (clientContacts.Count == 0);
        }

        #endregion

        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion

        #region Sub Types

        protected enum SearchType
        {
            Keyword,
            Recent,
            Init
        }

        #endregion


        protected void dropDownListState_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Update the agency list based on the users scope and clear the counselors and submitters lists.
            BindStateDependentData(new State(dropDownListState.SelectedValue));
        }

        protected void dropDownListAgency_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAgencyDependatData(Convert.ToInt32(dropDownListAgency.SelectedValue));
        }

        protected void BindStateDependentData(State state)
        {
            dropDownListAgency.Items.Clear();
            dropDownListAgency.DataSource = new List<KeyValuePair<int, string>>(UserBLL.GetAllAgenciesForUser(UserId, Scope, state.Code));
            dropDownListAgency.DataBind();
            dropDownListAgency.Items.Insert(0, new ListItem(AGENCY_DROPDOWN_DEFAULT_TEXT, "0"));

            dropDownListAgency.Enabled = true;
            
            BindAgencyDependatData(null);
        }

        protected void BindAgencyDependatData(int? agencyId)
        {
            dropDownlistCounselor.Items.Clear();
            dropDownlistSubmitter.Items.Clear();
            PopulateCounselorDropDown();
            PopulateSubmitterDropDown();
            dropDownlistCounselor.Enabled = true;
            dropDownlistSubmitter.Enabled = true;
        }

        #region IAuthorize Members

        bool IAuthorize.IsAuthorized()
        {
            //TODO: Need to make sure User is Counselor, Data Submitter, IsSuperEditor, ShipDirector, CMS Admin, Reviewer
            UserViewData UserData = UserBLL.GetUser(AccountInfo.UserId);
            if (UserData.IsStateLevel)
            {
                int CounselorDescriptorId = Descriptor.Counselor.EnumValue<int>();
                int SubmitterDescriptorId = Descriptor.DataSubmitter.EnumValue<int>();
                int EditorDescriptorId = Descriptor.DataEditor_Reviewer.EnumValue<int>();

                //State Users need to be either ShipDirector, StateSuperEditor or have one of the Descriptors.
                if (UserData.IsUserStateScope)
                {
                    //Check ShipDirector or StateSuperEditor
                    if (UserData.IsShipDirector || UserData.IsStateSuperDataEditor) return true;

                    //Check Descriptors
                    var DescriptorIdsForStateUser = UserData.DescriptorIds;
                    return DescriptorIdsForStateUser.Contains(CounselorDescriptorId) || DescriptorIdsForStateUser.Contains(SubmitterDescriptorId)
                            || DescriptorIdsForStateUser.Contains(EditorDescriptorId);
                }
                else if (UserData.IsUserSubStateRegionalScope || UserData.IsUserAgencyScope)
                {
                    var superEditors = UserData.RegionalProfiles.Where(sup => sup.IsSuperDataEditor == true);
                    if (superEditors != null && superEditors.Count() > 0)
                        return true;

                    var DescriptorIdsForStateUser = UserData.RegionalProfiles.Where(prof => prof.DescriptorIDList != null && (
                                                            prof.DescriptorIDList.Contains(CounselorDescriptorId)
                                                            || prof.DescriptorIDList.Contains(SubmitterDescriptorId)
                                                            || prof.DescriptorIDList.Contains(EditorDescriptorId)
                                                            )
                                                      );

                    return (DescriptorIdsForStateUser != null && DescriptorIdsForStateUser.Count() > 0);
                }

            }
            else
            {
                return UserData.IsCMSLevel;
            }

            return false;
            

        }

        #endregion
    }
}
