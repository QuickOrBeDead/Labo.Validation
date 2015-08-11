namespace Labo.Validation.Tests.Message
{
    using System.Collections.Generic;

    using Labo.Validation.Message;

    using NUnit.Framework;

    [TestFixture]
    public class StringReplaceValidationMessageFormatterFixture
    {
        [Test]
        public void FormatMessage()
        {
            const string messageFormat = "'{ValueName}' must be greater than or equal to '{ValueToCompare}'.";

            StringReplaceValidationMessageFormatter validationMessageFormatter = new StringReplaceValidationMessageFormatter();

            Assert.AreEqual("'Age' must be greater than or equal to '40'.", validationMessageFormatter.FormatMessage(messageFormat, new Dictionary<string, string> { { "ValueName", "Age" }, { "ValueToCompare", "40" } }));
        }

        [Test]
        public void FormatMessageDoesNotThrowExceptionAlthoughTheParameterDoesNotExist()
        {
            const string messageFormat = "'{ValueName}' must be greater than or equal to '{ValueToCompare}'.";

            StringReplaceValidationMessageFormatter validationMessageFormatter = new StringReplaceValidationMessageFormatter();

            Assert.AreEqual("'Age' must be greater than or equal to '{ValueToCompare}'.", validationMessageFormatter.FormatMessage(messageFormat, new Dictionary<string, string> { { "ValueName", "Age" } }));
        }
    }
}
