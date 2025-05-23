﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-06-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-13-2025
// ***********************************************************************
// <copyright file="ProjectBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Foolproof;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>ProjectBaseCommand</summary>
    public class ProjectBaseCommand : BaseCommand
    {
        public List<ProjectTitleBaseCommand> Titles { get; set; }

        public List<ProjectLogLineBaseCommand> LogLines { get; set; }

        public List<ProjectSummaryBaseCommand> Summaries { get; set; }

        public InterestBaseCommand[][] Interests { get; set; }

        [Display(Name = "TotalPlayingTime", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string TotalPlayingTime { get; set; }

        [Display(Name = "NumberOfEpisodes", ResourceType = typeof(Labels))]
        public int? NumberOfEpisodes { get; set; }

        [Display(Name = "EachEpisodePlayingTime", ResourceType = typeof(Labels))]
        [RequiredIfNotEmpty("NumberOfEpisodes", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string EachEpisodePlayingTime { get; set; }

        public List<Guid> TargetAudiencesUids { get; set; }

        public List<ProjectProductionPlanBaseCommand> ProductPlans { get; set; }

        [Display(Name = "TotalValueOfProject", ResourceType = typeof(Labels))]
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string TotalValueOfProject { get; set; }

        [Display(Name = "ValuePerEpisode", ResourceType = typeof(Labels))]
        //[RequiredIfNotEmpty("NumberOfEpisodes", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string ValuePerEpisode { get; set; }

        [Display(Name = "ValueAlreadyRaised", ResourceType = typeof(Labels))]
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string ValueAlreadyRaised { get; set; }

        [Display(Name = "ValueStillNeeded", ResourceType = typeof(Labels))]
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string ValueStillNeeded { get; set; }

        [Display(Name = "LinksToImageOrConceptualLayout", ResourceType = typeof(Labels))]
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(3000, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string ImageLinks { get; set; }

        [Display(Name = "LinksForPromoTeaser", ResourceType = typeof(Labels))]
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(3000, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string TeaserLinks { get; set; }

        public List<ProjectAdditionalInformationBaseCommand> AdditionalInformations { get; set; }

        public List<TargetAudience> TargetAudiences { get; private set; }

        public Guid? AttendeeOrganizationUid { get; private set; }
        public Guid ProjectTypeUid { get; private set; }
        public Guid ProjectModalityUid { get; private set; }

        public int ProjectMaxCount { get; private set; }

        public bool ProjectModalityRequired { get; private set; }

        #region Type of Financing

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public bool HasAnyTypeOfFinancing { get; set; }

        [Display(Name = "WhichTypeOfFinancing", ResourceType = typeof(Labels))]
        [RequiredIf("HasAnyTypeOfFinancing", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(300, MinimumLength = 0, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string WhichTypeOfFinancingDescription { get; set; }

        #endregion

        /// <summary>Initializes a new instance of the <see cref="ProjectBaseCommand"/> class.</summary>
        public ProjectBaseCommand()
        {
        }

        /// <summary>Updates the base properties.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="interestsDtos">The interests dtos.</param>
        /// <param name="isDataRequired">if set to <c>true</c> [is data required].</param>
        /// <param name="isProductionPlanRequired">if set to <c>true</c> [is production plan required].</param>
        /// <param name="isAdditionalInformationRequired">if set to <c>true</c> [is additional information required].</param>
        /// <param name="modalityRequired">if set to <c>true</c> [is modality required].</param>
        public void UpdateBaseProperties(
            ProjectDto entity,
            List<LanguageDto> languagesDtos,
            List<TargetAudience> targetAudiences,
            List<InterestDto> interestsDtos,
            bool isDataRequired,
            bool isProductionPlanRequired,
            bool isAdditionalInformationRequired,
            string userInterfaceLanguage,
            bool modalityRequired
        )
        {
            this.TotalPlayingTime = entity?.Project?.TotalPlayingTime;
            this.NumberOfEpisodes = entity?.Project?.NumberOfEpisodes;
            this.EachEpisodePlayingTime = entity?.Project?.EachEpisodePlayingTime;
            this.ValuePerEpisode = entity?.Project?.ValuePerEpisode;
            this.TotalValueOfProject = entity?.Project?.TotalValueOfProject;
            this.ValueAlreadyRaised = entity?.Project?.ValueAlreadyRaised;
            this.ValueStillNeeded = entity?.Project?.ValueStillNeeded;
            this.ImageLinks = entity?.Project?.ProjectImageLinks?.FirstOrDefault()?.Value;
            this.TeaserLinks = entity?.Project?.ProjectTeaserLinks?.FirstOrDefault()?.Value;

            this.UpdateTitles(entity, languagesDtos, isDataRequired);
            this.UpdateLogLines(entity, languagesDtos, isDataRequired);
            this.UpdateSummaries(entity, languagesDtos, isDataRequired);
            this.UpdateProductionPlans(entity, languagesDtos, isProductionPlanRequired);
            this.UpdateAdditionalInformations(entity, languagesDtos, isAdditionalInformationRequired);
            this.UpdateInterests(entity, interestsDtos);
            this.TargetAudiencesUids = entity?.ProjectTargetAudienceDtos?.Select(pta => pta.TargetAudience.Uid)?.ToList();

            this.ProjectModalityRequired = modalityRequired;
            this.UpdateDropdownProperties(targetAudiences, userInterfaceLanguage);
        }

        /// <summary>Updates the dropdown properties.</summary>
        /// <param name="targetAudiences">The target audiences.</param>
        public void UpdateDropdownProperties(List<TargetAudience> targetAudiences)
        {
            this.TargetAudiences = targetAudiences;
        }

        /// <summary>Updates the dropdown properties.</summary>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateDropdownProperties(
            List<TargetAudience> targetAudiences,
            string userInterfaceLanguage
        )
        {
            this.TargetAudiences = targetAudiences;
        }

        /// <summary>Updates the pre send properties.</summary>
        /// <param name="attendeeOrganizationUid">The attendee organization uid.</param>
        /// <param name="projectTypeUid">The project type uid.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <param name="projectModalityUid">The project modality uid.</param>
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

        /// <summary>Updates the pre send properties.</summary>
        /// <param name="attendeeOrganizationUid">The attendee organization uid.</param>
        /// <param name="projectTypeUid">The project type uid.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <param name="projectModalityUid">The project modality uid.</param>
        public void UpdatePreSendProperties(
            Guid? attendeeOrganizationUid,
            Guid projectTypeUid,
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage,
            Guid projectModalityUid
        )
        {
            this.AttendeeOrganizationUid = attendeeOrganizationUid;
            this.ProjectTypeUid = projectTypeUid;
            this.ProjectModalityUid = projectModalityUid;
            this.UpdatePreSendProperties(userId, userUid, editionId, editionUid, UserInterfaceLanguage);
        }

        #region Private methods

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
            this.ProductPlans = new List<ProjectProductionPlanBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                var productionPlan = entity?.ProjectProductionPlanDtos?.FirstOrDefault(ptd => ptd.Language.Code == languageDto.Code);
                this.ProductPlans.Add(productionPlan != null ? new ProjectProductionPlanBaseCommand(productionPlan, isDataRequired) :
                                                               new ProjectProductionPlanBaseCommand(languageDto, isDataRequired));
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

        /// <summary>Updates the interests.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="interestsDtos">The interests dtos.</param>
        private void UpdateInterests(ProjectDto entity, List<InterestDto> interestsDtos)
        {
            var interestsBaseCommands = new List<InterestBaseCommand>();
            foreach (var interestDto in interestsDtos)
            {
                var projectInterest = entity?.ProjectInterestDtos?.FirstOrDefault(oad => oad.Interest.Uid == interestDto.Interest.Uid);
                interestsBaseCommands.Add(projectInterest != null ? new InterestBaseCommand(projectInterest) :
                                                                    new InterestBaseCommand(interestDto));
            }

            var groupedInterestsDtos = interestsBaseCommands?
                                            .GroupBy(i => new { i.InterestGroupUid, i.InterestGroupName, i.InterestGroupDisplayOrder })?
                                            .OrderBy(g => g.Key.InterestGroupDisplayOrder)?
                                            .ToList();

            if (groupedInterestsDtos?.Any() == true)
            {
                this.Interests = new InterestBaseCommand[groupedInterestsDtos.Count][];
                for (int i = 0; i < groupedInterestsDtos.Count; i++)
                {
                    this.Interests[i] = groupedInterestsDtos[i].ToArray();
                }
            }
        }

        #endregion
    }
}