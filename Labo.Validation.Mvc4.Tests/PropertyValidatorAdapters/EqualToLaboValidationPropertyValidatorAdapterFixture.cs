namespace Labo.Validation.Mvc4.Tests.PropertyValidatorAdapters
{
    using Labo.Validation.Mvc4.PropertyValidatorAdapters;
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class EqualToLaboValidationPropertyValidatorAdapterFixture : LaboPropertyValidatorAdapterFixtureBase
    {
        public override IValidator CreateValidator()
        {
            return new EqualToValidator(10);
        }

        public override LaboPropertyValidator CreateLaboPropertyValidator(System.Web.Mvc.ModelMetadata propertyMetaData, System.Web.Mvc.ControllerContext controllerContext, IEntityValidationRule entityValidationRule)
        {
            return new EqualToLaboValidationPropertyValidatorAdapter(propertyMetaData, controllerContext, entityValidationRule);
        }

        public override void ValidateClientValidationRules(LaboPropertyValidator laboPropertyValidator, System.Collections.Generic.IList<System.Web.Mvc.ModelClientValidationRule> modelClientValidationRules)
        {
            Assert.AreEqual(1, modelClientValidationRules.Count);
            Assert.AreEqual("'Test' must be equal to '10'.", modelClientValidationRules[0].ErrorMessage);
            Assert.AreEqual("equalto", modelClientValidationRules[0].ValidationType);

            Assert.IsFalse(laboPropertyValidator.ShouldValidate);
        }
    }
}
