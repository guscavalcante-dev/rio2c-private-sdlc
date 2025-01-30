// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-06-2019
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 01-30-2025
// ***********************************************************************
// <copyright file="MusicBusinessRoundProjectBaseCommand.cs" company="Softo">
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
    public class MusicBusinessRoundProjectBaseCommand : BaseCommand
    {
        public static readonly int PlayerCategoriesThatHaveOrHadContractMaxLength = 300;
        public static readonly int AttachmentUrlMaxLength = 300;

        public Guid? MusicProjectUid { get; set; }
        public int SellerAttendeeCollaboratorId { get; set; }
        public List<Guid> PlayerCategoriesUids { get; set; }

        [Display(Name = "IfAffirmativeWhichCompanies", ResourceType = typeof(Labels))]
        //[RequiredIfNotEmpty("PlayerCategoriesUids", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(300, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string PlayerCategoriesThatHaveOrHadContract { get; set; }

        [Display(Name = "AttachmentUrl", ResourceType = typeof(Labels))]
        [StringLength(300, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string AttachmentUrl { get; set; }
        public List<Guid> TargetAudiencesUids { get; set; }
        public List<Guid> ActivitiesUids { get; set; }
        public Guid? SellerAttendeeCollaboratorUid { get; private set; }
        public int ProjectBuyerEvaluationsCount { get; set; }
        public IEnumerable<ProjectInterestDto> ProjectInterestDtos { get; set; }
        public List<MusicBusinessRoundProjectTargetAudience> MusicBusinessRoundProjectTargetAudience { get; set; }
        public InterestBaseCommand[][] Interests { get; set; }
        //public List<MusicBusinessRoundProjectInterestBaseCommand> MusicBusinessRoundProjectInterests { get; set; }
        public List<TargetAudience> TargetAudiences { get; private set; }
        public List<Activity> Activities { get; private set; }

        //public List<MusicBusinessRoundProjectPlayerCategoryBaseCommand> PlayerCategories { get; set; }
        public List<PlayerCategory> PlayerCategories { get; set; }
        public List<MusicBusinessRoundProjectExpectationsForMeetingBaseCommand> MusicBusinessRoundProjectExpectationsForMeetings { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicBusinessRoundProjectBaseCommand"/> class.</summary>
        public MusicBusinessRoundProjectBaseCommand()
        {
        }

        /// <summary>
        /// Updates the base properties.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="interestsDtos">The interests dtos.</param>
        /// <param name="activities">The activities.</param>
        /// <param name="playersCategories">The players categories.</param>
        /// <param name="isDataRequired">if set to <c>true</c> [is data required].</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateBaseProperties(
            MusicBusinessRoundProjectDto entity,
            List<LanguageDto> languagesDtos,
            List<TargetAudience> targetAudiences,
            List<InterestDto> interestsDtos,
            List<Activity> activities,
            List<PlayerCategory> playersCategories,
            bool isDataRequired,
            string userInterfaceLanguage)
        {
            this.AttachmentUrl = entity?.AttachmentUrl;
            this.PlayerCategoriesThatHaveOrHadContract = entity?.PlayerCategoriesThatHaveOrHadContract;
            
            this.UpdateActivies(entity, activities);
            this.UpdateTargetAudiences(entity,targetAudiences);
            this.UpdateInterests(entity, interestsDtos);
            this.UpdateExpectationsForMeetings(entity, languagesDtos, isDataRequired);
            this.UpdatePlayerCategories(entity, playersCategories);
            this.UpdateDropdownProperties(targetAudiences, activities, playersCategories, userInterfaceLanguage);
        }

        /// <summary>
        /// Updates the base properties.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isDataRequired">if set to <c>true</c> [is data required].</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateBaseProperties(
            MusicBusinessRoundProjectDto entity,
            List<LanguageDto> languagesDtos,
            bool isDataRequired,
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage)
        {
            this.UpdateExpectationsForMeetings(entity, languagesDtos, isDataRequired);
            this.UpdatePreSendProperties(userId, userUid, editionId, editionUid, userInterfaceLanguage);
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
            int collaboratorId,
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage)
        {
            this.SellerAttendeeCollaboratorId = collaboratorId;
            this.UpdatePreSendProperties(userId, userUid, editionId, editionUid, userInterfaceLanguage);
        }

        /// <summary>Updates the dropdown properties.</summary>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateDropdownProperties(
            List<TargetAudience> targetAudiences,
            List<Activity> activities,
            List<PlayerCategory> playersCategories,
            string userInterfaceLanguage)
        {
            this.PlayerCategories = playersCategories;
            this.Activities = activities;
            this.TargetAudiences = targetAudiences;
        }

        /// <summary>Updates the dropdown properties.</summary>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateDropdownProperties(
            List<TargetAudience> targetAudiences,
            string userInterfaceLanguage)
        {
            this.TargetAudiences = targetAudiences;
        }


        /// <summary>Updates expectations for meetings.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isDataRequired">if set to <c>true</c> [is data required].</param>
        private void UpdateExpectationsForMeetings(MusicBusinessRoundProjectDto entity, List<LanguageDto> languagesDtos, bool isDataRequired)
        {
            this.MusicBusinessRoundProjectExpectationsForMeetings = new List<MusicBusinessRoundProjectExpectationsForMeetingBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                var expectationForMeeting = entity?.MusicBusinessRoundProjectExpectationsForMeetingDtos?.FirstOrDefault(ptd => ptd.Language.Code == languageDto.Code);
                this.MusicBusinessRoundProjectExpectationsForMeetings.Add(
                    expectationForMeeting != null
                        ? new MusicBusinessRoundProjectExpectationsForMeetingBaseCommand(expectationForMeeting, isDataRequired)
                        : new MusicBusinessRoundProjectExpectationsForMeetingBaseCommand(languageDto, isDataRequired)
                );
            }
        }

        /// <summary>Updates the interests.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="interestsDtos">The interests dtos.</param>
        private void UpdateInterests(MusicBusinessRoundProjectDto entity, List<InterestDto> interestsDtos)
        {
            var interestsBaseCommands = new List<InterestBaseCommand>();
            foreach (var interestDto in interestsDtos)
            {
                var projectInterest = entity?.MusicBusinessRoundProjectInterestDtos?.FirstOrDefault(oad => oad.Interest.Uid == interestDto.Interest.Uid);
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

       
        /// <summary>
        /// Updates the target audiences.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        public void UpdateTargetAudiences(MusicBusinessRoundProjectDto entity, List<TargetAudience> targetAudiences)
        {
            this.TargetAudiences = new List<TargetAudience>();
            this.TargetAudiencesUids = new List<Guid>();
            foreach (var targetAudience in targetAudiences)
            {
                var musicBusinessRoundProjectTargetAudience = entity?.MusicBusinessRoundProjectTargetAudienceDtos?.FirstOrDefault(ota => ota.TargetAudience.Uid == targetAudience.Uid);
                if (musicBusinessRoundProjectTargetAudience != null)
                {
                    this.TargetAudiences.Add(targetAudience);
                    this.TargetAudiencesUids.Add(targetAudience.Uid);
                }
            }
        }

        /// <summary>
        /// Updates the target audiences.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="activities">The activities of the music project.</param>
        public void UpdateActivies(MusicBusinessRoundProjectDto entity, List<Activity> activities)
        {
            this.Activities = new List<Activity>();
            this.ActivitiesUids = new List<Guid>();
            foreach (var activity in activities)
            {
                var musicBusinessRoundProjectActivities = entity?.MusicBusinessRoundProjectActivityDtos?.FirstOrDefault(ota => ota.Activity.Uid == activity.Uid);
                if (musicBusinessRoundProjectActivities != null)
                {
                    this.Activities.Add(activity);
                    this.ActivitiesUids.Add(activity.Uid);
                }
            }
        }

        /// <summary>
        /// Updates the target audiences.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="playerCategories">The activities of the music project.</param>
        public void UpdatePlayerCategories(MusicBusinessRoundProjectDto entity, List<PlayerCategory> playerCategories)
        {
            this.PlayerCategories = new List<PlayerCategory>();
            this.PlayerCategoriesUids = new List<Guid>();
            foreach (var playerCategory in playerCategories)
            {
                var musicBusinessRoundProjectPlayerCategories = entity?.MusicBusinessRoundProjectPlayerCategoryDtos?.FirstOrDefault(ota => ota.PlayerCategory.Uid == playerCategory.Uid);
                if (musicBusinessRoundProjectPlayerCategories != null)
                {
                    this.PlayerCategories.Add(playerCategory);
                    this.PlayerCategoriesUids.Add(playerCategory.Uid);
                }
            }
        }
    }
}