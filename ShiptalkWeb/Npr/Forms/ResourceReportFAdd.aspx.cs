using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkWeb;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration;
using Microsoft.Practices.Web.UI.WebControls;
using ShiptalkLogic.BusinessObjects.UI;
using System.Drawing;
using ShiptalkWeb.Routing;

namespace ShiptalkWeb.Npr.ResourceReportF
{
    public partial class ResourceReportFAdd : Page, IRouteDataPage, IAuthorize
    {

        private const string IdKey = "id";
        
        #region Event Handlers
        UserProfile prof = null;
        UserProfile Submitter = null;
        int _NewReportId = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            prof = UserBLL.GetUserProfile(AccountInfo.UserId);
            
            if (!IsPostBack) OnViewInitialized();
            OnViewLoaded();
        }

        #endregion


        #region Methods
        /// <summary>
        /// Called when page is initialized for the first time.
        /// </summary>
        protected void OnViewInitialized()
        {
     
           
            //Add the years to drop down list
            DropDownList cmbFrom = (DropDownList)formViewResourceReport.FindControl("cmbFromDate");
            DropDownList cmbTo = (DropDownList)formViewResourceReport.FindControl("cmbToDate");
            //We have 25 years we will permit.
            int YearMax = 25;
            int CurrentYear = 2010;
            for (int i = 0; i < YearMax; i++)
            {
                cmbFrom.Items.Add((CurrentYear + i).ToString());
                cmbTo.Items.Add((CurrentYear + i).ToString());
                if (Session["RR_SELECTED_YEAR"].ToString() == (CurrentYear + i).ToString())
                {
                    cmbFrom.SelectedIndex = i;
                }
            }
            cmbTo.SelectedIndex = cmbFrom.SelectedIndex + 1;
        }

        /// <summary>
        /// Called when page is loaded.
        /// </summary>
        protected void OnViewLoaded()
        {
            TextBox ctrlName = (TextBox)formViewResourceReport.FindControl("txtPersonCompletingReport");
            ctrlName.Text = prof.FirstName + " " + prof.LastName;

            TextBox ctrlPhone = (TextBox)formViewResourceReport.FindControl("txtTelephone");
            ctrlPhone.Text = prof.PrimaryPhone;


            
            TextBox ctrlStateCode = (TextBox)formViewResourceReport.FindControl("txtStateCode");
            AgencyBLL StatesInfo = new AgencyBLL();
            IEnumerable StateValues = StatesInfo.GetStates();
            foreach (System.Collections.Generic.KeyValuePair<string, string> StateValueFound in StateValues)
            {
                if (StateValueFound.Value == Session["RR_SELECTED_STATE"].ToString())
                {
                    ctrlStateCode.Text = StateValueFound.Key;
                    break;
                }
            }
            ctrlStateCode.Text = LookupBLL.GetStateFipsCodeByShortName(ctrlStateCode.Text);
            ctrlStateCode.Enabled = false;
            ctrlPhone.Enabled = false;


            
        }

        /// <summary>
        /// Binds the dependent data for the form.
        /// </summary>
        protected void BindDependentData()
        {
            if (DataView != null)
            {
                DataView.StateFIPSCode = AccountInfo.StateFIPS;
            }
            
        }
        #endregion

        #region Properties

        private bool IsFormValid
        { get; set; }
        /// <summary>
        /// Business Logic object
        /// </summary>
        private ResourceReportBLL _logic;
        public ResourceReportBLL Logic
        {
            get
            {
                if (_logic == null) _logic = new ResourceReportBLL();

                return _logic;
            }
        }

        /// <summary>
        /// View of Data
        /// </summary>
        ViewResourceReportViewData _View = null;
        public ViewResourceReportViewData DataView
        {
            get
            {
                if (_View == null)
                {
                    _View = Logic.GetViewResourceReport(ReportId.Value);
                    if ((AccountInfo.IsStateAdmin && (_View.StateFIPSCode == AccountInfo.StateFIPS)) || AccountInfo.IsAdmin)
                    {
                        return _View;
                    }
                    else
                    {
                        _View = null;
                    }
                    
                }
                return _View;

            }
        }

        private int? ReportId
        {
            get
            {
                // -1 was used to initialize the variable NewReportID - if -1 return null 
                //No report has been generated.
                if (_NewReportId == -1)
                {
                    return null;
                }
                else
                {
                    //Condtion Satisfied: Report has been generated along with ID
                    return _NewReportId;
                }
            }

        }


        #endregion



        #region Implementation of IRouteDataPage


        public RouteData RouteData { get; set; }
        public UserAccount AccountInfo { get; set; }


        #endregion


        #region Implementation of IAuthorize
        public bool IsAuthorized()
        {

            //Only CMS and State Admins are able to add a new resource report.
            return ((AccountInfo.Scope == Scope.CMS || AccountInfo.Scope == Scope.State) && AccountInfo.IsAdmin);


        }
        #endregion




        #region Events
        public void ValidateForm()
        {
            DropDownList cmbFrom = (DropDownList)formViewResourceReport.FindControl("cmbFromDate");
            DropDownList cmbTo = (DropDownList)formViewResourceReport.FindControl("cmbToDate");
            TextBox StateCode = (TextBox)formViewResourceReport.FindControl("txtStateCode");
            Label msg = (Label)formViewResourceReport.FindControl("MsgFeedBack");

           
            

            IsFormValid = true;
            TextBox TotalTotalCoun = (TextBox)formViewResourceReport.FindControl("txtTotalTotalCoun");

            TextBox YrsServiceLessThan1 = (TextBox)formViewResourceReport.FindControl("txtYrsServiceLessThan1");
            TextBox YrsService1To3 = (TextBox)formViewResourceReport.FindControl("txtYrsService1To3");
            TextBox YrsService3To5 = (TextBox)formViewResourceReport.FindControl("txtYrsService3To5");
            TextBox YrsServiceOver5 = (TextBox)formViewResourceReport.FindControl("txtYrsServiceOver5");
            TextBox YrsServiceNotCol = (TextBox)formViewResourceReport.FindControl("txtYrsServiceNotCol");

            int iYrsServiceLessThan1 = YrsServiceLessThan1.Text == "" ? 0 : int.Parse(YrsServiceLessThan1.Text);
            int iYrsService1To3 = YrsService1To3.Text == "" ? 0 : int.Parse(YrsService1To3.Text);
            int iYrsService3To5 = YrsService3To5.Text == "" ? 0 : int.Parse(YrsService3To5.Text);
            int iYrsServiceOver5 = YrsServiceOver5.Text == "" ? 0 : int.Parse(YrsServiceOver5.Text);
            int iYrsServiceNotCol = YrsServiceNotCol.Text == "" ? 0 : int.Parse(YrsServiceNotCol.Text);

         

            if ((iYrsServiceLessThan1 + iYrsService1To3 + iYrsService3To5 + iYrsServiceOver5 + iYrsServiceNotCol)
                != int.Parse(TotalTotalCoun.Text))
            {
                msg.Text = "Total counselors Years of services must be equal to counselors total.<br/><br/>";
                msg.ForeColor = Color.Red;
                msg.Visible = true;
                IsFormValid = false;
                return;
            }

            TextBox AgeLessThan65 = (TextBox)formViewResourceReport.FindControl("txtAgeLessThan65");
            TextBox AgeOver65 = (TextBox)formViewResourceReport.FindControl("txtAgeOver65");
            TextBox AgeNotCol = (TextBox)formViewResourceReport.FindControl("txtAgeNotCol");
            int iAgeLessThan65 = AgeLessThan65.Text == "" ? 0 : int.Parse(AgeLessThan65.Text);
            int iAgeOver65 = AgeOver65.Text == "" ? 0 : int.Parse(AgeOver65.Text);
            int iAgeNotCol = AgeNotCol.Text == "" ? 0 : int.Parse(AgeNotCol.Text);

            if ((iAgeLessThan65 + iAgeOver65 + iAgeNotCol)
               != int.Parse(TotalTotalCoun.Text))
            {
                msg.Text = "Total counselors Age must be equal to counselors total.<br/><br/>";
                msg.ForeColor = Color.Red;
                msg.Visible = true;
                IsFormValid = false;
                return;
            }


            TextBox GenderFemale = (TextBox)formViewResourceReport.FindControl("txtGenderFemale");
            TextBox GenderMale = (TextBox)formViewResourceReport.FindControl("txtGenderMale");
            TextBox GenderNotCol = (TextBox)formViewResourceReport.FindControl("txtGenderNotCol");
            int iGenderFemale = GenderFemale.Text == "" ? 0 : int.Parse(GenderFemale.Text);
            int iGenderMale = GenderMale.Text == "" ? 0 : int.Parse(GenderMale.Text);
            int iGenderNotCol = GenderNotCol.Text == "" ? 0 : int.Parse(GenderNotCol.Text);

            if ((iGenderFemale + iGenderMale + iGenderNotCol)
               != int.Parse(TotalTotalCoun.Text))
            {
                msg.Text = "Total counselors Gender must be equal to counselors total.<br/><br/>";
                msg.ForeColor = Color.Red;
                msg.Visible = true;
                IsFormValid = false;
                return;
            }


            TextBox RaceHispanicLatSpa = (TextBox)formViewResourceReport.FindControl("txtRaceHispanicLatSpa");
            TextBox RaceWhite = (TextBox)formViewResourceReport.FindControl("txtRaceWhite");
            TextBox RaceAfAm = (TextBox)formViewResourceReport.FindControl("txtRaceAfAm");
            TextBox RaceNative = (TextBox)formViewResourceReport.FindControl("txtRaceNative");
            TextBox RaceAsian = (TextBox)formViewResourceReport.FindControl("txtRaceAsian");
           
            TextBox RaceChinese = (TextBox)formViewResourceReport.FindControl("txtRaceChinese");
            TextBox RaceFilipino = (TextBox)formViewResourceReport.FindControl("txtRaceFilipino");
            TextBox RaceJapanese = (TextBox)formViewResourceReport.FindControl("txtRaceJapanese");
            TextBox RaceKorean = (TextBox)formViewResourceReport.FindControl("txtRaceKorean");
            TextBox RaceVietnamese = (TextBox)formViewResourceReport.FindControl("txtRaceVietnamese");

            TextBox RaceHawaiian = (TextBox)formViewResourceReport.FindControl("txtRaceHawaiian");
            TextBox RaceGuamanian = (TextBox)formViewResourceReport.FindControl("txtRaceGuamanian");
            TextBox VietnameseSamoan = (TextBox)formViewResourceReport.FindControl("txtVietnameseSamoan");
            TextBox VietnameseOtrAsian = (TextBox)formViewResourceReport.FindControl("txtVietnameseOtrAsian");
            TextBox RacePacIslander = (TextBox)formViewResourceReport.FindControl("txtRacePacIslander");


            TextBox RaceOtherRace = (TextBox)formViewResourceReport.FindControl("txtRaceOtherRace");
            TextBox RaceMoreThanOne = (TextBox)formViewResourceReport.FindControl("txtRaceMoreThanOne");
            TextBox RaceNotCol = (TextBox)formViewResourceReport.FindControl("txtRaceNotCol");


            int iRaceHispanicLatSpa = RaceHispanicLatSpa.Text == "" ? 0 : int.Parse(RaceHispanicLatSpa.Text);
            int iRaceWhite = RaceWhite.Text == "" ? 0 : int.Parse(RaceWhite.Text);
            int iRaceAfAm = RaceAfAm.Text == "" ? 0 : int.Parse(RaceAfAm.Text);
            int iRaceNative = RaceNative.Text == "" ? 0 : int.Parse(RaceNative.Text);
            int iRaceAsian = RaceAsian.Text == "" ? 0 : int.Parse(RaceAsian.Text);
            
            int iRaceChinese = RaceChinese.Text == "" ? 0 : int.Parse(RaceChinese.Text);

            int iRaceFilipino = RaceFilipino.Text == "" ? 0 : int.Parse(RaceFilipino.Text);
            int iRaceJapanese = RaceJapanese.Text == "" ? 0 : int.Parse(RaceJapanese.Text);
            int iRaceKorean = RaceKorean.Text == "" ? 0 : int.Parse(RaceKorean.Text);
            int iRaceVietnamese = RaceVietnamese.Text == "" ? 0 : int.Parse(RaceVietnamese.Text);
            int iRaceHawaiian = RaceHawaiian.Text == "" ? 0 : int.Parse(RaceHawaiian.Text);

            int iRaceGuamanian = RaceGuamanian.Text == "" ? 0 : int.Parse(RaceGuamanian.Text);
            int iVietnameseSamoan = VietnameseSamoan.Text == "" ? 0 : int.Parse(VietnameseSamoan.Text);
            int iVietnameseOtrAsian = VietnameseOtrAsian.Text == "" ? 0 : int.Parse(VietnameseOtrAsian.Text);
            int iRacePacIslander = RacePacIslander.Text == "" ? 0 : int.Parse(RacePacIslander.Text);
            int iRaceOtherRace = RaceOtherRace.Text == "" ? 0 : int.Parse(RaceOtherRace.Text);

            int iRaceMoreThanOne = RaceMoreThanOne.Text == "" ? 0 : int.Parse(RaceMoreThanOne.Text);
            int iRaceNotCol = RaceNotCol.Text == "" ? 0 : int.Parse(RaceNotCol.Text);

            if ((iRaceHispanicLatSpa + iRaceWhite + iRaceAfAm + iRaceNative + iRaceAsian + iRaceChinese + iRaceFilipino + iRaceJapanese +
                iRaceKorean + iRaceVietnamese + iRaceHawaiian + iRaceGuamanian + iVietnameseSamoan + iVietnameseOtrAsian + iRacePacIslander +
                iRaceOtherRace + iRaceMoreThanOne + iRaceNotCol)
                != int.Parse(TotalTotalCoun.Text))
            {
                msg.Text = "Total counselors Race must be equal to counselors total.<br/><br/>";
                msg.ForeColor = Color.Red;
                msg.Visible = true;
                IsFormValid = false;
                return;
            }


            TextBox DisabledTrue = (TextBox)formViewResourceReport.FindControl("txtDisabledTrue");
            TextBox DisabledFalse = (TextBox)formViewResourceReport.FindControl("txtDisabledFalse");
            TextBox CounselorDisabilityNotCollected = (TextBox)formViewResourceReport.FindControl("txtCounselorDisabilityNotCollected");
            int iDisabledTrue = DisabledTrue.Text == "" ? 0 : int.Parse(DisabledTrue.Text);
            int iDisabledFalse = DisabledFalse.Text == "" ? 0 : int.Parse(DisabledFalse.Text);
            int iCounselorDisabilityNotCollected = CounselorDisabilityNotCollected.Text == "" ? 0 : int.Parse(CounselorDisabilityNotCollected.Text);

            if ((iDisabledTrue + iDisabledFalse + iCounselorDisabilityNotCollected)
               != int.Parse(TotalTotalCoun.Text))
            {
                msg.Text = "Total counselors Disability must be equal to counselors total.<br/><br/>";
                msg.ForeColor = Color.Red;
                msg.Visible = true;
                IsFormValid = false;
                return;
            }

            TextBox txtLangOther = (TextBox)formViewResourceReport.FindControl("txtLangOther");
            TextBox txtLangEnglish = (TextBox)formViewResourceReport.FindControl("txtLangEnglish");
            TextBox txtLangNotCol = (TextBox)formViewResourceReport.FindControl("txtLangNotCol");
            int iLangOther = txtLangOther.Text == "" ? 0 : int.Parse(txtLangOther.Text);
            int iLangEnglish = txtLangEnglish.Text == "" ? 0 : int.Parse(txtLangEnglish.Text);
            int iLangNotCol = txtLangNotCol.Text == "" ? 0 : int.Parse(txtLangNotCol.Text);

            if ((iLangOther + iLangEnglish + iLangNotCol)
              != int.Parse(TotalTotalCoun.Text))
            {
                msg.Text = "Total counselors Counselor Speaks Another Language must be equal to counselors total.<br/><br/>";
                msg.ForeColor = Color.Red;
                msg.Visible = true;
                IsFormValid = false;
                return;
            }

            TextBox UpdtTrainingsAttend = (TextBox)formViewResourceReport.FindControl("txtUpdtTrainingsAttend");

            int iUpdtTrainingsAttend = UpdtTrainingsAttend.Text == "" ? 0 : int.Parse(UpdtTrainingsAttend.Text);

            if (iUpdtTrainingsAttend > int.Parse(TotalTotalCoun.Text))
            {
                msg.Text = "Total counselors attending update training must be equal to or less than counselors total.<br/><br/>";
                msg.ForeColor = Color.Red;
                msg.Visible = true;
                IsFormValid = false;
                return;
            }



            

            if (int.Parse(cmbFrom.SelectedItem.Text) >= int.Parse(cmbTo.SelectedItem.Text))
            {
                msg.Text = "To date must be greater than from date.<br/><br/>";
                msg.ForeColor = Color.Red;
                msg.Visible = true;
                IsFormValid = false;
                return;
            }
            
            //See if a report already exists for the given year.
            if (Logic.ReportExists(cmbFrom.Text, StateCode.Text))
            {
                msg.Text = "Resource report has been previously created cannot create new report.<br/><br/>";
                msg.ForeColor = Color.Red;
                msg.Visible = true;
                IsFormValid = false;
            }
            else
            {
                IsFormValid = true;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
                ValidateForm();
                if (IsFormValid)
                {
                    formViewResourceReport.ChangeMode(FormViewMode.Insert);
                    formViewResourceReport.InsertItem(false);
                    RouteController.RouteTo(RouteController.ResourceReportView(ReportId.Value));
                }
                
            
        }

        protected void proxyValidatorPhoneNumber_ValueConvert(object sender, ValueConvertEventArgs e)
        {
            e.ConvertedValue = (!string.IsNullOrEmpty(e.ValueToConvert.ToString())) ? e.ValueConvertPhoneNumber() : null;
        }

        
        protected void dataSourceViewResourceReport_Inserted(object sender, ObjectContainerDataSourceStatusEventArgs e)
        {
            
            ViewResourceReportViewData RptData = (ViewResourceReportViewData)e.Instance;
            RptData.SubmitterID = AccountInfo.UserId.ToString();
            _NewReportId = Logic.AddReport(RptData);
        }

        protected void dataSourceViewResourceReport_Inserting(object sender, ObjectContainerDataSourceInsertingEventArgs e)
        {
            //Check for blank values and set the value to 0;
            IDictionaryEnumerator myEnumerator =
                e.NewValues.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                if (string.IsNullOrEmpty(myEnumerator.Value.ToString()))
                {
                    e.NewValues[myEnumerator.Key] = "0";
                }

            }
            e.NewValues["LastUpdatedBy"] = AccountInfo.UserId;
            e.NewValues["StateFIPSCode"] = ((TextBox)formViewResourceReport.FindControl("txtStateCode")).Text;
           

            DropDownList cmbFrom = (DropDownList)formViewResourceReport.FindControl("cmbFromDate");
            DropDownList cmbTo = (DropDownList)formViewResourceReport.FindControl("cmbToDate");

            e.NewValues["RepYrFrom"] = "04/01" + "/" + cmbFrom.SelectedItem.Text;
            e.NewValues["RepYrTo"] = "03/31" + "/" + cmbTo.SelectedItem.Text;
            e.NewValues["LastUpdatedBy"] = AccountInfo.UserId;
            //txtPersonCompletingReport

        }
        #endregion
    }
}
