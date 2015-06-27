namespace Labo.Validation.Tests.Attributes
{
    using Labo.Validation.Attributes;
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class LengthValidationAttributeFixture
    {
        [Test]
        public void GetValidator()
        {
            const int min = 10;
            const int max = 20;
            LengthValidationAttribute lengthValidationAttribute = new LengthValidationAttribute(min, max);

            Assert.IsInstanceOf(typeof(LengthValidator), lengthValidationAttribute.GetValidator());

            LengthValidator lenghtValidator = (LengthValidator)lengthValidationAttribute.GetValidator();
            Assert.AreEqual(min, lenghtValidator.Min);
            Assert.AreEqual(max, lenghtValidator.Max);
        }
    }
}
