namespace Labo.Validation.Mvc4.PropertyValidatorAdapters
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Labo.Validation.Validators;

    /// <summary>
    /// The greater than or equal to labo validation property validation adapter.
    /// </summary>
    internal sealed class GreaterThanOrEqualToLaboValidationPropertyValidatorAdapter : LaboPropertyValidator
    {
        /// <summary>
        /// Gets the greater than original equal to validator.
        /// </summary>
        /// <value>
        /// The greater than original equal to validator.
        /// </value>
        private GreaterThanOrEqualToValidator GreaterThanOrEqualToValidator
        {
            get { return (GreaterThanOrEqualToValidator)ValidationRule.Validator; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GreaterThanOrEqualToLaboValidationPropertyValidatorAdapter"/> class.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="validationRule">The validation rule.</param>
        public GreaterThanOrEqualToLaboValidationPropertyValidatorAdapter(ModelMetadata metadata, ControllerContext controllerContext, IEntityValidationRule validationRule)
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
            string message = ValidationRule.Validator.GetValidationMessage(ValidationRule.GetDisplayName());
            yield return new ModelClientValidationRangeRule(message, GreaterThanOrEqualToValidator.ValueToCompare, null);
        }
    }
}