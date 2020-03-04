// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-09-2020
// ***********************************************************************
// <copyright file="CreateTrackCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateLogisticsCommandHandler</summary>
    public class CreateLogisticsCommandHandler : LogisticsBaseCommandHandler, IRequestHandler<CreateLogisticRequest, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly ILogisticsRepository logisticsRepo;
        private readonly ILogisticSponsorRepository logisticSponsorRepo;
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;

        /// <summary>Initializes a new instance of the <see cref="T:PlataformaRio2C.Application.CQRS.CommandsHandlers.CreateLogisticsCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="logisticsRepo">The logistics repo.</param>
        /// <param name="logisticSponsorRepo">The logistic sponsor repo.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="attendeeCollaboratorRepo">The attendee collaborator repo.</param>
        /// <param name="languageRepository">The language repository.</param>
        public CreateLogisticsCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ILogisticsRepository logisticsRepo,
            ILogisticSponsorRepository logisticSponsorRepo,
            IEditionRepository editionRepository,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepo,
            ILanguageRepository languageRepository)
            : base(eventBus, uow, logisticsRepo)
        {
            this.editionRepo = editionRepository;
            this.languageRepo = languageRepository;
            this.logisticsRepo = logisticsRepo;
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepo;
            this.logisticSponsorRepo = logisticSponsorRepo;
        }

        /// <summary>Handles the specified create track.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateLogisticRequest cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();
            
            var logistics = new Logistics(
                this.attendeeCollaboratorRepo.Get(cmd.AttendeeCollaboratorUid),
                cmd.IsAirfareSponsored,
                this.logisticSponsorRepo.Get(cmd.AirfareSponsorOtherUid ?? cmd.AirfareSponsorUid ?? Guid.Empty)?.Id,
                cmd.IsAccommodationSponsored,
                this.logisticSponsorRepo.Get(cmd.AccommodationSponsorOtherUid ?? cmd.AccommodationSponsorUid ?? Guid.Empty)?.Id,
                cmd.IsAirportTransferSponsored,
                this.logisticSponsorRepo.Get(cmd.AirportTransferSponsorOtherUid ?? cmd.AirportTransferSponsorUid ?? Guid.Empty)?.Id,
                cmd.IsCityTransferRequired,
                cmd.IsVehicleDisposalRequired,
                cmd.AdditionalInfo,
                cmd.UserId);
            
            if (!logistics.IsValid())
            {
                this.AppValidationResult.Add(logistics.ValidationResult);
                return this.AppValidationResult;
            }

            this.repository.Create(logistics);

            this.Uow.SaveChanges();
            this.AppValidationResult.Data = logistics;

            return this.AppValidationResult;
        }
    }
}