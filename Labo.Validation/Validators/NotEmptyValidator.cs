namespace Labo.Validation.Validators
{
    using System.Collections;

    /// <summary>
    /// The not empty validator class.
    /// </summary>
    public sealed class NotEmptyValidator : IValidator
    {
        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is valid otherwise <c>false</c></returns>
        public bool IsValid(object value)
        {
            return value != null && !IsEmptyString(value) && !IsEmptyCollection(value);
        }

        /// <summary>
        /// Determines whether [is empty collection] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the value is an empty collection otherwise <c>false</c></returns>
        private static bool IsEmptyCollection(object value)
        {
            IEnumerable enumerable = value as IEnumerable;
            if (enumerable == null)
            {
                return false;
            }

            IEnumerator enumerator = enumerable.GetEnumerator();
            return !enumerator.MoveNext();
        }

        /// <summary>
        /// Determines whether [is empty string] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the value is an empty string otherwise <c>false</c></returns>
        private static bool IsEmptyString(object value)
        {
            string stringValue = value as string;
            return stringValue == null || string.IsNullOrWhiteSpace(stringValue);
        }
    }
}
