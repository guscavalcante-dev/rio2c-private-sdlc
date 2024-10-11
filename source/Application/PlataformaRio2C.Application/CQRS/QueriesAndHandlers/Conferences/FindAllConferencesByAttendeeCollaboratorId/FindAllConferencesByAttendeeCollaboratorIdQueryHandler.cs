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
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>
    /// FindAllConferencesByAttendeeCollaboratorIdQueryHandler
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler&lt;PlataformaRio2C.Application.CQRS.Queries.FindAllConferencesByAttendeeCollaboratorIdQuery, System.Int32&gt;" />
    public class FindAllConferencesByAttendeeCollaboratorIdQueryHandler : IRequestHandler<FindAllConferencesByAttendeeCollaboratorIdQuery, FindAllConferencesByAttendeeCollaboratorIdResponseDto>
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
        public async Task<FindAllConferencesByAttendeeCollaboratorIdResponseDto> Handle(FindAllConferencesByAttendeeCollaboratorIdQuery cmd, CancellationToken cancellationToken)
        {
            return new FindAllConferencesByAttendeeCollaboratorIdResponseDto(
                await this.repo.FindAllByAttendeeCollaboratorIdAsync(cmd.AttendeeCollaboratorId));
        }
    }
}