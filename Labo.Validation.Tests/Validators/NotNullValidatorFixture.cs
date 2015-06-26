namespace Labo.Validation.Tests.Validators
{
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class NotNullValidatorFixture
    {
        [Test]
        public void IsValidNullValue()
        {
            NotNullValidator notNullValidator = new NotNullValidator();
            Assert.IsFalse(notNullValidator.IsValid(null));

            object value = null;
            Assert.IsFalse(notNullValidator.IsValid(value));

            int? id = null;
            Assert.IsFalse(notNullValidator.IsValid(id));
        }

        [Test]
        public void IsValidNotNullValue()
        {
            NotNullValidator notNullValidator = new NotNullValidator();
            Assert.IsTrue(notNullValidator.IsValid(new object()));

            const int value = 0;
            Assert.IsTrue(notNullValidator.IsValid(value));
        }
    }
}
