namespace Labo.Validation.Tests.Attributes
{
    using Labo.Validation.Attributes;
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class NotEmptyValidationAttributeFixture
    {
        [Test]
        public void GetValidator()
        {
            NotEmptyValidationAttribute notEmptyValidationAttribute = new NotEmptyValidationAttribute();

            Assert.IsInstanceOf(typeof(NotEmptyValidator), notEmptyValidationAttribute.GetValidator());
        }
    }
}
