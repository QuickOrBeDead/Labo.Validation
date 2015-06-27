namespace Labo.Validation.Tests.Builder
{
    using System;
    using System.Linq.Expressions;

    using Labo.Validation.Builder;
    using Labo.Validation.Validators;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class EntityValidationRuleBuilderFixture
    {
        public sealed class Customer
        {
            public string Name { get; set; }

            public string Surname { get; set; }
        }

        public sealed class CustomerValidator : ValidatorBase<Customer>
        {
        }

        [Test]
        public void AddValidator()
        {
            EntityValidationRuleBuilder<Customer, string> entityValidationRuleBuilder = new EntityValidationRuleBuilder<Customer, string>(new CustomerValidator(), x => x.Name);
            entityValidationRuleBuilder.AddValidator(new NotNullValidator());
            entityValidationRuleBuilder.AddValidator(new NotEmptyValidator());

            Assert.AreEqual(2, entityValidationRuleBuilder.Validators.Count);
            Assert.IsInstanceOf<NotNullValidator>(entityValidationRuleBuilder.Validators[0]);
            Assert.IsInstanceOf<NotEmptyValidator>(entityValidationRuleBuilder.Validators[1]);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void AddValidatorMustThrowArgumentNullExceptionWhenTheValidatorIsNull()
        {
            EntityValidationRuleBuilder<Customer, string> entityValidationRuleBuilder = new EntityValidationRuleBuilder<Customer, string>(new CustomerValidator(), x => x.Name);
            entityValidationRuleBuilder.AddValidator(null);
        }

        [Test]
        public void SetSpecification()
        {
            EntityValidationRuleBuilder<Customer, string> entityValidationRuleBuilder = new EntityValidationRuleBuilder<Customer, string>(new CustomerValidator(), x => x.Name);
            Expression<Func<Customer, bool>> expression = x => x.Name != null;
            entityValidationRuleBuilder.SetSpecification(new Specification<Customer>(expression));

            Assert.IsNotNull(entityValidationRuleBuilder.Specification);
            Assert.AreEqual(expression, entityValidationRuleBuilder.Specification.Predicate);
        }

        [Test]
        public void SetSpecificationMustNotThrowArgumentNullExceptionWhenTheSpecificationIsNull()
        {
            EntityValidationRuleBuilder<Customer, string> entityValidationRuleBuilder = new EntityValidationRuleBuilder<Customer, string>(new CustomerValidator(), x => x.Name);
            Assert.DoesNotThrow(() => entityValidationRuleBuilder.SetSpecification(null));
        }

        [Test]
        public void Build()
        {
            ValidatorBase<Customer> customerValidator = Substitute.For<ValidatorBase<Customer>>();

            EntityValidationRuleBuilder<Customer, string> entityValidationRuleBuilder = new EntityValidationRuleBuilder<Customer, string>(customerValidator, x => x.Name);
            entityValidationRuleBuilder.AddValidator(new NotNullValidator());
            entityValidationRuleBuilder.AddValidator(new NotEmptyValidator());

            Expression<Func<Customer, bool>> expression = x => x.Name != null;
            entityValidationRuleBuilder.SetSpecification(new Specification<Customer>(expression));

            entityValidationRuleBuilder.Build();

            Assert.AreEqual(2, customerValidator.EntityValidationRules.Count);

            EntityPropertyValidationRule<Customer, string> validationRule0 = (EntityPropertyValidationRule<Customer, string>)customerValidator.EntityValidationRules[0];
            EntityPropertyValidationRule<Customer, string> validationRule1 = (EntityPropertyValidationRule<Customer, string>)customerValidator.EntityValidationRules[1];

            Assert.IsInstanceOf<NotNullValidator>(validationRule0.Validator);
            Assert.IsInstanceOf<NotEmptyValidator>(validationRule1.Validator);

            Assert.AreEqual(expression, validationRule0.Specification.Predicate);
            Assert.AreEqual(expression, validationRule1.Specification.Predicate);

            Assert.AreSame(validationRule0.Specification, validationRule1.Specification);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorMustThrowArgumentNullExceptionWhenTheValidatorIsNull()
        {
            new EntityValidationRuleBuilder<Customer, string>(null, x => x.Name);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorMustThrowArgumentNullExceptionWhenTheExpressionIsNull()
        {
            new EntityValidationRuleBuilder<Customer, string>(new CustomerValidator(), null);
        }
    }
}
