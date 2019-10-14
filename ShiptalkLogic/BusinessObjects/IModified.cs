using System;

namespace ShiptalkLogic.BusinessObjects
{
    public interface IModified
    {
        int? CreatedBy { get; set; }
        DateTime? CreatedDate { get; set; }
        int? LastUpdatedBy { get; set; }
        DateTime? LastUpdatedDate { get; set; }
    }
}