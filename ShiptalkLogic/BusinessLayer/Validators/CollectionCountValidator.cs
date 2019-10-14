using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace ShiptalkLogic.BusinessLayer.Validators
{
    /// <summary>
    /// Performs validation on collections by comparing their counts to the specified boundaries. 
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> is logged as a failure.
    /// Spin-off of the StringLengthValidator class.
    /// </remarks>
    public class CollectionCountValidator : ValueValidator<System.Collections.ICollection>
    {
        #region Fields
        // Patched because EntLib's RangeChecker is internal.
        // Obsolete StringLengthValidator logic commented and 
        // kept inline with code until it's made public.
        //private RangeChecker<int> rangeChecker;
        private RangeValidator<int> rangeValidator;
        private int lowerBound;
        private RangeBoundaryType lowerBoundType;
        private int upperBound;
        private RangeBoundaryType upperBoundType;
        #endregion

        #region Properties (testing only)
        internal int LowerBound
        {
            //            get { return this.rangeChecker.LowerBound; }
            get { return this.lowerBound; }
        }

        internal int UpperBound
        {
            //            get { return this.rangeChecker.UpperBound; }
            get { return this.upperBound; }
        }

        internal RangeBoundaryType LowerBoundType
        {
            //            get { return this.rangeChecker.LowerBoundType; }
            get { return this.lowerBoundType; }
        }

        internal RangeBoundaryType UpperBoundType
        {
            //            get { return this.rangeChecker.UpperBoundType; }
            get { return this.upperBoundType; }
        }
        #endregion

        #region Construction
        /// <summary>
        /// <para>Initializes a new instance of the <see cref="CollectionCountValidator"/> class with an upper bound constraint.</para>
        /// </summary>
        /// <param name="upperBound">The upper bound.</param>
        /// <remarks>
        /// No lower bound constraints will be checked by this instance, and the upper bound check will be <see cref="RangeBoundaryType.Inclusive"/>.
        /// </remarks>
        public CollectionCountValidator(int upperBound)
            : this(0, RangeBoundaryType.Ignore, upperBound, RangeBoundaryType.Inclusive)
        { }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="CollectionCountValidator"/> class with an upper bound constraint.</para>
        /// </summary>
        /// <param name="upperBound">The upper bound.</param>
        /// <param name="negated">True if the validator must negate the result of the validation.</param>
        /// <remarks>
        /// No lower bound constraints will be checked by this instance, and the upper bound check will be <see cref="RangeBoundaryType.Inclusive"/>.
        /// </remarks>
        public CollectionCountValidator(int upperBound, bool negated)
            : this(0, RangeBoundaryType.Ignore, upperBound, RangeBoundaryType.Inclusive, negated)
        { }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="CollectionCountValidator"/> class with lower and 
        /// upper bound constraints.</para>
        /// </summary>
        /// <param name="lowerBound">The lower bound.</param>
        /// <param name="upperBound">The upper bound.</param>
        /// <remarks>
        /// Both bound checks will be <see cref="RangeBoundaryType.Inclusive"/>.
        /// </remarks>
        public CollectionCountValidator(int lowerBound, int upperBound)
            : this(lowerBound, RangeBoundaryType.Inclusive, upperBound, RangeBoundaryType.Inclusive)
        { }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="CollectionCountValidator"/> class with lower and 
        /// upper bound constraints.</para>
        /// </summary>
        /// <param name="lowerBound">The lower bound.</param>
        /// <param name="upperBound">The upper bound.</param>
        /// <param name="negated">True if the validator must negate the result of the validation.</param>
        /// <remarks>
        /// Both bound checks will be <see cref="RangeBoundaryType.Inclusive"/>.
        /// </remarks>
        public CollectionCountValidator(int lowerBound, int upperBound, bool negated)
            : this(lowerBound, RangeBoundaryType.Inclusive, upperBound, RangeBoundaryType.Inclusive, negated)
        { }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="CollectionCountValidator"/> class with fully specified
        /// bound constraints.</para>
        /// </summary>
        /// <param name="lowerBound">The lower bound.</param>
        /// <param name="lowerBoundType">The indication of how to perform the lower bound check.</param>
        /// <param name="upperBound">The upper bound.</param>
        /// <param name="upperBoundType">The indication of how to perform the upper bound check.</param>
        /// <seealso cref="RangeBoundaryType"/>
        public CollectionCountValidator(int lowerBound, RangeBoundaryType lowerBoundType,
                int upperBound, RangeBoundaryType upperBoundType)
            : this(lowerBound, lowerBoundType, upperBound, upperBoundType, null)
        { }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="CollectionCountValidator"/> class with fully specified
        /// bound constraints.</para>
        /// </summary>
        /// <param name="lowerBound">The lower bound.</param>
        /// <param name="lowerBoundType">The indication of how to perform the lower bound check.</param>
        /// <param name="upperBound">The upper bound.</param>
        /// <param name="upperBoundType">The indication of how to perform the upper bound check.</param>
        /// <param name="negated">True if the validator must negate the result of the validation.</param>
        /// <seealso cref="RangeBoundaryType"/>
        public CollectionCountValidator(int lowerBound, RangeBoundaryType lowerBoundType,
                int upperBound, RangeBoundaryType upperBoundType, bool negated)
            : this(lowerBound, lowerBoundType, upperBound, upperBoundType, null, negated)
        { }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="CollectionCountValidator"/> class with fully specified
        /// bound constraints and a message template.</para>
        /// </summary>
        /// <param name="lowerBound">The lower bound.</param>
        /// <param name="lowerBoundType">The indication of how to perform the lower bound check.</param>
        /// <param name="upperBound">The upper bound.</param>
        /// <param name="upperBoundType">The indication of how to perform the upper bound check.</param>
        /// <param name="messageTemplate">The message template to use when logging results.</param>
        /// <seealso cref="RangeBoundaryType"/>
        public CollectionCountValidator(int lowerBound, RangeBoundaryType lowerBoundType,
                int upperBound, RangeBoundaryType upperBoundType,
                string messageTemplate)
            : this(lowerBound, lowerBoundType, upperBound, upperBoundType, messageTemplate, false)
        {
            //            this.rangeChecker = new RangeChecker<int>(lowerBound, lowerBoundType, upperBound, upperBoundType);
            this.rangeValidator = new RangeValidator<int>(lowerBound, lowerBoundType, upperBound, upperBoundType, messageTemplate, false);
            this.lowerBound = lowerBound;
            this.lowerBoundType = lowerBoundType;
            this.upperBound = upperBound;
            this.upperBoundType = upperBoundType;
        }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="CollectionCountValidator"/> class with fully specified
        /// bound constraints and a message template.</para>
        /// </summary>
        /// <param name="lowerBound">The lower bound.</param>
        /// <param name="lowerBoundType">The indication of how to perform the lower bound check.</param>
        /// <param name="upperBound">The upper bound.</param>
        /// <param name="upperBoundType">The indication of how to perform the upper bound check.</param>
        /// <param name="messageTemplate">The message template to use when logging results.</param>
        /// <param name="negated">True if the validator must negate the result of the validation.</param>
        /// <seealso cref="RangeBoundaryType"/>
        public CollectionCountValidator(int lowerBound, RangeBoundaryType lowerBoundType,
                int upperBound, RangeBoundaryType upperBoundType,
                string messageTemplate,
                bool negated)
            : base(messageTemplate, null, negated)
        {
            //            this.rangeChecker = new RangeChecker<int>(lowerBound, lowerBoundType, upperBound, upperBoundType);
            this.rangeValidator = new RangeValidator<int>(lowerBound, lowerBoundType, upperBound, upperBoundType, messageTemplate, negated);
            this.lowerBound = lowerBound;
            this.lowerBoundType = lowerBoundType;
            this.upperBound = upperBound;
            this.upperBoundType = upperBoundType;
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Validates by comparing the length for <paramref name="objectToValidate"/> with the constraints
        /// specified for the validator.
        /// </summary>
        /// <param name="objectToValidate">The object to validate.</param>
        /// <param name="currentTarget">The object on the behalf of which the validation is performed.</param>
        /// <param name="key">The key that identifies the source of <paramref name="objectToValidate"/>.</param>
        /// <param name="validationResults">The validation results to which the outcome of the validation should be stored.</param>
        /// <remarks>
        /// <see langword="null"/> is considered a failed validation.
        /// </remarks>
        protected override void DoValidate(ICollection objectToValidate,
                object currentTarget,
                string key,
                ValidationResults validationResults)
        {
            if (objectToValidate != null)
            {
                //if (this.rangeChecker.IsInRange(objectToValidate.Length) == Negated)
                if (!this.rangeValidator.Validate(objectToValidate.Count).IsValid)
                {
                    LogValidationResult(validationResults, GetMessage(objectToValidate, key), currentTarget, key);
                }
            }
            else
            {
                LogValidationResult(validationResults, GetMessage(objectToValidate, key), currentTarget, key);
            }
        }

        /// <summary>
        /// Gets the message representing a failed validation.
        /// </summary>
        /// <param name="objectToValidate">The object for which validation was performed.</param>
        /// <param name="key">The key representing the value being validated for <paramref name="objectToValidate"/>.</param>
        /// <returns>The message representing the validation failure.</returns>
        protected override string GetMessage(object objectToValidate, string key)
        {
            return string.Format(CultureInfo.CurrentUICulture,
                    this.MessageTemplate,
                    objectToValidate,
                    key,
                    this.Tag,
                //this.rangeChecker.LowerBound,
                //this.rangeChecker.LowerBoundType,
                //this.rangeChecker.UpperBound,
                //this.rangeChecker.UpperBoundType);
                    this.lowerBound,
                    this.lowerBoundType,
                    this.upperBound,
                    this.upperBoundType);
        }

        /// <summary>
        /// Gets the Default Message Template when the validator is non negated.
        /// </summary>
        protected override string DefaultNonNegatedMessageTemplate
        {
            get
            {
                return string.Empty;//Resources.CollectionCountValidatorNonNegatedDefaultMessageTemplate;
            }
        }

        /// <summary>
        /// Gets the Default Message Template when the validator is negated.
        /// </summary>
        protected override string DefaultNegatedMessageTemplate
        {
            get
            {
                return string.Empty;//Resources.CollectionCountValidatorNegatedDefaultMessageTemplate; }
            }
        #endregion

        }

    }
}