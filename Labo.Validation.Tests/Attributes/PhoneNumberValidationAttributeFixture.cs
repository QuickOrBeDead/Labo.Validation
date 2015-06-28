namespace Labo.Validation.Tests.Attributes
{
    using Labo.Validation.Attributes;
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class PhoneNumberValidationAttributeFixture
    {
        [Test]
        public void GetValidator()
        {
            PhoneNumberValidationAttribute phoneNumberValidationAttribute = new PhoneNumberValidationAttribute();

            Assert.IsInstanceOf(typeof(PhoneNumberValidator), phoneNumberValidationAttribute.GetValidator());
        }
    }
}