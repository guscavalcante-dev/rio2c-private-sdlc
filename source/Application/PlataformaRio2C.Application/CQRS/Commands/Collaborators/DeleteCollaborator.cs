// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-21-2023
// ***********************************************************************
// <copyright file="DeleteCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteCollaborator</summary>
    public class DeleteCollaborator : BaseCommand
    {
        public Guid CollaboratorUid { get; set; }
        public string CollaboratorTypeName { get; set; }

        /// <summary>
        /// This parameter is only necessary when deleting Producers or Players Executives.
        /// TODO: This will be deleted when split the CollaboratorType.AudiovisualExecutive to PlayerExecutiveAudiovisual and ProducerExecutiveAudiovisual
        /// </summary>
        public string OrganizationTypeName { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteCollaborator"/> class.</summary>
        public DeleteCollaborator()
        {
        }

        /// <summary>
        /// Updates the pre send properties.
        /// </summary>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="organizationTypeName">This parameter is only necessary when deleting Producers or Players Executives.
        /// This will be deleted when split the CollaboratorType.AudiovisualExecutive to n and ProducerExecutiveAudiovisual</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdatePreSendProperties(
            string collaboratorTypeName,
            string organizationTypeName,
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage)
        {
            this.CollaboratorTypeName = collaboratorTypeName;
            this.OrganizationTypeName = organizationTypeName;
            this.UpdatePreSendProperties(userId, userUid, editionId, editionUid, userInterfaceLanguage);
        }

        /// <summary>
        /// Updates the pre send properties.
        /// </summary>
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
            this.UpdatePreSendProperties(userId, userUid, editionId, editionUid, userInterfaceLanguage);
        }
    }
}