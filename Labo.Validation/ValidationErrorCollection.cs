namespace Labo.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The validation error collection.
    /// </summary>
    [CollectionDataContract]
    [Serializable]
    public sealed class ValidationErrorCollection : List<ValidationError>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationErrorCollection"/> class.
        /// </summary>
        public ValidationErrorCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationErrorCollection"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public ValidationErrorCollection(IEnumerable<ValidationError> collection)
            : base(collection)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationErrorCollection"/> class.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store.</param>
        public ValidationErrorCollection(int capacity)
            : base(capacity)
        {
        }
    }
}
