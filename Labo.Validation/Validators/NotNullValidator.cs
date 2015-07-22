namespace Labo.Validation.Validators
{
    /// <summary>
    /// The not null validator class.
    /// </summary>
    public sealed class NotNullValidator : ValidatorBase
    {
        /// <summary>
        /// The static not null validator instance.
        /// </summary>
        private static readonly NotNullValidator s_Instance = new NotNullValidator();

        /// <summary>
        /// Gets the static not null validator instance.
        /// </summary>
        /// <value>
        /// The static not null validator instance.
        /// </value>
        public static NotNullValidator Instance
        {
            get
            {
                return s_Instance;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotNullValidator"/> class.
        /// </summary>
        public NotNullValidator()
            : base(Constants.ValidationMessageResourceNames.NOT_NULL_VALIDATION_MESSAGE)
        {
        }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is valid otherwise <c>false</c></returns>
        public override bool IsValid(object value)
        {
            return value != null;
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
