namespace Labo.Validation.Tests.Attributes
{
    using Labo.Validation.Attributes;
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class BetweenValidationAttributeFixture
    {
        [Test]
        public void GetValidator()
        {
            int @from = 1;
            int to = 2;
            BetweenValidationAttribute betweenValidationAttribute = new BetweenValidationAttribute(@from, to);

            Assert.IsInstanceOf(typeof(BetweenValidator), betweenValidationAttribute.GetValidator());

            BetweenValidator betweenValidator = (BetweenValidator)betweenValidationAttribute.GetValidator();
            Assert.AreEqual(@from, betweenValidator.FromValue);
            Assert.AreEqual(@to, betweenValidator.ToValue);
        }
    }
}
