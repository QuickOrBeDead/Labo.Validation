namespace Labo.Validation.Mvc4.PropertyValidatorAdapters
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Labo.Validation.Validators;

    /// <summary>
    /// The less than or equal to labo validation property validation adapter.
    /// </summary>
    internal sealed class LessThanOrEqualToLaboValidationPropertyValidatorAdapter : LaboPropertyValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LessThanOrEqualToLaboValidationPropertyValidatorAdapter"/> class.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="validationRule">The validation rule.</param>
        public LessThanOrEqualToLaboValidationPropertyValidatorAdapter(ModelMetadata metadata, ControllerContext controllerContext, IEntityValidationRule validationRule)
            : base(metadata, controllerContext, validationRule)
        {
        }

        /// <summary>
        /// When implemented in a derived class, returns metadata for client validation.
        /// </summary>
        /// <returns>
        /// The metadata for client validation.
        /// </returns>
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            ValidatorProperties validatorProperties = ValidationRule.Validator.GetValidatorProperties();
            string message = ValidationRule.GetValidationMessage(Metadata.Model);

            yield return new ModelClientValidationRangeRule(message, null, validatorProperties.GetPropertyValue(Constants.ValidationMessageParameterNames.VALUE_TO_COMPARE));
        }
    }
}