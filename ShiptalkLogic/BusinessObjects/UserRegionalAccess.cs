using System;
using System.Collections.Generic;
using System.Text;

namespace ShiptalkLogic.BusinessObjects
{
    public class UserRegionalAccessProfile
    {

        public int Id { get; set; }

        public int RegionId { get; set; }

        public bool IsDefaultRegion { get; set; }

        public bool IsAdmin { get; set; }

        public List<int> DescriptorIDList { get; set; }

    }
}
