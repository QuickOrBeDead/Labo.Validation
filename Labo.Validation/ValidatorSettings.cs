namespace Labo.Validation
{
    using System;

    using Labo.Validation.Message;
    using Labo.Validation.Resources;

    /// <summary>
    /// The validator settings class.
    /// </summary>
    public static class ValidatorSettings
    {
        /// <summary>
        /// The property display name resolver
        /// </summary>
        private static IPropertyDisplayNameResolver s_PropertyDisplayNameResolver = new DefaultPropertyDisplayNameResolver();

        /// <summary>
        /// The validation message resource manager
        /// </summary>
        private static IValidationMessageResourceManager s_ValidationMessageResourceManager = new EmbeddedResourceValidationMessageResourceManager(typeof(Messages));

        /// <summary>
        /// The validation message formatter
        /// </summary>
        private static IValidationMessageFormatter s_ValidationMessageFormatter = new StringReplaceValidationMessageFormatter();

        /// <summary>
        /// Gets or sets the property display name resolver.
        /// </summary>
        /// <value>
        /// The property display name resolver.
        /// </value>
        public static IPropertyDisplayNameResolver PropertyDisplayNameResolver
        {
            get
            {
                return s_PropertyDisplayNameResolver;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                s_PropertyDisplayNameResolver = value;
            }
        }

        /// <summary>
        /// Gets or sets the validation message resource manager.
        /// </summary>
        /// <value>
        /// The validation message resource manager.
        /// </value>
        /// <exception cref="System.ArgumentNullException">value</exception>
        public static IValidationMessageResourceManager ValidationMessageResourceManager
        {
            get
            {
                return s_ValidationMessageResourceManager;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                s_ValidationMessageResourceManager = value;
            }
        }

        /// <summary>
        /// Gets or sets the validation message formatter.
        /// </summary>
        /// <value>
        /// The validation message formatter.
        /// </value>
        /// <exception cref="System.ArgumentNullException">value</exception>
        public static IValidationMessageFormatter ValidationMessageFormatter
        {
            get
            {
                return s_ValidationMessageFormatter;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                s_ValidationMessageFormatter = value;
            }
        }
    }
}
