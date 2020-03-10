// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-10-2020
// ***********************************************************************
// <copyright file="CreateLogisticAccomodationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateLogisticAccomodationCommandHandler</summary>
    public class CreateLogisticAccomodationCommandHandler : LogisticAccommodationBaseCommandHandler, IRequestHandler<CreateLogisticAccomodation, AppValidationResult>
    {
        private readonly ILogisticRepository logisticRepo;
        private readonly IAttendeePlacesRepository placeRepo;

        /// <summary>Initializes a new instance of the <see cref="CreateLogisticAccomodationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="logisticRepository">The logistic repository.</param>
        /// <param name="placeRepository">The place repository.</param>
        /// <param name="logisticAccommodationRepository">The logistic accommodation repository.</param>
        public CreateLogisticAccomodationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ILogisticRepository logisticRepository,
            IAttendeePlacesRepository placeRepository,
            ILogisticAccommodationRepository logisticAccommodationRepository) 
            : base(eventBus, uow, logisticAccommodationRepository)
        {
            this.logisticRepo = logisticRepository;
            this.placeRepo = placeRepository;
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
                logisticRepo.Get(cmd.LogisticsUid),
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