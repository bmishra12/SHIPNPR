using System;

namespace ShiptalkLogic.BusinessObjects
{
    public sealed class County : IModified
    {
        public string Code { get; set; }
        public State State { get; set; }
        public string ShortName { get; set; }
        public string MediumName { get; set; }
        public string LongName { get; set; }
        public string CBSACode { get; set; }
        public string CBSATitle { get; set; }
        public string CBSALSAD { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string WebLinkText { get; set; }

        #region IModified Members

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        #endregion
    }
}   