namespace Labo.Validation.Mvc4.Tests.PropertyValidatorAdapters
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Labo.Validation.Mvc4.PropertyValidatorAdapters;
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class RegexLaboValidationPropertyValidatorAdapterFixture : LaboPropertyValidatorAdapterFixtureBase
    {
        public override LaboPropertyValidator CreateLaboPropertyValidator(ModelMetadata propertyMetaData, ControllerContext controllerContext, IEntityValidationRule entityValidationRule)
        {
            return new RegexLaboValidationPropertyValidatorAdapter(propertyMetaData, controllerContext, entityValidationRule);
        }

        public override void ValidateClientValidationRules(LaboPropertyValidator laboPropertyValidator, IList<ModelClientValidationRule> modelClientValidationRules)
        {
            Assert.AreEqual(1, modelClientValidationRules.Count);
            Assert.AreEqual("'Test' is not in the correct format.", modelClientValidationRules[0].ErrorMessage);
            Assert.AreEqual("regex", modelClientValidationRules[0].ValidationType);

            Assert.IsFalse(laboPropertyValidator.ShouldValidate);
        }

        public override IValidator CreateValidator()
        {
            return new RegexValidator(@"^\d$");
        }
    }
}
