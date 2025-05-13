// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-23-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-23-2019
// ***********************************************************************
// <copyright file="FindAllCitiesBaseDtosByStateUidAsyncQueryHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.QueriesHandlers
{
    /// <summary>FindAllCitiesBaseDtosByStateUidAsyncQueryHandler</summary>
    public class FindAllCitiesBaseDtosByStateUidAsyncQueryHandler : IRequestHandler<FindAllCitiesBaseDtosByStateUidAsync, List<CityBaseDto>>
    {
        private readonly ICityRepository repo;

        /// <summary>Initializes a new instance of the <see cref="FindAllCitiesBaseDtosByStateUidAsyncQueryHandler"/> class.</summary>
        /// <param name="cityRepository">The city repository.</param>
        public FindAllCitiesBaseDtosByStateUidAsyncQueryHandler(ICityRepository cityRepository)
        {
            this.repo = cityRepository;
        }

        /// <summary>Handles the specified find all cities base dtos by state uid asynchronous.</summary>
        /// <param name="cmd"></param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<List<CityBaseDto>> Handle(FindAllCitiesBaseDtosByStateUidAsync cmd, CancellationToken cancellationToken)
        {
            return await this.repo.FindAllBaseDtosByStateUidAsync(cmd.StateUid);
        }
    }
}