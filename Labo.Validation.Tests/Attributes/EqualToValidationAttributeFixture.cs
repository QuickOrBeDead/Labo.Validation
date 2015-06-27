namespace Labo.Validation.Tests.Attributes
{
    using System;
    using System.Collections;

    using Labo.Validation.Attributes;
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class EqualToValidationAttributeFixture
    {
        [Test]
        public void GetValidator()
        {
            const string valueToCompare = "TEST";
            IEqualityComparer comparer = StringComparer.OrdinalIgnoreCase;
            EqualToValidationAttribute equalToValidationAttribute = new EqualToValidationAttribute(valueToCompare, comparer);

            Assert.IsInstanceOf(typeof(EqualToValidator), equalToValidationAttribute.GetValidator());

            EqualToValidator equalToValidator = (EqualToValidator)equalToValidationAttribute.GetValidator();
            Assert.AreEqual(valueToCompare, equalToValidator.ValueToCompare);
            Assert.AreEqual(comparer, equalToValidator.Comparer);
        }
    }
}
