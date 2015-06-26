namespace Labo.Validation.Tests.Validators
{
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class NotEmptyValidatorFixture
    {
        [Test]
        public void IsValidMustReturnFalseIfTheSpecifiedValueIsNullEmptyOrWhitespace()
        {
            NotEmptyValidator notEmptyValidator = new NotEmptyValidator();
            Assert.IsFalse(notEmptyValidator.IsValid(null));
            Assert.IsFalse(notEmptyValidator.IsValid(string.Empty));
            Assert.IsFalse(notEmptyValidator.IsValid("  "));
        }

        [Test]
        public void IsValidMustReturnTrueIfTheSpecifiedValueIsNotEmpty()
        {
            NotEmptyValidator notEmptyValidator = new NotEmptyValidator();
            Assert.IsTrue(notEmptyValidator.IsValid("1234"));
        }
    }
}
