// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-14-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-14-2019
// ***********************************************************************
// <copyright file="FindAllLanguagesAsyncQueryHandler.cs" company="Softo">
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
    /// <summary>FindAllLanguagesAsyncQueryHandler</summary>
    public class FindAllLanguagesAsyncQueryHandler : IRequestHandler<FindAllLanguagesAsync, List<LanguageDto>>
    {
        private readonly ILanguageRepository repo;

        /// <summary>Initializes a new instance of the <see cref="FindAllLanguagesAsyncQueryHandler"/> class.</summary>
        /// <param name="languageRepository">The language repository.</param>
        public FindAllLanguagesAsyncQueryHandler(ILanguageRepository languageRepository)
        {
            this.repo = languageRepository;
        }

        /// <summary>Handles the specified find all languages asynchronous.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<List<LanguageDto>> Handle(FindAllLanguagesAsync cmd, CancellationToken cancellationToken)
        {
            return await this.repo.FindAllAsync();
        }
    }
}