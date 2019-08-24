// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-23-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-23-2019
// ***********************************************************************
// <copyright file="FindAllCountriesBaseDtosAsyncQueryHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Application.CQRS.QueriesHandlers
{
    /// <summary>FindAllCountriesBaseDtosAsyncQueryHandler</summary>
    public class FindAllCountriesBaseDtosAsyncQueryHandler : IRequestHandler<FindAllCountriesBaseDtosAsync, List<CountryBaseDto>>
    {
        private readonly ICountryRepository repo;

        /// <summary>Initializes a new instance of the <see cref="FindAllCountriesBaseDtosAsyncQueryHandler"/> class.</summary>
        /// <param name="countryRepository">The country repository.</param>
        public FindAllCountriesBaseDtosAsyncQueryHandler(ICountryRepository countryRepository)
        {
            this.repo = countryRepository;
        }

        /// <summary>Handles the specified find all countries base dtos asynchronous.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<List<CountryBaseDto>> Handle(FindAllCountriesBaseDtosAsync cmd, CancellationToken cancellationToken)
        {
            return await this.repo.FindAllBaseDtosAsync();
        }
    }
}