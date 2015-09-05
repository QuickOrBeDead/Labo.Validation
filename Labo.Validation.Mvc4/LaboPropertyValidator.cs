namespace Labo.Validation.Mvc4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    /// <summary>
    /// The labo property validator class.
    /// </summary>
    public class LaboPropertyValidator : ModelValidator
    {
        /// <summary>
        /// The validation rule
        /// </summary>
        private readonly IEntityValidationRule m_ValidationRule;

        /// <summary>
        /// Gets the validation rule.
        /// </summary>
        /// <value>
        /// The validation rule.
        /// </value>
        public IEntityValidationRule ValidationRule
        {
            get { return m_ValidationRule; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [should validate].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [should validate]; otherwise, <c>false</c>.
        /// </value>
        protected internal bool ShouldValidate { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaboPropertyValidator"/> class.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="validationRule">The validation rule.</param>
        /// <param name="shouldValidate">if set to <c>true</c> [should validate].</param>
        public LaboPropertyValidator(ModelMetadata metadata, ControllerContext controllerContext, IEntityValidationRule validationRule, bool shouldValidate = false)
            : base(metadata, controllerContext)
        {
            if (validationRule == null)
            {
                throw new ArgumentNullException("validationRule");
            }

            ShouldValidate = shouldValidate;

            m_ValidationRule = validationRule;
        }

        /// <summary>
        /// When implemented in a derived class, validates the object.
        /// </summary>
        /// <returns>
        /// A list of validation results.
        /// </returns>
        /// <param name="container">The container.</param>
        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            if (ShouldValidate)
            {
                ModelMetadata modelMetadata = Metadata;
                bool isModelProperty = ModelMetadataHelper.IsModelProperty(modelMetadata);
                ValidationResult result = ValidationRule.Validate(isModelProperty ? null : modelMetadata.Model);

                ValidationErrorCollection errors = result.Errors;
                for (int i = 0; i < errors.Count; i++)
                {
                    ValidationError error = errors[i];
                    yield return new ModelValidationResult { Message = error.Message, MemberName = error.PropertyName };
                }
            }
        }

        /// <summary>
        /// When implemented in a derived class, returns metadata for client validation.
        /// </summary>
        /// <returns>
        /// The metadata for client validation.
        /// </returns>
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            IClientValidatable supportsClientValidation = ValidationRule.Validator as IClientValidatable;

            if (supportsClientValidation != null)
            {
                return supportsClientValidation.GetClientValidationRules(Metadata, ControllerContext);
            }

            return Enumerable.Empty<ModelClientValidationRule>();
        }
    }
}