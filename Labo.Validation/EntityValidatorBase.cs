namespace Labo.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq.Expressions;

    using Labo.Validation.Builder;

    /// <summary>
    /// The validator base class.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public abstract class ValidatorBase<TEntity> : IEntityValidator<TEntity>
    {
        /// <summary>
        /// The validators
        /// </summary>
        private readonly IList<IEntityValidationRule<TEntity>> m_EntityValidationRules;

        /// <summary>
        /// Gets the entity validation rules.
        /// </summary>
        /// <value>
        /// The entity validation rules.
        /// </value>
        internal IList<IEntityValidationRule<TEntity>> EntityValidationRules
        {
            get
            {
                return new ReadOnlyCollection<IEntityValidationRule<TEntity>>(m_EntityValidationRules);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorBase{TEntity}"/> class.
        /// </summary>
        protected ValidatorBase()
        {
            m_EntityValidationRules = new List<IEntityValidationRule<TEntity>>();
        }

        /// <summary>
        /// Validates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The validation result.</returns>
        public ValidationResult Validate(TEntity entity)
        {
            ValidationResult result = new ValidationResult();

            for (int i = 0; i < m_EntityValidationRules.Count; i++)
            {
                IEntityValidationRule<TEntity> entityValidationRule = m_EntityValidationRules[i];
                ValidationResult validationResult = entityValidationRule.Validate(entity);
                result.Errors.AddRange(validationResult.Errors);
            }

            return result;
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
        /// Adds the entity validation rule to the entity validator.
        /// </summary>
        /// <param name="entityValidationRule">The entity validation rule.</param>
        /// <exception cref="System.ArgumentNullException">entityValidationRule</exception>
        public void AddValidationRule(IEntityValidationRule<TEntity> entityValidationRule)
        {
            if (entityValidationRule == null)
            {
                throw new ArgumentNullException("entityValidationRule");
            }

            m_EntityValidationRules.Add(entityValidationRule);
        }

        /// <summary>
        /// Adds the validation rule.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="entityValidationRuleBuilder">The entity validation rule builder.</param>
        /// <exception cref="System.ArgumentNullException">entityValidationRuleBuilder</exception>
        public void AddValidationRule<TProperty>(Func<ValidatorBase<TEntity>, IEntityValidationRuleBuilder<TEntity, TProperty>> entityValidationRuleBuilder)
        {
            if (entityValidationRuleBuilder == null)
            {
                throw new ArgumentNullException("entityValidationRuleBuilder");
            }

            entityValidationRuleBuilder(this).Build();
        }

        /// <summary>
        /// Adds a validation rule for the specified property.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>The entity validation rule builder.</returns>
        /// <exception cref="System.ArgumentNullException">expression</exception>
        public IEntityValidationRuleBuilderInitial<TEntity, TProperty> RuleFor<TProperty>(Expression<Func<TEntity, TProperty>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            return new EntityValidationRuleBuilder<TEntity, TProperty>(this, expression);
        }
    }
}
