namespace Labo.Validation
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// The validation error class.
    /// </summary>
    [DataContract]
    [Serializable]
    public sealed class ValidationError
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the target value.
        /// </summary>
        /// <value>
        /// The target value.
        /// </value>
        [DataMember]
        public object TargetValue { get; set; }

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>
        /// The name of the property.
        /// </value>
        [DataMember]       
        public string PropertyName { get; set; }
    }
}