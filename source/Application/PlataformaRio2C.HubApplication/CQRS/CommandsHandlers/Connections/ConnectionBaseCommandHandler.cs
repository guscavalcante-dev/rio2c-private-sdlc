// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 05-13-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 05-13-2020
// ***********************************************************************
// <copyright file="ConnectionBaseCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.CommandsHandlers;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Threading.Tasks;

namespace PlataformaRio2C.HubApplication.CQRS.CommandsHandlers
{
    /// <summary>ConnectionBaseCommandHandler</summary>
    public class ConnectionBaseCommandHandler : BaseCommandHandler
    {
        protected readonly IConnectionRepository ConnectionRepo;

        /// <summary>Initializes a new instance of the <see cref="ConnectionBaseCommandHandler" /> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="connectionRepository">The connection repository.</param>
        public ConnectionBaseCommandHandler(IMediator eventBus, IUnitOfWork uow, IConnectionRepository connectionRepository)
            : base(eventBus, uow)
        {
            this.ConnectionRepo = connectionRepository;
        }

        /// <summary>Gets the connection by connection identifier.</summary>
        /// <param name="connectionId">The connection identifier.</param>
        /// <returns></returns>
        public async Task<Connection> GetConnectionByConnectionId(Guid connectionId)
        {
            var connection = await this.ConnectionRepo.GetAsync(c => c.ConnectionId == connectionId);
            if (connection == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, "Connection", Labels.FoundF), new string[] { "ToastrError" }));
                return null;
            }

            return connection;
        }
    }
}