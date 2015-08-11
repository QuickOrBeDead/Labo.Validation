namespace Labo.Validation.Tests.ValidatorMessages
{
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class GreaterThanOrEqualToValidatorFixture : ValidationMessageFixtureBase
    {
        public override string GetExpectedValidationMessage()
        {
            return "'Name' must be greater than or equal to '10'.";
        }

        public override ValidatorBase CreateValidator()
        {
            return new GreaterThanOrEqualToValidator(10);
        }
    }
}