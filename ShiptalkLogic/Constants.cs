namespace ShiptalkLogic.Constants.Tables
{
    public static class County
    {
        public const string CBSACODE = "CBSACODE";
        public const string CBSALSAD = "CBSALSAD";
        public const string CBSATITLE = "CBSATITLE";
        public const string CountyFIPS = "CountyFIPS";
        public const string CountyNameLong = "CountyNameLong";
        public const string CountyNameMedium = "CountyNameMedium";
        public const string CountyNameShort = "CountyNameShort";
        public const string CreatedDate = "CreatedDate";
        public const string LastUpdatedBy = "LastUpdatedBy";
        public const string LastUpdatedDate = "LastUpdatedDate";
        public const string LAT = "LAT";
        public const string LONG = "LONG";
        public const string StateFIPS = "StateFIPS";
        public const string WEBLINKTEXT = "WEBLINKTEXT";
    }

    public static class Agency
    {
        public const string ActiveInactiveDate = "ActiveInactiveDate";
        public const string AgencyCode = "AgencyCode";
        public const string AgencyId = "AgencyId";
        public const string AgencyName = "AgencyName";
        public const string AgencyTypeID = "AgencyTypeID";
        public const string Comments = "Comments";
        public const string CreatedBy = "CreatedBy";
        public const string CreatedDate = "CreatedDate";
        public const string IsActive = "IsActive";
        public const string LastUpdatedBy = "LastUpdatedBy";
        public const string LastUpdatedDate = "LastUpdatedDate";
        public const string SponsorFirstName = "SponsorFirstName";
        public const string SponsorLastName = "SponsorLastName";
        public const string SponsorMiddleName = "SponsorMiddleName";
        public const string SponsorTitle = "SponsorTitle";
        public const string StateFIPS = "StateFIPS";
        public const string URL = "URL";
        public const string HideAgencyFromPublic = "HideAgencyFromPublic";
    }

    public static class Descriptor
    {
        public const string DescriptorID = "DescriptorID";
        public const string Description = "Description";
    }

    public static class AgencyAddress
    {
        public const string Address1 = "Address1";
        public const string Address2 = "Address2";
        public const string AgencyAddressID = "AgencyAddressID";
        public const string City = "City";
        public const string CountyFIPS = "CountyFIPS";
        public const string CreatedBy = "CreatedBy";
        public const string CreatedDate = "CreatedDate";
        public const string LastUpdatedBy = "LastUpdatedBy";
        public const string LastUpdatedDate = "LastUpdatedDate";
        public const string MailingAddress1 = "MailingAddress1";
        public const string MailingAddress2 = "MailingAddress2";
        public const string MailingAddressID = "MailingAddressID";
        public const string MailingCity = "MailingCity";
        public const string MailingStateFIPS = "MailingStateFIPS";
        public const string MailingZip = "MailingZip";
        public const string PhysicalAddress1 = "PhysicalAddress1";
        public const string PhysicalAddress2 = "PhysicalAddress2";
        public const string PhysicalAddressID = "PhysicalAddressID";
        public const string PhysicalCity = "PhysicalCity";
        public const string PhysicalCountyFIPS = "PhysicalCountyFIPS";
        public const string PhysicalCountyShortName = "PhysicalCountyShortName";
        public const string PhysicalStateFIPS = "PhysicalStateFIPS";
        public const string PhysicalZip = "PhysicalZip";
        public const string StateFIPS = "StateFIPS";
        public const string Zip = "Zip";

        //Added by Lavanya
        public const string Longitude = "Longitude";
        public const string Latitude = "Latitude";
        //end
    }

    public static class AgencyLocation
    {
        public const string ActiveInactiveDate = "ActiveInactiveDate";
        public const string AgencyID = "AgencyID";
        public const string AgencyLocationID = "AgencyLocationID";
        public const string Comments = "Comments";
        public const string ContactFirstName = "ContactFirstName";
        public const string ContactLastName = "ContactLastName";
        public const string ContactMiddleName = "ContactMiddleName";
        public const string ContactTitle = "ContactTitle";
        public const string CreatedBy = "CreatedBy";
        public const string CreatedDate = "CreatedDate";
        public const string Fax = "Fax";
        public const string HoursOfOperation = "HoursOfOperation";
        public const string IsActive = "IsActive";
        public const string IsMainOffice = "IsMainOffice";
        public const string LastUpdatedBy = "LastUpdatedBy";
        public const string LastUpdatedDate = "LastUpdatedDate";
        public const string LocationName = "LocationName";
        public const string MailingAddressID = "MailingAddressID";
        public const string PhysicalAddressID = "PhysicalAddressID";
        public const string PrimaryEmail = "PrimaryEmail";
        public const string PrimaryPhone = "PrimaryPhone";
        public const string SecondaryEmail = "SecondaryEmail";
        public const string SecondaryPhone = "SecondaryPhone";
        public const string TDD = "TDD";
        public const string TollFreePhone = "TollFreePhone";
        public const string TollFreeTDD = "TollFreeTDD";
        //Added by Lavanya
        public const string AvailableLanguages = "AvailableLanguages";
        public const string HideAgencyFromSearch = "HideAgencyFromSearch";
        //end
    }

    public static class AuthorizedRoutes
    {
        public const string AdminRequired = "AdminRequired";
        public const string RouteName = "RouteName";
        public const string ScopeId = "ScopeId";
        public const string UserId = "UserId";
    }

    public static class SubStateRegion
    {
        public const string SubStateRegionID = "SubStateRegionID";
        public const string Name = "Name";
        public const string StateFIPS = "StateFIPS";
        public const string CreatedBy = "CreatedBy";
        public const string CreatedDate = "CreatedDate";
        public const string LastUpdatedBy = "LastUpdatedBy";
        public const string LastUpdatedDate = "LastUpdatedDate";
        public const string IsActive = "IsActive";
        public const string ActiveInactiveDate = "ActiveInactiveDate";
    }

    public static class SHIPProfile
    {
        public const string ID = "ID";
        public const string StateName = "StateName";
        public const string ProgramName = "ProgramName";
        public const string ProgramWebsite = "ProgramWebsite";
        public const string ProgramSummary = "ProgramSummary";

        public const string BeneficiaryContactPhoneTollFree = "BeneficiaryContactPhoneTollFree";
        public const string BeneficiaryContactPhoneTollFreeInStateOnly = "BeneficiaryContactPhoneTollFreeInStateOnly";
        public const string BeneficiaryContactPhoneTollLine = "BeneficiaryContactPhoneTollLine";
        public const string BeneficiaryContactWebsite = "BeneficiaryContactWebsite";
        public const string BeneficiaryContactTDDLine = "BeneficiaryContactTDDLine";
        public const string BeneficiaryContactEmail = "BeneficiaryContactEmail";
        public const string BeneficiaryContactHours = "BeneficiaryContactHours";

        public const string AdminAgencyContactName = "AdminAgencyContactName";
        public const string AdminAgencyName = "AdminAgencyName";
        public const string AdminAgencyContactTitle = "AdminAgencyContactTitle";
        public const string AdminAgencyAddress = "AdminAgencyAddress";
        public const string AdminAgencyCity = "AdminAgencyCity";
        public const string AdminAgencyZipcode = "AdminAgencyZipcode";
        public const string AdminAgencyPhone = "AdminAgencyPhone";
        public const string AdminAgencyFax = "AdminAgencyFax";
        public const string AdminAgencyEmail = "AdminAgencyEmail";

        public const string ProgramCoordinatorName = "ProgramCoordinatorName";
        public const string ProgramCoordinatorAddress = "ProgramCoordinatorAddress";
        public const string ProgramCoordinatorCity = "ProgramCoordinatorCity";
        public const string ProgramCoordinatorZipcode = "ProgramCoordinatorZipcode";
        public const string ProgramCoordinatorPhone = "ProgramCoordinatorPhone";
        public const string ProgramCoordinatorFax = "ProgramCoordinatorFax";
        public const string ProgramCoordinatorEmail = "ProgramCoordinatorEmail";

        public const string NumberOfVolunteerCounselors = "NumberOfVolunteerCounselors";
        public const string NumberOfStateStaff = "NumberOfStateStaff";
        public const string TotalCounties = "TotalCounties";
        public const string NumberOfCountiesServed = "NumberOfCountiesServed";
        public const string NumberOfSponsors = "NumberOfSponsors";
        public const string AvailableLanguages = "AvailableLanguages";

        //New fields: added by Lavanya Maram - 04/23/2013

        public const string StateOversightAgency = "StateOversightAgency";
        public const string NumberOfPaidStaff = "NumberOfPaidStaff";
     	public const string NumberOfCoordinators = "NumberOfCoordinators";
        public const string NumberOfCertifiedCounselors = "NumberOfCertifiedCounselors";
        public const string NumberOfEligibleBeneficiaries = "NumberOfEligibleBeneficiaries";
        public const string NumberOfBeneficiaryContacts = "NumberOfBeneficiaryContacts";
        public const string LocalAgencies = "LocalAgencies";
        public const string Longitude = "Longitude";
        public const string Latitude = "Latitude";

    }
	
	public static class User
    {
        public const string UserId = "UserId";
        public const string OldUserID = "OldUserID";
        public const string StateFIPS = "StateFIPS";
        public const string ScopeID = "ScopeID";
        public const string CounselingLocation = "CounselingLocation";
        public const string CountyOfCounselingCounty = "CountyOfCounselingCounty";
        public const string IsAdmin = "IsAdmin";
        public const string Password = "Password";
        public const string FirstName = "FirstName";
        public const string MiddleName = "MiddleName";
        public const string LastName = "LastName";
        public const string PrevLastName = "PrevLastName";
        public const string Nickname = "Nickname";
        public const string Suffix = "Suffix";
        public const string Honorifics = "Honorifics";
        public const string PrimaryPhone = "PrimaryPhone";
        public const string SecondaryPhone = "SecondaryPhone";
        public const string PrimaryEmail = "PrimaryEmail";
        public const string SecondaryEmail = "SecondaryEmail";
        public const string IsActive = "IsActive";
        public const string ActiveInactiveDate = "ActiveInactiveDate";
        public const string ApprovedBy = "ApprovedBy";
        public const string ApprovedDate = "ApprovedDate";
        public const string MedicareUniqueID = "MedicareUniqueID";
        public const string OldMedicareUniqueID = "OldMedicareUniqueID";
        public const string CreatedBy = "CreatedBy";
        public const string CreatedDate = "CreatedDate";
        public const string LastUpdatedBy = "LastUpdatedBy";
        public const string LastUpdatedDate = "LastUpdatedDate";
        public const string IsRegistrationRequest = "IsRegistrationRequest";
        public const string RequestedAgencyID = "RequestedAgencyID";
        public const string EmailVerificationToken = "EmailVerificationToken";
        public const string EmailVerificationTimeStamp = "EmailVerificationTimeStamp";
        public const string FailedLoginAttemptsCount = "FailedLoginAttemptsCount";
        public const string LastLoginAttempt = "LastLoginAttempt";
        public const string IsLocked = "IsLocked";
        public const string FirstFailedLoginAttempt = "FirstFailedLoginAttempt";
        public const string LastFailedLoginAttempt = "LastFailedLoginAttempt";
        public const string PasswordResetToken = "PasswordResetToken";
        public const string PasswordResetTimeStamp = "PasswordResetTimeStamp";
	    public const string RegionName = "RegionName";
    }

	public static class SpecialField
    {
        public const string PasswordResetTimeStamp = "PasswordResetTimeStamp";
        public const string SpecialFieldID = "SpecialFieldID";
        public const string Name = "Name";
        public const string StateFIPS = "StateFIPS";
        public const string FormType = "FormType";
        public const string StartDate = "StartDate";
        public const string EndDate = "EndDate";
        public const string Description = "Description";
        public const string ValidationType = "ValidationType";
        public const string IsRequired = "IsRequired";
        public const string CreatedBy = "CreatedBy";
        public const string CreatedDate = "CreatedDate";
        public const string LastUpdatedBy = "LastUpdatedBy";
        public const string LastUpdatedDate = "LastUpdatedDate";
        public const string Ordinal = "Ordinal";       
        public const string Range = "Range";
       
    }

    public static class ClientContactSpecialField
    {
        public const string ClientContactSpecialFieldID = "ClientContactSpecialFieldID";
        public const string ClientContactID = "ClientContactID";
        public const string SpecialFieldID = "SpecialFieldID";
        public const string SpecialFieldValue = "SpecialFieldValue";
        public const string CreatedBy = "CreatedBy";
        public const string CreatedDate = "CreatedDate";
        public const string LastUpdatedBy = "LastUpdatedBy";
        public const string LastUpdatedDate = "LastUpdatedDate";
    }

    public static class ClientContact
    {
        public const string ClientContactID = "ClientContactID";
        public const string CounselorUserID = "CounselorUserID";
        public const string SubmitterUserID = "SubmitterUserID";
        public const string ReviewerUserID = "ReviewerUserID";
        public const string ReviewedDate = "ReviewedDate";
        public const string Version = "Version";
        public const string IsBatchUploadData = "IsBatchUploadData";
        public const string AgencyID = "AgencyID";
        public const string CountyOfCounselorLocation = "CountyOfCounselorLocation";
        public const string CountyNameOfCounselorLocation = "CountyNameOfCounselorLocation";
        public const string ZIPCodeOfCounselorLocation = "ZIPCodeOfCounselorLocation";
        public const string ClientID = "ClientID";
        public const string DateOfContact = "DateOfContact";
        public const string StateSpecificClientID = "StateSpecificClientID";
        public const string HowLearnedSHIP = "HowLearnedSHIP";
        public const string FirstVSContinuingService = "FirstVSContinuingService";
        public const string MethodOfContact = "MethodOfContact";
        public const string ClientFirstName = "ClientFirstName";
        public const string ClientLastName = "ClientLastName";
        public const string ClientPhone = "ClientPhone";
        public const string RepresentativeFirstName = "RepresentativeFirstName";
        public const string RepresentativeLastName = "RepresentativeLastName";
        public const string ZIPCodeOfClientRes = "ZIPCodeOfClientRes";
        public const string CountycodeOfClientRes = "CountycodeOfClientRes";
        public const string CountyNameOfClientRes = "CountyNameOfClientRes";
        public const string ClientAgeGroup = "ClientAgeGroup";
        public const string ClientGender = "ClientGender";
        public const string PrimaryLanguageOtherThanEnglish = "PrimaryLanguageOtherThanEnglish";
        public const string MonthlyIncome = "MonthlyIncome";
        public const string ClientAsset = "ClientAsset";
        public const string Disability = "Disability";
        public const string DualMental = "DualMental";
        public const string HoursSpent = "HoursSpent";
        public const string MinutesSpent = "MinutesSpent";
        public const string CurrentStatus = "CurrentStatus";
        public const string Comments = "Comments";
        public const string CreatedDate = "CreatedDate";
        public const string LastUpdatedBy = "LastUpdatedBy";
        public const string LastUpdatedDate = "LastUpdatedDate";
    }
    
    public static class ClientTopic
    {
        public const string ClientTopicID = "ClientTopicID";
        public const string Description = "Description";
        public const string GroupID = "GroupID";
        public const string GroupDescription = "GroupDescription";
        public const string OtherDescription = "OtherDescription";
    }

    public static class ClientRace
    {
        public const string ClientRaceID = "ClientRaceID";
        public const string ClientContactID = "ClientContactID";
        public const string RaceID = "RaceID";
        public const string CreatedDate = "CreatedDate";
    }
    public static class PAMSummary
    {
        public const string TotalEventsAndActivities = "TotalEventsAndActivities";

        public const string InteractivePresentationstoPublicFacetoFaceInPersonNumberofEvents = "Interactive Presentations to Public - Face to Face In-Person - Number of Events";
        public const string InteractivePresentationstoPublicEstimatedNumberofAttendees = "Interactive Presentations to Public - Estimated Number of Attendees";
        public const string InteractivePresentationstoPublicEstimatedPersonsProvidedEnrollmentAssistance = "Interactive Presentations to Public - Estimated Persons Provided Enrollment Assistance";

        public const string BoothorExhibitAtHeathFairSeniorFairorSpecialEventNumberofEvents = "Booth or Exhibit At Heath Fair, Senior Fair, or Special Event - Number of Events";
        public const string BoothorExhibitEstimatedNumberofDirectInteractionswithAttendees = "Booth or Exhibit - Estimated Number of Direct Interactions with Attendees";
        public const string BoothorExhibitEstimatedPersonsProvidedEnrollmentAssistance = "Booth or Exhibit - Estimated Persons Provided Enrollment Assistance";

        public const string DedicatedEnrollmentEventSponsoredBySHIPOrInPartnershipNumberofEvents = "Dedicated Enrollment Event Sponsored By SHIP or in Partnership - Number of Events";
        public const string DedicatedEnrollmentEventEstNumberPersonsReachedatEventRegardlessofEnrollAssistance = "Dedicated Enrollment Event - Est Number Persons Reached at Event Regardless of Enroll Assistance";
        public const string DedicatedEnrollmentEventEstimatedNumberPersonsProvidedAnyEnrollmentAssistance = "Dedicated Enrollment Event - Estimated Number Persons Provided Any Enrollment Assistance";
        public const string DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithPartD = "Dedicated Enrollment Event - Estimated Number Provided Enrollment Assistance with Part D";
        public const string DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithLIS = "Dedicated Enrollment Event - Estimated Number Provided Enrollment Assistance with LIS";
        public const string DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithMSP = "Dedicated Enrollment Event - Estimated Number Provided Enrollment Assistance with MSP";
        public const string DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistOtherMedicareProgram = "Dedicated Enrollment Event - Estimated Number Provided Enrollment Assist Other Medicare Program";

        public const string RadioShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents = "Radio Show Live or Taped - Not a Public Service Announce or Ad - Number of Events";
        public const string RadioShowLiveorTapedEstimatedNumberofListenersReached = "Radio Show Live or Taped - Estimated Number of Listeners Reached";

        public const string TVorCableShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents = "TV or Cable Show Live or Taped - Not a Public Service Announce or Ad - Number of Events";
        public const string TVorCableShowLiveorTapedEstimatedNumberofViewersReached = "TV or Cable Show Live or Taped - Estimated Number of Viewers Reached";

        public const string ElectronicOtherActivityPSAsElectronicAdsCrawlsVideoConfWebConfWebChatEvents = "Electronic Other Activity - PSAs, Electronic Ads, Crawls, Video Conf, Web Conf, Web Chat - Events";
        public const string ElectronicOtherActivityEstPersonsViewingorListeningtoElectronicOtherActivityAcrossCampaign = "Electronic Other Activity - Est Persons Viewing or Listening to Electronic Other Activity Across Campaign";

        public const string PrintOtherActivityNewspaperNewsletterPamphletsFliersPostersTargetedMailingsEvents = "Print Other Activity - Newspaper, Newsletter, Pamphlets, Fliers, Posters, Targeted Mailings - Events";
        public const string PrintOtherActivityEstPersonsReadingorReceivingPrintedMaterialsAcrossEntireCampaign = "Print Other Activity - Est Persons Reading or Receiving Printed Materials Across Entire Campaign";

        public const string NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoInteractivePresentations = "Number of Unduplicated SHIP Staff and Affiliated Partners Contributing to Interactive Presentations";
        public const string TotalPersonHoursofEffortSpentonInteractivePresentationstoPublic = "Total Person-Hours of Effort Spent on Interactive Presentations to Public";

        public const string NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoBoothsandExhibits = "Number of Unduplicated SHIP Staff and Affiliated Partners Contributing to Booths and Exhibits";
        public const string TotalPersonHoursofEffortSpentonBoothsandExhibits = "Total Person-Hours of Effort Spent on Booths and Exhibits";

        public const string NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoEnrollmentEvents = "Number of Unduplicated SHIP Staff and Affiliated Partners Contributing to Enrollment Events";
        public const string TotalPersonHoursofEffortSpentonEnrollmentEvents = "Total Person-Hours of Effort Spent on Enrollment Events";

        public const string NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoRadioEvents = "Number of Unduplicated SHIP Staff and Affiliated Partners Contributing to Radio Events";
        public const string TotalPersonHoursofEffortSpentonRadioEvents = "Total Person-Hours of Effort Spent on Radio Events";

        public const string NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoTelevisionEvents = "Number of Unduplicated SHIP Staff and Affiliated Partners Contributing to Television Events";
        public const string TotalPersonHoursofEffortSpentonTelevisionEvents = "Total Person-Hours of Effort Spent on Television Events";

        public const string NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoElectronicOtherActivities = "Number of Unduplicated SHIP Staff and Affiliated Partners Contributing to Electronic Other Activities";
        public const string TotalPersonHoursofEffortSpentonElectronicOtherActivities = "Total Person-Hours of Effort Spent on Electronic Other Activities";

        public const string NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoPrintOtherActivities = "Number of Unduplicated SHIP Staff and Affiliated Partners Contributing to Print Other Activities";
        public const string TotalPersonHoursofEffortSpentonPrintOtherActivities = "Total Person-Hours of Effort Spent on Print Other Activities";

        public const string NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoAllEventsActivities = "Number of Unduplicated SHIP Staff and Affiliated Partners Contributing to All Events-Activities";
        public const string TotalPersonHoursofEffortSpentonAllEventsActivities = "Total Person-Hours of Effort Spent on All Events-Activities";

        public const string MedicarePartsAandBTopicFocus = "Medicare Parts A and B [Topic Focus]";
        public const string PlanIssuesNonRenewalTerminationEmployerCOBRA = "Plan Issues - Non-Renewal, Termination, Employer-COBRA";
        public const string LongTermCare = "Long-Term Care";
        public const string MedigapMedicareSupplements = "Medigap - Medicare Supplements";
        public const string MedicareFraudandAbuse = "Medicare Fraud and Abuse";
        public const string MedicarePrescriptionDrugCoveragePDPMAPD = "Medicare Prescription Drug Coverage - PDP / MA-PD";
        public const string OtherPrescriptionDrugCoverageAssistance = "Other Prescription Drug Coverage - Assistance";
        public const string MedicareAdvantageHealthPlans = "Medicare Advantage - Health Plans";
        public const string QMBSLMBQI = "QMB - SLMB - QI";
        public const string OtherMedicaid = "Other Medicaid";
        public const string GeneralSHIPProgramInformation = "General SHIP Program Information";
        public const string MedicarePreventiveServices = "Medicare Preventive Services";
        public const string LowIncomeAssistance = "Low-Income Assistance";
        public const string DualEligiblewithMentalIllnessMentalDisability = "Dual Eligible with Mental Illness Mental Disability";
        public const string VolunteerRecruitment = "Volunteer Recruitment";
        public const string PartnershipRecruitment = "Partnership Recruitment";
        public const string OtherTopics = "Other Topics";

        public const string MedicarePreEnrolleesAge4564TargetAudience = "Medicare Pre-Enrollees - Age 45-64 [Target Audience]";
        public const string MedicareBeneficiaries = "Medicare Beneficiaries";
        public const string FamilyMembersCaregiversofMedicareBeneficiaries = "Family Members - Caregivers of Medicare Beneficiaries";
        public const string LowIncome = "Low-Income";
        public const string HispanicLatinoorSpanishOrigin = "Hispanic, Latino, or Spanish Origin";
        public const string WhiteNonHispanic = "White, Non-Hispanic";
        public const string BlackorAfricanAmerican = "Black or African-American";
        public const string AmericanIndianorAlaskaNative = "American Indian or Alaska Native";
        public const string AsianIndian = "Asian Indian";
        public const string Chinese = "Chinese";
        public const string Filipino = "Filipino";
        public const string Japanese = "Japanese";
        public const string Korean = "Korean";
        public const string Vietnamese = "Vietnamese";
        public const string NativeHawaiian = "Native Hawaiian";
        public const string GuamanianorChamorro = "Guamanian or Chamorro";
        public const string Samoan = "Samoan";
        public const string OtherAsian = "Other Asian";
        public const string OtherPacificIslander = "Other Pacific Islander";
        public const string SomeOtherRaceEthnicity = "Some Other Race Ethnicity";
        public const string Disabled = "Disabled";
        public const string Rural = "Rural";
        public const string EmployerRelatedGroups = "Employer-Related Groups";
        public const string MentalHealthProfessionals = "Mental Health Professionals";
        public const string SocialWorkProfessionals = "Social Work Professionals";
        public const string DualEligibleGroups = "Dual-Eligible Groups";
        public const string PartnershipOutreach = "Partnership Outreach";
        public const string PresentationstoGroupsinLanguagesOtherThanEnglish = "Presentations to Groups in Languages Other Than English";
        public const string OtherAudiences = "Other Audiences";
        public const string OldMedicareBeneficiariesandorPreEnrollees = "Old Medicare Beneficiaries and/or Pre-Enrollees";
        public const string OldAsian = "Old Asian";
        public const string OldNativeHawaiianorOtherPacificIslander = "Old Native Hawaiian or Other Pacific Islander";
    }
    public static class CCSummaryReport
    {
        public const string TotalClientContacts = "Total Client Contacts";
        public const string ClientFirstVsContinuingContact1 = "First Contact for the Client's Issue";
        public const string ClientFirstVsContinuingContact2 = "Continuing Contacts for the Client's Issue";
        public const string ClientFirstVsContinuingContact3 = "First vs Continuing Missing, Blank, Not Collected, Invalid, Miscoded, Out of Range";

        public const string ClientLearnedAboutSHIP1 = "Client Learned About SHIP From Previous Contact with a SHIP";
        public const string ClientLearnedAboutSHIP2 = "Client Learned About SHIP From CMS / Medicare Website Brochures Mailings 1-800";
        public const string ClientLearnedAboutSHIP3 = "Client Learned About SHIP From Presentations or Fairs";
        public const string ClientLearnedAboutSHIP4 = "Client Learned About SHIP From State-Specific Mailings, Brochures, Posters";
        public const string ClientLearnedAboutSHIP5 = "Client Learned About SHIP From Another Agency - Social Security, Senior Org, Disability Org";
        public const string ClientLearnedAboutSHIP6 = "Client Learned About SHIP From Friend or Relative";
        public const string ClientLearnedAboutSHIP7 = "Client Learned About SHIP From Media - PSA Ad Newspaper Radio TV";
        public const string ClientLearnedAboutSHIP8 = "Client Learned About SHIP From State Website";
        public const string ClientLearnedAboutSHIP9 = "Client Learned About SHIP From Some Other Method";
        public const string ClientLearnedAboutSHIP10 = "How Learned Missing, Blank, Not Collected, Invalid, Miscoded, Out of Range";

        public const string ClientMethodOfContact1 = "How Learned Missing, Blank, Not Collected, Invalid, Miscoded, Out of Range";
        public const string ClientMethodOfContact2 = "Face to Face Contact at Counseling Location or Event Site";
        public const string ClientMethodOfContact3 = "Face to Face Contact at Client's Home or Facility";
        public const string ClientMethodOfContact4 = "Email Contact";
        public const string ClientMethodOfContact5 = "Postal Mail or Fax Contact";
        public const string ClientMethodOfContact6 = "Old Email-Fax-Postal";
        public const string ClientMethodOfContact7 = "Old Unknown";
        public const string ClientMethodOfContact8 = "Method of Contact Missing, Blank, Not Collected, Invalid, Miscoded, Out of Range";

        public const string ClientAgeGroup1 = "Client Age 64 or Younger";
        public const string ClientAgeGroup2 = "Client Age 65-74";
        public const string ClientAgeGroup3 = "Client Age 75-84";
        public const string ClientAgeGroup4 = "Client Age 85 or Older";
        public const string ClientAgeGroup5 = "Client Age Missing, Blank, Not Collected, Invalid, Miscoded, Out of Range";

        public const string ClientGender1 = "Client Female";
        public const string ClientGender2 = "Client Male";
        public const string ClientGender3 = "Client Gender Missing, Blank, Not Collected, Invalid, Miscoded, Out of Range";

        public const string ClientContactRace1 = "Client Any Mention of Hispanic, Latino, or Spanish Origin [Can Select More Than One]";
        public const string ClientContactRace2 = "Client Any Mention of White, Non-Hispanic [Can Select More Than One]";
        public const string ClientContactRace3 = "Client Any Mention of Black, African American [Can Select More Than One]";
        public const string ClientContactRace4 = "Client Any Mention of American Indian or Alaska Native [Can Select More Than One]";
        public const string ClientContactRace5 = "Client Any Mention of Asian Indian [Can Select More Than One]";
        public const string ClientContactRace6 = "Client Any Mention of Chinese [Can Select More Than One]";
        public const string ClientContactRace7 = "Client Any Mention of Filipino [Can Select More Than One]";
        public const string ClientContactRace8 = "Client Any Mention of Japanese [Can Select More Than One]";
        public const string ClientContactRace9 = "Client Any Mention of Korean [Can Select More Than One]";
        public const string ClientContactRace10 = "Client Any Mention of Vietnamese [Can Select More Than One]";
        public const string ClientContactRace11 = "Client Any Mention of Native Hawaiian [Can Select More Than One]";
        public const string ClientContactRace12 = "Client Any Mention of Guamanian or Chamorro [Can Select More Than One]";
        public const string ClientContactRace13 = "Client Any Mention of Samoan [Can Select More Than One]";
        public const string ClientContactRace14 = "Client Any Mention of Other Asian [Can Select More Than One]";
        public const string ClientContactRace15 = "Client Any Mention of Other Pacific Islander [Can Select More Than One]";
        public const string ClientContactRace16 = "Client Any Mention of Some Other Race-Ethnicity [Can Select More Than One]";
        public const string ClientContactRace17 = "Client Old Asian Code [Single Choice]";
        public const string ClientContactRace18 = "Client Old Native Hawaiian or other Pacific Islander Code [Single Choice]";
        public const string ClientContactRace19 = "Client Old Other Code [Single Choice]";
        public const string ClientContactRace20 = "Client Race-Ethnicity Missing, Blank, Not Collected, Invalid, Miscoded, Out of Range";
        public const string ClientContactRace21 = "Client Selected More Than One Race-Ethnicity Category [Can Select More Than One]";

        public const string ClientPrimaryLanguageOtherThanEnglish1 = "Client's Primary  Language is Other Than English";
        public const string ClientPrimaryLanguageOtherThanEnglish2 = "English is Client's Primary Language";
        public const string ClientPrimaryLanguageOtherThanEnglish3 = "Client 's Primary Language  Missing, Blank, Not Collected, Invalid, Miscoded, Out of Range";

        public const string ClientMonthlyIncome1 = "Client's Income (or Client Plus Spouse's Income) is Below 150% of Federal Poverty Level";
        public const string ClientMonthlyIncome2 = "Client's Income (or Client Plus Spouse's Income) is At or Above 150% of FPL";
        public const string ClientMonthlyIncome3 = "Client's Income Missing, Blank, Not Collected, Invalid, Miscoded, Out of Range";

        public const string ClientAssests1 = "Client's Assets are Below LIS Asset Limits";
        public const string ClientAssests2 = "Client's Assets are Above LIS Asset Limits";
        public const string ClientAssests3 = "Client's Assets Missing, Blank, Not Collected, Invalid, Miscoded, Out of Range";

        public const string ClientReceivingSSOrMedicareDisability1 = "Client is Receiving or Applying for Social Security Disability or Medicare Disability";
        public const string ClientReceivingSSOrMedicareDisability2 = "Client is Neither Receiving Nor Applying for Social Security Disability or Medicare Disability";
        public const string ClientReceivingSSOrMedicareDisability3 = "Client's Disabled Prog Status Missing, Blank, Not Collected, Invalid, Miscoded, Out of Range";

        public const string ClientDualEligble1 = "Client is Dual Eligible Medicare-Medicaid with Mental Illness / Mental Disability [DMD]";
        public const string ClientDualEligble2 = "Client is Not Dual Eligible Medicare-Medicaid with Mental Illness / Mental Disability [DMD]";
        public const string ClientDualEligble3 = "Client's DMD Status Missing, Blank, Not Collected, Invalid, Miscoded, Out of Range";

        public const string ClientTopicID1 = "Medicare Prescription Drug Coverage (Part D) - Eligibility/Screening [Topic]";
        public const string ClientTopicID2 = "Medicare Prescription Drug Coverage (Part D ) - Benefit Explanation";
        public const string ClientTopicID3 = "Medicare Prescription Drug Coverage (Part D) - Plans Comparison";
        public const string ClientTopicID4 = "Medicare Prescription Drug Coverage (Part D) - Plan Enrollment/Disenrollment";
        public const string ClientTopicID5 = "Medicare Prescription Drug Coverage (Part D) - Claims/Billing";
        public const string ClientTopicID6 = "Medicare Prescription Drug Coverage (Part D) - Appeals/Grievances";
        public const string ClientTopicID7 = "Medicare Prescription Drug Coverage (Part D) - Fraud and Abuse";
        public const string ClientTopicID8 = "Medicare Prescription Drug Coverage (Part D) - Marketing/Sales Complaints or Issues";
        public const string ClientTopicID9 = "Medicare Prescription Drug Coverage (Part D) - Quality of Care";
        public const string ClientTopicID10 = "Medicare Prescription Drug Coverage (Part D) - Plan Non-Renewal";
        public const string ClientTopicID62 = "Medicare Prescription Drug Coverage (Part D) - Old Plan Eligibility, Benefits Comparisons";
        public const string ClientTopicID63 = "Medicare Prescription Drug Coverage (Part D) - Old Appeals, Quality of Care, Complaints";

        public const string ClientTopicID11 = "Part D Low Income Subsidy (LIS/Extra Help) - Eligibility/Screening";
        public const string ClientTopicID12 = "Part D Low Income Subsidy (LIS/Extra Help) - Benefit Explanation";
        public const string ClientTopicID13 = "Part D Low Income Subsidy (LIS/Extra Help) - Application Assistance";
        public const string ClientTopicID14 = "Part D Low Income Subsidy (LIS/Extra Help) - Claims/Billing";
        public const string ClientTopicID15 = "Part D Low Income Subsidy (LIS/Extra Help) - Appeals/Grievances";
        public const string ClientTopicID64 = "Part D Low Income Subsidy (LIS/Extra Help) - Old Low Income Assist - Eligibil, Benefit Comp";

        public const string ClientTopicID16 = "Other Prescription Assistance - Union/Employer Plan";
        public const string ClientTopicID17 = "Other Prescription Assistance - Military Drug Benefits";
        public const string ClientTopicID18 = "Other Prescription Assistance - Manufacturer Programs";
        public const string ClientTopicID19 = "Other Prescription Assistance - State Pharmaceutical Assistance Programs";
        public const string ClientTopicID20 = "Other Prescription Assistance - Other";
        public const string ClientTopicID65 = "Other Prescription Assistance - Old Medicare-Approved Drug Discount Card";
        public const string ClientTopicID66 = "Other Prescription Assistance - Old Discount Plans";

        public const string ClientTopicID21 = "Medicare (Parts A & B) - Eligibility";
        public const string ClientTopicID22 = "Medicare (Parts A & B) - Benefit Explanation";
        public const string ClientTopicID23 = "Medicare (Parts A & B) - Claims/Billing";
        public const string ClientTopicID24 = "Medicare (Parts A & B) - Appeals/Grievances";
        public const string ClientTopicID25 = "Medicare (Parts A & B) - Fraud and Abuse";
        public const string ClientTopicID26 = "Medicare (Parts A & B) - Quality of Care";
        public const string ClientTopicID60 = "Medicare (Parts A & B) - Old Enrolment, Eligibility, Benefits";
        public const string ClientTopicID61 = "Medicare (Parts A & B) - Old Appeal-Quality of Care-Complaints";

        public const string ClientTopicID27 = "Medicare Advantage (HMO, POS, PPO, PFFS, SNP, MSA, Cost) - Eligibility/Screening";
        public const string ClientTopicID28 = "Medicare Advantage (HMO, POS, PPO, PFFS, SNP, MSA, Cost) - Benefit Explanation";
        public const string ClientTopicID29 = "Medicare Advantage (HMO, POS, PPO, PFFS, SNP, MSA, Cost) - Plans Comparison";
        public const string ClientTopicID30 = "Medicare Advantage (HMO, POS, PPO, PFFS, SNP, MSA, Cost) - Plan Enrollment/Disenroll";
        public const string ClientTopicID31 = "Medicare Advantage (HMO, POS, PPO, PFFS, SNP, MSA, Cost) - Claims/Billing";
        public const string ClientTopicID32 = "Medicare Advantage (HMO, POS, PPO, PFFS, SNP, MSA, Cost) - Appeals/Grievances";
        public const string ClientTopicID33 = "Medicare Advantage (HMO, POS, PPO, PFFS, SNP, MSA, Cost) - Fraud and Abuse";
        public const string ClientTopicID34 = "Medicare Advantage (HMO, POS, PPO, PFFS, SNP, MSA, Cost) - Marketing/Sales Complaints";
        public const string ClientTopicID35 = "Medicare Advantage (HMO, POS, PPO, PFFS, SNP, MSA, Cost) - Quality of Care";
        public const string ClientTopicID36 = "Medicare Advantage (HMO, POS, PPO, PFFS, SNP, MSA, Cost) - Plan Non-Renewal";
        public const string ClientTopicID67 = "Medicare Advantage - Old Enrollment, Disenrollment, Eligibility, Comparisons";
        public const string ClientTopicID68 = "Medicare Advantage - Old Appeals - Quality of Care - Complaints";

        public const string ClientTopicID37 = "Medicare Supplement/SELECT - Eligibility/Screening";
        public const string ClientTopicID38 = "Medicare Supplement/SELECT - Benefit Explanation";
        public const string ClientTopicID39 = "Medicare Supplement/SELECT - Plans Comparison";
        public const string ClientTopicID40 = "Medicare Supplement/SELECT - Claims/Billing";
        public const string ClientTopicID41 = "Medicare Supplement/SELECT - Appeals/Grievances";
        public const string ClientTopicID42 = "Medicare Supplement/SELECT - Fraud and Abuse";
        public const string ClientTopicID43 = "Medicare Supplement/SELECT - Marketing/Sales Complaints or Issues";
        public const string ClientTopicID44 = "Medicare Supplement/SELECT  - Quality of Care";
        public const string ClientTopicID45 = "Medicare Supplement/SELECT - Plan Non-Renewal";
        public const string ClientTopicID70 = "Medicare Supplement/SELECT - Old Enrollment, Eligibility, Comparisons";
        public const string ClientTopicID71 = "Medicare Supplement/SELECT - Old Change Coverage";
        public const string ClientTopicID72 = "Medicare Supplement/SELECT - Old Claims or Appeals";

        public const string ClientTopicID46 = "Medicaid - Medicare Savings Programs (MSP) Screening (QMB, SLMB, QI)";
        public const string ClientTopicID47 = "Medicaid - MSP Application Assistance";
        public const string ClientTopicID48 = "Medicaid - Medicaid (SSI, Nursing Home, MEPD, Elderly Waiver) Screening";
        public const string ClientTopicID49 = "Medicaid - Medicaid Application Assistance";
        public const string ClientTopicID50 = "Medicaid - Medicaid/QMB Claims";
        public const string ClientTopicID51 = "Medicaid - Fraud and Abuse";
        public const string ClientTopicID69 = "Medicaid - Old Other Medicaid";

        public const string ClientTopicID52 = "Other Topics - Long Term Care (LTC) Insurance";
        public const string ClientTopicID53 = "Other Topics - LTC Partnership";
        public const string ClientTopicID54 = "Other Topics - LTC Other";
        public const string ClientTopicID55 = "Other Topics - Military Health Benefits";
        public const string ClientTopicID56 = "Other Topics - Employer/Federal Employee Health Benefits (FEHB)";
        public const string ClientTopicID57 = "Other Topics - COBRA";
        public const string ClientTopicID58 = "Other Topics - Other Health Insurance";
        public const string ClientTopicID59 = "Other Topics - Other";
        public const string ClientTopicID73 = "Other Topics - Old Fraud and Abuse";
        public const string ClientTopicID74 = "Other Topics - Old Customer Service Issues or Complaints";

        public const string ClientContactHoursSpent1 = "Contacts in Which the Total Time Spent With or On Behalf of Client Was 1 - 9 Minutes";
        public const string ClientContactHoursSpent2 = "Contacts in Which the Total Time Spent With or On Behalf of Client Was 10 - 29 Minutes";
        public const string ClientContactHoursSpent3 = "Contacts in Which the Total Time Spent With or On Behalf of Client Was 30 - 59 Minutes";
        public const string ClientContactHoursSpent4 = "Contacts in Which the Total Time Spent With or On Behalf of Client Was 60 or More Minutes";
        public const string ClientContactHoursSpent5 = "Contacts in Which Time Spent Missing, Blank, Null, Not Collected";
        public const string ClientContactHoursSpent6 = "Total Time Spent With or On Behalf of Client Across All Contacts";

        public const string ClientContactCurrentStatus1 = "Contacts Described as General Information and Referral";
        public const string ClientContactCurrentStatus2 = "Contacts Described as Detailed Assistance - In Progress";
        public const string ClientContactCurrentStatus3 = "Contacts Described as Detailed Assistance - Fully Completed";
        public const string ClientContactCurrentStatus4 = "Contacts Described as Problem Solving / Problem Resolution - In Progress";
        public const string ClientContactCurrentStatus5 = "Contacts Described as Problem Solving / Problem Resolution - Fully Completed";
        public const string ClientContactCurrentStatus6 = "Contact Description-Status Missing, Blank, Not Collected, Invalid, Miscoded, Out of Range";
    }
    public static class Pam
    {
        public const string AgencyCode = "AgencyCode";

        public const string PAMID = "PAMID";
        public const string UserID = "UserID";
        public const string SubmitterUserID = "SubmitterUserID";
        public const string SubmitterName = "SubmitterName";

        public const string ReviewerUserID = "ReviewerUserID";
        public const string AgencyID = "AgencyID";
        public const string AgencyName = "AgencyName";
        public const string Version = "Version";
        public const string IsBatchUploadData = "IsBatchUploadData";
        public const string InteractiveEstAttendees = "InteractiveEstAttendees";
        public const string InteractiveEstProvidedEnrollAssistance = "InteractiveEstProvidedEnrollAssistance";
        public const string BoothEstDirectContacts = "BoothEstDirectContacts";
        public const string BoothEstEstProvidedEnrollAssistance = "BoothEstEstProvidedEnrollAssistance";
        public const string RadioEstListenerReach = "RadioEstListenerReach";
        public const string TVEstViewersReach = "TVEstViewersReach";
        public const string DedicatedEstPersonsReached = "DedicatedEstPersonsReached";
        public const string DedicatedEstAnyEnrollmentAssistance = "DedicatedEstAnyEnrollmentAssistance";
        public const string DedicatedEstPartDEnrollmentAssistance = "DedicatedEstPartDEnrollmentAssistance";
        public const string DedicatedEstLISEnrollmentAssistance = "DedicatedEstLISEnrollmentAssistance";
        public const string DedicatedEstMSPEnrollmentAssistance = "DedicatedEstMSPEnrollmentAssistance";
        public const string DedicatedEstOtherEnrollmentAssistance = "DedicatedEstOtherEnrollmentAssistance";
        public const string ElectronicEstPersonsViewingOrListening = "ElectronicEstPersonsViewingOrListening";
        public const string PrintEstPersonsReading = "PrintEstPersonsReading";
        public const string ActivityStartDate = "ActivityStartDate";
        public const string ActivityEndDate = "ActivityEndDate";
        public const string EventName = "EventName";
        public const string ContactFirstName = "ContactFirstName";
        public const string ContactLastName = "ContactLastName";
        public const string ContactPhone = "ContactPhone";
        public const string EventStateCode = "EventStateCode";
        public const string EventCountycode = "EventCountycode";
        public const string EventCountyName = "EventCountyName";
        public const string EventZIPCode = "EventZIPCode";
        public const string EventCity = "EventCity";
        public const string EventStreet = "EventStreet";
        public const string CreatedDate = "CreatedDate";
        public const string LastUpdatedBy = "LastUpdatedBy";
        public const string LastUpdatedDate = "LastUpdatedDate";

        public const string PAMTopicID = "PAMTopicID";
        public const string Description = "Description";
        public const string PAMAudienceID = "PAMAudienceID";
    }

    public static class Zip
    {
        public const string ZipCode = "ZipCode";
        public const string ZipCodeDisplay = "ZipCodeDisplay";
    }
}

public static class PamPresenter
{
    public const string PamUserID = "PamUserID";

    public const string UserName = "UserName";
    public const string Affiliation = "Affiliation";
    public const string HoursSpent = "HoursSpent";
    
}


namespace ShiptalkLogic.Constants.Defaults
{
    public static class DefaultValues
    {
        public static readonly int AgencyIdForNonAgencyUsers;
    }
}

namespace ShiptalkLogic.Constants.StoredProcs
{
    public static class GetCountiesByStateFIPS
    {
        public const string StateFIPS = "@StateFIPS";
        public const string LocationType = "@LocationType";
    }

    
    public static class GetPAMSubmittersForScope
    {
        public const string StateFIPS = "@StateFIPS";
        public const string ScopeId = "@ScopeId";
        public const string userId = "@userId";
        public const string IsActive = "@IsActive";
        
    }
    public static class GetPAMPresentersForScope
    {
        public const string StateFIPS = "@StateFIPS";
        public const string ScopeId = "@ScopeId";
        public const string userId = "@userId";
        public const string IsActive = "@IsActive";

    }


    public static class SearchPAM
    {

        public const string AgencyId = "@AgencyId";

        public const string ActivityStartDateTo = "@ActivityStartDateTo";
        public const string ActivityStartDateFrom = "@ActivityStartDateFrom";

    }

    public static class DeleteAgency
    {
        public const string Id = "@Id";
    }

    public static class DeleteAgencyLocation
    {
        public const string Id = "@Id";
    }   

    public static class CreateAgency
    {
        public const string AgencyCode = "@AgencyCode";
        public const string AgencyComments = "@AgencyComments";
        public const string AgencyCounties = "@AgencyCounties";
        public const string AgencyId = "@AgencyId";
        public const string AgencyLocationComments = "@AgencyLocationComments";
        public const string AgencyLocationId = "@AgencyLocationId";
        public const string AgencyName = "@AgencyName";
        public const string AgencyTypeId = "@AgencyTypeId";
        public const string ContactFirstName = "@ContactFirstName";
        public const string ContactLastName = "@ContactLastName";
        public const string ContactMiddleName = "@ContactMiddleName";
        public const string ContactTitle = "@ContactTitle";
        public const string CreatedBy = "@CreatedBy";
        public const string Fax = "@Fax";
        public const string HoursOfOperation = "@HoursOfOperation";
        public const string LocationName = "@LocationName";
        public const string MailingAddress1 = "@MailingAddress1";
        public const string MailingAddress2 = "@MailingAddress2";
        public const string MailingCity = "@MailingCity";
        public const string MailingStateFIPS = "@MailingStateFIPS";
        public const string MailingZip = "@MailingZip";
        public const string PhysicalAddress1 = "@PhysicalAddress1";
        public const string PhysicalAddress2 = "@PhysicalAddress2";
        public const string PhysicalCity = "@PhysicalCity";
        public const string PhysicalCountyFIPS = "@PhysicalCountyFIPS";
        public const string PhysicalZip = "@PhysicalZip";
        //Added by Lavanya
        public const string Longitude = "Longitude";
        public const string Latitude = "Latitude";       
        public const string AvailableLanguages = "AvailableLanguages";
        public const string HideAgencyFromSearch = "HideAgencyFromSearch";
        //end
        public const string PrimaryEmail = "@PrimaryEmail";
        public const string PrimaryPhone = "@PrimaryPhone";
        public const string SecondaryEmail = "@SecondaryEmail";
        public const string SecondaryPhone = "@SecondaryPhone";
        public const string ServiceAreas = "@ServiceAreas";
        public const string SponsorFirstName = "@SponsorFirstName";
        public const string SponsorLastName = "@SponsorLastName";
        public const string SponsorMiddleName = "@SponsorMiddleName";
        public const string SponsorTitle = "@SponsorTitle";
        public const string StateFIPS = "@StateFIPS";
        public const string TDD = "@TDD";
        public const string TollFreePhone = "@TollFreePhone";
        public const string TollFreeTDD = "@TollFreeTDD";
        public const string Url = "@Url";
        public const string HideAgencyFromPublic = "@HideAgencyFromPublic";
    }

    public static class CreateAgencyLocation
    {
        public const string AgencyId = "@AgencyId";
        public const string Comments = "@Comments";
        public const string ContactFirstName = "@ContactFirstName";
        public const string ContactLastName = "@ContactLastName";
        public const string ContactMiddleName = "@ContactMiddleName";
        public const string ContactTitle = "@ContactTitle";
        public const string CreatedBy = "@CreatedBy";
        public const string Fax = "@Fax";
        public const string HoursOfOperation = "@HoursOfOperation";
        public const string LocationName = "@LocationName";
        public const string MailingAddress1 = "@MailingAddress1";
        public const string MailingAddress2 = "@MailingAddress2";
        public const string MailingCity = "@MailingCity";
        public const string MailingStateFIPS = "@MailingStateFIPS";
        public const string MailingZip = "@MailingZip";
        public const string PhysicalAddress1 = "@PhysicalAddress1";
        public const string PhysicalAddress2 = "@PhysicalAddress2";
        public const string PhysicalCity = "@PhysicalCity";
        public const string PhysicalCountyFIPS = "@PhysicalCountyFIPS";
        public const string PhysicalZip = "@PhysicalZip";
        public const string PhysicalStateFIPS = "@PhysicalStateFIPS";
        public const string PrimaryEmail = "@PrimaryEmail";
        public const string PrimaryPhone = "@PrimaryPhone";
        public const string SecondaryEmail = "@SecondaryEmail";
        public const string SecondaryPhone = "@SecondaryPhone";
        public const string SponsorTitle = "@SponsorTitle";
        public const string StateFIPS = "@StateFIPS";
        public const string TDD = "@TDD";
        public const string TollFreePhone = "@TollFreePhone";
        public const string TollFreeTDD = "@TollFreeTDD";
        public const string IsMainOffice = "@IsMainOffice";
        public const string AgencyLocationId = "@AgencyLocationId";
        //Added by Lavanya
        public const string Longitude = "Longitude";
        public const string Latitude = "Latitude";
        public const string AvailableLanguages = "AvailableLanguages";
        public const string HideAgencyFromSearch = "HideAgencyFromSearch";
        //end
    }

    public static class UpdateAgencyLocation
    {
        public const string Comments = "@Comments";
        public const string ContactFirstName = "@ContactFirstName";
        public const string ContactLastName = "@ContactLastName";
        public const string ContactMiddleName = "@ContactMiddleName";
        public const string ContactTitle = "@ContactTitle";
        public const string LastUpdatedBy = "@LastUpdatedBy";
        public const string Fax = "@Fax";
        public const string HoursOfOperation = "@HoursOfOperation";
        public const string LocationName = "@LocationName";
        public const string MailingAddress1 = "@MailingAddress1";
        public const string MailingAddress2 = "@MailingAddress2";
        public const string MailingCity = "@MailingCity";
        public const string MailingStateFIPS = "@MailingStateFIPS";
        public const string MailingZip = "@MailingZip";
        public const string PhysicalAddress1 = "@PhysicalAddress1";
        public const string PhysicalAddress2 = "@PhysicalAddress2";
        public const string PhysicalCity = "@PhysicalCity";
        public const string PhysicalCountyFIPS = "@PhysicalCountyFIPS";
        public const string PhysicalZip = "@PhysicalZip";
        public const string PhysicalStateFIPS = "@PhysicalStateFIPS";
        public const string PrimaryEmail = "@PrimaryEmail";
        public const string PrimaryPhone = "@PrimaryPhone";
        public const string SecondaryEmail = "@SecondaryEmail";
        public const string SecondaryPhone = "@SecondaryPhone";
        public const string SponsorTitle = "@SponsorTitle";
        public const string StateFIPS = "@StateFIPS";
        public const string TDD = "@TDD";
        public const string TollFreePhone = "@TollFreePhone";
        public const string TollFreeTDD = "@TollFreeTDD";
        public const string IsMainOffice = "@IsMainOffice";
        public const string AgencyLocationId = "@AgencyLocationId"; 
    }

    public static class UpdateAgency
    {
        public const string AgencyComments = "@AgencyComments";
        public const string AgencyId = "@AgencyId";
        public const string AgencyLocationComments = "@AgencyLocationComments";
        public const string AgencyName = "@AgencyName";
        public const string AgencyTypeId = "@AgencyTypeId";
        public const string ContactFirstName = "@ContactFirstName";
        public const string ContactLastName = "@ContactLastName";
        public const string ContactMiddleName = "@ContactMiddleName";
        public const string ContactTitle = "@ContactTitle";
        public const string Fax = "@Fax";
        public const string HoursOfOperation = "@HoursOfOperation";
        public const string LastUpdatedBy = "@LastUpdatedBy";
        public const string LocationName = "@LocationName";
        public const string MailingAddress1 = "@MailingAddress1";
        public const string MailingAddress2 = "@MailingAddress2";
        public const string MailingCity = "@MailingCity";
        public const string MailingStateFIPS = "@MailingStateFIPS";
        public const string MailingZip = "@MailingZip";
        public const string PhysicalAddress1 = "@PhysicalAddress1";
        public const string PhysicalAddress2 = "@PhysicalAddress2";
        public const string PhysicalCity = "@PhysicalCity";
        public const string PhysicalCountyFIPS = "@PhysicalCountyFIPS";
        public const string PhysicalZip = "@PhysicalZip";
        //Added by Lavanya
        public const string Longitude = "Longitude";
        public const string Latitude = "Latitude";        
        public const string AvailableLanguages = "AvailableLanguages";
        public const string HideAgencyFromSearch = "HideAgencyFromSearch";
        //end
        public const string PrimaryEmail = "@PrimaryEmail";
        public const string PrimaryPhone = "@PrimaryPhone";
        public const string SecondaryEmail = "@SecondaryEmail";
        public const string SecondaryPhone = "@SecondaryPhone";
        public const string ServiceAreas = "@ServiceAreas";
        public const string SponsorFirstName = "@SponsorFirstName";
        public const string SponsorLastName = "@SponsorLastName";
        public const string SponsorMiddleName = "@SponsorMiddleName";
        public const string SponsorTitle = "@SponsorTitle";
        public const string StateFIPS = "@StateFIPS";
        public const string TDD = "@TDD";
        public const string TollFreePhone = "@TollFreePhone";
        public const string TollFreeTDD = "@TollFreeTDD";
        public const string Url = "@Url";
        public const string IsActive = "@IsActive";
        public const string HideAgencyFromPublic = "@HideAgencyFromPublic";
    }

    public static class SearchAgencies
    {
        public const string Keywords = "@Keywords";
        public const string StateFIPS = "@StateFIPS";
        public const string ScopeId = "@ScopeId";
        public const string UserId = "@UserId";

    }

    public static class ListAllAgencies
    {
        public const string StateFIPS = "@StateFIPS";
        public const string ScopeId = "@ScopeId";
        public const string UserId = "@UserId";

    }

    public static class GetAgency
    {
        public const string AgencyId = "@AgencyId";
        public const string IncludeAllLocations = "@IncludeAllLocations";
    }

    public static class GetAgencyLocation
    {
        public const string AgencyLocationId = "@AgencyLocationId";
    }

    public static class GetAgenciesForStateLookup
    {
        public const string StateFIPS = "@StateFIPS";
        public const string IncludeInactive = "@IncludeInactive";
    }

    public static class GetAgenciesForUser
    {
        public const string UserId = "@UserId";
        public const string ShowAllAgencyUser = "@ShowAllAgencyUser";

    }

    public static class CreateSubStateRegion
    {
        public const string Name = "@Name";
        public const string StateFIPS = "@StateFIPS";
        public const string Agencies = "@Agencies";
        public const string CreatedBy = "@CreatedBy";
        public const string SubStateRegionId = "@SubStateRegionId";
    }

    public static class SearchSubStateRegions
    {
        public const string StateFIPS = "@StateFIPS";
        public const string SubStateRegionID = "@SubStateRegionID";
        public const string SubStateRegionType = "@SubStateRegionType";
    }

    public static class GetSubStateRegion
    {
        public const string Id = "@Id";
    }

    public static class UpdateSubStateRegion
    {
        public const string Id = "@Id";
        public const string Name = "@Name";
        public const string StateFIPS = "@StateFIPS";
        public const string LastUpdatedBy = "@LastUpdatedBy";
        public const string IsActive = "@IsActive";
        public const string ActiveInactiveDate = "@ActiveInactiveDate";
        public const string Agencies = "@Agencies";
    }

    public static class DeleteSubStateRegion
    {
        public const string Id = "@Id";       
    }

    public static class GetAgenciesForStateCountyZipLookup
    {
        public const string StateFIPS = "@StateFIPS";
        public const string CountyFIPS = "@CountyFIPS";
        public const string Zip = "@Zip";
    }

    public static class UpdateSHIPProfile
    {
        public const string  ID = "@ID" ;
        public const string ProgramName = "@ProgramName";
        public const string ProgramWebsite = "@ProgramWebsite";
        public const string ProgramSummary = "@ProgramSummary";

        public const string BeneficiaryContactPhoneTollFree = "@BeneficiaryContactPhoneTollFree";
        public const string BeneficiaryContactPhoneTollFreeInStateOnly = "@BeneficiaryContactPhoneTollFreeInStateOnly";
        public const string BeneficiaryContactPhoneTollLine = "@BeneficiaryContactPhoneTollLine";
        public const string BeneficiaryContactWebsite = "@BeneficiaryContactWebsite";
        public const string BeneficiaryContactTDDLine = "@BeneficiaryContactTDDLine";
        public const string BeneficiaryContactEmail = "@BeneficiaryContactEmail";
        public const string BeneficiaryContactHours = "@BeneficiaryContactHours";

        public const string AdminAgencyContactName = "@AdminAgencyContactName";
        public const string AdminAgencyName = "@AdminAgencyName";
        public const string AdminAgencyContactTitle = "@AdminAgencyContactTitle";
        public const string AdminAgencyAddress = "@AdminAgencyAddress";
        public const string AdminAgencyCity = "@AdminAgencyCity";
        public const string AdminAgencyZipcode = "@AdminAgencyZipcode";
        public const string AdminAgencyPhone = "@AdminAgencyPhone";
        public const string AdminAgencyFax = "@AdminAgencyFax";
        public const string AdminAgencyEmail = "@AdminAgencyEmail";

        public const string ProgramCoordinatorName = "@ProgramCoordinatorName";
        public const string ProgramCoordinatorAddress = "@ProgramCoordinatorAddress";
        public const string ProgramCoordinatorCity = "@ProgramCoordinatorCity";
        public const string ProgramCoordinatorZipcode = "@ProgramCoordinatorZipcode";
        public const string ProgramCoordinatorPhone = "@ProgramCoordinatorPhone";
        public const string ProgramCoordinatorFax = "@ProgramCoordinatorFax";
        public const string ProgramCoordinatorEmail = "@ProgramCoordinatorEmail";

        public const string NumberOfVolunteerCounselors = "@NumberOfVolunteerCounselors";
        public const string NumberOfStateStaff = "@NumberOfStateStaff";
        public const string TotalCounties = "@TotalCounties";
        public const string NumberOfCountiesServed = "@NumberOfCountiesServed";
        public const string NumberOfSponsors = "@NumberOfSponsors";
        public const string AvailableLanguages = "@AvailableLanguages";
        public const string LastUpdatedBy = "@LastUpdatedBy";

        //New fields: added by Lavanya Maram - 04/23/2013

        public const string StateOversightAgency = "@StateOversightAgency";
        public const string NumberOfPaidStaff = "@NumberOfPaidStaff";
        public const string NumberOfCoordinators = "@NumberOfCoordinators";
        public const string NumberOfCertifiedCounselors = "@NumberOfCertifiedCounselors";
        public const string NumberOfEligibleBeneficiaries = "@NumberOfEligibleBeneficiaries";
        public const string NumberOfBeneficiaryContacts = "@NumberOfBeneficiaryContacts";
        public const string LocalAgencies = "@LocalAgencies";
        public const string Longitude = "@Longitude";
        public const string Latitude = "@Latitude";       

    }

    public static class GetSHIPProfile
    {
        public const string Id = "@Id";
    }
    //Added by Lavanya Maram
    public static class GetSHIPProfileAgencyDetails
    {
        public const string StateFIPS = "@StateFIPS";
        public const string IncludeInactive = "@IncludeInactive";
        public const string IncludeAgencyLocations = "@IncludeAgencyLocations";
    }
    //end

    public static class GetNextAutoAssignedClientID
    {
        public const string AgencyCode = "@AgencyCode";
    }

    public static class GetAgencyUsersByDescriptor
    {
        public const string AgencyId = "@AgencyId";
        public const string DescriptorId = "@DescriptorId";
    }

    public static class GetSpecialFielsForForm
    {
        public const string FormType = "@FormType";
        public const string StateFIPS = "@StateFIPS";
        public const string RestrictDate = "@RestrictDate";
    }

    public static class DoesAgencyNameExist
    {
        public const string AgencyName = "@AgencyName";
    }

    public static class CreateClientContact
    {
        public const string ClientContactID = "@ClientContactID";
        public const string CounselorUserID = "@CounselorUserID";
        public const string SubmitterUserID = "@SubmitterUserID";
        public const string IsBatchUploadData = "@IsBatchUploadData";
        public const string AgencyID = "@AgencyID";
        public const string CountyOfCounselorLocation = "@CountyOfCounselorLocation";
        public const string ZIPCodeOfCounselorLocation = "@ZIPCodeOfCounselorLocation";
        public const string ClientID = "@ClientID";
        public const string DateOfContact = "@DateOfContact";
        public const string StateSpecificClientID = "@StateSpecificClientID";
        public const string BatchStateUniqueID = "@BatchStateUniqueID";
        public const string HowLearnedSHIP = "@HowLearnedSHIP";
        public const string FirstVSContinuingService = "@FirstVSContinuingService";
        public const string MethodOfContact = "@MethodOfContact";
        public const string ClientFirstName = "@ClientFirstName";
        public const string ClientLastName = "@ClientLastName";
        public const string ClientPhone = "@ClientPhone";
        public const string RepresentativeFirstName = "@RepresentativeFirstName";
        public const string RepresentativeLastName = "@RepresentativeLastName";
        public const string ZIPCodeOfClientRes = "@ZIPCodeOfClientRes";
        public const string CountyCodeOfClientRes = "@CountyCodeOfClientRes";
        public const string ClientAgeGroup = "@ClientAgeGroup";
        public const string ClientGender = "@ClientGender";
        public const string PrimaryLanguageOtherThanEnglish = "@PrimaryLanguageOtherThanEnglish";
        public const string MonthlyIncome = "@MonthlyIncome";
        public const string ClientAsset = "@ClientAsset";
        public const string Disability = "@Disability";
        public const string DualMental = "@DualMental";
        public const string HoursSpent = "@HoursSpent";
        public const string MinutesSpent = "@MinutesSpent";
        public const string CurrentStatus = "@CurrentStatus";
        public const string Comments = "@Comments";
        public const string ClientRaceDescriptions = "@ClientRaceDescriptions";
        public const string MedicarePrescriptionDrugCoverageTopics = "@MedicarePrescriptionDrugCoverageTopics";
        public const string MedicareAdvantageTopics = "@MedicareAdvantageTopics";
        public const string PartDLowIncomeSubsidyTopics = "@PartDLowIncomeSubsidyTopics";
        public const string MedicareSupplementTopics = "@MedicareSupplementTopics";
        public const string OtherPrescriptionAssistanceTopics = "@OtherPrescriptionAssistanceTopics";
        public const string OtherPreseriptionAssitanceSpecified = "@OtherPreseriptionAssitanceSpecified";
        public const string MedicaidTopics = "@MedicaidTopics";
        public const string MedicarePartsAandBTopics = "@MedicarePartsAandBTopics";
        public const string OtherDrugTopics = "@OtherDrugTopics";
        public const string OtherDrugTopicsSpecified = "@OtherDrugTopicsSpecified";
        public const string CMSSpecialUseFields = "@CMSSpecialUseFields";
        public const string StateSpecialUseFields = "@StateSpecialUseFields";
    }

    public static class UpdateClientContact
    {
        public const string UserID = "@UserID";
        public const string SubmitterUserID = "@SubmitterUserID";
        public const string ClientContactID = "@ClientContactID";
        public const string CounselorUserID = "@CounselorUserID";
        public const string ReviewerUserID = "@ReviewerUserID";
        public const string AgencyID = "@AgencyID";
        public const string CountyOfCounselorLocation = "@CountyOfCounselorLocation";
        public const string ZIPCodeOfCounselorLocation = "@ZIPCodeOfCounselorLocation";
        public const string ClientID = "@ClientID";
        public const string DateOfContact = "@DateOfContact";
        public const string StateSpecificClientID = "@StateSpecificClientID";
        public const string BatchStateUniqueID = "@BatchStateUniqueID";
        public const string HowLearnedSHIP = "@HowLearnedSHIP";
        public const string FirstVSContinuingService = "@FirstVSContinuingService";
        public const string MethodOfContact = "@MethodOfContact";
        public const string ClientFirstName = "@ClientFirstName";
        public const string ClientLastName = "@ClientLastName";
        public const string ClientPhone = "@ClientPhone";
        public const string RepresentativeFirstName = "@RepresentativeFirstName";
        public const string RepresentativeLastName = "@RepresentativeLastName";
        public const string ZIPCodeOfClientRes = "@ZIPCodeOfClientRes";
        public const string CountyCodeOfClientRes = "@CountyCodeOfClientRes";
        public const string ClientAgeGroup = "@ClientAgeGroup";
        public const string ClientGender = "@ClientGender";
        public const string PrimaryLanguageOtherThanEnglish = "@PrimaryLanguageOtherThanEnglish";
        public const string MonthlyIncome = "@MonthlyIncome";
        public const string ClientAsset = "@ClientAsset";
        public const string Disability = "@Disability";
        public const string DualMental = "@DualMental";
        public const string HoursSpent = "@HoursSpent";
        public const string MinutesSpent = "@MinutesSpent";
        public const string CurrentStatus = "@CurrentStatus";
        public const string Comments = "@Comments";
        public const string ClientRaceDescriptions = "@ClientRaceDescriptions";
        public const string MedicarePrescriptionDrugCoverageTopics = "@MedicarePrescriptionDrugCoverageTopics";
        public const string MedicareAdvantageTopics = "@MedicareAdvantageTopics";
        public const string PartDLowIncomeSubsidyTopics = "@PartDLowIncomeSubsidyTopics";
        public const string MedicareSupplementTopics = "@MedicareSupplementTopics";
        public const string OtherPrescriptionAssistanceTopics = "@OtherPrescriptionAssistanceTopics";
        public const string OtherPreseriptionAssitanceSpecified = "@OtherPreseriptionAssitanceSpecified";
        public const string MedicaidTopics = "@MedicaidTopics";
        public const string MedicarePartsAandBTopics = "@MedicarePartsAandBTopics";
        public const string OtherDrugTopics = "@OtherDrugTopics";
        public const string OtherDrugTopicsSpecified = "@OtherDrugTopicsSpecified";
        public const string CMSSpecialUseFields = "@CMSSpecialUseFields";
        public const string StateSpecialUseFields = "@StateSpecialUseFields";
    }

    public static class GetClientContact
    {
        public const string ClientContactID = "@ClientContactID";

    }

    public static class CreatePAM
    {
        public const string UserID = "@UserID";
        public const string SubmitterUserID = "@SubmitterUserID";
        public const string ReviewerUserID = "@ReviewerUserID";
        public const string AgencyID = "@AgencyID";
        public const string InteractiveEstAttendees = "@InteractiveEstAttendees";
        public const string InteractiveEstProvidedEnrollAssistance = "@InteractiveEstProvidedEnrollAssistance";
        public const string BoothEstDirectContacts = "@BoothEstDirectContacts";
        public const string BoothEstEstProvidedEnrollAssistance = "@BoothEstEstProvidedEnrollAssistance";
        public const string RadioEstListenerReach = "@RadioEstListenerReach";
        public const string TVEstViewersReach = "@TVEstViewersReach";
        public const string DedicatedEstPersonsReached = "@DedicatedEstPersonsReached";
        public const string DedicatedEstAnyEnrollmentAssistance = "@DedicatedEstAnyEnrollmentAssistance";
        public const string DedicatedEstPartDEnrollmentAssistance = "@DedicatedEstPartDEnrollmentAssistance";
        public const string DedicatedEstLISEnrollmentAssistance = "@DedicatedEstLISEnrollmentAssistance";
        public const string DedicatedEstMSPEnrollmentAssistance = "@DedicatedEstMSPEnrollmentAssistance";
        public const string DedicatedEstOtherEnrollmentAssistance = "@DedicatedEstOtherEnrollmentAssistance";
        public const string ElectronicEstPersonsViewingOrListening = "@ElectronicEstPersonsViewingOrListening";
        public const string PrintEstPersonsReading = "@PrintEstPersonsReading";
        public const string ActivityStartDate = "@ActivityStartDate";
        public const string ActivityEndDate = "@ActivityEndDate";
        public const string EventName = "@EventName";
        public const string ContactFirstName = "@ContactFirstName";
        public const string ContactLastName = "@ContactLastName";
        public const string ContactPhone = "@ContactPhone";
        
        public const string EventStateCode = "@EventStateCode";
        public const string EventCountycode = "@EventCountycode";
        public const string EventZIPCode = "@EventZIPCode";
        public const string EventCity = "@EventCity";
        public const string EventStreet = "@EventStreet";
        public const string LastUpdatedBy = "@LastUpdatedBy";
        public const string IsBatchUploadData = "@IsBatchUploadData";
        public const string BatchStateUniqueID = "@BatchStateUniqueID";
        
        public const string CMSSpecialUseFields = "@CMSSpecialUseFields";
        public const string StateSpecialUseFields = "@StateSpecialUseFields";

        public const string PamTopics = "@PamTopics";
        public const string OtherPamTopicSpecified = "@OtherPamTopicSpecified";
        
        public const string PAMAudiences = "@PAMAudiences";
        public const string OtherPamAudienceSpecified = "@OtherPamAudienceSpecified";
        
        public const string PamPresenters = "@PamPresenters";

        public const string PAMID = "@PAMID";
    }


    public static class GetPam
    {
        public const string PamId = "@PAMID";
    }

    public static class UpdatePam
    {
        public const string PAMID = "@PAMID";

        public const string UserID = "@UserID";
        public const string SubmitterUserID = "@SubmitterUserID";
        public const string ReviewerUserID = "@ReviewerUserID";
        public const string AgencyID = "@AgencyID";
        public const string InteractiveEstAttendees = "@InteractiveEstAttendees";
        public const string InteractiveEstProvidedEnrollAssistance = "@InteractiveEstProvidedEnrollAssistance";
        public const string BoothEstDirectContacts = "@BoothEstDirectContacts";
        public const string BoothEstEstProvidedEnrollAssistance = "@BoothEstEstProvidedEnrollAssistance";
        public const string RadioEstListenerReach = "@RadioEstListenerReach";
        public const string TVEstViewersReach = "@TVEstViewersReach";
        public const string DedicatedEstPersonsReached = "@DedicatedEstPersonsReached";
        public const string DedicatedEstAnyEnrollmentAssistance = "@DedicatedEstAnyEnrollmentAssistance";
        public const string DedicatedEstPartDEnrollmentAssistance = "@DedicatedEstPartDEnrollmentAssistance";
        public const string DedicatedEstLISEnrollmentAssistance = "@DedicatedEstLISEnrollmentAssistance";
        public const string DedicatedEstMSPEnrollmentAssistance = "@DedicatedEstMSPEnrollmentAssistance";
        public const string DedicatedEstOtherEnrollmentAssistance = "@DedicatedEstOtherEnrollmentAssistance";
        public const string ElectronicEstPersonsViewingOrListening = "@ElectronicEstPersonsViewingOrListening";
        public const string PrintEstPersonsReading = "@PrintEstPersonsReading";
        public const string ActivityStartDate = "@ActivityStartDate";
        public const string ActivityEndDate = "@ActivityEndDate";
        public const string EventName = "@EventName";
        public const string ContactFirstName = "@ContactFirstName";
        public const string ContactLastName = "@ContactLastName";
        public const string ContactPhone = "@ContactPhone";

        public const string EventStateCode = "@EventStateCode";
        public const string EventCountycode = "@EventCountycode";
        public const string EventZIPCode = "@EventZIPCode";
        public const string EventCity = "@EventCity";
        public const string EventStreet = "@EventStreet";
        public const string LastUpdatedBy = "@LastUpdatedBy";
        public const string IsBatchUploadData = "@IsBatchUploadData";
        public const string BatchStateUniqueID = "@BatchStateUniqueID";

        public const string CMSSpecialUseFields = "@CMSSpecialUseFields";
        public const string StateSpecialUseFields = "@StateSpecialUseFields";

        public const string PamTopics = "@PamTopics";
        public const string OtherPamTopicSpecified = "@OtherPamTopicSpecified";

        public const string PAMAudiences = "@PAMAudiences";
        public const string OtherPamAudienceSpecified = "@OtherPamAudienceSpecified";

        public const string PamPresenters = "@PamPresenters";

    }
    public static class GetCCSummaryReport
    {
        public const string StartDate = "@StartDate";
        public const string EndDate = "@EndDate";
        public const string StateFIPS = "@StateFIPS";
        public const string AgencyId = "@AgencyId";
        public const string CountyCounselorLocation = "@CountyCounselorLocation";
        public const string ZipCodeCounselorLocation = "@ZipCodeCounselorLocation";
        public const string ZipCodeClientResidence = "@ZipCodeClientResidence";
        public const string CountyCodeClientResidence = "@CountyCodeClientResidence";
        public const string SubStateRegionID = "@SubStateRegionID";
        public const string CounselorId = "@CounselorID";
        public const string SubmitterId = "@SubmitterID";

        public const string ScopeId = "@ScopeId";
        public const string UserId = "@UserId";

        public const string FormType = "@FormType";

        
            
    }
    public static class GetPAMSummaryReport
    {
        public const string StartDate = "@StartDate";
        public const string EndDate = "@EndDate";
        public const string StateFIPS = "@StateFIPS";
        public const string AgencyId = "@AgencyId";
        public const string CountyOfActivityEvent = "@CountyCode";
        public const string ZipCodeOfActivityEvent = "@ZIPCode";
        public const string SubStateRegionID = "@SubStateRegionID";
        public const string PresenterContributorId = "@PresenterID";
        public const string SubmitterId = "@SubmitterUserID";

        public const string ScopeId = "@ScopeId";
        public const string UserId = "@UserId";
        public const string StateFIPSCode = "@StateFIPSCode";


    }

    public static class IsZIPCodeValid
    {
        public const string ZIPCode = "@ZIPCode";
    }

    public static class IsCounselingCountyCodeValid
    {
        public const string UserId = "@UserId";
        public const string CountyCode = "@CountyCode";
    }

    public static class IsCounselingZIPCodeValid
    {
        public const string UserId = "@UserId";
        public const string ZIPCode = "@ZIPCode";
    }
    
    public static class IsUserClientContactReviewer
    {
        public const string ClientContactId = "@ClientContactId";
        public const string UserId = "@UserId";
    }


    public static class IsUserPamReviewer
    {
        public const string PamID = "@PamId";
        public const string UserId = "@UserId";
    }

    public static class IsUserIdInPresenters
    {
        public const string PamID = "@PamId";
        public const string UserId = "@UserId";
    }



    public static class IsDuplicateClientContact
    {
        public const string CheckType = "@CheckType";
        public const string AgencyID = "@AgencyID";
        public const string StateSpecifiedClientID = "@StateSpecifiedClientID";
        public const string ClientID = "@ClientID";

        public const string ClientFirstName = "@ClientFirstName";
        public const string ClientLastName = "@ClientLastName";
        public const string DateOfContact = "@DateOfContact";
        public const string CounselorID = "@CounselorID";
    }


    public static class GetRecentClientContacts
    {
        public const string UserId = "@UserId";
        public const string DescriptorId = "@DescriptorId";
    }

    public static class SearchClientContacts
    {
        public const string Keywords = "@Keywords";
        public const string FromContactDate = "@FromContactDate";
        public const string ToContactDate = "@ToContactDate";
        public const string StateFIPS = "@StateFIPS";
        public const string CounselorId = "@CounselorId";
        public const string SubmitterId = "@SubmitterId";
        public const string ScopeId = "@ScopeId";
        public const string UserId = "@UserId";
        public const string AgencyId = "@AgencyId";

    }

    //public static class GetClientContactCounselorsByState
    //{
    //    public const string StateFIPS = "@StateFIPS";
    //}

    //public static class GetClientContactSubmittersByState
    //{
    //    public const string StateFIPS = "@StateFIPS";
    //}

    public static class GetClientContactCounselors
    {
        public const string UserId = "@UserId";
        public const string ScopeId = "@UserScopeId";
        public const string StateFIPS = "@StateFIPS";
        public const string AgencyId = "@AgencyId";
    }

    public static class GetClientContactCounselorsByAgencyActiveInactive
    {
        public const string UserId = "@UserId";
        public const string ScopeId = "@UserScopeId";
        public const string StateFIPS = "@StateFIPS";
        public const string AgencyId = "@AgencyId";
        public const string IsActive = "@IsActive";

    }

    public static class GetClientContactSubmitters
    {
        public const string UserId = "@UserId";
        public const string ScopeId = "@UserScopeId";
        public const string StateFIPS = "@StateFIPS";
        public const string AgencyId = "@AgencyId";
    }

    public static class IsAgencyUserActive
    {
        public const string AgencyId = "@AgencyId";
        public const string UserId = "@UserId";
    }

    //public static class GetClientContactCounselorsByUserId
    //{
    //    public const string UserId = "@UserId";
    //}

    //public static class GetClientContactSubmittersByUserId
    //{
    //    public const string UserId = "@UserId";
    //}

    public static class GetActiveAgencyDescriptors
    {
        public const string UserId = "@UserId";
    }

    public static class GetCounselorsForAgencies
    {
        public const string ScopeID = "@ScopeID";
        public const string Agencies = "@Agencies";
    }

    public static class GetSubmittersForAgencies
    {
        public const string ScopeID = "@ScopeID";
        public const string Agencies = "@Agencies";
    }

    public static class GetReviewerSubmittersForAgencies
    {
        public const string ScopeID = "@ScopeID";
        public const string Agencies = "@Agencies";
        public const string UserId = "@UserId";
    }

    public static class IsSuperDataEditorForActiveAgencies
    {
        public const string UserId = "@UserId";
    }

    public static class GetCounselorsForSuperDataEditor
    {
        public const string UserId = "@UserId";    
    }

    public static class GetSubmittersForSuperDataEditor
    {
        public const string UserId = "@UserId";
    }

    public static class GetActiveAgencies
    {
        public const string UserId = "@UserId";
    }

    public static class GetStateAgenciesList
    {
        public const string StateFIPS = "@StateFIPS";
        public const string Scope = "@Scope";
        public const string UserId = "@UserId";
    }
}

