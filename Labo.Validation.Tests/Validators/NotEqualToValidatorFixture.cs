namespace Labo.Validation.Tests.Validators
{
    using System;

    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class NotEqualToValidatorFixture
    {
        [Test]
        public void IsValidCompareStringWithOrdinalIgnoreCaseComparer()
        {
            NotEqualToValidator notEqualToValidator = new NotEqualToValidator("TEST", StringComparer.OrdinalIgnoreCase);

            Assert.IsFalse(notEqualToValidator.IsValid("test"));
            Assert.IsFalse(notEqualToValidator.IsValid("TEST"));
        }

        [Test]
        public void IsValidCompareString()
        {
            NotEqualToValidator notEqualToValidator = new NotEqualToValidator("TEST");

            Assert.IsTrue(notEqualToValidator.IsValid("test"));
            Assert.IsTrue(notEqualToValidator.IsValid("Test"));
            Assert.IsFalse(notEqualToValidator.IsValid("TEST"));
        }

        [Test]
        public void IsValidCompareEmptyString()
        {
            NotEqualToValidator notEqualToValidator = new NotEqualToValidator(string.Empty);

            Assert.IsFalse(notEqualToValidator.IsValid(string.Empty));
        }

        [Test]
        public void IsValidCompareNullValue()
        {
            NotEqualToValidator notEqualToValidator = new NotEqualToValidator(string.Empty);

            Assert.IsTrue(notEqualToValidator.IsValid(null));
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShouldThrowArgumentNullExceptionWhenValueToCompareIsNull()
        {
            new NotEqualToValidator(null);
        }

        [Test]
        public void IsValidCompareIntegerValue()
        {
            NotEqualToValidator notEqualToValidator = new NotEqualToValidator(10);

            Assert.IsFalse(notEqualToValidator.IsValid(10));
            Assert.IsTrue(notEqualToValidator.IsValid(1));
        }

        [Test]
        public void IsValidCompareDifferentTypes()
        {
            NotEqualToValidator notEqualToValidator = new NotEqualToValidator(10);

            Assert.IsTrue(notEqualToValidator.IsValid(10M));
            Assert.IsTrue(notEqualToValidator.IsValid("10"));
        }
    }
}
