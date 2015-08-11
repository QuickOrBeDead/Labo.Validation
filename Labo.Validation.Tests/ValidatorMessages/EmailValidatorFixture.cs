namespace Labo.Validation.Tests.ValidatorMessages
{
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class EmailValidatorFixture : ValidationMessageFixtureBase
    {
        public override string GetExpectedValidationMessage()
        {
            return "'Name' is not a valid email address.";
        }

        public override ValidatorBase CreateValidator()
        {
            return new EmailValidator();
        }
    }
}