namespace Labo.Validation.Tests
{
    using System;

    using Labo.Validation;
    using Labo.Validation.Message;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class ValidatorSettingsFixture
    {
        [Test]
        public void PropertyDisplayNameResolverMustNotBeSetToNull()
        {
            Assert.Throws<ArgumentNullException>(() => ValidatorSettings.PropertyDisplayNameResolver = null);
        }

        [Test]
        public void ValidationMessageResourceManagerMustNotBeSetToNull()
        {
            Assert.Throws<ArgumentNullException>(() => ValidatorSettings.ValidationMessageResourceManager = null);
        }

        [Test]
        public void ValidationMessageFormatterMustNotBeSetToNull()
        {
            Assert.Throws<ArgumentNullException>(() => ValidatorSettings.ValidationMessageFormatter = null);
        }

        [Test]
        public void PropertyDisplayNameResolver()
        {
            IPropertyDisplayNameResolver propertyDisplayNameResolver = Substitute.For<IPropertyDisplayNameResolver>();
            ValidatorSettings.PropertyDisplayNameResolver = propertyDisplayNameResolver;

            Assert.AreSame(propertyDisplayNameResolver, ValidatorSettings.PropertyDisplayNameResolver);
        }

        [Test]
        public void ValidationMessageResourceManager()
        {
            IValidationMessageResourceManager validationMessageResourceManager = Substitute.For<IValidationMessageResourceManager>();
            ValidatorSettings.ValidationMessageResourceManager = validationMessageResourceManager;

            Assert.AreSame(validationMessageResourceManager, ValidatorSettings.ValidationMessageResourceManager);
        }

        [Test]
        public void ValidationMessageFormatter()
        {
            IValidationMessageFormatter validationMessageFormatter = Substitute.For<IValidationMessageFormatter>();
            ValidatorSettings.ValidationMessageFormatter = validationMessageFormatter;

            Assert.AreSame(validationMessageFormatter, ValidatorSettings.ValidationMessageFormatter);
        }
    }
}
