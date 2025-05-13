// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-14-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-23-2019
// ***********************************************************************
// <copyright file="FindAllLanguagesDtosAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindAllLanguagesDtosAsync</summary>
    public class FindAllLanguagesDtosAsync : BaseQuery<List<LanguageDto>>
    {
        /// <summary>Initializes a new instance of the <see cref="FindAllLanguagesDtosAsync"/> class.</summary>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public FindAllLanguagesDtosAsync(string userInterfaceLanguage)
        {
            this.UpdatePreSendProperties(userInterfaceLanguage);
        }
    }
}