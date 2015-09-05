namespace Labo.Validation.Mvc4.Tests.PropertyValidatorAdapters
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Labo.Validation.Mvc4.PropertyValidatorAdapters;
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class StringLengthLaboValidationPropertyValidatorAdapterFixture : LaboPropertyValidatorAdapterFixtureBase
    {
        public override LaboPropertyValidator CreateLaboPropertyValidator(ModelMetadata propertyMetaData, ControllerContext controllerContext, IEntityValidationRule entityValidationRule)
        {
            return new StringLengthLaboValidationPropertyValidatorAdapter(propertyMetaData, controllerContext, entityValidationRule);
        }

        public override void ValidateClientValidationRules(LaboPropertyValidator laboPropertyValidator, IList<ModelClientValidationRule> modelClientValidationRules)
        {
            Assert.AreEqual(1, modelClientValidationRules.Count);
            Assert.AreEqual("'Test' must be between 5 and 10 length.", modelClientValidationRules[0].ErrorMessage);
            Assert.AreEqual("length", modelClientValidationRules[0].ValidationType);

            Assert.IsFalse(laboPropertyValidator.ShouldValidate);
        }

        public override IEntityPropertyValidator CreateValidator()
        {
            return new EntityPropertyValidator(new LengthValidator(5, 10));
        }
    }
}
