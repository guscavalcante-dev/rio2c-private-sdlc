// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-23-2019
// ***********************************************************************
// <copyright file="FindOrganizationDtoByUidAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindOrganizationDtoByUidAsync</summary>
    public class FindOrganizationDtoByUidAsync : BaseQuery<OrganizationDto>
    {
        public Guid HoldingUid { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="FindOrganizationDtoByUidAsync"/> class.</summary>
        /// <param name="holdingUid">The holding uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public FindOrganizationDtoByUidAsync(
            Guid? holdingUid,
            string userInterfaceLanguage)
        {
            this.HoldingUid = holdingUid ?? Guid.Empty;
            this.UpdatePreSendProperties(userInterfaceLanguage);
        }
    }
}