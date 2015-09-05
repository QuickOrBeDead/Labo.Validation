namespace Labo.Validation.Tests.Message
{
    using System;

    using Labo.Validation.Message;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class DefaultValidationMessageBuilderFixture
    {
        [Test]
        public void SetMessageFormat()
        {
            IValidationMessageFormatter validationMessageFormatter = Substitute.For<IValidationMessageFormatter>();
            IValidationMessageResourceManager validationMessageResourceManager = Substitute.For<IValidationMessageResourceManager>();
            DefaultValidationMessageBuilder defaultValidationMessageBuilder = new DefaultValidationMessageBuilder(validationMessageFormatter, validationMessageResourceManager);
            const string messageFormat = "'{ValueName}' must be greater than or equal to '{ValueToCompare}'.";
            defaultValidationMessageBuilder.SetMessageFormat(messageFormat);

            Assert.AreEqual(messageFormat, defaultValidationMessageBuilder.ValidationMessageFormat);
        }

        [Test]
        public void SetParameter()
        {
            IValidationMessageFormatter validationMessageFormatter = Substitute.For<IValidationMessageFormatter>();
            IValidationMessageResourceManager validationMessageResourceManager = Substitute.For<IValidationMessageResourceManager>();
            DefaultValidationMessageBuilder defaultValidationMessageBuilder = new DefaultValidationMessageBuilder(validationMessageFormatter, validationMessageResourceManager);
            defaultValidationMessageBuilder.SetParameter("ValueName", "Name");

            Assert.AreEqual("Name", defaultValidationMessageBuilder.Parameters["ValueName"]);
        }

        [Test]
        public void Build()
        {
            IValidationMessageFormatter validationMessageFormatter = Substitute.For<IValidationMessageFormatter>();
            IValidationMessageResourceManager validationMessageResourceManager = Substitute.For<IValidationMessageResourceManager>();
            DefaultValidationMessageBuilder defaultValidationMessageBuilder = new DefaultValidationMessageBuilder(validationMessageFormatter, validationMessageResourceManager);
            const string messageFormat = "'{ValueName}' must be greater than or equal to '{ValueToCompare}'.";
            defaultValidationMessageBuilder.SetMessageFormat(messageFormat);
            defaultValidationMessageBuilder.SetParameter("ValueToCompare", "1");

            defaultValidationMessageBuilder.Build("Name");

            validationMessageFormatter.Received(1).FormatMessage(messageFormat, defaultValidationMessageBuilder.Parameters);
        }

        [Test]
        public void BuildThrowsExceptionWhenMessageFormatIsNull()
        {
            IValidationMessageFormatter validationMessageFormatter = Substitute.For<IValidationMessageFormatter>();
            IValidationMessageResourceManager validationMessageResourceManager = Substitute.For<IValidationMessageResourceManager>();
            DefaultValidationMessageBuilder defaultValidationMessageBuilder = new DefaultValidationMessageBuilder(validationMessageFormatter, validationMessageResourceManager);
            defaultValidationMessageBuilder.SetParameter("ValueToCompare", "1");

            Assert.Throws<InvalidOperationException>(() => defaultValidationMessageBuilder.Build("Name"));
        }
    }
}
