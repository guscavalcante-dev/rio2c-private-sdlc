// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-08-2020
// ***********************************************************************
// <copyright file="UpdateLogisticAirfareCommandHandler - Copy.cs" company="Softo">
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
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>
    /// CreateLogisticsCommandHandler
    /// </summary>
    public class UpdateLogisticTransferCommandHandler : LogisticTransferBaseCommandHandler, IRequestHandler<UpdateLogisticTransfer, AppValidationResult>
    {
        /// <summary>
        /// The logistics repo
        /// </summary>
        private readonly ILogisticsRepository logisticsRepo;
        private readonly IAttendeePlacesRepository placeRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PlataformaRio2C.Application.CQRS.CommandsHandlers.CreateLogisticsCommandHandler" /> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="logisticsRepo">The logistics repo.</param>
        /// <param name="repository">The repository.</param>
        public UpdateLogisticTransferCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ILogisticsRepository logisticsRepo,
            IAttendeePlacesRepository placeRepo,
            ILogisticTransferRepository repository) : base(eventBus, uow, repository)
        {
            this.logisticsRepo = logisticsRepo;
            this.placeRepo = placeRepo;
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

            var entity = await this.GetByUid(cmd.Uid);

            #region Initial validations
            if (!cmd.Date.HasValue)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Departure), new string[] { "Date" }));
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }
            #endregion

            entity.Update(
                cmd.AdditionalInfo,
                cmd.Date.Value,
                placeRepo.Get(cmd.FromAttendeePlaceId),
                placeRepo.Get(cmd.ToAttendeePlaceId),
                cmd.UserId);
            
            if (!entity.IsValid())
            {
                this.AppValidationResult.Add(entity.ValidationResult);
                return this.AppValidationResult;
            }

            this.repository.Update(entity);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = entity;

            return this.AppValidationResult;
        }
    }
}