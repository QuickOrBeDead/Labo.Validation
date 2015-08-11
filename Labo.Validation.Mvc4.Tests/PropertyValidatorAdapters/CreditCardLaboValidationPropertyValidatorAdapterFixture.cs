namespace Labo.Validation.Mvc4.Tests.PropertyValidatorAdapters
{
    using Labo.Validation.Mvc4.PropertyValidatorAdapters;
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class CreditCardLaboValidationPropertyValidatorAdapterFixture : LaboPropertyValidatorAdapterFixtureBase
    {
        public override IValidator CreateValidator()
        {
            return new CreditCardValidator();
        }

        public override LaboPropertyValidator CreateLaboPropertyValidator(System.Web.Mvc.ModelMetadata propertyMetaData, System.Web.Mvc.ControllerContext controllerContext, IEntityValidationRule entityValidationRule)
        {
            return new CreditCardLaboValidationPropertyValidatorAdapter(propertyMetaData, controllerContext, entityValidationRule);
        }

        public override void ValidateClientValidationRules(LaboPropertyValidator laboPropertyValidator, System.Collections.Generic.IList<System.Web.Mvc.ModelClientValidationRule> modelClientValidationRules)
        {
            Assert.AreEqual(1, modelClientValidationRules.Count);
            Assert.AreEqual("'Test' is not a valid credit card number.", modelClientValidationRules[0].ErrorMessage);
            Assert.AreEqual("creditcard", modelClientValidationRules[0].ValidationType);

            Assert.IsFalse(laboPropertyValidator.ShouldValidate);
        }
    }
}
