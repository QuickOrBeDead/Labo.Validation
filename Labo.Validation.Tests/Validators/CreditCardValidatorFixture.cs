namespace Labo.Validation.Tests.Validators
{
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class CreditCardValidatorFixture
    {
        [Test]
        public void IsValidMustReturnTrueWhenTheSpeficiedValueIsNull()
        {
            CreditCardValidator creditCardValidator = new CreditCardValidator();

            Assert.IsTrue(creditCardValidator.IsValid(null));
        }

        [Test, Sequential]
        public void IsValid(
            [Values(null, "0000000000000000", "1234567890123452", "1234-5678-9012-3452", "1234 5678 9012 3452", "0000000000000001", "000000000000000A")]
            string value,
            [Values(true, true, true, true, true, false, false)]
            bool expectedResult)
        {
            CreditCardValidator creditCardValidator = new CreditCardValidator();
            Assert.AreEqual(expectedResult, creditCardValidator.IsValid(value));
        }
    }
}
