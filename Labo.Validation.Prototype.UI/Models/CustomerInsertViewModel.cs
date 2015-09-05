namespace Labo.Validation.Prototype.UI.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [Serializable]
    public class CustomerInsertViewModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Confirm Email Address")]
        public string ConfirmEmailAddress { get; set; }
    }
}