namespace Labo.Validation.Tests.ValidatorMessages
{
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class SmallerThanLengthValidatorValidatorFixture : ValidationMessageFixtureBase
    {
        public override string GetExpectedValidationMessage()
        {
            return "'Name' must be smaller than 1000 length.";
        }

        public override ValidatorBase CreateValidator()
        {
            return new LengthValidator(0, 1000);
        }
    }
}