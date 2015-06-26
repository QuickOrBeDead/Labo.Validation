namespace Labo.Validation
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// The specification interface.
    /// </summary>
    public interface ISpecification
    {
        /// <summary>
        /// Evaluates the specification against an entity.
        /// </summary>
        /// <param name="entity">The instance to evaluate the specification against.</param>
        /// <returns><c>true</c> if the specification was satisfied by the entity, otherwise <c>false</c>.</returns>
        bool IsSatisfiedBy(object entity);
    }

    /// <summary>
    /// The <see cref="ISpecification{TEntity}"/> interface defines a basic contract to express specifications declaratively.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity</typeparam>
    public interface ISpecification<TEntity> : ISpecification
    {
        /// <summary>
        /// Gets the expression that encapsulates the criteria of the specification.
        /// </summary>
        /// <value>
        /// The predicate expression.
        /// </value>
        Expression<Func<TEntity, bool>> Predicate { get; }

        /// <summary>
        /// Evaluates the specification against an entity of <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity"/> instance to evaluate the specification against.</param>
        /// <returns><c>true</c> if the specification was satisfied by the entity, otherwise <c>false</c>.</returns>
        bool IsSatisfiedBy(TEntity entity);
    }
}