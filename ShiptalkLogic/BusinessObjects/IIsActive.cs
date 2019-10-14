using System;

namespace ShiptalkLogic.BusinessObjects
{
    public interface IIsActive
    {
        bool IsActive { get; set; }
        DateTime? ActiveInactiveDate { get; set; }
    }
}