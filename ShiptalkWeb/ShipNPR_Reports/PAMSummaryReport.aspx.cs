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
    public partial class PAMSummaryReport : System.Web.UI.Page
    {
        private PAMSummaryReportBLL _logic;

        #region Properties
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
        public int PresenterContributorUserId
        {
            get;
            set;
        }
        public string PresenterContributorName
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
        public string ZipCodeOfActivityEvent
        {
            get;
            set;
        }
        public string CountyOfActivityEvent
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
        public PAMReportType ReportType
        {
            get;set;
        }
        public DateTime DOCStartDate
        {
            get;set;
        }
        public DateTime DOCEndDate
        {
            get;set;
        }
        public string StateFIPSCode
        {
            get;set;
        }

        public int ScopeId
        {
            get;
            set;
        }

        public int UserId
        {
            get;
            set;
        }

        public bool IsSubStateAdmin
        {
            get;
            set;
        }

        public bool ReportStateFIPSCode
        {
            get;
            set;
        }

        public PAMSummaryReportBLL Logic
        {
            get
            {
                if (_logic == null)
                    _logic = new PAMSummaryReportBLL();

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


        private ViewPAMSummaryReportViewData _viewData;

        private ViewPAMSummaryReportViewData ViewData
        {
            get;
            set;
        }
        #endregion
        
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
        }
        private ViewPAMSummaryReportViewData GetPAMSummaryReport()
        {
            if (ReportType == PAMReportType.State)
            {
                return Logic.GetPAMSummaryReportByState(DOCStartDate, DOCEndDate, StateFIPSCode);
            }
            else if (ReportType == PAMReportType.Agency)
            {
                return Logic.GetPAMSummaryReportByAgency(DOCStartDate, DOCEndDate, AgencyId);
            }
            else if (ReportType == PAMReportType.CountyOfActivityEvent)
            {
                if (IsSubStateAdmin)
                    return Logic.GetPAMSummaryReportByCountyOfActivityEvent(DOCStartDate, DOCEndDate, CountyOfActivityEvent, ScopeId, UserId, Convert.ToInt32(AgencyId), StateFIPSCode);
                else
                    return Logic.GetPAMSummaryReportByCountyOfActivityEvent(DOCStartDate, DOCEndDate, CountyOfActivityEvent, ScopeId, UserId, -999, StateFIPSCode);
            }
            else if (ReportType == PAMReportType.ZipCodeOfActivityEvent)
            {
                if (IsSubStateAdmin)
                    return Logic.GetPAMSummaryReportByZipCodeOfActivityEvent(DOCStartDate, DOCEndDate, ZipCodeOfActivityEvent, ScopeId, UserId, Convert.ToInt32(AgencyId));
                else
                    return Logic.GetPAMSummaryReportByZipCodeOfActivityEvent(DOCStartDate, DOCEndDate, ZipCodeOfActivityEvent, ScopeId, UserId, -999);

            }
            else if (ReportType == PAMReportType.SubStateRegionOnAgency)
            {
                return Logic.GetPAMSummaryReportBySubStateRegionOnAgency(DOCStartDate, DOCEndDate, SubStateRegionId);
            }
            else if (ReportType == PAMReportType.SubStateRegionOnCountiesOfActivityEvent)
            {
                return Logic.GetPAMSummaryReportBySubStateRegionCountiesEvent(DOCStartDate, DOCEndDate, SubStateRegionId);
            }
            else if (ReportType == PAMReportType.National)
            {
                return Logic.GetPAMSummaryReportByNational(DOCStartDate, DOCEndDate);
            }
            else if (ReportType == PAMReportType.PresenterContributor)
            {
                return Logic.GetPAMSummaryReportByPresenterContributor(DOCStartDate, DOCEndDate,Convert.ToInt32(AgencyId), PresenterContributorUserId);
            }
            else if (ReportType == PAMReportType.Submitter)
            {
                return Logic.GetPAMSummaryReportBySubmitter(DOCStartDate, DOCEndDate, Convert.ToInt32(AgencyId),SubmitterUserId);
            }
            else
            {
                return null;
            }
        }
        private void BindSessionData()
        {
            if (Session["ReportType"] != null && Session["ReportType"].ToString() != "")
            {
                ReportType = (PAMReportType)Convert.ToInt16(Session["ReportType"].ToString());

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

                    if (Session["ReportCountyOfActivityEvent"] != null && Session["ReportCountyOfActivityEvent"].ToString() != "")
                    {
                        CountyOfActivityEvent = Session["ReportCountyOfActivityEvent"].ToString();
                        CountyName = Session["ReportCountyName"].ToString();
                    }
                    if (Session["ReportZipCodeOfActivityEvent"] != null && Session["ReportZipCodeOfActivityEvent"].ToString() != "")
                    {
                        ZipCodeOfActivityEvent = Session["ReportZipCodeOfActivityEvent"].ToString();
                    }
                    if (Session["SubStateRegionId"] != null && Session["SubStateRegionId"].ToString() != "")
                    {
                        SubStateRegionId = Convert.ToInt32(Session["SubStateRegionId"].ToString());
                        SubStateRegionName = Session["SubStateRegionName"].ToString();
                    }
                    if (Session["PresenterContributorUserId"] != null && Session["PresenterContributorUserId"].ToString() != "")
                    {
                        PresenterContributorUserId = Convert.ToInt32(Session["PresenterContributorUserId"].ToString());
                        PresenterContributorName = Session["PresenterContributor"].ToString();
                    }
                    if (Session["SubmitterUserId"] != null && Session["SubmitterUserId"].ToString() != "")
                    {
                        SubmitterUserId = Convert.ToInt32(Session["SubmitterUserId"].ToString());
                        SubmitterName = Session["SubmitterUserName"].ToString();
                    }

                    if (Session["ScopeId"] != null && Session["ScopeId"].ToString() != "")
                    {
                        ScopeId = Convert.ToInt32(Session["ScopeId"].ToString());
                    }

                    if (Session["UserId"] != null && Session["UserId"].ToString() != "")
                    {
                        UserId = Convert.ToInt32(Session["UserId"].ToString());
                    }

                    if (Session["IsSubStateAdmin"] != null && Session["IsSubStateAdmin"].ToString() != "")
                    {
                        IsSubStateAdmin = Convert.ToBoolean(Session["IsSubStateAdmin"].ToString());
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
        protected void dataSourceViewPAMSummary_Selecting(object sender, Microsoft.Practices.Web.UI.WebControls.ObjectContainerDataSourceSelectingEventArgs e)
        {
            BindSessionData();
            ViewData = GetPAMSummaryReport();
            if(ViewData != null)
            {
                dataSourceViewPAMSummary.DataSource = ViewData;
                ////////////Session["PAMSummaryReport"] = ViewData;
                panelPrinterFriendly.Visible = true;

            }
            else
            {
                /////////////////////Session["PAMSummaryReport"] = null;
                lblMessage.Visible = true;
                panelPrinterFriendly.Visible = false;

                lblMessage.Text = "No Records found for the given parameter";
            }
            DisplayReportParameter();
        }
        protected void DisplayReportParameter()
        {
            if (ReportType == PAMReportType.Submitter)

                strDateOfContact.Text = string.Format("{0}{1}{2}{3}{4}", "Date Record Initially Submitted (Entered) = [Start Date : ", DOCStartDate.Date.ToString("MM/dd/yyyy"), "] - [End Date : ", DOCEndDate.Date.ToString("MM/dd/yyyy"), "]");
            else
            strDateOfContact.Text = string.Format("{0}{1}{2}{3}{4}", "Date of Event = [Start Date : ", DOCStartDate.Date.ToString("MM/dd/yyyy"), "] - [End Date : ", DOCEndDate.Date.ToString("MM/dd/yyyy"), "]");
          
            strRunDate.Text = string.Format("{0}{1}","Run Date Time = ", DateTime.Now.ToString());

            if (ReportType != PAMReportType.National)
            {
                trState.Visible = true;
                strState.Text = string.Format("{0}{1}", "State Providing the Activity-Event = ", State.GetStateName(StateFIPSCode));
            }
            if (ReportType == PAMReportType.National)
            {
                trNational.Visible = true;
                strNational.Text = "National = United States Total";
            }

            if (ReportType == PAMReportType.PresenterContributor)
            {
                trPresenterContributor.Visible = true;
                strPresenterContributor.Text = string.Format("{0}{1}", "Presenter Contributor = ", PresenterContributorName);
                
                trAgencies.Visible = true;
                if (AgencyId == "0")
                    strAgencies.Text = string.Format("{0}{1}", "Agency = ", "All Agencies For Presenter Contributor");
                else
                    strAgencies.Text = string.Format("{0}{1}", "Agency = ", AgencyName);
            }
            if (ReportType == PAMReportType.Submitter)
            {
                trSubmitter.Visible = true;
                strSubmitter.Text = string.Format("{0}{1}", "Submitter = ", SubmitterName);

                trAgencies.Visible = true;
                if (AgencyId == "0")
                    strAgencies.Text = string.Format("{0}{1}", "Agency = ", "All Agencies For Submitter");
                else
                    strAgencies.Text = string.Format("{0}{1}", "Agency = ", AgencyName);
            }
            if (ReportType == PAMReportType.Agency)
            {
                trAgencies.Visible = true;
                strAgencies.Text = string.Format("{0}{1}", "Agency = ", AgencyName);
            }
            if (ReportType == PAMReportType.CountyOfActivityEvent)
            {
                trCountyOfActivityEvent.Visible = true;
                strCountyOfActivityEvent.Text = string.Format("{0}{1}{2}", "County Of Activity-Event = ", CountyName, " County");

                if (IsSubStateAdmin) //substate admin has selected agencydropdown
                {
                    trAgencies.Visible = true;
                    strAgencies.Text = string.Format("{0}{1}", "Agency = ", AgencyName);
                }
            }
            if (ReportType == PAMReportType.ZipCodeOfActivityEvent)
            {
                trZipCodeOfActivityEvent.Visible = true;
                strZipCodeOfActivityEvent.Text = string.Format("{0}{1}", "ZIP Code of Activity-Event = ", ZipCodeOfActivityEvent);
                
                if (IsSubStateAdmin) //substate admin has selected agencydropdown
                {
                    trAgencies.Visible = true;
                    strAgencies.Text = string.Format("{0}{1}", "Agency = ", AgencyName);
                }
            }

            if (ReportType == PAMReportType.SubStateRegionOnAgency)
            {
                string _agencyName = SubStatLogic.GetSubStateRegionAgencyNameForReport(SubStateRegionId);

                trContactsBySubStateRegionOnAgency.Visible = true;
                strContactsBySubStateRegionOnAgency.Text = string.Format("{0}{1}{2}{3}{4}", "Reporting Substate Regions Based on Agency Groupings = ", SubStateRegionName, " [", _agencyName, "]");

            }

            if (ReportType == PAMReportType.SubStateRegionOnCountiesOfActivityEvent)
            {
                string _countyname = SubStatLogic.GetSubStateRegionCountyNameForReport(SubStateRegionId);

                trContactsBySubStateRegionOnCountiesOfActivityEvent.Visible = true;
                strContactsBySubStateRegionOnCountiesOfActivityEvent.Text = string.Format("{0}{1}{2}{3}{4}", "Substate Region Based on Counties of Activity-Event = ", SubStateRegionName, " [", _countyname, "]");
            }
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

        public enum PAMReportType
        {
            State = 1,
            Agency = 2,
            CountyOfActivityEvent = 3,
            ZipCodeOfActivityEvent = 4,
            PresenterContributor = 5,
            Submitter = 6,
            SubStateRegionOnAgency = 7,
            SubStateRegionOnCountiesOfActivityEvent = 8,
            National = 9
        }

    }
    
}
