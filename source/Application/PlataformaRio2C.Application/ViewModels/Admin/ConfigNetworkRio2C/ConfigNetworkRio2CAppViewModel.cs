using System.Web.Mvc;

namespace PlataformaRio2C.Application.ViewModels.Admin.ConfigNetworkRio2C
{
    [Authorize(Roles = "Administrator")]
    public class ConfigNetworkRio2CAppViewModel
    {
        public string[] Emails { get; set; }

        public ConfigNetworkRio2CAppViewModel()
        {

        }

        public ConfigNetworkRio2CAppViewModel(string valueConfig)
        {
            if (!string.IsNullOrWhiteSpace(valueConfig))
            {
                Emails = valueConfig.Split(';');
            }
        }

        public string MapReverse()
        {
            return string.Join(";", Emails);
        }
    }
}
