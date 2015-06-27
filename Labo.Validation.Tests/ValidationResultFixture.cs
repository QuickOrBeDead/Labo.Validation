namespace Labo.Validation.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class ValidationResultFixture
    {
        [Test]
        public void IsValidMustReturnFalseWhenErrorCountIsBiggerThan0()
        {
            ValidationResult validationResult = new ValidationResult();
            validationResult.Errors = new ValidationErrorCollection { new ValidationError() };

            Assert.IsFalse(validationResult.IsValid);
        }

        [Test]
        public void IsValidMustReturnTrueWhenErrorCountIs0()
        {
            ValidationResult validationResult = new ValidationResult();
            validationResult.Errors = new ValidationErrorCollection();

            Assert.IsTrue(validationResult.IsValid);
        }

        [Test]
        public void IsValidMustReturnTrueForEmptyValidationResult()
        {
            Assert.IsTrue(ValidationResult.Empty().IsValid);
        }
    }
}
