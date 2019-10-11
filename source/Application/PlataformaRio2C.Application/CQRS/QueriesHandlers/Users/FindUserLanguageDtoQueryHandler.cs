// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : William Almado
// Created          : 10-09-2019
//
// Last Modified By : William Almado
// Last Modified On : 09-27-2019
// ***********************************************************************
// <copyright file="FindUserLanguageDtoQueryHandler.cs" company="Softo">
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
    /// <summary>FindUserAccessControlDtoQueryHandler</summary>
    public class FindUserLanguageDtoQueryHandler : RequestHandler<FindUserLanguageDto, UserLanguageDto>
    {
        private readonly IUserRepository repo;

        /// <summary>Initializes a new instance of the <see cref="FindUserAccessControlDtoQueryHandler"/> class.</summary>
        /// <param name="repository">The repository.</param>
        public FindUserLanguageDtoQueryHandler(IUserRepository repository)
        {
            this.repo = repository;
        }

        /// <summary>Handles the specified find user access control dto.</summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        protected override UserLanguageDto Handle(FindUserLanguageDto cmd)
        {
            return this.repo.FindUserLanguageByUserId(cmd.UserId);
        }
    }
}