// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-23-2019
// ***********************************************************************
// <copyright file="FindAllHoldingsBaseDtosAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindAllHoldingsBaseDtosAsync</summary>
    public class FindAllHoldingsBaseDtosAsync : BaseQuery<List<HoldingBaseDto>>
    {
        public string Keywords { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="FindAllHoldingsBaseDtosAsync"/> class.</summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public FindAllHoldingsBaseDtosAsync(
            string keywords, 
            string userInterfaceLanguage)
        {
            this.Keywords = keywords;
            this.UpdatePreSendProperties(userInterfaceLanguage);
        }
    }
}