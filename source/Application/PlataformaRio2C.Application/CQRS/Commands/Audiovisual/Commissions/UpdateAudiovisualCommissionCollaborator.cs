// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Updated          : 07-19-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-19-2021
// ***********************************************************************
// <copyright file="UpdateAudiovisualCommissionCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using PlataformaRio2C.Domain.Dtos;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    public class UpdateAudiovisualCommissionCollaborator : AudiovisualCommissionCollaboratorBaseCommand
    {
        public Guid CollaboratorUid { get; set; }
        public bool IsAddingToCurrentEdition { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAudiovisualCommissionCollaborator" /> class.
        /// </summary>
        public UpdateAudiovisualCommissionCollaborator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAudiovisualCommissionCollaborator"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="isAddingToCurrentEdition">The is adding to current edition.</param>
        /// <param name="attendeeCollaboratorInterestsWidgetDto">The attendee collaborator interests widget dto.</param>
        /// <param name="interestsDtos">The interests dtos.</param>
        public UpdateAudiovisualCommissionCollaborator(
            CollaboratorDto entity,
            bool? isAddingToCurrentEdition,
            AttendeeCollaboratorInterestsWidgetDto attendeeCollaboratorInterestsWidgetDto,
            List<InterestDto> interestsDtos)
        {
            this.CollaboratorUid = entity.Uid;
            this.IsAddingToCurrentEdition = isAddingToCurrentEdition ?? false;

            this.UpdateBaseProperties(entity, attendeeCollaboratorInterestsWidgetDto, interestsDtos);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAudiovisualCommissionCollaborator" /> class.
        /// </summary>
        /// <param name="interestsDtos">The interests dtos.</param>
        public UpdateAudiovisualCommissionCollaborator(
            List<InterestDto> interestsDtos)
        {
            base.UpdateBaseProperties(null, interestsDtos);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAudiovisualCommissionCollaborator" /> class.
        /// </summary>
        public UpdateAudiovisualCommissionCollaborator(Guid collaboratorUid, CreateAudiovisualCommissionCollaborator cmd)
        {
            this.CollaboratorUid = collaboratorUid;
            this.IsAddingToCurrentEdition = true;

            this.FirstName = cmd.FirstName;
            this.LastNames = cmd.LastNames;
            this.Email = cmd.Email;
            this.Interests = cmd.Interests;
        }
    }
}