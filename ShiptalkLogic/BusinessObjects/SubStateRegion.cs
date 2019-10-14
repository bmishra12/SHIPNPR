using System;
using System.Collections.Generic;

namespace ShiptalkLogic.BusinessObjects
{
    public sealed class SubStateRegion : IModified, IIsActive
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public State State { get; set; }
        public IList<Agency> Agencies { get; set; }

        #region Implementation of IModified

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        #endregion

        #region Implementation of IIsActive

        public bool IsActive { get; set; }
        public DateTime? ActiveInactiveDate { get; set; }

        #endregion
    }
}
