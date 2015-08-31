namespace Labo.Validation.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    using Labo.Common.Exceptions;

    /// <summary>
    /// The critical validation exception class.
    /// </summary>
    [Serializable]
    public class CriticalValidationException : ValidationException, ICoreLevelException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CriticalValidationException"/> class.
        /// </summary>
        public CriticalValidationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CriticalValidationException"/> class.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        public CriticalValidationException(Exception innerException)
            : base(null, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CriticalValidationException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public CriticalValidationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CriticalValidationException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public CriticalValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CriticalValidationException"/> class.
        /// </summary>
        /// <param name="serializationInfo">The serialization info.</param>
        /// <param name="context">The context.</param>
        protected CriticalValidationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}