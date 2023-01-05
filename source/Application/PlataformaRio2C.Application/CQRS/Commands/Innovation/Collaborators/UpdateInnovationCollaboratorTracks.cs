// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-19-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-30-2022
// ***********************************************************************
// <copyright file="UpdateInnovationCollaboratorTracks.cs" company="Softo">
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
    /// <summary>UpdateInnovationCollaboratorTracks</summary>
    public class UpdateInnovationCollaboratorTracks : BaseCommand
    { 
        public Guid CollaboratorUid { get; set; }
       
        [Display(Name = "Tracks", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "SelectAtLeastOneOption")]
        public List<InnovationOrganizationTrackOptionBaseCommand> AttendeeInnovationOrganizationTracks { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateInnovationCollaboratorTracks"/> class.</summary>
        public UpdateInnovationCollaboratorTracks()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInnovationCollaboratorTracks"/> class.
        /// </summary>
        /// <param name="attendeeCollaboratorTracksWidgetDto">The attendee collaborator tracks widget dto.</param>
        /// <param name="innovationOrganizationTrackOptionDtos">The innovation organization track option dtos.</param>
        public UpdateInnovationCollaboratorTracks(
            AttendeeCollaboratorTracksWidgetDto attendeeCollaboratorTracksWidgetDto, 
            List<InnovationOrganizationTrackOptionDto> innovationOrganizationTrackOptionDtos)
        {
            this.CollaboratorUid = attendeeCollaboratorTracksWidgetDto.AttendeeCollaboratorDto.Collaborator.Uid;
            this.UpdateBaseProperties(attendeeCollaboratorTracksWidgetDto, innovationOrganizationTrackOptionDtos);
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
        /// Updates the base properties.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="innovationOrganizationTrackOptionDtos">The innovation organization track option dtos.</param>
        public void UpdateBaseProperties(
            AttendeeCollaboratorTracksWidgetDto entity,
            List<InnovationOrganizationTrackOptionDto> innovationOrganizationTrackOptionDtos)
        {
            this.UpdateInnovationOrganizationTrackOptions(entity, innovationOrganizationTrackOptionDtos);
        }

        /// <summary>
        /// Updates the dropdown properties.
        /// </summary>
        /// <param name="innovationOrganizationTrackOptionDtos">The innovation organization track option dtos.</param>
        public void UpdateDropdownProperties(
            List<InnovationOrganizationTrackOptionDto> innovationOrganizationTrackOptionDtos)
        {
            this.UpdateInnovationOrganizationTrackOptions(null, innovationOrganizationTrackOptionDtos);
        }

        /// <summary>
        /// Updates the innovation organization track options.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="innovationOrganizationTrackOptionDtos">The innovation organization track option dtos.</param>
        private void UpdateInnovationOrganizationTrackOptions(
            AttendeeCollaboratorTracksWidgetDto entity,
            List<InnovationOrganizationTrackOptionDto> innovationOrganizationTrackOptionDtos)
        {
            this.AttendeeInnovationOrganizationTracks = new List<InnovationOrganizationTrackOptionBaseCommand>();
            foreach (var innovationOrganizationTrackOptionDto in innovationOrganizationTrackOptionDtos)
            {
                var attendeeCollaboratorInnovationOrganizationTrackDto = entity?.AttendeeCollaboratorInnovationOrganizationTrackDtos
                                                                                    ?.FirstOrDefault(aot => aot.InnovationOrganizationTrackOption.Uid == innovationOrganizationTrackOptionDto.InnovationOrganizationTrackOption.Uid);

                this.AttendeeInnovationOrganizationTracks.Add(attendeeCollaboratorInnovationOrganizationTrackDto != null ? new InnovationOrganizationTrackOptionBaseCommand(attendeeCollaboratorInnovationOrganizationTrackDto) :
                                                                                                                           new InnovationOrganizationTrackOptionBaseCommand(innovationOrganizationTrackOptionDto));
            }
        }
    }
}