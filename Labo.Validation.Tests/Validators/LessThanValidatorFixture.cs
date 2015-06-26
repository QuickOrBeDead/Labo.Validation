namespace Labo.Validation.Tests.Validators
{
    using System;
    using System.Collections.Generic;

    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class LessThanValidatorFixture
    {
        [Test]
        public void IsValidShouldReturnTrueIfTheSpecifiedValueIsSmaller()
        {
            LessThanValidator validator = new LessThanValidator(-5);

            Assert.IsTrue(validator.IsValid(-6));
            Assert.IsTrue(validator.IsValid(-7));
        }

        [Test]
        public void IsValidShouldReturnFalseIfTheSpecifiedValueIsBiggerOrEqual()
        {
            LessThanValidator validator = new LessThanValidator(5);

            Assert.IsFalse(validator.IsValid(5));
            Assert.IsFalse(validator.IsValid(10));
        }

        [Test]
        public void IsValidShouldReturnFalseIfTheSpecifiedValueIsNotIComparable()
        {
            LessThanValidator validator = new LessThanValidator(5);

            Assert.IsFalse(validator.IsValid(new Dictionary<string, object>()));
            Assert.IsFalse(validator.IsValid(new List()));
        }

        [Test]
        public void IsValidShouldReturnTrueIfTheSpecifiedValueIsNull()
        {
            LessThanValidator validator = new LessThanValidator(5);

            Assert.IsTrue(validator.IsValid(null));
        }

        [Test]
        public void IsValidShouldReturnFalseIfTheSpecifiedValueIsNotSameType()
        {
            LessThanValidator validator = new LessThanValidator(5);

            Assert.IsFalse(validator.IsValid("0"));
            Assert.IsFalse(validator.IsValid(5M));
            Assert.IsFalse(validator.IsValid(5F));
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void WhenValueToCompareIsNullThenArgumentNullExceptionShouldBeThrown()
        {
            new LessThanValidator(null);
        }
    }
}