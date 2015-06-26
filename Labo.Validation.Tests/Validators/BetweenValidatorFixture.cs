namespace Labo.Validation.Tests.Validators
{
    using System;
    using System.Collections.Generic;

    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class BetweenValidatorFixture
    {
        [Test]
        public void FromValueAndToValuePropertiesShouldBeSetCorrectly()
        {
            BetweenValidator validator = new BetweenValidator(5, 10);

            Assert.AreEqual(5, validator.FromValue);
            Assert.AreEqual(10, validator.ToValue);
        }

        [Test]
        public void IsValidShouldReturnTrueIfTheSpecifiedValueIsBetweenBoundaries()
        {
            BetweenValidator validator = new BetweenValidator(-5, 5);

            Assert.IsTrue(validator.IsValid(1));
            Assert.IsTrue(validator.IsValid(0));
        }

        [Test]
        public void IsValidShouldReturnFalseIfTheSpecifiedValueIsNotBetweenBoundaries()
        {
            BetweenValidator validator = new BetweenValidator(-5, 5);

            Assert.IsFalse(validator.IsValid(10));
            Assert.IsFalse(validator.IsValid(-10));
        }

        [Test]
        public void IsValidShouldReturnFalseIfTheSpecifiedValueIsNotIComparable()
        {
            BetweenValidator validator = new BetweenValidator(-5, 5);

            Assert.IsFalse(validator.IsValid(new Dictionary<string, object>()));
            Assert.IsFalse(validator.IsValid(new List()));
        }

        [Test]
        public void IsValidShouldReturnTrueIfTheSpecifiedValueIsNull()
        {
            BetweenValidator validator = new BetweenValidator(-5, 5);

            Assert.IsTrue(validator.IsValid(null));
        }

        [Test]
        public void IsValidShouldReturnFalseIfTheSpecifiedValueIsNotSameType()
        {
            BetweenValidator validator = new BetweenValidator(-5, 5);

            Assert.IsFalse(validator.IsValid("0"));
            Assert.IsFalse(validator.IsValid(5M));
            Assert.IsFalse(validator.IsValid(5F));
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void WhenFromValueAndToValueAreNotSameTypeThenArgumentExceptionShouldBeThrown()
        {
            new BetweenValidator(-5, "0");
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WhenFromValueIsBiggerThanToValueAreNotSameTypeThenArgumentOutOfRangeExceptionShouldBeThrown()
        {
            new BetweenValidator(5, 4);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void WhenFromValueNullThenArgumentNullExceptionShouldBeThrown()
        {
            new BetweenValidator(null, 4);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void WhenToValueNullThenArgumentNullExceptionShouldBeThrown()
        {
            new BetweenValidator(5, null);
        }
    }
}
