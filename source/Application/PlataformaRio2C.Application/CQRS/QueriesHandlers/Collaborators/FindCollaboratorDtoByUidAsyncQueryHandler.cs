// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-26-2019
// ***********************************************************************
// <copyright file="FindCollaboratorDtoByUidAsyncQueryHandler.cs" company="Softo">
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
    /// <summary>FindCollaboratorDtoByUidAsyncQueryHandler</summary>
    public class FindCollaboratorDtoByUidAsyncQueryHandler : IRequestHandler<FindCollaboratorDtoByUidAsync, CollaboratorDto>
    {
        private readonly IOrganizationRepository repo;

        /// <summary>Initializes a new instance of the <see cref="FindCollaboratorDtoByUidAsyncQueryHandler"/> class.</summary>
        /// <param name="repository">The repository.</param>
        public FindCollaboratorDtoByUidAsyncQueryHandler(IOrganizationRepository repository)
        {
            this.repo = repository;
        }

        /// <summary>Handles the specified find collaborator dto by uid asynchronous.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<CollaboratorDto> Handle(FindCollaboratorDtoByUidAsync cmd, CancellationToken cancellationToken)
        {
            return null;
            //return await this.repo.FindDtoByUidAsync(cmd.HoldingUid);
        }
    }
}