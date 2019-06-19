using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    public class ProjectDetailAppViewModel : ProjectBasicAppViewModel
    {
        #region props

        public static readonly int SendPlayerCountMax = Project.SendPlayerCountMax;

        public virtual string Title { get; set; }
        public string ProducerName { get; set; }
        public Guid ProducerUid { get; set; }

        public string[] Platforms { get; set; }

        public string[] ProjectStatus { get; set; }
        public string[] LookingFor { get; set; }

        public string[] Seeking { get; set; }

        public string[] Formats { get; set; }

        public string[] Genres { get; set; }

        public string[] SubGenres { get; set; }
        public string[] TargetAudience { get; set; }

        public bool ProjectSubmitted { get; set; }

        public IEnumerable<string> RelatedPlayers { get; set; }
        public IEnumerable<PlayerProjectStatusAppViewModel> RelatedPlayersStatus { get; set; }

        #endregion

        #region ctor

        public ProjectDetailAppViewModel()
            : base()
        {

        }

        public ProjectDetailAppViewModel(Project entity)
            : base(entity)
        {
            if (entity != null)
            {
                Uid = entity.Uid;

                if (entity.Titles.Any())
                {
                    var titlePt = entity.Titles.FirstOrDefault(e => e.Language.Code == "PtBr");
                    var titleEn = entity.Titles.FirstOrDefault(e => e.Language.Code == "En");

                    if (titlePt != null && titleEn != null)
                    {
                        Title = string.Format("{0} | {1}", titlePt.Value, titleEn.Value);
                    }
                }

                if (entity.Summaries.Any())
                {
                    Summaries = ProjectSummaryAppViewModel.MapList(entity.Summaries);
                }

                if (entity.Interests != null)
                {
                    Platforms = GetInterestsName(entity.Interests, "Platforms");
                    ProjectStatus = GetInterestsName(entity.Interests, "Status");
                    LookingFor = GetInterestsName(entity.Interests, "Looking For");                    
                    Seeking = GetInterestsName(entity.Interests, "Seeking");
                    Formats = GetInterestsName(entity.Interests, "Format");
                    Genres = GetInterestsName(entity.Interests, "Gênero");
                    SubGenres = GetInterestsName(entity.Interests, "Sub-genre");
                    TargetAudience = GetInterestsName(entity.Interests, "Target audience");
                }

                if (entity.PlayersRelated != null && entity.PlayersRelated.Any())
                {
                    RelatedPlayers = entity.PlayersRelated.Select(e => e.Player.Uid.ToString());
                    ProjectSubmitted = entity.PlayersRelated.Any(e => e.Sent);

                    RelatedPlayersStatus = PlayerProjectStatusAppViewModel.MapList(entity.PlayersRelated);
                }

                if (entity.Producer != null)
                {
                    ProducerName = entity.Producer.Name;
                    ProducerUid = entity.Producer.Uid;
                }
            }
        }

        #endregion

        #region Public methods
        public string[] GetInterestsName(IEnumerable<ProjectInterest> interests, string nameGroup)
        {
            if (interests != null)
            {
                var formats = interests.Where(e => e.Interest.InterestGroup.Name.Contains(nameGroup)).Select(e => e.Interest.Name);

                if (formats != null)
                {
                    return formats.ToArray();
                }
            }

            return new string[] { };
        }
        #endregion


    }
}


