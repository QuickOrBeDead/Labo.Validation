namespace Labo.Validation.Validators
{
    using System;

    /// <summary>
    /// The between validator interface.
    /// </summary>
    public interface IBetweenValidator : IValidator
    {
        /// <summary>
        /// Gets from value.
        /// </summary>
        /// <value>
        /// The from value.
        /// </value>
        IComparable FromValue { get; }

        /// <summary>
        /// Gets the to value.
        /// </summary>
        /// <value>
        /// The to value.
        /// </value>
        IComparable ToValue { get; }
    }
}