namespace Labo.Validation.Tests.ValidatorMessages
{
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class CreditCardValidatorFixture : ValidationMessageFixtureBase
    {
        public override string GetExpectedValidationMessage()
        {
            return "'Name' is not a valid credit card number.";
        }

        public override ValidatorBase CreateValidator()
        {
            return new CreditCardValidator();
        }
    }
}