// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-11-2020
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

            var accommodation = await this.GetByUid(cmd.Uid);

            #region Initial validations

            //// Check if exists an user with the same email
            //var user = await this.repository.GetAsync(u => u.Email == cmd.Email.Trim());
            //if (user != null && (collaborator?.User == null || user.Uid != collaborator?.User?.Uid))
            //{
            //    this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityExistsWithSameProperty, Labels.Executive.ToLowerInvariant(), $"{Labels.TheM} {Labels.Email}", cmd.Email), new string[] { "Email" }));
            //}

            //if (!this.ValidationResult.IsValid)
            //{
            //    this.AppValidationResult.Add(this.ValidationResult);
            //    return this.AppValidationResult;
            //}

            #endregion

            accommodation.Update(cmd.AdditionalInfo,
                cmd.CheckInDate,
                cmd.CheckOutDate,
                attendeePlaceRepo.Get(cmd.PlaceId),
                cmd.UserId);
            
            if (!accommodation.IsValid())
            {
                this.AppValidationResult.Add(accommodation.ValidationResult);
                return this.AppValidationResult;
            }

            this.repository.Update(accommodation);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = accommodation;

            return this.AppValidationResult;
        }
    }
}