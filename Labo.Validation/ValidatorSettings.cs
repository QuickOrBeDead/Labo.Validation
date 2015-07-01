namespace Labo.Validation
{
    using System;

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
    }
}
