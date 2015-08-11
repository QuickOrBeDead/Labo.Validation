namespace Labo.Validation.Tests.ValidatorMessages
{
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class UrlValidatorFixture : ValidationMessageFixtureBase
    {
        public override string GetExpectedValidationMessage()
        {
            return "'Name' is not a valid url.";
        }

        public override ValidatorBase CreateValidator()
        {
            return new UrlValidator();
        }
    }
}