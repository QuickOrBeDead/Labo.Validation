namespace Labo.Validation.Tests.Validators
{
    using System;
    using System.Collections.Generic;

    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class LessThanOrEqualToValidatorFixture
    {
        [Test]
        public void IsValidShouldReturnTrueIfTheSpecifiedValueIsSmallerOrEqual()
        {
            LessThanOrEqualToValidator validator = new LessThanOrEqualToValidator(-5);

            Assert.IsTrue(validator.IsValid(-6));
            Assert.IsTrue(validator.IsValid(-5));
        }

        [Test]
        public void IsValidShouldReturnFalseIfTheSpecifiedValueIsBigger()
        {
            LessThanOrEqualToValidator validator = new LessThanOrEqualToValidator(5);

            Assert.IsFalse(validator.IsValid(6));
            Assert.IsFalse(validator.IsValid(10));
        }

        [Test]
        public void IsValidShouldReturnFalseIfTheSpecifiedValueIsNotIComparable()
        {
            LessThanOrEqualToValidator validator = new LessThanOrEqualToValidator(5);

            Assert.IsFalse(validator.IsValid(new Dictionary<string, object>()));
            Assert.IsFalse(validator.IsValid(new List()));
        }

        [Test]
        public void IsValidShouldReturnTrueIfTheSpecifiedValueIsNull()
        {
            LessThanOrEqualToValidator validator = new LessThanOrEqualToValidator(5);

            Assert.IsTrue(validator.IsValid(null));
        }

        [Test]
        public void IsValidShouldReturnFalseIfTheSpecifiedValueIsNotSameType()
        {
            LessThanOrEqualToValidator validator = new LessThanOrEqualToValidator(5);

            Assert.IsFalse(validator.IsValid("0"));
            Assert.IsFalse(validator.IsValid(5M));
            Assert.IsFalse(validator.IsValid(5F));
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void WhenValueToCompareIsNullThenArgumentNullExceptionShouldBeThrown()
        {
            new LessThanOrEqualToValidator(null);
        }
    }
}