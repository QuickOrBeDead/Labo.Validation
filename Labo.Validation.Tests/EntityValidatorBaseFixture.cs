namespace Labo.Validation.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using Labo.Validation;
    using Labo.Validation.Exceptions;
    using Labo.Validation.Validators;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class EntityValidatorBaseFixture
    {
        public class Customer
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }
        }

        [Test]
        public void ValidationRules()
        {
            EntityValidatorBase<string> entityValidatorBase = Substitute.ForPartsOf<EntityValidatorBase<string>>();
            IEntityValidationRule<string> entityValidationRule = Substitute.For<IEntityValidationRule<string>>();
            entityValidatorBase.AddRule(entityValidationRule);

            Assert.AreSame(entityValidationRule, entityValidatorBase.ValidationRules.Single());
        }

        [Test]
        public void AddRuleWithRuleSetName()
        {
            EntityValidatorBase<string> entityValidatorBase = Substitute.ForPartsOf<EntityValidatorBase<string>>();
            IEntityValidationRule<string> entityValidationRule = Substitute.For<IEntityValidationRule<string>>();
            entityValidatorBase.AddRule("Test", entityValidationRule);
            IList<IEntityValidationRule<string>> validationRules = entityValidatorBase.GetValidationRulesByRuleSetName("Test");

            Assert.AreEqual(1, validationRules.Count);
            Assert.AreSame(entityValidationRule, validationRules[0]);
        }

        [Test]
        public void AddRuleWithRuleWithStringEmptyRuleSetName()
        {
            EntityValidatorBase<string> entityValidatorBase = Substitute.ForPartsOf<EntityValidatorBase<string>>();
            IEntityValidationRule<string> entityValidationRule = Substitute.For<IEntityValidationRule<string>>();
            entityValidatorBase.AddRule(string.Empty, entityValidationRule);
            IList<IEntityValidationRule<string>> validationRules = entityValidatorBase.GetValidationRulesByRuleSetName(string.Empty);

            Assert.AreEqual(1, validationRules.Count);
            Assert.AreSame(entityValidationRule, validationRules[0]);
        }

        [Test]
        public void AddRule()
        {
            EntityValidatorBase<string> entityValidatorBase = Substitute.ForPartsOf<EntityValidatorBase<string>>();
            IEntityValidationRule<string> entityValidationRule = Substitute.For<IEntityValidationRule<string>>();
            entityValidatorBase.AddRule(entityValidationRule);
            IList<IEntityValidationRule<string>> validationRules = entityValidatorBase.GetValidationRulesByRuleSetName();

            Assert.AreEqual(1, validationRules.Count);
        }

        [Test]
        public void GetValidationRulesByRuleSetNameShouldReturnEmptyValidationRuleListWhenNoRuleSetIsFound()
        {
            EntityValidatorBase<string> entityValidatorBase = Substitute.ForPartsOf<EntityValidatorBase<string>>();
            IEntityValidationRule<string> entityValidationRule = Substitute.For<IEntityValidationRule<string>>();
            entityValidatorBase.AddRule("Test", entityValidationRule);
            IList<IEntityValidationRule<string>> validationRules = entityValidatorBase.GetValidationRulesByRuleSetName(string.Empty);

            Assert.IsNotNull(validationRules);
            Assert.AreEqual(0, validationRules.Count);
        }

        [Test]
        public void AddRuleSet()
        {
            EntityValidatorBase<Customer> entityValidatorBase = Substitute.ForPartsOf<EntityValidatorBase<Customer>>();
            entityValidatorBase.AddRuleSet("Test", x =>
            {
                x.AddRule(y => y.RuleFor(z => z.FirstName).NotNull());
                x.AddRule(y => y.RuleFor(z => z.LastName).NotEmpty());
            });

            SortedList<string, IList<IEntityValidationRule<Customer>>> validationRules = entityValidatorBase.AllEntityValidationRules;

            Assert.IsTrue(validationRules.ContainsKey("Test"));

            IList<IEntityValidationRule<Customer>> entityValidationRules = validationRules["Test"];

            Assert.IsNotNull(entityValidationRules);
            Assert.AreEqual(2, entityValidationRules.Count);
          
            Assert.AreEqual("FirstName", entityValidationRules[0].MemberName);
            Assert.IsInstanceOf<NotNullValidator>(((EntityPropertyValidator)entityValidationRules[0].Validator).InnerValidator);
            
            Assert.AreEqual("LastName", entityValidationRules[1].MemberName);
            Assert.IsInstanceOf<NotEmptyValidator>(((EntityPropertyValidator)entityValidationRules[1].Validator).InnerValidator);
        }

        [Test]
        public void AddRuleSetAndRuleShouldAddTheRuleToTheDefaultRuleSet()
        {
            EntityValidatorBase<Customer> entityValidatorBase = Substitute.ForPartsOf<EntityValidatorBase<Customer>>();
            entityValidatorBase.AddRule(x => x.RuleFor(y => y.FirstName).NotEmpty());
            entityValidatorBase.AddRuleSet("Test", x =>
            {
                x.AddRule(y => y.RuleFor(z => z.FirstName).NotNull());
                x.AddRule(y => y.RuleFor(z => z.LastName).NotEmpty());
            });

            SortedList<string, IList<IEntityValidationRule<Customer>>> validationRules = entityValidatorBase.AllEntityValidationRules;

            Assert.IsTrue(validationRules.ContainsKey("Test"));
            Assert.IsTrue(validationRules.ContainsKey(string.Empty));

            IList<IEntityValidationRule<Customer>> entityValidationRules = validationRules["Test"];

            Assert.IsNotNull(entityValidationRules);
            Assert.AreEqual(2, entityValidationRules.Count);

            Assert.AreEqual("FirstName", entityValidationRules[0].MemberName);
            Assert.IsInstanceOf<NotNullValidator>(((EntityPropertyValidator)entityValidationRules[0].Validator).InnerValidator);

            Assert.AreEqual("LastName", entityValidationRules[1].MemberName);
            Assert.IsInstanceOf<NotEmptyValidator>(((EntityPropertyValidator)entityValidationRules[1].Validator).InnerValidator);

            entityValidationRules = validationRules[string.Empty];
            
            Assert.IsNotNull(entityValidationRules);
            Assert.AreEqual(1, entityValidationRules.Count);

            Assert.AreEqual("FirstName", entityValidationRules[0].MemberName);
            Assert.IsInstanceOf<NotEmptyValidator>(((EntityPropertyValidator)entityValidationRules[0].Validator).InnerValidator);
        }

        [Test]
        public void ValidateAndThrowExceptionShouldThrowExceptionWhenInvalid()
        {
            EntityValidatorBase<Customer> entityValidatorBase = Substitute.ForPartsOf<EntityValidatorBase<Customer>>();
            entityValidatorBase.AddRule(x => x.RuleFor(y => y.FirstName).NotEmpty());

            Assert.Throws<ValidationException>(() => entityValidatorBase.ValidateAndThrowException(new Customer
            {
                FirstName = string.Empty
            }));
        }

        [Test]
        public void ValidateAndThrowExceptionShouldNotThrowExceptionWhenValid()
        {
            EntityValidatorBase<Customer> entityValidatorBase = Substitute.ForPartsOf<EntityValidatorBase<Customer>>();
            entityValidatorBase.AddRule(x => x.RuleFor(y => y.FirstName).NotEmpty());

            Assert.DoesNotThrow(() => entityValidatorBase.ValidateAndThrowException(new Customer
            {
                FirstName = "Test"
            }));
        }

        [Test]
        public void ValidateAndThrowExceptionShouldRunTheCorrectRuleSet()
        {
            EntityValidatorBase<Customer> entityValidatorBase = Substitute.ForPartsOf<EntityValidatorBase<Customer>>();
            entityValidatorBase.AddRule(x => x.RuleFor(y => y.FirstName).NotNull());
            entityValidatorBase.AddRuleSet("Test", x =>
            {
                x.AddRule(y => y.RuleFor(z => z.FirstName).NotEmpty());
                x.AddRule(y => y.RuleFor(z => z.LastName).NotEmpty());
            });

            Assert.DoesNotThrow(() => entityValidatorBase.ValidateAndThrowException(new Customer
            {
                FirstName = string.Empty
            }));

            Assert.Throws<ValidationException>(() => entityValidatorBase.ValidateAndThrowException(new Customer
            {
                FirstName = null
            }));

            Assert.DoesNotThrow(() => entityValidatorBase.ValidateAndThrowException(new Customer
            {
                FirstName = "Test",
                LastName = "Test"
            }, "Test"));

            Assert.Throws<ValidationException>(() => entityValidatorBase.ValidateAndThrowException(new Customer
            {
                FirstName = string.Empty,
                LastName = "Test"
            }, "Test"));
        }

        [Test]
        public void ValidateExceptionShouldRunTheCorrectRuleSet()
        {
            EntityValidatorBase<Customer> entityValidatorBase = Substitute.ForPartsOf<EntityValidatorBase<Customer>>();
            entityValidatorBase.AddRule(x => x.RuleFor(y => y.FirstName).NotNull());
            entityValidatorBase.AddRuleSet("Test", x =>
            {
                x.AddRule(y => y.RuleFor(z => z.FirstName).NotEmpty());
                x.AddRule(y => y.RuleFor(z => z.LastName).NotEmpty());
            });

            ValidationResult validationResult = entityValidatorBase.Validate(new Customer
            {
                FirstName = string.Empty
            });

            Assert.IsTrue(validationResult.IsValid);

            validationResult = entityValidatorBase.Validate(new Customer
            {
                FirstName = null
            });

            Assert.IsFalse(validationResult.IsValid);

            validationResult = entityValidatorBase.Validate(new Customer
            {
                FirstName = "Test",
                LastName = "Test"
            }, "Test");

            Assert.IsTrue(validationResult.IsValid);

            validationResult = entityValidatorBase.Validate(new Customer
            {
                FirstName = string.Empty,
                LastName = "Test"
            }, "Test");

            Assert.IsFalse(validationResult.IsValid);
        }
    }
}
