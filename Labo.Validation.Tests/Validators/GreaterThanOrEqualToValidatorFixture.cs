using NUnit.Framework;

namespace Labo.Validation.Tests.Validators
{
    using System;
    using System.Collections.Generic;

    using Labo.Validation.Validators;

    [TestFixture]
    public class GreaterThanOrEqualToValidatorFixture
    {
        [Test]
        public void IsValidShouldReturnTrueIfTheSpecifiedValueIsBiggerOrEqual()
        {
            GreaterThanOrEqualToValidator validator = new GreaterThanOrEqualToValidator(-5);

            Assert.IsTrue(validator.IsValid(1));
            Assert.IsTrue(validator.IsValid(-5));
        }

        [Test]
        public void IsValidShouldReturnFalseIfTheSpecifiedValueIsSmaller()
        {
            GreaterThanOrEqualToValidator validator = new GreaterThanOrEqualToValidator(5);

            Assert.IsFalse(validator.IsValid(4));
            Assert.IsFalse(validator.IsValid(-5));
        }

        [Test]
        public void IsValidShouldReturnFalseIfTheSpecifiedValueIsNotIComparable()
        {
            GreaterThanOrEqualToValidator validator = new GreaterThanOrEqualToValidator(5);

            Assert.IsFalse(validator.IsValid(new Dictionary<string, object>()));
            Assert.IsFalse(validator.IsValid(new List()));
        }

        [Test]
        public void IsValidShouldReturnTrueIfTheSpecifiedValueIsNull()
        {
            GreaterThanOrEqualToValidator validator = new GreaterThanOrEqualToValidator(5);

            Assert.IsTrue(validator.IsValid(null));
        }

        [Test]
        public void IsValidShouldReturnFalseIfTheSpecifiedValueIsNotSameType()
        {
            GreaterThanOrEqualToValidator validator = new GreaterThanOrEqualToValidator(5);

            Assert.IsFalse(validator.IsValid("0"));
            Assert.IsFalse(validator.IsValid(5M));
            Assert.IsFalse(validator.IsValid(5F));
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void WhenValueToCompareIsNullThenArgumentNullExceptionShouldBeThrown()
        {
            new GreaterThanOrEqualToValidator(null);
        }
    }
}
