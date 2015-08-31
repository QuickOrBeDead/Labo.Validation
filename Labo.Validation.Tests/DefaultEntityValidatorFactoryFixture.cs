namespace Labo.Validation.Ioc.Tests
{
    using Labo.Validation;
    using Labo.Validation.Exceptions;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class IocContainerValidatorFactoryFixture
    {
        public class Customer
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }
        }

        public class CustomerValidator : EntityValidatorBase<Customer>
        {
             
        }

        [Test]
        public void GetValidatorForShouldReturnTheRegisteredEntityValidator()
        {
            IEntityValidator<string> entityValidator = Substitute.For<IEntityValidator<string>>();

            DefaultEntityValidatorFactory validatorFactory = new DefaultEntityValidatorFactory();
            validatorFactory.RegisterValidator(entityValidator);

            Assert.AreSame(entityValidator, validatorFactory.GetValidatorFor<string>());
            Assert.AreSame(entityValidator, validatorFactory.GetValidatorFor(typeof(string)));
        }

        [Test]
        public void GetValidatorForThrowsExceptionWhenThereIsNoEntityValidatorRegisteredForTheSpecifiedEntity()
        {
            DefaultEntityValidatorFactory validatorFactory = new DefaultEntityValidatorFactory();
            Assert.Throws<ValidatorFactoryException>(() => validatorFactory.GetValidatorFor<string>());
        }

        [Test]
        public void RegisterValidatorAndRetrieve()
        {
            DefaultEntityValidatorFactory validatorFactory = new DefaultEntityValidatorFactory();
            CustomerValidator customerValidator = new CustomerValidator();

            validatorFactory.RegisterValidator(customerValidator);
            IEntityValidator<Customer> entityValidator = validatorFactory.GetValidatorFor<Customer>();

            Assert.IsNotNull(entityValidator);
            Assert.AreSame(customerValidator, entityValidator);
        }
    }
}
