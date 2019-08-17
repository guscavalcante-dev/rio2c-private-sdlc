// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-16-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-16-2019
// ***********************************************************************
// <copyright file="FindHoldingByUidAsyncQueryHandler.cs" company="Softo">
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

namespace PlataformaRio2C.Application.CQRS.QueriesHandlers
{
    /// <summary>FindHoldingByUidAsyncQueryHandler</summary>
    public class FindHoldingByUidAsyncQueryHandler : IRequestHandler<FindHoldingByUidAsync, HoldingDto>
    {
        private readonly IHoldingRepository repo;

        /// <summary>Initializes a new instance of the <see cref="FindHoldingByUidAsyncQueryHandler"/> class.</summary>
        /// <param name="holdingRepository">The holding repository.</param>
        public FindHoldingByUidAsyncQueryHandler(IHoldingRepository holdingRepository)
        {
            this.repo = holdingRepository;
        }

        /// <summary>Handles the specified find holding by uid asynchronous.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<HoldingDto> Handle(FindHoldingByUidAsync cmd, CancellationToken cancellationToken)
        {
            return await this.repo.FindDtoByUidAsync(cmd.HoldingUid);
        }
    }
}