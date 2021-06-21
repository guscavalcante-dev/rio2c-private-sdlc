// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-21-2021
// ***********************************************************************
// <copyright file="UpdateProjectMainInformation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Foolproof;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateProjectMainInformation</summary>
    public class UpdateProjectMainInformation : BaseCommand
    {
        public Guid? ProjectUid { get; set; }

        public List<ProjectTitleBaseCommand> Titles { get; set; }

        public List<ProjectLogLineBaseCommand> LogLines { get; set; }

        public List<ProjectSummaryBaseCommand> Summaries { get; set; }

        [Display(Name = "TotalPlayingTime", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string TotalPlayingTime { get; set; }

        [Display(Name = "NumberOfEpisodes", ResourceType = typeof(Labels))]
        public int? NumberOfEpisodes { get; set; }

        [Display(Name = "EachEpisodePlayingTime", ResourceType = typeof(Labels))]
        [RequiredIfNotEmpty("NumberOfEpisodes", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string EachEpisodePlayingTime { get; set; }

        public List<ProjectProductionPlanBaseCommand> ProductPlans { get; set; }

        [Display(Name = "ValuePerEpisode", ResourceType = typeof(Labels))]
        //[RequiredIfNotEmpty("NumberOfEpisodes", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string ValuePerEpisode { get; set; }

        [Display(Name = "TotalValueOfProject", ResourceType = typeof(Labels))]
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string TotalValueOfProject { get; set; }

        [Display(Name = "ValueAlreadyRaised", ResourceType = typeof(Labels))]
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string ValueAlreadyRaised { get; set; }

        [Display(Name = "ValueStillNeeded", ResourceType = typeof(Labels))]
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string ValueStillNeeded { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "SelectAnOption")]
        public bool? IsPitching { get; set; }

        public List<ProjectAdditionalInformationBaseCommand> AdditionalInformations { get; set; }

        public Guid ProjectTypeUid { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateProjectMainInformation"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isDataRequired">if set to <c>true</c> [is data required].</param>
        /// <param name="isProductionPlanRequired">if set to <c>true</c> [is production plan required].</param>
        /// <param name="isAdditionalInformationRequired">if set to <c>true</c> [is additional information required].</param>
        public UpdateProjectMainInformation(
            ProjectDto entity,
            List<LanguageDto> languagesDtos,
            bool isDataRequired,
            bool isProductionPlanRequired,
            bool isAdditionalInformationRequired)
        {
            this.ProjectUid = entity?.Project?.Uid;
            this.TotalPlayingTime = entity?.Project?.TotalPlayingTime;
            this.NumberOfEpisodes = entity?.Project?.NumberOfEpisodes;
            this.EachEpisodePlayingTime = entity?.Project?.EachEpisodePlayingTime;
            this.ValuePerEpisode = entity?.Project?.ValuePerEpisode;
            this.TotalValueOfProject = entity?.Project?.TotalValueOfProject;
            this.ValueAlreadyRaised = entity?.Project?.ValueAlreadyRaised;
            this.ValueStillNeeded = entity?.Project?.ValueStillNeeded;
            this.IsPitching = entity?.Project?.IsPitching;

            this.UpdateTitles(entity, languagesDtos, isDataRequired);
            this.UpdateLogLines(entity, languagesDtos, isDataRequired);
            this.UpdateSummaries(entity, languagesDtos, isDataRequired);
            this.UpdateProductionPlans(entity, languagesDtos, isProductionPlanRequired);
            this.UpdateAdditionalInformations(entity, languagesDtos, isAdditionalInformationRequired);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateProjectMainInformation"/> class.</summary>
        public UpdateProjectMainInformation()
        {
        }

        /// <summary>
        /// Updates the pre send properties.
        /// </summary>
        /// <param name="projectTypeUid">The project type uid.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <param name="isAdmin">if set to <c>true</c> [is admin].</param>
        public void UpdatePreSendProperties(
            Guid projectTypeUid,
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage,
            bool? isAdmin = false)
        {
            this.ProjectTypeUid = projectTypeUid;
            this.UpdatePreSendProperties(userId, userUid, editionId, editionUid, UserInterfaceLanguage, isAdmin);
        }

        #region Private Methods

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

        #endregion
    }
}