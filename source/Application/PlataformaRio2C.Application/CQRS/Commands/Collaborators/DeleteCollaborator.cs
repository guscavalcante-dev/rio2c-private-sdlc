// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-26-2019
// ***********************************************************************
// <copyright file="DeleteCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteCollaborator</summary>
    public class DeleteCollaborator : BaseCommand
    {
        public Guid CollaboratorUid { get; set; }
        public string CollaboratorTypeName { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteCollaborator"/> class.</summary>
        /// <param name="entity">The entity.</param>
        public DeleteCollaborator(HoldingDto entity)
        {
            this.CollaboratorUid = entity.Uid;
        }

        /// <summary>Initializes a new instance of the <see cref="DeleteCollaborator"/> class.</summary>
        public DeleteCollaborator()
        {
        }

        /// <summary>Updates the pre send properties.</summary>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdatePreSendProperties(
            string collaboratorTypeName,
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage)
        {
            this.CollaboratorTypeName = collaboratorTypeName;
            this.UpdatePreSendProperties(userId, userUid, editionId, editionUid, UserInterfaceLanguage);
        }
    }
}