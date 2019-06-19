using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Infra.CrossCutting.Identity.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email", ResourceType = typeof(Labels))]
        public string Email { get; set; }
    }
}
