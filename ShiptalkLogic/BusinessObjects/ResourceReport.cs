using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShiptalkLogic.BusinessObjects
{
    /// <summary>
    /// Resoource Report Data object
    /// </summary>
    public class ResourceReport
    {
        #region Constructor
        public ResourceReport()
        {
            SubmitterID = -1;
            StateFIPSCode = string.Empty;


         SubmitterID= -1;
	     ReviewerID = -1;
	     NoOfStateVolunteerCounselors= 0;
	     NoOfOtherVolunteerCounselors= 0;
	     NoOfStateShipPaidCounselors= 0;
	     NoOfOtherShipPaidCounselors= 0;
	     NoOfStateInKindPaidCounselors= 0;
	     NoOfOtherInKindPaidCounselors= 0;
	     NoOfStateVolunteerCounselorsHrs= 0;
	     NoOfOtherVolunteerCounselorsHrs= 0;
	     NoOfStateShipPaidCounselorsHrs= 0;
	     NoOfOtherShipPaidCounselorsHrs= 0;
	     NoOfStateInKindPaidCounselorsHrs= 0;
	     NoOfOtherInKindPaidCounselorsHrs= 0;
	     NoOfUnpaidVolunteerCoordinators= 0;
	     NoOfSHIPPaidCoordinators= 0;
	     NoOfInKindPaidCoordinators= 0;
	     NoOfUnpaidVolunteerCoordinatorsHrs= 0;
	     NoOfSHIPPaidCoordinatorsHrs= 0;
	     NoOfInKindPaidCoordinatorsHrs= 0;
	     NoOfStateOtherStaff= 0;
	     NoOfOtherOtherStaff= 0;
	     NoOfStateShipPaidOtherStaff= 0;
	     NoOfOtherShipPaidOtherStaff= 0;
	     NoOfStateInKindPaidOtherStaff= 0;
	     NoOfOtherInKindPaidOtherStaff= 0;
	     NoOfStateVolunteerOtherStaffHrs= 0;
	     NoOfOtherVolunteerOtherStaffHrs= 0;
	     NoOfStateShipPaidOtherStaffHrs= 0;
	     NoOfOtherShipPaidOtherStaffHrs= 0;
	     NoOfStateInKindPaidOtherStaffHrs= 0;
	     NoOfOtherInKindPaidOtherStaffHrs= 0;
	     NoOfInitialTrainings= 0;
	     NoOfInitialTrainingsAttend= 0;
	     NoOfInitTrainingsTotalHrs= 0;
	     NoOfUpdateTrainings= 0;
	     NoOfUpdateTrainingsAttend= 0;
	     NoOfUpdateTrainingsTotalHrs= 0;
	     NoOfYrsServiceLessThan1= 0;
	     NoOfYrsService1To3= 0;
	     NoOfYrsService3To5= 0;
	     NoOfYrsServiceOver5= 0;
	     NoOfYrsServiceNotCol= 0;
	     NoOfDisabled= 0;
	     NoOfNotDisabled= 0;
	     NoOfEthnicityHispanic= 0;
	     NoOfEthnicityWhite= 0;
	     NoOfEthnicityAfricanAmerican= 0;
	     NoOfEthnicityAmericanIndian= 0;
	     NoOfEthnicityAsianIndian= 0;
	     NoOfEthnicityChinese= 0;
	     NoOfEthnicityFilipino= 0;
	     NoOfEthnicityJapanese= 0;
	     NoOfEthnicityKorean= 0;
	     NoOfEthnicityVietnamese= 0;
	     NoOfEthnicityNativeHawaiian= 0;
	     NoOfEthnicityGuamanian= 0;
	     NoOfEthnicitySamoan= 0;
	     NoOfEthnicityOtherAsian= 0;
	     NoOfEthnicityOthherPacificIslander= 0;
	     NoOfEthnicitySomeOtherRace= 0;
	     NoOfEthnicityMoreThanOneRaceEthnicity= 0;
	     NoOfEthnicityNotCollected= 0;
	     NoOfAgeLessThan65= 0;
	     NoOfAgeOver65= 0;
	     NoOfAgeNotCollected= 0;
	     NoOfGenderFemale= 0;
	     NoOfGenderMale= 0;
	     NoOfGenderNotCollected= 0;
	     NoOfSpeaksAnotherLanguageOtherThanEnglish= 0;
	     NoOfEnglishSpeakerOnly= 0;
	     NoOfSpeaksAnotherLanguageNotCollected= 0;
	     LastUpdatedBy= -1;

        }
        #endregion
        #region Properties
        public int SubmitterID{get;set;}
	    public int? ReviewerID{get;set;}
	    public DateTime RepYrFrom {get;set;}
	    public DateTime RepYrTo {get;set;}
	    public string StateFIPSCode {get;set;}
	    public string StateGranteeName {get;set;}
	    public string PersonCompletingReportName {get;set;}
	    public string PersonCompletingReportTitle {get;set;}
	    public string PersonCompletingReportTel {get;set;}
	    public int? NoOfStateVolunteerCounselors{get;set;}
	    public int? NoOfOtherVolunteerCounselors{get;set;}
	    public int? NoOfStateShipPaidCounselors{get;set;}
	    public int? NoOfOtherShipPaidCounselors{get;set;}
	    public int? NoOfStateInKindPaidCounselors{get;set;}
	    public int? NoOfOtherInKindPaidCounselors{get;set;}
	    public int? NoOfStateVolunteerCounselorsHrs{get;set;}
	    public int? NoOfOtherVolunteerCounselorsHrs{get;set;}
	    public int? NoOfStateShipPaidCounselorsHrs{get;set;}
	    public int? NoOfOtherShipPaidCounselorsHrs{get;set;}
	    public int? NoOfStateInKindPaidCounselorsHrs{get;set;}
	    public int? NoOfOtherInKindPaidCounselorsHrs{get;set;}
	    public int? NoOfUnpaidVolunteerCoordinators{get;set;}
	    public int? NoOfSHIPPaidCoordinators{get;set;}
	    public int? NoOfInKindPaidCoordinators{get;set;}
	    public int? NoOfUnpaidVolunteerCoordinatorsHrs{get;set;}
	    public int? NoOfSHIPPaidCoordinatorsHrs{get;set;}
	    public int? NoOfInKindPaidCoordinatorsHrs{get;set;}
	    public int? NoOfStateOtherStaff{get;set;}
	    public int? NoOfOtherOtherStaff{get;set;}
	    public int? NoOfStateShipPaidOtherStaff{get;set;}
	    public int? NoOfOtherShipPaidOtherStaff{get;set;}
	    public int? NoOfStateInKindPaidOtherStaff{get;set;}
	    public int? NoOfOtherInKindPaidOtherStaff{get;set;}
	    public int? NoOfStateVolunteerOtherStaffHrs{get;set;}
	    public int? NoOfOtherVolunteerOtherStaffHrs{get;set;}
	    public int? NoOfStateShipPaidOtherStaffHrs{get;set;}
	    public int? NoOfOtherShipPaidOtherStaffHrs{get;set;}
	    public int? NoOfStateInKindPaidOtherStaffHrs{get;set;}
	    public int? NoOfOtherInKindPaidOtherStaffHrs{get;set;}
	    public int? NoOfInitialTrainings{get;set;}
	    public int? NoOfInitialTrainingsAttend{get;set;}
	    public int? NoOfInitTrainingsTotalHrs{get;set;}
	    public int? NoOfUpdateTrainings{get;set;}
	    public int? NoOfUpdateTrainingsAttend{get;set;}
	    public int? NoOfUpdateTrainingsTotalHrs{get;set;}
	    public int? NoOfYrsServiceLessThan1{get;set;}
	    public int? NoOfYrsService1To3{get;set;}
	    public int? NoOfYrsService3To5{get;set;}
	    public int? NoOfYrsServiceOver5{get;set;}
	    public int? NoOfYrsServiceNotCol{get;set;}
	    public int? NoOfDisabled{get;set;}
	    public int? NoOfNotDisabled{get;set;}
	    public int? NoOfEthnicityHispanic{get;set;}
	    public int? NoOfEthnicityWhite{get;set;}
	    public int? NoOfEthnicityAfricanAmerican{get;set;}
	    public int? NoOfEthnicityAmericanIndian{get;set;}
	    public int? NoOfEthnicityAsianIndian{get;set;}
	    public int? NoOfEthnicityChinese{get;set;}
	    public int? NoOfEthnicityFilipino{get;set;}
	    public int? NoOfEthnicityJapanese{get;set;}
	    public int? NoOfEthnicityKorean{get;set;}
	    public int? NoOfEthnicityVietnamese{get;set;}
	    public int? NoOfEthnicityNativeHawaiian{get;set;}
	    public int? NoOfEthnicityGuamanian{get;set;}
	    public int? NoOfEthnicitySamoan{get;set;}
	    public int? NoOfEthnicityOtherAsian{get;set;}
	    public int? NoOfEthnicityOthherPacificIslander{get;set;}
	    public int? NoOfEthnicitySomeOtherRace{get;set;}
	    public int? NoOfEthnicityMoreThanOneRaceEthnicity{get;set;}
	    public int? NoOfEthnicityNotCollected{get;set;}
	    public int? NoOfAgeLessThan65{get;set;}
	    public int? NoOfAgeOver65{get;set;}
	    public int? NoOfAgeNotCollected{get;set;}
	    public int? NoOfGenderFemale{get;set;}
	    public int? NoOfGenderMale{get;set;}
	    public int? NoOfGenderNotCollected{get;set;}
	    public int? NoOfSpeaksAnotherLanguageOtherThanEnglish{get;set;}
	    public int? NoOfEnglishSpeakerOnly{get;set;}
	    public int? NoOfSpeaksAnotherLanguageNotCollected{get;set;}
	    public int? LastUpdatedBy{get;set;}
        public DateTime ResourceReportId { get; set; }
        #endregion
    }
}
