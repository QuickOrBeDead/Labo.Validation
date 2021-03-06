namespace Labo.Validation.Mvc4.PropertyValidatorAdapters
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Labo.Validation.Validators;

    /// <summary>
    /// The regex labo validation property validation adapter.
    /// </summary>
    internal sealed class RegexLaboValidationPropertyValidatorAdapter : LaboPropertyValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegexLaboValidationPropertyValidatorAdapter"/> class.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="validationRule">The validation rule.</param>
        public RegexLaboValidationPropertyValidatorAdapter(ModelMetadata metadata, ControllerContext controllerContext, IEntityValidationRule validationRule)
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
            IEntityPropertyValidator entityPropertyValidator = entityValidationRule.Validator;
            ValidatorProperties validatorProperties = entityPropertyValidator.GetValidatorProperties();
            string message = entityValidationRule.GetValidationMessage(Metadata.Model);

            yield return new ModelClientValidationRegexRule(message, validatorProperties.GetPropertyValue<string>(Constants.ValidationMessageParameterNames.REGEX));
        }
    }
}