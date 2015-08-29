namespace Labo.Validation.Mvc4.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class LaboModelValidatorFixture
    {
        public class TestModel
        {
            public string Name { get; set; }

            public string Test { get; set; }

            public int Age { get; set; }
        }

        public sealed class TestModelValidator : EntityValidatorBase<TestModel>, IClientValidatable
        {
            public TestModelValidator()
            {
                AddRule(x => x.RuleFor(y => y.Name).NotNull());
            }

            public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
            {
                yield return new ModelClientValidationRule
                {
                    ValidationType = "testmodel",
                    ErrorMessage = "TestModel validation invalid"
                };
            }
        }

        public sealed class NonClientValidatableTestModelValidator : EntityValidatorBase<TestModel>
        {
        }

        private ModelValidatorProviderCollection m_OriginalModelValidatorProviderCollection;

        [SetUp]
        public void Setup()
        {
            m_OriginalModelValidatorProviderCollection = new ModelValidatorProviderCollection(ModelValidatorProviders.Providers.ToList());
        }

        [TearDown]
        public void TearDown()
        {
            ModelValidatorProviders.Providers.Clear();

            for (int i = 0; i < m_OriginalModelValidatorProviderCollection.Count; i++)
            {
                ModelValidatorProvider modelValidatorProvider = m_OriginalModelValidatorProviderCollection[i];
                ModelValidatorProviders.Providers.Add(modelValidatorProvider);
            }
        }

        [Test]
        public void GetClientValidationRulesShouldReturnIClientValidatableGetClientValidationRulesWhenValidatorImplementsIClientValidatable()
        {
            ControllerContext controllerContext = new ControllerContext { HttpContext = Substitute.For<HttpContextBase>() };


            ModelMetadata propertyMetaData = GetModelMetaData(typeof(TestModel));
            LaboModelValidator laboModelValidator = new LaboModelValidator(propertyMetaData, controllerContext, new TestModelValidator());
            ModelClientValidationRule clientValidationRule = laboModelValidator.GetClientValidationRules().First();

            Assert.AreEqual("TestModel validation invalid", clientValidationRule.ErrorMessage);
            Assert.AreEqual("testmodel", clientValidationRule.ValidationType);
        }

        [Test]
        public void GetClientValidationRulesShouldReturnEmptyRulesWhenValidatorDoesNotImplementIClientValidatable()
        {
            ControllerContext controllerContext = new ControllerContext { HttpContext = Substitute.For<HttpContextBase>() };

            ModelMetadata propertyMetaData = GetModelMetaData(typeof(TestModel));
            LaboModelValidator laboModelValidator = new LaboModelValidator(propertyMetaData, controllerContext, new NonClientValidatableTestModelValidator());
            IEnumerable<ModelClientValidationRule> clientValidationRules = laboModelValidator.GetClientValidationRules();

            Assert.AreEqual(0, clientValidationRules.Count());
        }

        [Test]
        public void ValidateMustReturnValidationErrorsWhenModelIsNotValid()
        {
            ControllerContext controllerContext = new ControllerContext { HttpContext = Substitute.For<HttpContextBase>() };


            ModelMetadata propertyMetaData = GetModelMetaData(typeof(TestModel), () => new TestModel());
            LaboModelValidator laboModelValidator = new LaboModelValidator(propertyMetaData, controllerContext, new TestModelValidator());
            ModelValidationResult modelValidationResult = laboModelValidator.Validate(null).Single();

            Assert.AreEqual("'Name' must not be empty.", modelValidationResult.Message);
            Assert.AreEqual("Name", modelValidationResult.MemberName);
        }

        [Test]
        public void ValidateMustReturnEmptyValidationResultsWhenModelIsValid()
        {
            ControllerContext controllerContext = new ControllerContext { HttpContext = Substitute.For<HttpContextBase>() };


            ModelMetadata propertyMetaData = GetModelMetaData(typeof(TestModel), () => new TestModel { Name = "Test" });
            LaboModelValidator laboModelValidator = new LaboModelValidator(propertyMetaData, controllerContext, new TestModelValidator());
            IEnumerable<ModelValidationResult> modelValidationResults = laboModelValidator.Validate(null);

            Assert.AreEqual(0, modelValidationResults.Count());
        }

        private static ModelMetadata GetModelMetaData(Type type, Func<object> modelAccessor = null)
        {
            DataAnnotationsModelMetadataProvider modelMetadataProvider = new DataAnnotationsModelMetadataProvider();
            return modelMetadataProvider.GetMetadataForType(modelAccessor, type);
        }
    }
}
