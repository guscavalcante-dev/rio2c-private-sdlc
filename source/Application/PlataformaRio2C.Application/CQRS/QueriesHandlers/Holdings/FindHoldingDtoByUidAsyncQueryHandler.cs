// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-16-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-18-2019
// ***********************************************************************
// <copyright file="FindHoldingDtoByUidAsyncQueryHandler.cs" company="Softo">
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
    /// <summary>FindHoldingDtoByUidAsyncQueryHandler</summary>
    public class FindHoldingDtoByUidAsyncQueryHandler : IRequestHandler<FindHoldingDtoByUidAsync, HoldingDto>
    {
        private readonly IHoldingRepository repo;

        /// <summary>Initializes a new instance of the <see cref="FindHoldingDtoByUidAsyncQueryHandler"/> class.</summary>
        /// <param name="holdingRepository">The holding repository.</param>
        public FindHoldingDtoByUidAsyncQueryHandler(IHoldingRepository holdingRepository)
        {
            this.repo = holdingRepository;
        }

        /// <summary>Handles the specified find holding by uid asynchronous.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<HoldingDto> Handle(FindHoldingDtoByUidAsync cmd, CancellationToken cancellationToken)
        {
            return await this.repo.FindDtoByUidAsync(cmd.HoldingUid);
        }
    }
}