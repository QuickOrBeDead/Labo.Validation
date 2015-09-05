namespace Labo.Validation.Mvc4.Tests.PropertyValidatorAdapters
{
    using Labo.Validation.Mvc4.PropertyValidatorAdapters;
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class LessThanOrEqualToLaboValidationPropertyValidatorAdapterFixture : LaboPropertyValidatorAdapterFixtureBase
    {
        public override IEntityPropertyValidator CreateValidator()
        {
            return new EntityPropertyValidator(new LessThanOrEqualToValidator(20));
        }

        public override LaboPropertyValidator CreateLaboPropertyValidator(System.Web.Mvc.ModelMetadata propertyMetaData, System.Web.Mvc.ControllerContext controllerContext, IEntityValidationRule entityValidationRule)
        {
            return new LessThanOrEqualToLaboValidationPropertyValidatorAdapter(propertyMetaData, controllerContext, entityValidationRule);
        }

        public override void ValidateClientValidationRules(LaboPropertyValidator laboPropertyValidator, System.Collections.Generic.IList<System.Web.Mvc.ModelClientValidationRule> modelClientValidationRules)
        {
            Assert.AreEqual(1, modelClientValidationRules.Count);
            Assert.AreEqual("'Test' must be less than or equal to '20'.", modelClientValidationRules[0].ErrorMessage);
            Assert.AreEqual("range", modelClientValidationRules[0].ValidationType);

            Assert.IsFalse(laboPropertyValidator.ShouldValidate);
        }
    }
}
