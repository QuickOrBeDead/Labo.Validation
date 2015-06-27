namespace Labo.Validation.Tests.Attributes
{
    using Labo.Validation.Attributes;
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class LessThanOrEqualToValidationAttributeFixture
    {
        [Test]
        public void GetValidator()
        {
            const int valueToCompare = 10;
            LessThanOrEqualToValidationAttribute lessThanOrEqualToValidationAttribute = new LessThanOrEqualToValidationAttribute(valueToCompare);

            Assert.IsInstanceOf(typeof(LessThanOrEqualToValidator), lessThanOrEqualToValidationAttribute.GetValidator());

            LessThanOrEqualToValidator lessThanOrEqualToValidator = (LessThanOrEqualToValidator)lessThanOrEqualToValidationAttribute.GetValidator();
            Assert.AreEqual(valueToCompare, lessThanOrEqualToValidator.ValueToCompare);
        }
    }
}
