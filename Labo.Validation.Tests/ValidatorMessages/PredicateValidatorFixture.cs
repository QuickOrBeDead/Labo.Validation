namespace Labo.Validation.Tests.ValidatorMessages
{
    using Labo.Validation.Validators;

    using NUnit.Framework;

    [TestFixture]
    public class PredicateValidatorFixture : ValidationMessageFixtureBase
    {
        public override string GetExpectedValidationMessage()
        {
            return "The specified condition was not met for 'Name'.";
        }

        public override ValidatorBase CreateValidator()
        {
            return new PredicateValidator(x => x != null);
        }
    }
}