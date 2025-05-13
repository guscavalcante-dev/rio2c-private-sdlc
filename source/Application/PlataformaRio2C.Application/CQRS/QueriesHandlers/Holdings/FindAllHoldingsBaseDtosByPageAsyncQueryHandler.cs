// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="FindAllHoldingsBaseDtosByPageAsyncQueryHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using X.PagedList;

namespace PlataformaRio2C.Application.CQRS.QueriesHandlers
{
    /// <summary>FindAllHoldingsBaseDtosByPageAsyncQueryHandler</summary>
    public class FindAllHoldingsBaseDtosByPageAsyncQueryHandler : IRequestHandler<FindAllHoldingsBaseDtosByPageAsync, IPagedList<HoldingBaseDto>>
    {
        private readonly IHoldingRepository repo;

        /// <summary>Initializes a new instance of the <see cref="FindAllHoldingsBaseDtosByPageAsyncQueryHandler"/> class.</summary>
        /// <param name="repository">The repository.</param>
        public FindAllHoldingsBaseDtosByPageAsyncQueryHandler(IHoldingRepository repository)
        {
            this.repo = repository;
        }

        /// <summary>Handles the specified find all holdings base dtos asynchronous.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<IPagedList<HoldingBaseDto>> Handle(FindAllHoldingsBaseDtosByPageAsync cmd, CancellationToken cancellationToken)
        {
            return await this.repo.FindAllBaseDtoByPage(cmd.Page, cmd.PageSize, cmd.Keywords, cmd.SortColumns, cmd.ShowAllEditions, cmd.EditionId);
        }
    }
}