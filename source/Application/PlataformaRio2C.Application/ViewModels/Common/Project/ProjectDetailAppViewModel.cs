//using PlataformaRio2C.Domain.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class ProjectDetailAppViewModel : ProjectBasicAppViewModel
//    {
//        #region props

//        //public static readonly int SendPlayerCountMax = Project.SendPlayerCountMax;

//        public virtual string Title { get; set; }
//        public string ProducerName { get; set; }
//        public Guid ProducerUid { get; set; }

//        public string[] Platforms { get; set; }

//        public string[] ProjectStatus { get; set; }
//        public string[] LookingFor { get; set; }

//        public string[] Seeking { get; set; }

//        public string[] Formats { get; set; }

//        public string[] Genres { get; set; }

//        public string[] SubGenres { get; set; }
//        public string[] TargetAudience { get; set; }

//        public bool ProjectSubmitted { get; set; }

//        public IEnumerable<string> RelatedPlayers { get; set; }
//        public IEnumerable<PlayerProjectStatusAppViewModel> RelatedPlayersStatus { get; set; }

//        #endregion

//        #region ctor

//        public ProjectDetailAppViewModel()
//            : base()
//        {

//        }

//        public ProjectDetailAppViewModel(Project entity)
//            : base(entity)
//        {
//            if (entity != null)
//            {
//                Uid = entity.Uid;

//                if (entity.ProjectTitles.Any())
//                {
//                    var titlePt = entity.ProjectTitles.FirstOrDefault(e => e.Language.Code == "PtBr");
//                    var titleEn = entity.ProjectTitles.FirstOrDefault(e => e.Language.Code == "En");

//                    if (titlePt != null && titleEn != null)
//                    {
//                        Title = string.Format("{0} | {1}", titlePt.Value, titleEn.Value);
//                    }
//                }

//                if (entity.ProjectSummaries.Any())
//                {
//                    Summaries = ProjectSummaryAppViewModel.MapList(entity.ProjectSummaries);
//                }

//                if (entity.ProjectInterests != null)
//                {
//                    Platforms = GetInterestsName(entity.ProjectInterests, "Platforms");
//                    ProjectStatus = GetInterestsName(entity.ProjectInterests, "Status");
//                    LookingFor = GetInterestsName(entity.ProjectInterests, "Looking For");                    
//                    Seeking = GetInterestsName(entity.ProjectInterests, "Seeking");
//                    Formats = GetInterestsName(entity.ProjectInterests, "Format");
//                    Genres = GetInterestsName(entity.ProjectInterests, "Gênero");
//                    SubGenres = GetInterestsName(entity.ProjectInterests, "Sub-genre");
//                    TargetAudience = GetInterestsName(entity.ProjectInterests, "Target audience");
//                }

//                //if (entity.PlayersRelated != null && entity.PlayersRelated.Any())
//                //{
//                //    RelatedPlayers = entity.PlayersRelated.Select(e => e.Player.Uid.ToString());
//                //    ProjectSubmitted = entity.PlayersRelated.Any(e => e.Sent);

//                //    RelatedPlayersStatus = PlayerProjectStatusAppViewModel.MapList(entity.PlayersRelated);
//                //}

//                //if (entity.Producer != null)
//                //{
//                //    ProducerName = entity.Producer.Name;
//                //    ProducerUid = entity.Producer.Uid;
//                //}
//            }
//        }

//        #endregion

//        #region Public methods
//        public string[] GetInterestsName(IEnumerable<ProjectInterest> interests, string nameGroup)
//        {
//            if (interests != null)
//            {
//                var formats = interests.Where(e => e.Interest.InterestGroup.Name.Contains(nameGroup)).Select(e => e.Interest.Name);

//                if (formats != null)
//                {
//                    return formats.ToArray();
//                }
//            }

//            return new string[] { };
//        }
//        #endregion


//    }
//}


