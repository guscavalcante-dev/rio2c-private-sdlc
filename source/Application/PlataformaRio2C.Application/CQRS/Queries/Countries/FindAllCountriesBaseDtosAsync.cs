// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-23-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-23-2019
// ***********************************************************************
// <copyright file="FindAllCountriesBaseDtosAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindAllCountriesBaseDtosAsync</summary>
    public class FindAllCountriesBaseDtosAsync : BaseQuery<List<CountryBaseDto>>
    {
        /// <summary>Initializes a new instance of the <see cref="FindAllCountriesBaseDtosAsync"/> class.</summary>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public FindAllCountriesBaseDtosAsync(string userInterfaceLanguage)
        {
            this.UpdatePreSendProperties(userInterfaceLanguage);
        }
    }
}