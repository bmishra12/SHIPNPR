using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace ShiptalkLogic.BusinessObjects
{
    public sealed class CCSummaryReport : IModified
    {
        public string StateName { get; set; }
        public int? TotalClientContacts { get; set;}

        public int? ClientFirstVsContinuingContact1 { get; set; }
        public int? ClientFirstVsContinuingContact2 { get; set; }
        public int? ClientFirstVsContinuingContact3 { get; set; }

        public int? ClientLearnedAboutSHIP1 { get; set; }
        public int? ClientLearnedAboutSHIP2 { get; set; }
        public int? ClientLearnedAboutSHIP3 { get; set; }
        public int? ClientLearnedAboutSHIP4 { get; set; }
        public int? ClientLearnedAboutSHIP5 { get; set; }
        public int? ClientLearnedAboutSHIP6 { get; set; }
        public int? ClientLearnedAboutSHIP7 { get; set; }
        public int? ClientLearnedAboutSHIP8 { get; set; }
        public int? ClientLearnedAboutSHIP9 { get; set; }
        public int? ClientLearnedAboutSHIP10 { get; set; }

        public int? ClientMethodOfContact1 { get; set; }
        public int? ClientMethodOfContact2 { get; set; }
        public int? ClientMethodOfContact3 { get; set; }
        public int? ClientMethodOfContact4 { get; set; }
        public int? ClientMethodOfContact5 { get; set; }
        public int? ClientMethodOfContact6 { get; set; }
        public int? ClientMethodOfContact7 { get; set; }
        public int? ClientMethodOfContact8 { get; set; }

        public int? ClientAgeGroup1 { get; set; }
        public int? ClientAgeGroup2 { get; set; }
        public int? ClientAgeGroup3 { get; set; }
        public int? ClientAgeGroup4 { get; set; }
        public int? ClientAgeGroup5 { get; set; }

        public int? ClientGender1 { get; set; }
        public int? ClientGender2 { get; set; }
        public int? ClientGender3 { get; set; }

        public int? ClientContactRace1 { get; set; }
        public int? ClientContactRace2 { get; set; }
        public int? ClientContactRace3 { get; set; }
        public int? ClientContactRace4 { get; set; }
        public int? ClientContactRace5 { get; set; }
        public int? ClientContactRace6 { get; set; }
        public int? ClientContactRace7 { get; set; }
        public int? ClientContactRace8 { get; set; }
        public int? ClientContactRace9 { get; set; }
        public int? ClientContactRace10 { get; set; }
        public int? ClientContactRace11 { get; set; }
        public int? ClientContactRace12 { get; set; }
        public int? ClientContactRace13 { get; set; }
        public int? ClientContactRace14 { get; set; }
        public int? ClientContactRace15 { get; set; }
        public int? ClientContactRace16 { get; set; }
        public int? ClientContactRace17 { get; set; }
        public int? ClientContactRace18 { get; set; }
        public int? ClientContactRace19 { get; set; }
        public int? ClientContactRace20 { get; set; }
        public int? ClientContactRace21 { get; set; }

        public int? ClientPrimaryLanguageOtherThanEnglish1 { get; set; }
        public int? ClientPrimaryLanguageOtherThanEnglish2 { get; set; }
        public int? ClientPrimaryLanguageOtherThanEnglish3 { get; set; }

        public int? ClientMonthlyIncome1 { get; set; }
        public int? ClientMonthlyIncome2 { get; set; }
        public int? ClientMonthlyIncome3 { get; set; }

        public int? ClientAssests1 { get; set; }
        public int? ClientAssests2 { get; set; }
        public int? ClientAssests3 { get; set; }

        public int? ClientReceivingSSOrMedicareDisability1 { get; set; }
        public int? ClientReceivingSSOrMedicareDisability2 { get; set; }
        public int? ClientReceivingSSOrMedicareDisability3 { get; set; }

        public int? ClientDualEligble1 { get; set; }
        public int? ClientDualEligble2 { get; set; }
        public int? ClientDualEligble3 { get; set; }

        public int? ClientTopicID1 { get; set; }
        public int? ClientTopicID2 { get; set; }
        public int? ClientTopicID3 { get; set; }
        public int? ClientTopicID4 { get; set; }
        public int? ClientTopicID5 { get; set; }
        public int? ClientTopicID6 { get; set; }
        public int? ClientTopicID7 { get; set; }
        public int? ClientTopicID8 { get; set; }
        public int? ClientTopicID9 { get; set; }
        public int? ClientTopicID10 { get; set; }
        public int? ClientTopicID11 { get; set; }
        public int? ClientTopicID12 { get; set; }
        public int? ClientTopicID13 { get; set; }
        public int? ClientTopicID14 { get; set; }
        public int? ClientTopicID15 { get; set; }
        public int? ClientTopicID16 { get; set; }
        public int? ClientTopicID17 { get; set; }
        public int? ClientTopicID18 { get; set; }
        public int? ClientTopicID19 { get; set; }
        public int? ClientTopicID20 { get; set; }
        public int? ClientTopicID21 { get; set; }
        public int? ClientTopicID22 { get; set; }
        public int? ClientTopicID23 { get; set; }
        public int? ClientTopicID24 { get; set; }
        public int? ClientTopicID25 { get; set; }
        public int? ClientTopicID26 { get; set; }
        public int? ClientTopicID27 { get; set; }
        public int? ClientTopicID28 { get; set; }
        public int? ClientTopicID29 { get; set; }
        public int? ClientTopicID30 { get; set; }
        public int? ClientTopicID31 { get; set; }
        public int? ClientTopicID32 { get; set; }
        public int? ClientTopicID33 { get; set; }
        public int? ClientTopicID34 { get; set; }
        public int? ClientTopicID35 { get; set; }
        public int? ClientTopicID36 { get; set; }
        public int? ClientTopicID37 { get; set; }
        public int? ClientTopicID38 { get; set; }
        public int? ClientTopicID39 { get; set; }
        public int? ClientTopicID40 { get; set; }
        public int? ClientTopicID41 { get; set; }
        public int? ClientTopicID42 { get; set; }
        public int? ClientTopicID43 { get; set; }
        public int? ClientTopicID44 { get; set; }
        public int? ClientTopicID45 { get; set; }
        public int? ClientTopicID46 { get; set; }
        public int? ClientTopicID47 { get; set; }
        public int? ClientTopicID48 { get; set; }
        public int? ClientTopicID49 { get; set; }
        public int? ClientTopicID50 { get; set; }
        public int? ClientTopicID51 { get; set; }
        public int? ClientTopicID52 { get; set; }
        public int? ClientTopicID53 { get; set; }
        public int? ClientTopicID54 { get; set; }
        public int? ClientTopicID55 { get; set; }
        public int? ClientTopicID56 { get; set; }
        public int? ClientTopicID57 { get; set; }
        public int? ClientTopicID58 { get; set; }
        public int? ClientTopicID59 { get; set; }
        public int? ClientTopicID60 { get; set; }
        public int? ClientTopicID61 { get; set; }
        public int? ClientTopicID62 { get; set; }
        public int? ClientTopicID63 { get; set; }
        public int? ClientTopicID64 { get; set; }
        public int? ClientTopicID65 { get; set; }
        public int? ClientTopicID66 { get; set; }
        public int? ClientTopicID67 { get; set; }
        public int? ClientTopicID68 { get; set; }
        public int? ClientTopicID69 { get; set; }
        public int? ClientTopicID70 { get; set; }
        public int? ClientTopicID71 { get; set; }
        public int? ClientTopicID72 { get; set; }
        public int? ClientTopicID73 { get; set; }
        public int? ClientTopicID74 { get; set; }

        public int? ClientContactHoursSpent1 { get; set; }
        public int? ClientContactHoursSpent2 { get; set; }
        public int? ClientContactHoursSpent3 { get; set; }
        public int? ClientContactHoursSpent4 { get; set; }
        public int? ClientContactHoursSpent5 { get; set; }
        public string ClientContactHoursSpent6 { get; set; }

        public int? ClientContactCurrentStatus1 { get; set; }
        public int? ClientContactCurrentStatus2 { get; set; }
        public int? ClientContactCurrentStatus3 { get; set; }
        public int? ClientContactCurrentStatus4 { get; set; }
        public int? ClientContactCurrentStatus5 { get; set; }
        public int? ClientContactCurrentStatus6 { get; set; }

        #region Implementation of IModified

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        #endregion
    }
}
