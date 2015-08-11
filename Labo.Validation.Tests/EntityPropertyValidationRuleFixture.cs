namespace Labo.Validation.Tests
{
    using System;

    using Labo.Common.Utils;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class EntityPropertyValidationRuleFixture
    {
        public sealed class Customer
        {
            public string FirstName { get; set; }
        }

        [Test]
        public void MemberInfo()
        {
            IValidator validator = Substitute.For<IValidator>();
            EntityPropertyValidationRule<Customer, string> entityPropertyValidationRule = new EntityPropertyValidationRule<Customer, string>(validator, Substitute.For<IPropertyDisplayNameResolver>(), x => x.FirstName);

            Assert.AreEqual(LinqUtils.GetMemberName<Customer, string>(x => x.FirstName), entityPropertyValidationRule.MemberInfo.Name);
        }

        [Test]
        public void MemberName()
        {
            IValidator validator = Substitute.For<IValidator>();
            EntityPropertyValidationRule<Customer, string> entityPropertyValidationRule = new EntityPropertyValidationRule<Customer, string>(validator, Substitute.For<IPropertyDisplayNameResolver>(), x => x.FirstName);

            Assert.AreEqual(LinqUtils.GetMemberName<Customer, string>(x => x.FirstName), entityPropertyValidationRule.MemberName);
        }

        [Test]
        public void Validator()
        {
            IValidator validator = Substitute.For<IValidator>();
            EntityPropertyValidationRule<Customer, string> entityPropertyValidationRule = new EntityPropertyValidationRule<Customer, string>(validator, Substitute.For<IPropertyDisplayNameResolver>(), x => x.FirstName);

            Assert.AreSame(validator, entityPropertyValidationRule.Validator);
        }

        [Test]
        public void Specification()
        {
            IValidator validator = Substitute.For<IValidator>();
            ISpecification<Customer> specification = Substitute.For<ISpecification<Customer>>();
            EntityPropertyValidationRule<Customer, string> entityPropertyValidationRule = new EntityPropertyValidationRule<Customer, string>(validator, Substitute.For<IPropertyDisplayNameResolver>(), x => x.FirstName, specification);

            Assert.AreSame(specification, entityPropertyValidationRule.Specification);
        }

        [Test]
        public void Message()
        {
            IValidator validator = Substitute.For<IValidator>();
            EntityPropertyValidationRule<Customer, string> entityPropertyValidationRule = new EntityPropertyValidationRule<Customer, string>(validator, Substitute.For<IPropertyDisplayNameResolver>(), x => x.FirstName, null, "Test Message");

            Assert.AreEqual("Test Message", entityPropertyValidationRule.Message);
        }

        [Test]
        public void ValidateEnsureValidatorIsValidIsCalledForTheEntityProperty()
        {
            const string firstName = "Foo";

            IValidator validator = Substitute.For<IValidator>();
            EntityPropertyValidationRule<Customer, string> entityPropertyValidationRule = new EntityPropertyValidationRule<Customer, string>(validator, Substitute.For<IPropertyDisplayNameResolver>(), x => x.FirstName);
            entityPropertyValidationRule.Validate(new Customer
                                                      {
                                                          FirstName = firstName
                                                      });

            validator.Received(1).IsValid(firstName);
        }

        [Test]
        public void ValidateMustReturnValidWhenValidatorIsValidReturnsTrue()
        {
            const string firstName = "Foo";

            IValidator validator = Substitute.For<IValidator>();
            validator.IsValid(firstName).Returns(true);

            EntityPropertyValidationRule<Customer, string> entityPropertyValidationRule = new EntityPropertyValidationRule<Customer, string>(validator, Substitute.For<IPropertyDisplayNameResolver>(), x => x.FirstName);
            ValidationResult validationResult = entityPropertyValidationRule.Validate(new Customer
                                                                                          {
                                                                                              FirstName = firstName
                                                                                          });

            validator.Received(1).IsValid(firstName);

            Assert.AreEqual(0, validationResult.Errors.Count);
            Assert.IsTrue(validationResult.IsValid);
        }

        [Test]
        public void ValidateMustReturnEmptyValidationResultWhenTheSpecifiedSpecificationIsNotSatisfied()
        {
            const string firstName = "Foo";

            IValidator validator = Substitute.For<IValidator>();
            ISpecification<Customer> specification = Substitute.For<ISpecification<Customer>>();
            specification.IsSatisfiedBy(null).ReturnsForAnyArgs(false);

            EntityPropertyValidationRule<Customer, string> entityPropertyValidationRule = new EntityPropertyValidationRule<Customer, string>(validator, Substitute.For<IPropertyDisplayNameResolver>(), x => x.FirstName, specification);
            ValidationResult validationResult = entityPropertyValidationRule.Validate(new Customer
            {
                FirstName = firstName
            });

            validator.DidNotReceiveWithAnyArgs().IsValid(firstName);
            specification.ReceivedWithAnyArgs(1).IsSatisfiedBy(null);

            Assert.AreEqual(0, validationResult.Errors.Count);
            Assert.IsTrue(validationResult.IsValid);
        }

        [Test]
        public void ValidateMustReturnEmptyValidationResultWhenTheSpecifiedSpecificationIsSatisfiedAndTheValidatorReturnsTrue()
        {
            const string firstName = "Foo";

            IValidator validator = Substitute.For<IValidator>();
            validator.IsValid(null).ReturnsForAnyArgs(true);

            ISpecification<Customer> specification = Substitute.For<ISpecification<Customer>>();
            specification.IsSatisfiedBy(null).ReturnsForAnyArgs(true);

            EntityPropertyValidationRule<Customer, string> entityPropertyValidationRule = new EntityPropertyValidationRule<Customer, string>(validator, Substitute.For<IPropertyDisplayNameResolver>(), x => x.FirstName, specification);
            ValidationResult validationResult = entityPropertyValidationRule.Validate(new Customer
            {
                FirstName = firstName
            });

            validator.Received(1).IsValid(firstName);
            specification.ReceivedWithAnyArgs(1).IsSatisfiedBy(null);

            Assert.AreEqual(0, validationResult.Errors.Count);
            Assert.IsTrue(validationResult.IsValid);
        }

        [Test]
        public void ValidateMustReturnInvalidValidationResultWhenTheSpecifiedSpecificationIsSatisfiedAndTheValidatorReturnsFalse()
        {
            const string firstName = "Foo";

            IValidator validator = Substitute.For<IValidator>();
            validator.IsValid(null).ReturnsForAnyArgs(false);

            ISpecification<Customer> specification = Substitute.For<ISpecification<Customer>>();
            specification.IsSatisfiedBy(null).ReturnsForAnyArgs(true);

            EntityPropertyValidationRule<Customer, string> entityPropertyValidationRule = new EntityPropertyValidationRule<Customer, string>(validator, Substitute.For<IPropertyDisplayNameResolver>(), x => x.FirstName, specification);
            ValidationResult validationResult = entityPropertyValidationRule.Validate(new Customer
            {
                FirstName = firstName
            });

            validator.Received(1).IsValid(firstName);
            specification.ReceivedWithAnyArgs(1).IsSatisfiedBy(null);

            Assert.AreEqual(1, validationResult.Errors.Count);
            Assert.AreEqual(LinqUtils.GetMemberName<Customer, string>(x => x.FirstName), validationResult.Errors[0].PropertyName);
            Assert.IsFalse(validationResult.IsValid);
        }

        [Test]
        public void ValidateMustReturnInvalidValidationResultWhenTheSpecifiedTheValidatorReturnsFalse()
        {
            const string firstName = "Foo";

            IValidator validator = Substitute.For<IValidator>();
            validator.IsValid(null).ReturnsForAnyArgs(false);

            EntityPropertyValidationRule<Customer, string> entityPropertyValidationRule = new EntityPropertyValidationRule<Customer, string>(validator, Substitute.For<IPropertyDisplayNameResolver>(), x => x.FirstName);
            ValidationResult validationResult = entityPropertyValidationRule.Validate(new Customer
            {
                FirstName = firstName
            });

            validator.Received(1).IsValid(firstName);

            Assert.AreEqual(1, validationResult.Errors.Count);
            Assert.AreEqual(LinqUtils.GetMemberName<Customer, string>(x => x.FirstName), validationResult.Errors[0].PropertyName);
            Assert.IsFalse(validationResult.IsValid);
        }

        [Test]
        public void ValidateByObject()
        {
            const string firstName = "Foo";

            IValidator validator = Substitute.For<IValidator>();
            validator.IsValid(null).ReturnsForAnyArgs(false);

            EntityPropertyValidationRule<Customer, string> entityPropertyValidationRule = new EntityPropertyValidationRule<Customer, string>(validator, Substitute.For<IPropertyDisplayNameResolver>(), x => x.FirstName);
            ValidationResult validationResult = ((IEntityValidationRule)entityPropertyValidationRule).Validate(new Customer
            {
                FirstName = firstName
            });

            validator.Received(1).IsValid(firstName);

            Assert.IsFalse(validationResult.IsValid);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorMustThrowArgumentNullExceptionWhenValidatorIsNull()
        {
            new EntityPropertyValidationRule<Customer, string>(null, Substitute.For<IPropertyDisplayNameResolver>(), x => x.FirstName);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorMustThrowArgumentNullExceptionWhenExpressionIsNull()
        {
            new EntityPropertyValidationRule<Customer, string>(Substitute.For<IValidator>(), Substitute.For<IPropertyDisplayNameResolver>(), null);
        }
    }
}
