namespace Labo.Validation.Mvc4.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Labo.Common.Utils;
    using Labo.Validation.Validators;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class LaboPropertyValidatorFixture
    {
        public class TestModel
        {
            public string Name { get; set; }

            public string Test { get; set; }

            public int Age { get; set; }
        }

        public sealed class TestModelValidator : EntityValidatorBase<TestModel>
        {
            public TestModelValidator()
            {
                AddRule(x => x.RuleFor(y => y.Name).NotNull());

                AddRule(x => x.RuleFor(y => y.Test).AddValidator(new EntityPropertyValidator(new TestValidator())));
            }
        }

        public sealed class TestValidator : IValidator, IClientValidatable
        {
            public bool IsValid(object value)
            {
                return true;
            }

            public string GetValidationMessage(string valueName, params string[] arguments)
            {
                return string.Empty;
            }

            public ValidatorProperties GetValidatorProperties()
            {
                return new ValidatorProperties();
            }

            public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
            {
                yield return new ModelClientValidationRule
                {
                    ValidationType = "email",
                    ErrorMessage = "Error Message"
                };
            }

            public ValidatorType ValidatorType
            {
                get { return ValidatorType.EmailValidator; }
            }
        }

        [Test]
        public void ValidateShouldReturnValidationMessageWhenShouldValidateIsSetToTrue()
        {
            ControllerContext controllerContext = new ControllerContext { HttpContext = Substitute.For<HttpContextBase>() };
            
            string propertyName = LinqUtils.GetMemberName<TestModel, string>(x => x.Name);
            ModelMetadata propertyMetaData = GetModelMetaDataForProperty(typeof(TestModel), propertyName, () => new TestModel());
            IEntityValidationRule<TestModel> entityValidationRule = new TestModelValidator().EntityValidationRules[0];
            LaboPropertyValidator laboPropertyValidator = new LaboPropertyValidator(propertyMetaData, controllerContext, entityValidationRule, true);
            ModelValidationResult modelValidationResult = laboPropertyValidator.Validate(null).First();

            Assert.AreEqual("'Name' must not be empty.", modelValidationResult.Message);
            Assert.AreEqual("Name", modelValidationResult.MemberName);
        }

        [Test]
        public void ValidateShouldReturnEmptyValidationResultWhenShouldValidateIsSetToFalse()
        {
            ControllerContext controllerContext = new ControllerContext { HttpContext = Substitute.For<HttpContextBase>() };

            string propertyName = LinqUtils.GetMemberName<TestModel, string>(x => x.Name);
            ModelMetadata propertyMetaData = GetModelMetaDataForProperty(typeof(TestModel), propertyName, () => new TestModel());
            IEntityValidationRule<TestModel> entityValidationRule = new TestModelValidator().EntityValidationRules[0];
            LaboPropertyValidator laboPropertyValidator = new LaboPropertyValidator(propertyMetaData, controllerContext, entityValidationRule, false);
            IEnumerable<ModelValidationResult> modelValidationResults = laboPropertyValidator.Validate(null);

            Assert.AreEqual(0, modelValidationResults.Count());
        }

        [Test]
        public void ValidateShouldReturnEmptyValidationResultWhenShouldValidateIsSetToTrueAndModelIsValid()
        {
            ControllerContext controllerContext = new ControllerContext { HttpContext = Substitute.For<HttpContextBase>() };

            string propertyName = LinqUtils.GetMemberName<TestModel, string>(x => x.Name);
            ModelMetadata propertyMetaData = GetModelMetaDataForProperty(typeof(TestModel), propertyName, () => new TestModel { Name = "Name" });
            IEntityValidationRule<TestModel> entityValidationRule = new TestModelValidator().EntityValidationRules[0];
            LaboPropertyValidator laboPropertyValidator = new LaboPropertyValidator(propertyMetaData, controllerContext, entityValidationRule, true);
            IEnumerable<ModelValidationResult> modelValidationResults = laboPropertyValidator.Validate(null);

            Assert.AreEqual(0, modelValidationResults.Count());
        }

        [Test]
        public void GetClientValidationRulesShouldReturnIClientValidatableGetClientValidationRulesWhenValidatorImplementsIClientValidatable()
        {
            ControllerContext controllerContext = new ControllerContext { HttpContext = Substitute.For<HttpContextBase>() };

            string propertyName = LinqUtils.GetMemberName<TestModel, string>(x => x.Test);
            ModelMetadata propertyMetaData = GetModelMetaDataForProperty(typeof(TestModel), propertyName, () => new TestModel());
            IEntityValidationRule<TestModel> entityValidationRule = new TestModelValidator().EntityValidationRules[1];
            LaboPropertyValidator laboPropertyValidator = new LaboPropertyValidator(propertyMetaData, controllerContext, entityValidationRule);
            ModelClientValidationRule clientValidationRule = laboPropertyValidator.GetClientValidationRules().First();

            Assert.AreEqual("Error Message", clientValidationRule.ErrorMessage);
            Assert.AreEqual("email", clientValidationRule.ValidationType);
        }

        [Test]
        public void GetClientValidationRulesShouldReturnEmptyRulesWhenValidatorDoesNotImplementIClientValidatable()
        {
            ControllerContext controllerContext = new ControllerContext { HttpContext = Substitute.For<HttpContextBase>() };

            string propertyName = LinqUtils.GetMemberName<TestModel, string>(x => x.Test);
            ModelMetadata propertyMetaData = GetModelMetaDataForProperty(typeof(TestModel), propertyName, () => new TestModel());
            IEntityValidationRule<TestModel> entityValidationRule = new TestModelValidator().EntityValidationRules[0];
            LaboPropertyValidator laboPropertyValidator = new LaboPropertyValidator(propertyMetaData, controllerContext, entityValidationRule);
            IEnumerable<ModelClientValidationRule> clientValidationRules = laboPropertyValidator.GetClientValidationRules();

            Assert.AreEqual(0, clientValidationRules.Count());
        }

        private static ModelMetadata GetModelMetaDataForProperty(Type type, string propertyName, Func<object> modelCreator)
        {
            DataAnnotationsModelMetadataProvider modelMetadataProvider = new DataAnnotationsModelMetadataProvider();
            return modelMetadataProvider.GetMetadataForProperty(modelCreator, type, propertyName);
        }
    }
}
