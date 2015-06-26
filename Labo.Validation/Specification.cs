namespace Labo.Validation
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// The specification class.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public sealed class Specification<TEntity> : ISpecification<TEntity>
    {
        /// <summary>
        /// The predicate
        /// </summary>
        private readonly Expression<Func<TEntity, bool>> m_Predicate;
        
        /// <summary>
        /// The predicate function
        /// </summary>
        private readonly Func<TEntity, bool> m_PredicateFunc;

        /// <summary>
        /// Initializes a new instance of the <see cref="Specification{TEntity}"/> class.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <exception cref="System.ArgumentNullException">predicate</exception>
        public Specification(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            m_Predicate = predicate;
            m_PredicateFunc = predicate.Compile();
        }

        /// <summary>
        /// Gets the expression that encapsulates the criteria of the specification.
        /// </summary>
        /// <value>
        /// The predicate expression.
        /// </value>
        public Expression<Func<TEntity, bool>> Predicate
        {
            get { return m_Predicate; }
        }

        /// <summary>
        /// Evaluates the specification against an entity of <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity"/> instance to evaluate the specification against.</param>
        /// <returns><c>true</c> if the specification was satisfied by the entity, otherwise <c>false</c>.</returns>
        public bool IsSatisfiedBy(TEntity entity)
        {
            return m_PredicateFunc.Invoke(entity);
        }

        /// <summary>
        /// Evaluates the specification against an entity.
        /// </summary>
        /// <param name="entity">The instance to evaluate the specification against.</param>
        /// <returns><c>true</c> if the specification was satisfied by the entity, otherwise <c>false</c>.</returns>
        public bool IsSatisfiedBy(object entity)
        {
            return IsSatisfiedBy((TEntity)entity);
        }
    }
}