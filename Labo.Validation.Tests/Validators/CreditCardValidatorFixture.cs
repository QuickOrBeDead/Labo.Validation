namespace Labo.Validation.Tests.Validators
{
    using System;

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
    }
}
