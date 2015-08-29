namespace Labo.Validation
{
    using System.Collections.Generic;

    /// <summary>
    /// The entity validator interface.
    /// </summary>
    public interface IEntityValidator
    {
        /// <summary>
        /// Validates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="ruleSetName">The rule set name.</param>
        /// <returns>The validation result.</returns>
        ValidationResult Validate(object entity, string ruleSetName = "");

        /// <summary>
        /// Validates and throws exception when invalid.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="ruleSetName">The rule set name.</param>
        void ValidateAndThrowException(object entity, string ruleSetName = "");

        /// <summary>
        /// Gets the entity validation rules.
        /// </summary>
        /// <value>
        /// The entity validation rules.
        /// </value>
        IList<IEntityValidationRule> ValidationRules { get; }
    }

    /// <summary>
    /// The entity validator interface.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IEntityValidator<TEntity> : IEntityValidator
    {
        /// <summary>
        /// Validates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="ruleSetName">The rule set name.</param>
        /// <returns>The validation result.</returns>
        ValidationResult Validate(TEntity entity, string ruleSetName = "");

        /// <summary>
        /// Validates and throws exception when invalid.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="ruleSetName">The rule set name.</param>
        void ValidateAndThrowException(TEntity entity, string ruleSetName = "");

        /// <summary>
        /// Gets the entity validation rules.
        /// </summary>
        /// <value>
        /// The entity validation rules.
        /// </value>
        IList<IEntityValidationRule<TEntity>> EntityValidationRules { get; }
    }
}
