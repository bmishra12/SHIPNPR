using System;
using System.Collections.Generic;
using System.Text;



namespace ShiptalkLogic.BusinessObjects
{
    public class UserProfile : IIsActive
    {
                
        public int UserId { get; set; }

        public string FirstName { get; set; }


        public string Honorifics { get; set; }


        public string LastName { get; set; }


        public string MiddleName { get; set; }


        public string NickName { get; set; }        

        public string PrimaryPhone { get; set; }       

        public string SecondaryEmail { get; set; }


        public string SecondaryPhone { get; set; }


        public string Suffix { get; set; }

        public string TempPrimaryEmail { get; set; }        


        #region IIsActive Members

        public bool IsActive
        {
            get;

            set;
        }

        public DateTime? ActiveInactiveDate
        {
            get;
            set;
        }

        public DateTime? LastPasswordChangeDate
        {
            get;
            set;
        }

        public DateTime? EmailChangeRequestDate
        {
            get;
            set;
        }
        #endregion
    }
}
