//using PlataformaRio2C.Application.Common;
//using PlataformaRio2C.Domain.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class ProjectPlayerItemListAppViewModel : EntityViewModel<ProjectPlayerItemListAppViewModel, ProjectPlayer>
//    {
//        public string Title { get; set; }

//        public string[] Genres { get; set; }

//        public string ProducerName { get; set; }

//        public Guid ProducerUid { get; set; }

//        public IEnumerable<PlayerProjectStatusAppViewModel> Players { get; set; }

//        public DateTime? DateSending { get; set; }


//        public ProjectPlayerItemListAppViewModel()
//            : base()
//        {

//        }

//        public ProjectPlayerItemListAppViewModel(ProjectPlayer entity)
//            : base(entity)
//        {

//            DateSending = entity.DateSending;

//            Uid = entity.Project.Uid;

//            //if (entity.Project.PlayersRelated != null && entity.Project.PlayersRelated.Any())
//            //{
//            //    Players = PlayerProjectStatusAppViewModel.MapList(entity.Project.PlayersRelated).ToList();
//            //}


//            if (entity.Project.Titles.Any())
//            {
//                var titlePt = entity.Project.Titles.FirstOrDefault(j => j.Language.Code == "PtBr");
//                var titleEn = entity.Project.Titles.FirstOrDefault(j => j.Language.Code == "En");

//                if (titlePt != null && !string.IsNullOrWhiteSpace(titlePt.Value))
//                {
//                    Title = titlePt.Value;
//                }

//                if (titlePt != null && !string.IsNullOrWhiteSpace(titlePt.Value) && titleEn != null && !string.IsNullOrWhiteSpace(titleEn.Value))
//                {
//                    Title = string.Format("{0} | {1}", Title, titleEn.Value);
//                }
//                else if (titleEn != null && !string.IsNullOrWhiteSpace(titleEn.Value))
//                {
//                    Title = titleEn.Value;
//                }

//            }

//            if (entity.Project.Interests != null)
//            {
//                var genres = entity.Project.Interests.Where(e => e.Interest.InterestGroup.Name.Contains("Gênero")).Select(e => e.Interest.Name);

//                if (genres != null)
//                {
//                    Genres = genres.ToArray();
//                }
//            }

//            //if (entity.Project.Producer != null)
//            //{
//            //    ProducerName = entity.Project.Producer.Name;
//            //    ProducerUid = entity.Project.Producer.Uid;
//            //}
//        }
//    }

//}
