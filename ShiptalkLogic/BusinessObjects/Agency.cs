using System;
using System.Collections.Generic;
using System.Linq;

namespace ShiptalkLogic.BusinessObjects
{
    public sealed class Agency : IModified, IIsActive
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public AgencyType? Type { get; set; }
        public string SponsorFirstName { get; set; }
        public string SponsorMiddleName { get; set; }
        public string SponsorLastName { get; set; }
        public string SponsorTitle { get; set; }
        public string URL { get; set; }
        public string Comments { get; set; }
        public State State { get; set; }
        public IList<AgencyLocation> Locations { get; set; }
        public IList<County> ServiceAreas { get; set; }

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

        public bool HideAgencyFromPublic { get; set; }

        public AgencyLocation GetMainOffice()
        {
            return (from agencyLocation in Locations
                    where agencyLocation.IsMainOffice
                    select agencyLocation).FirstOrDefault();
        }

    }
}