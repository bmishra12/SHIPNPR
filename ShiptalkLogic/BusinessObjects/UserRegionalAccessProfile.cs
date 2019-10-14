using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;

namespace ShiptalkLogic.BusinessObjects
{
    public sealed class UserRegionalAccessProfile : IIsActive
    {
        public int Id { get; set; }
        public int RegionId { get; set; }
        public bool IsDefaultRegion { get; set; }
        public bool IsAdmin { get; set; }
        public int UserId { get; set; }
        public string RegionName { get; set; }
        public bool IsApproverDesignate { get; set; }

        //Super Editor of a person's jurisdiction will help the person
        //Edit any one's data in the jurisdiction.
        public bool IsSuperDataEditor { get; set; }
        
        private Scope? _RegionScope;
        public Scope? RegionScope 
        {
            get
            {
                return _RegionScope;
            }
            set
            {
                _RegionScope = (Scope?)value;
            }
        }

        private List<int> _DescriptorIDList = null;
        public List<int> DescriptorIDList {
            get { 
                if (_DescriptorIDList == null) _DescriptorIDList = new List<int>(); 
                return _DescriptorIDList; 
            }
            set { _DescriptorIDList = value; }
        }

        public UserRegionalAccessProfile()
        {
            DescriptorIDList = new List<int>();
        }

        #region Implementation of IIsActive

        public bool IsActive { get; set; }
        public DateTime? ActiveInactiveDate { get; set; }

        #endregion

    }
    
}
