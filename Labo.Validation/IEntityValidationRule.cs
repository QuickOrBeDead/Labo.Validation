namespace Labo.Validation
{
    using System.Reflection;

    using Labo.Validation.Validators;

    /// <summary>
    /// The entity validation rule interface.
    /// </summary>
    public interface IEntityValidationRule
    {
        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <returns>The display name.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        string GetDisplayName();

        /// <summary>
        /// Gets the validator.
        /// </summary>
        /// <value>
        /// The validator.
        /// </value>
        IEntityPropertyValidator Validator { get; }

        /// <summary>
        /// Gets the member information.
        /// </summary>
        /// <value>
        /// The member information.
        /// </value>
        MemberInfo MemberInfo { get; }

        /// <summary>
        /// Gets the name of the member.
        /// </summary>
        /// <value>
        /// The name of the member.
        /// </value>
        string MemberName { get; }

        /// <summary>
        /// Validates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The validation result.</returns>
        ValidationResult Validate(object entity);

        /// <summary>
        /// Gets the validation message.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The validation message</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        string GetValidationMessage(object entity);
    }

    /// <summary>
    /// The entity validation rule interface.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IEntityValidationRule<in TEntity> : IEntityValidationRule
    {
        /// <summary>
        /// Validates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The validation result.</returns>
        ValidationResult Validate(TEntity entity);

        /// <summary>
        /// Gets the validation message.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The validation message.</returns>
        string GetValidationMessage(TEntity entity);
    }
}
