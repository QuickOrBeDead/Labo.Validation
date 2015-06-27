namespace Labo.Validation.Tests.Attributes
{
    using Labo.Validation.Attributes;
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class GreaterThanOrEqualToValidationAttributeFixture
    {
        [Test]
        public void GetValidator()
        {
            const int valueToCompare = 10;
            GreaterThanOrEqualToValidationAttribute greaterThanOrEqualToValidationAttribute = new GreaterThanOrEqualToValidationAttribute(valueToCompare);

            Assert.IsInstanceOf(typeof(GreaterThanOrEqualToValidator), greaterThanOrEqualToValidationAttribute.GetValidator());

            GreaterThanOrEqualToValidator greaterThanOrEqualToValidator = (GreaterThanOrEqualToValidator)greaterThanOrEqualToValidationAttribute.GetValidator();
            Assert.AreEqual(valueToCompare, greaterThanOrEqualToValidator.ValueToCompare);
        }
    }
}
