// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-13-2020
// ***********************************************************************
// <copyright file="CreateLogisticTransferCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateLogisticTransferCommandHandler</summary>
    public class CreateLogisticTransferCommandHandler : LogisticTransferBaseCommandHandler, IRequestHandler<CreateLogisticTransfer, AppValidationResult>
    {
        private readonly ILogisticRepository logisticRepo;
        private readonly IAttendeePlacesRepository placeRepo;

        /// <summary>Initializes a new instance of the <see cref="CreateLogisticTransferCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="logisticRepository">The logistic repository.</param>
        /// <param name="placeRepository">The place repository.</param>
        /// <param name="logisticTransferRepository">The logistic transfer repository.</param>
        public CreateLogisticTransferCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ILogisticRepository logisticRepository,
            IAttendeePlacesRepository placeRepository,
            ILogisticTransferRepository logisticTransferRepository) 
            : base(eventBus, uow, logisticTransferRepository)
        {
            this.logisticRepo = logisticRepository;
            this.placeRepo = placeRepository;
        }

        /// <summary>Handles the specified create track.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateLogisticTransfer cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var logisticTransfer = new LogisticTransfer(
                cmd.AdditionalInfo, 
                cmd.Date,
                placeRepo.Get(cmd.FromAttendeePlaceId),
                placeRepo.Get(cmd.ToAttendeePlaceId),
                logisticRepo.Get(cmd.LogisticsUid),
                cmd.UserId);

            if (!logisticTransfer.IsValid())
            {
                this.AppValidationResult.Add(logisticTransfer.ValidationResult);
                return this.AppValidationResult;
            }

            this.LogisticTransferRepo.Create(logisticTransfer);

            this.Uow.SaveChanges();
            this.AppValidationResult.Data = logisticTransfer;

            return this.AppValidationResult;
        }
    }
}