namespace Labo.Validation.Mvc4.Tests.PropertyValidatorAdapters
{
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;

    using Labo.Common.Utils;
    using Labo.Validation.Mvc4.PropertyValidatorAdapters;
    using Labo.Validation.Validators;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class RequiredLaboPropertyValidatorAdapterFixture : LaboPropertyValidatorAdapterFixtureBase
    {
        public sealed class TestModelValidator : EntityValidatorBase<TestModel>
        {
            public TestModelValidator()
            {
                AddValidationRule(x => x.RuleFor(y => y.Name).NotNull());
            }
        }

        [Test]
        public void ShouldValidateShouldBeTrueWhenTheSpecifiedPropertyIsNotNullableValueTypeAndModelIsNull()
        {
            ControllerContext controllerContext = new ControllerContext { HttpContext = Substitute.For<HttpContextBase>() };

            string propertyName = LinqUtils.GetMemberName<TestModel, int>(x => x.Age);
            ModelMetadata propertyMetaData = GetModelMetaDataForProperty(typeof(TestModel), propertyName, null);
            IEntityValidationRule<TestModel> entityValidationRule = new TestModelValidator().EntityValidationRules[0];
            LaboPropertyValidator laboPropertyValidator = CreateLaboPropertyValidator(propertyMetaData, controllerContext, entityValidationRule);
            
            Assert.IsTrue(laboPropertyValidator.ShouldValidate);
        }

        public override LaboPropertyValidator CreateLaboPropertyValidator(ModelMetadata propertyMetaData, ControllerContext controllerContext, IEntityValidationRule entityValidationRule)
        {
            return new RequiredLaboPropertyValidatorAdapter(propertyMetaData, controllerContext, entityValidationRule);
        }

        public override void ValidateClientValidationRules(LaboPropertyValidator laboPropertyValidator, IList<ModelClientValidationRule> modelClientValidationRules)
        {
            Assert.AreEqual(1, modelClientValidationRules.Count);
            Assert.AreEqual("'Test' must not be empty.", modelClientValidationRules[0].ErrorMessage);
            Assert.AreEqual("required", modelClientValidationRules[0].ValidationType);

            Assert.IsFalse(laboPropertyValidator.ShouldValidate);
        }

        public override IValidator CreateValidator()
        {
            return new NotNullValidator();
        }
    }
}
