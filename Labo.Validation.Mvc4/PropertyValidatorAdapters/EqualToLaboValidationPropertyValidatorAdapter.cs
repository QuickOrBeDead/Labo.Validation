namespace Labo.Validation.Mvc4.PropertyValidatorAdapters
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Web.Mvc;

    using Labo.Validation.Transform;
    using Labo.Validation.Validators;

    /// <summary>
    /// The equal to labo validation property validation adapter.
    /// </summary>
    internal sealed class EqualToLaboValidationPropertyValidatorAdapter : LaboPropertyValidator
    {
        /// <summary>
        /// The validation transformer manager
        /// </summary>
        private readonly IValidationTransformerManager m_ValidationTransformerManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="EqualToLaboValidationPropertyValidatorAdapter"/> class.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="validationRule">The validation rule.</param>
        /// <param name="validationTransformerManager">The validation transformer manager.</param>
        public EqualToLaboValidationPropertyValidatorAdapter(ModelMetadata metadata, ControllerContext controllerContext, IEntityValidationRule validationRule, IValidationTransformerManager validationTransformerManager = null)
            : base(metadata, controllerContext, validationRule)
        {
            ShouldValidate = false;

            m_ValidationTransformerManager = validationTransformerManager;
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
            MemberInfo memberToCompareMemberInfo = validatorProperties.GetPropertyValue<MemberInfo>(Constants.ValidationMessageParameterNames.MEMBER_TO_COMPARE_MEMBER_INFO);
            PropertyInfo propertyInfoToCompare = memberToCompareMemberInfo as PropertyInfo;
            
            if (propertyInfoToCompare != null)
            {
                string message = ValidationRule.GetValidationMessage(Metadata.Model);
                Type ownerType = validatorProperties.GetPropertyValue<Type>(Constants.ValidationMessageParameterNames.OWNER_TYPE);

                string validationModelPropertyName = propertyInfoToCompare.Name;
                string propertyName = GetTransformedPropertyName(validationModelPropertyName, ownerType) ?? validationModelPropertyName;

                string propertyForClientValidation = CompareAttribute.FormatPropertyForClientValidation(propertyName);
                yield return new ModelClientValidationEqualToRule(message, propertyForClientValidation);
            }
        }

        /// <summary>
        /// Gets the name of the transformed property.
        /// </summary>
        /// <param name="validationModelPropertyName">
        /// Name of the validation model property.
        /// </param>
        /// <param name="validationModelType">
        /// The validation Model Type.
        /// </param>
        /// <returns>
        /// The transformed property name.
        /// </returns>
        private string GetTransformedPropertyName(string validationModelPropertyName, Type validationModelType)
        {
            if (m_ValidationTransformerManager == null)
            {
                return null;
            }

            if (validationModelType == null)
            {
                return null;
            }

            IValidationTransformer validationTransformerForValidationModel = m_ValidationTransformerManager.GetValidationTransformerForValidationModel(validationModelType);
            if (validationTransformerForValidationModel == null)
            {
                return null;
            }

            MappingMemberInfo mappingMemberInfo = validationTransformerForValidationModel.TransformPropertyNameFromValidationModel(validationModelPropertyName);
            if (mappingMemberInfo != null)
            {
                return mappingMemberInfo.PropertyName;
            }

            return null;
        }
    }
}