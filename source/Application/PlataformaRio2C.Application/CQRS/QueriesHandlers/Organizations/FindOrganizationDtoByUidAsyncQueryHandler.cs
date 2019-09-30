// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-30-2019
// ***********************************************************************
// <copyright file="FindOrganizationDtoByUidAsyncQueryHandler.cs" company="Softo">
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
    /// <summary>FindOrganizationDtoByUidAsyncQueryHandler</summary>
    public class FindOrganizationDtoByUidAsyncQueryHandler : IRequestHandler<FindOrganizationDtoByUidAsync, OrganizationDto>
    {
        private readonly IOrganizationRepository repo;

        /// <summary>Initializes a new instance of the <see cref="FindOrganizationDtoByUidAsyncQueryHandler"/> class.</summary>
        /// <param name="repository">The repository.</param>
        public FindOrganizationDtoByUidAsyncQueryHandler(IOrganizationRepository repository)
        {
            this.repo = repository;
        }

        /// <summary>Handles the specified find organization dto by uid asynchronous.</summary>
        /// <param name="cmd"></param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<OrganizationDto> Handle(FindOrganizationDtoByUidAsync cmd, CancellationToken cancellationToken)
        {
            return await this.repo.FindDtoByUidAsync(cmd.OrganizationUid, cmd.EditionId);
        }
    }
}