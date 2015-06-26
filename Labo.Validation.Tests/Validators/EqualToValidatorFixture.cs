namespace Labo.Validation.Tests.Validators
{
    using System;

    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class EqualToValidatorFixture
    {
        [Test]
        public void IsValidCompareStringWithOrdinalIgnoreCaseComparer()
        {
            EqualToValidator equalToValidator = new EqualToValidator("TEST", StringComparer.OrdinalIgnoreCase);

            Assert.IsTrue(equalToValidator.IsValid("test"));
            Assert.IsTrue(equalToValidator.IsValid("TEST"));
        }

        [Test]
        public void IsValidCompareString()
        {
            EqualToValidator equalToValidator = new EqualToValidator("TEST");

            Assert.IsFalse(equalToValidator.IsValid("test"));
            Assert.IsFalse(equalToValidator.IsValid("Test"));
            Assert.IsTrue(equalToValidator.IsValid("TEST"));
        }

        [Test]
        public void IsValidCompareEmptyString()
        {
            EqualToValidator equalToValidator = new EqualToValidator(string.Empty);

            Assert.IsTrue(equalToValidator.IsValid(string.Empty));
        }

        [Test]
        public void IsValidCompareNullValue()
        {
            EqualToValidator equalToValidator = new EqualToValidator(string.Empty);

            Assert.IsFalse(equalToValidator.IsValid(null));
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShouldThrowArgumentNullExceptionWhenValueToCompareIsNull()
        {
            new EqualToValidator(null);
        }

        [Test]
        public void IsValidCompareIntegerValue()
        {
            EqualToValidator equalToValidator = new EqualToValidator(10);

            Assert.IsTrue(equalToValidator.IsValid(10));
            Assert.IsFalse(equalToValidator.IsValid(1));
        }

        [Test]
        public void IsValidCompareDifferentTypes()
        {
            EqualToValidator equalToValidator = new EqualToValidator(10);

            Assert.IsFalse(equalToValidator.IsValid(10M));
            Assert.IsFalse(equalToValidator.IsValid("10"));
        }
    }
}
