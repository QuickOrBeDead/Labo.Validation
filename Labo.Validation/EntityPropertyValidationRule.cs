namespace Labo.Validation
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    using Labo.Common.Utils;
    using Labo.Validation.Transform;
    using Labo.Validation.Validators;

    /// <summary>
    /// The entity property validation rule class.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TProperty">The type of the entity property.</typeparam>
    public sealed class EntityPropertyValidationRule<TEntity, TProperty> : IEntityValidationRule<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// The specification
        /// </summary>
        private readonly ISpecification<TEntity> m_Specification;

        /// <summary>
        /// The message
        /// </summary>
        private readonly string m_Message;

        /// <summary>
        /// The validator
        /// </summary>
        private readonly IEntityPropertyValidator m_Validator;

        /// <summary>
        /// The property display name resolver
        /// </summary>
        private readonly IPropertyDisplayNameResolver m_PropertyDisplayNameResolver;

        /// <summary>
        /// The property function
        /// </summary>
        private readonly Func<TEntity, TProperty> m_PropertyFunc;

        /// <summary>
        /// The member information
        /// </summary>
        private readonly MemberInfo m_MemberInfo;

        /// <summary>
        /// Gets the validator.
        /// </summary>
        /// <value>
        /// The validator.
        /// </value>
        public IEntityPropertyValidator Validator
        {
            get
            {
                return m_Validator;
            }
        }

        /// <summary>
        /// Gets the specification.
        /// </summary>
        /// <value>
        /// The specification.
        /// </value>
        internal ISpecification<TEntity> Specification
        {
            get
            {
                return m_Specification;
            }
        }

        /// <summary>
        /// Gets the member information.
        /// </summary>
        /// <value>
        /// The member information.
        /// </value>
        public MemberInfo MemberInfo
        {
            get
            {
                return m_MemberInfo;
            }
        }

        /// <summary>
        /// Gets the name of the member.
        /// </summary>
        /// <value>
        /// The name of the member.
        /// </value>
        public string MemberName
        {
            get { return MemberInfo.Name; }
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        internal string Message
        {
            get
            {
                return m_Message;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityPropertyValidationRule{TEntity, TProperty}"/> class.
        /// </summary>
        /// <param name="validator">The validator.</param>
        /// <param name="propertyDisplayNameResolver">The property display name resolver.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="specification">The specification.</param>
        /// <param name="message">The message.</param>
        public EntityPropertyValidationRule(IEntityPropertyValidator validator, IPropertyDisplayNameResolver propertyDisplayNameResolver, Expression<Func<TEntity, TProperty>> propertyExpression, ISpecification<TEntity> specification = null, string message = null)
        {
            if (validator == null)
            {
                throw new ArgumentNullException("validator");
            }

            if (propertyDisplayNameResolver == null)
            {
                throw new ArgumentNullException("propertyDisplayNameResolver");
            }

            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            m_Validator = validator;
            m_PropertyDisplayNameResolver = propertyDisplayNameResolver;
            m_Specification = specification;
            m_Message = message;

            m_PropertyFunc = propertyExpression.Compile();
            m_MemberInfo = LinqUtils.GetMemberInfo(propertyExpression);
        }

        /// <summary>
        /// Validates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The validation result.</returns>
        public ValidationResult Validate(TEntity entity)
        {
            ValidationResult validationResult = ValidationResult.Empty();
            if (m_Specification != null && !m_Specification.IsSatisfiedBy(entity))
            {
                return validationResult;
            }

            object propertyValue = m_PropertyFunc(entity);
            if (m_Validator.IsValid(entity, propertyValue))
            {
                return validationResult;
            }

            MemberInfo memberInfo = GetMemberInfo();
            string message = GetValidationMessage(entity, memberInfo);

            validationResult.Errors.Add(new ValidationError
                                            {
                                                Message = message,
                                                PropertyName = memberInfo.Name,
                                                TargetValue = entity
                                            });
            return validationResult;
        }

        /// <summary>
        /// Gets the validation message.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The validation message</returns>
        public string GetValidationMessage(object entity)
        {
            TEntity value = entity as TEntity;
            return GetValidationMessage(value);
        }

        /// <summary>
        /// Gets the validation message.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The validation message</returns>
        public string GetValidationMessage(TEntity entity)
        {
            MemberInfo memberInfo = GetMemberInfo();
            return GetValidationMessage(entity, memberInfo);
        }

        /// <summary>
        /// Validates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The validation result.</returns>
        public ValidationResult Validate(object entity)
        {
            return Validate((TEntity)entity);
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <returns>The display name.</returns>
        public string GetDisplayName()
        {
            MemberInfo memberInfo = GetMemberInfo();

            return GetDisplayName(memberInfo);
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>The display name.</returns>
        private string GetDisplayName(MemberInfo memberInfo)
        {
            return m_PropertyDisplayNameResolver.GetDisplayName(memberInfo);
        }

        /// <summary>
        /// Gets the validation message.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>The validation message</returns>
        private string GetValidationMessage(TEntity entity, MemberInfo memberInfo)
        {
            string propertyDisplayName = GetDisplayName(memberInfo);

            return m_Message ?? m_Validator.GetValidationMessage(entity, propertyDisplayName);
        }

        /// <summary>
        /// Gets the member information.
        /// </summary>
        /// <returns>The member info.</returns>
        private MemberInfo GetMemberInfo()
        {
            IValidationTransformerManager validationTransformerManager = ValidatorSettings.ValidationTransformerManager;
            IValidationTransformer validationTransformer = validationTransformerManager.GetValidationTransformerForValidationModel(typeof(TEntity));
            MemberInfo memberInfo;

            if (validationTransformer == null)
            {
                memberInfo = MemberInfo;
            }
            else
            {
                MappingMemberInfo mappingMemberInfo = validationTransformer.TransformPropertyNameFromValidationModel(MemberName);
                memberInfo = mappingMemberInfo == null ? MemberInfo : mappingMemberInfo.MemberInfo;
            }

            return memberInfo;
        }
    }
}
