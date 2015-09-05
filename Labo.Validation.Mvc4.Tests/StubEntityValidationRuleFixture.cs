namespace Labo.Validation.Mvc4.Tests
{
    using System.Reflection;

    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class StubEntityValidationRuleFixture
    {
        private sealed class User
        {
            public string Email { get; set; } 
        }

        [Test]
        public void Validate()
        {
            const string propertyName = "Email";
            StubEntityValidationRule entityValidationRule = new StubEntityValidationRule(new EntityPropertyValidator(new NotNullValidator()),  x =>
                {
                    if (x == null)
                    {
                        return null;
                    }

                    PropertyInfo propertyInfo = x.GetType().GetProperty(propertyName);
                    return propertyInfo == null ? null : propertyInfo.GetValue(x);
                }, "User Email", propertyName);

            ValidationResult validationResult = entityValidationRule.Validate(new User());
            
            Assert.IsFalse(validationResult.IsValid);
             
            Assert.AreEqual(1, validationResult.Errors.Count);
            Assert.AreEqual("'User Email' must not be empty.", validationResult.Errors[0].Message);
            Assert.AreEqual("Email", validationResult.Errors[0].PropertyName);
        }

        [Test]
        public void Validate1()
        {
            const string propertyName = "Email";
            StubEntityValidationRule entityValidationRule = new StubEntityValidationRule(new EntityPropertyValidator(new NotNullValidator()), x =>
            {
                if (x == null)
                {
                    return null;
                }

                PropertyInfo propertyInfo = x.GetType().GetProperty(propertyName);
                return propertyInfo == null ? null : propertyInfo.GetValue(x);
            }, "User Email", propertyName);

            ValidationResult validationResult = entityValidationRule.Validate(new User { Email = "test@labo.com" });

            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(0, validationResult.Errors.Count);
        }
    }
}
