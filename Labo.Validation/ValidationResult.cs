namespace Labo.Validation
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// The validation result class.
    /// </summary>
    [DataContract]
    [Serializable]
    public sealed class ValidationResult
    {
        /// <summary>
        /// The errors
        /// </summary>
        private ValidationErrorCollection m_Errors;

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        [DataMember]
        public ValidationErrorCollection Errors
        {
            get
            {
                return m_Errors ?? (m_Errors = new ValidationErrorCollection());
            }

            set
            {
                m_Errors = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [is valid].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is valid]; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid
        {
            get
            {
                return Errors.Count == 0;
            }
        }

        /// <summary>
        /// Creates new empty validation result.
        /// </summary>
        /// <returns>The validation result.</returns>
        public static ValidationResult Empty()
        {
            return new ValidationResult();
        }
    }
}
