namespace Labo.Validation
{
    using System.Reflection;

    /// <summary>
    /// The property display name resolver.
    /// </summary>
    public interface IPropertyDisplayNameResolver
    {
        /// <summary>
        /// Gets the display name of the property.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>The display name.</returns>
        string GetDisplayName(MemberInfo memberInfo);
    }
}
