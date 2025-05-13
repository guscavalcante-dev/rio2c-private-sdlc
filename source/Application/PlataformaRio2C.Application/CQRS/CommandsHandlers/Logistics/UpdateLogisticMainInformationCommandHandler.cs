// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-13-2020
// ***********************************************************************
// <copyright file="UpdateLogisticMainInformationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>
    /// UpdateLogisticMainInformationCommandHandler
    /// </summary>
    public class UpdateLogisticMainInformationCommandHandler : LogisticsBaseCommandHandler, IRequestHandler<UpdateLogisticMainInformation, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly IAttendeeLogisticSponsorRepository attendeeLogisticSponsorRepo;
        private readonly ILogisticSponsorRepository logisticSponsorRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateLogisticMainInformationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="logisticRepository">The logistic repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="attendeeLogisticSponsorRepository">The attendee logistic sponsor repository.</param>
        /// <param name="logisticSponsorRepository">The logistic sponsor repository.</param>
        public UpdateLogisticMainInformationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ILogisticRepository logisticRepository,
            IEditionRepository editionRepository,
            IAttendeeLogisticSponsorRepository attendeeLogisticSponsorRepository,
            ILogisticSponsorRepository logisticSponsorRepository)
            : base(eventBus, uow, logisticRepository)
        {
            this.editionRepo = editionRepository;
            this.attendeeLogisticSponsorRepo = attendeeLogisticSponsorRepository;
            this.logisticSponsorRepo = logisticSponsorRepository;
        }

        /// <summary>Handles the specified update logistic main information.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateLogisticMainInformation cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var logistic = await this.GetLogisticByUid(cmd.LogisticUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            logistic.Update(
                    this.editionRepo.Get(cmd.EditionId),
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

            this.repository.Update(logistic);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = logistic;

            return this.AppValidationResult;
        }
    }
}