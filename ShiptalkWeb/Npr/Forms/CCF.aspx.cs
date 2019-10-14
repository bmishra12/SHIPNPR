using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration;
using Microsoft.Practices.Web.UI.WebControls;
using ShiptalkLogic;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkWeb.Routing;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet;
using System.Linq;


namespace ShiptalkWeb.Npr.Forms
{
    public partial class CCF : Page, IRouteDataPage, IAuthorize
    {
        private const string IdKey = "Id";
        private const string AgencyIdKey = "AgencyId";
        private const string AgencyCodeKey = "AgencyCode";
        private const string AgencyNameKey = "AgencyName";
        private const string AgencyStateKey = "AgencyState";
        private const string DefaultStateKey = "DefaultState";
        private const string IsAdminKey = "IsAdmin";
        private const string ScopeKey = "Scope";
        private const string AutoAssignedClientIdKey = "AutoAssignedClientId";
        private const string HoursSpentKey = "HoursSpent";
        private const string MinutesSpentKey = "MinutesSpent";
        private const string ClientCountyCodeKey = "ClientCountyCode";
        private const string CounselorUserIdKey = "CounselorUserId";
        private const string CounselorCountyCodeKey = "CounselorCountyCode";
        private const string CounselorZIPCodeKey = "CounselorZIPCode";
        private const string ClientZIPCodeKey = "ClientZIPCode";
        private const string ClientLearnedAboutSHIPKey = "ClientLearnedAboutSHIP";
        private const string ClientAgeGroupKey = "ClientAgeGroup";
        private const string ClientGenderKey = "ClientGender";
        private const string ClientMonthlyIncomeKey = "ClientMonthlyIncome";
        private const string ClientAssetsKey = "ClientAssets";
        private const string ClientMethodOfContactKey = "ClientMethodOfContact";
        private const string ClientFirstVsContinuingContactKey = "ClientFirstVsContinuingContact";
        private const string ClientPrimaryLanguageOtherThanEnglishKey = "ClientPrimaryLanguageOtherThanEnglish";
        private const string ClientReceivingSSOrMedicareDisabilityKey = "ClientReceivingSSOrMedicareDisability";
        private const string ClientDualEligbleKey = "ClientDualEligble";
        private const string ClientStatusKey = "ClientStatus";

        private AgencyBLL _agencyLogic;
        private CCFBLL _cffLogic;
        private ViewClientContactViewData _viewCCViewData;

        #region Properties

        protected AddType CcfAddType
        {
            get
            {
                if (RouteData == null)
                    return AddType.None;

                switch (((AuthorizeRoute) RouteData.Route).Url)
                {
                    case "forms/ccf/add-a-{id}":
                        return AddType.Agency;
                    case "forms/ccf/add-c-{id}":
                        return AddType.Client;
                    default:
                        return AddType.None;
                }
            }
        }

        public IEnumerable<SpecialField> CMSSpecialFields { get; set; }
        
        public IEnumerable<SpecialField> StateSpecialFields { get; set; }

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

        public int AgencyId { get { return Convert.ToInt32(ViewState[AgencyIdKey]); } set { ViewState[AgencyIdKey] = value; } }

        public string AgencyCode { get { return ViewState[AgencyCodeKey].ToString(); } set { ViewState[AgencyCodeKey] = value; } }

        public string AgencyName { get { return ViewState[AgencyNameKey].ToString(); } set { ViewState[AgencyNameKey] = value; } }

        public State AgencyState { get { return (State)ViewState[AgencyStateKey]; } set { ViewState[AgencyStateKey] = value; } }

        public string AutoAssignedClientId { get { return ViewState[AutoAssignedClientIdKey].ToString(); } set { ViewState[AutoAssignedClientIdKey] = value; } }

        public State DefaultState { get { return (State)ViewState[DefaultStateKey]; } set { ViewState[DefaultStateKey] = value; } }

        public bool IsAdmin { get { return (bool)ViewState[IsAdminKey]; } set { ViewState[IsAdminKey] = value; } }

        public AgencyBLL AgencyLogic
        {
            get
            {
                if (_agencyLogic == null) _agencyLogic = new AgencyBLL();

                return _agencyLogic;
            }
        }

        public CCFBLL Logic
        {
            get
            {
                if (_cffLogic == null) _cffLogic = new CCFBLL();

                return _cffLogic;
            }
        }

        public UserAccount Counselor { get; set; }

        public Scope Scope { get { return (Scope)ViewState[ScopeKey]; } set { ViewState[ScopeKey] = value; } }

        public IEnumerable<KeyValuePair<int, string>> Counselors { get; set; }

        public IEnumerable<KeyValuePair<int, string>> ClientLearnedAboutSHIP { get { return Logic.GetClientLearnedAboutSHIP(); } }

        public IEnumerable<KeyValuePair<int, string>> ClientAgeGroup { get { return Logic.GetClientAgeGroup(); } }
        
        public IEnumerable<KeyValuePair<int, string>> ClientGender { get { return Logic.GetClientGender(); } }

        public IEnumerable<KeyValuePair<int, string>> ClientMonthlyIncome { get { return Logic.GetClientMonthlyIncome(); } }

        public IEnumerable<KeyValuePair<int, string>> ClientAssets { get { return Logic.GetClientAssets(); } }

        public IEnumerable<KeyValuePair<int, string>> ClientMethodOfContact { get { return Logic.GetClientMethodOfContact(); } }

        public IEnumerable<KeyValuePair<int, string>> ClientFirstVsContinuingContact { get { return Logic.GetContactType(); } }

        public IEnumerable<KeyValuePair<int, string>> ClientPrimaryLanguageOtherThanEnglish { get { return Logic.GetClientLanguage(); } }

        public IEnumerable<KeyValuePair<int, string>> ClientReceivingSSOrMedicareDisability { get { return Logic.GetClientReceivingSSOrMedicareDisability(); } }

        public IEnumerable<KeyValuePair<int, string>> ClientRaceDescription { get { return Logic.GetClientRace(); } }

        public IEnumerable<KeyValuePair<int, string>> ClientDualEligble { get { return Logic.GetClientDualEligble(); } }

        public IEnumerable<KeyValuePair<int, string>> ClientStatus { get { return Logic.GetClientStatus(); } }

        public IEnumerable<KeyValuePair<int, string>> MedicarePrescriptionDrugCoverage { get { return Logic.GetMedicarePrescriptionDrugCoverage(); } }

        public IEnumerable<KeyValuePair<int, string>> MedicareAdvantage { get { return Logic.GetMedicareAdvantage(); } }

        public IEnumerable<KeyValuePair<int, string>> PartDLowIncomeSubsidy { get { return Logic.GetPartDLowIncomeSubsidy(); } }

        public IEnumerable<KeyValuePair<int, string>> MedicareSupplement { get { return Logic.GetMedicareSupplement(); } }

        public IEnumerable<KeyValuePair<int, string>> OtherPrescriptionAssistance { get { return Logic.GetOtherPrescriptionAssistance(); } }

        public IEnumerable<KeyValuePair<int, string>> Medicaid { get { return Logic.GetMedicaid(); } }

        public IEnumerable<KeyValuePair<int, string>> MedicarePartsAandB { get { return Logic.GetMedicarePartsAandB(); } }

        public IEnumerable<KeyValuePair<int, string>> OtherDrug { get { return Logic.GetOtherDrug(); } }

        #endregion

        #region Methods

        /// <summary>
        /// Binds the dependent data for the form.
        /// </summary>
        protected void BindDependentData()
        {
            if (CcfAddType != AddType.None && _viewCCViewData==null)
                _viewCCViewData = Logic.GetViewClientContact(Id.GetValueOrDefault(0));

            if (_viewCCViewData != null) return;

            dropDownListState.DataSource = AgencyLogic.GetStates();
            dropDownListState.DataBind();
            dropDownListState.Focus();

            if (dropDownListState.Items.Count > 1)
                BindStateAgencies();
        }

        protected void BindStateAgencies()
        {
            panelAgencyFilter.Visible = false;
            labelNoStateAgenciesFound.Visible = false;
            dropDownListAgency.Visible = true;
            
            List<KeyValuePair<int, string>> stateAgencies;

            if (AccountInfo.Scope == Scope.CMS)
            {
                //Create a stateAgencies without any value: for the initial case of selected value "CM"
                //so that CMS guy can pickup the state and agency using dropdowns
                if (dropDownListState.SelectedValue == "CM")
                    stateAgencies = new List<KeyValuePair<int, string>>();
                else
                    stateAgencies = new List<KeyValuePair<int, string>>(AgencyLogic.GetAgencies(new State(dropDownListState.SelectedValue).Code));

            }
            else if (AccountInfo.Scope == Scope.State)
                stateAgencies = new List<KeyValuePair<int, string>>(AgencyLogic.GetAgencies(new State(dropDownListState.SelectedValue).Code));
            else
                stateAgencies = new List<KeyValuePair<int, string>>(AgencyLogic.GetAgencies(AccountInfo.UserId, false));
            
            if (stateAgencies.Count > 0)
            {
                dropDownListAgency.DataSource = stateAgencies;
                dropDownListAgency.DataBind();
                dropDownListAgency.Items.Insert(0, new ListItem("-- Select an Agency --", ""));
                dropDownListAgency.Focus();
                panelAgencyFilter.Visible = true;
                labelNoStateAgenciesFound.Visible = false;

                if (stateAgencies.Count == 1)
                {
                    dropDownListAgency.SelectedIndex = 1;
                    BindFormData();
                }

                return;
            }

            if (dropDownListState.SelectedIndex <= 0) return;

            panelAgencyFilter.Visible = true;
            labelNoStateAgenciesFound.Visible = true;
            dropDownListAgency.Visible = false;
        }

        protected void BindFormData()
        {
            if (_viewCCViewData != null)
                AgencyId = _viewCCViewData.AgencyId;
            else
                AgencyId = (string.IsNullOrEmpty(dropDownListAgency.SelectedValue))
                               ? 0
                               : Convert.ToInt32(dropDownListAgency.SelectedValue);

            if (AgencyId > 0)
            {
                var agencyIdentifiers = AgencyLogic.GetAgencyIdentifiers(AgencyId);
                AgencyCode = agencyIdentifiers.Code;
                AgencyState = agencyIdentifiers.State;
                AgencyName = agencyIdentifiers.Name;
                AutoAssignedClientId = (CcfAddType == AddType.Client) ? string.Empty : Logic.GetNextAutoAssignedClientID(AgencyCode);
                
                //OLD logic
                //Counselors = AgencyLogic.GetAgencyUsers(AgencyId, Descriptor.Counselor).OrderBy(p => p.Value);

                Counselors = GetCounselors(AgencyId);
                
                CMSSpecialFields = Logic.GetCMSSpecialFields();
                StateSpecialFields = Logic.GetStateSpecialFields(agencyIdentifiers.State);
                panelStateAndAgencyFilter.Visible = false;
                panelForm.Visible = true;
            }
            else
            {
                panelForm.Visible = false;
                panelStateAndAgencyFilter.Visible = true;
            }
        }


        private IEnumerable<KeyValuePair<int, string>> GetCounselors(int AgencyId)
        {
            //Populate Counselors from DB
            CCFBLL.CCCounselorsFilterCriteria criteria = new CCFBLL.CCCounselorsFilterCriteria();
            criteria.StateFIPS = AgencyState.Code;
            criteria.scope = this.AccountInfo.Scope;
            criteria.UserId = this.AccountInfo.UserId;
            criteria.AgencyId = AgencyId;
            return Logic.GetClientContactCounselorsForCCForm(criteria,true);
        }

        /// <summary>
        /// Called when page is initialized for the first time.
        /// </summary>
        protected void OnViewInitialized()
        {
            DefaultState = new State(AccountInfo.StateFIPS);
            IsAdmin = AccountInfo.IsAdmin;
            Scope = (Scope)AccountInfo.ScopeId;
            BindDependentData();
            BindFormData();
        }

        /// <summary>
        /// Called when page is loaded.
        /// </summary>
        protected void OnViewLoaded()
        {
        }

        public override void Validate()
        {
            var radioButtonUseStateSpecificClientId =
                formViewAddClientContact.FindControl("radioButtonUseStateSpecificClientId") as RadioButton;
            var dropDownListCounselorCounty = (DropDownList)formViewAddClientContact.FindControl("dropDownListCounselorCounty");
            var dropDownListCounselorZipCode = (DropDownList)formViewAddClientContact.FindControl("dropDownListCounselorZipCode");
            var proxyValidatorStateSpecificClientId =
                formViewAddClientContact.FindControl("proxyValidatorStateSpecificClientId") as PropertyProxyValidator;
            var proxyValidatorCounselorCounty =
                formViewAddClientContact.FindControl("proxyValidatorCounselorCounty") as PropertyProxyValidator;
            var proxyValidatorCounselorZipCode =
                formViewAddClientContact.FindControl("proxyValidatorCounselorZipCode") as PropertyProxyValidator;

            if (proxyValidatorStateSpecificClientId != null)
                proxyValidatorStateSpecificClientId.Enabled = (radioButtonUseStateSpecificClientId != null && radioButtonUseStateSpecificClientId.Checked);

            if (proxyValidatorCounselorCounty != null)
                proxyValidatorCounselorCounty.Enabled = (string.IsNullOrEmpty(dropDownListCounselorCounty.SelectedItem.Value));

            if (proxyValidatorCounselorZipCode != null)
                proxyValidatorCounselorZipCode.Enabled = (string.IsNullOrEmpty(dropDownListCounselorZipCode.SelectedItem.Value));

            base.Validate();                
        }

        //This method has been implemented on 06/10/2013 : Lavanya Maram
        //Validate "CMS Special Use Fields"
        protected bool ValidCMSSpecialUseFields()
        {
            bool IsValidCMSSpecialUseFields = false;

            var specialFieldListCMS = formViewAddClientContact.FindControl("specialFieldListCMS") as ShiptalkWebControls.SpecialFieldList;
            Label ErrSpecialFieldListCMSmsg = formViewAddClientContact.FindControl("ErrspecialFieldListCMSmsg") as Label;

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
                ErrSpecialFieldListCMSmsg.Text = "If you complete one CC Duals data field, you must complete all nine Duals data elements.";
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
       
        #endregion

        #region Event Handlers

        protected void textBoxClientZIPCode_Blur(object sender, EventArgs e)
        {
            var textBoxClientZIPCode = (TextBox)sender;
            var proxyValidatorClientZIPCode = formViewAddClientContact.FindControl("proxyValidatorClientZIPCode") as PropertyProxyValidator;
            var dropDownClientCountyCode = formViewAddClientContact.FindControl("dropDownClientCountyCode") as DropDownList;

            proxyValidatorClientZIPCode.Validate();
            if (!proxyValidatorClientZIPCode.IsValid) return;

            dropDownClientCountyCode.DataSource = LookupBLL.GetCountiesForZipCode(textBoxClientZIPCode.Text);
            dropDownClientCountyCode.DataBind();

            //if there are more than one value make the 1st item as selected item..
            //As the counties are orderd by zipCounty.displayorder
            if (dropDownClientCountyCode.Items.Count > 1)
                dropDownClientCountyCode.SelectedIndex = 0;

            dropDownClientCountyCode.Focus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) OnViewInitialized();

            OnViewLoaded();
        }

        protected void dropDownListState_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindStateAgencies();
        }

        protected void dropDownListAgency_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindFormData();
            formViewAddClientContact.FindControl("textBoxClientFirstName").Focus();
        }

        protected void dropDownListCounselor_DataBound(object sender, EventArgs e)
        {
            var dropDownCounselor = sender as DropDownList;

            foreach (ListItem listItem in dropDownCounselor.Items)
            {
                var counselorId = 0;
                if (!int.TryParse(listItem.Value, out counselorId) || counselorId != AccountInfo.UserId) continue;

                listItem.Selected = true;
                dropDownCounselor.Enabled = false;

                var descriptors = UserBLL.GetDescriptorsForUser(AccountInfo.UserId, AgencyId);

                foreach (var descriptor in descriptors)
                {
                    if (descriptor == (int)Descriptor.DataSubmitter || descriptor == (int)Descriptor.ShipDirector)
                        dropDownCounselor.Enabled = true;
                }
                
                SetCounselorDefaultCountyAndZip();

                return;
            }
        }

        protected void SetCounselorDefaultCountyAndZip()
        {
            var dropDownListCounselor = formViewAddClientContact.FindControl("dropDownListCounselor") as DropDownList;
            var counselorIdSelected = (!string.IsNullOrEmpty(dropDownListCounselor.SelectedValue))
                                          ? Convert.ToInt32(dropDownListCounselor.SelectedValue)
                                          : 0;

            if (counselorIdSelected == 0) return;

            var counselor = UserBLL.GetUserAccount(counselorIdSelected);

            if (counselor == null) return;

            string counselorLocation = string.Empty;

            var dropDownListCounselorCounty = formViewAddClientContact.FindControl("dropDownListCounselorCounty") as DropDownList;
            var dropDownListCounselorZipCode = formViewAddClientContact.FindControl("dropDownListCounselorZipCode") as DropDownList;

            var counties = new List<KeyValuePair<string, string>>(AgencyLogic.GetCounties(AgencyState.Code));
            counties.Insert(0, new KeyValuePair<string, string>("", "-- Select a County --"));
            dropDownListCounselorCounty.DataSource = counties;
            dropDownListCounselorCounty.DataBind();

            var localPath = System.Web.HttpContext.Current.Request.Url.LocalPath.ToLower();


            ListItem county  ;
            List<KeyValuePair<string, string>> zipCodes;

            //for the first time if the user comes using add-new show the county/zip that he has entered using viewdata.
            //Next time if he changes the counselor dropdown use counselor.county/zip
            if (localPath.Contains("forms/ccf/add-c-") && (_viewCCViewData != null))
            {
                county = dropDownListCounselorCounty.Items.FindByValue(_viewCCViewData.CounselorCountyCode);
                zipCodes = new List<KeyValuePair<string, string>>(LookupBLL.GetZipCodeForCountyFips2(_viewCCViewData.CounselorCountyCode));
                counselorLocation = _viewCCViewData.CounselorZIPCode;
            }
            else
            {
                county = dropDownListCounselorCounty.Items.FindByValue(counselor.CounselingCounty);
                zipCodes = new List<KeyValuePair<string, string>>(LookupBLL.GetZipCodeForCountyFips2(counselor.CounselingCounty));
                counselorLocation = counselor.CounselingLocation;

            }

            if (county != null)
                dropDownListCounselorCounty.SelectedValue = county.Value;


            zipCodes.Insert(0, new KeyValuePair<string, string>("", "-- Select a ZIP Code --"));
            dropDownListCounselorZipCode.DataSource = zipCodes;
            dropDownListCounselorZipCode.DataBind();

            var zipCode = dropDownListCounselorZipCode.Items.FindByText(counselorLocation);

            if (zipCode != null)
                dropDownListCounselorZipCode.SelectedValue = zipCode.Value;
        }

        protected void dropDownListCounselor_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCounselorDefaultCountyAndZip();
        }

        protected void dropDownListCounselorCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dropDownListCounselorZipCode = formViewAddClientContact.FindControl("dropDownListCounselorZipCode") as DropDownList;
            dropDownListCounselorZipCode.DataSource = LookupBLL.GetZipCodeForCountyFips(((DropDownList)sender).SelectedValue); 
            dropDownListCounselorZipCode.DataBind();
            dropDownListCounselorZipCode.Items.Insert(0, new ListItem("-- Select a ZIP Code --", ""));
            dropDownListCounselorZipCode.Focus();
        }

        protected void dataSourceAddClientContact_Inserting(object sender, ObjectContainerDataSourceInsertingEventArgs e)
        {
            int hoursSpent;
            int minutesSpent;
            var dropDownListCounselor = formViewAddClientContact.FindControl("dropDownListCounselor") as DropDownList;
            var dropDownClientCountyCode = formViewAddClientContact.FindControl("dropDownClientCountyCode") as DropDownList;
            var dropDownListCounselorCounty = formViewAddClientContact.FindControl("dropDownListCounselorCounty") as DropDownList;
            var dropDownListCounselorZipCode = formViewAddClientContact.FindControl("dropDownListCounselorZipCode") as DropDownList;
            var textBoxClientZIPCode = formViewAddClientContact.FindControl("textBoxClientZIPCode") as TextBox;

            e.NewValues[HoursSpentKey] =
                (int.TryParse(e.NewValues[HoursSpentKey].ToString().Replace("_", ""), out hoursSpent)) ? hoursSpent : 0;
            e.NewValues[MinutesSpentKey] =
                (int.TryParse(e.NewValues[MinutesSpentKey].ToString().Replace("_", ""), out minutesSpent)) ? minutesSpent : 0;

            e.NewValues["ClientPhoneNumber"] = e.NewValues["ClientPhoneNumber"].ToString().FormatPhoneNumber();

            e.NewValues[ClientLearnedAboutSHIPKey] = ((e.NewValues[ClientLearnedAboutSHIPKey].ToString() == string.Empty) ? null :
                ((ClientLearnedAboutSHIP?)(int)e.NewValues[ClientLearnedAboutSHIPKey]));
            e.NewValues[ClientAgeGroupKey] = ((e.NewValues[ClientAgeGroupKey].ToString() == string.Empty) ? null :
                ((ClientAgeGroup?)(int)e.NewValues[ClientAgeGroupKey]));
            e.NewValues[ClientGenderKey] = ((e.NewValues[ClientGenderKey].ToString() == string.Empty) ? null :
                ((ClientGender?)(int)e.NewValues[ClientGenderKey]));
            e.NewValues[ClientMonthlyIncomeKey] = ((e.NewValues[ClientMonthlyIncomeKey].ToString() == string.Empty) ? null :
                ((ClientMonthlyIncome?)(int)e.NewValues[ClientMonthlyIncomeKey]));
            e.NewValues[ClientAssetsKey] = ((e.NewValues[ClientAssetsKey].ToString() == string.Empty) ? null :
                ((ClientAssets?)(int)e.NewValues[ClientAssetsKey]));
            e.NewValues[ClientMethodOfContactKey] = ((e.NewValues[ClientMethodOfContactKey].ToString() == string.Empty) ? null :
                ((ClientMethodOfContact?)(int)e.NewValues[ClientMethodOfContactKey]));
            e.NewValues[ClientFirstVsContinuingContactKey] = ((e.NewValues[ClientFirstVsContinuingContactKey].ToString() == string.Empty) ? null :
                ((ClientFirstVsContinuingContact?)(int)e.NewValues[ClientFirstVsContinuingContactKey]));
            e.NewValues[ClientPrimaryLanguageOtherThanEnglishKey] = ((e.NewValues[ClientPrimaryLanguageOtherThanEnglishKey].ToString() == string.Empty) ? null :
                ((ClientPrimaryLanguageOtherThanEnglish?)(int)e.NewValues[ClientPrimaryLanguageOtherThanEnglishKey]));
            e.NewValues[ClientReceivingSSOrMedicareDisabilityKey] = ((e.NewValues[ClientReceivingSSOrMedicareDisabilityKey].ToString() == string.Empty) ? null :
                ((ClientReceivingSSOrMedicareDisability?)(int)e.NewValues[ClientReceivingSSOrMedicareDisabilityKey]));
            e.NewValues[ClientDualEligbleKey] = ((e.NewValues[ClientDualEligbleKey].ToString() == string.Empty) ? null :
                ((ClientDualEligble?)(int)e.NewValues[ClientDualEligbleKey]));
            e.NewValues[ClientStatusKey] = ((e.NewValues[ClientStatusKey].ToString() == string.Empty) ? null :
                ((ClientStatus?)(int)e.NewValues[ClientStatusKey]));

            e.NewValues[AutoAssignedClientIdKey] = AutoAssignedClientId;
            e.NewValues[AgencyIdKey] = AgencyId;
            e.NewValues[AgencyCodeKey] = AgencyCode;
            e.NewValues[AgencyStateKey] = AgencyState;
            e.NewValues[CounselorUserIdKey] = (string.IsNullOrEmpty(dropDownListCounselor.SelectedItem.Value)) ? null : dropDownListCounselor.SelectedItem.Value;
            e.NewValues[ClientCountyCodeKey] = (string.IsNullOrEmpty(dropDownClientCountyCode.SelectedItem.Value)) ? null : dropDownClientCountyCode.SelectedItem.Value;
            e.NewValues[CounselorCountyCodeKey] = dropDownListCounselorCounty.SelectedValue;
            e.NewValues[CounselorZIPCodeKey] = dropDownListCounselorZipCode.SelectedItem.Text; //text is the zipcode: value is CountyZIPID;
            e.NewValues[ClientZIPCodeKey] = textBoxClientZIPCode.Text;
        }

        protected void dataSourceAddClientContact_Inserted(object sender, ObjectContainerDataSourceStatusEventArgs e)
        {
            var viewData = (AddClientContactViewData) e.Instance;

            viewData.SetCreated(AccountInfo.UserId);
            RouteController.RouteTo(RouteController.CcfView(Logic.AddClientContact(viewData)));
        } 

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            Validate();

            //Updated on 06/10/2013 : Lavanya Maram
            if (ValidCMSSpecialUseFields())
            {
                formViewAddClientContact.InsertItem(true);
            }
              else
                  return;
        }

        //this method is not used
        protected void proxyValidatorPhoneNumber_ValueConvert(object sender, ValueConvertEventArgs e)
        {
            e.ConvertedValue = (!string.IsNullOrEmpty(e.ValueToConvert.ToString())) ? e.ValueConvertPhoneNumber() : null;
        }

        protected void proxyValidatorHoursAndMinutes_ValueConvert(object sender, ValueConvertEventArgs e)
        {
            int value;

            e.ConvertedValue = (int.TryParse(e.ValueToConvert.ToString().Replace("_", ""), out value)) ? value : 0;
        }

        protected void proxyValidatorCounselorAndCode_ValueConvert(object sender, ValueConvertEventArgs e)
        {
            var dropDownListCounselor = formViewAddClientContact.FindControl("dropDownListCounselor") as DropDownList;
            int counselorId;

            if (dropDownListCounselor != null && int.TryParse(dropDownListCounselor.SelectedValue, out counselorId))
                e.ConvertedValue = new KeyValuePair<int, string>(counselorId, e.ValueToConvert.ToString());
            else
                e.ConvertedValue = null;
        }

        protected void proxyValidatorPrescriptionDrugAssistance_ValueConvert(object sender, ValueConvertEventArgs e)
        {
            var checkBoxListMedicarePrescriptionDrugCoverageTopics = formViewAddClientContact.FindControl("checkBoxListMedicarePrescriptionDrugCoverageTopics") as ShiptalkWebControls.CheckBoxList;
            var checkBoxListPartDLowIncomeSubsidyTopics = formViewAddClientContact.FindControl("checkBoxListPartDLowIncomeSubsidyTopics") as ShiptalkWebControls.CheckBoxList;
            var checkBoxListOtherPrescriptionAssitanceTopics = formViewAddClientContact.FindControl("checkBoxListOtherPrescriptionAssitanceTopics") as ShiptalkWebControls.CheckBoxList;
            var checkBoxListMedicarePartsAandBTopics = formViewAddClientContact.FindControl("checkBoxListMedicarePartsAandBTopics") as ShiptalkWebControls.CheckBoxList;
            var checkBoxListMedicareAdvantageTopics = formViewAddClientContact.FindControl("checkBoxListMedicareAdvantageTopics") as ShiptalkWebControls.CheckBoxList;
            var checkBoxListMedicareSupplementTopics = formViewAddClientContact.FindControl("checkBoxListMedicareSupplementTopics") as ShiptalkWebControls.CheckBoxList;
            var checkBoxListMedicaidTopics = formViewAddClientContact.FindControl("checkBoxListMedicaidTopics") as ShiptalkWebControls.CheckBoxList;
            var checkBoxListOtherDrugTopics = formViewAddClientContact.FindControl("checkBoxListOtherDrugTopics") as ShiptalkWebControls.CheckBoxList;

            e.ConvertedValue = checkBoxListMedicarePrescriptionDrugCoverageTopics.SelectedItemCount
                             + checkBoxListPartDLowIncomeSubsidyTopics.SelectedItemCount
                             + checkBoxListOtherPrescriptionAssitanceTopics.SelectedItemCount
                             + checkBoxListMedicarePartsAandBTopics.SelectedItemCount
                             + checkBoxListMedicareAdvantageTopics.SelectedItemCount
                             + checkBoxListMedicareSupplementTopics.SelectedItemCount
                             + checkBoxListMedicaidTopics.SelectedItemCount
                             + checkBoxListOtherDrugTopics.SelectedItemCount;
        }

        protected void validatorTimeSpent_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var textBoxHoursSpent = formViewAddClientContact.FindControl("textBoxHoursSpent") as TextBox;
            var textBoxMinutesSpent = formViewAddClientContact.FindControl("textBoxMinutesSpent") as TextBox;

            // Pbattineni 09/26/2012  "CC" Should not Accept 0 or 0.0 in Hours and Minutes - Hours Plus Minutes Can't be Zero 

            //args.IsValid = !(string.IsNullOrEmpty(textBoxHoursSpent.Text) && string.IsNullOrEmpty(textBoxMinutesSpent.Text));

            args.IsValid = !(string.IsNullOrEmpty((textBoxHoursSpent.Text.Replace("0", string.Empty)).Replace("_", string.Empty))
                         && string.IsNullOrEmpty((textBoxMinutesSpent.Text.Replace("0", string.Empty)).Replace("_", string.Empty)));
            if(args.IsValid)
                args.IsValid = (!textBoxHoursSpent.Text.ToString().Contains(".") && !textBoxMinutesSpent.Text.ToString().Contains("."));
        }

        protected void validatorCheckIfOtherPrescriptionChecked_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var checkBoxListOtherPrescriptionAssitanceTopics = formViewAddClientContact.FindControl("checkBoxListOtherPrescriptionAssitanceTopics") as ShiptalkWebControls.CheckBoxList;
            var textBoxOtherPrescriptionAssitanceSpecified = formViewAddClientContact.FindControl("textBoxOtherPrescriptionAssitanceSpecified") as TextBox;


            if (checkBoxListOtherPrescriptionAssitanceTopics.Items[4].Selected == true)
            {
                if (textBoxOtherPrescriptionAssitanceSpecified.Text.Length > 0)
                    args.IsValid = true;
                else
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;

            }

        }

        protected void validatorCheckIfOtherPrescriptionText_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var checkBoxListOtherPrescriptionAssitanceTopics = formViewAddClientContact.FindControl("checkBoxListOtherPrescriptionAssitanceTopics") as ShiptalkWebControls.CheckBoxList;
            var textBoxOtherPrescriptionAssitanceSpecified = formViewAddClientContact.FindControl("textBoxOtherPrescriptionAssitanceSpecified") as TextBox;

            if (textBoxOtherPrescriptionAssitanceSpecified.Text.Length > 0)
            //check if the other checkbox is checked or not..
            {
                if (checkBoxListOtherPrescriptionAssitanceTopics.Items[4].Selected == true)
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
            else
            {
                args.IsValid = true;

            }

        }
      

        protected void validatorCheckIfNotCollectedChecked_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var checkBoxListClientRaceDescriptions = formViewAddClientContact.FindControl("checkBoxListClientRaceDescriptions") as ShiptalkWebControls.CheckBoxList;
        //    var textBoxOtherDrugTopicsSpecified = formViewAddClientContact.FindControl("textBoxOtherDrugTopicsSpecified") as TextBox;

            if (checkBoxListClientRaceDescriptions.Items[16].Selected == true)

            {
                if (checkBoxListClientRaceDescriptions.Items[0].Selected == true ||
                    checkBoxListClientRaceDescriptions.Items[1].Selected == true ||
                    checkBoxListClientRaceDescriptions.Items[2].Selected == true ||
                    checkBoxListClientRaceDescriptions.Items[3].Selected == true ||
                    checkBoxListClientRaceDescriptions.Items[4].Selected == true ||
                    checkBoxListClientRaceDescriptions.Items[5].Selected == true ||
                    checkBoxListClientRaceDescriptions.Items[6].Selected == true ||
                    checkBoxListClientRaceDescriptions.Items[7].Selected == true ||
                    checkBoxListClientRaceDescriptions.Items[8].Selected == true ||
                    checkBoxListClientRaceDescriptions.Items[9].Selected == true ||
                    checkBoxListClientRaceDescriptions.Items[10].Selected == true ||
                    checkBoxListClientRaceDescriptions.Items[11].Selected == true ||
                    checkBoxListClientRaceDescriptions.Items[12].Selected == true ||
                    checkBoxListClientRaceDescriptions.Items[13].Selected == true ||
                    checkBoxListClientRaceDescriptions.Items[14].Selected == true ||
                    checkBoxListClientRaceDescriptions.Items[15].Selected == true )

                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
            else
            {
                args.IsValid = true;

            }

        }


        protected void validatorCheckIfOtherDrugTopicsChecked_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var checkBoxListOtherDrugTopics = formViewAddClientContact.FindControl("checkBoxListOtherDrugTopics") as ShiptalkWebControls.CheckBoxList;
            var textBoxOtherDrugTopicsSpecified = formViewAddClientContact.FindControl("textBoxOtherDrugTopicsSpecified") as TextBox;

            if (checkBoxListOtherDrugTopics.Items[7].Selected == true)

            {
                if (textBoxOtherDrugTopicsSpecified.Text.Length > 0)

                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
            else
            {
                args.IsValid = true;

            }

        }

        protected void validatorCheckIfOtherDrugTopicsText_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var checkBoxListOtherDrugTopics = formViewAddClientContact.FindControl("checkBoxListOtherDrugTopics") as ShiptalkWebControls.CheckBoxList;
            var textBoxOtherDrugTopicsSpecified = formViewAddClientContact.FindControl("textBoxOtherDrugTopicsSpecified") as TextBox;

            if (textBoxOtherDrugTopicsSpecified.Text.Length > 0)
            //check if the other checkbox is checked or not..
            {
                if (checkBoxListOtherDrugTopics.Items[7].Selected == true)
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
            else
            {
                args.IsValid = true;

            }

        }

        protected void validatorCheckUpperBound_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var textBoxDateOfContact = formViewAddClientContact.FindControl("textBoxDateOfContact") as TextBox;

            DateTime _dateOfContact;
            if (DateTime.TryParse(textBoxDateOfContact.Text, out _dateOfContact) == false)
                _dateOfContact = new DateTime(1753, 1, 1);

            //Allow 1 calendar date in the future on cc date of contact and on the pam date of event only for GU [FIPS 66].
            if (DefaultState.Code == "66") _dateOfContact = _dateOfContact.AddDays(-1);

            if (_dateOfContact> DateTime.Now)
                args.IsValid = false;
            else
                args.IsValid = true;

        }
        protected void validatorCheckDuplicates_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var textBoxStateSpecificClientId = formViewAddClientContact.FindControl("textBoxStateSpecificClientId") as TextBox;
            var textBoxClientFirstName = formViewAddClientContact.FindControl("textBoxClientFirstName") as TextBox;
            var textBoxClientLastName = formViewAddClientContact.FindControl("textBoxClientLastName") as TextBox;
            var textBoxDateOfContact = formViewAddClientContact.FindControl("textBoxDateOfContact") as TextBox;

            DateTime _dateOfContact;
            if (DateTime.TryParse(textBoxDateOfContact.Text, out _dateOfContact) == false)
                _dateOfContact = new DateTime(1753, 1, 1);

            AutoAssignedClientId = AutoAssignedClientId;
            CCFBLL.DuplicateCheckType checkType;

            var dropDownListCounselor = formViewAddClientContact.FindControl("dropDownListCounselor") as DropDownList;
            var counselorIdSelected = (!string.IsNullOrEmpty(dropDownListCounselor.SelectedValue))
                                         ? Convert.ToInt32(dropDownListCounselor.SelectedValue)
                                         : 0;
            //This method is new.
            args.IsValid = !(Logic.IsDuplicateClientContact(((CcfAddType == AddType.Client) ? CCFBLL.DuplicateCheckType.NewClientContact : CCFBLL.DuplicateCheckType.AddNewClientContact)
                , AgencyId
                , (string.IsNullOrEmpty(textBoxStateSpecificClientId.Text.Trim()) ? string.Empty : textBoxStateSpecificClientId.Text)
                , AutoAssignedClientId
                , (string.IsNullOrEmpty(textBoxClientFirstName.Text.Trim()) ? string.Empty : textBoxClientFirstName.Text)
                , (string.IsNullOrEmpty(textBoxClientLastName.Text.Trim()) ? string.Empty : textBoxClientLastName.Text)
                , _dateOfContact
                , counselorIdSelected
                ));
        }

        protected void formViewAddclientContact_DataBound(object sender, EventArgs e)
        {
            if (_viewCCViewData == null || CcfAddType != AddType.Client) return;

            var textBoxStateSpecificClientId = formViewAddClientContact.FindControl("textBoxStateSpecificClientId") as TextBox;
            var literalAutoAssignedClientId = formViewAddClientContact.FindControl("literalAutoAssignedClientId") as Literal;
            var textBoxClientFirstName = formViewAddClientContact.FindControl("textBoxClientFirstName") as TextBox;
            var textBoxClientLastName = formViewAddClientContact.FindControl("textBoxClientLastName") as TextBox;
            var textBoxClientPhoneNumber = formViewAddClientContact.FindControl("textBoxClientPhoneNumber") as TextBox;
            var textBoxRepresentativeFirstName = formViewAddClientContact.FindControl("textBoxRepresentativeFirstName") as TextBox;
            var textBoxRepresentativeLastName = formViewAddClientContact.FindControl("textBoxRepresentativeLastName") as TextBox;
            var textBoxClientZIPCode = formViewAddClientContact.FindControl("textBoxClientZIPCode") as TextBox;
            //call the blur event if ness.
            var dropDownClientCountyCode = formViewAddClientContact.FindControl("dropDownClientCountyCode") as DropDownList;
            var dropDownListCounselor = formViewAddClientContact.FindControl("dropDownListCounselor") as DropDownList;
            //call select/
            var dropDownListCounselorCounty = formViewAddClientContact.FindControl("dropDownListCounselorCounty") as DropDownList;
            //call select.
            var dropDownListCounselorZipCode = formViewAddClientContact.FindControl("dropDownListCounselorZipCode") as DropDownList;
            var radioButtonListClientAgeGroup = formViewAddClientContact.FindControl("radioButtonListClientAgeGroup") as ShiptalkWebControls.RadioButtonList;
            var radioButtonListClientGender = formViewAddClientContact.FindControl("radioButtonListClientGender") as ShiptalkWebControls.RadioButtonList;
            var checkBoxListClientRaceDescriptions = formViewAddClientContact.FindControl("checkBoxListClientRaceDescriptions") as ShiptalkWebControls.CheckBoxList;
            var radioButtonListClientMonthlyIncome = formViewAddClientContact.FindControl("radioButtonListClientMonthlyIncome") as ShiptalkWebControls.RadioButtonList;
            var radioButtonListClientAssets = formViewAddClientContact.FindControl("radioButtonListClientAssets") as ShiptalkWebControls.RadioButtonList;
            var radioButtonListClientPrimaryLanguageOtherThanEnglish = formViewAddClientContact.FindControl("radioButtonListClientPrimaryLanguageOtherThanEnglish") as ShiptalkWebControls.RadioButtonList;
            var radioButtonListClientReceivingSSOrMedicareDisability = formViewAddClientContact.FindControl("radioButtonListClientReceivingSSOrMedicareDisability") as ShiptalkWebControls.RadioButtonList;
            var radioButtonListClientDualEligble = formViewAddClientContact.FindControl("radioButtonListClientDualEligble") as ShiptalkWebControls.RadioButtonList;

            textBoxStateSpecificClientId.Text = _viewCCViewData.StateSpecificClientId;

            RadioButton radioButtonUseStateSpecificClientId = formViewAddClientContact.FindControl("radioButtonUseStateSpecificClientId") as RadioButton;

            RadioButton radioButtonUseAutoClientId = formViewAddClientContact.FindControl("radioButtonUseAutoClientId") as RadioButton;

            if (_viewCCViewData.StateSpecificClientId.Length > 0)
            {
                radioButtonUseStateSpecificClientId.Checked = true;
                radioButtonUseAutoClientId.Checked = false;
            }

            AutoAssignedClientId = _viewCCViewData.AutoAssignedClientId;
            literalAutoAssignedClientId.Text = AutoAssignedClientId;
            textBoxClientFirstName.Text = _viewCCViewData.ClientFirstName;
            textBoxClientLastName.Text = _viewCCViewData.ClientLastName;
            textBoxClientPhoneNumber.Text = _viewCCViewData.ClientPhoneNumber;
            textBoxRepresentativeFirstName.Text = _viewCCViewData.RepresentativeFirstName;
            textBoxRepresentativeLastName.Text = _viewCCViewData.RepresentativeLastName;
            textBoxClientZIPCode.Text = _viewCCViewData.ClientZIPCode;
            textBoxClientZIPCode_Blur(textBoxClientZIPCode, EventArgs.Empty);
            dropDownClientCountyCode.SelectedValue = _viewCCViewData.ClientCountyCode;
            dropDownListCounselor.SelectedValue = _viewCCViewData.CounselorUserId.ToString();
            dropDownListCounselor_SelectedIndexChanged(dropDownListCounselor, EventArgs.Empty);
            dropDownListCounselorCounty.SelectedValue = _viewCCViewData.CounselorCountyCode;
            dropDownListCounselorCounty_SelectedIndexChanged(dropDownListCounselorCounty, EventArgs.Empty);
            var item = dropDownListCounselorZipCode.Items.FindByText(_viewCCViewData.CounselorZIPCode);
            if (item != null) item.Selected = true;
            radioButtonListClientAgeGroup.SelectedEnumValue = Convert.ToInt32(_viewCCViewData.ClientAgeGroup);
            radioButtonListClientGender.SelectedEnumValue = Convert.ToInt32(_viewCCViewData.ClientGender);
            checkBoxListClientRaceDescriptions.SelectedItems = _viewCCViewData.ClientRaceDescriptions;
            radioButtonListClientMonthlyIncome.SelectedEnumValue = Convert.ToInt32(_viewCCViewData.ClientMonthlyIncome);
            radioButtonListClientAssets.SelectedEnumValue = Convert.ToInt32(_viewCCViewData.ClientAssets);
            radioButtonListClientPrimaryLanguageOtherThanEnglish.SelectedEnumValue = Convert.ToInt32(_viewCCViewData.ClientPrimaryLanguageOtherThanEnglish);
            radioButtonListClientReceivingSSOrMedicareDisability.SelectedEnumValue = Convert.ToInt32(_viewCCViewData.ClientReceivingSSOrMedicareDisability);
            radioButtonListClientDualEligble.SelectedEnumValue = Convert.ToInt32(_viewCCViewData.ClientDualEligble);
        }

        #endregion

        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion

        #region Implementation of IAuthorize

        public bool IsAuthorized()
        {

            //if the user is not active in the agency do not allow him if he is using a new contact link..
            var localPath = System.Web.HttpContext.Current.Request.Url.LocalPath.ToLower();
            if (localPath.Contains("forms/ccf/add-c-") )
            {
                if (_viewCCViewData == null)
                    _viewCCViewData = Logic.GetViewClientContact(Id.GetValueOrDefault(0));
    

                if (!AgencyLogic.IsAgencyUserActive(_viewCCViewData.AgencyId, AccountInfo.UserId))
                    return false;
            }


            var descriptors = UserBLL.GetDescriptorsForUser(AccountInfo.UserId, null);

            foreach (var descriptor in descriptors)
            {
                if (descriptor == (int) Descriptor.Counselor
                    || descriptor == (int) Descriptor.DataSubmitter
                    || descriptor == (int) Descriptor.ShipDirector)
                    return true;
            }

            return false;
        }

        #endregion

        #region Sub Types

        public enum AddType
        {
            None,
            Agency,
            Client
        }

        #endregion

        protected void dropDownListCounselorCounty_DataBound(object sender, EventArgs e)
        {

        }
    }
}
