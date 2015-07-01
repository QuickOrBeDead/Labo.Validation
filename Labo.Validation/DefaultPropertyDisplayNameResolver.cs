namespace Labo.Validation
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    using Labo.Common.Utils;

    /// <summary>
    /// The default property display name resolver.
    /// </summary>
    public sealed class DefaultPropertyDisplayNameResolver : IPropertyDisplayNameResolver
    {
        /// <summary>
        /// Gets the display name of the property.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>The display name.</returns>
        public string GetDisplayName(MemberInfo memberInfo)
        {
            if (memberInfo == null)
            {
                throw new ArgumentNullException("memberInfo");
            }

            DisplayAttribute propertyDisplayAttribute = ReflectionUtils.GetCustomAttribute<DisplayAttribute>(memberInfo);
            string propertyDisplayName = null;
            if (propertyDisplayAttribute != null)
            {
                propertyDisplayName = propertyDisplayAttribute.GetName();
            }

            if (string.IsNullOrEmpty(propertyDisplayName))
            {
                DisplayNameAttribute propertyDisplayNameAttribute = ReflectionUtils.GetCustomAttribute<DisplayNameAttribute>(memberInfo);
                if (propertyDisplayNameAttribute != null)
                {
                    propertyDisplayName = propertyDisplayNameAttribute.DisplayName;
                }
            }

            if (string.IsNullOrEmpty(propertyDisplayName))
            {
                propertyDisplayName = memberInfo.Name;
            }

            return propertyDisplayName;
        }
    }
}