// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Updated          : 08-19-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-17-2024
// ***********************************************************************
// <copyright file="UpdateCreatorCommissionCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    public class UpdateCreatorCommissionCollaborator : CreatorCommissionCollaboratorBaseCommand
    {
        public Guid CollaboratorUid { get; set; }
        public bool IsAddingToCurrentEdition { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCreatorCommissionCollaborator"/> class.
        /// </summary>
        public UpdateCreatorCommissionCollaborator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCreatorCommissionCollaborator" /> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="isAddingToCurrentEdition">The is adding to current edition.</param>
        public UpdateCreatorCommissionCollaborator(
            CollaboratorDto entity, 
            bool? isAddingToCurrentEdition)
        {
            this.CollaboratorUid = entity.Uid;
            this.IsAddingToCurrentEdition = isAddingToCurrentEdition ?? false;

            this.UpdateBaseProperties(entity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCreatorCommissionCollaborator"/> class.
        /// </summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="cmd">The command.</param>
        public UpdateCreatorCommissionCollaborator(Guid collaboratorUid, CreateCreatorCommissionCollaborator cmd)
        {
            this.CollaboratorUid = collaboratorUid;
            this.IsAddingToCurrentEdition = true;

            this.FirstName = cmd.FirstName;
            this.LastNames = cmd.LastNames;
            this.Email = cmd.Email;
        }
    }
}