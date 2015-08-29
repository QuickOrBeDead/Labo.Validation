namespace Labo.Validation.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    using Labo.Common.Exceptions;

    /// <summary>
    /// The validator factory exception class.
    /// </summary>
    [Serializable]
    public class ValidatorFactoryException : CoreLevelException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorFactoryException"/> class.
        /// </summary>
        public ValidatorFactoryException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorFactoryException"/> class.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        public ValidatorFactoryException(Exception innerException)
            : base(null, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorFactoryException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ValidatorFactoryException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorFactoryException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ValidatorFactoryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorFactoryException"/> class.
        /// </summary>
        /// <param name="serializationInfo">The serialization info.</param>
        /// <param name="context">The context.</param>
        protected ValidatorFactoryException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}