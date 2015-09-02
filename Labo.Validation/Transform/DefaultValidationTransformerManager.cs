namespace Labo.Validation.Transform
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The default validation transformer manager class.
    /// </summary>
    public sealed class DefaultValidationTransformerManager : IValidationTransformerManager
    {
        /// <summary>
        /// The validation transformers for ui models
        /// </summary>
        private readonly IDictionary<Type, IValidationTransformer> m_ValidationTransformersForUIModels;

        /// <summary>
        /// The validation transformers for validation models
        /// </summary>
        private readonly IDictionary<Type, IValidationTransformer> m_ValidationTransformersForValidationModels;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultValidationTransformerManager"/> class.
        /// </summary>
        public DefaultValidationTransformerManager()
        {
            m_ValidationTransformersForValidationModels = new Dictionary<Type, IValidationTransformer>();
            m_ValidationTransformersForUIModels = new Dictionary<Type, IValidationTransformer>();
        }

        /// <summary>
        /// Registers the validation transformer.
        /// </summary>
        /// <param name="validationTransformer">The validation transformer.</param>
        public void RegisterValidationTransformer(IValidationTransformer validationTransformer)
        {
            if (validationTransformer == null)
            {
                throw new ArgumentNullException("validationTransformer");
            }

            m_ValidationTransformersForUIModels.Add(validationTransformer.UIModelType, validationTransformer);
            m_ValidationTransformersForValidationModels.Add(validationTransformer.ValidationModelType, validationTransformer);
        }

        /// <summary>
        /// Gets the validation transformer for model.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        /// <returns>The validation transformer.</returns>
        public IValidationTransformer GetValidationTransformerForValidationModel(Type modelType)
        {
            if (modelType == null)
            {
                throw new ArgumentNullException("modelType");
            }

            IValidationTransformer validationTransformer;
            m_ValidationTransformersForValidationModels.TryGetValue(modelType, out validationTransformer);

            return validationTransformer;
        }

        /// <summary>
        /// Gets the validation transformer for ui model.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        /// <returns>The validation transformer.</returns>
        public IValidationTransformer GetValidationTransformerForUIModel(Type modelType)
        {
            if (modelType == null)
            {
                throw new ArgumentNullException("modelType");
            }

            IValidationTransformer validationTransformer;
            m_ValidationTransformersForUIModels.TryGetValue(modelType, out validationTransformer);

            return validationTransformer;
        }
    }
}