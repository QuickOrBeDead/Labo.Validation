namespace Labo.Validation.Tests.Attributes
{
    using System;
    using System.Collections;

    using Labo.Validation.Attributes;
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class NotEqualToValidationAttributeFixture
    {
        [Test]
        public void GetValidator()
        {
            const string valueToCompare = "TEST";
            IEqualityComparer comparer = StringComparer.OrdinalIgnoreCase;
            NotEqualToValidationAttribute notEqualToValidationAttribute = new NotEqualToValidationAttribute(valueToCompare, comparer);

            Assert.IsInstanceOf(typeof(NotEqualToValidator), notEqualToValidationAttribute.GetValidator());

            NotEqualToValidator notEqualToValidator = (NotEqualToValidator)notEqualToValidationAttribute.GetValidator();
            Assert.AreEqual(valueToCompare, notEqualToValidator.ValueToCompare);
            Assert.AreEqual(comparer, notEqualToValidator.Comparer);
        }
    }
}
