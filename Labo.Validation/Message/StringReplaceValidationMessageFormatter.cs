using System.Collections.Generic;

namespace Labo.Validation.Message
{
    using System;
    using System.Text;

    /// <summary>
    /// The string replace validation message formatter class.
    /// </summary>
    public sealed class StringReplaceValidationMessageFormatter : IValidationMessageFormatter
    {
        /// <summary>
        /// Formats the message.
        /// </summary>
        /// <param name="messageFormat">The message format.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The formatted message.</returns>
        public string FormatMessage(string messageFormat, IDictionary<string, string> arguments)
        {
            if (messageFormat == null)
            {
                throw new ArgumentNullException("messageFormat");
            }

            if (arguments == null)
            {
                throw new ArgumentNullException("arguments");
            }

            IEnumerator<KeyValuePair<string, string>> parametersEnumerator = arguments.GetEnumerator();
            while (parametersEnumerator.MoveNext())
            {
                KeyValuePair<string, string> pair = parametersEnumerator.Current;
                if (pair.Value != null)
                {
                    messageFormat = messageFormat.Replace("{" + pair.Value + "}", pair.Value);
                }
            }

            return messageFormat;
        }
    }
}
