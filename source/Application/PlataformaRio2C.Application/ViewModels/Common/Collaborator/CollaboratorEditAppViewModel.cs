using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PlataformaRio2C.Application.ViewModels
{
    public class CollaboratorEditAppViewModel : CollaboratorAppViewModel
    {
        #region props

        public IEnumerable<LanguageAppViewModel> LanguagesOptions { get; set; }

        public IEnumerable<PlayerOptionAppViewModel> PlayersOptions { get; set; }
        public IEnumerable<ProducerOptionAppViewModel> ProducersOptions { get; set; }
        public IEnumerable<Country> Countries { get; set; }
        public IEnumerable<State> States { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }



        #endregion

        #region ctor

        public CollaboratorEditAppViewModel()
            :base()
        {
            var 
            LanguagesOptions = new List<LanguageAppViewModel>();
            PlayersOptions = new List<PlayerOptionAppViewModel>();
            ProducersOptions = new List<ProducerOptionAppViewModel>();

            Players = new List<PlayerCollaboratorAppViewModel>() {
                new PlayerCollaboratorAppViewModel() { }
            };


            Producers = new List<ProducerCollaboratorAppViewModel>() {
                new ProducerCollaboratorAppViewModel() { }
            };
        }

        public CollaboratorEditAppViewModel(Collaborator entity)
            : base(entity)
        {
            if (entity.Players != null && entity.Players.Any())
            {
                Players = PlayerCollaboratorAppViewModel.MapList(entity.Players).ToList();
            }
            else
            {
                Players = new List<PlayerCollaboratorAppViewModel>() { new PlayerCollaboratorAppViewModel() { } };
            }


            if (entity.ProducersEvents != null && entity.ProducersEvents.Any())
            {
                Producers = ProducerCollaboratorAppViewModel.MapList(entity.ProducersEvents.Select(e => e.Producer)).ToList();
            }
            else
            {
                Producers = new List<ProducerCollaboratorAppViewModel>() { new ProducerCollaboratorAppViewModel() { } };
            }

            if (entity.Image != null)
            {
                Image = new ImageFileAppViewModel(entity.Image);
            }
        }

        #endregion

        #region Public methods       



        #endregion
    }
}
