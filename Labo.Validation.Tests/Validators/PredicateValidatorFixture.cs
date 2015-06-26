namespace Labo.Validation.Tests.Validators
{
    using System;

    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class PredicateValidatorFixture
    {
        [Test]
        public void IsValid()
        {
            AssertNotNullPredicate(null);
            AssertNotNullPredicate(1);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorMustThrowArgumentNullExceptionWhenPredicateIsNull()
        {
            new PredicateValidator(null);
        }

        private static void AssertNotNullPredicate(object value)
        {
            Predicate<object> predicate = x => x != null;
            PredicateValidator predicateValidator = new PredicateValidator(predicate);

            Assert.AreEqual(predicate(value), predicateValidator.IsValid(value));
        }
    }
}
