// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 05-13-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 05-13-2020
// ***********************************************************************
// <copyright file="DeleteConnectionCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.HubApplication.CQRS.Commands;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.HubApplication.CQRS.CommandsHandlers
{
    /// <summary>DeleteConnectionCommandHandler</summary>
    public class DeleteConnectionCommandHandler : ConnectionBaseCommandHandler, IRequestHandler<DeleteConnection, AppValidationResult>
    {
        private readonly IUserRepository userRepo;

        /// <summary>Initializes a new instance of the <see cref="DeleteConnectionCommandHandler" /> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="connectionRepository">The connection repository.</param>
        /// <param name="userRepository">The user repository.</param>
        public DeleteConnectionCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IConnectionRepository connectionRepository,
            IUserRepository userRepository)
            : base(eventBus, uow, connectionRepository)
        {
            this.userRepo = userRepository;
        }

        /// <summary>Handles the specified delete connection.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(DeleteConnection cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var connection = await this.GetConnectionByConnectionId(cmd.ConnectionId);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            this.ConnectionRepo.Delete(connection);
            this.Uow.SaveChanges();

            return this.AppValidationResult;
        }
    }
}