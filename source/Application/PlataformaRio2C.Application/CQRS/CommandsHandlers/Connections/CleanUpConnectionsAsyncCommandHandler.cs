// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 05-13-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 05-13-2020
// ***********************************************************************
// <copyright file="CleanUpConnectionsAsyncCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CleanUpConnectionsAsyncCommandHandler</summary>
    public class CleanUpConnectionsAsyncCommandHandler : BaseCommandHandler, IRequestHandler<CleanUpConnectionsAsync, AppValidationResult>
    {
        private readonly IConnectionRepository connectionRepo;

        /// <summary>Initializes a new instance of the <see cref="CleanUpConnectionsAsyncCommandHandler" /> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="connectionRepository">The connection repository.</param>
        public CleanUpConnectionsAsyncCommandHandler(
            IMediator commandBus,
            IUnitOfWork uow,
            IConnectionRepository connectionRepository)
            : base(commandBus, uow)
        {
            this.connectionRepo = connectionRepository;
        }

        /// <summary>Handles the specified clean up connections asynchronous.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CleanUpConnectionsAsync cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            this.connectionRepo.CleanUp();

            this.Uow.SaveChanges();

            return this.AppValidationResult;
        }
    }
}