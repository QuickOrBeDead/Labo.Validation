namespace Labo.Validation.Tests.ValidatorMessages
{
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class BetweenValidatorFixture : ValidationMessageFixtureBase
    {
        public override string GetExpectedValidationMessage()
        {
            return "'Name' must be between 5 and 10.";
        }

        public override ValidatorBase CreateValidator()
        {
            return new BetweenValidator(5, 10);
        }
    }
}
