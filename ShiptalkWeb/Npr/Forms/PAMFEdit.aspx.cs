using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Routing;
using System.Web.UI;
using TextBox = System.Web.UI.WebControls.TextBox;
using System.Web.UI.WebControls;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet;
using Microsoft.Practices.Web.UI.WebControls;
using ShiptalkLogic;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkWeb;
using ShiptalkWeb.Routing;
using ShiptalkWebControls;




namespace NPRRebuild.ShiptalkWeb.PAMF
{
    public partial class PAMFEdit : Page, IRouteDataPage, IPostBackEventHandler, IAuthorize  
        
        
    {

        private const string IdKey = "Id";

        private const string AgencyIdKey = "AgencyId";
        private const string AgencyCodeKey = "AgencyCode";
        private const string AgencyNameKey = "AgencyName"; 

        private const string AgencyStateKey = "AgencyState";
        private const string DefaultStateKey = "DefaultState";
        private const string SelectedStateKey = "SelectedState";

        private const string DefaultEventStateKey = "DefaultEventState";

        private const string IsAdminKey = "IsAdmin";
        private const string ScopeKey = "Scope";
        private const string AutoAssignedClientIdKey = "AutoAssignedClientId";

        private const string ListPresenters = "ListPresenters";

        private const string ContactPhone = "ContactPhone";
        private const string EventZIPCode = "EventZIPCode";
        private const string ReviewerUserIdKey = "ReviewerUserID";


        #region Properties
        private AgencyBLL _agencyLogic;

        public AgencyBLL AgencyLogic
        {
            get
            {
                if (_agencyLogic == null) _agencyLogic = new AgencyBLL();

                return _agencyLogic;
            }
        }

        private PamBLL _pamLogic;


        public PamBLL Logic
        {
            get
            {
                if (_pamLogic == null) _pamLogic = new PamBLL();

                return _pamLogic;
            }
        }

        private EditPublicMediaEventViewData  _viewData;

        private EditPublicMediaEventViewData ViewData
        {
            get
            {
                if (_viewData == null)
                    _viewData = Logic.GetEditPAM(Id.GetValueOrDefault(0));

                return _viewData;
            }
        }


        public int? Id
        {
            get
            {
                if (RouteData.Values[IdKey] == null) return null;

                int id;

                if (int.TryParse(RouteData.Values[IdKey].ToString(), out id))
                    return id;

                return null;
            }
        }

        public int? ReviewerUserId { get { return (ViewState[ReviewerUserIdKey] != null) ? (int?)Convert.ToInt32(ViewState[ReviewerUserIdKey]) : null; } set { ViewState[ReviewerUserIdKey] = value; } }

        public IEnumerable<SpecialField> CMSSpecialFields { get; set; }
        public IEnumerable<SpecialField> StateSpecialFields { get; set; }

        public int AgencyId { get { return Convert.ToInt32(ViewState[AgencyIdKey]); } set { ViewState[AgencyIdKey] = value; } }

        public string AgencyCode { get { return ViewState[AgencyCodeKey].ToString(); } set { ViewState[AgencyCodeKey] = value; } }

        public string AgencyName { get { return ViewState[AgencyNameKey].ToString(); } set { ViewState[AgencyNameKey] = value; } }

        public State AgencyState { get { return (State)ViewState[AgencyStateKey]; } set { ViewState[AgencyStateKey] = value; } }

        public string AutoAssignedClientId { get { return ViewState[AutoAssignedClientIdKey].ToString(); } set { ViewState[AutoAssignedClientIdKey] = value; } }

        public State DefaultState { get { return (State)ViewState[DefaultStateKey]; } set { ViewState[DefaultStateKey] = value; } }


        public bool IsAdmin { get { return (bool)ViewState[IsAdminKey]; } set { ViewState[IsAdminKey] = value; } }

       // public UserAccount Counselor { get; set; }

        public Scope Scope { get { return (Scope)ViewState[ScopeKey]; } set { ViewState[ScopeKey] = value; } }

        public IEnumerable<KeyValuePair<int, string>> Presenters { get; set; }


        public IEnumerable<KeyValuePair<int, string>> PamFocus { get { return Logic.GetPamTopics(); } }
        public IEnumerable<KeyValuePair<int, string>> PamAudiences { get { return Logic.GetPamTargetAudience(); } }


        public IList<PamPresentersType> _listShipUsr;

        public IList<PamPresentersType> LISTPRESENTERS
        { 
            get { return (IList<PamPresentersType>)ViewState[ListPresenters]; } 
            
            set { ViewState[ListPresenters] = value; } 
        }

        public IEnumerable<KeyValuePair<string, string>> EventCounties { get; set; }
        public IEnumerable<KeyValuePair<string, string>> EventStates { get; set; }
        public State DefaultEventState { get { return (State)ViewState[DefaultEventStateKey]; } set { ViewState[DefaultEventStateKey] = value; } }
        

        #endregion

        #region Methods

        protected void BindFormData(int intAgencyId)
        {
            //populate the viewstate
           AgencyId = intAgencyId;

            if (AgencyId > 0)
            {
                var agencyIdentifiers = AgencyLogic.GetAgencyIdentifiers(AgencyId);
                AgencyCode = agencyIdentifiers.Code;
                AgencyState = agencyIdentifiers.State;
                AgencyName = agencyIdentifiers.Name;

                ////OLD logic
                ////Presenters = LookupBLL.GetPresentorsForState(AgencyState.Code,false);
                Presenters = GetPresenters(AgencyId);

 

                CMSSpecialFields = Logic.GetCMSSpecialFields();
                StateSpecialFields = Logic.GetStateSpecialFields(agencyIdentifiers.State);
                panelForm.Visible = true;
                bindGridWithAtleastFive();


                DefaultEventState = (State)ViewData.EventState;
                EventStates = AgencyLogic.GetStates();
                EventCounties = AgencyLogic.GetCounties(((State)ViewData.EventState).Code);

                /////panelForm.DataBind();

            }
            else
            {
               //throw error;
            }

        }


        private IEnumerable<KeyValuePair<int, string>> GetPresenters(int AgencyId)
        {
            //Populate Counselors from DB
            PamBLL.PamPresenterFilterCriteria criteria = new PamBLL.PamPresenterFilterCriteria();
            criteria.StateFIPS = AgencyState.Code;
            criteria.scope = this.AccountInfo.Scope;
            criteria.UserId = this.AccountInfo.UserId;
            criteria.AgencyId = AgencyId;
            return Logic.GetPamPresentersForPamForm(criteria, false);
        }

        private void bindGridWithAtleastFive()
        {
            int noOfPresentersToDraw = 5;
            int countOfPresenters = ViewData.PamPresenters.Count;
            if (countOfPresenters > 5) noOfPresentersToDraw = countOfPresenters;

            _listShipUsr = new List<PamPresentersType>();

            for (int i = 0; i < noOfPresentersToDraw; i++)
            {
                PamPresentersType Line = new PamPresentersType();
                Line.PAMShipUsers = Presenters;
                Line.HoursSpent = null;
                Line.Affiliation = "";

                _listShipUsr.Add(Line);

            }


            //assign to the viewstate
            LISTPRESENTERS = _listShipUsr;


        }

        /// <summary>
        /// Called when page is initialized for the first time.
        /// </summary>
        protected void OnViewInitialized()
        {

            DefaultState = new State(AccountInfo.StateFIPS);
            IsAdmin = AccountInfo.IsAdmin;
            Scope = (Scope)AccountInfo.ScopeId;

        }


        private void AddPresenterGridRow( bool addNewRow)
        {
            //clean up the grid validation error if any..
            Label ctrlName = (Label)formViewPAM.FindControl("lblgridValidation");
            ctrlName.Text = string.Empty;

            ////OLD logic
            ////Presenters = LookupBLL.GetPresentorsForState(AgencyState.Code,false);
            Presenters = GetPresenters(AgencyId);


            if (addNewRow)
            {
                PamPresentersType _newRow = new PamPresentersType();
                _newRow.PAMShipUsers = Presenters;
                _newRow.HoursSpent = null;
                _newRow.Affiliation = "";

                if (LISTPRESENTERS != null)
                    LISTPRESENTERS.Add(_newRow);
            }

            //if the addNewRow = false that means a pampresenter is added..
            // we are refreshing the grid with new dropdown.
            // refresh the dropdown..
            if (addNewRow == false)
            {
                foreach (PamPresentersType _aListPresnter in LISTPRESENTERS)
                {
                    _aListPresnter.PAMShipUsers = Presenters;
                }
            }


            //get the state of datagrid
            IList<PamPresenters> listShipUsr = userStatePresenterList();

            GridView ctrlGrid = (GridView)formViewPAM.FindControl("grdPamPresenters");
            ///////ctrlGrid.DataSource = _listShipUsr;



            ctrlGrid.DataSource = LISTPRESENTERS;
            ctrlGrid.DataBind();


            //formViewPAM.DataBind();  //this bind does not reflect the user choices  for the grid in the code below hence, I am binding the grid directly.

            int gridRowCount = addNewRow ? ctrlGrid.Rows.Count - 1 : ctrlGrid.Rows.Count;
            //adjust the userState if any..
            for (int i = 0; i < gridRowCount; i++)
            {
                System.Web.UI.WebControls.DropDownList ctrldp = (System.Web.UI.WebControls.DropDownList)ctrlGrid.Rows[i].Cells[0].Controls[1];
                ((System.Web.UI.WebControls.ListControl)(ctrldp)).SelectedValue = listShipUsr[i].PAMUserId;

                ((System.Web.UI.WebControls.TextBox)ctrlGrid.Rows[i].Cells[1].Controls[1]).Text = listShipUsr[i].Affiliation;
                ((System.Web.UI.WebControls.TextBox)ctrlGrid.Rows[i].Cells[2].Controls[1]).Text = listShipUsr[i].HoursSpent.ToString();

            }
        }


        IList<PamPresenters> userStatePresenterList()
        {
            IList<PamPresenters> listShipUsr = new List<PamPresenters>();

            GridView ctrlGrid = (GridView)formViewPAM.FindControl("grdPamPresenters");

            for (int i = 0; i < ctrlGrid.Rows.Count; i++)
            {
                PamPresenters _myone = new PamPresenters();

                System.Web.UI.WebControls.DropDownList ctrldp = (System.Web.UI.WebControls.DropDownList)ctrlGrid.Rows[i].Cells[0].Controls[1];
                _myone.PAMUserId = ((System.Web.UI.WebControls.ListControl)(ctrldp)).SelectedValue;
                _myone.Affiliation = ((System.Web.UI.WebControls.TextBox)ctrlGrid.Rows[i].Cells[1].Controls[1]).Text;

                decimal _hoursSpent;
                string _hrsString = ((System.Web.UI.WebControls.TextBox)ctrlGrid.Rows[i].Cells[2].Controls[1]).Text;
                if (_hrsString == string.Empty)
                    _myone.HoursSpent = null;
                else
                    _myone.HoursSpent = (decimal.TryParse(_hrsString, out _hoursSpent)) ? _hoursSpent : 0;


                listShipUsr.Add(_myone);
            }

            return listShipUsr;
        }

        string ValidateSections()
        {
            string _validationError = string.Empty;

            //section 1
            TextBox ctrltxtNoOfIPAttendees = (TextBox)formViewPAM.FindControl("txtNoOfIPAttendees");
            TextBox ctrltxtIPEnrollmentAssistance = (TextBox)formViewPAM.FindControl("txtIPEnrollmentAssistance");

            //section 2
            TextBox ctrltxtNoOfBoothAttendees = (TextBox)formViewPAM.FindControl("txtNoOfBoothAttendees");
            TextBox ctrltxtBoothEnrollmentAssistance = (TextBox)formViewPAM.FindControl("txtBoothEnrollmentAssistance");

            //section 3
            TextBox ctrltxtDedicatedEnrollmentEvent1 = (TextBox)formViewPAM.FindControl("txtDedicatedEnrollmentEvent1");
            TextBox ctrltxtDedicatedEnrollmentEvent2 = (TextBox)formViewPAM.FindControl("txtDedicatedEnrollmentEvent2");
            TextBox ctrltxtDedicatedEnrollmentEvent3 = (TextBox)formViewPAM.FindControl("txtDedicatedEnrollmentEvent3");
            TextBox ctrltxtDedicatedEnrollmentEvent4 = (TextBox)formViewPAM.FindControl("txtDedicatedEnrollmentEvent4");
            TextBox ctrltxtDedicatedEnrollmentEvent5 = (TextBox)formViewPAM.FindControl("txtDedicatedEnrollmentEvent5");
            TextBox ctrltxtDedicatedEnrollmentEvent6 = (TextBox)formViewPAM.FindControl("txtDedicatedEnrollmentEvent6");

            //section 4
            TextBox ctrltxtRadioShowEstNo = (TextBox)formViewPAM.FindControl("txtRadioShowEstNo");

            //section 5
            TextBox ctrltxtTVshowEstNo = (TextBox)formViewPAM.FindControl("txtTVshowEstNo");
            //section 6
            TextBox ctrltxtEloctronicEstPersons = (TextBox)formViewPAM.FindControl("txtEloctronicEstPersons");
            //section 7
            TextBox ctrltxtReadingEstPersons = (TextBox)formViewPAM.FindControl("txtReadingEstPersons");


            //parse all the digits
            int? interactiveEstAttendees = ToNullableInt32(ctrltxtNoOfIPAttendees.Text);
            int? interactiveEstProvidedEnrollAssistance = ToNullableInt32(ctrltxtIPEnrollmentAssistance.Text);

            int? boothEstDirectContacts = ToNullableInt32(ctrltxtNoOfBoothAttendees.Text);
            int? boothEstEstProvidedEnrollAssistance = ToNullableInt32(ctrltxtBoothEnrollmentAssistance.Text);

            int? dedicatedEstPersonsReached = ToNullableInt32(ctrltxtDedicatedEnrollmentEvent1.Text);
            int? dedicatedEstAnyEnrollmentAssistance = ToNullableInt32(ctrltxtDedicatedEnrollmentEvent2.Text);
            int? dedicatedEstLISEnrollmentAssistance = ToNullableInt32(ctrltxtDedicatedEnrollmentEvent3.Text);
            int? dedicatedEstPartDEnrollmentAssistance = ToNullableInt32(ctrltxtDedicatedEnrollmentEvent4.Text);
            int? dedicatedEstMSPEnrollmentAssistance = ToNullableInt32(ctrltxtDedicatedEnrollmentEvent5.Text);
            int? dedicatedEstOtherEnrollmentAssistance = ToNullableInt32(ctrltxtDedicatedEnrollmentEvent6.Text);

            int? radioEstListenerReach = ToNullableInt32(ctrltxtRadioShowEstNo.Text);
            int? tVEstViewersReach = ToNullableInt32(ctrltxtTVshowEstNo.Text);

            int? electronicEstPersonsViewingOrListening = ToNullableInt32(ctrltxtEloctronicEstPersons.Text);
            int? printEstPersonsReading = ToNullableInt32(ctrltxtReadingEstPersons.Text);

            //check at least one section value is populated
            if (interactiveEstAttendees == null
                && interactiveEstProvidedEnrollAssistance == null
                && boothEstDirectContacts == null
                && boothEstEstProvidedEnrollAssistance == null
                && dedicatedEstPersonsReached == null
                && dedicatedEstAnyEnrollmentAssistance == null
                && dedicatedEstLISEnrollmentAssistance == null
                && dedicatedEstPartDEnrollmentAssistance == null
                && dedicatedEstMSPEnrollmentAssistance == null
                && dedicatedEstOtherEnrollmentAssistance == null
                && radioEstListenerReach == null
                && tVEstViewersReach == null
                && electronicEstPersonsViewingOrListening == null
                && printEstPersonsReading == null)
            {
                _validationError = "You have to enter value for atleast one Event Type.";
                return _validationError;
            }

            //check Section 1
            if (interactiveEstAttendees >= 0 || interactiveEstProvidedEnrollAssistance >= 0)
            {
                //all the value in the section should be 0 or > 0
                if (interactiveEstAttendees == null || interactiveEstProvidedEnrollAssistance == null)
                    _validationError = "You have to enter a non zero value for all the boxes in Event 1.";

                //check all the other section value should be null
                if (boothEstDirectContacts != null
                    || boothEstEstProvidedEnrollAssistance != null
                    || dedicatedEstPersonsReached != null
                    || dedicatedEstAnyEnrollmentAssistance != null
                    || dedicatedEstLISEnrollmentAssistance != null
                    || dedicatedEstPartDEnrollmentAssistance != null
                    || dedicatedEstMSPEnrollmentAssistance != null
                    || dedicatedEstOtherEnrollmentAssistance != null
                    || radioEstListenerReach != null
                    || tVEstViewersReach != null
                    || electronicEstPersonsViewingOrListening != null
                    || printEstPersonsReading != null)
                {

                    _validationError += "<br /> You have to enter value for only one Event Type.";
                    return _validationError;
                }

            }
            //section 1 interactiveEstAttendees should be the gt value
            if (interactiveEstProvidedEnrollAssistance > interactiveEstAttendees)
            {
                _validationError += "<br /> Estimated Number of Attendees should be greater than Estimated Persons Provided Enrollment Assistance in Event 1.";
                return _validationError;
            }

            //check Section 2
            if (boothEstDirectContacts >= 0 || boothEstEstProvidedEnrollAssistance >= 0)
            {
                //all the value in the section should be 0 or > 0
                if (boothEstDirectContacts == null || boothEstEstProvidedEnrollAssistance == null)
                    _validationError = "You have to enter a non zero value for all the boxes in Event 2.";

                //check all the other section value should be null
                if (interactiveEstAttendees != null
                    || interactiveEstProvidedEnrollAssistance != null
                    || dedicatedEstPersonsReached != null
                    || dedicatedEstAnyEnrollmentAssistance != null
                    || dedicatedEstLISEnrollmentAssistance != null
                    || dedicatedEstPartDEnrollmentAssistance != null
                    || dedicatedEstMSPEnrollmentAssistance != null
                    || dedicatedEstOtherEnrollmentAssistance != null
                    || radioEstListenerReach != null
                    || tVEstViewersReach != null
                    || electronicEstPersonsViewingOrListening != null
                    || printEstPersonsReading != null)
                {
                    _validationError += "<br /> You have to enter value for only one Event Type.";
                    return _validationError;
                }

            }
            //section 2 boothEstDirectContacts should be the gt value
            if (boothEstEstProvidedEnrollAssistance > boothEstDirectContacts)
            {
                _validationError += "<br /> Estimated Number of Direct Interactions with Attendees should be greater than Estimated Persons Provided Enrollment Assistance in Event 2.";
                return _validationError;
            }

            //check Section 3 
            if (dedicatedEstPersonsReached >= 0 || dedicatedEstAnyEnrollmentAssistance >= 0
                || dedicatedEstLISEnrollmentAssistance >= 0 || dedicatedEstPartDEnrollmentAssistance >= 0
                            || dedicatedEstMSPEnrollmentAssistance >= 0 || dedicatedEstOtherEnrollmentAssistance >= 0
                )
            {
                //all the value in the section should be 0 or > 0
                if (dedicatedEstPersonsReached == null || dedicatedEstAnyEnrollmentAssistance == null
                     || dedicatedEstLISEnrollmentAssistance == null || dedicatedEstPartDEnrollmentAssistance == null
                       || dedicatedEstMSPEnrollmentAssistance == null || dedicatedEstOtherEnrollmentAssistance == null
                    )
                    _validationError = "You have to enter a non zero value for all the boxes in Event 3.";

                //check all the other section value should be null
                if (interactiveEstAttendees != null
                    || interactiveEstProvidedEnrollAssistance != null
                    || boothEstDirectContacts != null
                    || boothEstEstProvidedEnrollAssistance != null
                    || radioEstListenerReach != null
                    || tVEstViewersReach != null
                    || electronicEstPersonsViewingOrListening != null
                    || printEstPersonsReading != null)
                {
                    _validationError += "<br /> You have to enter value for only one Event Type.";
                    return _validationError;
                }

            }
            //section 3 dedicatedEstPersonsReached should be the gt value
            if (dedicatedEstAnyEnrollmentAssistance > dedicatedEstPersonsReached
                || dedicatedEstLISEnrollmentAssistance > dedicatedEstPersonsReached
                || dedicatedEstPartDEnrollmentAssistance > dedicatedEstPersonsReached
                || dedicatedEstMSPEnrollmentAssistance > dedicatedEstPersonsReached
                || dedicatedEstOtherEnrollmentAssistance > dedicatedEstPersonsReached
                )
            {
                _validationError += "<br /> Est Number Persons Reached at Event Regardless of Enroll Assistance should be the greatest value in Event 3.";
                return _validationError;
            }


            //check Section 4
            if (radioEstListenerReach > 0)
            {
                //check all the other section value should be null
                if (interactiveEstAttendees != null
                    || interactiveEstProvidedEnrollAssistance != null
                    || boothEstDirectContacts != null
                    || boothEstEstProvidedEnrollAssistance != null
                    || dedicatedEstPersonsReached != null
                    || dedicatedEstAnyEnrollmentAssistance != null
                    || dedicatedEstLISEnrollmentAssistance != null
                    || dedicatedEstPartDEnrollmentAssistance != null
                    || dedicatedEstMSPEnrollmentAssistance != null
                    || dedicatedEstOtherEnrollmentAssistance != null
                    || tVEstViewersReach != null
                    || electronicEstPersonsViewingOrListening != null
                    || printEstPersonsReading != null)
                {
                    _validationError = "You have to enter value for only one Event Type.";
                    return _validationError;
                }

            }

            //check Section 5
            if (tVEstViewersReach > 0)
            {
                //check all the other section value should be null
                if (interactiveEstAttendees != null
                    || interactiveEstProvidedEnrollAssistance != null
                    || boothEstDirectContacts != null
                    || boothEstEstProvidedEnrollAssistance != null
                    || dedicatedEstPersonsReached != null
                    || dedicatedEstAnyEnrollmentAssistance != null
                    || dedicatedEstLISEnrollmentAssistance != null
                    || dedicatedEstPartDEnrollmentAssistance != null
                    || dedicatedEstMSPEnrollmentAssistance != null
                    || dedicatedEstOtherEnrollmentAssistance != null
                    || radioEstListenerReach != null
                    || electronicEstPersonsViewingOrListening != null
                    || printEstPersonsReading != null)
                {
                    _validationError = "You have to enter value for only one Event Type.";
                    return _validationError;
                }

            }

            //check Section 6: todo
            if (electronicEstPersonsViewingOrListening > 0)
            {

                //check all the other section value should be null
                if (interactiveEstAttendees != null
                    || interactiveEstProvidedEnrollAssistance != null
                    || boothEstDirectContacts != null
                    || boothEstEstProvidedEnrollAssistance != null
                    || dedicatedEstPersonsReached != null
                    || dedicatedEstAnyEnrollmentAssistance != null
                    || dedicatedEstLISEnrollmentAssistance != null
                    || dedicatedEstPartDEnrollmentAssistance != null
                    || dedicatedEstMSPEnrollmentAssistance != null
                    || dedicatedEstOtherEnrollmentAssistance != null
                    || radioEstListenerReach != null
                    || tVEstViewersReach != null
                    || printEstPersonsReading != null)
                {
                    _validationError = "You have to enter value for only one Event Type.";
                    return _validationError;
                }

            }

            //check Section 7
            if (printEstPersonsReading > 0)
            {

                //check all the other section value should be null
                if (interactiveEstAttendees != null
                    || interactiveEstProvidedEnrollAssistance != null
                    || boothEstDirectContacts != null
                    || boothEstEstProvidedEnrollAssistance != null
                    || dedicatedEstPersonsReached != null
                    || dedicatedEstAnyEnrollmentAssistance != null
                    || dedicatedEstLISEnrollmentAssistance != null
                    || dedicatedEstPartDEnrollmentAssistance != null
                    || dedicatedEstMSPEnrollmentAssistance != null
                    || dedicatedEstOtherEnrollmentAssistance != null
                    || radioEstListenerReach != null
                    || tVEstViewersReach != null
                    || electronicEstPersonsViewingOrListening != null)
                {
                    _validationError = "You have to enter value for only one Event Type.";
                    return _validationError;
                }

            }

            return _validationError;

        }

        string ValidatePresenterList()
        {
            string _validationError = string.Empty;

            GridView ctrlGrid = (GridView)formViewPAM.FindControl("grdPamPresenters");
            Regex rgx = new Regex(@"^\d*\.?((25)|(50)|(5)|(75)|(0)|(00))?$");

            int _noOfPresentersSelected = 0;
            for (int i = 0; i < ctrlGrid.Rows.Count; i++)
            {
                PamPresenters _presenter = new PamPresenters();

                System.Web.UI.WebControls.DropDownList ctrldp = (System.Web.UI.WebControls.DropDownList)ctrlGrid.Rows[i].Cells[0].Controls[1];
                _presenter.PAMUserId = ((System.Web.UI.WebControls.ListControl)(ctrldp)).SelectedValue;
                _presenter.Affiliation = ((System.Web.UI.WebControls.TextBox)ctrlGrid.Rows[i].Cells[1].Controls[1]).Text;

                decimal _hoursSpent;
                string _hrsString = ((System.Web.UI.WebControls.TextBox)ctrlGrid.Rows[i].Cells[2].Controls[1]).Text;
                if (_hrsString == string.Empty)
                    _presenter.HoursSpent = null;
                else
                    _presenter.HoursSpent = (decimal.TryParse(_hrsString, out _hoursSpent)) ? _hoursSpent : 0;

                if (Convert.ToInt32(_presenter.PAMUserId) > 0) _noOfPresentersSelected++;
                //construct validation error
                if (Convert.ToInt32(_presenter.PAMUserId) > 0 && 
                    (_presenter.HoursSpent == null || rgx.IsMatch(_presenter.HoursSpent.ToString())==false
                        || _presenter.HoursSpent == 0))
                    _validationError += "<br />Please enter valid Total Hours Spent for line no: " + (i + 1).ToString();


                if (Convert.ToInt32(_presenter.PAMUserId) == 0 &&
                (_presenter.HoursSpent != null || _presenter.Affiliation.Length> 0))
                    _validationError += "<br />Please Select a Presenter or Contributor for line no: " + (i + 1).ToString();
            }

            if (_noOfPresentersSelected ==0)
                _validationError += "<br />Please Select at least One Presenter or Contributor.";

            return _validationError;
        }

        IList<PamPresenters> buildPresenterList()
        {
            IList<PamPresenters> listShipUsr = new List<PamPresenters>();

            GridView ctrlGrid = (GridView)formViewPAM.FindControl("grdPamPresenters");

            for (int i = 0; i < ctrlGrid.Rows.Count; i++)
            {
               decimal _hoursSpent;

                PamPresenters _myone = new PamPresenters();

                System.Web.UI.WebControls.DropDownList ctrldp = (System.Web.UI.WebControls.DropDownList)ctrlGrid.Rows[i].Cells[0].Controls[1];
                _myone.PAMUserId = ((System.Web.UI.WebControls.ListControl)(ctrldp)).SelectedValue;
                _myone.Affiliation = ((System.Web.UI.WebControls.TextBox)ctrlGrid.Rows[i].Cells[1].Controls[1]).Text;

                _myone.HoursSpent =
                (decimal.TryParse(((System.Web.UI.WebControls.TextBox)ctrlGrid.Rows[i].Cells[2].Controls[1]).Text, out _hoursSpent)) ? _hoursSpent : 0;
                 
                if (Convert.ToInt32(_myone.PAMUserId) > 0 && _myone.HoursSpent > 0)
                                    listShipUsr.Add(_myone);
            }

            return listShipUsr;
        }


        public int? ToNullableInt32(string s)
        {
            s = s.Replace("_", "");
            int i;
            if (Int32.TryParse(s, out i)) return i;
            return null;
        }

        #endregion

        #region Event Handlers


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OnViewInitialized();
            }

        }


        public void RaisePostBackEvent(string eventArgument)
        {
            AddPresenterGridRow(false);

        }



        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            AddPresenterGridRow(true);

        }

        protected void dropDownListAgency_SelectedIndexChanged(object sender, EventArgs e)
        {
            /////////BindFormData();
        }



        protected void dropDownListEventState_SelectedIndexChanged(object sender, EventArgs e)
        {
            //bind the dependent county
            DefaultEventState = new State(((System.Web.UI.WebControls.DropDownList)sender).SelectedValue);

            EventCounties = AgencyLogic.GetCounties(DefaultEventState.Code);

            System.Web.UI.WebControls.DropDownList ctrlName = (System.Web.UI.WebControls.DropDownList)formViewPAM.FindControl("dropDownListEventCounty");
            ctrlName.DataSource = EventCounties;
            ctrlName.DataBind();

            ctrlName.Items.Insert(0, new ListItem("-- Select County --", ""));
            ctrlName.Focus();


            //cler the zipcode value if any...
            System.Web.UI.WebControls.DropDownList ctrlZip = (System.Web.UI.WebControls.DropDownList)formViewPAM.FindControl("dropDownListEventZipCode");
            ctrlZip.Items.Clear();
            ctrlZip.Items.Insert(0, new ListItem("-- Select Zip Code --", ""));
        }

        protected void dropDownListEventCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            //bind the dependent Zip Code
            string _countyCode = ((System.Web.UI.WebControls.DropDownList)sender).SelectedValue;

            IEnumerable<KeyValuePair<int, string>> EventZips = LookupBLL.GetZipCodeForCountyFips(_countyCode);

            System.Web.UI.WebControls.DropDownList ctrlName = (System.Web.UI.WebControls.DropDownList)formViewPAM.FindControl("dropDownListEventZipCode");
            ctrlName.DataSource = EventZips;
            ctrlName.DataBind();

            ctrlName.Items.Insert(0, new ListItem("-- Select Zip Code --", ""));

            ctrlName.Focus();


        } 

        protected void dataSourceEditPAM_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
           BindFormData( ViewData.AgencyId);

           dataSourceEditPAM.DataSource = ViewData;

        }
        


        protected void dataSourceEditPAM_Updated(object sender, ObjectContainerDataSourceStatusEventArgs e)
        {
            var viewData = (EditPublicMediaEventViewData)e.Instance;


            viewData.IsBatchUploadData = false;
            viewData.BatchStateUniqueID = null;

            viewData.SetLastUpdated(AccountInfo.UserId);


            Logic.UpdatePublicMediaEvent(viewData);
            RouteController.RouteTo(RouteController.PamView(Id.Value));
        }

        protected void dataSourceEditPAM_Updating(object sender, ObjectContainerDataSourceUpdatingEventArgs e)
        {
            //add the pampresenters from gridView..
            e.NewValues["PamPresenters"] = buildPresenterList();

            e.NewValues[AgencyIdKey] = AgencyId;
            e.NewValues[AgencyCodeKey] = AgencyCode;

            e.NewValues[ContactPhone] = e.NewValues[ContactPhone].ToString().FormatPhoneNumber();

            //parse all the digits
            e.NewValues["InteractiveEstAttendees"] = ToNullableInt32(e.NewValues["InteractiveEstAttendees"].ToString());
            e.NewValues["InteractiveEstProvidedEnrollAssistance"] = ToNullableInt32(e.NewValues["InteractiveEstProvidedEnrollAssistance"].ToString());

            e.NewValues["BoothEstDirectContacts"] = ToNullableInt32(e.NewValues["BoothEstDirectContacts"].ToString());
            e.NewValues["BoothEstEstProvidedEnrollAssistance"] = ToNullableInt32(e.NewValues["BoothEstEstProvidedEnrollAssistance"].ToString());

            e.NewValues["RadioEstListenerReach"] = ToNullableInt32(e.NewValues["RadioEstListenerReach"].ToString());
            e.NewValues["TVEstViewersReach"] = ToNullableInt32(e.NewValues["TVEstViewersReach"].ToString());

            e.NewValues["DedicatedEstPersonsReached"] = ToNullableInt32(e.NewValues["DedicatedEstPersonsReached"].ToString());
            e.NewValues["DedicatedEstAnyEnrollmentAssistance"] = ToNullableInt32(e.NewValues["DedicatedEstAnyEnrollmentAssistance"].ToString());
            e.NewValues["DedicatedEstLISEnrollmentAssistance"] = ToNullableInt32(e.NewValues["DedicatedEstLISEnrollmentAssistance"].ToString());
            e.NewValues["DedicatedEstPartDEnrollmentAssistance"] = ToNullableInt32(e.NewValues["DedicatedEstPartDEnrollmentAssistance"].ToString());
            e.NewValues["DedicatedEstMSPEnrollmentAssistance"] = ToNullableInt32(e.NewValues["DedicatedEstMSPEnrollmentAssistance"].ToString());
            e.NewValues["DedicatedEstOtherEnrollmentAssistance"] = ToNullableInt32(e.NewValues["DedicatedEstOtherEnrollmentAssistance"].ToString());

            e.NewValues["ElectronicEstPersonsViewingOrListening"] = ToNullableInt32(e.NewValues["ElectronicEstPersonsViewingOrListening"].ToString());
            e.NewValues["PrintEstPersonsReading"] = ToNullableInt32(e.NewValues["PrintEstPersonsReading"].ToString());


            //populate the state/county/zip dropdown value
            e.NewValues["EventState"] = DefaultEventState;
            System.Web.UI.WebControls.DropDownList _county = (System.Web.UI.WebControls.DropDownList)formViewPAM.FindControl("dropDownListEventCounty");
            System.Web.UI.WebControls.DropDownList _zip = (System.Web.UI.WebControls.DropDownList)formViewPAM.FindControl("dropDownListEventZipCode");
            e.NewValues["EventCountycode"] = _county.SelectedItem.Value;
            e.NewValues["EventZIPCode"] = _zip.SelectedItem.Text; //text is the zipcode: value is CountyZIPID

            e.NewValues[ReviewerUserIdKey] = ReviewerUserId;

        }




        public override void Validate()
        {
            var _proxyValidatorEventCounty =
                formViewPAM.FindControl("proxyValidatorEventCounty") as PropertyProxyValidator;

            var _proxyValidatorEventZipCode =
                formViewPAM.FindControl("proxyValidatorEventZipCode") as PropertyProxyValidator;

            System.Web.UI.WebControls.DropDownList _county = (System.Web.UI.WebControls.DropDownList)formViewPAM.FindControl("dropDownListEventCounty");
            System.Web.UI.WebControls.DropDownList _zip = (System.Web.UI.WebControls.DropDownList)formViewPAM.FindControl("dropDownListEventZipCode");


             if (_proxyValidatorEventCounty != null)
                 _proxyValidatorEventCounty.Enabled = (_county.SelectedItem.Value == null ||
                                                               _county.SelectedItem.Value == string.Empty);
             if (_proxyValidatorEventZipCode != null)
                 _proxyValidatorEventZipCode.Enabled = (_zip.SelectedItem.Value == null ||
                                                               _zip.SelectedItem.Value == string.Empty);

            base.Validate();
        }

        //This method has been implemented on 06/11/2013 : Lavanya Maram
        //Validate "CMS Special Use Fields"
        protected bool ValidCMSSpecialUseFields()
        {
            bool IsValidCMSSpecialUseFields = false;

            var specialFieldListCMS = formViewPAM.FindControl("specialFieldListCMS") as ShiptalkWebControls.SpecialFieldList;
            Label ErrSpecialFieldListCMSmsg = formViewPAM.FindControl("ErrspecialFieldListCMSmsg") as Label;

            List<KeyValuePair<int, string>> ItemsList = new List<KeyValuePair<int, string>>();

            foreach (DictionaryEntry Item in specialFieldListCMS.Items)
            {
                ItemsList.Add(new KeyValuePair<int, string>(Convert.ToInt32(Item.Key.ToString()), Item.Value.ToString()));
            }

            ItemsList.Sort(CompareKey);

            int specialFieldListCMSItemCount = 0;
            bool FirstItemPassed = false;

            foreach (var Item in ItemsList)
            {
                if (!string.IsNullOrEmpty(Item.Value.ToString()))
                {
                    if (FirstItemPassed)
                    {
                        specialFieldListCMSItemCount += 1;
                    }
                }

                FirstItemPassed = true;
            }

            if (specialFieldListCMSItemCount != 0 && specialFieldListCMSItemCount != ItemsList.Count - 1)
            {
                ErrSpecialFieldListCMSmsg.Visible = true;
                ErrSpecialFieldListCMSmsg.Text = "If you complete one PAM Duals data field, you must complete both Duals data elements.";
                specialFieldListCMS.Focus();
            }

            else
            {
                IsValidCMSSpecialUseFields = true;
                ErrSpecialFieldListCMSmsg.Visible = false;
            }

            return IsValidCMSSpecialUseFields;
        }

        //this method receives two KeyValuePair structs and returns the result of CompareTo on their Key
        static int CompareKey(KeyValuePair<int, string> a, KeyValuePair<int, string> b)
        {
            return a.Key.CompareTo(b.Key);
        }

        protected void proxyValidatorEventState_ValueConvert(object sender, ValueConvertEventArgs e)
        {
            e.ConvertedValue = e.ValueConvertState();
        }

        protected void PropertyProxyValidatorActivityStartDate_ValueConvert(object sender, ValueConvertEventArgs e)
        {
            DateTime value;
            e.ConvertedValue = (System.DateTime.TryParse(e.ValueToConvert.ToString(), out value)) ? value : System.DateTime.MinValue;
        }

        protected void validatorCheckUpperBound_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var textBoxStartDate = formViewPAM.FindControl("txtStartDate") as TextBox;

            DateTime _dateOfContact;
            if (DateTime.TryParse(textBoxStartDate.Text, out _dateOfContact) == false)
                _dateOfContact = new DateTime(1753, 1, 1);

            //Allow 1 calendar date in the future on cc date of contact and on the pam date of event only for GU [FIPS 66].
            if (DefaultState.Code == "66") _dateOfContact = _dateOfContact.AddDays(-1);

            if (_dateOfContact > DateTime.Now)
                args.IsValid = false;
            else
                args.IsValid = true;

        }

        protected void validatorCheckEndDateUpperBound_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var textBoxStartDate = formViewPAM.FindControl("txtEndDate") as TextBox;

            DateTime _dateOfContact;
            if (DateTime.TryParse(textBoxStartDate.Text, out _dateOfContact) == false)
                _dateOfContact = new DateTime(1753, 1, 1);

            //Allow 1 calendar date in the future on cc date of contact and on the pam date of event only for GU [FIPS 66].
            if (DefaultState.Code == "66") _dateOfContact = _dateOfContact.AddDays(-1);

            if (_dateOfContact > DateTime.Now)
                args.IsValid = false;
            else
                args.IsValid = true;

        }

        protected void validatorCheckIfOtherTopicsChecked_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var checkBoxListPamTopics = formViewPAM.FindControl("checkBoxListPamTopics") as ShiptalkWebControls.CheckBoxList;
            var textBoxOtherPamTopicSpecified = formViewPAM.FindControl("textBoxOtherPamTopicSpecified") as TextBox;


            if (checkBoxListPamTopics.Items[16].Selected == true)
            {
                if (textBoxOtherPamTopicSpecified.Text.Length > 0)
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
            else
            {
                args.IsValid = true;

            }

        }

        protected void validatorCheckIfOtherTopicsText_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var checkBoxListPamTopics = formViewPAM.FindControl("checkBoxListPamTopics") as ShiptalkWebControls.CheckBoxList;
            var textBoxOtherPamTopicSpecified = formViewPAM.FindControl("textBoxOtherPamTopicSpecified") as TextBox;

            if (textBoxOtherPamTopicSpecified.Text.Length > 0)
            //check if the other checkbox is checked or not..
            {
                if (checkBoxListPamTopics.Items[16].Selected == true)
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
            else
            {
                args.IsValid = true;

            }

        }



        protected void validatorCheckIfOtherAudienceChecked_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var checkBoxListPamAudience = formViewPAM.FindControl("checkBoxListPamAudience") as ShiptalkWebControls.CheckBoxList;
            var textBoxOtherPamAudienceSpecified = formViewPAM.FindControl("textBoxOtherPamAudienceSpecified") as TextBox;

            if (checkBoxListPamAudience.Items[28].Selected == true)
            {
                if (textBoxOtherPamAudienceSpecified.Text.Length > 0)

                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
            else
            {
                args.IsValid = true;

            }

        }

        protected void validatorCheckIfOtherAudienceText_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var checkBoxListPamAudience = formViewPAM.FindControl("checkBoxListPamAudience") as ShiptalkWebControls.CheckBoxList;
            var textBoxOtherPamAudienceSpecified = formViewPAM.FindControl("textBoxOtherPamAudienceSpecified") as TextBox;

            if (textBoxOtherPamAudienceSpecified.Text.Length > 0)
            //check if the other checkbox is checked or not..
            {
                if (checkBoxListPamAudience.Items[28].Selected == true)
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
            else
            {
                args.IsValid = true;

            }

        }


        protected void buttonSubmit_Click(object sender, EventArgs e)
        {

            string _validationGridErrorText = ValidatePresenterList();
            string _validationSectionErrorText = ValidateSections();

            Label ctrlGridError = (Label)formViewPAM.FindControl("lblgridValidation");
            Label ctrlSectionError = (Label)formViewPAM.FindControl("lblSectionValidation");

            Panel ctrlPanelError = (Panel)formViewPAM.FindControl("panelError");
            ctrlPanelError.Visible = false;

            if (_validationGridErrorText.Length > 0 || _validationSectionErrorText.Length > 0)
            {
                //set the error string
                ctrlGridError.Text = _validationGridErrorText;
                ctrlSectionError.Text = _validationSectionErrorText;
                ctrlPanelError.Visible = true;

                return;
            }

            ctrlGridError.Text = string.Empty;
            ctrlSectionError.Text = string.Empty;


            Validate();

            if (Page.IsValid == false)
                ctrlPanelError.Visible = true;

            //Updated on 06/11/2013 : Lavanya Maram
            if (ValidCMSSpecialUseFields())
            {
                 formViewPAM.UpdateItem(true);
            }
            else
                return;
        }

        protected void grdPamPresenters_OnPreRender(object sender, EventArgs e)
        {
            //bind it only once at the initilization of grid..
            if (!IsPostBack)
            {
                GridView ctrlGrid = (GridView)formViewPAM.FindControl("grdPamPresenters");

                int _gridCount = ctrlGrid.Rows.Count;
                int _presentersCountInData = ViewData.PamPresenters.Count;

                if (_presentersCountInData < _gridCount)
                    _gridCount = _presentersCountInData;

                //adjust the userState if any..
                for (int i = 0; i < _gridCount; i++)
                {
                    System.Web.UI.WebControls.DropDownList ctrldp = (System.Web.UI.WebControls.DropDownList)ctrlGrid.Rows[i].Cells[0].Controls[1];
                    ((System.Web.UI.WebControls.ListControl)(ctrldp)).SelectedValue = ViewData.PamPresenters[i].PAMUserId;

                    ((System.Web.UI.WebControls.TextBox)ctrlGrid.Rows[i].Cells[1].Controls[1]).Text = ViewData.PamPresenters[i].Affiliation;
                    ((System.Web.UI.WebControls.TextBox)ctrlGrid.Rows[i].Cells[2].Controls[1]).Text = ViewData.PamPresenters[i].HoursSpent.ToString();

                }
            }
        }

        protected void checkBoxListPamTopics_DataBound(object sender, EventArgs e)
        {
            ((ShiptalkWebControls.CheckBoxList)sender).SelectedItems = ViewData.PAMSelectedTopics;
        }


        protected void checkBoxListPamAudience_DataBound(object sender, EventArgs e)
        {
            ((ShiptalkWebControls.CheckBoxList)sender).SelectedItems = ViewData.PAMSelectedAudiences;
        }
        
        protected void formViewPAM_OnPreRender(object sender, EventArgs e)
        {
            //bind it only once at the initilization of form..
            if (!IsPostBack)
            {
                string _stateCode = ((State)ViewData.EventState).Code;
                string _stateAbbr = ((State)ViewData.EventState).StateAbbr;
                string _countyCode = ViewData.EventCountycode;
                string _zipCode = ViewData.EventZIPCode;

                //put the state code
                System.Web.UI.WebControls.DropDownList ctrlState = (System.Web.UI.WebControls.DropDownList)formViewPAM.FindControl("dropDownListEventState");
                ctrlState.SelectedValue = _stateAbbr;

                //bind the dependent county
                EventCounties = AgencyLogic.GetCounties(_stateCode);

                System.Web.UI.WebControls.DropDownList ctrlCounty = (System.Web.UI.WebControls.DropDownList)formViewPAM.FindControl("dropDownListEventCounty");
                ctrlCounty.DataSource = EventCounties;
                ctrlCounty.DataBind();
                ctrlCounty.Items.Insert(0, new ListItem("-- Select County --", ""));
                ctrlCounty.SelectedValue = _countyCode;

                //bind the dependent Zip Code
                IEnumerable<KeyValuePair<int, string>> EventZips = LookupBLL.GetZipCodeForCountyFips(_countyCode);

                System.Web.UI.WebControls.DropDownList ctrlName = (System.Web.UI.WebControls.DropDownList)formViewPAM.FindControl("dropDownListEventZipCode");
                ctrlName.DataSource = EventZips;
                ctrlName.DataBind();
                ctrlName.Items.Insert(0, new ListItem("-- Select Zip Code --", ""));

                if (ctrlName.Items.Count >1)
                {
                 ListItem _zipItem =   ctrlName.Items.FindByText(_zipCode);
                    if (_zipItem != null)
                        _zipItem.Selected = true;
                }

            }
        }

        #endregion


        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion




        #region IAuthorize Members

        public bool IsAuthorized()
        {
            if (!AgencyLogic.IsAgencyUserActive(ViewData.AgencyId, AccountInfo.UserId))
                return false;

            //set the ReviewerUserId
            ReviewerUserId = (Logic.IsUserPamReviewer(ViewData.PamID, AccountInfo.UserId))
                                 ? (int?)AccountInfo.UserId
                                 : null;

            if (ViewData.SubmitterUserID == AccountInfo.UserId)
                return true;

            if (Logic.IsUserIdInPresenters(ViewData.PamID, AccountInfo.UserId))
                return true;

            var descriptors = UserBLL.GetDescriptorsForUser(AccountInfo.UserId, ViewData.AgencyId);

            foreach (var descriptor in descriptors)
            {
                if (descriptor == (int)Descriptor.DataSubmitter
                    || descriptor == (int)Descriptor.ShipDirector)
                    return true;
            }

            return (ReviewerUserId.HasValue);
        }

        #endregion
    }
}
