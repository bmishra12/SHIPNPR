using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShiptalkLogic.BusinessObjects
{
    public interface IAddress : IModified
    {
        int? Id { get; set; }
        string Address1 { get; set; }
        string Address2 { get; set; }
        string City { get; set; }
        string Zip { get; set; }
        State? State { get; set; }
        County County { get; set; }
        
        //Added by Lavanya
        double? Latitude { get; set; }
        double? Longitude { get; set; }
        //end
    }
}
