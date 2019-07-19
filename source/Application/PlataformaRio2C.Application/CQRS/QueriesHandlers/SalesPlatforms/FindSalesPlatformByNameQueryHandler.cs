// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 07-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-19-2019
// ***********************************************************************
// <copyright file="FindSalesPlatformByNameQueryHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Application.CQRS.QueriesHandlers
{
    /// <summary>FindSalesPlatformByNameQueryHandler</summary>
    public class FindSalesPlatformByNameQueryHandler : IRequestHandler<FindSalesPlatformByName, SalesPlatformBaseViewModel>
    {
        private readonly ISalesPlatformRepository repo;

        public FindSalesPlatformByNameQueryHandler(ISalesPlatformRepository salesPlatformRepository)
        {
            this.repo = salesPlatformRepository;
        }

        /// <summary>Handles the specified create sales platform webhook request.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<SalesPlatformBaseViewModel> Handle(FindSalesPlatformByName cmd, CancellationToken cancellationToken)
        {
            var salesplatform = await this.repo.GetByNameAsync(cmd.Name);
            if (salesplatform == null)
            {
                return null;
            }

            return new SalesPlatformBaseViewModel(salesplatform);
        }
    }
}