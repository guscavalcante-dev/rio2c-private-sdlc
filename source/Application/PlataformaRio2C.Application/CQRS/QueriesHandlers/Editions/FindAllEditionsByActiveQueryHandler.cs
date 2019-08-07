// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-07-2019
// ***********************************************************************
// <copyright file="FindEditionsByCurrentQueryHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using MediatR;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Application.Dtos;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Application.CQRS.QueriesHandlers
{
    /// <summary>FindAllEditionsByActiveQueryHandler</summary>
    public class FindAllEditionsByActiveQueryHandler : RequestHandler<FindAllEditionsByIsActive, List<EditionDto>>
    {
        private readonly IEditionRepository repo;

        /// <summary>Initializes a new instance of the <see cref="FindAllEditionsByActiveQueryHandler"/> class.</summary>
        /// <param name="editionRepository">The edition repository.</param>
        public FindAllEditionsByActiveQueryHandler(IEditionRepository editionRepository)
        {
            this.repo = editionRepository;
        }

        /// <summary>Handles the specified command.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        protected override List<EditionDto> Handle(FindAllEditionsByIsActive cmd)
        {
            var edition = this.repo.FindAllByIsActive(cmd.IsActive);
            return edition?.Select(e => new EditionDto(e))?.ToList();
        }
    }
}