namespace Labo.Validation.Tests.ValidatorMessages
{
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class GreaterThanValidatorFixture : ValidationMessageFixtureBase
    {
        public override string GetExpectedValidationMessage()
        {
            return "'Name' must be greater than '100'.";
        }

        public override ValidatorBase CreateValidator()
        {
            return new GreaterThanValidator(100);
        }
    }
}