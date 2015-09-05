namespace Labo.Validation.Mvc4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;

    using Labo.Validation.Mvc4.PropertyValidatorAdapters;
    using Labo.Validation.Transform;
    using Labo.Validation.Validators;

    /// <summary>
    /// The labo model validator provider class.
    /// </summary>
    public sealed class LaboModelValidatorProvider : ModelValidatorProvider
    {
        /// <summary>
        /// Gets or sets a value indicating whether [add implicit required attribute for value types].
        /// </summary>
        /// <value>
        /// <c>true</c> if [add implicit required attribute for value types]; otherwise, <c>false</c>.
        /// </value>
        public bool AddImplicitRequiredAttributeForValueTypes { get; set; }

        /// <summary>
        /// Gets or sets the validator factory.
        /// </summary>
        /// <value>
        /// The validator factory.
        /// </value>
        public IValidatorFactory ValidatorFactory
        {
            get
            {
                return m_ValidatorFactory;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                m_ValidatorFactory = value;
            }
        }

        /// <summary>
        /// Gets the property validator factories.
        /// </summary>
        /// <value>
        /// The property validator factories.
        /// </value>
        internal Dictionary<string, Func<ModelMetadata, ControllerContext, IEntityValidationRule, IValidationTransformerManager, ModelValidator>> PropertyValidatorFactories
        {
            get { return m_PropertyValidatorFactories; }
        }

        /// <summary>
        /// The property validator factories
        /// </summary>
        private readonly Dictionary<string, Func<ModelMetadata, ControllerContext, IEntityValidationRule, IValidationTransformerManager, ModelValidator>> m_PropertyValidatorFactories = new Dictionary<string, Func<ModelMetadata, ControllerContext, IEntityValidationRule, IValidationTransformerManager, ModelValidator>>(StringComparer.OrdinalIgnoreCase)
        {
            { "NotNullValidator", (metadata, context, validator, validationTransformerManager) => new RequiredLaboPropertyValidatorAdapter(metadata, context, validator) },
            { "NotEmptyValidator", (metadata, context, validator, validationTransformerManager) => new RequiredLaboPropertyValidatorAdapter(metadata, context, validator) },
                                                                                                                                                                
            // email must come before regex.
            { "EmailValidator", (metadata, context, validator, validationTransformerManager) => new EmailLaboValidationPropertyValidatorAdapter(metadata, context, validator) },
            { "RegexValidator", (metadata, context, validator, validationTransformerManager) => new RegexLaboValidationPropertyValidatorAdapter(metadata, context, validator) },
            { "LengthValidator", (metadata, context, validator, validationTransformerManager) => new StringLengthLaboValidationPropertyValidatorAdapter(metadata, context, validator) },
            { "GreaterThanOrEqualToValidator", (metadata, context, validator, validationTransformerManager) => new GreaterThanOrEqualToLaboValidationPropertyValidatorAdapter(metadata, context, validator) },
            { "LessThanOrEqualToValidator", (metadata, context, validator, validationTransformerManager) => new LessThanOrEqualToLaboValidationPropertyValidatorAdapter(metadata, context, validator) },
            { "BetweenValidator", (metadata, context, validator, validationTransformerManager) => new BetweenLaboValidationPropertyValidatorAdapter(metadata, context, validator) },
            { "EqualToValidator", (metadata, context, validator, validationTransformerManager) => new EqualToLaboValidationPropertyValidatorAdapter(metadata, context, validator, validationTransformerManager) },
            { "EqualToEntityPropertyValidator", (metadata, context, validator, validationTransformerManager) => new EqualToLaboValidationPropertyValidatorAdapter(metadata, context, validator, validationTransformerManager) },            
            { "CreditCardValidator", (metadata, context, validator, validationTransformerManager) => new CreditCardLaboValidationPropertyValidatorAdapter(metadata, context, validator) }
        };

        /// <summary>
        /// The validator factory
        /// </summary>
        private IValidatorFactory m_ValidatorFactory;

        /// <summary>
        /// The validation transformer manager
        /// </summary>
        private readonly IValidationTransformerManager m_ValidationTransformerManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LaboModelValidatorProvider"/> class.
        /// </summary>
        /// <param name="validatorFactory">The validator factory.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
        public LaboModelValidatorProvider(IValidatorFactory validatorFactory = null)
        {
            AddImplicitRequiredAttributeForValueTypes = true;
            ValidatorFactory = validatorFactory ?? new DefaultEntityValidatorFactory();
            m_ValidationTransformerManager = ValidatorSettings.ValidationTransformerManager;
        }

        /// <summary>
        /// Configures the labo model validator provider and adds to the mvc model validator providers.
        /// </summary>
        /// <param name="validatorFactory">The validator factory.</param>
        /// <param name="configurationAction">The configuration action.</param>
        public static void Configure(IValidatorFactory validatorFactory = null, Action<LaboModelValidatorProvider> configurationAction = null)
        {
            configurationAction = configurationAction ?? delegate { };

            LaboModelValidatorProvider provider = new LaboModelValidatorProvider(validatorFactory);
            configurationAction(provider);

            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            ModelValidatorProviders.Providers.Add(provider);
        }

        /// <summary>
        /// Registers the validator factory.
        /// </summary>
        /// <param name="validatorType">Type of the validator.</param>
        /// <param name="factory">The factory.</param>
        /// <exception cref="System.ArgumentNullException">
        /// validatorType
        /// or
        /// factory
        /// </exception>
        public void RegisterValidatorFactory(string validatorType, Func<ModelMetadata, ControllerContext, IEntityValidationRule, IValidationTransformerManager, ModelValidator> factory)
        {
            if (validatorType == null)
            {
                throw new ArgumentNullException("validatorType");
            }

            if (factory == null)
            {
                throw new ArgumentNullException("factory");
            }

            PropertyValidatorFactories[validatorType] = factory;
        }

        /// <summary>
        /// Gets a list of validators.
        /// </summary>
        /// <returns>
        /// A list of validators.
        /// </returns>
        /// <param name="metadata">The metadata.</param><param name="context">The context.</param>
        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
        {
            if (metadata == null)
            {
                throw new ArgumentNullException("metadata");
            }

            bool isModelProperty = ModelMetadataHelper.IsModelProperty(metadata);

            Type modelType = isModelProperty ? metadata.ContainerType : metadata.ModelType;
            IValidationTransformer validationTransformer = GetValidationTransformer(modelType);
            IEntityValidator validator = ValidatorFactory.GetValidatorForOptional(validationTransformer == null ? modelType : validationTransformer.ValidationModelType);

            if (isModelProperty)
            {
                return GetValidatorsForProperty(metadata, context, validator, validationTransformer);
            }

            return GetValidatorsForModel(metadata, context, validator, validationTransformer);
        }

        /// <summary>
        /// Gets the validation transformer.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        /// <returns>The validation transformer.</returns>
        private IValidationTransformer GetValidationTransformer(Type modelType)
        {
            return m_ValidationTransformerManager.GetValidationTransformerForUIModel(modelType);
        }

        /// <summary>
        /// Gets the model property validator.
        /// </summary>
        /// <param name="meta">The meta.</param>
        /// <param name="context">The context.</param>
        /// <param name="validationRule">The validation rule.</param>
        /// <returns>The model validator.</returns>
        private ModelValidator GetModelPropertyValidator(ModelMetadata meta, ControllerContext context, IEntityValidationRule validationRule)
        {
            string type = validationRule.Validator.GetValidatorTypeName();

            Func<ModelMetadata, ControllerContext, IEntityValidationRule, IValidationTransformerManager, ModelValidator> factory;

            if (!m_PropertyValidatorFactories.TryGetValue(type, out factory))
            {
                factory = (metadata, controllerContext, validator, validatorTransFormerManager) => new LaboPropertyValidator(metadata, controllerContext, validator);
            }

            return factory(meta, context, validationRule, m_ValidationTransformerManager);
        }

        /// <summary>
        /// Gets the validators for property.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="context">The context.</param>
        /// <param name="validator">The validator.</param>
        /// <param name="validationTransformer">The validation transformer.</param>
        /// <returns>The validators.</returns>
        private IEnumerable<ModelValidator> GetValidatorsForProperty(ModelMetadata metadata, ControllerContext context, IEntityValidator validator, IValidationTransformer validationTransformer)
        {
            List<ModelValidator> modelValidators = new List<ModelValidator>();

            if (validator != null)
            {
                IList<IEntityValidationRule> validationRules = validator.ValidationRules;

                HashSet<string> requiredPropertyValidators = new HashSet<string>(StringComparer.OrdinalIgnoreCase); 

                for (int i = 0; i < validationRules.Count; i++)
                {
                    IEntityValidationRule entityValidationRule = validationRules[i];

                    string propertyName = ValidationTransformerHelper.GetPropertyName(metadata, validationTransformer);
                    if (entityValidationRule.MemberInfo.Name == propertyName)
                    {
                        ModelValidator modelValidator = GetModelPropertyValidator(metadata, context, entityValidationRule);
                        if (modelValidator != null)
                        {
                            if (modelValidator.IsRequired)
                            {
                                // There can be one required validator per property
                                if (!requiredPropertyValidators.Contains(propertyName))
                                {
                                    requiredPropertyValidators.Add(propertyName);
                                    modelValidators.Add(modelValidator); 
                                }
                            }
                            else
                            {
                                modelValidators.Add(modelValidator);                                
                            }
                        }
                    }
                }

                if (metadata.IsRequired && AddImplicitRequiredAttributeForValueTypes)
                {
                    bool hasRequiredValidators = modelValidators.Any(x => x.IsRequired);

                    if (!hasRequiredValidators)
                    {
                        modelValidators.Add(CreateNotNullValidatorForProperty(metadata, context));
                    }
                }
            }

            return modelValidators;
        }

        /// <summary>
        /// Creates the not null validator for property.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="context">The context.</param>
        /// <returns>The model validator.</returns>
        private static ModelValidator CreateNotNullValidatorForProperty(ModelMetadata metadata, ControllerContext context)
        {
            return new RequiredLaboPropertyValidatorAdapter(
                metadata, 
                context, 
                new StubEntityValidationRule(
                    new EntityPropertyValidator(new NotNullValidator()),
                    x =>
                    {
                        if (x == null)
                        {
                            return null;
                        }

                        PropertyInfo propertyInfo = x.GetType().GetProperty(metadata.PropertyName);
                        return propertyInfo == null ? null : propertyInfo.GetValue(x);
                    }, 
                    metadata.GetDisplayName(), 
                    metadata.PropertyName));
        }

        /// <summary>
        /// Gets the validators for model.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="context">The context.</param>
        /// <param name="validator">The validator.</param>
        /// <param name="validationTransformer">The validation transformer.</param>
        /// <returns>The model validators.</returns>
        private static IEnumerable<ModelValidator> GetValidatorsForModel(ModelMetadata metadata, ControllerContext context, IEntityValidator validator, IValidationTransformer validationTransformer)
        {
            if (validator != null)
            {
                yield return new LaboModelValidator(metadata, context, validator, validationTransformer);
            }
        }
    }
}
