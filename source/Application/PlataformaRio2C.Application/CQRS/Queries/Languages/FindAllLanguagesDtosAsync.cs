// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-14-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-16-2019
// ***********************************************************************
// <copyright file="FindAllLanguagesDtosAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using MediatR;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindAllLanguagesDtosAsync</summary>
    public class FindAllLanguagesDtosAsync : BaseUserRequest, IRequest<List<LanguageDto>>
    {
        /// <summary>Initializes a new instance of the <see cref="FindAllLanguagesDtosAsync"/> class.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public FindAllLanguagesDtosAsync(
            int userId, 
            Guid userUid, 
            int? editionId, 
            Guid? editionUid, 
            string userInterfaceLanguage)
            : base(userId, userUid, editionId, editionUid, userInterfaceLanguage)
        {
        }
    }
}