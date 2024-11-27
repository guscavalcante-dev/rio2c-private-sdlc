// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Updated          : 11-19-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 11-19-2024
// ***********************************************************************
// <copyright file="UpdateMusicCommissionCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    public class UpdateMusicCommissionCollaborator : MusicCommissionCollaboratorBaseCommand
    {
        public Guid CollaboratorUid { get; set; }
        public bool IsAddingToCurrentEdition { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMusicCommissionCollaborator"/> class.
        /// </summary>
        /// <param name="MusicOptions">The Music options.</param>
        public UpdateMusicCommissionCollaborator(
            CollaboratorDto entity, 
            List<CollaboratorType> collaboratorTypes,
            bool isAddingToCurrentEdition,
            string userInterfaceLanguage)
        {
            this.CollaboratorUid = entity.Uid;
            this.IsAddingToCurrentEdition = isAddingToCurrentEdition;

            this.UpdateBaseProperties(
                entity, 
                collaboratorTypes, 
                userInterfaceLanguage);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMusicCommissionCollaborator"/> class.
        /// </summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="cmd">The command.</param>
        public UpdateMusicCommissionCollaborator(Guid collaboratorUid, CreateMusicCommissionCollaborator cmd)
        {
            this.CollaboratorUid = collaboratorUid;
            this.IsAddingToCurrentEdition = true;

            this.FirstName = cmd.FirstName;
            this.LastNames = cmd.LastNames;
            this.Email = cmd.Email;
            this.CollaboratorTypeName = cmd.CollaboratorTypeName;
        }

        public UpdateMusicCommissionCollaborator()
        {
        }
    }
}