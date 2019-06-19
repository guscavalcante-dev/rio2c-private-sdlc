using System.Collections.Generic;

namespace PlataformaRio2C.Infra.CrossCutting.Identity.ViewModels
{
    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}
