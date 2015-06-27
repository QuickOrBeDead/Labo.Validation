namespace Labo.Validation.Tests.Attributes
{
    using Labo.Validation.Attributes;
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class LessThanValidationAttributeFixture
    {
        [Test]
        public void GetValidator()
        {
            const int valueToCompare = 10;
            LessThanValidationAttribute lessThanValidationAttribute = new LessThanValidationAttribute(valueToCompare);

            Assert.IsInstanceOf(typeof(LessThanValidator), lessThanValidationAttribute.GetValidator());

            LessThanValidator lessThanValidator = (LessThanValidator)lessThanValidationAttribute.GetValidator();
            Assert.AreEqual(valueToCompare, lessThanValidator.ValueToCompare);
        }
    }
}
