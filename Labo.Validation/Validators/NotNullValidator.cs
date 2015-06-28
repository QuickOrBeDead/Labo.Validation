namespace Labo.Validation.Validators
{
    /// <summary>
    /// The not null validator class.
    /// </summary>
    public sealed class NotNullValidator : IValidator
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
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is valid otherwise <c>false</c></returns>
        public bool IsValid(object value)
        {
            return value != null;
        }
    }
}
