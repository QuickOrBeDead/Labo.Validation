namespace Labo.Validation.Validators
{
    using System.Collections;

    /// <summary>
    /// The not empty validator class.
    /// </summary>
    public sealed class NotEmptyValidator : ValidatorBase
    {
        /// <summary>
        /// The static not empty validator instance.
        /// </summary>
        private static readonly NotEmptyValidator s_Instance = new NotEmptyValidator();

        /// <summary>
        /// Gets the static not empty validator instance.
        /// </summary>
        /// <value>
        /// The static not empty validator instance.
        /// </value>
        public static NotEmptyValidator Instance
        {
            get
            {
                return s_Instance;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotEmptyValidator"/> class.
        /// </summary>
        public NotEmptyValidator()
            : base(Constants.ValidationMessageResourceNames.NOT_EMPTY_VALIDATION_MESSAGE)
        {
        }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is valid otherwise <c>false</c></returns>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            string stringValue = value as string;
            if (stringValue != null)
            {
                return !IsEmptyString(stringValue);
            }

            IEnumerable enumerable = value as IEnumerable;
            if (enumerable != null)
            {
                return !IsEmptyCollection(enumerable);                
            }
         
            return true;
        }

        /// <summary>
        /// Sets the validation message parameters.
        /// </summary>
        /// <param name="validationMessageBuilderParameterSetter">The validation message builder parameter setter.</param>
        protected override void SetValidationMessageParameters(Message.IValidationMessageBuilderParameterSetter validationMessageBuilderParameterSetter)
        {
        }

        /// <summary>
        /// Determines whether [is empty collection] [the specified value].
        /// </summary>
        /// <param name="enumerable">The enumerable value.</param>
        /// <returns><c>true</c> if the value is an empty collection otherwise <c>false</c></returns>
        private static bool IsEmptyCollection(IEnumerable enumerable)
        {
            IEnumerator enumerator = enumerable.GetEnumerator();
            return !enumerator.MoveNext();
        }

        /// <summary>
        /// Determines whether [is empty string] [the specified value].
        /// </summary>
        /// <param name="stringValue">The string value.</param>
        /// <returns><c>true</c> if the value is an empty string otherwise <c>false</c></returns>
        private static bool IsEmptyString(string stringValue)
        {
            return string.IsNullOrWhiteSpace(stringValue);
        }
    }
}
