using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace ShiptalkLogic.BusinessLayer.Validators
{
    public class CounselorZIPCodeValidator : ValueValidator<KeyValuePair<int, string>>
    {
        public CounselorZIPCodeValidator()
            : base(null, null, false)
        {
        }

        public CounselorZIPCodeValidator(string messageTemplate)
            : base(messageTemplate, null, false)
        {
        }

        public CounselorZIPCodeValidator(bool negated)
            : base(null, null, negated)
        {
        }

        #region Overrides of Validator<string>

        protected override void DoValidate(KeyValuePair<int, string> objectToValidate, object currentTarget, string key, ValidationResults validationResults)
        {
            if (!new CCFBLL().IsCounselingZIPCodeValid(objectToValidate.Key, objectToValidate.Value))
                LogValidationResult(validationResults, MessageTemplate, currentTarget, key);
        }

        #endregion

        #region Overrides of ValueValidator<string>

        protected override string DefaultNegatedMessageTemplate
        {
            get { return "The ZIP Code of Counselor Location is not valid for the Counselor specified."; }
        }

        protected override string DefaultNonNegatedMessageTemplate
        {
            get { return "The ZIP Code of Counselor Location is not valid for the Counselor specified."; }
        }

        #endregion
    }
}
