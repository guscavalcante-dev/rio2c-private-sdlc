// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-02-2019
// ***********************************************************************
// <copyright file="FindAccessControlDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindAccessControlDto</summary>
    public class FindAccessControlDto : BaseQuery<AccessControlAttendeeCollaboratorDto>
    {
        public Guid EditionUid { get; private set; }
        public int UserId { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="FindAccessControlDto"/> class.</summary>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public FindAccessControlDto(Guid editionUid,  int userId, string userInterfaceLanguage)
            : base(userInterfaceLanguage)
        {
            this.EditionUid = editionUid;
            this.UserId = userId;
        }
    }
}