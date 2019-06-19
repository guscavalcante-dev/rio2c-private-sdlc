using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PlataformaRio2C.Application.ViewModels.Common;


namespace PlataformaRio2C.Application.ViewModels
{
    public class PlayerCollaboratorDetailAppViewModel
    {
        [Display(Name = "Player", ResourceType = typeof(Labels))]
        public Guid Uid { get; set; }

        [Display(Name = "CompanyName", ResourceType = typeof(Labels))]
        public string Name { get; set; }

        public bool RegisterComplete { get; set; }

        public bool InterestFilled { get; set; }

        [Display(Name = "Image", ResourceType = typeof(Labels))]
        public ImageFileAppViewModel Image { get; set; }

        public PlayerCollaboratorDetailAppViewModel()
        {

        }

        public PlayerCollaboratorDetailAppViewModel(Player entity)
        {
            Uid = entity.Uid;
            Name = entity.Name;

            if (entity.Address != null)
            {
                RegisterComplete = entity.Address.ZipCode != null && !string.IsNullOrWhiteSpace(entity.Address.ZipCode) && entity.CompanyName != null && !string.IsNullOrWhiteSpace(entity.CompanyName);
            }

            InterestFilled = entity.Interests != null && entity.Interests.Any();

            //if (entity.Image != null)
            //{
            //    Image = new ImageFileAppViewModel(entity.Image);
            //}
        }

        public static IEnumerable<PlayerCollaboratorDetailAppViewModel> MapList(IEnumerable<Player> entities)
        {
            foreach (var entity in entities)
            {
                yield return new PlayerCollaboratorDetailAppViewModel(entity);
            }
        }
    }
}
