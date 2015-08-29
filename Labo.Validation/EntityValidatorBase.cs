namespace Labo.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Linq.Expressions;

    using Labo.Validation.Builder;
    using Labo.Validation.Exceptions;

    /// <summary>
    /// The entity validator base class.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public abstract class EntityValidatorBase<TEntity> : IEntityValidator<TEntity>
    {
        /// <summary>
        /// The validators
        /// </summary>
        private readonly SortedList<string, IList<IEntityValidationRule<TEntity>>> m_EntityValidationRules;

        /// <summary>
        /// The property display name resolver
        /// </summary>
        private readonly IPropertyDisplayNameResolver m_PropertyDisplayNameResolver;

        /// <summary>
        /// Gets the entity validation rules.
        /// </summary>
        /// <value>
        /// The entity validation rules.
        /// </value>
        public IList<IEntityValidationRule<TEntity>> EntityValidationRules
        {
            get
            {
                IList<IEntityValidationRule<TEntity>> entityValidationRules = GetValidationRulesByRuleSetName(string.Empty);

                return new ReadOnlyCollection<IEntityValidationRule<TEntity>>(entityValidationRules);
            }
        }

        /// <summary>
        /// Gets the entity validation rules.
        /// </summary>
        /// <value>
        /// The entity validation rules.
        /// </value>
        public IList<IEntityValidationRule> ValidationRules
        {
            get { return new ReadOnlyCollection<IEntityValidationRule>(EntityValidationRules.Cast<IEntityValidationRule>().ToList()); }
        }

        /// <summary>
        /// The all entity validation rules.
        /// </summary>
        internal SortedList<string, IList<IEntityValidationRule<TEntity>>> AllEntityValidationRules
        {
            get { return m_EntityValidationRules; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityValidatorBase{TEntity}"/> class.
        /// </summary>
        protected EntityValidatorBase()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityValidatorBase{TEntity}"/> class.
        /// </summary>
        /// <param name="propertyDisplayNameResolver">The property display name resolver.</param>
        protected EntityValidatorBase(IPropertyDisplayNameResolver propertyDisplayNameResolver)
        {
            m_EntityValidationRules = new SortedList<string, IList<IEntityValidationRule<TEntity>>>();
            m_PropertyDisplayNameResolver = propertyDisplayNameResolver ?? ValidatorSettings.PropertyDisplayNameResolver;
        }

        /// <summary>
        /// Validates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="ruleSetName">The rule set name.</param>
        /// <returns>The validation result.</returns>
        public ValidationResult Validate(TEntity entity, string ruleSetName = "")
        {
            if (ruleSetName == null)
            {
                throw new ArgumentNullException("ruleSetName");
            }

            ValidationResult result = new ValidationResult();

            IList<IEntityValidationRule<TEntity>> validationRules = GetValidationRulesByRuleSetName(ruleSetName);

            for (int i = 0; i < validationRules.Count; i++)
            {
                IEntityValidationRule<TEntity> entityValidationRule = validationRules[i];
                ValidationResult validationResult = entityValidationRule.Validate(entity);
                result.Errors.AddRange(validationResult.Errors);
            }

            return result;
        }

        /// <summary>
        /// Validates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="ruleSetName">The rule set name.</param>
        /// <returns>The validation result.</returns>
        public ValidationResult Validate(object entity, string ruleSetName = "")
        {
            return Validate((TEntity)entity, ruleSetName);
        }

        /// <summary>
        /// Adds the entity validation rule to the entity validator.
        /// </summary>
        /// <param name="entityValidationRule">The entity validation rule.</param>
        /// <exception cref="System.ArgumentNullException">entityValidationRule</exception>
        public void AddRule(IEntityValidationRule<TEntity> entityValidationRule)
        {
            if (entityValidationRule == null)
            {
                throw new ArgumentNullException("entityValidationRule");
            }

            AddValidationRule(string.Empty, entityValidationRule);
        }

        /// <summary>
        /// Adds the entity validation rule to the entity validator.
        /// </summary>
        /// <param name="ruleSetName">Name of the rule set.</param>
        /// <param name="entityValidationRule">The entity validation rule.</param>
        public void AddRule(string ruleSetName, IEntityValidationRule<TEntity> entityValidationRule)
        {
            if (ruleSetName == null)
            {
                throw new ArgumentNullException("ruleSetName");
            }

            if (entityValidationRule == null)
            {
                throw new ArgumentNullException("entityValidationRule");
            }

            AddValidationRule(ruleSetName, entityValidationRule);
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

            return new EntityValidationRuleBuilder<TEntity, TProperty>(this, m_PropertyDisplayNameResolver, expression);
        }

        /// <summary>
        /// Adds the rule set.
        /// </summary>
        /// <param name="ruleSetName">Name of the rule set.</param>
        /// <param name="entityValidationRuleBuilder">The entity validation rule builder.</param>
        public void AddRuleSet(string ruleSetName, Action<IEntityValidationRuleSetBuilder<TEntity>> entityValidationRuleBuilder)
        {
            if (ruleSetName == null)
            {
                throw new ArgumentNullException("ruleSetName");
            }

            if (entityValidationRuleBuilder == null)
            {
                throw new ArgumentNullException("entityValidationRuleBuilder");
            }

            entityValidationRuleBuilder(new EntityValidationRuleSetBuilder<TEntity>(m_PropertyDisplayNameResolver, this, ruleSetName));
        }

        /// <summary>
        /// Validates and throws exception when invalid.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="ruleSetName">The rule set name.</param>
        public void ValidateAndThrowException(TEntity entity, string ruleSetName = "")
        {
            ValidationResult validationResult = Validate(entity, ruleSetName);
            if (!validationResult.IsValid)
            {
                throw new ValidationException
                {
                    Errors = validationResult.Errors
                };
            }
        }

        /// <summary>
        /// Validates and throws exception when invalid.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="ruleSetName">The rule set name.</param>
        public void ValidateAndThrowException(object entity, string ruleSetName = "")
        {
            ValidateAndThrowException((TEntity)entity, ruleSetName);
        }

        internal IList<IEntityValidationRule<TEntity>> GetValidationRulesByRuleSetName(string ruleSetName = "")
        {
            if (ruleSetName == null)
            {
                throw new ArgumentNullException("ruleSetName");
            }

            IList<IEntityValidationRule<TEntity>> entityValidationRules;
            if (!m_EntityValidationRules.TryGetValue(ruleSetName, out entityValidationRules))
            {
                entityValidationRules = new List<IEntityValidationRule<TEntity>>(0);
            }
            return entityValidationRules;
        }

        private void AddValidationRule(string ruleSetName, IEntityValidationRule<TEntity> entityValidationRule)
        {
            if (ruleSetName == null)
            {
                throw new ArgumentNullException("ruleSetName");
            }

            if (entityValidationRule == null)
            {
                throw new ArgumentNullException("entityValidationRule");
            }

            IList<IEntityValidationRule<TEntity>> entityValidationRules;
            if (m_EntityValidationRules.TryGetValue(ruleSetName, out entityValidationRules))
            {
                entityValidationRules.Add(entityValidationRule);
            }
            else
            {
                m_EntityValidationRules.Add(ruleSetName, new List<IEntityValidationRule<TEntity>> { entityValidationRule });
            }
        }
    }
}
