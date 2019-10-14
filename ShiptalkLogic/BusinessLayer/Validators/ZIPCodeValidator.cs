using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace ShiptalkLogic.BusinessLayer.Validators
{
    public class ZIPCodeValidator : ValueValidator<string>
    {
        public ZIPCodeValidator()
            : base(null, null, false)
        {
        }

        public ZIPCodeValidator(string messageTemplate)
            : base(messageTemplate, null, false)
        {
        }

        public ZIPCodeValidator(bool negated)
            : base(null, null, negated)
        {
        }

        #region Overrides of Validator<string>

        protected override void DoValidate(string objectToValidate, object currentTarget, string key, ValidationResults validationResults)
        {
            if (!new CCFBLL().IsZIPCodeValid(objectToValidate))
                LogValidationResult(validationResults, MessageTemplate, currentTarget, key);
        }

        #endregion

        #region Overrides of ValueValidator<string>

        protected override string DefaultNegatedMessageTemplate
        {
            get { return "The ZIP Code specified is not valid."; }
        }

        protected override string DefaultNonNegatedMessageTemplate
        {
            get { return "The ZIP Code specified is not valid."; }
        }

        #endregion
    }
}
