namespace Labo.Validation.Mvc4
{
    using System.Web.Mvc;

    /// <summary>
    /// The model metadata helper class.
    /// </summary>
    internal static class ModelMetadataHelper
    {
        /// <summary>
        /// Determines whether [is model property] [the specified metadata].
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <returns><c>true</c> if the [the specified metadata] [is model property], otherwise <c>false</c>.</returns>
        public static bool IsModelProperty(ModelMetadata metadata)
        {
            return metadata.ContainerType != null && !string.IsNullOrEmpty(metadata.PropertyName);
        }
    }
}
