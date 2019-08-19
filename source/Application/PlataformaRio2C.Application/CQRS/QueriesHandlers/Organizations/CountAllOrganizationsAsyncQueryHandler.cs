// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="CountAllOrganizationsAsyncQueryHandler.cs" company="Softo">
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
    /// <summary>CountAllOrganizationsAsyncQueryHandler</summary>
    public class CountAllOrganizationsAsyncQueryHandler : IRequestHandler<CountAllOrganizationsAsync, int>
    {
        private readonly IOrganizationRepository repo;

        /// <summary>Initializes a new instance of the <see cref="CountAllOrganizationsAsyncQueryHandler"/> class.</summary>
        /// <param name="repository">The repository.</param>
        public CountAllOrganizationsAsyncQueryHandler(IOrganizationRepository repository)
        {
            this.repo = repository;
        }

        /// <summary>Handles the specified count all organizations asynchronous.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<int> Handle(CountAllOrganizationsAsync cmd, CancellationToken cancellationToken)
        {
            return await this.repo.CountAllByDataTable(cmd.OrganizationTypeId, cmd.ShowAllEditions, cmd.EditionId);
        }
    }
}