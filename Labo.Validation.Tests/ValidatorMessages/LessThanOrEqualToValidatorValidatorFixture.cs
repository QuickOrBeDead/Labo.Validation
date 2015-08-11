namespace Labo.Validation.Tests.ValidatorMessages
{
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class LessThanOrEqualToValidatorValidatorFixture : ValidationMessageFixtureBase
    {
        public override string GetExpectedValidationMessage()
        {
            return "'Name' must be less than or equal to '250'.";
        }

        public override ValidatorBase CreateValidator()
        {
            return new LessThanOrEqualToValidator(250);
        }
    }
}