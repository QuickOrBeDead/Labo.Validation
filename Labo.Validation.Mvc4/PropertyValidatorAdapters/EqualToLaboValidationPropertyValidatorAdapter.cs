namespace Labo.Validation.Mvc4.PropertyValidatorAdapters
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    /// <summary>
    /// The equal to labo validation property validation adapter.
    /// </summary>
    internal sealed class EqualToLaboValidationPropertyValidatorAdapter : LaboPropertyValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EqualToLaboValidationPropertyValidatorAdapter"/> class.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="validationRule">The validation rule.</param>
        public EqualToLaboValidationPropertyValidatorAdapter(ModelMetadata metadata, ControllerContext controllerContext, IEntityValidationRule validationRule)
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
            string message = ValidationRule.Validator.GetValidationMessage(ValidationRule.GetDisplayName());
            string propertyForClientValidation = CompareAttribute.FormatPropertyForClientValidation(ValidationRule.MemberName);
            yield return new ModelClientValidationEqualToRule(message, propertyForClientValidation);
        }
    }
}