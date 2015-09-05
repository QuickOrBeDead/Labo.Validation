namespace Labo.Validation.Prototype.UI.Validation.Transformers
{
    using Labo.Validation.Prototype.Service.Dto;
    using Labo.Validation.Prototype.UI.Models;
    using Labo.Validation.Transform;

    public sealed class CustomerInsertModelToDtoValidationTransformer : ValidationTransformerBase<CustomerInsertViewModel, CustomerInsertDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerInsertModelToDtoValidationTransformer"/> class.
        /// </summary>
        public CustomerInsertModelToDtoValidationTransformer()
        {
            AddPropertyMapping(x => x.Name, x => x.FirstName);
            AddPropertyMapping(x => x.Surname, x => x.LastName);
            AddPropertyMapping(x => x.EmailAddress, x => x.Email);
            AddPropertyMapping(x => x.ConfirmEmailAddress, x => x.ConfirmEmail);
        }

        /// <summary>
        /// Maps ui model to validation model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The validation model.</returns>
        protected override CustomerInsertDto MapTo(CustomerInsertViewModel model)
        {
            return new CustomerInsertDto { FirstName = model.Name, LastName = model.Surname, Email = model.EmailAddress, ConfirmEmail = model.ConfirmEmailAddress };
        }
    }
}