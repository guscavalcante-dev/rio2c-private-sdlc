using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    public class CollaboratorDetailAppViewModel : CollaboratorBasicAppViewModel
    {
        public IEnumerable<PlayerCollaboratorDetailAppViewModel> Players { get; set; }
        public IEnumerable<ProducerCollaboratorDetailAppViewModel> Producers { get; set; }
        public IEnumerable<Country> Countries { get; set; }

        public CollaboratorDetailAppViewModel()
            :base()
        {

        }

        public CollaboratorDetailAppViewModel(Collaborator entity)
            : base(entity)
        {
            if (entity.Players != null && entity.Players.Any())
            {
                Players = PlayerCollaboratorDetailAppViewModel.MapList(entity.Players);
            }

            if (entity.ProducersEvents != null && entity.ProducersEvents.Any())
            {
                Producers = ProducerCollaboratorDetailAppViewModel.MapList(entity.ProducersEvents.Select(e => e.Producer));
            }            
        }
    }
}
