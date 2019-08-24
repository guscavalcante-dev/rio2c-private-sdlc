using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.ViewModels
{
    public class ProducerCollaboratorDetailAppViewModel
    {
        [Display(Name = "Player", ResourceType = typeof(Labels))]
        public Guid Uid { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Labels))]
        public string Name { get; set; }

        public bool RegisterComplete { get; set; }

        [Display(Name = "Image", ResourceType = typeof(Labels))]
        public ImageFileAppViewModel Image { get; set; }

        public ProducerCollaboratorDetailAppViewModel()
        {

        }

        public ProducerCollaboratorDetailAppViewModel(Producer entity)
        {
            Uid = entity.Uid;

            Name = entity.Name;

            if (entity.Address != null)
            {
                //RegisterComplete = entity.Address.ZipCode != null && !string.IsNullOrWhiteSpace(entity.Address.ZipCode);
            }

            //if (entity.Image != null)
            //{
            //    Image = new ImageFileAppViewModel(entity.Image);
            //}
        }

        public static IEnumerable<ProducerCollaboratorDetailAppViewModel> MapList(IEnumerable<Producer> entities)
        {
            foreach (var entity in entities)
            {
                yield return new ProducerCollaboratorDetailAppViewModel(entity);
            }
        }
    }
}
