// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 07-12-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-16-2022
// ***********************************************************************
// <copyright file="CreateSalesPlatformWebhookRequestCommandHandler.cs" company="Softo">
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
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Dtos;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Sympla.Models;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Statics;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateSalesPlatformWebhookRequestCommandHandler</summary>
    public class CreateSalesPlatformWebhookRequestCommandHandler : IRequestHandler<CreateSalesPlatformWebhookRequest, AppValidationResult>
    {
        private readonly IMediator eventBus;
        private readonly IUnitOfWork uow;
        private readonly ISalesPlatformWebhookRequestRepository salesPlatformWebhookRequestRepo;
        private readonly ISalesPlatformRepository salesPlatformRepo;
        private readonly ISalesPlatformServiceFactory salesPlatformServiceFactory;
        private readonly ICollaboratorRepository collaboratorRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSalesPlatformWebhookRequestCommandHandler" /> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="salesPlatformWebhookRequestRepository">The sales platform webhook request repository.</param>
        /// <param name="salesPlatformRepository">The sales platform repository.</param>
        /// <param name="salesPlatformServiceFactory">The sales platform service factory.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        public CreateSalesPlatformWebhookRequestCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ISalesPlatformWebhookRequestRepository salesPlatformWebhookRequestRepository,
            ISalesPlatformRepository salesPlatformRepository,
            ISalesPlatformServiceFactory salesPlatformServiceFactory,
            ICollaboratorRepository collaboratorRepository
            )
        {
            this.eventBus = eventBus;
            this.uow = uow;
            this.salesPlatformWebhookRequestRepo = salesPlatformWebhookRequestRepository;
            this.salesPlatformRepo = salesPlatformRepository;
            this.salesPlatformServiceFactory = salesPlatformServiceFactory;
            this.collaboratorRepo = collaboratorRepository;
        }

        /// <summary>Handles the specified create sales platform webhook request.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateSalesPlatformWebhookRequest cmd, CancellationToken cancellationToken)
        {
            AppValidationResult appValidationResult = new AppValidationResult();

            this.uow.BeginTransaction();

            var salesPlatform = await this.salesPlatformRepo.FindByNameAsync(cmd.SalesPlatformName);
            if(salesPlatform == null)
            {
                throw new DomainException(string.Format("Sales platform {0} not found", cmd.SalesPlatformName));
            }

            switch (salesPlatform.Name)
            {
                case SalePlatformName.Sympla:
                    {
                        #region Sympla

                        var salesPlatformDto = await this.salesPlatformRepo.FindDtoByNameAsync(salesPlatform.Name);
                        var salesPlatformService = this.salesPlatformServiceFactory.Get(salesPlatformDto);
                        var salesPlatformAttendeeDtos = salesPlatformService.GetAttendees(cmd.ReimportAllAttendees);
                        if (salesPlatformAttendeeDtos.Count > 0)
                        {
                            List<SalesPlatformAttendeeDto> salesPlatformAttendeeDtosToCreate = new List<SalesPlatformAttendeeDto>();

                            // This OrderBy is required to import attendees in chronological order and updates "AttendeeSalesPlatform.LastOrderUpdateDate" properly. Take care when changing this!
                            foreach (var salesPlatformAttendeeDto in salesPlatformAttendeeDtos.OrderBy(dto => dto.SalesPlatformUpdateDate))
                            {
                                #region Filter payloads to import

                                var collaboratorByAttendeeId = await this.collaboratorRepo.FindBySalesPlatformAttendeeIdAsync(salesPlatformAttendeeDto.AttendeeId);
                                var collaboratorByEmail = await this.collaboratorRepo.GetAsync(c => c.User.Email == salesPlatformAttendeeDto.Email);

                                if (salesPlatformAttendeeDto.SalesPlatformAttendeeStatus == SalesPlatformAttendeeStatus.Attending)
                                {
                                    // Collaborator not found by attendee id and email
                                    if (collaboratorByAttendeeId == null && collaboratorByEmail == null)
                                    {
                                        salesPlatformAttendeeDtosToCreate.Add(salesPlatformAttendeeDto);
                                    }
                                    // Collaborator attendee not exits or is the same of the email
                                    else if ((collaboratorByAttendeeId == null && collaboratorByEmail != null)
                                             || (collaboratorByAttendeeId != null && collaboratorByEmail != null && collaboratorByAttendeeId.Uid == collaboratorByEmail.Uid))
                                    {
                                        salesPlatformAttendeeDtosToCreate.Add(salesPlatformAttendeeDto);
                                    }
                                    // Collaborator attendee exists but is not the same email (Ownership change)
                                    else if (collaboratorByAttendeeId != null && collaboratorByEmail == null && collaboratorByAttendeeId.User.Email != salesPlatformAttendeeDto.Email)
                                    {
                                        // Generates cancellation for old participant
                                        var symplaParticipantToCancel = salesPlatformAttendeeDto.GetPayload<SymplaParticipant>();
                                        symplaParticipantToCancel.UpdateParticipantAndCancelTicket(
                                                                    collaboratorByAttendeeId.FirstName,
                                                                    collaboratorByAttendeeId.LastNames,
                                                                    collaboratorByAttendeeId.User?.Email);

                                        // Add cancellation payload to cancel ticket for old participant
                                        salesPlatformAttendeeDtosToCreate.Add(new SalesPlatformAttendeeDto(symplaParticipantToCancel));

                                        // Add current payload to generate ticket for new participant
                                        salesPlatformAttendeeDtosToCreate.Add(salesPlatformAttendeeDto);
                                    }
                                    // Collaborator attendee exists and collaborator email exists but are differente emails (Ownership change)
                                    else if (collaboratorByAttendeeId != null && collaboratorByEmail != null && collaboratorByAttendeeId.Uid != collaboratorByEmail.Uid)
                                    {
                                        // Generates cancellation for old participant
                                        var symplaParticipantToCancel = salesPlatformAttendeeDto.GetPayload<SymplaParticipant>();
                                        symplaParticipantToCancel.UpdateParticipantAndCancelTicket(
                                                                    collaboratorByAttendeeId.FirstName,
                                                                    collaboratorByAttendeeId.LastNames,
                                                                    collaboratorByAttendeeId.User?.Email);

                                        // Add cancellation payload to cancel ticket for old participant
                                        salesPlatformAttendeeDtosToCreate.Add(new SalesPlatformAttendeeDto(symplaParticipantToCancel));

                                        // Add current payload to update ticket for new participant
                                        salesPlatformAttendeeDtosToCreate.Add(salesPlatformAttendeeDto);
                                    }
                                    else
                                    {
                                        // Needs to break because if continues, it will update the SalesPlatformUpdateDate and it will no longer be possible to import this webhook again
                                        throw new DomainException($"No logic found for webhook request (Email: {salesPlatformAttendeeDto.Email}; SalesPlatformAttendeeStatus: {salesPlatformAttendeeDto.SalesPlatformAttendeeStatus})");
                                    }
                                }
                                else if (salesPlatformAttendeeDto.SalesPlatformAttendeeStatus == SalesPlatformAttendeeStatus.NotAttending)
                                {
                                    salesPlatformAttendeeDtosToCreate.Add(salesPlatformAttendeeDto);
                                }
                                else
                                {
                                    // Needs to break because if continues, it will update the SalesPlatformUpdateDate and it will no longer be possible to import this webhook again
                                    throw new DomainException($"Attendee status not configured (Email: {salesPlatformAttendeeDto.Email}; SalesPlatformAttendeeStatus: {salesPlatformAttendeeDto.SalesPlatformAttendeeStatus})");
                                }

                                #endregion
                            }

                            if (cmd.ReimportAllAttendees)
                            {
                                var salesPlatformWebhookRequestDtos = await this.salesPlatformWebhookRequestRepo.FindAllDtoBySalesPlatformIdAsync(salesPlatform.Id);

                                // Remove previously imported payloads from list to import
                                salesPlatformAttendeeDtosToCreate = salesPlatformAttendeeDtosToCreate.Where(spaDto => !salesPlatformWebhookRequestDtos
                                                                                                                        .Select(spwrDto => spwrDto.Payload)
                                                                                                                        .Contains(spaDto.Payload)).ToList();
                            }

                            foreach (var salesPlatformAttendeeDto in salesPlatformAttendeeDtosToCreate.OrderBy(dto => dto.SalesPlatformUpdateDate))
                            {
                                #region Create payloads into database

                                // Creates the webhook
                                var salesPlatformWebhookRequest = new SalesPlatformWebhookRequest(
                                    Guid.NewGuid(),
                                    salesPlatform,
                                    cmd.WebhookSecurityKey,
                                    cmd.Endpoint,
                                    cmd.Header,
                                    salesPlatformAttendeeDto.Payload,
                                    cmd.IpAddress);

                                this.salesPlatformWebhookRequestRepo.Create(salesPlatformWebhookRequest);

                                // Updates last sales platform order date
                                salesPlatform.UpdateLastSalesPlatformOrderDate(salesPlatformAttendeeDto.EventId, 
                                                                               salesPlatformAttendeeDto.SalesPlatformUpdateDate);
                                this.salesPlatformRepo.Update(salesPlatform);

                                #endregion
                            }

                            var result = this.uow.SaveChanges();
                            if (!result.Success)
                            {
                                foreach (var validationResult in result.ValidationResults)
                                {
                                    appValidationResult.Add(validationResult.ErrorMessage);
                                }

                                return appValidationResult;
                            }
                        }

                        // Process pending created webhooks
                        var processingResult = await this.eventBus.Send(new ProcessPendingPlatformWebhookRequestsAsync());
                        if (!processingResult.IsValid)
                        {
                            return processingResult;
                        }

                        break;

                        #endregion
                    };
                default:
                    {
                        #region Default

                        var salesPlatformWebhookRequest = new SalesPlatformWebhookRequest(
                                cmd.SalesPlatformWebhookRequestUid,
                                salesPlatform,
                                cmd.WebhookSecurityKey,
                                cmd.Endpoint,
                                cmd.Header,
                                cmd.Payload,
                                cmd.IpAddress);

                        this.salesPlatformWebhookRequestRepo.Create(salesPlatformWebhookRequest);

                        var result = this.uow.SaveChanges();
                        if (!result.Success)
                        {
                            foreach (var validationResult in result.ValidationResults)
                            {
                                appValidationResult.Add(validationResult.ErrorMessage);
                            }

                            return appValidationResult;
                        }

                        break;

                        #endregion
                    };
            }

            return appValidationResult;
        }
    }
}