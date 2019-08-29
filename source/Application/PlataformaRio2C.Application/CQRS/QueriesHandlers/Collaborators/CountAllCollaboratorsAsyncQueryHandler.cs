// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-27-2019
// ***********************************************************************
// <copyright file="CountAllCollaboratorsAsyncQueryHandler.cs" company="Softo">
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
    /// <summary>CountAllCollaboratorsAsyncQueryHandler</summary>
    public class CountAllCollaboratorsAsyncQueryHandler : IRequestHandler<CountAllCollaboratorsAsync, int>
    {
        private readonly ICollaboratorRepository repo;

        /// <summary>Initializes a new instance of the <see cref="CountAllCollaboratorsAsyncQueryHandler"/> class.</summary>
        /// <param name="repository">The repository.</param>
        public CountAllCollaboratorsAsyncQueryHandler(ICollaboratorRepository repository)
        {
            this.repo = repository;
        }

        /// <summary>Handles the specified count all collaborators asynchronous.</summary>
        /// <param name="cmd">The comman.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<int> Handle(CountAllCollaboratorsAsync cmd, CancellationToken cancellationToken)
        {
            return await this.repo.CountAllByDataTable(cmd.OrganizationTypeId, cmd.ShowAllEditions, cmd.EditionId);
        }
    }
}