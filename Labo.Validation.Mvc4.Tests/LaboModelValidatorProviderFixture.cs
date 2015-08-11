namespace Labo.Validation.Mvc4.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;
    using System.Web.Mvc;

    using Labo.Common.Utils;
    using Labo.Validation.Mvc4.PropertyValidatorAdapters;
    using Labo.Validation.Validators;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class LaboModelValidatorProviderFixture
    {
        private ModelValidatorProviderCollection m_OriginalModelValidatorProviderCollection;

        [SetUp]
        public void Setup()
        {
            m_OriginalModelValidatorProviderCollection = new ModelValidatorProviderCollection(ModelValidatorProviders.Providers.ToList());
        }

        [TearDown]
        public void TearDown()
        {
            ModelValidatorProviders.Providers.Clear();

            for (int i = 0; i < m_OriginalModelValidatorProviderCollection.Count; i++)
            {
                ModelValidatorProvider modelValidatorProvider = m_OriginalModelValidatorProviderCollection[i];
                ModelValidatorProviders.Providers.Add(modelValidatorProvider);
            }
        }

        [Test]
        public void Configure()
        {
            IValidatorFactory validatorFactory = Substitute.For<IValidatorFactory>();

            LaboModelValidatorProvider.Configure(validatorFactory);

            Assert.IsFalse(DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes);

            LaboModelValidatorProvider laboModelValidatorProvider = ModelValidatorProviders.Providers.Last() as LaboModelValidatorProvider;
           
            Assert.IsNotNull(laboModelValidatorProvider);
            Assert.AreEqual(validatorFactory, laboModelValidatorProvider.ValidatorFactory);
            Assert.AreEqual(true, laboModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes);
        }

        [Test]
        public void ConfigureWithConfigurationAction()
        {
            IValidatorFactory validatorFactory = Substitute.For<IValidatorFactory>();
            Func<ModelMetadata, ControllerContext, IEntityValidationRule, ModelValidator> phoneNumberValidatorFactory = (metadata, context, validator) => new LaboPropertyValidator(metadata, context, new StubEntityValidationRule(new PhoneNumberValidator(), y => metadata.Model, metadata.GetDisplayName(), metadata.PropertyName));

            LaboModelValidatorProvider.Configure(validatorFactory, x =>
            {
                x.AddImplicitRequiredAttributeForValueTypes = false;
                x.RegisterValidatorFactory(typeof(PhoneNumberValidator), phoneNumberValidatorFactory);
            });

            Assert.IsFalse(DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes);

            LaboModelValidatorProvider laboModelValidatorProvider = ModelValidatorProviders.Providers.Last() as LaboModelValidatorProvider;

            Assert.IsNotNull(laboModelValidatorProvider);
            Assert.AreEqual(false, laboModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes);
            Assert.AreSame(phoneNumberValidatorFactory, laboModelValidatorProvider.PropertyValidatorFactories[typeof(PhoneNumberValidator)]);
        }

        public class TestModel
        {
            public string Name { get; set; }

            public string Email { get; set; }

            public string Url { get; set; }

            public string Type { get; set; }

            public string Notes { get; set; }

            public int Age { get; set; }
        }

        public sealed class TestModelValidator : EntityValidatorBase<TestModel>
        {
            public TestModelValidator()
            {
                AddValidationRule(x => x.RuleFor(y => y.Name).NotNull());
                AddValidationRule(x => x.RuleFor(y => y.Name).NotEmpty());
                AddValidationRule(x => x.RuleFor(y => y.Name).MaxLength(10));

                AddValidationRule(x => x.RuleFor(y => y.Type).Must(y => !string.IsNullOrWhiteSpace(y)));
                
                AddValidationRule(x => x.RuleFor(y => y.Email).EmailAddress());

                AddValidationRule(x => x.RuleFor(y => y.Url).Url());
            } 
        }

        [Test]
        public void GetValidators_ValidatorFactoryGetValidatorForShouldBeCalled()
        {
            IValidatorFactory validatorFactory = Substitute.For<IValidatorFactory>();
            ControllerContext controllerContext = new ControllerContext { HttpContext = Substitute.For<HttpContextBase>() };
            LaboModelValidatorProvider provider = new LaboModelValidatorProvider(validatorFactory);
            ModelMetadata modelMetaData = GetModelMetaData(typeof (TestModel));
            IEnumerable<ModelValidator> modelValidators = provider.GetValidators(modelMetaData, controllerContext);

            Assert.IsTrue(modelValidators.First() is LaboModelValidator);

            validatorFactory.Received(1).GetValidatorFor(modelMetaData.ModelType);
        }

        [Test]
        public void GetValidators_ValidatorFactoryGetValidatorForContainerTypeShouldBeCalled()
        {
            IValidatorFactory validatorFactory = Substitute.For<IValidatorFactory>();
            ControllerContext controllerContext = new ControllerContext { HttpContext = Substitute.For<HttpContextBase>() };
            LaboModelValidatorProvider provider = new LaboModelValidatorProvider(validatorFactory);
            ModelMetadata modelMetaData = GetModelMetaDataForProperty(typeof(TestModel), LinqUtils.GetMemberName<TestModel, string>(x => x.Name));
            provider.GetValidators(modelMetaData, controllerContext);

            validatorFactory.Received(1).GetValidatorFor(modelMetaData.ContainerType);
        }

        [Test]
        public void GetValidators_ShouldReturnValidatorsWithTheSamePropertiesAsTheSpecifiedProperty()
        {
            IValidatorFactory validatorFactory = Substitute.For<IValidatorFactory>();
            validatorFactory.GetValidatorFor(typeof(TestModel)).Returns(x => new TestModelValidator());

            ControllerContext controllerContext = new ControllerContext { HttpContext = Substitute.For<HttpContextBase>() };
            LaboModelValidatorProvider provider = new LaboModelValidatorProvider(validatorFactory);

            ValidateValidatorPropertyNames(x => x.Name, provider, controllerContext);
            ValidateValidatorPropertyNames(x => x.Email, provider, controllerContext);
        }

        [Test]
        public void GetValidators_ShouldReturnTheRightValidators()
        {
            IValidatorFactory validatorFactory = Substitute.For<IValidatorFactory>();
            validatorFactory.GetValidatorFor(typeof(TestModel)).Returns(x => new TestModelValidator());

            ControllerContext controllerContext = new ControllerContext { HttpContext = Substitute.For<HttpContextBase>() };
            LaboModelValidatorProvider provider = new LaboModelValidatorProvider(validatorFactory);

            string propertyName = LinqUtils.GetMemberName<TestModel, string>(x => x.Name);
            ModelMetadata propertyMetaData = GetModelMetaDataForProperty(typeof(TestModel), propertyName);
            IList<ModelValidator> properyValidators = provider.GetValidators(propertyMetaData, controllerContext).ToList();

            Assert.AreEqual(3, properyValidators.Count);
            Assert.IsTrue(((LaboPropertyValidator)properyValidators[0]).ValidationRule.Validator is NotNullValidator);
            Assert.IsTrue(((LaboPropertyValidator)properyValidators[1]).ValidationRule.Validator is NotEmptyValidator);
            Assert.IsTrue(((LaboPropertyValidator)properyValidators[2]).ValidationRule.Validator is LengthValidator);
            Assert.AreEqual(10, ((LengthValidator)((LaboPropertyValidator)properyValidators[2]).ValidationRule.Validator).Max);
        }

        [Test]
        public void GetValidators_ShouldReturnNotNullValidatorWhenModelMetadataIsRequiredAndAddImplicitRequiredAttributeForValueTypesIsTrue()
        {
            IValidatorFactory validatorFactory = Substitute.For<IValidatorFactory>();
            validatorFactory.GetValidatorFor(typeof(TestModel)).Returns(x => new TestModelValidator());

            ControllerContext controllerContext = new ControllerContext { HttpContext = Substitute.For<HttpContextBase>() };
            LaboModelValidatorProvider provider = new LaboModelValidatorProvider(validatorFactory)
            {
                AddImplicitRequiredAttributeForValueTypes = true
            };

            string propertyName = LinqUtils.GetMemberName<TestModel, int>(x => x.Age);
            ModelMetadata propertyMetaData = GetModelMetaDataForProperty(typeof(TestModel), propertyName);
            IList<ModelValidator> properyValidators = provider.GetValidators(propertyMetaData, controllerContext).ToList();

            Assert.AreEqual(1, properyValidators.Count);
            Assert.IsTrue(properyValidators[0] is RequiredLaboPropertyValidatorAdapter);
            Assert.IsTrue(((LaboPropertyValidator)properyValidators[0]).ValidationRule.Validator is NotNullValidator);
        }

        [Test]
        public void GetValidators_ShouldNotReturnNotNullValidatorWhenModelMetadataIsRequiredAndAddImplicitRequiredAttributeForValueTypesIsFalse()
        {
            IValidatorFactory validatorFactory = Substitute.For<IValidatorFactory>();
            validatorFactory.GetValidatorFor(typeof(TestModel)).Returns(x => new TestModelValidator());

            ControllerContext controllerContext = new ControllerContext { HttpContext = Substitute.For<HttpContextBase>() };
            LaboModelValidatorProvider provider = new LaboModelValidatorProvider(validatorFactory)
            {
                AddImplicitRequiredAttributeForValueTypes = false
            };

            string propertyName = LinqUtils.GetMemberName<TestModel, int>(x => x.Age);
            ModelMetadata propertyMetaData = GetModelMetaDataForProperty(typeof(TestModel), propertyName);
            IList<ModelValidator> properyValidators = provider.GetValidators(propertyMetaData, controllerContext).ToList();

            Assert.AreEqual(0, properyValidators.Count);
        }

        [Test]
        public void GetValidators_ShouldReturnRegisteredLaboPropertyValidatorWhenTheSpecifiedValidatorTypeImplementsOneOfTheTypesInThePropertyValidatorFactories()
        {
            IValidatorFactory validatorFactory = Substitute.For<IValidatorFactory>();
            validatorFactory.GetValidatorFor(typeof(TestModel)).Returns(x => new TestModelValidator());

            ControllerContext controllerContext = new ControllerContext { HttpContext = Substitute.For<HttpContextBase>() };
            LaboModelValidatorProvider provider = new LaboModelValidatorProvider(validatorFactory)
            {
                AddImplicitRequiredAttributeForValueTypes = false
            };

            string propertyName = LinqUtils.GetMemberName<TestModel, string>(x => x.Url);
            ModelMetadata propertyMetaData = GetModelMetaDataForProperty(typeof(TestModel), propertyName);
            IList<ModelValidator> properyValidators = provider.GetValidators(propertyMetaData, controllerContext).ToList();

            Assert.IsTrue(properyValidators.First() is RegexLaboValidationPropertyValidatorAdapter);
        }

        [Test]
        public void GetValidators_ShouldReturnLaboPropertyValidatorWhenTheSpecifiedValidatorTypeIsNotInThePropertyValidatorFactories()
        {
            IValidatorFactory validatorFactory = Substitute.For<IValidatorFactory>();
            validatorFactory.GetValidatorFor(typeof(TestModel)).Returns(x => new TestModelValidator());

            ControllerContext controllerContext = new ControllerContext { HttpContext = Substitute.For<HttpContextBase>() };
            LaboModelValidatorProvider provider = new LaboModelValidatorProvider(validatorFactory)
            {
                AddImplicitRequiredAttributeForValueTypes = false
            };

            string propertyName = LinqUtils.GetMemberName<TestModel, string>(x => x.Type);
            ModelMetadata propertyMetaData = GetModelMetaDataForProperty(typeof(TestModel), propertyName);
            IList<ModelValidator> properyValidators = provider.GetValidators(propertyMetaData, controllerContext).ToList();

            Assert.IsTrue(properyValidators.First() is LaboPropertyValidator);
        }

        [Test]
        public void ShouldProcessTheDefaultValidatorForNotNullableValueTypesWhenFormValueIsNull()
        {
            ControllerContext controllerContext = new ControllerContext { HttpContext = Substitute.For<HttpContextBase>() };
            IValidatorFactory validatorFactory = Substitute.For<IValidatorFactory>();
            validatorFactory.GetValidatorFor(typeof(TestModel)).Returns(x => new TestModelValidator());

            LaboModelValidatorProvider.Configure(validatorFactory);

            FormCollection form = new FormCollection
            {
                {"Age", string.Empty}
            };

            ModelBindingContext bindingContext = new ModelBindingContext
            {
                ModelName = "test",
                ModelMetadata = GetModelMetaData(typeof(TestModel)),
                ModelState = new ModelStateDictionary(),
                FallbackToEmptyPrefix = true,
                ValueProvider = form.ToValueProvider()
            };

            DefaultModelBinder binder = new DefaultModelBinder();
            binder.BindModel(controllerContext, bindingContext);

            Assert.AreEqual("'Age' must not be empty.", bindingContext.ModelState["Age"].Errors.Single().ErrorMessage);
        }

        private static void ValidateValidatorPropertyNames(Expression<Func<TestModel, string>> propertyNameExpression, ModelValidatorProvider provider, ControllerContext controllerContext)
        {
            string propertyName = LinqUtils.GetMemberName(propertyNameExpression);
            ModelMetadata propertyMetaData = GetModelMetaDataForProperty(typeof (TestModel), propertyName);
            IList<ModelValidator> properyValidators = provider.GetValidators(propertyMetaData, controllerContext).ToList();

            Assert.IsTrue(properyValidators.Count > 0);
            Assert.IsTrue((properyValidators.Cast<LaboPropertyValidator>()).All(x => x.ValidationRule.MemberInfo.Name == propertyName));
        }

        private static ModelMetadata GetModelMetaData(Type type)
        {
            DataAnnotationsModelMetadataProvider modelMetadataProvider = new DataAnnotationsModelMetadataProvider();
            return modelMetadataProvider.GetMetadataForType(null, type);
        }

        private static ModelMetadata GetModelMetaDataForProperty(Type type, string propertyName)
        {
            DataAnnotationsModelMetadataProvider modelMetadataProvider = new DataAnnotationsModelMetadataProvider();
            return modelMetadataProvider.GetMetadataForProperty(null, type, propertyName);
        }
    }
}
