namespace Labo.Validation.Ioc.Tests
{
    using System;

    using Labo.Validation.Ioc.Exceptions;

    using NSubstitute;

    using NUnit.Framework;

    //[TestFixture]
    //public class IocContainerValidatorFactoryFixture
    //{
    //    [Test]
    //    public void ConstructorThrowsArgumentNullExceptionWhenIIocContainerResolverIsNull()
    //    {
    //        Assert.Throws<ArgumentNullException>(() => new IocContainerValidatorFactory(null));
    //    }

    //    [Test]
    //    public void GetValidatorForShouldReturnTheRegisteredEntityValidator()
    //    {
    //        IIocContainerResolver iocContainerResolver = Substitute.For<IIocContainerResolver>();
    //        IEntityValidator<string> entityValidator = Substitute.For<IEntityValidator<string>>();
    //        iocContainerResolver.GetInstanceOptionalByName(typeof(IEntityValidator<string>), typeof(string).FullName).Returns(entityValidator);

    //        IocContainerValidatorFactory iocContainerValidatorFactory = new IocContainerValidatorFactory(iocContainerResolver);
    //        Assert.AreSame(entityValidator, iocContainerValidatorFactory.GetValidatorFor<string>());
    //        Assert.AreSame(entityValidator, iocContainerValidatorFactory.GetValidatorFor(typeof(string)));
    //    }

    //    [Test]
    //    public void GetValidatorForThrowsExceptionWhenThereIsNoEntityValidatorRegisteredForTheSpecifiedEntity()
    //    {
    //        IIocContainerResolver iocContainerResolver = Substitute.For<IIocContainerResolver>();
    //        iocContainerResolver.GetInstanceOptionalByName(typeof(IEntityValidator<string>), typeof(string).FullName).Returns(null);

    //        IocContainerValidatorFactory iocContainerValidatorFactory = new IocContainerValidatorFactory(iocContainerResolver);
    //        Assert.Throws<IocContainerValidatorFactoryException>(() => iocContainerValidatorFactory.GetValidatorFor<string>());
    //    }
    //}
}
