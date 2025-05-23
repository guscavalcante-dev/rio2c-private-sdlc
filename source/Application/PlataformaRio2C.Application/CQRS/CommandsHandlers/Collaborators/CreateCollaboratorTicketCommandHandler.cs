﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-31-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-11-2019
// ***********************************************************************
// <copyright file="CreateCollaboratorWithTicketsHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateCollaboratorWithTicketsHandler</summary>
    public class CreateCollaboratorWithTicketsHandler : BaseCollaboratorCommandHandler, IRequestHandler<CreateCollaboratorTicket, AppValidationResult>
    {
        private readonly IUserRepository userRepo;

        /// <summary>Initializes a new instance of the <see cref="CreateCollaboratorWithTicketsHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="userRepository">The user repository.</param>
        public CreateCollaboratorWithTicketsHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ICollaboratorRepository collaboratorRepository,
            IUserRepository userRepository)
            : base(eventBus, uow, collaboratorRepository)
        {
            this.userRepo = userRepository;
        }

        /// <summary>Handles the specified create collaborator ticket.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateCollaboratorTicket cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var collaboratorUid = Guid.NewGuid();

            #region Initial validations

            var user = await this.userRepo.GetAsync(u => u.Email == cmd.SalesPlatformAttendeeDto.Email);

            #endregion

            // Create if the user was not found in database
            if (user == null)
            {
                var collaborator = Collaborator.CreateCollaboratorTicket(
                    collaboratorUid,
                    cmd.Edition,
                    cmd.AttendeeOrganizations,
                    cmd.AttendeeSalesPlatformTicketType,
                    cmd.CollaboratorType,
                    cmd.Role,
                    cmd.SalesPlatformAttendeeDto.AttendeeId,
                    cmd.SalesPlatformAttendeeDto.SalesPlatformUpdateDate,
                    cmd.SalesPlatformAttendeeDto.FirstName,
                    cmd.SalesPlatformAttendeeDto.LastName,
                    cmd.SalesPlatformAttendeeDto.Email,
                    cmd.SalesPlatformAttendeeDto.CellPhone,
                    cmd.SalesPlatformAttendeeDto.JobTitle,
                    cmd.SalesPlatformAttendeeDto.Barcode,
                    cmd.SalesPlatformAttendeeDto.IsBarcodePrinted,
                    cmd.SalesPlatformAttendeeDto.IsBarcodeUsed,
                    cmd.SalesPlatformAttendeeDto.BarcodeUpdateDate,
                    cmd.SalesPlatformAttendeeDto.TicketUrl,
                    cmd.SalesPlatformAttendeeDto.IsTicketPrinted,
                    cmd.SalesPlatformAttendeeDto.IsTicketUsed,
                    cmd.SalesPlatformAttendeeDto.TicketUpdateDate,
                    1);

                collaborator?.UpdateWelcomeEmailSendDate(cmd.Edition.Id, cmd.UserId);

                if (!collaborator.IsValid())
                {
                    this.AppValidationResult.Add(collaborator.ValidationResult);
                    return this.AppValidationResult;
                }

                this.CollaboratorRepo.Create(collaborator);
                this.Uow.SaveChanges();
                this.AppValidationResult.Data = collaborator;

                #region Send welcome email

                await this.CommandBus.Send(new SendProducerWelcomeEmailAsync(
                    collaborator.User.SecurityStamp,
                    collaborator.User.Id,
                    collaborator.User.Uid,
                    collaborator.FirstName,
                    collaborator.GetFullName(),
                    cmd.SalesPlatformAttendeeDto.Email,
                    cmd.Edition,
                    "pt-BR"), cancellationToken);

                #endregion
            }
            //else
            //{
            //    var updateCmd = new UpdateCollaborator
            //    {
            //        CollaboratorUid = user.Collaborator.Uid,
            //        IsAddingToCurrentEdition = true,
            //        FirstName = cmd.FirstName,
            //        LastNames = cmd.LastNames,
            //        Badge = cmd.Badge,
            //        Email = cmd.Email,
            //        PhoneNumber = cmd.PhoneNumber,
            //        CellPhone = cmd.CellPhone,
            //        Address = cmd.Address,
            //        AttendeeOrganizationBaseCommands = cmd.AttendeeOrganizationBaseCommands,
            //        JobTitles = cmd.JobTitles,
            //        MiniBios = cmd.MiniBios,
            //        CropperImage = cmd.CropperImage
            //    };
            //    updateCmd.UpdatePreSendProperties(cmd.OrganizationType, cmd.UserId, cmd.UserUid, cmd.EditionId, cmd.EditionUid, cmd.UserInterfaceLanguage);

            //    this.AppValidationResult = await this.CommandBus.Send(updateCmd, cancellationToken);
            //}

            return this.AppValidationResult;
        }
    }
}