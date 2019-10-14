using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace ShiptalkLogic.BusinessObjects
{
    public sealed class SHIPProfile : IModified

    {
        public string ID { get; set; }
        public string StateName { get; set; }

       
        public string ProgramName { get; set; }
       
        public string ProgramWebsite { get; set; }
        
        public string ProgramSummary { get; set; }

        public string BeneficiaryContactPhoneTollFree { get; set; }
        public bool BeneficiaryContactPhoneTollFreeInStateOnly { get; set; }
        public string BeneficiaryContactPhoneTollLine { get; set; }
        public string BeneficiaryContactWebsite { get; set; }
        public string BeneficiaryContactTDDLine { get; set; }
        public string BeneficiaryContactEmail { get; set; }
        public string BeneficiaryContactHours { get; set; }

        public string AdminAgencyContactName { get; set; }
        public string AdminAgencyName { get; set; }
        public string AdminAgencyContactTitle { get; set; }
        public string AdminAgencyAddress { get; set; }
        public string AdminAgencyCity { get; set; }
        public string AdminAgencyZipcode { get; set; }
        public string AdminAgencyPhone { get; set; }
        public string AdminAgencyFax { get; set; }
        public string AdminAgencyEmail { get; set; }

        public string ProgramCoordinatorName { get; set; }
        public string ProgramCoordinatorAddress { get; set; }
        public string ProgramCoordinatorCity { get; set; }
        public string ProgramCoordinatorZipcode { get; set; }
        public string ProgramCoordinatorPhone { get; set; }
        public string ProgramCoordinatorFax { get; set; }
        public string ProgramCoordinatorEmail { get; set; }

        public string AvailableLanguages { get; set; }
        public int? NumberOfVolunteerCounselors { get; set; }
        public int? NumberOfStateStaff { get; set; }
        public int? TotalCounties { get; set; }
        public int? NumberOfCountiesServed { get; set; }
        public int? NumberOfSponsors { get; set; }

        //New fields: added by Lavanya Maram - 04/23/2013

        public string StateOversightAgency { get; set; }
        public int? NumberOfPaidStaff { get; set; }
        public int? NumberOfCoordinators { get; set; }
        public int? NumberOfCertifiedCounselors { get; set; }
        public string NumberOfEligibleBeneficiaries { get; set; }
        public string NumberOfBeneficiaryContacts { get; set; }
        public string LocalAgencies { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        #region Implementation of IModified

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        #endregion
   
    }
}
