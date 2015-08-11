namespace Labo.Validation.Mvc4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    /// <summary>
    /// The model validator class.
    /// </summary>
    public sealed class LaboModelValidator : ModelValidator
    {
        /// <summary>
        /// The validator
        /// </summary>
        private readonly IEntityValidator m_Validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="LaboModelValidator"/> class.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="validator">The validator.</param>
        /// <exception cref="System.ArgumentNullException">validator</exception>
        public LaboModelValidator(ModelMetadata metadata, ControllerContext controllerContext, IEntityValidator validator)
            : base(metadata, controllerContext)
        {
            if (validator == null)
            {
                throw new ArgumentNullException("validator");
            }

            m_Validator = validator;
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
            object model = Metadata.Model;
            if (model != null)
            {
                ValidationResult result = m_Validator.Validate(model);

                if (!result.IsValid)
                {
                    return GetModelValidationResults(result);
                }
            }

            return Enumerable.Empty<ModelValidationResult>();
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
            IClientValidatable supportsClientValidation = m_Validator as IClientValidatable;

            if (supportsClientValidation != null)
            {
                return supportsClientValidation.GetClientValidationRules(Metadata, ControllerContext);
            }

            return Enumerable.Empty<ModelClientValidationRule>();
        }

        /// <summary>
        /// Gets the model validation results.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>The model validation results.</returns>
        private static IEnumerable<ModelValidationResult> GetModelValidationResults(ValidationResult result)
        {
            ValidationErrorCollection errors = result.Errors;
            ModelValidationResult[] modelValidationResults = new ModelValidationResult[errors.Count];
            for (int i = 0; i < errors.Count; i++)
            {
                ValidationError validationError = errors[i];
                modelValidationResults[i] = new ModelValidationResult
                                                {
                                                    MemberName = validationError.PropertyName,
                                                    Message = validationError.Message
                                                };
            }

            return modelValidationResults;
        }
    }
}
