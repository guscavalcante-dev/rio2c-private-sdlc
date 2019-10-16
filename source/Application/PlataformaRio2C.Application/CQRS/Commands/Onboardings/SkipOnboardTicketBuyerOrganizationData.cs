// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-15-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-15-2019
// ***********************************************************************
// <copyright file="SkipOnboardTicketBuyerOrganizationData.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>SkipOnboardTicketBuyerOrganizationData</summary>
    public class SkipOnboardTicketBuyerOrganizationData : BaseCommand
    {
        public Guid CollaboratorUid { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="SkipOnboardTicketBuyerOrganizationData"/> class.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public SkipOnboardTicketBuyerOrganizationData(
            Guid collaboratorUid,
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage)
        {
            this.CollaboratorUid = collaboratorUid;
            this.UpdatePreSendProperties(userId, userUid, editionId, editionUid, userInterfaceLanguage);
        }

        /// <summary>Initializes a new instance of the <see cref="SkipOnboardTicketBuyerOrganizationData"/> class.</summary>
        public SkipOnboardTicketBuyerOrganizationData()
        {
        }
    }
}