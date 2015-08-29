namespace Labo.Validation.Tests.Builder
{
    using System.Collections.Generic;

    using Labo.Validation;
    using Labo.Validation.Builder;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class EntityValidationRuleSetBuilderFixture
    {
        public class Customer
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }
        }

        [Test]
        public void AddRuleShouldCallEntityValidationRuleSetRulesetName()
        {
            EntityValidatorBase<Customer> entityValidator = Substitute.For<EntityValidatorBase<Customer>>();
            const string ruleSetName = "TestRuleSet";
            EntityValidationRuleSetBuilder<Customer> ruleSetBuilder = new EntityValidationRuleSetBuilder<Customer>(Substitute.For<IPropertyDisplayNameResolver>(), entityValidator, ruleSetName);

            IEntityValidationRule<Customer> entityValidationRule = Substitute.For<IEntityValidationRule<Customer>>();
            ruleSetBuilder.AddRule(entityValidationRule);

            entityValidator.Received(1).AddRule(ruleSetName, entityValidationRule);
        }

        [Test]
        public void RuleForShouldCallEntityValidationRuleSetRulesetName()
        {
            EntityValidatorBase<Customer> entityValidator = Substitute.For<EntityValidatorBase<Customer>>();
            const string ruleSetName = "TestRuleSet";
            EntityValidationRuleSetBuilder<Customer> ruleSetBuilder = new EntityValidationRuleSetBuilder<Customer>(Substitute.For<IPropertyDisplayNameResolver>(), entityValidator, ruleSetName);

            EntityValidationRuleBuilder<Customer, string> entityValidationRuleBuilder = (EntityValidationRuleBuilder<Customer, string>) ruleSetBuilder.RuleFor(x => x.FirstName);

            Assert.AreEqual(ruleSetName, entityValidationRuleBuilder.RuleSetName);
        }

        [Test]
        public void AddRuleShouldAddToRuleSetCollectionOfTheValidator()
        {
            EntityValidatorBase<Customer> entityValidator = Substitute.ForPartsOf<EntityValidatorBase<Customer>>();
            const string ruleSetName = "TestRuleSet";
            EntityValidationRuleSetBuilder<Customer> ruleSetBuilder = new EntityValidationRuleSetBuilder<Customer>(Substitute.For<IPropertyDisplayNameResolver>(), entityValidator, ruleSetName);
            ruleSetBuilder.AddRule(x => x.RuleFor(y => y.FirstName).NotNull());

            IList<IEntityValidationRule<Customer>> validationRules = entityValidator.AllEntityValidationRules[ruleSetName];
            
            Assert.IsNotNull(validationRules);
            Assert.AreEqual(1, validationRules.Count);
            Assert.AreEqual("FirstName", validationRules[0].MemberName);
        }
    }
}
