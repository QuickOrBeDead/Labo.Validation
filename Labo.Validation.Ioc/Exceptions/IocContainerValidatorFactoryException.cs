namespace Labo.Validation.Ioc.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    using Labo.Validation.Exceptions;

    /// <summary>
    /// The ioc validator factory exception class.
    /// </summary>
    [Serializable]
    public class IocContainerValidatorFactoryException : ValidatorFactoryException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IocContainerValidatorFactoryException"/> class.
        /// </summary>
        public IocContainerValidatorFactoryException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IocContainerValidatorFactoryException"/> class.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        public IocContainerValidatorFactoryException(Exception innerException)
            : base(null, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IocContainerValidatorFactoryException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public IocContainerValidatorFactoryException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IocContainerValidatorFactoryException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public IocContainerValidatorFactoryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IocContainerValidatorFactoryException"/> class.
        /// </summary>
        /// <param name="serializationInfo">The serialization info.</param>
        /// <param name="context">The context.</param>
        protected IocContainerValidatorFactoryException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}