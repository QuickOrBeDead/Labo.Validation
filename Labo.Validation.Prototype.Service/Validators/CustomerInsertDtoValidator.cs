namespace Labo.Validation.Prototype.Service.Validators
{
    using Labo.Validation.Prototype.Service.Dto;

    public sealed class CustomerInsertDtoValidator : EntityValidatorBase<CustomerInsertDto>
    {
        public CustomerInsertDtoValidator()
        {
            AddRule(x => x.RuleFor(y => y.FirstName).NotNull().NotEmpty());
            AddRule(x => x.RuleFor(y => y.LastName).NotNull().NotEmpty());
        }
    }
}
