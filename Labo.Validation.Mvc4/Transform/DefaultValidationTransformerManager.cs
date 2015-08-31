namespace Labo.Validation.Mvc4.Transform
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The default validation transformer manager class.
    /// </summary>
    public sealed class DefaultValidationTransformerManager : IValidationTransformerManager
    {
        private readonly IDictionary<Type, IValidationTransformer> m_ValidationTransformers;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultValidationTransformerManager"/> class.
        /// </summary>
        public DefaultValidationTransformerManager()
        {
            m_ValidationTransformers = new Dictionary<Type, IValidationTransformer>();
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

            m_ValidationTransformers.Add(validationTransformer.UIModelType, validationTransformer);
        }

        /// <summary>
        /// Gets the validation transformer for model.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        /// <returns>The validation transformer.</returns>
        public IValidationTransformer GetValidationTransformerForModel(Type modelType)
        {
            if (modelType == null)
            {
                throw new ArgumentNullException("modelType");
            }

            IValidationTransformer validationTransformer;
            m_ValidationTransformers.TryGetValue(modelType, out validationTransformer);

            return validationTransformer;
        }
    }
}