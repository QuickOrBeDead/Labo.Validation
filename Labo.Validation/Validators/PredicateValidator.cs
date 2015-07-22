namespace Labo.Validation.Validators
{
    using System;

    /// <summary>
    /// The predicate validator class.
    /// </summary>
    public sealed class PredicateValidator : ValidatorBase
    {
        /// <summary>
        /// The predicate
        /// </summary>
        private readonly Predicate<object> m_Predicate;

        /// <summary>
        /// Gets the predicate.
        /// </summary>
        /// <value>
        /// The predicate.
        /// </value>
        public Predicate<object> Predicate
        {
            get
            {
                return m_Predicate;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PredicateValidator"/> class.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <exception cref="System.ArgumentNullException">predicate</exception>
        public PredicateValidator(Predicate<object> predicate)
            : base(Constants.ValidationMessageResourceNames.PREDICATE_VALIDATION_MESSAGE)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            m_Predicate = predicate;
        }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is valid otherwise <c>false</c></returns>
        public override bool IsValid(object value)
        {
            return m_Predicate(value);
        }

        /// <summary>
        /// Sets the validation message parameters.
        /// </summary>
        /// <param name="validationMessageBuilderParameterSetter">The validation message builder parameter setter.</param>
        protected override void SetValidationMessageParameters(Message.IValidationMessageBuilderParameterSetter validationMessageBuilderParameterSetter)
        {
        }
    }
}
