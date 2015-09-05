namespace Labo.Validation.Prototype.Service.Validators
{
    using Labo.Validation.Prototype.Service.Dto;

    public sealed class CustomerInsertDtoValidator : EntityValidatorBase<CustomerInsertDto>
    {
        public CustomerInsertDtoValidator()
        {
            //AddRule(x => x.RuleFor(y => y.FirstName).NotNull().NotEmpty().MaxLength(50));
            AddRule(x => x.RuleFor(y => y.FirstName).NotNull().NotEmpty().SetMessage("Value should not be empty"));
            AddRule(x => x.RuleFor(y => y.FirstName).MaxLength(10).SetMessage("Value is too long"));
            AddRule(x => x.RuleFor(y => y.LastName).NotNull().NotEmpty().MaxLength(50));
            AddRule(x => x.RuleFor(y => y.Email).NotNull().NotEmpty().EmailAddress().MaxLength(256));
            AddRule(x => x.RuleFor(y => y.ConfirmEmail).EqualTo(y => y.Email));
        }
    }
}
