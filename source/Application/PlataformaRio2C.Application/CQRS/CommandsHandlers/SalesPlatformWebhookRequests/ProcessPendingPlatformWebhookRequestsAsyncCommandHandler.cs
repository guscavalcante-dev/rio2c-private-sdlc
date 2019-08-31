// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-31-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-31-2019
// ***********************************************************************
// <copyright file="ProcessPendingPlatformWebhookRequestsAsyncCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>ProcessPendingPlatformWebhookRequestsAsyncCommandHandler</summary>
    public class ProcessPendingPlatformWebhookRequestsAsyncCommandHandler : BaseSalesPlatformWebhookRequestCommandHandler, IRequestHandler<ProcessPendingPlatformWebhookRequestsAsync, AppValidationResult>
    {
        /// <summary>Initializes a new instance of the <see cref="ProcessPendingPlatformWebhookRequestsAsyncCommandHandler"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="salesPlatformWebhookRequestRepository">The sales platform webhook request repository.</param>
        /// <param name="salesPlatformServiceFactory">The sales platform service factory.</param>
        public ProcessPendingPlatformWebhookRequestsAsyncCommandHandler(
            IMediator commandBus,
            IUnitOfWork uow,
            ISalesPlatformWebhookRequestRepository salesPlatformWebhookRequestRepository,
            ISalesPlatformServiceFactory salesPlatformServiceFactory)
            : base(commandBus, uow, salesPlatformWebhookRequestRepository, salesPlatformServiceFactory)
        {
        }

        /// <summary>Handles the specified process pending platform webhook requests asynchronous.</summary>
        /// <param name="cmd"></param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The command.</returns>
        public async Task<AppValidationResult> Handle(ProcessPendingPlatformWebhookRequestsAsync cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var pendingRequestsDtos = await this.CommandBus.Send(new FindAllSalesPlatformWebhooRequestsDtoByPending(), cancellationToken);
            if (pendingRequestsDtos?.Any() != true)
            {
                this.ValidationResult.Add(new ValidationError("No pending webhook requests to process."));
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #region Mark all for processing to avoid other threads to get the same

            foreach (var request in pendingRequestsDtos)
            {
                request.SalesPlatformWebhookRequest.Process();
                this.SalesPlatformWebhookRequestRepo.Update(request.SalesPlatformWebhookRequest);
            }

            this.Uow.SaveChanges();

            #endregion

            foreach (var processingRequestDto in pendingRequestsDtos)
            {
                List<SalesPlatformAttendeeDto> salesPlatformAttendeeDtos;

                #region Get info from api

                try
                {
                    var salesPlatformService = this.SalesPlatformServiceFactory.Get(processingRequestDto);
                    salesPlatformAttendeeDtos = salesPlatformService.ExecuteRequest();
                    if (salesPlatformAttendeeDtos?.Any() != true)
                    {
                        throw new DomainException($"No attendee returned by api for Uid: {processingRequestDto.Uid}");
                    }
                }
                catch (Exception ex)
                {
                    this.ValidationResult.Add(new ValidationError($"Error processing the webhook request Uid {processingRequestDto.Uid} (Error: {ex.GetInnerMessage()})."));
                    this.AppValidationResult.Add(this.ValidationResult);

                    processingRequestDto.SalesPlatformWebhookRequest.Postpone(null, ex.GetInnerMessage());
                    this.SalesPlatformWebhookRequestRepo.Update(processingRequestDto.SalesPlatformWebhookRequest);
                    continue;
                }

                #endregion

                #region Process the attendees

                //if (!organization.IsValid())
                //{
                //    this.AppValidationResult.Add(organization.ValidationResult);
                //    return this.AppValidationResult;
                //}

                processingRequestDto.SalesPlatformWebhookRequest.Conclude();
                this.SalesPlatformWebhookRequestRepo.Update(processingRequestDto.SalesPlatformWebhookRequest);

                #endregion
            }

            this.Uow.SaveChanges();

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}