namespace Labo.Validation.Tests.ValidatorMessages
{
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class RegexValidatorFixture : ValidationMessageFixtureBase
    {
        public override string GetExpectedValidationMessage()
        {
            return "'Name' is not in the correct format.";
        }

        public override ValidatorBase CreateValidator()
        {
            return new RegexValidator(@"^\d$");
        }
    }
}