namespace Labo.Validation.Tests.Attributes
{
    using Labo.Validation.Attributes;
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class NotNullValidationAttributeFixture
    {
        [Test]
        public void GetValidator()
        {
            NotNullValidationAttribute notNullValidationAttribute = new NotNullValidationAttribute();

            Assert.IsInstanceOf(typeof(NotNullValidator), notNullValidationAttribute.GetValidator());
        }
    }
}
