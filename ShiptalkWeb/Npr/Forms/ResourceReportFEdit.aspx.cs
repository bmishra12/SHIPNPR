using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.Routing;
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

namespace ShiptalkWeb.Npr.Forms
{
    public partial class ResourceReportFEdit : Page, IRouteDataPage, IAuthorize
    {
        UserProfile prof = null;
        UserProfile Submitter = null;
        private const string IdKey = "Id";
        #region Event Handlers
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
            dataSourceViewResourceReport.DataSource = DataView;
            dataSourceViewResourceReport.DataBind();
            formViewResourceReport.DataBind();
            

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
            }

            SetDropDownYearValues(cmbFrom, true);
            SetDropDownYearValues(cmbTo,false);
        }

        private void SetDropDownYearValues(DropDownList ctrlDropDown, bool IsFromYear)
        {
            //Set the From and To date.
            char[] sep = { '/' };
            
            //Get the from year value 
            string[] YearValue = DataView.RepYrFrom.Split(sep);

            //Determine if we really want the to year value.
            if(!IsFromYear)
                //Get the value for the To Year
                YearValue = DataView.RepYrTo.Split(sep);
            ListItem ddlYear = ctrlDropDown.Items.FindByText("20" + YearValue[2]);
            if (ddlYear == null)
            {
                Label FoundErrMessageLbl = (Label)formViewResourceReport.FindControl("lblErrMessage");
                FoundErrMessageLbl.Text = "Application Error: - Report year not found in dropdown = " + "20" + YearValue[2];
                FoundErrMessageLbl.ForeColor = Color.Red;
                FoundErrMessageLbl.Visible = true;
                Button btnDisabled = (Button)formViewResourceReport.FindControl("btnSave");
                btnDisabled.Enabled = false;
                return;
            }
            else
            {
                ddlYear.Selected = true;
            }

        }

        /// <summary>
        /// Called when page is loaded.
        /// </summary>
        protected void OnViewLoaded()
        {
            Submitter = UserBLL.GetUserProfile(int.Parse(DataView.SubmitterID.ToString()));
            TextBox ctrlName =  (TextBox)formViewResourceReport.FindControl("txtPersonCompletingReport");
            UserProfile prof = UserBLL.GetUserProfile(AccountInfo.UserId);
            ctrlName.Text = prof.FirstName + " " + prof.LastName;
            ctrlName.Enabled = false;

            TextBox ctrlPhone = (TextBox)formViewResourceReport.FindControl("txtTelephone");
            ctrlPhone.Text = prof.PrimaryPhone;
            ctrlPhone.Enabled = false;

            TextBox ctrlStateCode = (TextBox)formViewResourceReport.FindControl("txtStateCode");
            
            ctrlStateCode.Enabled = false;
            ctrlPhone.Enabled = false;

        }

        /// <summary>
        /// Binds the dependent data for the form.
        /// </summary>
        protected void BindDependentData()
        {
                string FullName = Submitter.FirstName + " " + Submitter.LastName;
                DataView.PersonCompletingReportName = FullName;
                DataView.PersonCompletingReportTel = Submitter.PrimaryPhone;
                DataView.PersonCompletingReportTitle = FullName;
           
        }


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
            else
            {
                IsFormValid = true;
            }
        }
        #endregion

        #region Properties
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
                        if (string.IsNullOrEmpty(_View.StateGranteeName) || _View.StateGranteeName == "0")
                        {
                            _View.StateGranteeName = string.Empty;
                        }
                        if (string.IsNullOrEmpty(_View.Title) || _View.Title == "0") 
                        {
                            _View.Title = string.Empty;
                        }
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
                if (RouteData.Values[IdKey] == null) return null;
               
                int ResourceReportId = 0;
                int.TryParse(RouteData.Values[IdKey].ToString(), out ResourceReportId);
                return ResourceReportId;
                
            }

        }

        private bool IsFormValid
        { get; set; }
        


        #endregion


     

        #region events

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ValidateForm();
            if (IsFormValid)
            {

                formViewResourceReport.ChangeMode(FormViewMode.Insert);
                formViewResourceReport.InsertItem(false);
                RouteController.RouteTo(RouteController.ResourceReportView(Id.Value));
            }
            
        }

        protected void proxyValidatorPhoneNumber_ValueConvert(object sender, ValueConvertEventArgs e)
        {
            e.ConvertedValue = (!string.IsNullOrEmpty(e.ValueToConvert.ToString())) ? e.ValueConvertPhoneNumber() : null;
        }


        protected void dataSourceViewResourceReport_Inserted(object sender, ObjectContainerDataSourceStatusEventArgs e)
        {

            ViewResourceReportViewData RptData = (ViewResourceReportViewData)e.Instance;
            RptData.ResourceReportId = ReportId.ToString();
            RptData.SubmitterID = AccountInfo.UserId.ToString();
            Logic.UpdateReport(RptData);
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

            DropDownList cmbFrom = (DropDownList)formViewResourceReport.FindControl("cmbFromDate");
            DropDownList cmbTo = (DropDownList)formViewResourceReport.FindControl("cmbToDate");
            TextBox PersonCompletingReport = (TextBox)formViewResourceReport.FindControl("txtPersonCompletingReport");
            TextBox StateFipsCode = (TextBox)formViewResourceReport.FindControl("txtStateCode");
            e.NewValues["LastUpdatedBy"] = AccountInfo.UserId;
            e.NewValues["StateFIPSCode"] = StateFipsCode.Text;
            e.NewValues["RepYrFrom"] = "04/01" + "/" + cmbFrom.SelectedItem.Text;
            e.NewValues["RepYrTo"] = "03/31" + "/" + cmbTo.SelectedItem.Text;
            e.NewValues["LastUpdatedBy"] = AccountInfo.UserId;
            e.NewValues["PersonCompletingReportName"] = PersonCompletingReport.Text;
        }
        #endregion


        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion

        #region Implementation of IAuthorize

        public bool IsAuthorized()
        {
            //Only CMS and State Admins are able to add a new sub-state region.
            return ((AccountInfo.Scope == Scope.CMS || AccountInfo.Scope == Scope.State) && AccountInfo.IsAdmin);
        }

        #endregion

    }
}
