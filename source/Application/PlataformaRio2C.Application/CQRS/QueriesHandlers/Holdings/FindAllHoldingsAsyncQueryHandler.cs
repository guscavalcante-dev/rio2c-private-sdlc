// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-09-2019
// ***********************************************************************
// <copyright file="FindAllHoldingsAsyncQueryHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Interfaces;
using X.PagedList;

namespace PlataformaRio2C.Application.CQRS.QueriesHandlers
{
    /// <summary>FindAllHoldingsAsyncQueryHandler</summary>
    public class FindAllHoldingsAsyncQueryHandler : IRequestHandler<FindAllHoldingsAsync, IPagedList<HoldingListDto>>
    {
        private readonly IHoldingRepository repo;

        /// <summary>Initializes a new instance of the <see cref="FindAllHoldingsAsyncQueryHandler"/> class.</summary>
        /// <param name="holdingRepository">The holding repository.</param>
        public FindAllHoldingsAsyncQueryHandler(IHoldingRepository holdingRepository)
        {
            this.repo = holdingRepository;
        }

        /// <summary>Handles the specified find all holdings asynchronous.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<IPagedList<HoldingListDto>> Handle(FindAllHoldingsAsync cmd, CancellationToken cancellationToken)
        {
            return await this.repo.FindAllByDataTable(cmd.Page, cmd.PageSize, cmd.Keywords, cmd.SortColumns, cmd.ShowAllEditions, cmd.EditionUid);
        }
    }
}