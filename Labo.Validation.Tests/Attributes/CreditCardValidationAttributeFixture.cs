namespace Labo.Validation.Tests.Attributes
{
    using Labo.Validation.Attributes;
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class CreditCardValidationAttributeFixture
    {
        [Test]
        public void GetValidator()
        {
            CreditCardValidationAttribute creditCardValidationAttribute = new CreditCardValidationAttribute();

            Assert.IsInstanceOf(typeof(CreditCardValidator), creditCardValidationAttribute.GetValidator());
        }
    }
}
