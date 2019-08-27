using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    public class MailCollaboratorListItemAppViewModel : MailAppViewModel
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

        public MailCollaboratorListItemAppViewModel()
        {

        }

        public MailCollaboratorListItemAppViewModel(Collaborator collaborator, DateTime sendDate)
        //: base(collaborator)
        {
            Name = collaborator.FirstName;
            SendDate = sendDate;

            if (collaborator.User != null)
            {
                Email = collaborator.User.Email;
            }

            //if (collaborator.Players != null && collaborator.Players.Any())
            //{
            //    PlayersName = string.Join(", ", collaborator.Players.Select(e => e.Name));
            //}

            //if (collaborator.Players != null && collaborator.Players.Any())
            //{
            //    HoldingsName = string.Join(", ", collaborator.Players.Select(e => e.Holding.Name).Distinct());
            //}

        }
    }
}
