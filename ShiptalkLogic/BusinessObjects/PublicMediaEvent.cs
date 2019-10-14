using System;
using System.Collections.Generic;
using System.Collections;

namespace ShiptalkLogic.BusinessObjects
{
    //[Serializable]
    //public sealed class PamPresenters
    //{

    //    public string PAMShipUser { get; set; }
    //    public string Affiliation { get; set; }
    //    public int HoursSpent { get; set; }

    //}


    public sealed class PublicMediaEvent : IModified
    {
        #region Properties

        #region Batch Upload Members.

        public string Action { get; set; } //TODO: Consider using an enum to constrain the types of action.
        public string RecordId { get; set; }

        #endregion

        public int? PamID { get; set; }
        
        public int? SubmitterUserID { get; set; }
        public string SubmitterName { get; set; }

        public int? ReviewerUserID { get; set; }
        
        public string AgencyCode { get; set; }
        public int? AgencyId { get; set; }
        public string AgencyName { get; set; }

        public int? InteractiveEstAttendees { get; set; }
        public int? InteractiveEstProvidedEnrollAssistance { get; set; }
        public int? BoothEstDirectContacts { get; set; }
        public int? BoothEstEstProvidedEnrollAssistance { get; set; }

        public int? DedicatedEstPersonsReached { get; set; }
        public int? DedicatedEstAnyEnrollmentAssistance { get; set; }
        public int? DedicatedEstPartDEnrollmentAssistance { get; set; }
        public int? DedicatedEstLISEnrollmentAssistance { get; set; }
        public int? DedicatedEstMSPEnrollmentAssistance { get; set; }
        public int? DedicatedEstOtherEnrollmentAssistance { get; set; }

        public int? RadioEstListenerReach { get; set; }
        public int? TVEstViewersReach { get; set; }

        public int? ElectronicEstPersonsViewingOrListening { get; set; }
        public int? PrintEstPersonsReading { get; set; }


        public DateTime? ActivityStartDate { get; set; }
        public DateTime? ActivityEndDate { get; set; }
        
        public string EventName { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactPhone { get; set; }

        public State EventState { get; set; }
        public string EventCountycode { get; set; }
        public string EventCountyName { get; set; }
        
        public string EventZIPCode { get; set; }
        public string EventCity { get; set; }
        public string EventStreet { get; set; }

        public bool IsBatchUploadData { get; set; }
        public string BatchStateUniqueID { get; set; }
        
       
        public IList<PAMTopic> PAMSelectedTopics { get; set; }
        public string OtherPamTopicSpecified { get; set; }


        public IList<PAMAudiance> PAMSelectedAudiences { get; set; }
        public string OtherPamAudienceSpecified { get; set; }


        public IList<SpecialFieldValue> CMSSpecialUseFields { get; set; }
        public IList<SpecialFieldValue> StateSpecialUseFields { get; set; }

        public IList<ShiptalkLogic.BusinessObjects.UI.PamPresenters> PamPresenters { get; set; } 



        #region Implementation of IModified

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        #endregion

        #endregion
    }
}
