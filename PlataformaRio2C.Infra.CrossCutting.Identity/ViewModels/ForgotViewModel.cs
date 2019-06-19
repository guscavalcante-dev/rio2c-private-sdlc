using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Infra.CrossCutting.Identity.ViewModels
{
    public class ForgotViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
