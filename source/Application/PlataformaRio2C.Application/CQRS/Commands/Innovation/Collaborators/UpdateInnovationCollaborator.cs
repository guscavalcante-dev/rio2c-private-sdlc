// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Updated          : 08-19-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-19-2021
// ***********************************************************************
// <copyright file="UpdateInnovationCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateInnovationCollaborator</summary>
    public class UpdateInnovationCollaborator : InnovationCollaboratorBaseCommand
    {
        public Guid CollaboratorUid { get; set; }
        public bool IsAddingToCurrentEdition { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInnovationCollaborator"/> class.
        /// </summary>
        /// <param name="innovationOptions">The innovation options.</param>
        public UpdateInnovationCollaborator(List<InnovationOrganizationTrackOption> innovationOrganizationTrackOptions)
        {
            this.UpdateBaseProperties(null, innovationOrganizationTrackOptions);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInnovationCollaborator"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="isAddingToCurrentEdition">The is adding to current edition.</param>
        /// <param name="attendeeCollaboratorTracksWidgetDto">The attendee collaborator tracks widget dto.</param>
        /// <param name="innovationOrganizationTrackOptions">The innovation organization track options.</param>
        /// <exception cref="PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions.DomainException"></exception>
        public UpdateInnovationCollaborator(
            CollaboratorDto entity,
            bool? isAddingToCurrentEdition,
            AttendeeCollaboratorTracksWidgetDto attendeeCollaboratorTracksWidgetDto,
            List<InnovationOrganizationTrackOption> innovationOrganizationTrackOptions)
        {
            if (entity == null)
            {
                throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Speaker, Labels.FoundM));
            }

            this.CollaboratorUid = entity.Uid;
            this.IsAddingToCurrentEdition = isAddingToCurrentEdition ?? false;

            this.UpdateBaseProperties(entity, attendeeCollaboratorTracksWidgetDto, innovationOrganizationTrackOptions);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInnovationCollaborator" /> class.
        /// </summary>
        public UpdateInnovationCollaborator()
        {
        }
    }
}