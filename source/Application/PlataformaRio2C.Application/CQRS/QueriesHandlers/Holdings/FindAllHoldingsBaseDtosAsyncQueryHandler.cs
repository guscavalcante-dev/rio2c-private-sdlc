// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="FindAllHoldingsBaseDtosAsyncQueryHandler.cs" company="Softo">
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
    /// <summary>FindAllHoldingsBaseDtosAsyncQueryHandler</summary>
    public class FindAllHoldingsBaseDtosAsyncQueryHandler : IRequestHandler<FindAllHoldingsBaseDtosAsync, List<HoldingBaseDto>>
    {
        private readonly IHoldingRepository repo;

        /// <summary>Initializes a new instance of the <see cref="FindAllHoldingsBaseDtosAsyncQueryHandler"/> class.</summary>
        /// <param name="repository">The repository.</param>
        public FindAllHoldingsBaseDtosAsyncQueryHandler(IHoldingRepository repository)
        {
            this.repo = repository;
        }

        /// <summary>Handles the specified find all holdings base dtos asynchronous.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<List<HoldingBaseDto>> Handle(FindAllHoldingsBaseDtosAsync cmd, CancellationToken cancellationToken)
        {
            return await this.repo.FindAllBaseDto(cmd.Keywords);
        }
    }
}