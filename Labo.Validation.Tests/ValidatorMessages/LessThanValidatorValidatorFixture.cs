namespace Labo.Validation.Tests.ValidatorMessages
{
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class LessThanValidatorValidatorFixture : ValidationMessageFixtureBase
    {
        public override string GetExpectedValidationMessage()
        {
            return "'Name' must be less than '300'.";
        }

        public override ValidatorBase CreateValidator()
        {
            return new LessThanValidator(300);
        }
    }
}