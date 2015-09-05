namespace Labo.Validation.Prototype.Service.Dto
{
    using System;

    public class CustomerInsertDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string ConfirmEmail { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
