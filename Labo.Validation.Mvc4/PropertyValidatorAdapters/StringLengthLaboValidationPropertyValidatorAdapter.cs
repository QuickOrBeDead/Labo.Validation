namespace Labo.Validation.Mvc4.PropertyValidatorAdapters
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Labo.Validation.Validators;

    /// <summary>
    /// The string length labo validation property validation adapter.
    /// </summary>
    internal sealed class StringLengthLaboValidationPropertyValidatorAdapter : LaboPropertyValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringLengthLaboValidationPropertyValidatorAdapter"/> class.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="validationRule">The validation rule.</param>
        public StringLengthLaboValidationPropertyValidatorAdapter(ModelMetadata metadata, ControllerContext controllerContext, IEntityValidationRule validationRule)
            : base(metadata, controllerContext, validationRule)
        {
            ShouldValidate = false;
        }

        /// <summary>
        /// When implemented in a derived class, returns metadata for client validation.
        /// </summary>
        /// <returns>
        /// The metadata for client validation.
        /// </returns>
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            IEntityValidationRule entityValidationRule = ValidationRule;
            string message = entityValidationRule.GetValidationMessage(Metadata.Model);
            ValidatorProperties validatorProperties = entityValidationRule.Validator.GetValidatorProperties();

            yield return
                new ModelClientValidationStringLengthRule(
                    message,
                    validatorProperties.GetPropertyValue<int>(Constants.ValidationMessageParameterNames.MIN),
                    validatorProperties.GetPropertyValue<int>(Constants.ValidationMessageParameterNames.MAX));
        }
    }
}