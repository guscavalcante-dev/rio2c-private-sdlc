// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-08-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-08-2021
// ***********************************************************************
// <copyright file="InnovationCollaboratorBaseCommand.cs" company="Softo">
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
    /// <summary>InnovationCollaboratorBaseCommand</summary>
    public class InnovationCollaboratorBaseCommand : CollaboratorBaseCommand
    {
        [Display(Name = "Tracks", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "SelectAtLeastOneOption")]
        public List<AttendeeInnovationOrganizationTrackBaseCommand> AttendeeInnovationOrganizationTracks { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationCollaboratorBaseCommand" /> class.
        /// </summary>
        public InnovationCollaboratorBaseCommand()
        {
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
            this.AttendeeInnovationOrganizationTracks = new List<AttendeeInnovationOrganizationTrackBaseCommand>();
            foreach (var innovationOrganizationTrackOption in innovationOrganizationTrackOptions)
            {
                var attendeeCollaboratorInnovationOrganizationTrackDto = entity?.AttendeeCollaboratorInnovationOrganizationTrackDtos?.FirstOrDefault(aot => aot.InnovationOrganizationTrackOption.Uid == innovationOrganizationTrackOption.Uid);
                this.AttendeeInnovationOrganizationTracks.Add(attendeeCollaboratorInnovationOrganizationTrackDto != null ? new AttendeeInnovationOrganizationTrackBaseCommand(attendeeCollaboratorInnovationOrganizationTrackDto) :
                                                                                                            new AttendeeInnovationOrganizationTrackBaseCommand(innovationOrganizationTrackOption));
            }
        }
    }
}