namespace Labo.Validation.Validators
{
    using System;

    /// <summary>
    /// The entity property validator class.
    /// </summary>
    public sealed class EntityPropertyValidator : IEntityPropertyValidator
    {
        /// <summary>
        /// The validator
        /// </summary>
        private readonly IValidator m_Validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityPropertyValidator"/> class.
        /// </summary>
        /// <param name="validator">The validator.</param>
        /// <exception cref="System.ArgumentNullException">validator</exception>
        public EntityPropertyValidator(IValidator validator)
        {
            if (validator == null)
            {
                throw new ArgumentNullException("validator");
            }

            m_Validator = validator;
        }

        /// <summary>
        /// Determines whether the specified property value for the entity is valid.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="propertyValue">The property value.</param>
        /// <returns><c>true</c> if the specified property value for the entity is valid, otherwise <c>false</c></returns>
        public bool IsValid(object entity, object propertyValue)
        {
            return m_Validator.IsValid(propertyValue);
        }

        /// <summary>
        /// Gets the validation message.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="valueName">Name of the value.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The validation message</returns>
        public string GetValidationMessage(object entity, string valueName, params string[] arguments)
        {
            return m_Validator.GetValidationMessage(valueName, arguments);
        }

        /// <summary>
        /// Gets the validator type name.
        /// </summary>
        /// <returns>The type name.</returns>
        public string GetValidatorTypeName()
        {
            return m_Validator.GetType().Name;
        }

        /// <summary>
        /// Gets the validator properties.
        /// </summary>
        /// <returns>
        /// The validator properties.
        /// </returns>
        public ValidatorProperties GetValidatorProperties()
        {
            return m_Validator.GetValidatorProperties();
        }
    }
}