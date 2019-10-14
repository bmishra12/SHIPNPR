using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace ShiptalkLogic.BusinessLayer.Validators
{
    /// <summary>
    /// Describes a <see cref="CollectionCountValidator"/>.
    /// </summary>
    /// <remarks>
    /// Spin-off of the StringLengthValidatorAttribute class.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property
            | AttributeTargets.Field
            | AttributeTargets.Method
            | AttributeTargets.Parameter,
            AllowMultiple = true,
            Inherited = false)]
    public sealed class CollectionCountValidatorAttribute : ValueValidatorAttribute
    {
        #region Fields
        private int lowerBound;
        private RangeBoundaryType lowerBoundType;
        private int upperBound;
        private RangeBoundaryType upperBoundType;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the lower bound.
        /// </summary>
        /// <value>The lower bound.</value>
        public int LowerBound
        {
            get { return lowerBound; }
        }

        /// <summary>
        /// Gets the type of the lower bound.
        /// </summary>
        /// <value>The type of the lower bound.</value>
        public RangeBoundaryType LowerBoundType
        {
            get { return lowerBoundType; }
        }

        /// <summary>
        /// Gets the upper bound.
        /// </summary>
        /// <value>The upper bound.</value>
        public int UpperBound
        {
            get { return upperBound; }
        }

        /// <summary>
        /// Gets the type of the upper bound.
        /// </summary>
        /// <value>The type of the upper bound.</value>
        public RangeBoundaryType UpperBoundType
        {
            get { return upperBoundType; }
        }
        #endregion

        #region Construction
        /// <summary>
        /// <para>Initializes a new instance of the <see cref="CollectionCountValidatorAttribute"/> class with an upper bound constraint.</para>
        /// </summary>
        /// <param name="upperBound">The upper bound.</param>
        public CollectionCountValidatorAttribute(int upperBound)
            : this(0, RangeBoundaryType.Ignore, upperBound, RangeBoundaryType.Inclusive)
        { }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="CollectionCountValidatorAttribute"/> class with lower and 
        /// upper bound constraints.</para>
        /// </summary>
        /// <param name="lowerBound">The lower bound.</param>
        /// <param name="upperBound">The upper bound.</param>
        public CollectionCountValidatorAttribute(int lowerBound, int upperBound)
            : this(lowerBound, RangeBoundaryType.Inclusive, upperBound, RangeBoundaryType.Inclusive)
        { }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="CollectionCountValidatorAttribute"/> class with fully specified
        /// bound constraints.</para>
        /// </summary>
        /// <param name="lowerBound">The lower bound.</param>
        /// <param name="lowerBoundType">The indication of how to perform the lower bound check.</param>
        /// <param name="upperBound">The upper bound.</param>
        /// <param name="upperBoundType">The indication of how to perform the upper bound check.</param>
        /// <seealso cref="RangeBoundaryType"/>
        public CollectionCountValidatorAttribute(int lowerBound,
                RangeBoundaryType lowerBoundType,
                int upperBound,
                RangeBoundaryType upperBoundType)
        {
            this.lowerBound = lowerBound;
            this.lowerBoundType = lowerBoundType;
            this.upperBound = upperBound;
            this.upperBoundType = upperBoundType;
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Creates the <see cref="CollectionCountValidator"/> described by the configuration object.
        /// </summary>
        /// <param name="targetType">The type of object that will be validated by the validator.</param>
        /// <returns>The created <see cref="Validator"/>.</returns>
        protected override Validator DoCreateValidator(Type targetType)
        {
            return new CollectionCountValidator(this.lowerBound,
                    this.lowerBoundType,
                    this.upperBound,
                    this.upperBoundType,
                    Negated);
        }
        #endregion
    }
}
