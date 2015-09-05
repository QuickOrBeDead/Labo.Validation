namespace Labo.Validation.Validators
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Reflection;

    using Labo.Validation.Message;

    /// <summary>
    /// The equal to entity property validator class.
    /// </summary>
    public sealed class EqualToEntityPropertyValidator : IEntityPropertyValidator
    {
        /// <summary>
        /// The member to compare function
        /// </summary>
        private readonly Func<object, object> m_MemberToCompareFunc;

        /// <summary>
        /// The member to compare member info
        /// </summary>
        private readonly MemberInfo m_MemberToCompareMemberInfo;

        /// <summary>
        /// The confirm property
        /// </summary>
        private readonly bool m_ConfirmProperty;

        /// <summary>
        /// The comparer
        /// </summary>
        private readonly IEqualityComparer m_Comparer;

        /// <summary>
        /// The validator properties
        /// </summary>
        private readonly ValidatorProperties m_ValidatorProperties;

        /// <summary>
        /// Gets the member to compare member info
        /// </summary>
        public MemberInfo MemberToCompareMemberInfo
        {
            get
            {
                return m_MemberToCompareMemberInfo;
            }
        }

        /// <summary>
        /// Gets the type of the owner.
        /// </summary>
        /// <value>
        /// The type of the owner.
        /// </value>
        public Type OwnerType { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EqualToEntityPropertyValidator"/> class.
        /// </summary>
        /// <param name="memberToCompareFunc">The member to compare function.</param>
        /// <param name="member">The member.</param>
        /// <param name="ownerType">The owner type.</param>
        /// <param name="confirmProperty">if set to <c>true</c> [confirm property].</param>
        /// <param name="comparer">The comparer.</param>
        /// <exception cref="System.ArgumentNullException">
        /// memberToCompareFunc
        /// or
        /// member
        /// or
        /// ownerType
        /// </exception>
        public EqualToEntityPropertyValidator(Func<object, object> memberToCompareFunc, MemberInfo member, Type ownerType, bool confirmProperty = true, IEqualityComparer comparer = null)
        {
            if (memberToCompareFunc == null)
            {
                throw new ArgumentNullException("memberToCompareFunc");
            }

            if (member == null)
            {
                throw new ArgumentNullException("member");
            }

            if (ownerType == null)
            {
                throw new ArgumentNullException("ownerType");
            }

            OwnerType = ownerType;
            m_MemberToCompareFunc = memberToCompareFunc;
            m_MemberToCompareMemberInfo = member;
            m_ConfirmProperty = confirmProperty;
            m_Comparer = comparer;
            m_ValidatorProperties = new ValidatorProperties
                                        {
                                            { Constants.ValidationMessageParameterNames.MEMBER_TO_COMPARE_MEMBER_INFO, m_MemberToCompareMemberInfo },
                                            { Constants.ValidationMessageParameterNames.OWNER_TYPE, ownerType }
                                        };
        }

        /// <summary>
        /// Determines whether the specified property value for the entity is valid.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="propertyValue">The property value.</param>
        /// <returns><c>true</c> if the specified property value for the entity is valid, otherwise <c>false</c></returns>
        public bool IsValid(object entity, object propertyValue)
        {
            return EqualToValidator.IsValid(propertyValue, GetValueToCompare(entity), m_Comparer);
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
            // TODO: Use property property name transformer and display name resolver
            // TODO: Test not equal to validator
            IValidationMessageBuilder messageBuilder = ValidatorBase.GetValidationMessageBuilder();
            string validationMessage = messageBuilder.SetMessageResourceName(Constants.ValidationMessageResourceNames.EQUAL_TO_VALIDATION_MESSAGE)
                                                     .SetParameter(Constants.ValidationMessageParameterNames.VALUE_TO_COMPARE, m_ConfirmProperty ? m_MemberToCompareMemberInfo.Name : Convert.ToString(GetValueToCompare(entity), CultureInfo.CurrentCulture))
                                                     .Build(valueName, arguments);

            return validationMessage;
        }

        /// <summary>
        /// Gets the value to compare.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The value to compare.</returns>
        private object GetValueToCompare(object entity)
        {
            return m_MemberToCompareFunc(entity);
        }

        /// <summary>
        /// Gets the validator type name.
        /// </summary>
        /// <returns>The type name.</returns>
        public string GetValidatorTypeName()
        {
            return ValidatorType.EqualToEntityPropertyValidator.ToString();
        }

        /// <summary>
        /// Gets the validator properties.
        /// </summary>
        /// <returns>The validator properties.</returns>
        public ValidatorProperties GetValidatorProperties()
        {
            return m_ValidatorProperties;
        }
    }
}