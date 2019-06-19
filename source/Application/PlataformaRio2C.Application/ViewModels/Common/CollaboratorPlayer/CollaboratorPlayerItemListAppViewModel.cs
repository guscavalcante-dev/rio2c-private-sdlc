using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    public class CollaboratorPlayerItemListAppViewModel : EntityViewModel<CollaboratorPlayerItemListAppViewModel, Collaborator>
    {
        [Display(Name = "FullName", ResourceType = typeof(Labels))]
        public string Name { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Labels))]
        public string Email { get; set; }

        [Display(Name = "Holding", ResourceType = typeof(Labels))]
        public string HoldingsName { get; set; }

        [Display(Name = "Player", ResourceType = typeof(Labels))]
        public string PlayersName { get; set; }

        [Display(Name = "SendDate", ResourceType = typeof(Labels))]
        public DateTime? SendDate { get; set; }

        public CollaboratorPlayerItemListAppViewModel()
        {

        }

        public CollaboratorPlayerItemListAppViewModel(Collaborator entity)
            : base(entity)
        {
            Name = entity.Name;

            if (entity.User != null)
            {
                Email = entity.User.Email;
            }

            if (entity.Players != null && entity.Players.Any())
            {
                PlayersName = string.Join(", ", entity.Players.Select(e => e.Name));
            }

            if (entity.Players != null && entity.Players.Any())
            {
                HoldingsName = string.Join(", ", entity.Players.Select(e => e.Holding.Name).Distinct());
            }
        }
    }
}
