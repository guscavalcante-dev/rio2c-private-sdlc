// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-23-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-23-2019
// ***********************************************************************
// <copyright file="FindAllStatesBaseDtosByCountryUidAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindAllStatesBaseDtosByCountryUidAsync</summary>
    public class FindAllStatesBaseDtosByCountryUidAsync : BaseQuery<List<StateBaseDto>>
    {
        public Guid CountryUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="FindAllStatesBaseDtosByCountryUidAsync"/> class.</summary>
        public FindAllStatesBaseDtosByCountryUidAsync()
        {
        }
    }
}