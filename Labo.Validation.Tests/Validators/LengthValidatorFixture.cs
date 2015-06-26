namespace Labo.Validation.Tests.Validators
{
    using System;
    using System.CodeDom;

    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class LengthValidatorFixture
    {
        [Test]
        public void IsValidMinLengthMustReturnTrueWhenTheSpecifiedValueLenghtIsBigger()
        {
            LengthValidator lengthValidator = new LengthValidator(1);
            
            Assert.IsTrue(lengthValidator.IsValid("1"));
            Assert.IsTrue(lengthValidator.IsValid("123"));
        }

        [Test]
        public void IsValidMinLengthMustReturnFalseWhenTheSpecifiedValueLenghtIsSmaller()
        {
            LengthValidator lengthValidator = new LengthValidator(5);

            Assert.IsFalse(lengthValidator.IsValid(string.Empty));
            Assert.IsFalse(lengthValidator.IsValid("1"));
            Assert.IsFalse(lengthValidator.IsValid("1234"));
        }

        [Test]
        public void IsValidMinLengthMustReturnTrueWhenTheSpecifiedValueIsNull()
        {
            LengthValidator lengthValidator = new LengthValidator(5);

            Assert.IsTrue(lengthValidator.IsValid(null));
        }

        [Test]
        public void IsValidMaxLengthMustReturnTrueWhenTheSpecifiedValueLenghtIsSmaller()
        {
            LengthValidator lengthValidator = LengthValidator.CreateMaxLengthValidator(3);

            Assert.IsTrue(lengthValidator.IsValid("1"));
            Assert.IsTrue(lengthValidator.IsValid("123"));
            Assert.IsTrue(lengthValidator.IsValid(string.Empty));
        }

        [Test]
        public void IsValidMaxLengthMustReturnFalseWhenTheSpecifiedValueLenghtIsBigger()
        {
            LengthValidator lengthValidator = LengthValidator.CreateMaxLengthValidator(1);

            Assert.IsFalse(lengthValidator.IsValid("12"));
            Assert.IsFalse(lengthValidator.IsValid("1234"));
        }

        [Test]
        public void IsValidMaxLengthMustReturnTrueWhenTheSpecifiedValueIsNull()
        {
            LengthValidator lengthValidator = LengthValidator.CreateMaxLengthValidator(1);

            Assert.IsTrue(lengthValidator.IsValid(null));
        }

        [Test]
        public void IsValidMinMaxLengthMustReturnTrueWhenTheSpecifiedValueLenghtIsBetweenTheBoundaries()
        {
            LengthValidator lengthValidator = new LengthValidator(1, 5);

            Assert.IsTrue(lengthValidator.IsValid("1"));
            Assert.IsTrue(lengthValidator.IsValid("123"));
            Assert.IsTrue(lengthValidator.IsValid("12345"));
        }

        [Test]
        public void IsValidBetweenLengthMustReturnTrueWhenTheSpecifiedValueLenghtIsOutsideTheBoundaries()
        {
            LengthValidator lengthValidator = new LengthValidator(1, 5);

            Assert.IsTrue(lengthValidator.IsValid("1"));
            Assert.IsTrue(lengthValidator.IsValid("123"));
            Assert.IsTrue(lengthValidator.IsValid("12345"));
        }

        [Test]
        public void IsValidBetweenLengthMustReturnFalseWhenTheSpecifiedValueLenghtIsOutsideTheBoundaries()
        {
            LengthValidator lengthValidator = new LengthValidator(2, 4);

            Assert.IsFalse(lengthValidator.IsValid(string.Empty));
            Assert.IsFalse(lengthValidator.IsValid("1"));
            Assert.IsFalse(lengthValidator.IsValid("12345"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructorMustThrowArgumentOutOfRangeExceptionWhenMinValueIsSmallerThan0()
        {
            new LengthValidator(-1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructorMustThrowArgumentOutOfRangeExceptionWhenMinValueIsBiggerThanMaxValue()
        {
            new LengthValidator(3, 1);
        }
    }
}
