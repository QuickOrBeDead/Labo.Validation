namespace Labo.Validation
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    using Labo.Common.Utils;

    /// <summary>
    /// The entity property validation rule class.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TProperty">The type of the entity property.</typeparam>
    public sealed class EntityPropertyValidationRule<TEntity, TProperty> : IEntityValidationRule<TEntity>
    {
        /// <summary>
        /// The specification
        /// </summary>
        private readonly ISpecification<TEntity> m_Specification;

        /// <summary>
        /// The validator
        /// </summary>
        private readonly IValidator m_Validator;

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
        internal IValidator Validator
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
        /// Initializes a new instance of the <see cref="EntityPropertyValidationRule{TEntity, TProperty}"/> class.
        /// </summary>
        /// <param name="validator">The validator.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="specification">The specification.</param>
        public EntityPropertyValidationRule(IValidator validator, Expression<Func<TEntity, TProperty>> propertyExpression, ISpecification<TEntity> specification = null)
        {
            if (validator == null)
            {
                throw new ArgumentNullException("validator");
            }

            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            m_Validator = validator;
            m_Specification = specification;

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
            if (m_Validator.IsValid(propertyValue))
            {
                return validationResult;
            }

            validationResult.Errors.Add(new ValidationError
                                            {
                                                Message = "xyz",
                                                PropertyName = m_MemberInfo.Name,
                                                TargetValue = entity
                                            });
            return validationResult;
        }
    }
}