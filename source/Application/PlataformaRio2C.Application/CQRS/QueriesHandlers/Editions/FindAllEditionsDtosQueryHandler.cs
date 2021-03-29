// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-06-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-26-2021
// ***********************************************************************
// <copyright file="FindAllEditionsDtosQueryHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Application.CQRS.QueriesHandlers
{
    /// <summary>FindAllEditionsDtosQueryHandler</summary>
    public class FindAllEditionsDtosQueryHandler : RequestHandler<FindAllEditionsDtosAsync, List<EditionDto>>
    {
        private readonly IEditionRepository repo;

        /// <summary>Initializes a new instance of the <see cref="FindAllEditionsDtosQueryHandler"/> class.</summary>
        /// <param name="editionRepository">The edition repository.</param>
        public FindAllEditionsDtosQueryHandler(IEditionRepository editionRepository)
        {
            this.repo = editionRepository;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        protected override List<EditionDto> Handle(FindAllEditionsDtosAsync request)
        {
            var editions = this.repo.FindAllByIsActive(request.ShowInactive);

            return editions?
                .Select(e => new EditionDto(e))
                .OrderByDescending(e => e.StartDate)
                .ToList();
        }
    }
}