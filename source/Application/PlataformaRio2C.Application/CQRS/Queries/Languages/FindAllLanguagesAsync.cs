// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-14-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-14-2019
// ***********************************************************************
// <copyright file="FindAllLanguagesAsync.cs" company="Softo">
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
    /// <summary>FindAllLanguagesAsync</summary>
    public class FindAllLanguagesAsync : BaseUserRequest, IRequest<List<LanguageDto>>
    {
        public FindAllLanguagesAsync(
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