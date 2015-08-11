namespace Labo.Validation.Tests
{
    using System.Linq;

    using Labo.Validation;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class EntityValidatorBaseFixture
    {
        [Test]
        public void ValidationRules()
        {
            EntityValidatorBase<string> entityValidatorBase = Substitute.For<EntityValidatorBase<string>>();
            IEntityValidationRule<string> entityValidationRule = Substitute.For<IEntityValidationRule<string>>();
            entityValidatorBase.AddValidationRule(entityValidationRule);

            Assert.AreSame(entityValidationRule, entityValidatorBase.ValidationRules.Single());
        }
    }
}
