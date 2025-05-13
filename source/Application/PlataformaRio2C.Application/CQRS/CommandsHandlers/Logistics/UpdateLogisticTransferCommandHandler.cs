// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-13-2020
// ***********************************************************************
// <copyright file="UpdateLogisticTransferCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateLogisticTransferCommandHandler</summary>
    public class UpdateLogisticTransferCommandHandler : LogisticTransferBaseCommandHandler, IRequestHandler<UpdateLogisticTransfer, AppValidationResult>
    {
        private readonly IAttendeePlacesRepository placeRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateLogisticTransferCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="placeRepository">The place repository.</param>
        /// <param name="logisticTransferRepository">The logistic transfer repository.</param>
        public UpdateLogisticTransferCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IAttendeePlacesRepository placeRepository,
            ILogisticTransferRepository logisticTransferRepository)
            : base(eventBus, uow, logisticTransferRepository)
        {
            this.placeRepo = placeRepository;
        }

        /// <summary>
        /// Handles the specified create track.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;AppValidationResult&gt;.</returns>
        public async Task<AppValidationResult> Handle(UpdateLogisticTransfer cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var logisticTransfer = await this.GetLogisticTransferByUid(cmd.Uid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            logisticTransfer.Update(
                cmd.AdditionalInfo,
                cmd.Date,
                placeRepo.Get(cmd.FromAttendeePlaceId),
                placeRepo.Get(cmd.ToAttendeePlaceId),
                cmd.UserId);

            if (!logisticTransfer.IsValid())
            {
                this.AppValidationResult.Add(logisticTransfer.ValidationResult);
                return this.AppValidationResult;
            }

            this.LogisticTransferRepo.Update(logisticTransfer);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = logisticTransfer;

            return this.AppValidationResult;
        }
    }
}