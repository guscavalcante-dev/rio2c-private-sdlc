using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.ViewModels
{
    public class LogisticsEditAppViewModel : LogisticsAppViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [Display(Name = "Executive", ResourceType = typeof(Labels))]
        public Guid CollaboratorUid { get; set; }
        public IEnumerable<LogisticsCollaboratorAppViewModel> CollaboratorOptions { get; set; }


        public LogisticsEditAppViewModel()
            :base()
        {

        }

        public LogisticsEditAppViewModel(Logistics entity)
            :base(entity)
        {
            if (entity.Collaborator != null)
            {
                CollaboratorUid = entity.Collaborator.Uid;
            }
        }
    }
}
