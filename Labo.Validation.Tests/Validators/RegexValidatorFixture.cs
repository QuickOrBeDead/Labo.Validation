namespace Labo.Validation.Tests.Validators
{
    using System;

    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class RegexValidatorFixture
    {
        [Test]
        public void IsValidMustReturnTrueWhenTheSpecifiedValueIsMatch()
        {
            RegexValidator regexValidator = new RegexValidator(@"^[a-zA-Z0-9]\d{2}[a-zA-Z0-9](-\d{3}){2}[A-Za-z0-9]$");
            Assert.IsTrue(regexValidator.IsValid("1298-673-4192"));
            Assert.IsTrue(regexValidator.IsValid("A08Z-931-468A"));
        }

        [Test]
        public void IsValidMustReturnFalseWhenTheSpecifiedValueIsNotMatch()
        {
            RegexValidator regexValidator = new RegexValidator(@"^[a-zA-Z0-9]\d{2}[a-zA-Z0-9](-\d{3}){2}[A-Za-z0-9]$");
            Assert.IsFalse(regexValidator.IsValid("_A90-123-129X"));
            Assert.IsFalse(regexValidator.IsValid("12345-KKA-1230"));
            Assert.IsFalse(regexValidator.IsValid("0919-2893-1256"));
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorMustThrowArgumentNullExceptionWhenPredicateIsNull()
        {
            new RegexValidator(null);
        }
    }
}
