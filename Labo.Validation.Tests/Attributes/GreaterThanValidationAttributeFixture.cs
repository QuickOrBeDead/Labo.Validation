namespace Labo.Validation.Tests.Attributes
{
    using Labo.Validation.Attributes;
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class GreaterThanValidationAttributeFixture
    {
        [Test]
        public void GetValidator()
        {
            const int valueToCompare = 10;
            GreaterThanValidationAttribute greaterThanValidationAttribute = new GreaterThanValidationAttribute(valueToCompare);

            Assert.IsInstanceOf(typeof(GreaterThanValidator), greaterThanValidationAttribute.GetValidator());

            GreaterThanValidator greaterThanValidator = (GreaterThanValidator)greaterThanValidationAttribute.GetValidator();
            Assert.AreEqual(valueToCompare, greaterThanValidator.ValueToCompare);
        }
    }
}
