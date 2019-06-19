using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PlataformaRio2C.Application.ViewModels
{
    public class ConferenceEditAppViewModel : ConferenceAppViewModel
    {
        public IEnumerable<LanguageAppViewModel> Languages { get; set; }
        public ConferenceEditAppViewModel()
            :base()
        {
            
        }

        public ConferenceEditAppViewModel(Conference entity)
            :base(entity)
        {
           
        }
    }
}
