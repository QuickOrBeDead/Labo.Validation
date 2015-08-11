namespace Labo.Validation.Tests
{
    using System;
    using System.Linq.Expressions;
    using System.Text.RegularExpressions;

    using Labo.Validation.Builder;
    using Labo.Validation.Validators;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class EntityPropertyValidationRuleExtensionsFixture
    {
        public sealed class Customer
        {
            public string Name { get; set; }

            public string Surname { get; set; }

            public int? Age { get; set; }
        }

        public sealed class CustomerValidator : EntityValidatorBase<Customer>
        {
        }

        [Test]
        public void NotNull()
        {
            AssertAddValidatorIsCalled<NotNullValidator>(x => x.NotNull());
        }

        [Test]
        public void NotEmpty()
        {
            AssertAddValidatorIsCalled<NotEmptyValidator>(x => x.NotEmpty());
        }

        [Test]
        public void Length()
        {
            AssertAddValidatorIsCalled<LengthValidator>(x => x.Length(10, 20));
        }

        [Test]
        public void MaxLength()
        {
            AssertAddValidatorIsCalled<LengthValidator>(x => x.MaxLength(10));
        }

        [Test]
        public void ExactLength()
        {
            AssertAddValidatorIsCalled<LengthValidator>(x => x.Length(10));
        }

        [Test]
        public void Matches()
        {
            AssertAddValidatorIsCalled<RegexValidator>(x => x.Matches(string.Empty, RegexOptions.Compiled));
        }

        [Test]
        public void EmailAddress()
        {
            AssertAddValidatorIsCalled<EmailValidator>(x => x.EmailAddress());
        }

        [Test]
        public void EqualTo()
        {
            AssertAddValidatorIsCalled<EqualToValidator>(x => x.EqualTo(string.Empty, StringComparer.OrdinalIgnoreCase));
        }

        [Test]
        public void NotEqualTo()
        {
            AssertAddValidatorIsCalled<NotEqualToValidator>(x => x.NotEqualTo(string.Empty, StringComparer.OrdinalIgnoreCase));
        }

        [Test]
        public void Between()
        {
            AssertAddValidatorIsCalled<BetweenValidator>(x => x.Between("1", "2"));
        }

        [Test]
        public void CreditCard()
        {
            AssertAddValidatorIsCalled<CreditCardValidator>(x => x.CreditCard());
        }

        [Test]
        public void GreaterThan()
        {
            AssertAddValidatorIsCalled<GreaterThanValidator>(x => x.GreaterThan("1"));
        }

        [Test]
        public void GreaterThanOrEqualTo()
        {
            AssertAddValidatorIsCalled<GreaterThanOrEqualToValidator>(x => x.GreaterThanOrEqualTo("1"));
        }

        [Test]
        public void LessThan()
        {
            AssertAddValidatorIsCalled<LessThanValidator>(x => x.LessThan("1"));
        }

        [Test]
        public void LessThanOrEqualTo()
        {
            AssertAddValidatorIsCalled<LessThanOrEqualToValidator>(x => x.LessThanOrEqualTo("1"));
        }

        [Test]
        public void Must()
        {
            AssertAddValidatorIsCalled<PredicateValidator>(x => x.Must(y => y != null));
        }

        [Test]
        public void MustMustValidateEntity()
        {
            CustomerValidator customerValidator = new CustomerValidator();
            customerValidator.AddValidationRule(x => x.RuleFor(y => y.Age).Must(y => y.HasValue && y > 35));

            Assert.IsFalse(customerValidator.Validate(new Customer()).IsValid);
            Assert.IsFalse(customerValidator.Validate(new Customer { Age = 35 }).IsValid);
            Assert.IsTrue(customerValidator.Validate(new Customer { Age = 36 }).IsValid);
        }

        [Test]
        public void Url()
        {
            AssertAddValidatorIsCalled<UrlValidator>(x => x.Url());
        }

        [Test]
        public void PhoneNumber()
        {
            AssertAddValidatorIsCalled<PhoneNumberValidator>(x => x.PhoneNumber());
        }

        [Test]
        public void LessThanOrEqualToForNullableProperty()
        {
            AssertAddValidatorIsCalledForNullableProperty<LessThanOrEqualToValidator>(x => x.LessThanOrEqualTo(1));
        }

        [Test]
        public void LessThanForNullableProperty()
        {
            AssertAddValidatorIsCalledForNullableProperty<LessThanValidator>(x => x.LessThan(1));
        }

        [Test]
        public void GreaterThanForNullableProperty()
        {
            AssertAddValidatorIsCalledForNullableProperty<GreaterThanValidator>(x => x.GreaterThan(1));
        }

        [Test]
        public void GreaterThanOrEqualToForNullableProperty()
        {
            AssertAddValidatorIsCalledForNullableProperty<GreaterThanOrEqualToValidator>(x => x.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void BetweenForNullableProperty()
        {
            AssertAddValidatorIsCalledForNullableProperty<BetweenValidator>(x => x.Between(1, 10));
        }

        [Test]
        public void When()
        {
            Expression<Func<Customer, bool>> expression = x => x.Age.HasValue;
            IEntityValidationRuleBuilder<Customer, string> ruleBuilder = Substitute.For<IEntityValidationRuleBuilder<Customer, string>>();
            ruleBuilder.WhenForAnyArgs(x => x.SetSpecification(null)).Do(x => Assert.AreEqual(expression, x.Arg<ISpecification<Customer>>().Predicate));

            EntityPropertyValidationRuleExtensions.When(ruleBuilder, expression);

            ruleBuilder.ReceivedWithAnyArgs(1).SetSpecification(null);
        }

        [Test]
        public void Unless()
        {
            Expression<Func<Customer, bool>> expression = x => x.Age.HasValue;
            IEntityValidationRuleBuilder<Customer, string> ruleBuilder = Substitute.For<IEntityValidationRuleBuilder<Customer, string>>();
            ruleBuilder.WhenForAnyArgs(x => x.SetSpecification(null)).Do(
                x =>
                    {
                        Expression<Func<Customer, bool>> expected = Expression.Lambda<Func<Customer, bool>>(Expression.Not(Expression.Invoke(expression, expression.Parameters)), expression.Parameters);
                        Func<Customer, bool> expectedFunc = expected.Compile();
                        Func<Customer, bool> parameterFunc = x.Arg<ISpecification<Customer>>().Predicate.Compile();
                        Customer customer = new Customer { Age = 35 };
                        Assert.AreEqual(expectedFunc(customer), parameterFunc(customer));
                        Assert.AreEqual(parameterFunc(customer), !expression.Compile()(customer));
                    });

            ruleBuilder.Unless(expression);

            ruleBuilder.ReceivedWithAnyArgs(1).SetSpecification(null);
        }

        [Test]
        public void NotNullMustThrowArgumentNullExceptionWhenRuleBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => EntityPropertyValidationRuleExtensions.NotNull<Customer, string>(null));
        }

        [Test]
        public void NotEmptyMustThrowArgumentNullExceptionWhenRuleBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => EntityPropertyValidationRuleExtensions.NotEmpty<Customer, string>(null));
        }

        [Test]
        public void LengthMustThrowArgumentNullExceptionWhenRuleBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => EntityPropertyValidationRuleExtensions.Length<Customer>(null, 10, 20));
        }

        [Test]
        public void MaxLengthMustThrowArgumentNullExceptionWhenRuleBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => EntityPropertyValidationRuleExtensions.MaxLength<Customer>(null, 10));
        }

        [Test]
        public void ExactLengthMustThrowArgumentNullExceptionWhenRuleBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => EntityPropertyValidationRuleExtensions.Length<Customer>(null, 10));
        }

        [Test]
        public void MatchesMustThrowArgumentNullExceptionWhenRuleBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => EntityPropertyValidationRuleExtensions.Matches<Customer>(null, string.Empty, RegexOptions.Compiled));
        }

        [Test]
        public void EmailAddressMustThrowArgumentNullExceptionWhenRuleBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => EntityPropertyValidationRuleExtensions.EmailAddress<Customer>(null));
        }

        [Test]
        public void EqualToMustThrowArgumentNullExceptionWhenRuleBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => EntityPropertyValidationRuleExtensions.EqualTo<Customer, string>(null, string.Empty, StringComparer.OrdinalIgnoreCase));
        }

        [Test]
        public void NotEqualToMustThrowArgumentNullExceptionWhenRuleBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => EntityPropertyValidationRuleExtensions.NotEqualTo<Customer, string>(null, string.Empty, StringComparer.OrdinalIgnoreCase));
        }

        [Test]
        public void BetweenMustThrowArgumentNullExceptionWhenRuleBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => EntityPropertyValidationRuleExtensions.Between((IEntityValidationRuleBuilderInitial<Customer, string>)null, "1", "2"));
        }

        [Test]
        public void CreditCardMustThrowArgumentNullExceptionWhenRuleBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => EntityPropertyValidationRuleExtensions.CreditCard<Customer>(null));
        }

        [Test]
        public void MustMustThrowArgumentNullExceptionWhenRuleBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => EntityPropertyValidationRuleExtensions.Must<Customer, string>(null, x => x != null));
        }

        [Test]
        public void MustMustThrowArgumentNullExceptionWhenPredicateIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => EntityPropertyValidationRuleExtensions.Must(Substitute.For<IEntityValidationRuleBuilderInitial<Customer, string>>(), null));
        }

        [Test]
        public void WhenMustThrowArgumentNullExceptionWhenRuleBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => EntityPropertyValidationRuleExtensions.When<Customer, string>(null, x => x.Age != null));
        }

        [Test]
        public void WhenMustThrowArgumentNullExceptionWhenPredicateIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => EntityPropertyValidationRuleExtensions.When(Substitute.For<IEntityValidationRuleBuilder<Customer, string>>(), null));
        }

        [Test]
        public void UnlessMustThrowArgumentNullExceptionWhenRuleBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => EntityPropertyValidationRuleExtensions.Unless<Customer, string>(null, x => x.Age != null));
        }

        [Test]
        public void UnlessMustThrowArgumentNullExceptionWhenPredicateIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => EntityPropertyValidationRuleExtensions.Unless(Substitute.For<IEntityValidationRuleBuilder<Customer, string>>(), null));
        }

        [Test]
        public void GreaterThanMustThrowArgumentNullExceptionWhenRuleBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => EntityPropertyValidationRuleExtensions.GreaterThan((IEntityValidationRuleBuilderInitial<Customer, int>)null, 1));
        }

        [Test]
        public void GreaterThanOrEqualToMustThrowArgumentNullExceptionWhenRuleBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => EntityPropertyValidationRuleExtensions.GreaterThanOrEqualTo((IEntityValidationRuleBuilderInitial<Customer, int>)null, 1));
        }

        [Test]
        public void LessThanMustThrowArgumentNullExceptionWhenRuleBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => EntityPropertyValidationRuleExtensions.LessThan((IEntityValidationRuleBuilderInitial<Customer, int>)null, 1));
        }

        [Test]
        public void LessThanOrEqualToMustThrowArgumentNullExceptionWhenRuleBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => EntityPropertyValidationRuleExtensions.LessThanOrEqualTo((IEntityValidationRuleBuilderInitial<Customer, int>)null, 1));
        }

        [Test]
        public void UrlMustThrowArgumentNullExceptionWhenRuleBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => EntityPropertyValidationRuleExtensions.Url((IEntityValidationRuleBuilderInitial<Customer, string>)null));
        }

        [Test]
        public void PhoneNumberThrowArgumentNullExceptionWhenRuleBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => EntityPropertyValidationRuleExtensions.PhoneNumber((IEntityValidationRuleBuilderInitial<Customer, string>)null));
        }

        [Test]
        public void LessThanOrEqualToForNullablePropertyMustThrowArgumentNullExceptionWhenRuleBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => EntityPropertyValidationRuleExtensions.LessThanOrEqualTo((IEntityValidationRuleBuilderInitial<Customer, int?>)null, 1));
        }

        [Test]
        public void LessThanForNullablePropertyMustThrowArgumentNullExceptionWhenRuleBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => EntityPropertyValidationRuleExtensions.LessThan((IEntityValidationRuleBuilderInitial<Customer, int?>)null, 1));
        }

        [Test]
        public void GreaterThanForNullablePropertyMustThrowArgumentNullExceptionWhenRuleBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => EntityPropertyValidationRuleExtensions.GreaterThan((IEntityValidationRuleBuilderInitial<Customer, int?>)null, 1));
        }

        [Test]
        public void GreaterThanOrEqualToForNullablePropertyMustThrowArgumentNullExceptionWhenRuleBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => EntityPropertyValidationRuleExtensions.GreaterThanOrEqualTo((IEntityValidationRuleBuilderInitial<Customer, int?>)null, 1));
        }

        [Test]
        public void BetweenForNullablePropertyMustThrowArgumentNullExceptionWhenRuleBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => EntityPropertyValidationRuleExtensions.Between((IEntityValidationRuleBuilderInitial<Customer, int?>)null, 1, 10));
        }

        private static void AssertAddValidatorIsCalledForNullableProperty<TValidator>(Action<IEntityValidationRuleBuilderInitial<Customer, int?>> action)
           where TValidator : IValidator
        {
            AssertAddValidatorIsCalled<TValidator, Customer, int?>(action);
        }

        private static void AssertAddValidatorIsCalled<TValidator>(Action<IEntityValidationRuleBuilderInitial<Customer, string>> action)
            where TValidator : IValidator
        {
            AssertAddValidatorIsCalled<TValidator, Customer, string>(action);
        }

        private static void AssertAddValidatorIsCalled<TValidator, TEntity, TProperty>(Action<IEntityValidationRuleBuilderInitial<TEntity, TProperty>> action)
            where TValidator : IValidator
        {
            IEntityValidationRuleBuilderInitial<TEntity, TProperty> ruleBuilder = Substitute.For<IEntityValidationRuleBuilderInitial<TEntity, TProperty>>();
            ruleBuilder.WhenForAnyArgs(x => x.AddValidator(null)).Do(x => Assert.IsInstanceOf<TValidator>(x.Arg<IValidator>()));

            action(ruleBuilder);

            ruleBuilder.ReceivedWithAnyArgs(1).AddValidator(null);
        }
    }
}
