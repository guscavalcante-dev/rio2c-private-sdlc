// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-19-2019
// ***********************************************************************
// <copyright file="DeleteCollaboratorOrganization.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteCollaboratorOrganization</summary>
    public class DeleteCollaboratorOrganization : BaseCommand
    {
        public Guid CollaboratorUid { get; set; }
        public Guid OrganizationUid { get; set; }

        public string CollaboratorTypeName { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteCollaboratorOrganization"/> class.</summary>
        public DeleteCollaboratorOrganization()
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