using System;

namespace ShiptalkLogic.BusinessObjects
{
    public sealed class AgencyLocation : IModified, IIsActive
    {
        public int? Id { get; set; }
        public int? AgencyId { get; set; }
        public string LocationName { get; set; }
        public IAddress PhysicalAddress { get; set; }
        public IAddress MailingAddress { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactMiddleName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactTitle { get; set; }
        public string Comments { get; set; }
        public string HoursOfOperation { get; set; }
        public string PrimaryPhone { get; set; }
        public string SecondaryPhone { get; set; }
        public string TollFreePhone { get; set; }
        public string TDD { get; set; }
        public string TollFreeTDD { get; set; }
        public string Fax { get; set; }
        public string PrimaryEmail { get; set; }
        public string SecondaryEmail { get; set; }
        public bool IsMainOffice { get; set; }
        //Added by Lavanya
        public string AvailableLanguages { get; set; }        
        public bool HideAgencyFromSearch { get; set; }
        //end
       

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
