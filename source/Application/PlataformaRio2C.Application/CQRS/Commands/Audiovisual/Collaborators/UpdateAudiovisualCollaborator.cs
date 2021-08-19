// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Updated          : 07-19-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-19-2021
// ***********************************************************************
// <copyright file="UpdateAudiovisualCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateAudiovisualCollaborator</summary>
    public class UpdateAudiovisualCollaborator : AudiovisualCollaboratorBaseCommand
    {
        public Guid CollaboratorUid { get; set; }
        public bool IsAddingToCurrentEdition { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAudiovisualCollaborator" /> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="isAddingToCurrentEdition">The is adding to current edition.</param>
        /// <param name="commissionAttendeeCollaboratorInterestsWidgetDto">The commission attendee collaborator interests widget dto.</param>
        /// <param name="interestsDtos">The interests dtos.</param>
        public UpdateAudiovisualCollaborator(
            CollaboratorDto entity,
            bool? isAddingToCurrentEdition,
            CommissionAttendeeCollaboratorInterestsWidgetDto commissionAttendeeCollaboratorInterestsWidgetDto,
            List<InterestDto> interestsDtos)
        {
            this.CollaboratorUid = entity.Uid;
            this.IsAddingToCurrentEdition = isAddingToCurrentEdition ?? false;

            this.UpdateBaseProperties(entity, commissionAttendeeCollaboratorInterestsWidgetDto, interestsDtos);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAudiovisualCollaborator" /> class.
        /// </summary>
        /// <param name="interestsDtos">The interests dtos.</param>
        public UpdateAudiovisualCollaborator(
            List<InterestDto> interestsDtos)
        {
            base.UpdateBaseProperties(null, interestsDtos);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAudiovisualCollaborator" /> class.
        /// </summary>
        public UpdateAudiovisualCollaborator()
        {
        }
    }
}