// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-22-2019
// ***********************************************************************
// <copyright file="FindAllOrganizationsBaseDtosAsyncQueryHandler.cs" company="Softo">
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
    /// <summary>FindAllOrganizationsBaseDtosAsyncQueryHandler</summary>
    public class FindAllOrganizationsBaseDtosAsyncQueryHandler : IRequestHandler<FindAllOrganizationsBaseDtosAsync, IPagedList<OrganizationBaseDto>>
    {
        private readonly IOrganizationRepository repo;

        /// <summary>Initializes a new instance of the <see cref="FindAllOrganizationsBaseDtosAsyncQueryHandler"/> class.</summary>
        /// <param name="repository">The repository.</param>
        public FindAllOrganizationsBaseDtosAsyncQueryHandler(IOrganizationRepository repository)
        {
            this.repo = repository;
        }

        /// <summary>Handles the specified find all organizations base dtos asynchronous.</summary>
        /// <param name="cmd"></param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<IPagedList<OrganizationBaseDto>> Handle(FindAllOrganizationsBaseDtosAsync cmd, CancellationToken cancellationToken)
        {
            return await this.repo.FindAllByDataTable(cmd.Page, cmd.PageSize, cmd.Keywords, cmd.SortColumns, cmd.OrganizationTypeId, cmd.ShowAllEditions, cmd.ShowAllOrganizations, cmd.EditionId);
        }
    }
}