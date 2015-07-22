namespace Labo.Validation.Message
{
    using System;
    using System.Resources;

    /// <summary>
    /// The embedded resource validation message resource manager class.
    /// </summary>
    public sealed class EmbeddedResourceValidationMessageResourceManager : IValidationMessageResourceManager
    {
        /// <summary>
        /// The resource manager
        /// </summary>
        private readonly ResourceManager m_ResourceManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmbeddedResourceValidationMessageResourceManager"/> class.
        /// </summary>
        /// <param name="resourceType">Type of the resource.</param>
        /// <exception cref="System.ArgumentNullException">resourceType</exception>
        public EmbeddedResourceValidationMessageResourceManager(Type resourceType)
        {
            if (resourceType == null)
            {
                throw new ArgumentNullException("resourceType");
            }

            m_ResourceManager = new ResourceManager(resourceType);
        }

        /// <summary>
        /// Gets the validation message format.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>The validation message format.</returns>
        public string GetValidationMessageFormat(string resourceName)
        {
            if (resourceName == null)
            {
                throw new ArgumentNullException("resourceName");
            }

            return m_ResourceManager.GetString(resourceName);
        }
    }
}
