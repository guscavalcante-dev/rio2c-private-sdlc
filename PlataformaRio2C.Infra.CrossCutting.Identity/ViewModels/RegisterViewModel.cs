using System.ComponentModel.DataAnnotations;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Infra.CrossCutting.Identity.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(Messages), ErrorMessage = "MinimumNumberOfCharacters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessageResourceType = typeof(Messages), ErrorMessage = "PasswordConfirmationDoesNotMatch")]
        public string ConfirmPassword { get; set; }
    }
}
