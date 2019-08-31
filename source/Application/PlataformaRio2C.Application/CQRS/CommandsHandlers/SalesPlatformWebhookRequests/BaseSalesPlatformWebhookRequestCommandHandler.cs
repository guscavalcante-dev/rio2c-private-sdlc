// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-31-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-31-2019
// ***********************************************************************
// <copyright file="BaseSalesPlatformWebhookRequestCommandHandler.cs" company="Softo">
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
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>BaseSalesPlatformWebhookRequestCommandHandler</summary>
    public class BaseSalesPlatformWebhookRequestCommandHandler : BaseCommandHandler
    {
        protected readonly ISalesPlatformWebhookRequestRepository SalesPlatformWebhookRequestRepo;
        protected readonly ISalesPlatformServiceFactory SalesPlatformServiceFactory;

        /// <summary>Initializes a new instance of the <see cref="BaseSalesPlatformWebhookRequestCommandHandler"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="salesPlatformWebhookRequestRepository">The sales platform webhook request repository.</param>
        /// <param name="salesPlatformServiceFactory">The sales platform service factory.</param>
        public BaseSalesPlatformWebhookRequestCommandHandler(
            IMediator commandBus, 
            IUnitOfWork uow, 
            ISalesPlatformWebhookRequestRepository salesPlatformWebhookRequestRepository,
            ISalesPlatformServiceFactory salesPlatformServiceFactory)
            : base(commandBus, uow)
        {
            this.SalesPlatformWebhookRequestRepo = salesPlatformWebhookRequestRepository;
            this.SalesPlatformServiceFactory = salesPlatformServiceFactory;
        }

        /// <summary>Gets the sales platform webhook request by uid.</summary>
        /// <param name="salesPlatformWebhookRequestUid">The sales platform webhook request uid.</param>
        /// <returns></returns>
        public async Task<SalesPlatformWebhookRequest> GetSalesPlatformWebhookRequestByUid(Guid salesPlatformWebhookRequestUid)
        {
            var salesPlatformWebhookRequest = await this.SalesPlatformWebhookRequestRepo.GetAsync(salesPlatformWebhookRequestUid);
            if (salesPlatformWebhookRequest == null)
            {
                this.ValidationResult.Add(new ValidationError($"Sales platform webhook request (Uid: {salesPlatformWebhookRequestUid}) not found."));
            }

            return salesPlatformWebhookRequest;
        }
    }
}