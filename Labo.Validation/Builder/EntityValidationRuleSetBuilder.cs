namespace Labo.Validation.Builder
{
    using System;
    using System.Linq.Expressions;

    using Labo.Validation;

    /// <summary>
    /// THe entity validation rule set builder class.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public sealed class EntityValidationRuleSetBuilder<TEntity> : IEntityValidationRuleSetBuilder<TEntity>
    {
        /// <summary>
        /// The entity validator
        /// </summary>
        private readonly EntityValidatorBase<TEntity> m_EntityValidator;

        /// <summary>
        /// The rule set name
        /// </summary>
        private readonly string m_RuleSetName;

        /// <summary>
        /// The property display name resolver
        /// </summary>
        private readonly IPropertyDisplayNameResolver m_PropertyDisplayNameResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityValidationRuleSetBuilder{TEntity}"/> class.
        /// </summary>
        /// <param name="propertyDisplayNameResolver">The property display name resolver.</param>
        /// <param name="entityValidator">The entity validator.</param>
        /// <param name="ruleSetName">The rule set name.</param>
        public EntityValidationRuleSetBuilder(IPropertyDisplayNameResolver propertyDisplayNameResolver, EntityValidatorBase<TEntity> entityValidator, string ruleSetName)
        {
            if (propertyDisplayNameResolver == null)
            {
                throw new ArgumentNullException("propertyDisplayNameResolver");
            }

            if (entityValidator == null)
            {
                throw new ArgumentNullException("entityValidator");
            }

            if (ruleSetName == null)
            {
                throw new ArgumentNullException("ruleSetName");
            }

            m_EntityValidator = entityValidator;
            m_RuleSetName = ruleSetName;
            m_PropertyDisplayNameResolver = propertyDisplayNameResolver;
        }

        /// <summary>
        /// Adds the entity validation rule to the entity validator.
        /// </summary>
        /// <param name="entityValidationRule">The entity validation rule.</param>
        /// <exception cref="System.ArgumentNullException">entityValidationRule</exception>
        public void AddRule(IEntityValidationRule<TEntity> entityValidationRule)
        {
            m_EntityValidator.AddRule(m_RuleSetName, entityValidationRule);
        }

        /// <summary>
        /// Adds the validation rule.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="entityValidationRuleBuilder">The entity validation rule builder.</param>
        /// <exception cref="System.ArgumentNullException">entityValidationRuleBuilder</exception>
        public void AddRule<TProperty>(Func<EntityValidatorBase<TEntity>, IEntityValidationRuleBuilder<TEntity, TProperty>> entityValidationRuleBuilder)
        {
            if (entityValidationRuleBuilder == null)
            {
                throw new ArgumentNullException("entityValidationRuleBuilder");
            }

            entityValidationRuleBuilder(m_EntityValidator).Build(m_RuleSetName);
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

            return new EntityValidationRuleBuilder<TEntity, TProperty>(m_EntityValidator, m_PropertyDisplayNameResolver, expression, m_RuleSetName);
        }
    }
}