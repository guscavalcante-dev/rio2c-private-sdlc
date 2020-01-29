// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-28-2019
// ***********************************************************************
// <copyright file="FindLogisticSponsorDtoByUid.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindLogisticSponsorDtoByUid</summary>
    public class FindLogisticSponsorDtoByUid : BaseQuery<LogisticSponsorBaseDto>
    {
        public Guid SponsorUid { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="FindCollaboratorDtoByUidAndByEditionIdAsync"/> class.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public FindLogisticSponsorDtoByUid(
            Guid? sponsorUid,
            string userInterfaceLanguage)
        {
            this.SponsorUid = sponsorUid ?? Guid.Empty;
            this.UpdatePreSendProperties(userInterfaceLanguage);
        }
    }
}