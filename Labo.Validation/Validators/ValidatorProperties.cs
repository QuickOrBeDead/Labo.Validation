namespace Labo.Validation.Validators
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// The validator properties.
    /// </summary>
    public sealed class ValidatorProperties : IEnumerable<KeyValuePair<string, object>>
    {
        /// <summary>
        /// The properties
        /// </summary>
        private readonly IDictionary<string, object> m_Properties;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorProperties"/> class.
        /// </summary>
        public ValidatorProperties()
        {
            m_Properties = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Gets the property value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The property value</returns>
        public object GetPropertyValue(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            object result;
            if (m_Properties.TryGetValue(key, out result))
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// Gets the property value.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>The value.</returns>
        public TValue GetPropertyValue<TValue>(string key)
        {
            object value = GetPropertyValue(key);

            if (value == null)
            {
                return default(TValue);
            }

            return (TValue)value;
        }

        /// <summary>
        /// Adds the property value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value</param>
        public void Add(string key, object value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            if (value == null)
            {
                return;
            }

            m_Properties[key] = value;
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_Properties.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return m_Properties.GetEnumerator();
        }
    }
}
