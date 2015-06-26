namespace Labo.Validation.Tests.Validators
{
    using System.Collections.Generic;

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

        [Test]
        public void IsValidMustReturnTrueIfTheSpecifiedValueIsNotEmptyList()
        {
            NotEmptyValidator notEmptyValidator = new NotEmptyValidator();
            Assert.IsTrue(notEmptyValidator.IsValid(new List<int> { 1, 2 }));
        }

        [Test]
        public void IsValidMustReturnFalseIfTheSpecifiedValueIsEmptyList()
        {
            NotEmptyValidator notEmptyValidator = new NotEmptyValidator();
            Assert.IsFalse(notEmptyValidator.IsValid(new List<int>()));
        }

        [Test]
        public void IsValidMustReturnTrueIfTheSpecifiedValueIsNotIEnumarableAndString()
        {
            NotEmptyValidator notEmptyValidator = new NotEmptyValidator();
            Assert.IsTrue(notEmptyValidator.IsValid(new object()));
            Assert.IsTrue(notEmptyValidator.IsValid(1));
        }
    }
}
