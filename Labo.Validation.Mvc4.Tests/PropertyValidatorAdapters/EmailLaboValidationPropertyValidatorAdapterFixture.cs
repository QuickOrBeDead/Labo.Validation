namespace Labo.Validation.Mvc4.Tests.PropertyValidatorAdapters
{
    using Labo.Validation.Mvc4.PropertyValidatorAdapters;
    using Labo.Validation.Validators;

    using NUnit.Framework;

    public class EmailLaboValidationPropertyValidatorAdapterFixture : LaboPropertyValidatorAdapterFixtureBase
    {
        public override IValidator CreateValidator()
        {
            return new EmailValidator();
        }

        public override LaboPropertyValidator CreateLaboPropertyValidator(System.Web.Mvc.ModelMetadata propertyMetaData, System.Web.Mvc.ControllerContext controllerContext, IEntityValidationRule entityValidationRule)
        {
            return new EmailLaboValidationPropertyValidatorAdapter(propertyMetaData, controllerContext, entityValidationRule);
        }

        public override void ValidateClientValidationRules(LaboPropertyValidator laboPropertyValidator, System.Collections.Generic.IList<System.Web.Mvc.ModelClientValidationRule> modelClientValidationRules)
        {
            Assert.AreEqual(1, modelClientValidationRules.Count);
            Assert.AreEqual("'Test' is not a valid email address.", modelClientValidationRules[0].ErrorMessage);
            Assert.AreEqual("email", modelClientValidationRules[0].ValidationType);

            Assert.IsFalse(laboPropertyValidator.ShouldValidate);
        }
    }
}
