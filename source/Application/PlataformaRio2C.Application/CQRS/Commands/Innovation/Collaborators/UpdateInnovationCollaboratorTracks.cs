// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-19-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-19-2021
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
        /// <param name="innovationOrganizationTrackOptions">The innovation organization track options.</param>
        public UpdateInnovationCollaboratorTracks(
            AttendeeCollaboratorTracksWidgetDto attendeeCollaboratorTracksWidgetDto, 
            List<InnovationOrganizationTrackOption> innovationOrganizationTrackOptions)
        {
            this.CollaboratorUid = attendeeCollaboratorTracksWidgetDto.AttendeeCollaboratorDto.Collaborator.Uid;
            this.UpdateBaseProperties(attendeeCollaboratorTracksWidgetDto, innovationOrganizationTrackOptions);
        }

        /// <summary>Updates the pre send properties.</summary>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
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
        /// <param name="innovationOrganizationTrackOptions">The innovation organization track options.</param>
        public void UpdateBaseProperties(
            AttendeeCollaboratorTracksWidgetDto entity,
            List<InnovationOrganizationTrackOption> innovationOrganizationTrackOptions)
        {
            this.UpdateInnovationOrganizationTrackOptions(entity, innovationOrganizationTrackOptions);
        }

        /// <summary>
        /// Updates the dropdown properties.
        /// </summary>
        /// <param name="innovationOrganizationTrackOptions">The innovation organization track options.</param>
        public void UpdateDropdownProperties(
            List<InnovationOrganizationTrackOption> innovationOrganizationTrackOptions)
        {
            this.UpdateInnovationOrganizationTrackOptions(null, innovationOrganizationTrackOptions);
        }

        /// <summary>
        /// Updates the innovation organization track options.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="innovationOrganizationTrackOptions">The innovation organization track options.</param>
        private void UpdateInnovationOrganizationTrackOptions(
            AttendeeCollaboratorTracksWidgetDto entity,
            List<InnovationOrganizationTrackOption> innovationOrganizationTrackOptions)
        {
            this.AttendeeInnovationOrganizationTracks = new List<InnovationOrganizationTrackOptionBaseCommand>();
            foreach (var innovationOrganizationTrackOption in innovationOrganizationTrackOptions)
            {
                var attendeeCollaboratorInnovationOrganizationTrackDto = entity?.AttendeeCollaboratorInnovationOrganizationTrackDtos?.FirstOrDefault(aot => aot.InnovationOrganizationTrackOption.Uid == innovationOrganizationTrackOption.Uid);
                this.AttendeeInnovationOrganizationTracks.Add(attendeeCollaboratorInnovationOrganizationTrackDto != null ? new InnovationOrganizationTrackOptionBaseCommand(attendeeCollaboratorInnovationOrganizationTrackDto) :
                                                                                                            new InnovationOrganizationTrackOptionBaseCommand(innovationOrganizationTrackOption));
            }
        }
    }
}