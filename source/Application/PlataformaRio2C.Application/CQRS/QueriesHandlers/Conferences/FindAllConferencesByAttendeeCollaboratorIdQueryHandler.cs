// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 10-01-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-01-2024
// ***********************************************************************
// <copyright file="FindAllConferencesByAttendeeCollaboratorIdQueryHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Dtos;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Application.CQRS.QueryHandlers
{
    /// <summary>
    /// FindAllConferencesByAttendeeCollaboratorIdQueryHandler
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler&lt;PlataformaRio2C.Application.CQRS.Queries.FindAllConferencesByAttendeeCollaboratorId, System.Int32&gt;" />
    public class FindAllConferencesByAttendeeCollaboratorIdQueryHandler : IRequestHandler<FindAllConferencesByAttendeeCollaboratorId, List<ConferenceDto>>
    {
        private readonly IConferenceRepository repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="FindAllConferencesByAttendeeCollaboratorIdQueryHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public FindAllConferencesByAttendeeCollaboratorIdQueryHandler(IConferenceRepository repository)
        {
            this.repo = repository;
        }

        /// <summary>Handles the specified count all organizations asynchronous.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<List<ConferenceDto>> Handle(FindAllConferencesByAttendeeCollaboratorId cmd, CancellationToken cancellationToken)
        {
            return await this.repo.FindAllByAttendeeCollaboratorIdAsync(cmd.AttendeeCollaboratorId);
        }
    }
}