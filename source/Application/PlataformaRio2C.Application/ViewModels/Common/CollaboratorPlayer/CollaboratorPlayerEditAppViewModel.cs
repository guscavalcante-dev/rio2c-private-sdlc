//using PlataformaRio2C.Domain.Entities;
//using System.Collections.Generic;
//using System.Linq;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class CollaboratorPlayerEditAppViewModel : CollaboratorBasicAppViewModel
//    {
//        public IEnumerable<LanguageAppViewModel> LanguagesOptions { get; set; }

//        public IEnumerable<PlayerOptionAppViewModel> PlayersOptions { get; set; }

//        public IEnumerable<PlayerCollaboratorAppViewModel> Players { get; set; }

//        public IEnumerable<Country> Countries { get; set; }
//        public IEnumerable<State> States { get; set; }
//        public IEnumerable<City> Cities { get; set; }
//        public int CountryId { get; set; }
//        public int StateId { get; set; }
//        public int CityId { get; set; }

//        public CollaboratorPlayerEditAppViewModel()
//            : base()
//        {
//            LanguagesOptions = new List<LanguageAppViewModel>();
//            PlayersOptions = new List<PlayerOptionAppViewModel>();
           
//            Players = new List<PlayerCollaboratorAppViewModel>() {
//                new PlayerCollaboratorAppViewModel() { }
//            };          
//        }

//        public CollaboratorPlayerEditAppViewModel(Collaborator entity)
//            : base(entity)
//        {
//            //if (entity.Players != null && entity.Players.Any())
//            //{
//            //    Players = PlayerCollaboratorAppViewModel.MapList(entity.Players).ToList();
//            //}
//            //else
//            //{
//            //    Players = new List<PlayerCollaboratorAppViewModel>() { new PlayerCollaboratorAppViewModel() { } };
//            //}

//            //if (entity.Image != null)
//            //{
//            //    Image = new ImageFileAppViewModel(entity.Image);
//            //}
//        }


//        public override Collaborator MapReverse()
//        {
//            var entity = base.MapReverse();

//            return entity;
//        }

//        public override Collaborator MapReverse(Collaborator entity)
//        {
//            return base.MapReverse(entity);
//        }
//    }
//}
