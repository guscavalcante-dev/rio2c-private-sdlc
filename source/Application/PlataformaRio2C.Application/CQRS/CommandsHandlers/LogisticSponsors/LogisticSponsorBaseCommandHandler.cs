// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-28-2020
//
// Last Modified By : Arthur Souza
// Last Modified On : 01-28-2020
// ***********************************************************************
// <copyright file="TrackBaseCommandHandler.cs" company="Softo">
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
    /// <summary>LogisticSponsorBaseCommandHandler</summary>
    public class LogisticSponsorBaseCommandHandler : BaseCommandHandler
    {
        protected readonly ILogisticSponsorRepository repository;

        /// <summary>Initializes a new instance of the <see cref="TrackBaseCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="trackRepository">The track repository.</param>
        public LogisticSponsorBaseCommandHandler(IMediator eventBus, IUnitOfWork uow, ILogisticSponsorRepository logisticSponsorRepository)
            : base(eventBus, uow)
        {
            this.repository = logisticSponsorRepository;
        }
    }
}