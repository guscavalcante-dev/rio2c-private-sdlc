// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-08-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-04-2023
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
        public List<InnovationOrganizationTrackOptionBaseCommand> InnovationOrganizationTrackOptions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationCollaboratorBaseCommand" /> class.
        /// </summary>
        public InnovationCollaboratorBaseCommand()
        {
        }

        /// <summary>
        /// Updates the base properties.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="innovationOrganizationTrackOptionDtos">The innovation organization track option dto.</param>
        public void UpdateBaseProperties(
            AttendeeCollaboratorTracksWidgetDto entity, 
            List<InnovationOrganizationTrackOptionDto> innovationOrganizationTrackOptionDtos)
        {
            this.UpdateInnovationOrganizationTrackOptions(entity, innovationOrganizationTrackOptionDtos);
        }

        /// <summary>
        /// Updates the base properties.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="attendeeCollaboratorTracksWidgetDto">The attendee collaborator tracks widget dto.</param>
        /// <param name="innovationOrganizationTrackOptionDtos">The innovation organization track option dtos.</param>
        public void UpdateBaseProperties(
            CollaboratorDto entity,
            AttendeeCollaboratorTracksWidgetDto attendeeCollaboratorTracksWidgetDto,
            List<InnovationOrganizationTrackOptionDto> innovationOrganizationTrackOptionDtos)
        {
            base.UpdateBaseProperties(entity);
            this.UpdateInnovationOrganizationTrackOptions(attendeeCollaboratorTracksWidgetDto, innovationOrganizationTrackOptionDtos);
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
            this.InnovationOrganizationTrackOptions = new List<InnovationOrganizationTrackOptionBaseCommand>();
            foreach (var innovationOrganizationTrackOptionDto in innovationOrganizationTrackOptionDtos)
            {
                var attendeeCollaboratorInnovationOrganizationTrackDto = entity?.AttendeeCollaboratorInnovationOrganizationTrackDtos?.FirstOrDefault(aot => aot.InnovationOrganizationTrackOption.Uid == innovationOrganizationTrackOptionDto.InnovationOrganizationTrackOption.Uid);
                this.InnovationOrganizationTrackOptions.Add(attendeeCollaboratorInnovationOrganizationTrackDto != null ? new InnovationOrganizationTrackOptionBaseCommand(attendeeCollaboratorInnovationOrganizationTrackDto) :
                                                                                                                         new InnovationOrganizationTrackOptionBaseCommand(innovationOrganizationTrackOptionDto));
            }
        }
    }
}