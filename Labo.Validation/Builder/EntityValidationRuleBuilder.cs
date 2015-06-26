namespace Labo.Validation.Builder
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// The entity validation rule builder class.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TProperty">The type of the entity property.</typeparam>
    public sealed class EntityValidationRuleBuilder<TEntity, TProperty> : IEntityValidationRuleBuilder<TEntity, TProperty>
    {
        /// <summary>
        /// The entity validator base
        /// </summary>
        private readonly ValidatorBase<TEntity> m_EntityValidatorBase;

        /// <summary>
        /// The property expression
        /// </summary>
        private readonly Expression<Func<TEntity, TProperty>> m_PropertyExpression;

        /// <summary>
        /// The validators
        /// </summary>
        private readonly IList<IValidator> m_Validators;

        /// <summary>
        /// The specification
        /// </summary>
        private ISpecification<TEntity> m_Specification;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityValidationRuleBuilder{TEntity, TProperty}"/> class.
        /// </summary>
        /// <param name="entityValidatorBase">The entity validator base.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="System.ArgumentNullException">propertyExpression</exception>
        public EntityValidationRuleBuilder(ValidatorBase<TEntity> entityValidatorBase, Expression<Func<TEntity, TProperty>> propertyExpression)
        {
            if (entityValidatorBase == null)
            {
                throw new ArgumentNullException("entityValidatorBase");
            }

            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            m_EntityValidatorBase = entityValidatorBase;
            m_PropertyExpression = propertyExpression;
            m_Validators = new List<IValidator>();
        }

        /// <summary>
        /// Sets the validator.
        /// </summary>
        /// <param name="validator">The validator.</param>
        /// <returns>The entity validation rule builder.</returns>
        public IEntityValidationRuleBuilder<TEntity, TProperty> AddValidator(IValidator validator)
        {
            if (validator == null)
            {
                throw new ArgumentNullException("validator");
            }

            m_Validators.Add(validator);

            return this;
        }

        /// <summary>
        /// Sets the specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <returns>The entity validation rule builder.</returns>
        public IEntityValidationRuleBuilder<TEntity, TProperty> SetSpecification(ISpecification<TEntity> specification)
        {
            m_Specification = specification;

            return this;
        }

        /// <summary>
        /// Builds the entity validation rule.
        /// </summary>
        public void Build()
        {
            for (int i = 0; i < m_Validators.Count; i++)
            {
                IValidator validator = m_Validators[i];

                m_EntityValidatorBase.AddValidationRule(new EntityPropertyValidationRule<TEntity, TProperty>(validator, m_PropertyExpression, m_Specification));
            }
        }
    }
}