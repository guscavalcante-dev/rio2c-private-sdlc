// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-31-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-01-2019
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
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>ProcessPendingPlatformWebhookRequestsAsyncCommandHandler</summary>
    public class ProcessPendingPlatformWebhookRequestsAsyncCommandHandler : BaseSalesPlatformWebhookRequestCommandHandler, IRequestHandler<ProcessPendingPlatformWebhookRequestsAsync, AppValidationResult>
    {
        private readonly IAttendeeSalesPlatformRepository attendeeSalesPlatformRepo;
        private readonly ICollaboratorRepository collaboratorRepo;

        /// <summary>Initializes a new instance of the <see cref="ProcessPendingPlatformWebhookRequestsAsyncCommandHandler"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="salesPlatformWebhookRequestRepository">The sales platform webhook request repository.</param>
        /// <param name="salesPlatformServiceFactory">The sales platform service factory.</param>
        /// <param name="attendeeSalesPlatformRepository">The attendee sales platform repository.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        public ProcessPendingPlatformWebhookRequestsAsyncCommandHandler(
            IMediator commandBus,
            IUnitOfWork uow,
            ISalesPlatformWebhookRequestRepository salesPlatformWebhookRequestRepository,
            ISalesPlatformServiceFactory salesPlatformServiceFactory,
            IAttendeeSalesPlatformRepository attendeeSalesPlatformRepository,
            ICollaboratorRepository collaboratorRepository)
            : base(commandBus, uow, salesPlatformWebhookRequestRepository, salesPlatformServiceFactory)
        {
            this.attendeeSalesPlatformRepo = attendeeSalesPlatformRepository;
            this.collaboratorRepo = collaboratorRepository;
        }

        /// <summary>Handles the specified process pending platform webhook requests asynchronous.</summary>
        /// <param name="cmd"></param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The command.</returns>
        public async Task<AppValidationResult> Handle(ProcessPendingPlatformWebhookRequestsAsync cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var attendeeSalesPlatformsDtos = await this.attendeeSalesPlatformRepo.FindAllDtoByIsActiveAsync();
            if (attendeeSalesPlatformsDtos?.Any() != true)
            {
                this.ValidationResult.Add(new ValidationError("Webhook requests will not be processed because there is no active sales platform."));
            }

            var pendingRequestsDtos = await this.CommandBus.Send(new FindAllSalesPlatformWebhooRequestsDtoByPending(), cancellationToken);
            if (pendingRequestsDtos?.Any() != true)
            {
                this.ValidationResult.Add(new ValidationError("No pending webhook requests to process."));
            }

            // Check to stop processing
            if (!this.ValidationResult.IsValid)
            {
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

            #region Process requests

            // Loop webhook requests
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
                        var errorMessage = $"No attendee returned by api for Uid: {processingRequestDto.Uid}";
                        this.ValidationResult.Add(new ValidationError(errorMessage));
                        processingRequestDto.SalesPlatformWebhookRequest.Postpone("000000001", errorMessage);
                        this.SalesPlatformWebhookRequestRepo.Update(processingRequestDto.SalesPlatformWebhookRequest);
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    var errorMessage = $"Error processing the webhook request Uid {processingRequestDto.Uid} (Error: {ex.GetInnerMessage()}).";
                    this.ValidationResult.Add(new ValidationError(errorMessage));
                    processingRequestDto.SalesPlatformWebhookRequest.Postpone("000000002", errorMessage);
                    this.SalesPlatformWebhookRequestRepo.Update(processingRequestDto.SalesPlatformWebhookRequest);
                    continue;
                }

                #endregion

                #region Process the attendees

                var currentValidationResult = new ValidationResult();

                // Loop platform attendees
                foreach (var salesPlatformAttendeeDto in salesPlatformAttendeeDtos)
                {
                    // Check if the edition exits
                    var attendeeSalesPlatformDto = attendeeSalesPlatformsDtos?.FirstOrDefault(asp => asp.AttendeeSalesPlatform.SalesPlatformEventid == salesPlatformAttendeeDto.EventId);
                    if (attendeeSalesPlatformDto == null)
                    {
                        var errorMessage = $"Edition not found or not active " +
                                           $"(SalesPlatformAttendeeId: {salesPlatformAttendeeDto.AttendeeId}; " +
                                           $"SalesPlatformEventId: {salesPlatformAttendeeDto.EventId}).";
                        currentValidationResult.Add(new ValidationError(errorMessage));
                        this.AppValidationResult.Add(this.ValidationResult);
                        continue;
                    }

                    // Check if the ticket type exists
                    var attendeeSalesPlatformTicketTypeDto = attendeeSalesPlatformDto.AttendeeSalesPlatformTicketTypesDtos.FirstOrDefault(asptt => asptt.AttendeeSalesPlatformTicketType.TicketClassId == salesPlatformAttendeeDto.TicketClassId);
                    if (attendeeSalesPlatformTicketTypeDto == null)
                    {
                        var errorMessage = $"Ticket class not found or not active " +
                                           $"(SalesPlatformAttendeeId: {salesPlatformAttendeeDto.AttendeeId}; " +
                                           $"TicketClassId: {salesPlatformAttendeeDto.TicketClassId}; " +
                                           $"TicketClassName: {salesPlatformAttendeeDto.TicketClassName}).";
                        processingRequestDto.SalesPlatformWebhookRequest.Postpone("000000004", errorMessage);
                        currentValidationResult.Add(new ValidationError(errorMessage));
                        continue;
                    }

                    // Create/Update Collaborator
                    var collaborator = await this.collaboratorRepo.FindBySalesPlatformAttendeeIdAsync(salesPlatformAttendeeDto.AttendeeId);
                    if (collaborator == null)
                    {
                        var response = await this.CommandBus.Send(new CreateCollaboratorTicket(
                            salesPlatformAttendeeDto, 
                            attendeeSalesPlatformDto.Edition, 
                            attendeeSalesPlatformTicketTypeDto.AttendeeSalesPlatformTicketType, 
                            attendeeSalesPlatformTicketTypeDto.Role), cancellationToken);
                        foreach (var error in response?.Errors)
                        {
                            currentValidationResult.Add(new ValidationError(error.Message));
                        }
                    }
                    else
                    {
                        var response = await this.CommandBus.Send(new UpdateCollaboratorTicket(
                            collaborator, salesPlatformAttendeeDto, 
                            attendeeSalesPlatformDto.Edition,
                            attendeeSalesPlatformTicketTypeDto.AttendeeSalesPlatformTicketType,
                            attendeeSalesPlatformTicketTypeDto.Role), cancellationToken);
                        foreach (var error in response?.Errors)
                        {
                            currentValidationResult.Add(new ValidationError(error.Message));
                        }
                    }

                    //var status = await this.CommandBus.Send(new CreateCollaboratorWithTickets(), cancellationToken);
                }

                if (!currentValidationResult.IsValid)
                {
                    this.AppValidationResult.Add(currentValidationResult);

                    var firstError = currentValidationResult?.Errors?.FirstOrDefault();
                    processingRequestDto.SalesPlatformWebhookRequest.Postpone(firstError?.Code, firstError?.Message);
                    this.SalesPlatformWebhookRequestRepo.Update(processingRequestDto.SalesPlatformWebhookRequest);
                    continue;
                }

                processingRequestDto.SalesPlatformWebhookRequest.Conclude();
                this.SalesPlatformWebhookRequestRepo.Update(processingRequestDto.SalesPlatformWebhookRequest);

                #endregion
            }

            this.Uow.SaveChanges();

            #endregion

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
            }

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}