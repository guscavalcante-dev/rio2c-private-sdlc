// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : William Almado
// Created          : 10-09-2019
//
// Last Modified By : William Almado
// Last Modified On : 09-27-2019
// ***********************************************************************
// <copyright file="FindUserLanguageDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindUserAccessControlDto</summary>
    public class FindUserLanguageDto : BaseQuery<UserLanguageDto>
    {
        public int UserId { get; private set; }
        public int EditionId { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="FindUserLanguageControlDto"/> class.</summary>
        /// <param name="userId">The user identifier.</param>
        public FindUserLanguageDto(int userId)
        {
            this.UserId = userId;
        }
    }
}