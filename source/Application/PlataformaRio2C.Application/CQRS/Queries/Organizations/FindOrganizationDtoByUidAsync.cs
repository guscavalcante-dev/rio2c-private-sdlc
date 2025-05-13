// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-30-2019
// ***********************************************************************
// <copyright file="FindOrganizationDtoByUidAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using System;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindOrganizationDtoByUidAsync</summary>
    public class FindOrganizationDtoByUidAsync : BaseQuery<OrganizationDto>
    {
        public Guid OrganizationUid { get; private set; }
        public int EditionId { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="FindOrganizationDtoByUidAsync"/> class.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public FindOrganizationDtoByUidAsync(
            Guid? organizationUid,
            int editionId,
            string userInterfaceLanguage)
        {
            this.OrganizationUid = organizationUid ?? Guid.Empty;
            this.EditionId = editionId;
            this.UpdatePreSendProperties(userInterfaceLanguage);
        }
    }
}