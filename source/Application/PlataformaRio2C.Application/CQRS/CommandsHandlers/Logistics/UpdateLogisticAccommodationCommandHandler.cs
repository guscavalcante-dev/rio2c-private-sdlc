// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-13-2020
// ***********************************************************************
// <copyright file="UpdateLogisticAccommodationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateLogisticAccommodationCommandHandler</summary>
    public class UpdateLogisticAccommodationCommandHandler : LogisticAccommodationBaseCommandHandler, IRequestHandler<UpdateLogisticAccommodation, AppValidationResult>
    {
        private readonly IAttendeePlacesRepository attendeePlaceRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateLogisticAccommodationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="attendeePlaceRepository">The attendee place repository.</param>
        /// <param name="logisticAccommodationRepository">The logistic accommodation repository.</param>
        public UpdateLogisticAccommodationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IAttendeePlacesRepository attendeePlaceRepository,
            ILogisticAccommodationRepository logisticAccommodationRepository) 
            : base(eventBus, uow, logisticAccommodationRepository)
        {
            this.attendeePlaceRepo = attendeePlaceRepository;
        }

        /// <summary>
        /// Handles the specified create track.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;AppValidationResult&gt;.</returns>
        public async Task<AppValidationResult> Handle(UpdateLogisticAccommodation cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var logisticAccommodation = await this.GetLogisticAccommodationByUid(cmd.Uid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            logisticAccommodation.Update(cmd.AdditionalInfo,
                cmd.CheckInDate,
                cmd.CheckOutDate,
                attendeePlaceRepo.Get(cmd.PlaceId),
                cmd.UserId);
            
            if (!logisticAccommodation.IsValid())
            {
                this.AppValidationResult.Add(logisticAccommodation.ValidationResult);
                return this.AppValidationResult;
            }

            this.LogisticAccommodationRepo.Update(logisticAccommodation);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = logisticAccommodation;

            return this.AppValidationResult;
        }
    }
}