// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-24-2021
// ***********************************************************************
// <copyright file="ReleasedMusicProjectBaseCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>ReleasedMusicProjectBaseCommandHandler</summary>
    public class ReleasedMusicProjectBaseCommandHandler : BaseCommandHandler
    {
        protected readonly IReleasedMusicProjectRepository ReleasedMusicProjectRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReleasedMusicProjectBaseCommandHandler"/> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="releasedMusicProjectRepo">The released music project repo.</param>
        public ReleasedMusicProjectBaseCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IReleasedMusicProjectRepository releasedMusicProjectRepo)
            : base(eventBus, uow)
        {
            this.ReleasedMusicProjectRepo = releasedMusicProjectRepo;
        }
    }
}