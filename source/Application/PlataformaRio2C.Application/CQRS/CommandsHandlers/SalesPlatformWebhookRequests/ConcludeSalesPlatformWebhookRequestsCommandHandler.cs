// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 07-22-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-22-2019
// ***********************************************************************
// <copyright file="ConcludeSalesPlatformWebhookRequestsCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>ConcludeSalesPlatformWebhookRequestsCommandHandler</summary>
    public class ConcludeSalesPlatformWebhookRequestsCommandHandler : IRequestHandler<ConcludeSalesPlatformWebhookRequest, Guid>
    {
        private readonly IMediator eventBus;
        private readonly IUnitOfWork uow;
        private readonly ISalesPlatformWebhookRequestRepository salesPlatformWebhookRequestRepo;

        /// <summary>Initializes a new instance of the <see cref="ConcludeSalesPlatformWebhookRequestsCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="salesPlatformWebhookRequestRepository">The sales platform webhook request repository.</param>
        public ConcludeSalesPlatformWebhookRequestsCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ISalesPlatformWebhookRequestRepository salesPlatformWebhookRequestRepository)
        {
            this.eventBus = eventBus;
            this.uow = uow;
            this.salesPlatformWebhookRequestRepo = salesPlatformWebhookRequestRepository;
        }

        /// <summary>Handles the specified command.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Guid> Handle(ConcludeSalesPlatformWebhookRequest cmd, CancellationToken cancellationToken)
        {
            this.uow.BeginTransaction();

            ////Validation
            //AssertionConcern.AssertArgumentNotEmpty(obj.Title, "O Titulo não pode ser vazio");
            //AssertionConcern.AssertArgumentNotEmpty(obj.Localization, "O Localização não pode ser vazio");
            //AssertionConcern.AssertArgumentNotEmpty(obj.Description, "O Descrição não pode ser vazio");
            //AssertionConcern.AssertArgumentNotNull(obj.Active, "O Ativo não pode ser vazio");
            //AssertionConcern.AssertArgumentNotEmpty(obj.Serial, "O Serial não pode ser vazio");
            //AssertionConcern.AssertArgumentFalse(_repository.HasExists(obj.Serial), "Ja existe cadastro com esse serial");

            var salesPlatformWebhooRequest = await this.salesPlatformWebhookRequestRepo.GetAsync(cmd.SalesPlatformWebhookRequestUid);
            if (salesPlatformWebhooRequest == null)
            {
                throw new DomainException("Sales platform webhook request not found.");
            }

            salesPlatformWebhooRequest.Conclude(cmd.SecurityStamp);

            this.uow.SaveChanges();

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            return cmd.SalesPlatformWebhookRequestUid;

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}