namespace Labo.Validation.Tests.Attributes
{
    using System.Text.RegularExpressions;

    using Labo.Validation.Attributes;
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class RegexValidationAttributeFixture
    {
        [Test]
        public void GetValidator()
        {
            const string expression = @"^[a-zA-Z0-9]\d{2}[a-zA-Z0-9](-\d{3}){2}[A-Za-z0-9]$";
            const RegexOptions regexOptions = RegexOptions.Singleline;
            RegexValidationAttribute regexValidationAttribute = new RegexValidationAttribute(expression, regexOptions);

            Assert.IsInstanceOf(typeof(RegexValidator), regexValidationAttribute.GetValidator());

            RegexValidator regexValidator = (RegexValidator)regexValidationAttribute.GetValidator();
            Assert.AreEqual(regexOptions | RegexOptions.Compiled, regexValidator.Regex.Options);

            Assert.IsTrue(regexValidator.Regex.IsMatch("1298-673-4192"));
        }
    }
}
