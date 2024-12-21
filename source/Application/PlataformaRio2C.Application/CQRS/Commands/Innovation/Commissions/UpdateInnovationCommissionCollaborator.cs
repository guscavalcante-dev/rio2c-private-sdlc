// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Updated          : 08-19-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-17-2024
// ***********************************************************************
// <copyright file="UpdateInnovationCommissionCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    public class UpdateInnovationCommissionCollaborator : InnovationCommissionCollaboratorBaseCommand
    {
        public Guid CollaboratorUid { get; set; }
        public bool IsAddingToCurrentEdition { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInnovationCommissionCollaborator"/> class.
        /// </summary>
        /// <param name="innovationOptions">The innovation options.</param>
        public UpdateInnovationCommissionCollaborator(List<InnovationOrganizationTrackOptionDto> innovationOrganizationTrackOptionDtos)
        {
            this.UpdateBaseProperties(null, innovationOrganizationTrackOptionDtos);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInnovationCommissionCollaborator" /> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="isAddingToCurrentEdition">The is adding to current edition.</param>
        /// <param name="attendeeCollaboratorTracksWidgetDto">The attendee collaborator tracks widget dto.</param>
        /// <param name="innovationOrganizationTrackOptionDtos">The innovation organization track options.</param>
        /// <exception cref="PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions.DomainException"></exception>
        public UpdateInnovationCommissionCollaborator(
            CollaboratorDto entity,
            bool? isAddingToCurrentEdition,
            AttendeeCollaboratorTracksWidgetDto attendeeCollaboratorTracksWidgetDto,
            List<InnovationOrganizationTrackOptionDto> innovationOrganizationTrackOptionDtos)
        {
            if (entity == null)
            {
                throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Speaker, Labels.FoundM));
            }

            this.CollaboratorUid = entity.Uid;
            this.IsAddingToCurrentEdition = isAddingToCurrentEdition ?? false;

            this.UpdateBaseProperties(entity, attendeeCollaboratorTracksWidgetDto, innovationOrganizationTrackOptionDtos);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInnovationCommissionCollaborator" /> class.
        /// </summary>
        public UpdateInnovationCommissionCollaborator(Guid collaboratorUid, CreateInnovationCommissionCollaborator cmd)
        {
            this.CollaboratorUid = collaboratorUid;
            this.IsAddingToCurrentEdition = true;

            this.FirstName = cmd.FirstName;
            this.LastNames = cmd.LastNames;
            this.Email = cmd.Email;
            this.InnovationOrganizationTrackGroups = cmd.InnovationOrganizationTrackGroups;
        }

        public UpdateInnovationCommissionCollaborator()
        {
        }
    }
}