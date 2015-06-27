namespace Labo.Validation.Tests
{
    using System;
    using System.Linq.Expressions;

    using NUnit.Framework;

    [TestFixture]
    public class SpecificationFixture
    {
        public sealed class Customer
        {
            public string FirstName { get; set; }
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorMustThrowArgumentNullExceptionWhenPredicateIsNull()
        {
            new Specification<Customer>(null);
        }

        [Test]
        public void Predicate()
        {
            Expression<Func<Customer, bool>> expression = x => x.FirstName == "Foo";
            Specification<Customer> specification = new Specification<Customer>(expression);
            Assert.AreSame(expression, specification.Predicate);
        }

        [Test]
        public void IsSatisfiedByMustReturnTrueWhenThePredicateReturnsTrue()
        {
            Specification<Customer> specification = new Specification<Customer>(x => x.FirstName == "Foo");
            Assert.IsTrue(specification.IsSatisfiedBy(new Customer { FirstName = "Foo" }));
            Assert.IsTrue(specification.IsSatisfiedBy((object)new Customer { FirstName = "Foo" }));
        }

        [Test]
        public void IsSatisfiedByMustReturnFalseWhenThePredicateReturnsFalse()
        {
            Specification<Customer> specification = new Specification<Customer>(x => x.FirstName != null);
            Assert.IsFalse(specification.IsSatisfiedBy(new Customer()));
            Assert.IsFalse(specification.IsSatisfiedBy((object)new Customer()));
        }
    }
}
