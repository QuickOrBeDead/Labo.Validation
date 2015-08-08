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
        /// <returns>The validation result.</returns>
        ValidationResult Validate(object entity);

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
        /// <returns>The validation result.</returns>
        ValidationResult Validate(TEntity entity);

        /// <summary>
        /// Gets the entity validation rules.
        /// </summary>
        /// <value>
        /// The entity validation rules.
        /// </value>
        IList<IEntityValidationRule<TEntity>> EntityValidationRules { get; }
    }
}
