namespace Labo.Validation.Transform
{
    using System;

    /// <summary>
    /// The validation transformer manager interface.
    /// </summary>
    public interface IValidationTransformerManager
    {
        /// <summary>
        /// Gets the validation transformer for ui model.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        /// <returns>The validation transformer.</returns>
        IValidationTransformer GetValidationTransformerForUIModel(Type modelType);

        /// <summary>
        /// Gets the validation transformer for validation model.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        /// <returns>The validation transformer.</returns>
        IValidationTransformer GetValidationTransformerForValidationModel(Type modelType);
    }
}