using System;
using System.Collections.Generic;
using System.Linq;

namespace ShiptalkLogic.BusinessObjects
{
    public sealed class SubStateRegionForReport : IModified
    {

        public int? ID { get; set; }

        public State State { get; set; }

        public string SubStateRegionName { get; set; }

        public string SubStateRegionServiceEntityCode { get; set; }

        public SubStateReportType Type { get; set; }

        public FormType ReprotFormType { get; set; }

        public int CreatedUserID { get; set; }

        public IList<SubStateRegionZIPFIPSForReport> ServiceAreas { get; set; }


        #region Implementation of IModified

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        #endregion



    }

    public sealed class SubStateRegionZIPFIPSForReport
      {
           public  int ZIPFIPSCodeID { get; set; }
           public  int  SubStateRegionID { get; set; }
           public  string  ZIPFIPSCountyCode { get; set; }
           public string Name { get; set; }

           public  DateTime?  CreatedDate { get; set; }
           public int   CreatedUserID { get; set; }
      }
}