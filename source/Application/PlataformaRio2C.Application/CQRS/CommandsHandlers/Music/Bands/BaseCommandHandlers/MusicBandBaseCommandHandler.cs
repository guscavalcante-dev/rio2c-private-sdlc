// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 23-03-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 23-03-2021
// ***********************************************************************
// <copyright file="MusicBandBaseCommandHandler.cs" company="Softo">
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
    /// <summary>MusicBandBaseCommandHandler</summary>
    public class MusicBandBaseCommandHandler : BaseCommandHandler
    {
        protected readonly IMusicBandRepository MusicBandRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBandBaseCommandHandler"/> class.
        /// </summary>
        /// <param name="commandBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="musicBandRepository">The music band repository.</param>
        public MusicBandBaseCommandHandler(
            IMediator commandBus, 
            IUnitOfWork uow, 
            IMusicBandRepository musicBandRepository)
            : base(commandBus, uow)
        {
            this.MusicBandRepo = musicBandRepository;
        }
    }
}