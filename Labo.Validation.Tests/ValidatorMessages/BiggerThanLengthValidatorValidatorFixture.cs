namespace Labo.Validation.Tests.ValidatorMessages
{
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class BiggerThanLengthValidatorValidatorFixture : ValidationMessageFixtureBase
    {
        public override string GetExpectedValidationMessage()
        {
            return "'Name' must be bigger than 500 length.";
        }

        public override ValidatorBase CreateValidator()
        {
            return new LengthValidator(500);
        }
    }
}