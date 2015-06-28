namespace Labo.Validation.Tests.Attributes
{
    using Labo.Validation.Attributes;
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class UrlValidationAttributeFixture
    {
        [Test]
        public void GetValidator()
        {
            UrlValidationAttribute urlValidationAttribute = new UrlValidationAttribute();

            Assert.IsInstanceOf(typeof(UrlValidator), urlValidationAttribute.GetValidator());
        }
    }
}