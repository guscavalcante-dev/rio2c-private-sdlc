// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-07-2019
// ***********************************************************************
// <copyright file="ProjectBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>ProjectBaseCommand</summary>
    public class ProjectBaseCommand : BaseCommand
    {
        public List<ProjectTitleBaseCommand> Titles { get; set; }

        public List<ProjectLogLineBaseCommand> LogLines { get; set; }

        public List<ProjectSummaryBaseCommand> Summaries { get; set; }

        [Display(Name = "NumberOfEpisodes", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? NumberOfEpisodes { get; set; }

        [Display(Name = "EachEpisodePlayingTime", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string EachEpisodePlayingTime { get; set; }

        public List<Guid> InterestsUids { get; set; }

        public List<Guid> TargetAudiencesUids { get; set; }

        public List<ProjectProductPlanBaseCommand> ProductPlans { get; set; }

        [Display(Name = "ValuePerEpisode", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string ValuePerEpisode { get; set; }

        [Display(Name = "TotalValueOfProject", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string TotalValueOfProject { get; set; }

        [Display(Name = "ValueAlreadyRaised", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string ValueAlreadyRaised { get; set; }

        [Display(Name = "ValueStillNeeded", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string ValueStillNeeded { get; set; }

        [Display(Name = "ImageLinks", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(3000, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string ImageLinks { get; set; }

        [Display(Name = "TeaserLinks", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(3000, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string TeaserLinks { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "SelectAnOption")]
        public bool? IsPitching { get; set; }

        public List<ProjectAdditionalInformationBaseCommand> AdditionalInformations { get; set; }

        public List<Activity> Activities { get; private set; }
        public List<TargetAudience> TargetAudiences { get; private set; }
        public List<IGrouping<InterestGroup, Interest>> GroupedInterests { get; private set; }

        public Guid? AttendeeOrganizationUid { get; private set; }
        public Guid ProjectTypeUid { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="OrganizationBaseCommand"/> class.</summary>
        public ProjectBaseCommand()
        {
        }

        /// <summary>Updates the base properties.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="activities">The activities.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="groupedInterests">The grouped interests.</param>
        /// <param name="isDataRequired">if set to <c>true</c> [is data required].</param>
        public void UpdateBaseProperties(
            ProjectDto entity, 
            List<LanguageDto> languagesDtos, 
            List<Activity> activities,
            List<TargetAudience> targetAudiences,
            List<IGrouping<InterestGroup, Interest>> groupedInterests,
            bool isDataRequired)
        {
            this.NumberOfEpisodes = entity?.Project?.NumberOfEpisodes;
            this.EachEpisodePlayingTime = entity?.Project?.EachEpisodePlayingTime;
            this.ValuePerEpisode = entity?.Project?.ValuePerEpisode;
            this.TotalValueOfProject = entity?.Project?.TotalValueOfProject;
            this.ValueAlreadyRaised = entity?.Project?.ValueAlreadyRaised;
            this.ValueStillNeeded = entity?.Project?.ValueStillNeeded;
            this.ImageLinks = entity?.Project?.ImageLinks?.FirstOrDefault()?.Value;
            this.TeaserLinks = entity?.Project?.TeaserLinks?.FirstOrDefault()?.Value;
            this.IsPitching= entity?.Project?.Pitching;

            this.UpdateTitles(entity, languagesDtos, isDataRequired);
            this.UpdateLogLines(entity, languagesDtos, isDataRequired);
            this.UpdateSummaries(entity, languagesDtos, isDataRequired);
            this.UpdateProductionPlans(entity, languagesDtos, isDataRequired);
            this.UpdateAdditionalInformations(entity, languagesDtos, isDataRequired);
            //this.UpdateRestrictionSpecifics(entity, languagesDtos, isRestrictionSpecificRequired);
            //this.UpdateActivities(entity, activities);
            //this.UpdateDropdownProperties(holdingBaseDtos, countriesBaseDtos, activities, targetAudiences, groupedInterests);
            //this.TargetAudiencesUids = entity?.OrganizationTargetAudiencesDtos?.Select(otad => otad.TargetAudienceUid)?.ToList();
            this.InterestsUids = entity?.ProjectInterestDtos?.Select(pid => pid.Interest.Uid)?.ToList();

            this.UpdateDropdownProperties(activities, targetAudiences, groupedInterests);
        }

        /// <summary>Updates the titles.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isDataRequired">if set to <c>true</c> [is data required].</param>
        private void UpdateTitles(ProjectDto entity, List<LanguageDto> languagesDtos, bool isDataRequired)
        {
            this.Titles = new List<ProjectTitleBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                var title = entity?.ProjectTitleDtos?.FirstOrDefault(ptd => ptd.Language.Code == languageDto.Code);
                this.Titles.Add(title != null ? new ProjectTitleBaseCommand(title, isDataRequired) : 
                                                new ProjectTitleBaseCommand(languageDto, isDataRequired));
            }
        }

        /// <summary>Updates the log lines.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isDataRequired">if set to <c>true</c> [is data required].</param>
        private void UpdateLogLines(ProjectDto entity, List<LanguageDto> languagesDtos, bool isDataRequired)
        {
            this.LogLines = new List<ProjectLogLineBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                var logLine = entity?.ProjectLogLineDtos?.FirstOrDefault(ptd => ptd.Language.Code == languageDto.Code);
                this.LogLines.Add(logLine != null ? new ProjectLogLineBaseCommand(logLine, isDataRequired) :
                                                    new ProjectLogLineBaseCommand(languageDto, isDataRequired));
            }
        }

        /// <summary>Updates the summaries.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isDataRequired">if set to <c>true</c> [is data required].</param>
        private void UpdateSummaries(ProjectDto entity, List<LanguageDto> languagesDtos, bool isDataRequired)
        {
            this.Summaries = new List<ProjectSummaryBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                var summary = entity?.ProjectSummaryDtos?.FirstOrDefault(ptd => ptd.Language.Code == languageDto.Code);
                this.Summaries.Add(summary != null ? new ProjectSummaryBaseCommand(summary, isDataRequired) :
                                                     new ProjectSummaryBaseCommand(languageDto, isDataRequired));
            }
        }

        /// <summary>Updates the production plans.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isDataRequired">if set to <c>true</c> [is data required].</param>
        private void UpdateProductionPlans(ProjectDto entity, List<LanguageDto> languagesDtos, bool isDataRequired)
        {
            this.ProductPlans = new List<ProjectProductPlanBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                var productionPlan = entity?.ProjectProductionPlanDtos?.FirstOrDefault(ptd => ptd.Language.Code == languageDto.Code);
                this.ProductPlans.Add(productionPlan != null ? new ProjectProductPlanBaseCommand(productionPlan, isDataRequired) :
                                                               new ProjectProductPlanBaseCommand(languageDto, isDataRequired));
            }
        }

        /// <summary>Updates the additional informations.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isDataRequired">if set to <c>true</c> [is data required].</param>
        private void UpdateAdditionalInformations(ProjectDto entity, List<LanguageDto> languagesDtos, bool isDataRequired)
        {
            this.AdditionalInformations = new List<ProjectAdditionalInformationBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                var additionalInformation = entity?.ProjectAdditionalInformationDtos?.FirstOrDefault(ptd => ptd.Language.Code == languageDto.Code);
                this.AdditionalInformations.Add(additionalInformation != null ? new ProjectAdditionalInformationBaseCommand(additionalInformation, isDataRequired) :
                                                                                new ProjectAdditionalInformationBaseCommand(languageDto, isDataRequired));
            }
        }
        //private void UpdateRestrictionSpecifics(OrganizationDto entity, List<LanguageDto> languagesDtos, bool isRestrictionSpecificRequired)
        //{
        //    this.RestrictionSpecifics = new List<OrganizationRestrictionSpecificsBaseCommand>();
        //    foreach (var languageDto in languagesDtos)
        //    {
        //        var restrictionSpecific = entity?.RestrictionSpecificsDtos?.FirstOrDefault(d => d.LanguageDto.Code == languageDto.Code);
        //        this.RestrictionSpecifics.Add(restrictionSpecific != null ? new OrganizationRestrictionSpecificsBaseCommand(restrictionSpecific, isRestrictionSpecificRequired) :
        //                                                                    new OrganizationRestrictionSpecificsBaseCommand(languageDto, isRestrictionSpecificRequired));
        //    }
        //}

        //private void UpdateActivities(OrganizationDto entity, List<Activity> activities)
        //{
        //    this.OrganizationActivities = new List<OrganizationActivityBaseCommand>();
        //    foreach (var activity in activities)
        //    {
        //        var organizationActivity = entity?.OrganizationActivitiesDtos?.FirstOrDefault(oad => oad.ActivityUid == activity.Uid);
        //        this.OrganizationActivities.Add(organizationActivity != null ? new OrganizationActivityBaseCommand(organizationActivity) :
        //                                                                       new OrganizationActivityBaseCommand(activity));
        //    }
        //}

        /// <summary>Updates the dropdown properties.</summary>
        /// <param name="activities">The activities.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="groupedInterests">The grouped interests.</param>
        public void UpdateDropdownProperties(
            List<Activity> activities,
            List<TargetAudience> targetAudiences,
            List<IGrouping<InterestGroup, Interest>> groupedInterests)
        {
            this.Activities = activities;
            this.TargetAudiences = targetAudiences;
            this.GroupedInterests = groupedInterests;
        }

        /// <summary>Updates the pre send properties.</summary>
        /// <param name="attendeeOrganizationUid">The attendee organization uid.</param>
        /// <param name="projectTypeUid">The project type uid.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdatePreSendProperties(
            Guid? attendeeOrganizationUid,
            Guid projectTypeUid,
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage)
        {
            this.AttendeeOrganizationUid = attendeeOrganizationUid;
            this.ProjectTypeUid = projectTypeUid;
            this.UpdatePreSendProperties(userId, userUid, editionId, editionUid, UserInterfaceLanguage);
        }
    }
}