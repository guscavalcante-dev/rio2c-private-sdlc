//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class ProducerDetailAppViewModel : ProducerBasicAppViewModel
//    {
//        #region props

//        [Display(Name = "Executives", ResourceType = typeof(Labels))]
//        public IEnumerable<CollaboratorBasicDetailAppViewModel> Collaborators { get; set; }

//        public IEnumerable<CollaboratorOptionMessageAppViewModel> Executives { get; set; }

//        [Display(Name = "TargetAudience", ResourceType = typeof(Labels))]
//        public IEnumerable<string> TargetAudience { get; set; }


//        [Display(Name = "Activity", ResourceType = typeof(Labels))]
//        public IEnumerable<string> Activitys { get; set; }

//        [Display(Name = "Image", ResourceType = typeof(Labels))]
//        public ImageFile Image { get; set; }

//        public string Description { get; set; }
//        #endregion

//        #region ctor

//        public ProducerDetailAppViewModel()
//            :base()
//        {

//        }

//        public ProducerDetailAppViewModel(Producer entity)
//            :base(entity)
//        {
//            if (entity.EventsCollaborators != null && entity.EventsCollaborators.Any())
//            {
//                Collaborators = CollaboratorBasicDetailAppViewModel.MapList(entity.EventsCollaborators.Select(e => e.Collaborator));
//                Executives = CollaboratorOptionMessageAppViewModel.MapList(entity.EventsCollaborators.Select(e => e.Collaborator));
//            }

//            if (entity.ProducerActivitys != null && entity.ProducerActivitys.Any())
//            {
//                Activitys = entity.ProducerActivitys.Select(e => e.Activity.Name);
//            }

//            if (entity.ProducerTargetAudience != null && entity.ProducerTargetAudience.Any())
//            {
//                TargetAudience = entity.ProducerTargetAudience.Select(e => e.TargetAudience.Name);
//            }

//            Description = entity.GetDescription();
//        }

//        #endregion
//    }
//}
