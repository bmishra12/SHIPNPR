using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace ShiptalkLogic.BusinessLayer.Validators
{
    public class AgencyNameValidator : ValueValidator<string>
    {
        public AgencyNameValidator()
            : base(null, null, false)
        {
        }

        public AgencyNameValidator(string messageTemplate)
            : base(messageTemplate, null, false)
        {
        }

        public AgencyNameValidator(bool negated)
            : base(null, null, negated)
        {
        }

        #region Overrides of Validator<string>

        protected override void DoValidate(string objectToValidate, object currentTarget, string key, ValidationResults validationResults)
        {
            if (new AgencyBLL().DoesAgencyNameExist(objectToValidate))
                LogValidationResult(validationResults, MessageTemplate, currentTarget, key);
        }

        #endregion

        #region Overrides of ValueValidator<string>

        protected override string DefaultNegatedMessageTemplate
        {
            get { return "Agency Name must be unique"; }
        }

        protected override string DefaultNonNegatedMessageTemplate
        {
            get { return "Agency Name must not be unique"; }
        }

        #endregion
    }
}
