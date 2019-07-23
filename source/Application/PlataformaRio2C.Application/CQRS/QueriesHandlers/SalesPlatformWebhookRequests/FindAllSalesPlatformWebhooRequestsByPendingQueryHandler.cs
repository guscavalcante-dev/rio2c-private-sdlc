// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 07-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-19-2019
// ***********************************************************************
// <copyright file="FindAllSalesPlatformWebhooRequestsByPendingQueryHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Application.Dtos;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Application.CQRS.QueriesHandlers
{
    /// <summary>FindAllSalesPlatformWebhooRequestsByPendingQueryHandler</summary>
    public class FindAllSalesPlatformWebhooRequestsByPendingQueryHandler : IRequestHandler<FindAllSalesPlatformWebhooRequestsByPending, List<SalesPlatformWebhookRequestDto>>
    {
        private readonly ISalesPlatformWebhookRequestRepository repo;

        /// <summary>Initializes a new instance of the <see cref="FindAllSalesPlatformWebhooRequestsByPendingQueryHandler"/> class.</summary>
        /// <param name="salesPlatformWebhookRequestRepository">The sales platform webhook request repository.</param>
        public FindAllSalesPlatformWebhooRequestsByPendingQueryHandler(ISalesPlatformWebhookRequestRepository salesPlatformWebhookRequestRepository)
        {
            this.repo = salesPlatformWebhookRequestRepository;
        }

        /// <summary>Handles the specified command.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<List<SalesPlatformWebhookRequestDto>> Handle(FindAllSalesPlatformWebhooRequestsByPending cmd, CancellationToken cancellationToken)
        {
            var pendingWebhookRequests = await this.repo.GetAllByPendingAsync();

            return pendingWebhookRequests?.Select(m => new SalesPlatformWebhookRequestDto(m))?.ToList();
        }
    }
}