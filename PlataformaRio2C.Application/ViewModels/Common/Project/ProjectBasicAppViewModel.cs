using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    public class ProjectBasicAppViewModel : EntityViewModel<ProjectBasicAppViewModel, Project>, IEntityViewModel<Project>
    {
        #region props
        [Display(Name = "NumberOfEpisodes", ResourceType = typeof(Labels))]
        public int NumberOfEpisodes { get; set; }

        [Display(Name = "EachEpisodePlayingTime", ResourceType = typeof(Labels))]
        public string EachEpisodePlayingTime { get; set; }

        [Display(Name = "ValuePerEpisode", ResourceType = typeof(Labels))]
        public string ValuePerEpisode { get; set; }

        [Display(Name = "TotalValueOfProject", ResourceType = typeof(Labels))]
        public string TotalValueOfProject { get; set; }

        [Display(Name = "ValueAlreadyRaised", ResourceType = typeof(Labels))]
        public string ValueAlreadyRaised { get; set; }

        [Display(Name = "ValueStillNeeded", ResourceType = typeof(Labels))]
        public string ValueStillNeeded { get; set; }

        public bool? Pitching { get; set; }

        [Display(Name = "Titles", ResourceType = typeof(Labels))]
        public virtual IEnumerable<ProjectTitleAppViewModel> Titles { get; set; }

        [Display(Name = "LogLines", ResourceType = typeof(Labels))]
        public virtual IEnumerable<ProjectLogLineAppViewModel> LogLines { get; set; }

        [Display(Name = "Summaries", ResourceType = typeof(Labels))]
        public virtual IEnumerable<ProjectSummaryAppViewModel> Summaries { get; set; }

        [Display(Name = "ProductionPlans", ResourceType = typeof(Labels))]
        public virtual IEnumerable<ProjectProductionPlanAppViewModel> ProductionPlans { get; set; }

        [Display(Name = "LinksImage", ResourceType = typeof(Labels))]
        public virtual IEnumerable<ProjectLinkImageAppViewModel> LinksImage { get; set; }

        [Display(Name = "LinksTeaser", ResourceType = typeof(Labels))]
        public virtual IEnumerable<ProjectLinkTeaserAppViewModel> LinksTeaser { get; set; }

        [Display(Name = "AdditionalInformations", ResourceType = typeof(Labels))]
        public virtual IEnumerable<ProjectAdditionalInformationAppViewModel> AdditionalInformations { get; set; }

        [Display(Name = "Interests", ResourceType = typeof(Labels))]
        public virtual IEnumerable<ProjectInterestAppViewModel> Interests { get; set; }

        #endregion

        #region ctor

        public ProjectBasicAppViewModel()
            :base()
        {

        }

        public ProjectBasicAppViewModel(Project entity)
            : base(entity)
        {
            NumberOfEpisodes = entity.NumberOfEpisodes;
            EachEpisodePlayingTime = entity.EachEpisodePlayingTime;
            ValuePerEpisode = entity.ValuePerEpisode;
            TotalValueOfProject = entity.TotalValueOfProject;
            ValueAlreadyRaised = entity.ValueAlreadyRaised;
            ValueStillNeeded = entity.ValueStillNeeded;
            Pitching = entity.Pitching;

            if (entity.Titles != null && entity.Titles.Any())
            {
                Titles = ProjectTitleAppViewModel.MapList(entity.Titles);
            }

            if (entity.LogLines != null && entity.LogLines.Any())
            {
                LogLines = ProjectLogLineAppViewModel.MapList(entity.LogLines);
            }

            if (entity.Summaries != null && entity.Summaries.Any())
            {
                Summaries = ProjectSummaryAppViewModel.MapList(entity.Summaries);
            }

            if (entity.ProductionPlans != null && entity.ProductionPlans.Any())
            {
                ProductionPlans = ProjectProductionPlanAppViewModel.MapList(entity.ProductionPlans);
            }

            if (entity.LinksImage != null && entity.LinksImage.Any())
            {
                LinksImage = ProjectLinkImageAppViewModel.MapList(entity.LinksImage);
            }

            if (entity.LinksTeaser != null && entity.LinksTeaser.Any())
            {
                LinksTeaser = ProjectLinkTeaserAppViewModel.MapList(entity.LinksTeaser);
            }

            if (entity.AdditionalInformations != null && entity.AdditionalInformations.Any())
            {
                AdditionalInformations = ProjectAdditionalInformationAppViewModel.MapList(entity.AdditionalInformations);
            }
        }

        #endregion

        #region Public methods

        public Project MapReverse()
        {
            var entity = new Project(null);

            entity.SetNumberOfEpisodes(NumberOfEpisodes);
            entity.SetEachEpisodePlayingTime(EachEpisodePlayingTime);
            entity.SetValuePerEpisode(ValuePerEpisode);
            entity.SetTotalValueOfProject(TotalValueOfProject);
            entity.SetValueAlreadyRaised(ValueAlreadyRaised);
            entity.SetValueStillNeeded(ValueStillNeeded);
            entity.SetPitching(Pitching);

            return entity;
        }

        public Project MapReverse(Project entity)
        {
            entity.SetNumberOfEpisodes(NumberOfEpisodes);
            entity.SetEachEpisodePlayingTime(EachEpisodePlayingTime);
            entity.SetValuePerEpisode(ValuePerEpisode);
            entity.SetTotalValueOfProject(TotalValueOfProject);
            entity.SetValueAlreadyRaised(ValueAlreadyRaised);
            entity.SetValueStillNeeded(ValueStillNeeded);
            entity.SetPitching(Pitching);

            return entity;
        }

        #endregion
    }
}
