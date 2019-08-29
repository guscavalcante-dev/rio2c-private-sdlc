// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-28-2019
// ***********************************************************************
// <copyright file="FindCollaboratorDtoByUidAndByEditionIdAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindCollaboratorDtoByUidAndByEditionIdAsync</summary>
    public class FindCollaboratorDtoByUidAndByEditionIdAsync : BaseQuery<CollaboratorDto>
    {
        public Guid CollaboratorUid { get; private set; }
        public int EditionId { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="FindCollaboratorDtoByUidAndByEditionIdAsync"/> class.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public FindCollaboratorDtoByUidAndByEditionIdAsync(
            Guid? collaboratorUid,
            int? editionId,
            string userInterfaceLanguage)
        {
            this.CollaboratorUid = collaboratorUid ?? Guid.Empty;
            this.EditionId = editionId ?? 0;
            this.UpdatePreSendProperties(userInterfaceLanguage);
        }
    }
}