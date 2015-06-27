namespace Labo.Validation.Tests.Attributes
{
    using System;

    using Labo.Validation.Attributes;
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class PredicateValidationAttributeFixture
    {
        [Test]
        public void GetValidator()
        {
            Predicate<object> predicate = x => true;
            PredicateValidationAttribute predicateValidationAttribute = new PredicateValidationAttribute(predicate);

            Assert.IsInstanceOf(typeof(PredicateValidator), predicateValidationAttribute.GetValidator());

            PredicateValidator predicateValidator = (PredicateValidator)predicateValidationAttribute.GetValidator();
            Assert.AreEqual(predicate, predicateValidator.Predicate);
        }
    }
}
