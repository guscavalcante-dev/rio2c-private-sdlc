using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Infra.CrossCutting.Identity.ViewModels
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }
}
