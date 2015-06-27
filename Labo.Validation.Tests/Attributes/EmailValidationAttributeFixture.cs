namespace Labo.Validation.Tests.Attributes
{
    using Labo.Validation.Attributes;
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class EmailValidationAttributeFixture
    {
        [Test]
        public void GetValidator()
        {
            EmailValidationAttribute emailValidationAttribute = new EmailValidationAttribute();

            Assert.IsInstanceOf(typeof(EmailValidator), emailValidationAttribute.GetValidator());
        }
    }
}
