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
    public class CreateLogisticAccomodationCommandHandler : LogisticAccommodationBaseCommandHandler, IRequestHandler<CreateLogisticAccomodation, AppValidationResult>
    {
        private readonly ILogisticsRepository logisticsRepo;
        private readonly IAttendeePlacesRepository placeRepo;

        /// <summary>Initializes a new instance of the <see cref="T:PlataformaRio2C.Application.CQRS.CommandsHandlers.CreateLogisticsCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="logisticsRepo">The logistics repo.</param>
        /// <param name="logisticSponsorRepo">The logistic sponsor repo.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="attendeeCollaboratorRepo">The attendee collaborator repo.</param>
        /// <param name="languageRepository">The language repository.</param>
        public CreateLogisticAccomodationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ILogisticsRepository logisticsRepo,
            IAttendeePlacesRepository placeRepo,
            ILogisticAccommodationRepository logisticAccommodationRepo) : base(eventBus, uow, logisticAccommodationRepo)
        {
            this.logisticsRepo = logisticsRepo;
            this.placeRepo = placeRepo;
        }

        /// <summary>Handles the specified create track.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateLogisticAccomodation cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var entity = new LogisticAccommodation(
                cmd.AdditionalInfo, 
                cmd.CheckInDate ?? DateTime.MinValue, 
                cmd.CheckOutDate ?? DateTime.MinValue,
                placeRepo.Get(cmd.PlaceId),
                logisticsRepo.Get(cmd.LogisticsUid),
                cmd.UserId);

            if (!entity.IsValid())
            {
                this.AppValidationResult.Add(entity.ValidationResult);
                return this.AppValidationResult;
            }

            this.repository.Create(entity);

            this.Uow.SaveChanges();
            this.AppValidationResult.Data = entity;

            return this.AppValidationResult;
        }
    }
}