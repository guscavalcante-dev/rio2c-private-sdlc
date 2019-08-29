// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-28-2019
// ***********************************************************************
// <copyright file="FindAllAttendeeOrganizationsBaseDtosByEditionUidAsyncQueryHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Application.CQRS.QueriesHandlers
{
    /// <summary>FindAllAttendeeOrganizationsBaseDtosByEditionUidAsyncQueryHandler</summary>
    public class FindAllAttendeeOrganizationsBaseDtosByEditionUidAsyncQueryHandler : IRequestHandler<FindAllAttendeeOrganizationsBaseDtosByEditionUidAsync, List<AttendeeOrganizationBaseDto>>
    {
        private readonly IAttendeeOrganizationRepository repo;

        /// <summary>Initializes a new instance of the <see cref="FindAllAttendeeOrganizationsBaseDtosByEditionUidAsyncQueryHandler"/> class.</summary>
        /// <param name="repository">The repository.</param>
        public FindAllAttendeeOrganizationsBaseDtosByEditionUidAsyncQueryHandler(IAttendeeOrganizationRepository repository)
        {
            this.repo = repository;
        }

        /// <summary>Handles the specified find all attendee organizations base dtos by edition uid asynchronous.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<List<AttendeeOrganizationBaseDto>> Handle(FindAllAttendeeOrganizationsBaseDtosByEditionUidAsync cmd, CancellationToken cancellationToken)
        {
            return await this.repo.FindAllBaseDtosByEditionUidAsync(cmd.EditionId, cmd.ShowAllEditions);
        }
    }
}