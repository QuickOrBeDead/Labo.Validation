namespace Labo.Validation.Validators
{
    /// <summary>
    /// The credit card validator class.
    /// </summary>
    public sealed class CreditCardValidator : IValidator
    {
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

            // http://www.beachnet.com/~hstiles/cardtype.html
            string cardNumber = value.ToString().Replace("-", string.Empty);

            int checksum = 0;
            bool evenDigit = false;
            
            char[] digits = cardNumber.ToCharArray();
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                char digit = digits[i];
                if (!char.IsDigit(digit))
                {
                    return false;
                }

                int digitValue = (digit - '0') * (evenDigit ? 2 : 1);
                evenDigit = !evenDigit;

                while (digitValue > 0)
                {
                    checksum += digitValue % 10;
                    digitValue /= 10;
                }
            }

            return (checksum % 10) == 0;
        }
    }
}
