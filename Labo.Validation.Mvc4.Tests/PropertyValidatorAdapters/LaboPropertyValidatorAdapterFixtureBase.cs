namespace Labo.Validation.Mvc4.Tests.PropertyValidatorAdapters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Labo.Common.Utils;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public abstract class LaboPropertyValidatorAdapterFixtureBase
    {
        public class TestModel
        {
            public string Name { get; set; }

            public int Age { get; set; }
        }

        protected static ModelMetadata GetModelMetaDataForProperty(Type type, string propertyName, Func<object> modelCreator)
        {
            DataAnnotationsModelMetadataProvider modelMetadataProvider = new DataAnnotationsModelMetadataProvider();
            return modelMetadataProvider.GetMetadataForProperty(modelCreator, type, propertyName);
        }

        [Test]
        public void AssertClientValidationRules()
        {
            ControllerContext controllerContext = new ControllerContext { HttpContext = Substitute.For<HttpContextBase>() };

            string propertyName = LinqUtils.GetMemberName<TestModel, string>(x => x.Name);
            ModelMetadata propertyMetaData = GetModelMetaDataForProperty(typeof(TestModel), propertyName, () => new TestModel());
            LaboPropertyValidator laboPropertyValidator = CreateLaboPropertyValidator(propertyMetaData, controllerContext, CreateEntityValidationRule());
            IList<ModelClientValidationRule> modelClientValidationRules = laboPropertyValidator.GetClientValidationRules().ToList();

            ValidateClientValidationRules(laboPropertyValidator, modelClientValidationRules);
        }

        private IEntityValidationRule CreateEntityValidationRule()
        {
            return new StubEntityValidationRule(CreateValidator(), x => null, "Test", "Test");
        }

        public abstract IValidator CreateValidator();

        public abstract LaboPropertyValidator CreateLaboPropertyValidator(ModelMetadata propertyMetaData, ControllerContext controllerContext, IEntityValidationRule entityValidationRule);

        public abstract void ValidateClientValidationRules(LaboPropertyValidator laboPropertyValidator, IList<ModelClientValidationRule> modelClientValidationRules);
    }
}