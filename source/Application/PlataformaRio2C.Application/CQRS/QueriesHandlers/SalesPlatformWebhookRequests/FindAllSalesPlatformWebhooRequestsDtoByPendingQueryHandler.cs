// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 07-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-31-2019
// ***********************************************************************
// <copyright file="FindAllSalesPlatformWebhooRequestsByPendingQueryHandler.cs" company="Softo">
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
    /// <summary>FindAllSalesPlatformWebhooRequestsDtoByPendingQueryHandlerQueryHandler</summary>
    public class FindAllSalesPlatformWebhooRequestsDtoByPendingQueryHandlerQueryHandler : IRequestHandler<FindAllSalesPlatformWebhooRequestsDtoByPending, List<SalesPlatformWebhookRequestDto>>
    {
        private readonly ISalesPlatformWebhookRequestRepository repo;

        /// <summary>Initializes a new instance of the <see cref="FindAllSalesPlatformWebhooRequestsDtoByPendingQueryHandlerQueryHandler"/> class.</summary>
        /// <param name="salesPlatformWebhookRequestRepository">The sales platform webhook request repository.</param>
        public FindAllSalesPlatformWebhooRequestsDtoByPendingQueryHandlerQueryHandler(ISalesPlatformWebhookRequestRepository salesPlatformWebhookRequestRepository)
        {
            this.repo = salesPlatformWebhookRequestRepository;
        }

        /// <summary>Handles the specified find all sales platform webhoo requests dto by pending.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<List<SalesPlatformWebhookRequestDto>> Handle(FindAllSalesPlatformWebhooRequestsDtoByPending cmd, CancellationToken cancellationToken)
        {
            return await this.repo.FindAllDtoByPendingAsync();
        }
    }
}