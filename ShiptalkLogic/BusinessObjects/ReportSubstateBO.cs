using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web;

namespace ShiptalkLogic.BusinessObjects
{



    public enum SubStateReportType
    {
        [Description("County Code Of Client Residence")]
        CountycodeOfClientRes = 1,
        [Description("ZIPCode Of Counselor Location")]
        ZIPCodeOfCounselorLocation = 2,
        [Description("County Of Counselor Location")]
        CountyOfCounselorLocation = 3,
        [Description("ZIP Code Of Client Residence")]
        ZIPCodeOfClientRes = 4,
        [Description("Agency")]
        Agency = 5,
        [Description("County Of Event")]
        CountycodeOfEvent = 6,
        [Description("Pam Agency")]
        AgencyPam = 7
    }



    public enum SubStateReportTypeCCF
    {
        [Description("County Code Of Client Residence")]
        CountycodeOfClientRes = 1,
        [Description("ZIPCode Of Counselor Location")]
        ZIPCodeOfCounselorLocation = 2,
        [Description("County Of Counselor Location")]
        CountyOfCounselorLocation = 3,
        [Description("ZIP Code Of Client Residence")]
        ZIPCodeOfClientRes = 4,
        [Description("Agency")]
        Agency = 5,
          
    }

    public enum SubStateReportTypePAM
    {
        [Description("County Of Event")]
        CountycodeOfEvent = 6,
        [Description("Pam Agency")]
        AgencyPam = 7
          
    }


}
