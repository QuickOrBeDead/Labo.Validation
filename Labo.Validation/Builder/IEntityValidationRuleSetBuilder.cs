namespace Labo.Validation.Builder
{
    using System;
    using System.Linq.Expressions;

    using Labo.Validation;

    /// <summary>
    /// The entity validation rule set builder interface.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IEntityValidationRuleSetBuilder<TEntity>
    {
        /// <summary>
        /// Adds the entity validation rule to the entity validator.
        /// </summary>
        /// <param name="entityValidationRule">The entity validation rule.</param>
        /// <exception cref="System.ArgumentNullException">entityValidationRule</exception>
        void AddRule(IEntityValidationRule<TEntity> entityValidationRule);

        /// <summary>
        /// Adds the validation rule.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="entityValidationRuleBuilder">The entity validation rule builder.</param>
        /// <exception cref="System.ArgumentNullException">entityValidationRuleBuilder</exception>
        void AddRule<TProperty>(Func<EntityValidatorBase<TEntity>, IEntityValidationRuleBuilder<TEntity, TProperty>> entityValidationRuleBuilder);

        /// <summary>
        /// Adds a validation rule for the specified property.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>The entity validation rule builder.</returns>
        /// <exception cref="System.ArgumentNullException">expression</exception>
        IEntityValidationRuleBuilderInitial<TEntity, TProperty> RuleFor<TProperty>(Expression<Func<TEntity, TProperty>> expression);
    }
}
