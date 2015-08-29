namespace Labo.Validation.Tests.Builder
{
    using System;
    using System.Collections.Generic;
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

        public sealed class CustomerValidator : EntityValidatorBase<Customer>
        {
        }

        [Test]
        public void AddValidator()
        {
            EntityValidationRuleBuilder<Customer, string> entityValidationRuleBuilder = new EntityValidationRuleBuilder<Customer, string>(new CustomerValidator(), Substitute.For<IPropertyDisplayNameResolver>(), x => x.Name);
            entityValidationRuleBuilder.AddValidator(new NotNullValidator());
            entityValidationRuleBuilder.AddValidator(new NotEmptyValidator());

            Assert.AreEqual(2, entityValidationRuleBuilder.Validators.Count);
            Assert.IsInstanceOf<NotNullValidator>(entityValidationRuleBuilder.Validators[0]);
            Assert.IsInstanceOf<NotEmptyValidator>(entityValidationRuleBuilder.Validators[1]);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void AddValidatorMustThrowArgumentNullExceptionWhenTheValidatorIsNull()
        {
            EntityValidationRuleBuilder<Customer, string> entityValidationRuleBuilder = new EntityValidationRuleBuilder<Customer, string>(new CustomerValidator(), Substitute.For<IPropertyDisplayNameResolver>(), x => x.Name);
            entityValidationRuleBuilder.AddValidator(null);
        }

        [Test]
        public void SetSpecification()
        {
            EntityValidationRuleBuilder<Customer, string> entityValidationRuleBuilder = new EntityValidationRuleBuilder<Customer, string>(new CustomerValidator(), Substitute.For<IPropertyDisplayNameResolver>(), x => x.Name);
            Expression<Func<Customer, bool>> expression = x => x.Name != null;
            entityValidationRuleBuilder.SetSpecification(new Specification<Customer>(expression));

            Assert.IsNotNull(entityValidationRuleBuilder.Specification);
            Assert.AreEqual(expression, entityValidationRuleBuilder.Specification.Predicate);
        }

        [Test]
        public void SetSpecificationMustNotThrowArgumentNullExceptionWhenTheSpecificationIsNull()
        {
            EntityValidationRuleBuilder<Customer, string> entityValidationRuleBuilder = new EntityValidationRuleBuilder<Customer, string>(new CustomerValidator(), Substitute.For<IPropertyDisplayNameResolver>(), x => x.Name);
            Assert.DoesNotThrow(() => entityValidationRuleBuilder.SetSpecification(null));
        }

        [Test]
        public void Build()
        {
            EntityValidatorBase<Customer> customerValidator = Substitute.For<EntityValidatorBase<Customer>>();

            EntityValidationRuleBuilder<Customer, string> entityValidationRuleBuilder = new EntityValidationRuleBuilder<Customer, string>(customerValidator, Substitute.For<IPropertyDisplayNameResolver>(), x => x.Name);
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

        [Test]
        public void BuildShouldAddValidatorsToTheRuleSetWhenRuleSetIsGiven()
        {
            EntityValidatorBase<Customer> customerValidator = Substitute.For<EntityValidatorBase<Customer>>();

            const string ruleSetName = "TestRuleSet";
            EntityValidationRuleBuilder<Customer, string> entityValidationRuleBuilder = new EntityValidationRuleBuilder<Customer, string>(customerValidator, Substitute.For<IPropertyDisplayNameResolver>(), x => x.Name, ruleSetName);
            entityValidationRuleBuilder.AddValidator(new NotNullValidator());
            entityValidationRuleBuilder.AddValidator(new NotEmptyValidator());

            Assert.AreEqual(ruleSetName, entityValidationRuleBuilder.RuleSetName);

            entityValidationRuleBuilder.Build();

            Assert.AreEqual(1, customerValidator.AllEntityValidationRules.Count);

            IList<IEntityValidationRule<Customer>> entityValidationRules = customerValidator.AllEntityValidationRules[ruleSetName];
            EntityPropertyValidationRule<Customer, string> validationRule0 = (EntityPropertyValidationRule<Customer, string>)entityValidationRules[0];
            EntityPropertyValidationRule<Customer, string> validationRule1 = (EntityPropertyValidationRule<Customer, string>)entityValidationRules[1];

            Assert.IsInstanceOf<NotNullValidator>(validationRule0.Validator);
            Assert.IsInstanceOf<NotEmptyValidator>(validationRule1.Validator);
        }

        [Test]
        public void SetMessage()
        {
            EntityValidatorBase<Customer> customerValidator = Substitute.For<EntityValidatorBase<Customer>>();

            EntityValidationRuleBuilder<Customer, string> entityValidationRuleBuilder = new EntityValidationRuleBuilder<Customer, string>(customerValidator, Substitute.For<IPropertyDisplayNameResolver>(), x => x.Name);
            const string message = "Costumer name cannot be empty.";
            entityValidationRuleBuilder.SetMessage(message);

            Assert.AreEqual(message, entityValidationRuleBuilder.Message);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorMustThrowArgumentNullExceptionWhenTheValidatorIsNull()
        {
            new EntityValidationRuleBuilder<Customer, string>(null, Substitute.For<IPropertyDisplayNameResolver>(), x => x.Name);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorMustThrowArgumentNullExceptionWhenTheExpressionIsNull()
        {
            new EntityValidationRuleBuilder<Customer, string>(new CustomerValidator(), Substitute.For<IPropertyDisplayNameResolver>(), null);
        }
    }
}
