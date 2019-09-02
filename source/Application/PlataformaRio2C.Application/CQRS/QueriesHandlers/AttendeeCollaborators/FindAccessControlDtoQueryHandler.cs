// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-02-2019
// ***********************************************************************
// <copyright file="FindAccessControlDtoQueryHandler.cs" company="Softo">
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
    /// <summary>FindAccessControlDtoQueryHandler</summary>
    public class FindAccessControlDtoQueryHandler : RequestHandler<FindAccessControlDto, AccessControlAttendeeCollaboratorDto>
    {
        private readonly IAttendeeCollaboratorRepository repo;

        /// <summary>Initializes a new instance of the <see cref="FindAccessControlDtoQueryHandler"/> class.</summary>
        /// <param name="repository">The repository.</param>
        public FindAccessControlDtoQueryHandler(IAttendeeCollaboratorRepository repository)
        {
            this.repo = repository;
        }

        /// <summary>Handles the specified command.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        protected override AccessControlAttendeeCollaboratorDto Handle(FindAccessControlDto cmd)
        {
            return this.repo.FindAccessDtoByEditionUidAndByUserId(cmd.EditionUid, cmd.UserId);
        }
    }
}