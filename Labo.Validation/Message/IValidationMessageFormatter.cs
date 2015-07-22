namespace Labo.Validation.Message
{
    using System.Collections.Generic;

    /// <summary>
    /// The validation message formatter interface.
    /// </summary>
    public interface IValidationMessageFormatter
    {
        /// <summary>
        /// Formats the message.
        /// </summary>
        /// <param name="messageFormat">The message format.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The formatted message.</returns>
        string FormatMessage(string messageFormat, IDictionary<string, string> arguments);
    }
}
