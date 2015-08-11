namespace Labo.Validation.Mvc4
{
    using System;
    using System.Reflection;

    /// <summary>
    /// The stub entity validation rule class.
    /// </summary>
    public sealed class StubEntityValidationRule : IEntityValidationRule
    {
        /// <summary>
        /// The validator
        /// </summary>
        private readonly IValidator m_Validator;

        /// <summary>
        /// The property function
        /// </summary>
        private readonly Func<object, object> m_PropertyFunc;

        /// <summary>
        /// The display name
        /// </summary>
        private readonly string m_DisplayName;

        /// <summary>
        /// The property name
        /// </summary>
        private readonly string m_PropertyName;

        /// <summary>
        /// Gets the validator.
        /// </summary>
        /// <value>
        /// The validator.
        /// </value>
        public IValidator Validator
        {
            get { return m_Validator; }
        }

        /// <summary>
        /// Gets the member information.
        /// </summary>
        /// <value>
        /// The member information.
        /// </value>
        public MemberInfo MemberInfo
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the name of the member.
        /// </summary>
        /// <value>
        /// The name of the member.
        /// </value>
        public string MemberName
        {
            get { return m_PropertyName; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StubEntityValidationRule"/> class.
        /// </summary>
        /// <param name="validator">The validator.</param>
        /// <param name="propertyFunc">The property function.</param>
        /// <param name="displayName">The member info.</param>
        /// <param name="propertyName">The property name.</param>
        /// <exception cref="System.ArgumentNullException">
        /// validator
        /// or
        /// propertyDisplayNameResolver
        /// or
        /// propertyExpression
        /// </exception>
        public StubEntityValidationRule(IValidator validator, Func<object, object> propertyFunc, string displayName, string propertyName)
        {
            if (validator == null)
            {
                throw new ArgumentNullException("validator");
            }

            if (propertyFunc == null)
            {
                throw new ArgumentNullException("propertyFunc");
            }

            m_Validator = validator;

            m_PropertyFunc = propertyFunc;
            m_DisplayName = displayName;
            m_PropertyName = propertyName;
        }

        /// <summary>
        /// Validates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The validation result.</returns>
        public ValidationResult Validate(object entity)
        {
            ValidationResult validationResult = ValidationResult.Empty();

            object propertyValue = m_PropertyFunc(entity);
            if (m_Validator.IsValid(propertyValue))
            {
                return validationResult;
            }

            string propertyDisplayName = GetDisplayName();

            validationResult.Errors.Add(new ValidationError
            {
                Message = m_Validator.GetValidationMessage(propertyDisplayName),
                PropertyName = m_PropertyName,
                TargetValue = entity
            });
            return validationResult;
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <returns>The display name.</returns>
        public string GetDisplayName()
        {
            return m_DisplayName;
        }
    }
}
