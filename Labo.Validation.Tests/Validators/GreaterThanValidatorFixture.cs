namespace Labo.Validation.Tests.Validators
{
    using System;
    using System.Collections.Generic;

    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class GreaterThanValidatorFixture
    {
        [Test]
        public void IsValidShouldReturnTrueIfTheSpecifiedValueIsBigger()
        {
            GreaterThanValidator validator = new GreaterThanValidator(-5);

            Assert.IsTrue(validator.IsValid(1));
            Assert.IsTrue(validator.IsValid(-4));
        }

        [Test]
        public void IsValidShouldReturnFalseIfTheSpecifiedValueIsSmaller()
        {
            GreaterThanValidator validator = new GreaterThanValidator(5);

            Assert.IsFalse(validator.IsValid(5));
            Assert.IsFalse(validator.IsValid(-5));
        }

        [Test]
        public void IsValidShouldReturnFalseIfTheSpecifiedValueIsNotIComparable()
        {
            GreaterThanValidator validator = new GreaterThanValidator(5);

            Assert.IsFalse(validator.IsValid(new Dictionary<string, object>()));
            Assert.IsFalse(validator.IsValid(new List()));
        }

        [Test]
        public void IsValidShouldReturnTrueIfTheSpecifiedValueIsNull()
        {
            GreaterThanValidator validator = new GreaterThanValidator(5);

            Assert.IsTrue(validator.IsValid(null));
        }

        [Test]
        public void IsValidShouldReturnFalseIfTheSpecifiedValueIsNotSameType()
        {
            GreaterThanValidator validator = new GreaterThanValidator(4);

            Assert.IsFalse(validator.IsValid("0"));
            Assert.IsFalse(validator.IsValid(5M));
            Assert.IsFalse(validator.IsValid(5F));
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void WhenValueToCompareIsNullThenArgumentNullExceptionShouldBeThrown()
        {
            new GreaterThanValidator(null);
        }
    }
}