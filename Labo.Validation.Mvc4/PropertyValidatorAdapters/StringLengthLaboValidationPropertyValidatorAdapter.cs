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
        /// Gets the length validator.
        /// </summary>
        /// <value>
        /// The length validator.
        /// </value>
        private LengthValidator LengthValidator
        {
            get { return (LengthValidator)ValidationRule.Validator; }
        }

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
            string message = ValidationRule.Validator.GetValidationMessage(ValidationRule.GetDisplayName());

            yield return new ModelClientValidationStringLengthRule(message, LengthValidator.Min, LengthValidator.Max);
        }
    }
}