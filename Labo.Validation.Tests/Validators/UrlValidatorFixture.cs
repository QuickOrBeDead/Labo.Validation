namespace Labo.Validation.Tests.Validators
{
    using System.Globalization;

    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class UrlValidatorFixture
    {
        [Test, Sequential]
        public void AssertValidUrls(
            [Values(
                "http://google.com",
                "http://apps.google.com",
                "http://www.google.com",
                "http://ms1.apps.google.com",
                "https://google.com",
                "https://apps.google.com",
                "https://www.google.com",
                "https://ms1.apps.google.com",
                "ftp://google.com",
                "ftp://ftp.google.com",
                "http://google.it")]
                string url)
        {
            UrlValidator urlValidator = new UrlValidator();
            Assert.AreEqual(true, urlValidator.IsValid(url), string.Format(CultureInfo.CurrentCulture, "The url '{0}' should be valid", url));
        }

        [Test, Sequential]
        public void AssertInvalidUrls(
            [Values(
                "google.com",
                "www.google.com",
                "google",
                "http://google",
                "amqp://google.com")]
                string url)
        {
            UrlValidator urlValidator = new UrlValidator();
            Assert.AreEqual(false, urlValidator.IsValid(url), string.Format(CultureInfo.CurrentCulture, "The url '{0}' should be invalid", url));
        }

        [Test]
        public void IsValidShouldReturnFalseWhenTheSpecifiedValueIsNotString()
        {
            UrlValidator urlValidator = new UrlValidator();
            Assert.AreEqual(false, urlValidator.IsValid(100));
            Assert.AreEqual(false, urlValidator.IsValid(new object()));
        }

        [Test]
        public void IsValidShouldReturnTrueWhenTheSpecifiedValueIsNull()
        {
            UrlValidator urlValidator = new UrlValidator();
            Assert.AreEqual(true, urlValidator.IsValid(null));
        }
    }
}