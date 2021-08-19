// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 08-18-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-18-2021
// ***********************************************************************
// <copyright file="UpdateAudiovisualCollaboratorInterests.cs" company="Softo">
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
    /// <summary>UpdateAudiovisualCollaboratorInterests</summary>
    public class UpdateAudiovisualCollaboratorInterests : BaseCommand
    {
        public Guid CollaboratorUid { get; set; }

        [Display(Name = "Interests", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "SelectAtLeastOneOption")]
        public InterestBaseCommand[][] Interests { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAudiovisualCollaboratorInterests"/> class.
        /// </summary>
        /// <param name="commissionAttendeeCollaboratorInterestsWidgetDto">The commission attendee collaborator interests widget dto.</param>
        /// <param name="interestsDtos">The interests dtos.</param>
        public UpdateAudiovisualCollaboratorInterests(
            CommissionAttendeeCollaboratorInterestsWidgetDto commissionAttendeeCollaboratorInterestsWidgetDto,
            List<InterestDto> interestsDtos)
        {
            this.CollaboratorUid = commissionAttendeeCollaboratorInterestsWidgetDto.AttendeeCollaboratorDto.Collaborator.Uid;
            this.UpdateBaseProperties(commissionAttendeeCollaboratorInterestsWidgetDto, interestsDtos);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateAudiovisualCollaboratorInterests"/> class.</summary>
        public UpdateAudiovisualCollaboratorInterests()
        {
        }

        /// <summary>
        /// Updates the pre send properties.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdatePreSendProperties(
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage)
        {
            base.UpdatePreSendProperties(userId, userUid, editionId, editionUid, userInterfaceLanguage);
        }

        /// <summary>
        /// Updates the models and lists.
        /// </summary>
        /// <param name="interestsDtos">The innovation organization track options.</param>
        public void UpdateBaseProperties(
            CommissionAttendeeCollaboratorInterestsWidgetDto entity,
            List<InterestDto> interestsDtos)
        {
            this.UpdateAudiovisualOrganizationTrackOptions(entity, interestsDtos);
        }

        /// <summary>
        /// Updates the dropdown properties.
        /// </summary>
        /// <param name="interestsDtos">The innovation organization track options.</param>
        public void UpdateDropdownProperties(
            List<InterestDto> interestsDtos)
        {
            this.UpdateAudiovisualOrganizationTrackOptions(null, interestsDtos);
        }

        /// <summary>
        /// Updates the audiovisual organization track options.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="interestsDtos">The interests dtos.</param>
        private void UpdateAudiovisualOrganizationTrackOptions(
            CommissionAttendeeCollaboratorInterestsWidgetDto entity,
            List<InterestDto> interestsDtos)
        {
            var interestsBaseCommands = new List<InterestBaseCommand>();
            foreach (var interestDto in interestsDtos)
            {
                var commissionAttendeeCollaboratorInterestDto = entity?.CommissionAttendeeCollaboratorInterestDtos?.FirstOrDefault(oad => oad.Interest.Uid == interestDto.Interest.Uid);
                interestsBaseCommands.Add(commissionAttendeeCollaboratorInterestDto != null ? new InterestBaseCommand(commissionAttendeeCollaboratorInterestDto) :
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