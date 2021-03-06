﻿namespace Labo.Validation.Mvc4
{
    using System;
    using System.Reflection;

    using Labo.Validation.Validators;

    /// <summary>
    /// The stub entity validation rule class.
    /// </summary>
    public sealed class StubEntityValidationRule : IEntityValidationRule
    {
        /// <summary>
        /// The validator
        /// </summary>
        private readonly IEntityPropertyValidator m_Validator;

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
        public IEntityPropertyValidator Validator
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
        /// <param name="displayName">The member info.</param>
        /// <param name="propertyName">The property name.</param>
        /// <exception cref="System.ArgumentNullException">
        /// validator
        /// or
        /// propertyDisplayNameResolver
        /// or
        /// propertyExpression
        /// </exception>
        public StubEntityValidationRule(IEntityPropertyValidator validator, string displayName, string propertyName)
        {
            if (validator == null)
            {
                throw new ArgumentNullException("validator");
            }

            m_Validator = validator;

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

            if (m_Validator.IsValid(null, entity /* property value */))
            {
                return validationResult;
            }

            string validationMessage = GetValidationMessage(null);
            validationResult.Errors.Add(new ValidationError
            {
                Message = validationMessage,
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

        /// <summary>
        /// Gets the validation message.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The validation message
        /// </returns>
        public string GetValidationMessage(object entity)
        {
            string propertyDisplayName = GetDisplayName();

            string validationMessage = m_Validator.GetValidationMessage(entity, propertyDisplayName);
            return validationMessage;
        }
    }
}
