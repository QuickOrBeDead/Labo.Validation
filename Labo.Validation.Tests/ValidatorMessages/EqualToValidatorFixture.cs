namespace Labo.Validation.Tests.ValidatorMessages
{
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class EqualToValidatorFixture : ValidationMessageFixtureBase
    {
        public override string GetExpectedValidationMessage()
        {
            return "'Name' must be equal to 'Test'.";
        }

        public override ValidatorBase CreateValidator()
        {
            return new EqualToValidator("Test");
        }
    }
}