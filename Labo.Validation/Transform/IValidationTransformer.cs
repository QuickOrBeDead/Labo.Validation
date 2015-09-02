namespace Labo.Validation.Transform
{
    using System;

    /// <summary>
    /// The validation tranformer interface.
    /// </summary>
    public interface IValidationTransformer
    {
        /// <summary>
        /// Gets the type of the UI model.
        /// </summary>
        /// <value>
        /// The type of the UI model.
        /// </value>
        Type UIModelType { get; }

        /// <summary>
        /// Gets the type of the validation model.
        /// </summary>
        /// <value>
        /// The type of the validation model.
        /// </value>
        Type ValidationModelType { get; }

        /// <summary>
        /// Transforms the property name from UI model.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>The property name.</returns>
        MappingMemberInfo TransformPropertyNameFromUIModel(string propertyName);

        /// <summary>
        /// Transforms the property name from validation model.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>The property name.</returns>
        MappingMemberInfo TransformPropertyNameFromValidationModel(string propertyName);

        /// <summary>
        /// Maps the model to validation model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The mapped model.</returns>
        object MapToValidationModel(object model);
    }
}
