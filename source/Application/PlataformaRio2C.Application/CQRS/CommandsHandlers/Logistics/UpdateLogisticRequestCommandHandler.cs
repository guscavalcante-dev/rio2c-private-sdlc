// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-08-2020
// ***********************************************************************
// <copyright file="UpdateLogisticAccomodationCommandHandler.cs" company="Softo">
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
    /// <summary>
    /// UpdateLogisticAccomodationCommandHandler
    /// </summary>
    public class UpdateLogisticRequestCommandHandler : LogisticsBaseCommandHandler, IRequestHandler<UpdateLogisticRequest, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly IAttendeeLogisticSponsorRepository attendeeLogisticSponsorRepo;
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PlataformaRio2C.Application.CQRS.CommandsHandlers.CreateLogisticsCommandHandler" /> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="logisticsRepo">The logistics repo.</param>
        /// <param name="placeRepo">The place repo.</param>
        /// <param name="repository">The repository.</param>
        public UpdateLogisticRequestCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IEditionRepository editionRepository,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepo,
            IAttendeeLogisticSponsorRepository attendeeLogisticSponsorRepo,
            ILogisticsRepository repository) : base(eventBus, uow, repository)
        {
            this.editionRepo = editionRepository;
            this.attendeeLogisticSponsorRepo = attendeeLogisticSponsorRepo;
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepo;
        }

        /// <summary>
        /// Handles the specified create track.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;AppValidationResult&gt;.</returns>
        public async Task<AppValidationResult> Handle(UpdateLogisticRequest cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var logistics = await this.GetByUid(cmd.LogisticRequestUid);

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

            logistics.Update(
                    this.editionRepo.Get(cmd.EditionId),
                    this.attendeeCollaboratorRepo.Get(cmd.AttendeeCollaboratorUid),
                    cmd.IsAirfareSponsored,
                    this.attendeeLogisticSponsorRepo.Get(cmd.AirfareSponsorOtherUid ?? cmd.AirfareSponsorUid ?? Guid.Empty),
                    cmd.AirfareSponsorOtherName,
                    cmd.IsAccommodationSponsored,
                    this.attendeeLogisticSponsorRepo.Get(cmd.AccommodationSponsorOtherUid ?? cmd.AccommodationSponsorUid ?? Guid.Empty),
                    cmd.AccommodationSponsorOtherName,
                    cmd.IsAirportTransferSponsored,
                    this.attendeeLogisticSponsorRepo.Get(cmd.AirportTransferSponsorOtherUid ?? cmd.AirportTransferSponsorUid ?? Guid.Empty),
                    cmd.AirportTransferSponsorOtherName,
                    cmd.IsCityTransferRequired,
                    cmd.IsVehicleDisposalRequired,
                    cmd.AdditionalInfo,
                    cmd.UserId);

            if (!logistics.IsValid())
            {
                this.AppValidationResult.Add(logistics.ValidationResult);
                return this.AppValidationResult;
            }

            this.repository.Update(logistics);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = logistics;

            return this.AppValidationResult;
        }
    }
}