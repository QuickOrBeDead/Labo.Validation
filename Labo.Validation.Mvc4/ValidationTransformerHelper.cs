namespace Labo.Validation.Mvc4
{
    using System.Web.Mvc;

    using Labo.Validation.Transform;

    /// <summary>
    /// The validation transformer helper class.
    /// </summary>
    internal static class ValidationTransformerHelper
    {
        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="validationTransformer">The validation transformer.</param>
        /// <returns>The property name.</returns>
        public static string GetPropertyName(ModelMetadata metadata, IValidationTransformer validationTransformer)
        {
            string propertyName = metadata.PropertyName;
            return GetPropertyName(validationTransformer, propertyName);
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <param name="validationTransformer">The validation transformer.</param>
        /// <param name="defaultPropertyName">Default name of the property.</param>
        /// <returns>The property name.</returns>
        public static string GetPropertyName(IValidationTransformer validationTransformer, string defaultPropertyName)
        {
            if (validationTransformer == null)
            {
                return defaultPropertyName;
            }

            MappingMemberInfo mappingMemberInfo = validationTransformer.TransformPropertyNameFromUIModel(defaultPropertyName);
            return mappingMemberInfo == null ? defaultPropertyName : mappingMemberInfo.PropertyName;
        }
    }
}
