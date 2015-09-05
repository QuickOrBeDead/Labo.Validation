namespace Labo.Validation.Builder
{
    using Labo.Validation.Validators;

    /// <summary>
    /// The entity validation rule builder initial interface.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TProperty">The type of the entity property.</typeparam>
    public interface IEntityValidationRuleBuilderInitial<TEntity, TProperty>
    {
        /// <summary>
        /// Adds a validator to the builder.
        /// </summary>
        /// <param name="validator">The validator.</param>
        /// <returns>The entity validation rule builder.</returns>
        IEntityValidationRuleBuilder<TEntity, TProperty> AddValidator(IEntityPropertyValidator validator);
    }
}