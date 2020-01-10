//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class PlayerDetailAppViewModel: PlayerBasicAppViewModel
//    {
//        #region props

//        [Display(Name = "Executives", ResourceType = typeof(Labels))]
//        public IEnumerable<CollaboratorBasicDetailAppViewModel> Collaborators { get; set; }

//        #endregion

//        #region ctor

//        public PlayerDetailAppViewModel()
//            :base()
//        {

//        }

//        public PlayerDetailAppViewModel(Player entity)
//            :base(entity)
//        {
//            //if (entity.Collaborators != null && entity.Collaborators.Any())
//            //{
//            //    Collaborators = CollaboratorBasicDetailAppViewModel.MapList(entity.Collaborators);
//            //}
//        }

//        #endregion
//    }
//}
