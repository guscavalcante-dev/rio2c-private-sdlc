// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-27-2019
// ***********************************************************************
// <copyright file="FindAdminAccessControlDtoQueryHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Application.CQRS.QueriesHandlers
{
    /// <summary>FindAdminAccessControlDtoQueryHandler</summary>
    public class FindAdminAccessControlDtoQueryHandler : RequestHandler<FindAdminAccessControlDto, AdminAccessControlDto>
    {
        private readonly IUserRepository repo;

        /// <summary>Initializes a new instance of the <see cref="FindAdminAccessControlDtoQueryHandler"/> class.</summary>
        /// <param name="repository">The repository.</param>
        public FindAdminAccessControlDtoQueryHandler(IUserRepository repository)
        {
            this.repo = repository;
        }

        /// <summary>Handles the specified find admin access control dto.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        protected override AdminAccessControlDto Handle(FindAdminAccessControlDto cmd)
        {
            return this.repo.FindAdminAccessControlDtoByUserIdAndByEditionId(cmd.UserId, cmd.EditionId);
        }
    }
}