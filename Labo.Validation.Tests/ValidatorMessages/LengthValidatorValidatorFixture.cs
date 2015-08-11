namespace Labo.Validation.Tests.ValidatorMessages
{
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class LengthValidatorValidatorFixture : ValidationMessageFixtureBase
    {
        public override string GetExpectedValidationMessage()
        {
            return "'Name' must be between 100 and 200 length.";
        }

        public override ValidatorBase CreateValidator()
        {
            return new LengthValidator(100, 200);
        }
    }
}