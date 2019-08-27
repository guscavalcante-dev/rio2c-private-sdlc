// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-26-2019
// ***********************************************************************
// <copyright file="FindCollaboratorDtoByUidAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindCollaboratorDtoByUidAsync</summary>
    public class FindCollaboratorDtoByUidAsync : BaseQuery<CollaboratorDto>
    {
        public Guid HoldingUid { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="FindCollaboratorDtoByUidAsync"/> class.</summary>
        /// <param name="holdingUid">The holding uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public FindCollaboratorDtoByUidAsync(
            Guid? holdingUid,
            string userInterfaceLanguage)
        {
            this.HoldingUid = holdingUid ?? Guid.Empty;
            this.UpdatePreSendProperties(userInterfaceLanguage);
        }
    }
}