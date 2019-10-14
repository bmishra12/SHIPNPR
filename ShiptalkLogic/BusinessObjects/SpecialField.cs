using System;

namespace ShiptalkLogic.BusinessObjects
{
    public sealed class SpecialField : IModified
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public State State { get; set; }
        public FormType FormType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public ValidationType ValidationType { get; set; }
        public bool IsRequired { get; set; }
        public int Ordinal { get; set; }
        //
        public string Range { get; set; }
        //

        #region Implementation of IModified

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        #endregion
    }
}
