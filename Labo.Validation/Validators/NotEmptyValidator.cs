namespace Labo.Validation.Validators
{
    using System.Collections;

    using Labo.Validation.Message;

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
        /// The validator properties
        /// </summary>
        private readonly ValidatorProperties m_ValidatorProperties;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotEmptyValidator"/> class.
        /// </summary>
        public NotEmptyValidator()
        {
            m_ValidatorProperties = new ValidatorProperties();
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
        /// Gets the validation message.
        /// </summary>
        /// <param name="valueName">Name of the value.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>
        /// The validation message
        /// </returns>
        public override string GetValidationMessage(string valueName, params string[] arguments)
        {
            IValidationMessageBuilder messageBuilder = GetValidationMessageBuilder();
            string validationMessage = messageBuilder.SetMessageResourceName(Constants.ValidationMessageResourceNames.NOT_EMPTY_VALIDATION_MESSAGE)
                                                     .Build(valueName, arguments);

            return validationMessage;
        }

        /// <summary>
        /// Gets the validator properties.
        /// </summary>
        /// <returns>
        /// The validator properties.
        /// </returns>
        public override ValidatorProperties GetValidatorProperties()
        {
            return m_ValidatorProperties;
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
