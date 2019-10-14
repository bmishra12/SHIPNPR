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
    public partial class CCFEdit : Page, IRouteDataPage, IAuthorize
    {
        private const string IdKey = "Id";
        private const string DefaultStateKey = "DefaultState";
        private const string IsAdminKey = "IsAdmin";
        private const string HoursSpentKey = "HoursSpent";
        private const string MinutesSpentKey = "MinutesSpent";
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
        private const string ClientZIPCodeKey = "ClientZIPCode";
        private const string ClientCountyCodeKey = "ClientCountyCode";
        private const string CounselorCountyCodeKey = "CounselorCountyCode";
        private const string CounselorZIPCodeKey = "CounselorZIPCode";
        private const string UserIdKey = "UserId";
        private const string CounselorUserIdKey = "CounselorUserId";
        private const string ReviewerUserIdKey = "ReviewerUserId";

        private CCFBLL _logic;
        private AgencyBLL _agencyLogic;
        private EditClientContactViewData _viewData;
        
        #region Properties

        public State DefaultState { get { return (State)ViewState[DefaultStateKey]; } set { ViewState[DefaultStateKey] = value; } }

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
                if (_logic == null) _logic = new CCFBLL();

                return _logic;
            }
        }

        private EditClientContactViewData ViewData
        {
            get
            {
                if (_viewData == null)
                    _viewData = Logic.GetEditClientContact(Id.GetValueOrDefault(0));

                return _viewData;
            }
        }

        public int? ReviewerUserId { get { return (ViewState[ReviewerUserIdKey] != null) ? (int?)Convert.ToInt32(ViewState[ReviewerUserIdKey]) : null; } set { ViewState[ReviewerUserIdKey] = value; } }

        public IEnumerable<KeyValuePair<int, string>> Counselors { get; set; }
        public IEnumerable<KeyValuePair<string, string>> ClientCounties { get; set; }
        public IEnumerable<KeyValuePair<string, string>> CounselorCounties { get; set; }
        public IEnumerable<KeyValuePair<string, string>> CounselorZipCodes { get; set; }

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

        public IEnumerable<SpecialField> CMSSpecialFields { get; set; }

        public IEnumerable<SpecialField> StateSpecialFields { get; set; }

        #endregion

        #region Methods

        // <summary>
        /// Called when page is initialized for the first time.
        /// </summary>
        protected void OnViewInitialized()
        {
            DefaultState = new State(AccountInfo.StateFIPS);
            IsAdmin = AccountInfo.IsAdmin;
            BindFormData();
        }

        /// <summary>
        /// Called when the page is loaded.
        /// </summary>
        protected void OnViewLoaded()
        {
        }

        protected void BindFormData()
        {
            //OLD logic
            //Counselors = AgencyLogic.GetAgencyUsers(ViewData.AgencyId, Descriptor.Counselor, true).OrderBy(p => p.Value);

            Counselors = GetCounselors(ViewData.AgencyId);

            CMSSpecialFields = Logic.GetCMSSpecialFields();
            StateSpecialFields = Logic.GetStateSpecialFields(ViewData.AgencyState);
            CounselorCounties = AgencyLogic.GetCounties(ViewData.AgencyState.Code);
            ClientCounties = LookupBLL.GetCountiesForZipCode(ViewData.ClientZIPCode);
            CounselorCounties = CounselorCounties;
            CounselorZipCodes = LookupBLL.GetZipCodeForCountyFips2(ViewData.CounselorCountyCode);
        }


        private IEnumerable<KeyValuePair<int, string>> GetCounselors(int AgencyId)
        {
            //Populate Counselors from DB
            CCFBLL.CCCounselorsFilterCriteria criteria = new CCFBLL.CCCounselorsFilterCriteria();
            criteria.StateFIPS = ViewData.AgencyState.Code;
            criteria.scope = this.AccountInfo.Scope;
            criteria.UserId = this.AccountInfo.UserId;
            criteria.AgencyId = AgencyId;
            return Logic.GetClientContactCounselorsForCCForm(criteria, false);
        }

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) OnViewInitialized();

            OnViewLoaded();
        }

        protected void formViewEditClientContact_DataBound(object sender, EventArgs e)
        {
            var dropDownClientCountyCode = formViewEditClientContact.FindControl("dropDownClientCountyCode") as DropDownList;
            dropDownClientCountyCode.DataSource = ClientCounties;
            dropDownClientCountyCode.DataBind();
            var clientCounty = dropDownClientCountyCode.Items.FindByValue(ViewData.ClientCountyCode);
            
            if (clientCounty != null)
                clientCounty.Selected = true;

            var dropDownListCounselorCounty = formViewEditClientContact.FindControl("dropDownListCounselorCounty") as DropDownList;
            dropDownListCounselorCounty.DataSource = CounselorCounties;
            dropDownListCounselorCounty.DataBind();
            dropDownListCounselorCounty.Items.Insert(0, new ListItem("-- Select a County --", ""));
            var county = dropDownListCounselorCounty.Items.FindByValue(ViewData.CounselorCountyCode);

            if (county != null)
                county.Selected = true;

            var dropDownListCounselorZipCode = formViewEditClientContact.FindControl("dropDownListCounselorZipCode") as DropDownList;
            dropDownListCounselorZipCode.DataSource = CounselorZipCodes;
            dropDownListCounselorZipCode.DataBind();
            dropDownListCounselorZipCode.Items.Insert(0, new ListItem("-- Select a ZIP Code --", ""));

            var zip = dropDownListCounselorZipCode.Items.FindByText(ViewData.CounselorZIPCode);

            if (zip != null)
                zip.Selected = true;
        }

        protected void textBoxClientZIPCode_TextChanged(object sender, EventArgs e)
        {
            var textBoxClientAIPCode = (TextBox)sender;
            var proxyValidatorClientZIPCode = formViewEditClientContact.FindControl("proxyValidatorClientZIPCode") as PropertyProxyValidator;
            var dropDownClientCountyCode = formViewEditClientContact.FindControl("dropDownClientCountyCode") as DropDownList;

            proxyValidatorClientZIPCode.Validate();
            if (!proxyValidatorClientZIPCode.IsValid) return;

            dropDownClientCountyCode.DataSource = LookupBLL.GetCountiesForZipCode(textBoxClientAIPCode.Text);
            dropDownClientCountyCode.DataBind();

            //if there are more than one value make the 1st item as selected item..
            //As the counties are orderd by zipCounty.displayorder
            if (dropDownClientCountyCode.Items.Count > 1)
                dropDownClientCountyCode.SelectedIndex = 0;

            dropDownClientCountyCode.Focus();
        }

        protected void dropDownListCounselor_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dropDownListCounselor = (DropDownList)sender;
            var counselorIdSelected = (!string.IsNullOrEmpty(dropDownListCounselor.SelectedValue))
                                          ? Convert.ToInt32(dropDownListCounselor.SelectedValue)
                                          : 0;

            if (counselorIdSelected == 0) return;

            var counselor = UserBLL.GetUserAccount(counselorIdSelected);

            if (counselor == null) return;

            var dropDownListCounselorCounty = formViewEditClientContact.FindControl("dropDownListCounselorCounty") as DropDownList;
            dropDownListCounselorCounty.DataSource = AgencyLogic.GetCounties(ViewData.AgencyState.Code);
            dropDownListCounselorCounty.DataBind();
            dropDownListCounselorCounty.Items.Insert(0, new ListItem("-- Select a County --", ""));

            var county = dropDownListCounselorCounty.Items.FindByValue(counselor.CounselingCounty);

            if (county != null)
                county.Selected = true;

            var dropDownListCounselorZipCode = formViewEditClientContact.FindControl("dropDownListCounselorZipCode") as DropDownList;
            dropDownListCounselorZipCode.DataSource = LookupBLL.GetZipCodeForCountyFips(counselor.CounselingCounty);
            dropDownListCounselorZipCode.DataBind();
            dropDownListCounselorZipCode.Items.Insert(0, new ListItem("-- Select a Zip Code --", ""));

            var zip = dropDownListCounselorZipCode.Items.FindByText(counselor.CounselingLocation);

            if (zip != null)
                zip.Selected = true;

            dropDownListCounselorCounty.Focus();
        }

        //this method is not used:
        protected void proxyValidatorPhoneNumber_ValueConvert(object sender, ValueConvertEventArgs e)
        {
            e.ConvertedValue = (!string.IsNullOrEmpty(e.ValueToConvert.ToString())) ? e.ValueConvertPhoneNumber() : null;
        }

        protected void proxyValidatorHoursAndMinutes_ValueConvert(object sender, ValueConvertEventArgs e)
        {
            int value;

            e.ConvertedValue = (int.TryParse(e.ValueToConvert.ToString().Replace("_", ""), out value)) ? value : 0;
        }


        protected void validatorCheckIfOtherPrescriptionChecked_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var checkBoxListOtherPrescriptionAssitanceTopics = formViewEditClientContact.FindControl("checkBoxListOtherPrescriptionAssitanceTopics") as ShiptalkWebControls.CheckBoxList;
            var textBoxOtherPrescriptionAssitanceSpecified = formViewEditClientContact.FindControl("textBoxOtherPrescriptionAssitanceSpecified") as TextBox;


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

        protected void validatorCheckIfNotCollectedChecked_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var checkBoxListClientRaceDescriptions = formViewEditClientContact.FindControl("checkBoxListClientRaceDescriptions") as ShiptalkWebControls.CheckBoxList;
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
                    checkBoxListClientRaceDescriptions.Items[15].Selected == true)

                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
            else
            {
                args.IsValid = true;

            }

        }


        protected void validatorCheckIfOtherPrescriptionText_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var checkBoxListOtherPrescriptionAssitanceTopics = formViewEditClientContact.FindControl("checkBoxListOtherPrescriptionAssitanceTopics") as ShiptalkWebControls.CheckBoxList;
            var textBoxOtherPrescriptionAssitanceSpecified = formViewEditClientContact.FindControl("textBoxOtherPrescriptionAssitanceSpecified") as TextBox;

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

        protected void validatorCheckIfOtherDrugTopicsChecked_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var checkBoxListOtherDrugTopics = formViewEditClientContact.FindControl("checkBoxListOtherDrugTopics") as ShiptalkWebControls.CheckBoxList;
            var textBoxOtherDrugTopicsSpecified = formViewEditClientContact.FindControl("textBoxOtherDrugTopicsSpecified") as TextBox;

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
            var checkBoxListOtherDrugTopics = formViewEditClientContact.FindControl("checkBoxListOtherDrugTopics") as ShiptalkWebControls.CheckBoxList;
            var textBoxOtherDrugTopicsSpecified = formViewEditClientContact.FindControl("textBoxOtherDrugTopicsSpecified") as TextBox;

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
            var textBoxDateOfContact = formViewEditClientContact.FindControl("textBoxDateOfContact") as TextBox;

            DateTime _dateOfContact;
            if (DateTime.TryParse(textBoxDateOfContact.Text, out _dateOfContact) == false)
                _dateOfContact = new DateTime(1753, 1, 1);

            //Allow 1 calendar date in the future on cc date of contact and on the pam date of event only for GU [FIPS 66].
            if (DefaultState.Code == "66") _dateOfContact = _dateOfContact.AddDays(-1);

            if (_dateOfContact > DateTime.Now)
                args.IsValid = false;
            else
                args.IsValid = true;

        }


        protected void validatorTimeSpent_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var textBoxHoursSpent = formViewEditClientContact.FindControl("textBoxHoursSpent") as TextBox;
            var textBoxMinutesSpent = formViewEditClientContact.FindControl("textBoxMinutesSpent") as TextBox;

            // Pbattineni 09/26/2012  "CC" Should not Accept 0 or 0.0 in Hours and Minutes - Hours Plus Minutes Can't be Zero 

            //args.IsValid = !(string.IsNullOrEmpty(textBoxHoursSpent.Text) && string.IsNullOrEmpty(textBoxMinutesSpent.Text));

            args.IsValid = !(string.IsNullOrEmpty((textBoxHoursSpent.Text.Replace("0", string.Empty)).Replace("_", string.Empty))
                        && string.IsNullOrEmpty((textBoxMinutesSpent.Text.Replace("0", string.Empty)).Replace("_", string.Empty)));
            if (args.IsValid)
                args.IsValid = (!textBoxHoursSpent.Text.ToString().Contains(".") && !textBoxMinutesSpent.Text.ToString().Contains("."));
        }

        protected void proxyValidatorPrescriptionDrugAssistance_ValueConvert(object sender, ValueConvertEventArgs e)
        {
            var checkBoxListMedicarePrescriptionDrugCoverageTopics = formViewEditClientContact.FindControl("checkBoxListMedicarePrescriptionDrugCoverageTopics") as ShiptalkWebControls.CheckBoxList;
            var checkBoxListPartDLowIncomeSubsidyTopics = formViewEditClientContact.FindControl("checkBoxListPartDLowIncomeSubsidyTopics") as ShiptalkWebControls.CheckBoxList;
            var checkBoxListOtherPrescriptionAssitanceTopics = formViewEditClientContact.FindControl("checkBoxListOtherPrescriptionAssitanceTopics") as ShiptalkWebControls.CheckBoxList;
            var checkBoxListMedicarePartsAandBTopics = formViewEditClientContact.FindControl("checkBoxListMedicarePartsAandBTopics") as ShiptalkWebControls.CheckBoxList;
            var checkBoxListMedicareAdvantageTopics = formViewEditClientContact.FindControl("checkBoxListMedicareAdvantageTopics") as ShiptalkWebControls.CheckBoxList;
            var checkBoxListMedicareSupplementTopics = formViewEditClientContact.FindControl("checkBoxListMedicareSupplementTopics") as ShiptalkWebControls.CheckBoxList;
            var checkBoxListMedicaidTopics = formViewEditClientContact.FindControl("checkBoxListMedicaidTopics") as ShiptalkWebControls.CheckBoxList;
            var checkBoxListOtherDrugTopics = formViewEditClientContact.FindControl("checkBoxListOtherDrugTopics") as ShiptalkWebControls.CheckBoxList;

            e.ConvertedValue = checkBoxListMedicarePrescriptionDrugCoverageTopics.SelectedItemCount
                             + checkBoxListPartDLowIncomeSubsidyTopics.SelectedItemCount
                             + checkBoxListOtherPrescriptionAssitanceTopics.SelectedItemCount
                             + checkBoxListMedicarePartsAandBTopics.SelectedItemCount
                             + checkBoxListMedicareAdvantageTopics.SelectedItemCount
                             + checkBoxListMedicareSupplementTopics.SelectedItemCount
                             + checkBoxListMedicaidTopics.SelectedItemCount
                             + checkBoxListOtherDrugTopics.SelectedItemCount;
        }        

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            Validate();

            //Updated on 06/10/2013 : Lavanya Maram
            if (ValidCMSSpecialUseFields())
            {
                formViewEditClientContact.UpdateItem(true);
            }
            else
                return;
        }

        protected void dataSourceEditClientContact_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            dataSourceEditClientContact.DataSource = ViewData;
        }

        protected void dataSourceEditClientContact_Updating(object sender, ObjectContainerDataSourceUpdatingEventArgs e)
        {
            int hoursSpent;
            int minutesSpent;
            var textBoxClientZIPCode = formViewEditClientContact.FindControl("textBoxClientZIPCode") as TextBox;
            var dropDownListCounselor = formViewEditClientContact.FindControl("dropDownListCounselor") as DropDownList;
            var dropDownClientCountyCode = formViewEditClientContact.FindControl("dropDownClientCountyCode") as DropDownList;
            var dropDownListCounselorCounty = formViewEditClientContact.FindControl("dropDownListCounselorCounty") as DropDownList;
            var dropDownListCounselorZipCode = formViewEditClientContact.FindControl("dropDownListCounselorZipCode") as DropDownList;

            e.NewValues[UserIdKey] = AccountInfo.UserId;
            e.NewValues[HoursSpentKey] =
                (int.TryParse(e.NewValues[HoursSpentKey].ToString().Replace("_", ""), out hoursSpent)) ? hoursSpent : 0;
            e.NewValues[MinutesSpentKey] =
                (int.TryParse(e.NewValues[MinutesSpentKey].ToString().Replace("_", ""), out minutesSpent)) ? minutesSpent : 0;

            e.NewValues["ClientPhoneNumber"] = e.NewValues["ClientPhoneNumber"].ToString().FormatPhoneNumber();

            e.NewValues[ClientLearnedAboutSHIPKey] = ((e.NewValues[ClientLearnedAboutSHIPKey].ToString() == string.Empty) ? null :
                ((ClientLearnedAboutSHIP?)(int) e.NewValues[ClientLearnedAboutSHIPKey]));
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

            e.NewValues[CounselorUserIdKey] = dropDownListCounselor.SelectedValue;
            e.NewValues[ClientZIPCodeKey] = textBoxClientZIPCode.Text;
            e.NewValues[ClientCountyCodeKey] = dropDownClientCountyCode.SelectedItem.Value;
            e.NewValues[CounselorCountyCodeKey] = dropDownListCounselorCounty.SelectedValue;
            e.NewValues[CounselorZIPCodeKey] = dropDownListCounselorZipCode.SelectedItem.Text;
            e.NewValues[ReviewerUserIdKey] = ReviewerUserId;
        }

        protected void dataSourceEditClientContact_Updated(object sender, ObjectContainerDataSourceStatusEventArgs e)
        {
            var viewData = (EditClientContactViewData)e.Instance;
            viewData.SetLastUpdated(AccountInfo.UserId);
            Logic.UpdateClientContact(viewData);
            RouteController.RouteTo(RouteController.CcfView(Id.GetValueOrDefault(0)));
        }

        public override void Validate()
        {
            var radioButtonUseStateSpecificClientId =
                           formViewEditClientContact.FindControl("radioButtonUseStateSpecificClientId") as RadioButton;
            var dropDownListCounselorCounty = (DropDownList)formViewEditClientContact.FindControl("dropDownListCounselorCounty");
            var dropDownListCounselorZipCode = (DropDownList)formViewEditClientContact.FindControl("dropDownListCounselorZipCode");
            var proxyValidatorStateSpecificClientId =
                formViewEditClientContact.FindControl("proxyValidatorStateSpecificClientId") as PropertyProxyValidator;
            var proxyValidatorCounselorCounty =
                formViewEditClientContact.FindControl("proxyValidatorCounselorCounty") as PropertyProxyValidator;
            var proxyValidatorCounselorZipCode =
                formViewEditClientContact.FindControl("proxyValidatorCounselorZipCode") as PropertyProxyValidator;

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

            var specialFieldListCMS = formViewEditClientContact.FindControl("specialFieldListCMS") as ShiptalkWebControls.SpecialFieldList;
            Label ErrSpecialFieldListCMSmsg = formViewEditClientContact.FindControl("ErrspecialFieldListCMSmsg") as Label;
          
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

        protected void dropDownListCounselorCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dropDownListCounselorZipCode = formViewEditClientContact.FindControl("dropDownListCounselorZipCode") as DropDownList;
            dropDownListCounselorZipCode.DataSource = LookupBLL.GetZipCodeForCountyFips(((DropDownList)sender).SelectedValue);
            dropDownListCounselorZipCode.DataBind();
            dropDownListCounselorZipCode.Items.Insert(0, new ListItem("-- Select a Zip Code --", ""));
            dropDownListCounselorZipCode.Focus();
        }

        #endregion

        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion

        #region Implementation of IAuthorize

        public bool IsAuthorized()
        {
            if (!AgencyLogic.IsAgencyUserActive(ViewData.AgencyId, AccountInfo.UserId))
                return false;

            ReviewerUserId = (Logic.IsUserClientContactReviewer(ViewData.Id, AccountInfo.UserId))
                                 ? (int?)AccountInfo.UserId
                                 : null; 

            if (ViewData.SubmitterUserId == AccountInfo.UserId || ViewData.CounselorUserId == AccountInfo.UserId)
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
