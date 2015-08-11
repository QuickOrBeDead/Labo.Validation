namespace Labo.Validation.Tests.ValidatorMessages
{
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class PhoneNumberValidatorFixture : ValidationMessageFixtureBase
    {
        public override string GetExpectedValidationMessage()
        {
            return "'Name' is not a valid phone number.";
        }

        public override ValidatorBase CreateValidator()
        {
            return new PhoneNumberValidator();
        }
    }
}