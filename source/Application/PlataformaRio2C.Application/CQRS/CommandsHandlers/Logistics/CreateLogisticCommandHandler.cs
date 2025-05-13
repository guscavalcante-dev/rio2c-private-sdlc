// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-20-2020
// ***********************************************************************
// <copyright file="CreateLogisticCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateLogisticCommandHandler</summary>
    public class CreateLogisticCommandHandler : LogisticsBaseCommandHandler, IRequestHandler<CreateLogistic, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly IAttendeeLogisticSponsorRepository attendeeLogisticSponsorRepo;
        private readonly ILogisticSponsorRepository logisticSponsorRepo;
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;

        /// <summary>Initializes a new instance of the <see cref="CreateLogisticCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="logisticRepository">The logistic repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="attendeeCollaboratorRepository">The attendee collaborator repository.</param>
        /// <param name="logisticSponsorRepository">The logistic sponsor repository.</param>
        /// <param name="attendeeLogisticSponsorRepository">The attendee logistic sponsor repository.</param>
        public CreateLogisticCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ILogisticRepository logisticRepository,
            IEditionRepository editionRepository,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepository,
            ILogisticSponsorRepository logisticSponsorRepository,
            IAttendeeLogisticSponsorRepository attendeeLogisticSponsorRepository)
            : base(eventBus, uow, logisticRepository)
        {
            this.editionRepo = editionRepository;
            this.attendeeLogisticSponsorRepo = attendeeLogisticSponsorRepository;
            this.logisticSponsorRepo = logisticSponsorRepository;
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepository;
        }

        /// <summary>Handles the specified create logistic.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateLogistic cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var attendeeCollaborator = await this.repository.GetAsync(l => l.AttendeeCollaborator.Uid == cmd.AttendeeCollaboratorUid && !l.IsDeleted);
            if (attendeeCollaborator?.AttendeeCollaborator?.Collaborator != null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityExistsWithSameProperty, Labels.Request.ToLower(), Labels.Participant, attendeeCollaborator.AttendeeCollaborator.Collaborator.GetDisplayName()), new string[] { "AttendeeCollaboratorUid" }));
            }

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            var logistic = new Logistic(
                this.editionRepo.Get(cmd.EditionId),
                this.attendeeCollaboratorRepo.Get(cmd.AttendeeCollaboratorUid ?? Guid.Empty),
                cmd.IsAirfareSponsored,
                this.attendeeLogisticSponsorRepo.Get(cmd.AirfareSponsorOtherUid ?? cmd.AirfareSponsorUid ?? Guid.Empty),
                await this.logisticSponsorRepo.GetAsync(ls => ls.Name.ToLower() == cmd.AirfareSponsorOtherName.ToLower().Trim() && !ls.IsDeleted),
                cmd.AirfareSponsorOtherName,
                cmd.IsAccommodationSponsored,
                this.attendeeLogisticSponsorRepo.Get(cmd.AccommodationSponsorOtherUid ?? cmd.AccommodationSponsorUid ?? Guid.Empty),
                await this.logisticSponsorRepo.GetAsync(ls => ls.Name.ToLower() == cmd.AccommodationSponsorOtherName.ToLower().Trim() && !ls.IsDeleted),
                cmd.AccommodationSponsorOtherName,
                cmd.IsAirportTransferSponsored,
                this.attendeeLogisticSponsorRepo.Get(cmd.AirportTransferSponsorOtherUid ?? cmd.AirportTransferSponsorUid ?? Guid.Empty),
                await this.logisticSponsorRepo.GetAsync(ls => ls.Name.ToLower() == cmd.AirportTransferSponsorOtherName.ToLower().Trim() && !ls.IsDeleted),
                cmd.AirportTransferSponsorOtherName,
                cmd.IsCityTransferRequired,
                cmd.IsVehicleDisposalRequired,
                cmd.AdditionalInfo,
                cmd.UserId);

            if (!logistic.IsValid())
            {
                this.AppValidationResult.Add(logistic.ValidationResult);
                return this.AppValidationResult;
            }

            this.repository.Create(logistic);

            this.Uow.SaveChanges();
            this.AppValidationResult.Data = logistic;

            return this.AppValidationResult;
        }
    }
}