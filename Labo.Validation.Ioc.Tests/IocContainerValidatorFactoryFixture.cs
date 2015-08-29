namespace Labo.Validation.Ioc.Tests
{
    using System;
    using System.Collections.Generic;

    using Labo.Common.Ioc;
    using Labo.Validation;
    using Labo.Validation.Exceptions;
    using Labo.Validation.Ioc;
    using Labo.Validation.Ioc.Exceptions;

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
        public void ConstructorThrowsArgumentNullExceptionWhenIIocContainerResolverIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new IocContainerValidatorFactory(null));
        }

        [Test]
        public void GetValidatorForShouldReturnTheRegisteredEntityValidator()
        {
            IIocContainer iocContainer = Substitute.For<IIocContainer>();
            IEntityValidator<string> entityValidator = Substitute.For<IEntityValidator<string>>();
            iocContainer.GetInstanceOptionalByName(typeof(IEntityValidator<string>), typeof(string).FullName).Returns(entityValidator);
            
            IocContainerValidatorFactory iocContainerValidatorFactory = new IocContainerValidatorFactory(iocContainer);
            Assert.AreSame(entityValidator, iocContainerValidatorFactory.GetValidatorFor<string>());
            Assert.AreSame(entityValidator, iocContainerValidatorFactory.GetValidatorFor(typeof(string)));
        }

        [Test]
        public void GetValidatorForThrowsExceptionWhenThereIsNoEntityValidatorRegisteredForTheSpecifiedEntity()
        {
            IIocContainer iocContainer = Substitute.For<IIocContainer>();
            iocContainer.GetInstanceOptionalByName(typeof(IEntityValidator<string>), typeof(string).FullName).Returns(null);

            IocContainerValidatorFactory iocContainerValidatorFactory = new IocContainerValidatorFactory(iocContainer);
            Assert.Throws<ValidatorFactoryException>(() => iocContainerValidatorFactory.GetValidatorFor<string>());
        }

        [Test]
        public void RegisterValidatorShouldCallRegisterSingleInstanceNamedWithCorrectArguments()
        {
            IIocContainer iocContainer = Substitute.For<IIocContainer>();
            IocContainerValidatorFactory iocContainerValidatorFactory = new IocContainerValidatorFactory(iocContainer);
            CustomerValidator customerValidator = new CustomerValidator();
            
            iocContainer.WhenForAnyArgs(x => x.RegisterSingleInstanceNamed<IEntityValidator<Customer>>(null, null)).Do(x =>
            {
                Func<IIocContainerResolver, IEntityValidator<Customer>> func = x.Arg<Func<IIocContainerResolver, IEntityValidator<Customer>>>();
                string name = x.Arg<string>();

                IEntityValidator<Customer> validator = func(iocContainer);

                Assert.AreEqual(typeof(Customer).FullName, name);
                Assert.AreSame(customerValidator, validator);
            });


            iocContainerValidatorFactory.RegisterValidator(customerValidator);
        }

        [Test]
        public void RegisterValidatorWithIIocContainerResolverShouldCallRegisterSingleInstanceNamedWithCorrectArguments()
        {
            IIocContainer iocContainer = Substitute.For<IIocContainer>();
            IocContainerValidatorFactory iocContainerValidatorFactory = new IocContainerValidatorFactory(iocContainer);
            CustomerValidator customerValidator = new CustomerValidator();

            iocContainer.WhenForAnyArgs(x => x.RegisterSingleInstanceNamed<IEntityValidator<Customer>>(null, null)).Do(x =>
            {
                Func<IIocContainerResolver, IEntityValidator<Customer>> func = x.Arg<Func<IIocContainerResolver, IEntityValidator<Customer>>>();
                string name = x.Arg<string>();

                IEntityValidator<Customer> validator = func(iocContainer);

                Assert.AreEqual(typeof(Customer).FullName, name);
                Assert.AreSame(customerValidator, validator);
            });

            iocContainerValidatorFactory.RegisterValidator(x => customerValidator);
        }

        [Test]
        public void RegisterValidatorAndRetrieve()
        {
            IIocContainer iocContainer = Substitute.For<IIocContainer>();
            IocContainerValidatorFactory iocContainerValidatorFactory = new IocContainerValidatorFactory(iocContainer);
            CustomerValidator customerValidator = new CustomerValidator();

            Dictionary<string, IEntityValidator<Customer>> registry = new Dictionary<string, IEntityValidator<Customer>>();

            iocContainer.WhenForAnyArgs(x => x.RegisterSingleInstanceNamed<IEntityValidator<Customer>>(null, null)).Do(x =>
            {
                Func<IIocContainerResolver, IEntityValidator<Customer>> func = x.Arg<Func<IIocContainerResolver, IEntityValidator<Customer>>>();
                string name = x.Arg<string>();

                IEntityValidator<Customer> validator = func(iocContainer);

                registry.Add(name, validator);
            });

            iocContainer.GetInstanceOptionalByName(null, null).ReturnsForAnyArgs(x =>
            {
                string name = x.Arg<string>();

                return registry[name];
            });

            iocContainerValidatorFactory.RegisterValidator(customerValidator);
            IEntityValidator<Customer> entityValidator = iocContainerValidatorFactory.GetValidatorFor<Customer>();

            Assert.IsNotNull(entityValidator);
            Assert.AreSame(customerValidator, entityValidator);
        }
    }
}
