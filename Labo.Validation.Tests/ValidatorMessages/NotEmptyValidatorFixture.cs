namespace Labo.Validation.Tests.ValidatorMessages
{
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class NotEmptyValidatorFixture : ValidationMessageFixtureBase
    {
        public override string GetExpectedValidationMessage()
        {
            return "'Name' must not be empty.";
        }

        public override ValidatorBase CreateValidator()
        {
            return new NotEmptyValidator();
        }
    }
}