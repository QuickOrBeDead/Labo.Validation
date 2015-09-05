namespace Labo.Validation.Mvc4.PropertyValidatorAdapters
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    /// <summary>
    /// The required labo validation property validation adapter.
    /// </summary>
    internal sealed class RequiredLaboPropertyValidatorAdapter : LaboPropertyValidator 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredLaboPropertyValidatorAdapter"/> class.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="validationRule">The validation rule.</param>
        public RequiredLaboPropertyValidatorAdapter(ModelMetadata metadata, ControllerContext controllerContext, IEntityValidationRule validationRule)
            : base(metadata, controllerContext, validationRule)
        {
            Type modelType = metadata.ModelType;
            bool isNonNullableValueType = modelType.IsValueType && !IsNullable(modelType);

            ShouldValidate = isNonNullableValueType && metadata.Model == null;
        }

        /// <summary>
        /// When implemented in a derived class, returns metadata for client validation.
        /// </summary>
        /// <returns>
        /// The metadata for client validation.
        /// </returns>
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules() 
        {
            string message = ValidationRule.GetValidationMessage(Metadata.Model);

            yield return new ModelClientValidationRequiredRule(message);
        }

        /// <summary>
        /// Gets or sets a value that indicates whether a model property is required.
        /// </summary>
        /// <returns>
        /// true if the model property is required; otherwise, false.
        /// </returns>
        public override bool IsRequired 
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Determines whether the specified type is nullable.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if the specified type is nullable, otherwise <c>false</c>.</returns>
        private static bool IsNullable(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}
