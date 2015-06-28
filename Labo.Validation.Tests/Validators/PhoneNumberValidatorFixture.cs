namespace Labo.Validation.Tests.Validators
{
    using System.Globalization;

    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class PhoneNumberValidatorFixture
    {
        [Test, Sequential]
        public void AssertValidPhoneNumbers(
            [Values(
                "206-555-0144",
                "11111111111111",
                "222-288-44-44",
                "(222)-288-44-44",
                "222 288 44 44")]
                string phoneNo)
        {
            PhoneNumberValidator phoneNumberValidator = new PhoneNumberValidator();
            Assert.AreEqual(true, phoneNumberValidator.IsValid(phoneNo), string.Format(CultureInfo.CurrentCulture, "The phone number '{0}' should be valid", phoneNo));
        }

        [Test, Sequential]
        public void AssertInvalidPhoneNumbers(
            [Values(
                "206--555-0144",
                "222-288--44-44",
                "(222))-288-44-44",
                "222  288 44 44")]
                string phoneNo)
        {
            PhoneNumberValidator phoneNumberValidator = new PhoneNumberValidator();
            Assert.AreEqual(false, phoneNumberValidator.IsValid(phoneNo), string.Format(CultureInfo.CurrentCulture, "The phone number '{0}' should be invalid", phoneNo));
        }

        [Test]
        public void IsValidShouldReturnFalseWhenTheSpecifiedValueIsNotString()
        {
            PhoneNumberValidator phoneNumberValidator = new PhoneNumberValidator();
            Assert.AreEqual(false, phoneNumberValidator.IsValid(new object()));
        }

        [Test]
        public void IsValidShouldReturnTrueWhenTheSpecifiedValueIsNull()
        {
            PhoneNumberValidator phoneNumberValidator = new PhoneNumberValidator();
            Assert.AreEqual(true, phoneNumberValidator.IsValid(null));
        }
    }
}