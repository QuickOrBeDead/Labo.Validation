namespace Labo.Validation.Tests.Validators
{
    using System.Globalization;

    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class EmailValidatorFixture
    {
        [Test]
        public void AssertValidEmailAddresses(
            [Values("test@gmail.com", "Test@gmail.com", "test+@gmail.com", "\"Abc\\@d\"@gmail.com", "$A12345@gmail.com", "ğüşıöçĞÜŞİÖÇ@gmail.com")]
            string email)
        {
            EmailValidator emailValidator = new EmailValidator();
            Assert.AreEqual(true, emailValidator.IsValid(email), string.Format(CultureInfo.CurrentCulture, "The email address '{0}' should be valid", email));
        }

        [Test]
        public void AssertInvalidEmailAddresses(
            [Values("test@gmail", "@gmail.com", "@")]
            string email)
        {
            EmailValidator emailValidator = new EmailValidator();
            Assert.AreEqual(false, emailValidator.IsValid(email), string.Format(CultureInfo.CurrentCulture, "The email address '{0}' should be invalid", email));
        }

        [Test]
        public void IsValidShouldReturnFalseWhenTheSpecifiedValueIsNotString()
        {
            EmailValidator emailValidator = new EmailValidator();
            Assert.AreEqual(false, emailValidator.IsValid(100));
            Assert.AreEqual(false, emailValidator.IsValid(new object()));
        }

        [Test]
        public void IsValidShouldReturnTrueWhenTheSpecifiedValueIsNull()
        {
            EmailValidator emailValidator = new EmailValidator();
            Assert.AreEqual(true, emailValidator.IsValid(null));
        }
    }
}
