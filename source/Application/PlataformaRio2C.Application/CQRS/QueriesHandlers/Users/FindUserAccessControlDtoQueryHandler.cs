// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-04-2019
// ***********************************************************************
// <copyright file="FindUserAccessControlDtoQueryHandler.cs" company="Softo">
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
    /// <summary>FindUserAccessControlDtoQueryHandler</summary>
    public class FindUserAccessControlDtoQueryHandler : RequestHandler<FindUserAccessControlDto, UserAccessControlDto>
    {
        private readonly IUserRepository repo;

        /// <summary>Initializes a new instance of the <see cref="FindUserAccessControlDtoQueryHandler"/> class.</summary>
        /// <param name="repository">The repository.</param>
        public FindUserAccessControlDtoQueryHandler(IUserRepository repository)
        {
            this.repo = repository;
        }

        /// <summary>Handles the specified find user access control dto.</summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        protected override UserAccessControlDto Handle(FindUserAccessControlDto cmd)
        {
            return this.repo.FindAccessControlDtoByUserIdAndByEditionId(cmd.UserId, cmd.EditionId);
        }
    }
}