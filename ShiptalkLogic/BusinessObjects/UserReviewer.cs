using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShiptalkLogic.BusinessObjects
{
    public class UserReviewer
    {
        private int _UserReviewerId;
        private int _UserAgencyId;
        private int _ReviewerId;
        private Int16 _FormId;
        private Int16 _ActionId;
        private int _CreatedBy;


        public UserReviewer(){}
        
        public UserReviewer(int UserReviewerId)
        {
            _UserReviewerId = UserReviewerId;
        }

        public int UserReviewerId
        {
            get{ return _UserReviewerId;}
            set{ _UserReviewerId = value;}
            
        }

        public int UserAgencyId
        {
            get { return _UserAgencyId; }
            set { _UserAgencyId = value; }
            
        }

        public int ReviewerId
        {
            get { return _ReviewerId; }
            set { _ReviewerId = value; }

        }

        public Int16 FormId
        {
            get { return _FormId; }
            set { _FormId = value; }
        }

        public Int16 ActionId
        {
            get { return _ActionId; }
            set { _ActionId = value; }

        }

        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }

        }



    }
}
