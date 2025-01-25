// ***********************************************************************
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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Foolproof;
using PlataformaRio2C.Application.CQRS.Commands.Music.BusinessRoundProjects.BaseCommands;
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

        public int SellerAttendeeCollaboratorId { get; set; }

        [Display(Name = "PlayerCategoriesThatHaveOrHadContract", ResourceType = typeof(Labels))]
        //TODO: Enable this when PlayersCategory is implemented; [RequiredIfEmpty("PlayerCategories", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
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
        public List<MusicBusinessRoundProjectPlayerCategoryBaseCommand> PlayerCategories { get; set; }
        public List<MusicBusinessRoundProjectExpectationsForMeetingBaseCommand> MusicBusinessRoundProjectExpectationsForMeetings { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicBusinessRoundProjectBaseCommand"/> class.</summary>
        public MusicBusinessRoundProjectBaseCommand()
        {
        }

        public void UpdateBaseProperties(
          MusicBusinessRoundProjectDto entity,
          List<LanguageDto> languagesDtos,
          List<TargetAudience> targetAudiences,
          List<InterestDto> interestsDtos,
          List<Activity> activities,
          bool isDataRequired,
          bool isProductionPlanRequired,
          bool isAdditionalInformationRequired,
          string userInterfaceLanguage,
          bool modalityRequired
      )
        {
            this.UpdateInterests(entity, interestsDtos);
            this.UpdateExpectationsForMeetings(entity, languagesDtos, isDataRequired);
            this.UpdateDropdownProperties(targetAudiences, activities, userInterfaceLanguage);

            //TODO:Implementar na parte de edicao/duplicacao de projeto.
            /*this.AttachmentUrl = entity.AttachmentUrl;*/
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
            Guid? editionUid
        )
        {
            this.SellerAttendeeCollaboratorId = collaboratorId;
            this.UpdatePreSendProperties(userId, userUid, editionId, editionUid, UserInterfaceLanguage);
        }

        /// <summary>Updates the dropdown properties.</summary>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateDropdownProperties(
            List<TargetAudience> targetAudiences,
            List<Activity> activities,
            string userInterfaceLanguage
        )
        {
            this.Activities = activities;
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


        /// <summary>Updates the summaries.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isDataRequired">if set to <c>true</c> [is data required].</param>
        private void UpdateExpectationsForMeetings(MusicBusinessRoundProjectDto entity, List<LanguageDto> languagesDtos, bool isDataRequired)
        {
            this.MusicBusinessRoundProjectExpectationsForMeetings = new List<MusicBusinessRoundProjectExpectationsForMeetingBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                var expectations = entity?.MusicBusinessRoundProjectExpectationsForMeetingDtos?.FirstOrDefault(ptd => ptd.Language.Code == languageDto.Code);
                this.MusicBusinessRoundProjectExpectationsForMeetings.Add(expectations != null ? new MusicBusinessRoundProjectExpectationsForMeetingBaseCommand(expectations, isDataRequired) :
                                                     new MusicBusinessRoundProjectExpectationsForMeetingBaseCommand(languageDto, isDataRequired));
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

    }
}