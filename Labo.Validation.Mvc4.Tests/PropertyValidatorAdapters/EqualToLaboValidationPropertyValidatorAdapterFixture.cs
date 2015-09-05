namespace Labo.Validation.Mvc4.Tests.PropertyValidatorAdapters
{
    using System.Linq.Expressions;
    using System.Security.Cryptography.X509Certificates;
    using System.Web.Security.AntiXss;

    using Labo.Common.Utils;
    using Labo.Validation.Mvc4.PropertyValidatorAdapters;
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class EqualToLaboValidationPropertyValidatorAdapterFixture : LaboPropertyValidatorAdapterFixtureBase
    {
        public class Model
        {
            public int Age { get; set; } 
        }

        public override IEntityPropertyValidator CreateValidator()
        {
            return new EqualToEntityPropertyValidator(x => 10, LinqUtils.GetMemberInfo<Model, int>(x => x.Age), typeof(Model));
        }

        public override LaboPropertyValidator CreateLaboPropertyValidator(System.Web.Mvc.ModelMetadata propertyMetaData, System.Web.Mvc.ControllerContext controllerContext, IEntityValidationRule entityValidationRule)
        {
            return new EqualToLaboValidationPropertyValidatorAdapter(propertyMetaData, controllerContext, entityValidationRule);
        }

        public override void ValidateClientValidationRules(LaboPropertyValidator laboPropertyValidator, System.Collections.Generic.IList<System.Web.Mvc.ModelClientValidationRule> modelClientValidationRules)
        {
            Assert.AreEqual(1, modelClientValidationRules.Count);
            Assert.AreEqual("'Test' must be equal to 'Age'.", modelClientValidationRules[0].ErrorMessage);
            Assert.AreEqual("equalto", modelClientValidationRules[0].ValidationType);

            Assert.IsFalse(laboPropertyValidator.ShouldValidate);
        }
    }
}
