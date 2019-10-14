using System;

namespace ShiptalkLogic.BusinessObjects
{
    internal class AgencyAddress : IAddress
    {
        #region Implementation of IModified

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        #endregion

        #region Implementation of IAddress

        public int? Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public State? State { get; set; }
        public County County { get; set; }
        //Added by Lavanya
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        //end
        #endregion
    }
}