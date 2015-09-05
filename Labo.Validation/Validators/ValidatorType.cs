namespace Labo.Validation.Validators
{
    /// <summary>
    /// The validator type enumeration.
    /// </summary>
    public enum ValidatorType
    {
        /// <summary>
        /// The not null validator
        /// </summary>
        NotNullValidator,

        /// <summary>
        /// The not empty validator
        /// </summary>
        NotEmptyValidator,

        /// <summary>
        /// The email validator
        /// </summary>
        EmailValidator,

        /// <summary>
        /// The regex validator
        /// </summary>
        RegexValidator,

        /// <summary>
        /// The length validator
        /// </summary>
        LengthValidator,

        /// <summary>
        /// The greater than or equal to validator
        /// </summary>
        GreaterThanOrEqualToValidator,

        /// <summary>
        /// The less than or equal to validator
        /// </summary>
        LessThanOrEqualToValidator,

        /// <summary>
        /// The between validator
        /// </summary>
        BetweenValidator,

        /// <summary>
        /// The equal to validator
        /// </summary>
        EqualToValidator,

        /// <summary>
        /// The equal to entity property validator
        /// </summary>
        EqualToEntityPropertyValidator,

        /// <summary>
        /// The credit card validator
        /// </summary>
        CreditCardValidator,

        /// <summary>
        /// The greater than validator
        /// </summary>
        GreaterThanValidator,

        /// <summary>
        /// The less than validator
        /// </summary>
        LessThanValidator,

        /// <summary>
        /// The not equal to validator
        /// </summary>
        NotEqualToValidator,

        /// <summary>
        /// The predicate validator
        /// </summary>
        PredicateValidator
    }
}
