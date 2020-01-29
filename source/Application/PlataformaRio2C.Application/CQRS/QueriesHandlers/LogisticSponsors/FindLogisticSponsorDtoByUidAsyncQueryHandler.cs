// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-28-2019
// ***********************************************************************
// <copyright file="FindCollaboratorDtoByUidAndByEditionIdAsyncQueryHandler.cs" company="Softo">
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
    /// <summary>FindCollaboratorDtoByUidAndByEditionIdAsyncQueryHandler</summary>
    public class FindLogisticSponsorDtoByUidAsyncQueryHandler : IRequestHandler<FindLogisticSponsorDtoByUid, LogisticSponsorBaseDto>
    {
        private readonly ILogisticSponsorRepository repo;

        /// <summary>Initializes a new instance of the <see cref="FindCollaboratorDtoByUidAndByEditionIdAsyncQueryHandler"/> class.</summary>
        /// <param name="repository">The repository.</param>
        public FindLogisticSponsorDtoByUidAsyncQueryHandler(ILogisticSponsorRepository repository)
        {
            this.repo = repository;
        }

        /// <summary>Handles the specified find collaborator dto by uid and by edition identifier asynchronous.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<LogisticSponsorBaseDto> Handle(FindLogisticSponsorDtoByUid cmd, CancellationToken cancellationToken)
        {
            return await this.repo.FindLogisticSponsorDtoByUid(cmd.SponsorUid);
        }
    }
}