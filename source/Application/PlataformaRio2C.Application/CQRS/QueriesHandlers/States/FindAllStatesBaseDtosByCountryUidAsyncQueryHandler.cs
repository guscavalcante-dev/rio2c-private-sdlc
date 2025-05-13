// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-23-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-23-2019
// ***********************************************************************
// <copyright file="FindAllStatesBaseDtosAsyncQueryHandler.cs" company="Softo">
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
    /// <summary>FindAllStatesBaseDtosAsyncQueryHandler</summary>
    public class FindAllStatesBaseDtosAsyncQueryHandler : IRequestHandler<FindAllStatesBaseDtosByCountryUidAsync, List<StateBaseDto>>
    {
        private readonly IStateRepository repo;

        /// <summary>Initializes a new instance of the <see cref="FindAllStatesBaseDtosAsyncQueryHandler"/> class.</summary>
        /// <param name="stateRepository">The state repository.</param>
        public FindAllStatesBaseDtosAsyncQueryHandler(IStateRepository stateRepository)
        {
            this.repo = stateRepository;
        }

        /// <summary>Handles the specified find all states base dtos by country uid asynchronous.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<List<StateBaseDto>> Handle(FindAllStatesBaseDtosByCountryUidAsync cmd, CancellationToken cancellationToken)
        {
            return await this.repo.FindAllBaseDtosByCountryUidAsync(cmd.CountryUid);
        }
    }
}