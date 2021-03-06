﻿namespace Labo.Validation.Builder
{
    /// <summary>
    /// The entity validation rule builder interface.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TProperty">The type of the entity property.</typeparam>
    public interface IEntityValidationRuleBuilder<TEntity, TProperty> : IEntityValidationRuleBuilderInitial<TEntity, TProperty>
    {
        /// <summary>
        /// Sets the specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <returns>The entity validation rule builder.</returns>
        IEntityValidationRuleBuilder<TEntity, TProperty> SetSpecification(ISpecification<TEntity> specification);

        /// <summary>
        /// Sets the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The entity validation rule builder.</returns>
        IEntityValidationRuleBuilder<TEntity, TProperty> SetMessage(string message);

        /// <summary>
        /// Builds the entity validation rule.
        /// </summary>
        void Build();

        /// <summary>
        /// Builds the entity validation rule.
        /// </summary>
        /// <param name="ruleSetName">Name of the rule set.</param>
        void Build(string ruleSetName);
    }
}
