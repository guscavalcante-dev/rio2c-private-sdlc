// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="CountAllHoldingsAsyncQueryHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Application.CQRS.QueriesHandlers
{
    /// <summary>CountAllHoldingsAsyncQueryHandler</summary>
    public class CountAllHoldingsAsyncQueryHandler : IRequestHandler<CountAllHoldingsAsync, int>
    {
        private readonly IHoldingRepository repo;

        /// <summary>Initializes a new instance of the <see cref="CountAllHoldingsAsyncQueryHandler"/> class.</summary>
        /// <param name="repository">The repository.</param>
        public CountAllHoldingsAsyncQueryHandler(IHoldingRepository repository)
        {
            this.repo = repository;
        }

        /// <summary>Handles the specified count all holdings asynchronous.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<int> Handle(CountAllHoldingsAsync cmd, CancellationToken cancellationToken)
        {
            return await this.repo.CountAllByDataTable(cmd.ShowAllEditions, cmd.EditionId);
        }
    }
}