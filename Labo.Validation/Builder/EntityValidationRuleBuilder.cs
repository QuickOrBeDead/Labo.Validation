namespace Labo.Validation.Builder
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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
        private readonly EntityValidatorBase<TEntity> m_EntityValidatorBase;

        /// <summary>
        /// The property display name resolver
        /// </summary>
        private readonly IPropertyDisplayNameResolver m_PropertyDisplayNameResolver;

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
        /// The message
        /// </summary>
        private string m_Message;

        /// <summary>
        /// Gets the validators.
        /// </summary>
        /// <value>
        /// The validators.
        /// </value>
        internal IList<IValidator> Validators
        {
            get
            {
                return new ReadOnlyCollection<IValidator>(m_Validators);
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
        /// Initializes a new instance of the <see cref="EntityValidationRuleBuilder{TEntity, TProperty}"/> class.
        /// </summary>
        /// <param name="entityValidatorBase">The entity validator base.</param>
        /// <param name="propertyDisplayNameResolver">The property display name resolver.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="System.ArgumentNullException">propertyExpression</exception>
        public EntityValidationRuleBuilder(EntityValidatorBase<TEntity> entityValidatorBase, IPropertyDisplayNameResolver propertyDisplayNameResolver, Expression<Func<TEntity, TProperty>> propertyExpression)
        {
            if (entityValidatorBase == null)
            {
                throw new ArgumentNullException("entityValidatorBase");
            }

            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            if (propertyDisplayNameResolver == null)
            {
                throw new ArgumentNullException("propertyDisplayNameResolver");
            }

            m_EntityValidatorBase = entityValidatorBase;
            m_PropertyDisplayNameResolver = propertyDisplayNameResolver;
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
        /// Sets the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The entity validation rule builder.</returns>
        public IEntityValidationRuleBuilder<TEntity, TProperty> SetMessage(string message)
        {
            m_Message = message;

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

                m_EntityValidatorBase.AddValidationRule(new EntityPropertyValidationRule<TEntity, TProperty>(validator, m_PropertyDisplayNameResolver, m_PropertyExpression, m_Specification, m_Message));
            }
        }
    }
}