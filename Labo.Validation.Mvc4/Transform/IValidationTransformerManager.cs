namespace Labo.Validation.Mvc4.Transform
{
    using System;

    /// <summary>
    /// The validation transformer manager interface.
    /// </summary>
    public interface IValidationTransformerManager
    {
        /// <summary>
        /// Gets the validation transformer for model.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        /// <returns>The validation transformer.</returns>
        IValidationTransformer GetValidationTransformerForModel(Type modelType);
    }
}