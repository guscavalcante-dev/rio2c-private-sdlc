using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    public class CollaboratorProducerEditAppViewModel : CollaboratorAppViewModel
    {
        public IEnumerable<LanguageAppViewModel> LanguagesOptions { get; set; }

        public IEnumerable<ProducerOptionAppViewModel> ProducersOptions { get; set; }
        

        public CollaboratorProducerEditAppViewModel()
            :base()
        {
            LanguagesOptions = new List<LanguageAppViewModel>();
        }

        public CollaboratorProducerEditAppViewModel(Domain.Entities.Collaborator entity)
            :base(entity)
        {
            //if (entity.Image != null)
            //{
            //    Image = new ImageFileAppViewModel(entity.Image);
            //}
        }
    }
}
