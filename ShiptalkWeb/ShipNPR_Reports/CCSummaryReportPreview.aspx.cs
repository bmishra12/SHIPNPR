using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkWeb.Routing;

namespace ShiptalkWeb.ShipNPR_Reports
{
    public partial class CCSummaryReportPreview : System.Web.UI.Page
    {
        private CCSummaryReportBLL _logic;
        public CCSummaryReportBLL Logic
        {
            get
            {
                if (_logic == null)
                    _logic = new CCSummaryReportBLL();

                return _logic;
            }
        }


        private ReportSubStateRegionBLL _sublogic;

        public ReportSubStateRegionBLL SubStatLogic
        {
            get
            {
                if (_sublogic == null) _sublogic = new ReportSubStateRegionBLL();

                return _sublogic;
            }
        }


        private ViewCCSummaryReportViewData _viewData;

        private ViewCCSummaryReportViewData ViewData
        {
            get;
            set;
        }
        public int SubmitterUserId
        {
            get;
            set;
        }
        public string SubmitterName
        {
            get;
            set;
        }
        public int CounselorUserId
        {
            get;
            set;
        }
        public string CounselorName
        {
            get;
            set;
        }
        public int SubStateRegionId
        {
            get;
            set;
        }
        public string SubStateRegionName
        {
            get;
            set;
        }
        public string ZIPCodeofClientResidence
        {
            get;
            set;
        }
        public string CountyNameofClientResidence
        {
            get;
            set;
        }
        public string CountyFipsCodeofClientResidence
        {
            get;
            set;
        }
        public string ZipCodeOfCounselorLocation
        {
            get;
            set;
        }
        public string CountyFipsCode
        {
            get;
            set;
        }
        public string CountyName
        {
            get;
            set;
        }
        public string AgencyId
        {
            get;
            set;
        }
        public string AgencyName
        {
            get;
            set;
        }
        public CCReportType ReportType
        {
            get;
            set;
        }
        public DateTime DOCStartDate
        {
            get;
            set;
        }
        public DateTime DOCEndDate
        {
            get;
            set;
        }
        public string StateFIPSCode
        {
            get;
            set;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;


        }
        protected string GetDistribution(decimal Contact, decimal TotalClientContacts)
        {
            string Distribution = ((Contact / TotalClientContacts) * 100).ToString("##.#");

            if (Distribution == "")
            {
                Distribution = "0.0";
            }
            return Distribution;
        }
        private void BindSessionData()
        {
            if (Session["ReportType"] != null && Session["ReportType"].ToString() != "")
            {
                ReportType = (CCReportType)Convert.ToInt16(Session["ReportType"].ToString());

                if (Session["ReportStartDate"] != null && Session["ReportStartDate"].ToString() != "")
                {
                    DOCStartDate = Convert.ToDateTime(Session["ReportStartDate"].ToString());

                    if (Session["ReportEndDate"] != null && Session["ReportEndDate"].ToString() != "")
                        DOCEndDate = Convert.ToDateTime(Session["ReportEndDate"].ToString());

                    if (Session["ReportStateFIPSCode"] != null && Session["ReportStateFIPSCode"].ToString() != "")
                        StateFIPSCode = Session["ReportStateFIPSCode"].ToString();

                    if (Session["ReportAgencyId"] != null && Session["ReportAgencyId"].ToString() != "")
                    {
                        AgencyId = Session["ReportAgencyId"].ToString();
                        AgencyName = Session["ReportAgencyName"].ToString();
                    }

                    if (Session["ReportCountyFipsCode"] != null && Session["ReportCountyFipsCode"].ToString() != "")
                    {
                        CountyFipsCode = Session["ReportCountyFipsCode"].ToString();
                        CountyName = Session["ReportCountyName"].ToString();
                    }
                    if (Session["ReportZipCodeOfCounselorLocation"] != null && Session["ReportZipCodeOfCounselorLocation"].ToString() != "")
                    {
                        ZipCodeOfCounselorLocation = Session["ReportZipCodeOfCounselorLocation"].ToString();
                    }

                    if (Session["ReportCountyofClientResidenceFipsCode"] != null && Session["ReportCountyofClientResidenceFipsCode"].ToString() != "")
                    {
                        CountyFipsCodeofClientResidence = Session["ReportCountyofClientResidenceFipsCode"].ToString();
                        CountyNameofClientResidence = Session["ReportCountyNameofClientResidence"].ToString();
                    }
                    if (Session["ReportZIPCodeofClientResidence"] != null && Session["ReportZIPCodeofClientResidence"].ToString() != "")
                    {
                        ZIPCodeofClientResidence = Session["ReportZIPCodeofClientResidence"].ToString();
                    }
                    if (Session["SubStateRegionId"] != null && Session["SubStateRegionId"].ToString() != "")
                    {
                        SubStateRegionId = Convert.ToInt32(Session["SubStateRegionId"].ToString());
                        SubStateRegionName = Session["SubStateRegionName"].ToString();
                    }
                    if (Session["CounselorUserId"] != null && Session["CounselorUserId"].ToString() != "")
                    {
                        CounselorUserId = Convert.ToInt32(Session["CounselorUserId"].ToString());
                        CounselorName = Session["CounselorUserName"].ToString();
                    }
                    if (Session["SubmitterUserId"] != null && Session["SubmitterUserId"].ToString() != "")
                    {
                        SubmitterUserId = Convert.ToInt32(Session["SubmitterUserId"].ToString());
                        SubmitterName = Session["SubmitterUserName"].ToString();
                    }

                }
                else
                {
                    RouteController.RouteTo(RouteController.NprReports());
                }
            }
            else
            {
                RouteController.RouteTo(RouteController.NprReports());
            }
        }
        protected void DisplayReportParameter()
        {
            if (ReportType == CCReportType.ContactsBySubmitter)
                strDateOfContact.Text = string.Format("{0}{1}{2}{3}{4}", "Date Record Initially Submitted (Entered) = [Start Date : ", DOCStartDate.Date.ToString("MM/dd/yyyy"), "] - [End Date : ", DOCEndDate.Date.ToString("MM/dd/yyyy"), "]");
            else
                 
                strDateOfContact.Text = string.Format("{0}{1}{2}{3}{4}", "Date Of Contact = [Start Date : ", DOCStartDate.Date.ToString("MM/dd/yyyy"), "] - [End Date : ", DOCEndDate.Date.ToString("MM/dd/yyyy"), "]");
          
            strRunDate.Text = string.Format("{0}{1}","Run Date Time = ", DateTime.Now.ToString());

            if (ReportType != CCReportType.ContactsByNational)
            {
                trState.Visible = true;
                    strState.Text = string.Format("{0}{1}", "State Providing the Counseling Activity  = ", State.GetStateName(StateFIPSCode));

            }
            if (ReportType == CCReportType.ContactsByNational)
            {
                trNational.Visible = true;
                strNational.Text = "National = United States Total";
            }

            if (ReportType == CCReportType.ContactsByCounselor)
            {
                trCounselor.Visible = true;
                strCounselor.Text = string.Format("{0}{1}", "Counselor = ", CounselorName);
            }
            if (ReportType == CCReportType.ContactsBySubmitter)
            {
                trSubmitter.Visible = true;
                strSubmitter.Text = string.Format("{0}{1}", "Submitter = ", SubmitterName);
            }
            if (ReportType == CCReportType.ContactsByAgency)
            {
                trAgencies.Visible = true;
                strAgencies.Text = string.Format("{0}{1}", "Agency = ", AgencyName);
            }
            if (ReportType == CCReportType.ContactsByCountyOfCounselorLocation)
            {
                trCountyOfCounselorLocation.Visible = true;
                strCountyOfCounselorLocation.Text = string.Format("{0}{1}{2}", "County of Counselor Location = ", CountyName, " County");
            }
            if (ReportType == CCReportType.ContactsByZipCodeOfCounselorLocation)
            {
                trZipCodeOfCounselorLocation.Visible = true;
                strZipCodeOfCounselorLocation.Text = string.Format("{0}{1}", "ZIP Code of Counselor Location = ", ZipCodeOfCounselorLocation);
            }
            if (ReportType == CCReportType.ContactsByCountyOfClientResidence)
            {
                trCountyOfClientResidence.Visible = true;
                strCountyOfClientResidence.Text = string.Format("{0}{1}{2}", "County of Client Residence = ", CountyNameofClientResidence, " County");
            }
            if (ReportType == CCReportType.ContactsByZipcodeOfClientResidence)
            {
                trZipCodeOfClientResidence.Visible = true;
                strZipCodeOfClientResidence.Text = string.Format("{0}{1}", "ZIP Code of Client Residence = ", ZIPCodeofClientResidence);
            }
            if (ReportType == CCReportType.ContactsBySubStateRegionOnAgency)
            {
                string _agencyName = SubStatLogic.GetSubStateRegionAgencyNameForReport(SubStateRegionId);

                trContactsBySubStateRegionOnAgency.Visible = true;
                strContactsBySubStateRegionOnAgency.Text = string.Format("{0}{1}{2}{3}{4}", "Reporting Substate Regions Based on Agency Groupings = ", SubStateRegionName, " [", _agencyName, "]");
            }

            if (ReportType ==CCReportType.ContactsBySubStateRegionOnCountiesoOfCounselorLocation)
             {
                 string _countyname = SubStatLogic.GetSubStateRegionCountyNameForReport(SubStateRegionId);

                 trCountyOfCounselorLocation.Visible = true;
                 strCountyOfCounselorLocation.Text = string.Format("{0}{1}{2}{3}{4}", "Substate Region Based on Counties of Counselor Locations = ", SubStateRegionName, " [",  _countyname , "]");

             }
            if (ReportType ==CCReportType.ContactsBySubStateRegionOnZipcodesOfCounselorLocation)
             {
                 string _zips = SubStatLogic.GetSubStateRegionZipCodeForReport(SubStateRegionId);

                 trZipCodeOfClientResidence.Visible = true;

                 strZipCodeOfClientResidence.Text = string.Format("{0}{1}{2}{3}{4}", "Substate Region Based on ZIP Codes of Counselor Locations = ", ZIPCodeofClientResidence, " [", _zips, "]");

             }
            if (ReportType ==CCReportType.ContactsBySubStateRegionOnCountiesOfClientResidence)
             {
                 string _countyname = SubStatLogic.GetSubStateRegionCountyNameForReport(SubStateRegionId);

                 trCountyOfCounselorLocation.Visible = true;

                 strCountyOfCounselorLocation.Text = string.Format("{0}{1}{2}{3}{4}", "Substate Region Based on Counties of Client Residence = ", SubStateRegionName, " [", _countyname, "]");

             }
            if (ReportType ==CCReportType.ContactsBySubStateRegionOnZipcodeOfClientResidence)
             {
                 string _zips = SubStatLogic.GetSubStateRegionZipCodeForReport(SubStateRegionId);

                trZipCodeOfClientResidence.Visible = true;
                strZipCodeOfClientResidence.Text = string.Format("{0}{1}{2}{3}{4}", "Substate Region Based on ZIP Codes of Client Residence = ", ZIPCodeofClientResidence, " [", _zips, "]");

             }
 
        }

        public enum CCReportType
        {
            ContactsByState = 1,
            ContactsByAgency = 2,
            ContactsByCountyOfCounselorLocation = 3,
            ContactsByZipCodeOfCounselorLocation = 4,
            ContactsByCountyOfClientResidence = 5,
            ContactsByZipcodeOfClientResidence = 6,
            ContactsByCounselor = 7,
            ContactsBySubmitter = 8,
            ContactsBySubStateRegionOnAgency = 9,
            ContactsBySubStateRegionOnCountiesoOfCounselorLocation = 10,
            ContactsBySubStateRegionOnZipcodesOfCounselorLocation = 11,
            ContactsBySubStateRegionOnCountiesOfClientResidence = 12,
            ContactsBySubStateRegionOnZipcodeOfClientResidence = 13,
            ContactsByNational = 14
        }
        protected void dataSourceViewCCSummary_Selecting(object sender, Microsoft.Practices.Web.UI.WebControls.ObjectContainerDataSourceSelectingEventArgs e)
        {
            if (Session["CCSummaryReport"] != null)
            {
                BindSessionData();
                ViewData = (ViewCCSummaryReportViewData)Session["CCSummaryReport"];
                if (ViewData != null)
                {
                    dataSourceViewCCSummary.DataSource = ViewData;
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "No Records found.";
                }
                DisplayReportParameter();
                ////Session["CCSummaryReport"] = "";
                ////Session["CCSummaryReport"] = null;
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Your report session is lost. Please rerun your report";
            }
        }
    }
}