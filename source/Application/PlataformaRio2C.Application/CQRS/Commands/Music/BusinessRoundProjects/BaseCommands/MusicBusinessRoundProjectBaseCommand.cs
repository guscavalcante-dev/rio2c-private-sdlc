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
        [RequiredIfEmpty("PlayerCategories", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(300, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string PlayerCategoriesThatHaveOrHadContract { get; set; }

        [Display(Name = "AttachmentUrl", ResourceType = typeof(Labels))]
        [StringLength(300, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string AttachmentUrl { get; set; }
        public List<Guid> TargetAudiencesUids { get; set; }
        public Guid? SellerAttendeeCollaboratorUid { get; private set; }
        public int ProjectBuyerEvaluationsCount { get; set; }
        public IEnumerable<ProjectInterestDto> ProjectInterestDtos { get; set; }
        public List<MusicBusinessRoundProjectTargetAudience> MusicBusinessRoundProjectTargetAudience { get; set; }
        public InterestBaseCommand[][] Interests { get; set; }
        //public List<MusicBusinessRoundProjectInterestBaseCommand> MusicBusinessRoundProjectInterests { get; set; }
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
          bool isDataRequired,
          bool isProductionPlanRequired,
          bool isAdditionalInformationRequired,
          string userInterfaceLanguage,
          bool modalityRequired
      )
        {
            this.UpdateInterests(entity, interestsDtos);
            this.UpdateExpectationsForMeetings(entity, languagesDtos, isDataRequired);

            //TODO:Implementar na parte de edicao/duplicacao de projeto.
            /*this.AttachmentUrl = entity.AttachmentUrl;*/
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