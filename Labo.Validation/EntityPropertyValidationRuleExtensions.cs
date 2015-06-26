namespace Labo.Validation
{
    using System;
    using System.Collections;
    using System.Linq.Expressions;
    using System.Text.RegularExpressions;

    using Labo.Validation.Builder;
    using Labo.Validation.Validators;

    /// <summary>
    /// The entity property validation rule extension methods.
    /// </summary>
    public static class EntityPropertyValidationRuleExtensions
    {
        /// <summary>
        /// Adds a not null validator to the rule builder. 
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <returns>Rule builder.</returns>
        public static IEntityValidationRuleBuilder<TEntity, TProperty> NotNull<TEntity, TProperty>(this IEntityValidationRuleBuilderInitial<TEntity, TProperty> ruleBuilder)
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException("ruleBuilder");
            }

            return ruleBuilder.AddValidator(new NotNullValidator());
        }

        /// <summary>
        /// Adds a not empty validator to the rule builder. 
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <returns>Rule builder.</returns>
        public static IEntityValidationRuleBuilder<TEntity, TProperty> NotEmpty<TEntity, TProperty>(this IEntityValidationRuleBuilderInitial<TEntity, TProperty> ruleBuilder)
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException("ruleBuilder");
            }

            return ruleBuilder.AddValidator(new NotEmptyValidator());
        }

        /// <summary>
        /// Adds a length validator to the rule builder. 
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>Rule builder.</returns>
        public static IEntityValidationRuleBuilder<TEntity, string> Length<TEntity>(this IEntityValidationRuleBuilderInitial<TEntity, string> ruleBuilder, int min, int max)
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException("ruleBuilder");
            }

            return ruleBuilder.AddValidator(new LengthValidator(min, max));
        }

        /// <summary>
        /// Adds a length validator to the rule builder. 
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="exactLength">Length of the exact.</param>
        /// <returns>Rule builder.</returns>
        public static IEntityValidationRuleBuilder<TEntity, string> Length<TEntity>(this IEntityValidationRuleBuilderInitial<TEntity, string> ruleBuilder, int exactLength)
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException("ruleBuilder");
            }

            return ruleBuilder.AddValidator(new LengthValidator(exactLength, exactLength));
        }

        /// <summary>
        /// Adds a regular expression validator to the rule builder. 
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="regexOptions">The regex options.</param>
        /// <returns>Rule builder.</returns>
        public static IEntityValidationRuleBuilder<TEntity, string> Matches<TEntity>(this IEntityValidationRuleBuilderInitial<TEntity, string> ruleBuilder, string expression, RegexOptions regexOptions)
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException("ruleBuilder");
            }

            return ruleBuilder.AddValidator(new RegexValidator(expression, regexOptions));
        }

        /// <summary>
        /// Adds an email address regular expression validator to the rule builder. 
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <returns>Rule builder.</returns>
        public static IEntityValidationRuleBuilder<TEntity, string> EmailAddress<TEntity>(this IEntityValidationRuleBuilderInitial<TEntity, string> ruleBuilder)
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException("ruleBuilder");
            }

            return ruleBuilder.AddValidator(new EmailValidator());
        }

        /// <summary>
        /// Adds a not equal to validator to the rule builder. 
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="toCompare">The value to compare.</param>
        /// <param name="comparer">The comparer.</param>
        /// <returns>Rule builder.</returns>
        public static IEntityValidationRuleBuilder<TEntity, TProperty> NotEqualTo<TEntity, TProperty>(this IEntityValidationRuleBuilderInitial<TEntity, TProperty> ruleBuilder, TProperty toCompare, IEqualityComparer comparer = null)
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException("ruleBuilder");
            }

            return ruleBuilder.AddValidator(new NotEqualToValidator(toCompare, comparer));
        }

        /// <summary>
        /// Adds an equal to validator to the rule builder. 
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="toCompare">The value to compare.</param>
        /// <param name="comparer">The comparer.</param>
        /// <returns>Rule builder.</returns>
        public static IEntityValidationRuleBuilder<TEntity, TProperty> EqualTo<TEntity, TProperty>(this IEntityValidationRuleBuilderInitial<TEntity, TProperty> ruleBuilder, TProperty toCompare, IEqualityComparer comparer = null)
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException("ruleBuilder");
            }

            return ruleBuilder.AddValidator(new EqualToValidator(toCompare, comparer));
        }

        /// <summary>
        /// Adds a predicate validator to the rule builder.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Rule builder.</returns>
        /// <exception cref="System.ArgumentNullException">predicate</exception>
        public static IEntityValidationRuleBuilder<TEntity, TProperty> Must<TEntity, TProperty>(this IEntityValidationRuleBuilderInitial<TEntity, TProperty> ruleBuilder, Func<TEntity, bool> predicate)
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException("ruleBuilder");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            return ruleBuilder.AddValidator(new PredicateValidator(instance => predicate((TEntity)instance)));
        }

        /// <summary>
        /// Adds a less than validator to the rule builder.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="valueToCompare">The value to compare.</param>
        /// <returns>Rule builder.</returns>
        public static IEntityValidationRuleBuilder<TEntity, TProperty> LessThan<TEntity, TProperty>(this IEntityValidationRuleBuilderInitial<TEntity, TProperty> ruleBuilder, TProperty valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException("ruleBuilder");
            }

            return ruleBuilder.AddValidator(new LessThanValidator(valueToCompare));
        }

        /// <summary>
        /// Adds a less than validator to the rule builder.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="valueToCompare">The value to compare.</param>
        /// <returns>Rule builder.</returns>
        public static IEntityValidationRuleBuilder<TEntity, TProperty?> LessThan<TEntity, TProperty>(this IEntityValidationRuleBuilderInitial<TEntity, TProperty?> ruleBuilder, TProperty valueToCompare)
            where TProperty : struct, IComparable<TProperty>, IComparable
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException("ruleBuilder");
            }

            return ruleBuilder.AddValidator(new LessThanValidator(valueToCompare));
        }

        /// <summary>
        /// Adds a less than or equal to validator to the rule builder.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="valueToCompare">The value to compare.</param>
        /// <returns>Rule builder.</returns>
        public static IEntityValidationRuleBuilder<TEntity, TProperty> LessThanOrEqualTo<TEntity, TProperty>(this IEntityValidationRuleBuilderInitial<TEntity, TProperty> ruleBuilder, TProperty valueToCompare) 
            where TProperty : IComparable<TProperty>, IComparable
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException("ruleBuilder");
            }

            return ruleBuilder.AddValidator(new LessThanOrEqualToValidator(valueToCompare));
        }

        /// <summary>
        /// Adds a less than or equal to validator to the rule builder.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="valueToCompare">The value to compare.</param>
        /// <returns>Rule builder.</returns>
        public static IEntityValidationRuleBuilder<TEntity, TProperty?> LessThanOrEqualTo<TEntity, TProperty>(this IEntityValidationRuleBuilderInitial<TEntity, TProperty?> ruleBuilder, TProperty valueToCompare) 
            where TProperty : struct, IComparable<TProperty>, IComparable
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException("ruleBuilder");
            }

            return ruleBuilder.AddValidator(new LessThanOrEqualToValidator(valueToCompare));
        }

        /// <summary>
        /// Adds a greater than validator to the rule builder.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="valueToCompare">The value to compare.</param>
        /// <returns>Rule builder.</returns>
        public static IEntityValidationRuleBuilder<TEntity, TProperty> GreaterThan<TEntity, TProperty>(this IEntityValidationRuleBuilderInitial<TEntity, TProperty> ruleBuilder, TProperty valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException("ruleBuilder");
            }

            return ruleBuilder.AddValidator(new GreaterThanValidator(valueToCompare));
        }

        /// <summary>
        /// Adds a greater than validator to the rule builder.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="valueToCompare">The value to compare.</param>
        /// <returns>Rule builder.</returns>
        public static IEntityValidationRuleBuilder<TEntity, TProperty?> GreaterThan<TEntity, TProperty>(this IEntityValidationRuleBuilderInitial<TEntity, TProperty?> ruleBuilder, TProperty valueToCompare)
            where TProperty : struct, IComparable<TProperty>, IComparable
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException("ruleBuilder");
            }

            return ruleBuilder.AddValidator(new GreaterThanValidator(valueToCompare));
        }

        /// <summary>
        /// Adds a greater than or equal to validator to the rule builder.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="valueToCompare">The value to compare.</param>
        /// <returns>Rule builder.</returns>
        public static IEntityValidationRuleBuilder<TEntity, TProperty> GreaterThanOrEqualTo<TEntity, TProperty>(this IEntityValidationRuleBuilderInitial<TEntity, TProperty> ruleBuilder, TProperty valueToCompare) 
            where TProperty : IComparable<TProperty>, IComparable
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException("ruleBuilder");
            }

            return ruleBuilder.AddValidator(new GreaterThanOrEqualToValidator(valueToCompare));
        }

        /// <summary>
        /// Adds a greater than or equal to validator to the rule builder.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="valueToCompare">The value to compare.</param>
        /// <returns>Rule builder.</returns>
        public static IEntityValidationRuleBuilder<TEntity, TProperty?> GreaterThanOrEqualTo<TEntity, TProperty>(this IEntityValidationRuleBuilderInitial<TEntity, TProperty?> ruleBuilder, TProperty valueToCompare) 
            where TProperty : struct, IComparable<TProperty>, IComparable
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException("ruleBuilder");
            }

            return ruleBuilder.AddValidator(new GreaterThanOrEqualToValidator(valueToCompare));
        }

        /// <summary>
        /// Adds a between validator to the rule builder.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="from">The lowest value.</param>
        /// <param name="to">The highest value.</param>
        /// <returns>Rule builder.</returns>
        public static IEntityValidationRuleBuilder<TEntity, TProperty> Between<TEntity, TProperty>(this IEntityValidationRuleBuilderInitial<TEntity, TProperty> ruleBuilder, TProperty from, TProperty to) where TProperty : IComparable<TProperty>, IComparable
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException("ruleBuilder");
            }

            return ruleBuilder.AddValidator(new BetweenValidator(from, to));
        }

        /// <summary>
        /// Adds a between validator to the rule builder.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="from">The lowest value.</param>
        /// <param name="to">The highest value.</param>
        /// <returns>Rule builder.</returns>
        public static IEntityValidationRuleBuilder<TEntity, TProperty?> Between<TEntity, TProperty>(this IEntityValidationRuleBuilderInitial<TEntity, TProperty?> ruleBuilder, TProperty from, TProperty to)
            where TProperty : struct, IComparable<TProperty>, IComparable
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException("ruleBuilder");
            }

            return ruleBuilder.AddValidator(new BetweenValidator(from, to));
        }

        /// <summary>
        /// Adds a credit card validator to the rule builder.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <returns>Rule builder.</returns>
        public static IEntityValidationRuleBuilder<TEntity, string> CreditCard<TEntity>(this IEntityValidationRuleBuilderInitial<TEntity, string> ruleBuilder)
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException("ruleBuilder");
            }

            return ruleBuilder.AddValidator(new CreditCardValidator());
        }

        /// <summary>
        /// Sets a predicate to the rule builder that specifies when the validator should run. 
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Rule builder.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// ruleBuilder
        /// or
        /// predicate
        /// </exception>
        public static IEntityValidationRuleBuilder<TEntity, TProperty> When<TEntity, TProperty>(this IEntityValidationRuleBuilder<TEntity, TProperty> ruleBuilder, Expression<Func<TEntity, bool>> predicate)
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException("ruleBuilder");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            return ruleBuilder.SetSpecification(new Specification<TEntity>(predicate));
        }

        /// <summary>
        /// Sets a predicate to the rule builder that specifies when the validator should not run.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Rule builder.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// ruleBuilder
        /// or
        /// predicate
        /// </exception>
        public static IEntityValidationRuleBuilder<TEntity, TProperty> Unless<TEntity, TProperty>(this IEntityValidationRuleBuilder<TEntity, TProperty> ruleBuilder, Expression<Func<TEntity, bool>> predicate)
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException("ruleBuilder");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            return ruleBuilder.When(Expression.Lambda<Func<TEntity, bool>>(Expression.Not(predicate)));
        }
    }
}