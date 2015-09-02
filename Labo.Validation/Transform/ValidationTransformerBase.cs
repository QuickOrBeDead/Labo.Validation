namespace Labo.Validation.Transform
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    using Labo.Common.Utils;

    /// <summary>
    /// The validation transformer base class.
    /// </summary>
    /// <typeparam name="TUIModel">The type of the ui model.</typeparam>
    /// <typeparam name="TValidationModel">The type of the validation model.</typeparam>
    public abstract class ValidationTransformerBase<TUIModel, TValidationModel> : IValidationTransformer
    {
        /// <summary>
        /// The property mappings from UI model to validation model
        /// </summary>
        private readonly IDictionary<string, MappingMemberInfo> m_PropertyMappingsFromUIModelToValidationModel;

        /// <summary>
        /// The property mappings from validation model to UI model
        /// </summary>
        private readonly IDictionary<string, MappingMemberInfo> m_PropertyMappingsFromValidationModelToUIModel;

        /// <summary>
        /// Gets the type of the UI model.
        /// </summary>
        /// <value>
        /// The type of the UI model.
        /// </value>
        public Type UIModelType
        {
            get { return typeof(TUIModel); }
        }

        /// <summary>
        /// Gets the type of the validation model.
        /// </summary>
        /// <value>
        /// The type of the validation model.
        /// </value>
        public Type ValidationModelType
        {
            get { return typeof(TValidationModel); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationTransformerBase{TUIModel, TValidationModel}"/> class.
        /// </summary>
        protected ValidationTransformerBase()
        {
            m_PropertyMappingsFromUIModelToValidationModel = new SortedDictionary<string, MappingMemberInfo>(StringComparer.OrdinalIgnoreCase);
            m_PropertyMappingsFromValidationModelToUIModel = new SortedDictionary<string, MappingMemberInfo>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Adds the property mapping.
        /// </summary>
        /// <param name="uiModelPropertyExpression">The source expression.</param>
        /// <param name="validationModelPropertyExpression">The target expression.</param>
        protected void AddPropertyMapping(Expression<Func<TUIModel, object>> uiModelPropertyExpression, Expression<Func<TValidationModel, object>> validationModelPropertyExpression)
        {
            if (uiModelPropertyExpression == null)
            {
                throw new ArgumentNullException("uiModelPropertyExpression");
            }

            if (validationModelPropertyExpression == null)
            {
                throw new ArgumentNullException("validationModelPropertyExpression");
            }

            MemberInfo uiModelPropertyInfo = LinqUtils.GetMemberInfo(uiModelPropertyExpression);
            MemberInfo validationModelPropertyInfo = LinqUtils.GetMemberInfo(validationModelPropertyExpression);

            string uiModelPropertyName = uiModelPropertyInfo.Name;
            string validationModelPropertyName = validationModelPropertyInfo.Name;
           
            m_PropertyMappingsFromUIModelToValidationModel.Add(uiModelPropertyName, new MappingMemberInfo(validationModelPropertyName, validationModelPropertyInfo));
            m_PropertyMappingsFromValidationModelToUIModel.Add(validationModelPropertyName, new MappingMemberInfo(uiModelPropertyName, uiModelPropertyInfo));
        }

        /// <summary>
        /// Transforms the name of the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>The transformed property name.</returns>
        public MappingMemberInfo TransformPropertyNameFromUIModel(string propertyName)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException("propertyName");
            }

            MappingMemberInfo result;
            if (m_PropertyMappingsFromUIModelToValidationModel.TryGetValue(propertyName, out result))
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// Transforms the property name from validation model.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>The property name.</returns>
        public MappingMemberInfo TransformPropertyNameFromValidationModel(string propertyName)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException("propertyName");
            }

            MappingMemberInfo result;
            if (m_PropertyMappingsFromValidationModelToUIModel.TryGetValue(propertyName, out result))
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// Maps the model to validation model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The mapped model.</returns>
        public object MapToValidationModel(object model)
        {
            return MapTo((TUIModel)model);
        }

        /// <summary>
        /// Maps ui model to validation model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The validation model.</returns>
        protected abstract TValidationModel MapTo(TUIModel model);
    }
}