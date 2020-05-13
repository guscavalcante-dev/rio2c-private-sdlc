// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 05-13-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 05-13-2020
// ***********************************************************************
// <copyright file="CreateConnectionCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.HubApplication.CQRS.Commands;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.HubApplication.CQRS.CommandsHandlers
{
    /// <summary>CreateConnectionCommandHandler</summary>
    public class CreateConnectionCommandHandler : ConnectionBaseCommandHandler, IRequestHandler<CreateConnection, AppValidationResult>
    {
        private readonly IUserRepository userRepo;

        /// <summary>Initializes a new instance of the <see cref="CreateConnectionCommandHandler" /> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="connectionRepository">The connection repository.</param>
        /// <param name="userRepository">The user repository.</param>
        public CreateConnectionCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IConnectionRepository connectionRepository,
            IUserRepository userRepository)
            : base(eventBus, uow, connectionRepository)
        {
            this.userRepo = userRepository;
        }

        /// <summary>Handles the specified create connection.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateConnection cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var connection = new Connection(
                cmd.ConnectionId,
                await this.userRepo.FindByUserNameAsync(cmd.UserName),
                cmd.UserAgent);
            if (!connection.IsValid())
            {
                this.AppValidationResult.Add(connection.ValidationResult);
                return this.AppValidationResult;
            }

            this.ConnectionRepo.Create(connection);
            this.Uow.SaveChanges();

            return this.AppValidationResult;
        }
    }
}