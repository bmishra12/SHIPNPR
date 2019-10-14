using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Web;

namespace ShiptalkLogic.BusinessObjects
{

    public enum Descriptor
    {
        [Description("Counselor")]
        Counselor = 1,
        [Description("Data Submitter")]
        DataSubmitter = 2,
        [Description("Public and Media Staff ")]
        PresentationAndMediaStaff = 3,
        [Description("Data Editor/Reviewer")]
        DataEditor_Reviewer = 4,
        [Description("State SHIP Director")]
        ShipDirector = 5,
        [Description("Other Staff (NPR Read-only)")]
        OtherStaff_NPR = 6,
        [Description("Other Staff (SHIP Read-only)")]
        OtherStaff_SHIP = 7,
        [Description("User Registrations Approver")]
        UserRegistrations_Approver = 8,
        [Description("Super Editor / Reviewer / Supervisor")]
        Super_Editor_Reviewer = 9,
    }

    public enum Scope
    {
        [Description("CMS")]
        CMS = 1,
        [Description("CMS Regional")]
        CMSRegional = 2,
        [Description("State")]
        State = 3,
        [Description("SubState Region")]
        SubStateRegion = 4,
        [Description("Agency")]
        Agency = 5
    }

    public enum AgencyType
    {
        [Description("Statewide Call Center")]
        StatewideCallCenter = 1,
        [Description("Information and Referral Phone Line")]
        InformationAndReferralPhoneLine = 2,
        [Description("Other Call Center")]
        OtherCallCenter = 3,
        [Description("Other Statewide Operations and Activities")]
        OtherStateCentralOfficeOperationsAndActivities = 4,
        [Description("Substate Regional Office")]
        SubstateRegionalOffice = 5,
        [Description("Local Agency")]
        LocalAgency = 6,
        [Description("OLD NPR DATA - NOT A REAL AGENCY")]
        OldNprDataNotARealAgency = 99

    }

    public enum ComparisonCriteria
    {
        IsHigher = 1,
        IsLower = 2,
        IsHigherThanOrEqualTo = 3,
        IsLowerThanOrEqualTo = 4,
        IsEqual = 5
    }

    //Used for Simple Search. Advanced Search will be different.
    public struct UserSearchSimpleParams
    {
        public string SearchText { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public int SearchedById { get; set; }
        public string SearchByStateFIPS { get; set; }
    }

    [Flags]
    public enum UserFillerOptions
    {
        Full = 0x0,
        UserAccount = 0x1,
        UserProfile = 0x2,
        UserRegionalProfile = 0x4,
        //= 0x4,
        //= 0x8,
        //= 0x10,
        //= 0x20,
        //= 0x40
    }
   
}
