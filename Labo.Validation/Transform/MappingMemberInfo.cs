namespace Labo.Validation.Transform
{
    using System;
    using System.Reflection;

    /// <summary>
    /// The mapping member info class.
    /// </summary>
    public sealed class MappingMemberInfo
    {
        /// <summary>
        /// The property name
        /// </summary>
        private readonly string m_PropertyName;

        /// <summary>
        /// The member information
        /// </summary>
        private readonly MemberInfo m_MemberInfo;

        /// <summary>
        /// Gets the property name
        /// </summary>
        public string PropertyName
        {
            get
            {
                return m_PropertyName;
            }
        }

        /// <summary>
        /// Gets the member information
        /// </summary>
        public MemberInfo MemberInfo
        {
            get
            {
                return m_MemberInfo;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingMemberInfo"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="memberInfo">The member information.</param>
        public MappingMemberInfo(string propertyName, MemberInfo memberInfo)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException("propertyName");
            }

            if (memberInfo == null)
            {
                throw new ArgumentNullException("memberInfo");
            }

            m_PropertyName = propertyName;
            m_MemberInfo = memberInfo;
        }
    }
}
