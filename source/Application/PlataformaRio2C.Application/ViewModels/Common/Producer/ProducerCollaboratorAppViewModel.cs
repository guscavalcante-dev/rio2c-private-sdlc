using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.ViewModels
{
    public class ProducerCollaboratorAppViewModel
    {
        [Display(Name = "Player", ResourceType = typeof(Labels))]
        public Guid Uid { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Labels))]
        public string Name { get; set; }

        public bool RegisterComplete { get; set; }

        public ProducerCollaboratorAppViewModel()
        {

        }

        public ProducerCollaboratorAppViewModel(Producer entity)
        {
            Uid = entity.Uid;

            Name = entity.Name;

            if (entity.Address != null)
            {
                //RegisterComplete = entity.Address.ZipCode != null && !string.IsNullOrWhiteSpace(entity.Address.ZipCode);
            }
        }

        public static IEnumerable<ProducerCollaboratorAppViewModel> MapList(IEnumerable<Producer> entities)
        {
            foreach (var entity in entities)
            {
                yield return new ProducerCollaboratorAppViewModel(entity);
            }
        }
    }
}
