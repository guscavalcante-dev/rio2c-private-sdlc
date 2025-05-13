// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 07-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-31-2019
// ***********************************************************************
// <copyright file="FindSalesPlatformDtoByNameQueryHandler.cs" company="Softo">
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

namespace PlataformaRio2C.Application.CQRS.QueriesHandlers
{
    /// <summary>FindSalesPlatformDtoByNameQueryHandler</summary>
    public class FindSalesPlatformDtoByNameQueryHandler : IRequestHandler<FindSalesPlatformDtoByName, SalesPlatformDto>
    {
        private readonly ISalesPlatformRepository repo;

        /// <summary>Initializes a new instance of the <see cref="FindSalesPlatformDtoByNameQueryHandler"/> class.</summary>
        /// <param name="salesPlatformRepository">The sales platform repository.</param>
        public FindSalesPlatformDtoByNameQueryHandler(ISalesPlatformRepository salesPlatformRepository)
        {
            this.repo = salesPlatformRepository;
        }

        /// <summary>Handles the specified find sales platform dto by name.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<SalesPlatformDto> Handle(FindSalesPlatformDtoByName cmd, CancellationToken cancellationToken)
        {
            return await this.repo.FindDtoByNameAsync(cmd.Name);
        }
    }
}