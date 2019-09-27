// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-27-2019
// ***********************************************************************
// <copyright file="FindAdminAccessControlDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindAdminAccessControlDto</summary>
    public class FindAdminAccessControlDto : BaseQuery<AdminAccessControlDto>
    {
        public int UserId { get; private set; }
        public int EditionId { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="FindAdminAccessControlDto"/> class.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public FindAdminAccessControlDto(int userId, int editionId, string userInterfaceLanguage)
            : base(userInterfaceLanguage)
        {
            this.UserId = userId;
            this.EditionId = editionId;
        }
    }
}