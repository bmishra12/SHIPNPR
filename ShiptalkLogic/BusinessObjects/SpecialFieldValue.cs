using System;

namespace ShiptalkLogic.BusinessObjects
{
    [Serializable]
    public sealed class SpecialFieldValue : IModified
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Range {get; set; }
        public int ValidationType { get; set; }
        public Boolean IsRequired { get; set; }

        #region Implementation of IModified

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        #endregion
    }
}
