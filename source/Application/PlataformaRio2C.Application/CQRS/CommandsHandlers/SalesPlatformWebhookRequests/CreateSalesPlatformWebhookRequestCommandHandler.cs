// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 07-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-31-2019
// ***********************************************************************
// <copyright file="CreateSalesPlatformWebhookRequestCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateSalesPlatformWebhookRequestCommandHandler</summary>
    public class CreateSalesPlatformWebhookRequestCommandHandler : IRequestHandler<CreateSalesPlatformWebhookRequest, Guid?>
    {
        private readonly IMediator eventBus;
        private readonly IUnitOfWork uow;
        private readonly ISalesPlatformWebhookRequestRepository salesPlatformWebhookRequestRepo;
        private readonly ISalesPlatformRepository salesPlatformRepo;

        /// <summary>Initializes a new instance of the <see cref="CreateSalesPlatformWebhookRequestCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="salesPlatformWebhookRequestRepository">The sales platform webhook request repository.</param>
        /// <param name="salesPlatformRepository">The sales platform repository.</param>
        public CreateSalesPlatformWebhookRequestCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ISalesPlatformWebhookRequestRepository salesPlatformWebhookRequestRepository,
            ISalesPlatformRepository salesPlatformRepository)
        {
            this.eventBus = eventBus;
            this.uow = uow;
            this.salesPlatformWebhookRequestRepo = salesPlatformWebhookRequestRepository;
            this.salesPlatformRepo = salesPlatformRepository;
        }

        /// <summary>Handles the specified create sales platform webhook request.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Guid?> Handle(CreateSalesPlatformWebhookRequest cmd, CancellationToken cancellationToken)
        {
            this.uow.BeginTransaction();            

            var salesPlatform = await this.salesPlatformRepo.FindByNameAsync(cmd.SalesPlatformName);

            var salesPlatformWebhookRequest = new SalesPlatformWebhookRequest(
                cmd.SalesPlatformWebhookRequestUid,
                salesPlatform,
                "7A8C7EDC-3084-47D5-AD5A-DF6A128B341C",//"8ed95423-0c36-4dc8-bf86-76aa79882b20", //cmd.WebhookSecurityKey,
                cmd.Endpoint,
                cmd.Header,
                cmd.Payload,
                cmd.IpAddress);

            this.salesPlatformWebhookRequestRepo.Create(salesPlatformWebhookRequest);
            this.uow.SaveChanges();

            return cmd.SalesPlatformWebhookRequestUid;
        }
    }
}