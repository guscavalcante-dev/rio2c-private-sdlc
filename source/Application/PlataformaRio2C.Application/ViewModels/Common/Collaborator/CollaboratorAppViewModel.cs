using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    public class CollaboratorAppViewModel : CollaboratorBasicAppViewModel
    {
        #region props
               
        public IEnumerable<PlayerCollaboratorAppViewModel> Players { get; set; }
        public IEnumerable<ProducerCollaboratorAppViewModel> Producers { get; set; }
        

        #endregion


        #region ctor

        public CollaboratorAppViewModel()
            :base()
        {
            Players = new List<PlayerCollaboratorAppViewModel>() {
                new PlayerCollaboratorAppViewModel() { }
            };

            Producers = new List<ProducerCollaboratorAppViewModel>() {
                new ProducerCollaboratorAppViewModel() { }
            };
        }

        #endregion

        #region Public methods       

        public CollaboratorAppViewModel(Collaborator entity)
            :base(entity)
        {
            //if (entity.Players != null && entity.Players.Any())
            //{
            //    Players = PlayerCollaboratorAppViewModel.MapList(entity.Players).ToList();
            //}
            //else
            //{
            //    Players = new List<PlayerCollaboratorAppViewModel>() { new PlayerCollaboratorAppViewModel() { } };
            //}


            //if (entity.ProducersEvents != null && entity.ProducersEvents.Any())
            //{
            //    Producers = ProducerCollaboratorAppViewModel.MapList(entity.ProducersEvents.Select(e => e.Producer)).ToList();
            //}
            //else
            //{
            //    Producers = new List<ProducerCollaboratorAppViewModel>() { new ProducerCollaboratorAppViewModel() { } };
            //}
        }

        #endregion
    }
}
