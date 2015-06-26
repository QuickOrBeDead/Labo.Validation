namespace Labo.Validation
{
    using System;

    internal sealed class DefaultEntityValidatorFactory : IEntityValidatorFactory
    {
        public IEntityValidator<TEntity> GetValidator<TEntity>()
        {
            throw new NotImplementedException();
        }

        public IEntityValidator GetValidator(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
