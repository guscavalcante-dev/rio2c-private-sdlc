// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-04-2019
// ***********************************************************************
// <copyright file="FindUserAccessControlDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindUserAccessControlDto</summary>
    public class FindUserAccessControlDto : BaseQuery<UserAccessControlDto>
    {
        public int UserId { get; private set; }
        public int EditionId { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="FindUserAccessControlDto"/> class.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public FindUserAccessControlDto(int userId, int editionId, string userInterfaceLanguage)
            : base(userInterfaceLanguage)
        {
            this.UserId = userId;
            this.EditionId = editionId;
        }
    }
}