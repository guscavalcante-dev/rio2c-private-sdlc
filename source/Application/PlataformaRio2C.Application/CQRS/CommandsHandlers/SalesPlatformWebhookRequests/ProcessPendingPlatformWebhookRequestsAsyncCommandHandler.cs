// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-31-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-03-2019
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
using System.Web.Script.Serialization;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.ByInti.Models;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.ByInti;

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
                return this.AppValidationResult;
            }

            var pendingRequestsDtos = await this.CommandBus.Send(new FindAllSalesPlatformWebhooRequestsDtoByPending(), cancellationToken);
            if (pendingRequestsDtos?.Any() != true)
            {
                return this.AppValidationResult;
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


                var payload = processingRequestDto.SalesPlatformWebhookRequest.Payload;
                var dto = new JavaScriptSerializer().Deserialize<IntiSaleOrCancellation>(payload);

                //salesPlatformServiceFactory

                /*
                List<SalesPlatformAttendeeDto> listAtt = new List<SalesPlatformAttendeeDto>()
                {
                    
                    new SalesPlatformAttendeeDto()
                    {
                        Name = processingRequestDto.na
                    }
                    
                };
                */
                Tuple<string, List<SalesPlatformAttendeeDto>> salesPlatformResponse;

                #region Get info from api

                try
                {

                    //if (processingRequestDto.SalesPlatformDto.Name == "Eventbrite"){
                        var salesPlatformService = this.SalesPlatformServiceFactory.Get(processingRequestDto);
                        salesPlatformResponse = salesPlatformService.ExecuteRequest();
                        if (salesPlatformResponse?.Item2?.Any() != true)
                        {
                            var errorMessage = $"No attendee returned by api for Uid: {processingRequestDto.Uid}";
                            this.ValidationResult.Add(new ValidationError(errorMessage));
                            processingRequestDto.SalesPlatformWebhookRequest.Postpone("000000001", errorMessage);
                            this.SalesPlatformWebhookRequestRepo.Update(processingRequestDto.SalesPlatformWebhookRequest);
                            continue;
                        }
                    //}
                }
                catch (Exception ex)
                {
                    var errorMessage = $"Error processing the webhook request Uid {processingRequestDto.Uid} (Error: {ex.GetInnerMessage()}).";
                    this.ValidationResult.Add(new ValidationError(errorMessage));
                    processingRequestDto.SalesPlatformWebhookRequest.Abort("000000002", ex.GetInnerMessage());
                    this.SalesPlatformWebhookRequestRepo.Update(processingRequestDto.SalesPlatformWebhookRequest);
                    continue;
                }

                #endregion

                #region Process the attendees 

                var currentValidationResult = new ValidationResult();

                // Loop platform attendees
                foreach (var salesPlatformAttendeeDto in salesPlatformResponse.Item2)
                {
                    // Attendee updated
                    if (salesPlatformResponse.Item1 == SalesPlatformAction.AttendeeUpdated)
                    {
                        // Check if the edition exits
                        var attendeeSalesPlatformDto = attendeeSalesPlatformsDtos?.FirstOrDefault(asp => asp.AttendeeSalesPlatform.SalesPlatformEventid == salesPlatformAttendeeDto.EventId);
                        if (attendeeSalesPlatformDto == null)
                        {
                            var errorMessage = $"Edition not found or not active " +
                                               $"(SalesPlatformAttendeeId: {salesPlatformAttendeeDto.AttendeeId}; " +
                                               $"SalesPlatformEventId: {salesPlatformAttendeeDto.EventId}).";
                            currentValidationResult.Add(new ValidationError("000000003", errorMessage));
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
                            currentValidationResult.Add(new ValidationError("000000004", errorMessage));
                            continue;
                        }

                        var collaboratorByAttendeeId = await this.collaboratorRepo.FindBySalesPlatformAttendeeIdAsync(salesPlatformAttendeeDto.AttendeeId);
                        var collaboratorByEmail = await this.collaboratorRepo.GetAsync(c => c.User.Email == salesPlatformAttendeeDto.Email);

                        // The person is attending to the event
                        if (salesPlatformAttendeeDto.SalesPlatformAttendeeStatus == SalesPlatformAttendeeStatus.Attending)
                        {
                            // Collaborator not found by attendee id and email
                            if (collaboratorByAttendeeId == null && collaboratorByEmail == null)
                            {
                                var response = await this.CommandBus.Send(new CreateCollaboratorTicket(
                                    salesPlatformAttendeeDto,
                                    attendeeSalesPlatformDto.Edition,
                                    collaboratorByAttendeeId?.GetAttendeeCollaboratorByEditionId(attendeeSalesPlatformDto.Edition.Id)?.GetAllAttendeeOrganizations(),
                                    attendeeSalesPlatformTicketTypeDto.AttendeeSalesPlatformTicketType,
                                    attendeeSalesPlatformTicketTypeDto.CollaboratorType,
                                    attendeeSalesPlatformTicketTypeDto.Role), cancellationToken);
                                foreach (var error in response?.Errors)
                                {
                                    currentValidationResult.Add(new ValidationError(error.Message));
                                }
                            }
                            // Collaborator attendee not exits or is the same of the email
                            else if ((collaboratorByAttendeeId == null && collaboratorByEmail != null)
                                     || (collaboratorByAttendeeId != null && collaboratorByEmail != null && collaboratorByAttendeeId.Uid == collaboratorByEmail.Uid))
                            {
                                // Update collaborator ticket
                                var response = await this.CommandBus.Send(new UpdateCollaboratorTicket(
                                    collaboratorByEmail, 
                                    salesPlatformAttendeeDto,
                                    attendeeSalesPlatformDto.Edition,
                                    collaboratorByAttendeeId?.GetAttendeeCollaboratorByEditionId(attendeeSalesPlatformDto.Edition.Id)?.GetAllAttendeeOrganizations(),
                                    attendeeSalesPlatformTicketTypeDto.AttendeeSalesPlatformTicketType,
                                    attendeeSalesPlatformTicketTypeDto.CollaboratorType,
                                    attendeeSalesPlatformTicketTypeDto.Role), cancellationToken);
                                foreach (var error in response?.Errors)
                                {
                                    currentValidationResult.Add(new ValidationError(error.Message));
                                }
                            }
                            // Collaborator attendee exists but is not the same email
                            else if (collaboratorByAttendeeId != null && collaboratorByEmail == null && collaboratorByAttendeeId.User.Email != salesPlatformAttendeeDto.Email)
                            {
                                // Delete ticket from collaboratorAttendeeId
                                var response1 = await this.CommandBus.Send(new DeleteCollaboratorTicket(
                                    collaboratorByAttendeeId, 
                                    salesPlatformAttendeeDto,
                                    attendeeSalesPlatformDto.Edition,
                                    attendeeSalesPlatformTicketTypeDto.AttendeeSalesPlatformTicketType,
                                    attendeeSalesPlatformTicketTypeDto.CollaboratorType,
                                    attendeeSalesPlatformTicketTypeDto.Role), cancellationToken);
                                foreach (var error in response1?.Errors)
                                {
                                    currentValidationResult.Add(new ValidationError(error.Message));
                                }

                                // Avoid to create collaborator ticket if the other one was not deleted
                                if (response1?.Errors?.Any() == true)
                                {
                                    continue;
                                }

                                // Create collaborator ant ticket for new email
                                var response2 = await this.CommandBus.Send(new CreateCollaboratorTicket(
                                    salesPlatformAttendeeDto,
                                    attendeeSalesPlatformDto.Edition,
                                    collaboratorByAttendeeId?.GetAttendeeCollaboratorByEditionId(attendeeSalesPlatformDto.Edition.Id)?.GetAllAttendeeOrganizations(),
                                    attendeeSalesPlatformTicketTypeDto.AttendeeSalesPlatformTicketType,
                                    attendeeSalesPlatformTicketTypeDto.CollaboratorType,
                                    attendeeSalesPlatformTicketTypeDto.Role), cancellationToken);
                                foreach (var error in response2?.Errors)
                                {
                                    currentValidationResult.Add(new ValidationError(error.Message));
                                }
                            }
                            // Collaborator attendee exists and collaborator email exists but are differente emails
                            else if (collaboratorByAttendeeId != null && collaboratorByEmail != null && collaboratorByAttendeeId.Uid != collaboratorByEmail.Uid)
                            {
                                // Delete ticket from collaboratorAttendeeId
                                var response1 = await this.CommandBus.Send(new DeleteCollaboratorTicket(
                                    collaboratorByAttendeeId,
                                    salesPlatformAttendeeDto,
                                    attendeeSalesPlatformDto.Edition,
                                    attendeeSalesPlatformTicketTypeDto.AttendeeSalesPlatformTicketType,
                                    attendeeSalesPlatformTicketTypeDto.CollaboratorType,
                                    attendeeSalesPlatformTicketTypeDto.Role), cancellationToken);
                                foreach (var error in response1?.Errors)
                                {
                                    currentValidationResult.Add(new ValidationError(error.Message));
                                }

                                // Avoid to create collaborator ticket if the other one was not deleted
                                if (response1?.Errors?.Any() == true)
                                {
                                    continue;
                                }

                                // Update collaborator ticket
                                var response2 = await this.CommandBus.Send(new UpdateCollaboratorTicket(
                                    collaboratorByEmail,
                                    salesPlatformAttendeeDto,
                                    attendeeSalesPlatformDto.Edition,
                                    collaboratorByAttendeeId?.GetAttendeeCollaboratorByEditionId(attendeeSalesPlatformDto.Edition.Id)?.GetAllAttendeeOrganizations(),
                                    attendeeSalesPlatformTicketTypeDto.AttendeeSalesPlatformTicketType,
                                    attendeeSalesPlatformTicketTypeDto.CollaboratorType,
                                    attendeeSalesPlatformTicketTypeDto.Role), cancellationToken);
                                foreach (var error in response2?.Errors)
                                {
                                    currentValidationResult.Add(new ValidationError(error.Message));
                                }
                            }
                            else
                            {
                                var errorMessage = $"No logic found for webhook request (Uid: {processingRequestDto.Uid}).";
                                currentValidationResult.Add(new ValidationError("000000005", errorMessage));
                                continue;
                            }

                            //var status = await this.CommandBus.Send(new CreateCollaboratorWithTickets(), cancellationToken);
                        }
                        // The person is not attending or unpaid the event
                        else if (salesPlatformAttendeeDto.SalesPlatformAttendeeStatus == SalesPlatformAttendeeStatus.NotAttending
                                 || salesPlatformAttendeeDto.SalesPlatformAttendeeStatus == SalesPlatformAttendeeStatus.Unpaid
                                 || salesPlatformAttendeeDto.SalesPlatformAttendeeStatus == SalesPlatformAttendeeStatus.Deleted
                                 || salesPlatformAttendeeDto.SalesPlatformAttendeeStatus == SalesPlatformAttendeeStatus.Transferred)
                        {
                            // Delete ticket from collaboratorAttendeeId
                            var response1 = await this.CommandBus.Send(new DeleteCollaboratorTicket(
                                collaboratorByAttendeeId,
                                salesPlatformAttendeeDto,
                                attendeeSalesPlatformDto.Edition,
                                attendeeSalesPlatformTicketTypeDto.AttendeeSalesPlatformTicketType,
                                attendeeSalesPlatformTicketTypeDto.CollaboratorType,
                                attendeeSalesPlatformTicketTypeDto.Role), cancellationToken);

                            foreach (var error in response1?.Errors)
                            {
                                currentValidationResult.Add(new ValidationError(error.Message));
                            }
                        }
                        else
                        {
                            var errorMessage = $"Attendee status not configured (Uid: {processingRequestDto.Uid}; SalesPlatformAttendeeStatus: {salesPlatformAttendeeDto.SalesPlatformAttendeeStatus})";
                            currentValidationResult.Add(new ValidationError("000000006", errorMessage));
                            continue;
                        }
                    }
                    // Attendee checked in
                    else if (salesPlatformResponse.Item1 == SalesPlatformAction.AttendeeCheckedIn)
                    {
                        //TODO: Implement attendee checked in
                        var errorMessage = $"Attended checked in not implemented (Uid: {processingRequestDto.Uid}).";
                        this.ValidationResult.Add(new ValidationError(errorMessage));
                        processingRequestDto.SalesPlatformWebhookRequest.Abort("000000007", errorMessage);
                        this.SalesPlatformWebhookRequestRepo.Update(processingRequestDto.SalesPlatformWebhookRequest);
                        continue;
                    }
                    // Attendee checked out
                    else if (salesPlatformResponse.Item1 == SalesPlatformAction.AttendeeCheckedOut)
                    {
                        var errorMessage = $"Attended checked out not implemented (Uid: {processingRequestDto.Uid}).";
                        this.ValidationResult.Add(new ValidationError(errorMessage));
                        processingRequestDto.SalesPlatformWebhookRequest.Abort("000000008", errorMessage);
                        this.SalesPlatformWebhookRequestRepo.Update(processingRequestDto.SalesPlatformWebhookRequest);
                        continue;
                    }
                    // Action not mapped
                    else
                    {
                        var errorMessage = $"Sales platform action not configured (Uid: {processingRequestDto.Uid}).";
                        this.ValidationResult.Add(new ValidationError(errorMessage));
                        processingRequestDto.SalesPlatformWebhookRequest.Abort("000000009", errorMessage);
                        this.SalesPlatformWebhookRequestRepo.Update(processingRequestDto.SalesPlatformWebhookRequest);
                        continue;
                    }
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