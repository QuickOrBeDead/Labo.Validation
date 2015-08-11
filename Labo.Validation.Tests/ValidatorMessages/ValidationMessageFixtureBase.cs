namespace Labo.Validation.Tests.ValidatorMessages
{
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public abstract class ValidationMessageFixtureBase
    {
        [Test]
        public void GetValidationMessage()
        {
            ValidatorBase validator = CreateValidator();
            string validationMessage = validator.GetValidationMessage("Name");
            Assert.AreEqual(GetExpectedValidationMessage(), validationMessage);
        }

        public abstract string GetExpectedValidationMessage();
        public abstract ValidatorBase CreateValidator();
    }
}