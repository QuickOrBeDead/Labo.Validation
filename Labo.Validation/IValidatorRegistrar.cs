namespace Labo.Validation
{
    /// <summary>
    /// The validator registrar interface.
    /// </summary>
    public interface IValidatorRegistrar
    {
        /// <summary>
        /// Registers the validator.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="validator">The validator.</param>
        void RegisterValidator<TEntity>(IEntityValidator<TEntity> validator);
    }
}