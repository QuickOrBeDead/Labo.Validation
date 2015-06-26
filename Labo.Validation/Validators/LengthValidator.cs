namespace Labo.Validation.Validators
{
    using System;

    /// <summary>
    /// The length validator class.
    /// </summary>
    public sealed class LengthValidator : IValidator
    {
        /// <summary>
        /// The minimum length.
        /// </summary>
        private readonly int m_Min;

        /// <summary>
        /// The maximum length.
        /// </summary>
        private readonly int m_Max;

        /// <summary>
        /// Initializes a new instance of the <see cref="LengthValidator"/> class.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// min;Min should be larger than -1.
        /// or
        /// max;Max should be larger than min.
        /// </exception>
        public LengthValidator(int min, int max = -1)
        {
            if (min < 0)
            {
                throw new ArgumentOutOfRangeException("min", "Min should be larger than -1.");
            }

            if (max != -1 && max < min)
            {
                throw new ArgumentOutOfRangeException("max", "Max should be larger than min.");
            }

            m_Min = min;
            m_Max = max;
        }

        /// <summary>
        /// Creates the maximum length validator.
        /// </summary>
        /// <param name="max">The maximum length.</param>
        /// <returns>A new maximum length validator.</returns>
        public static LengthValidator CreateMaxLengthValidator(int max)
        {
            return new LengthValidator(0, max);
        }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is valid otherwise <c>false</c></returns>
        public bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            int length = value.ToString().Length;

            return length >= m_Min && (length <= m_Max || m_Max == -1);
        }
    }
}