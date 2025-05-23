﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-18-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-21-2019
// ***********************************************************************
// <copyright file="DeleteHoldingCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2c.Infra.Data.FileRepository.Helpers;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>DeleteHoldingCommandHandler</summary>
    public class DeleteHoldingCommandHandler : BaseHoldingCommandHandler, IRequestHandler<DeleteHolding, AppValidationResult>
    {
        /// <summary>Initializes a new instance of the <see cref="DeleteHoldingCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="holdingRepository">The holding repository.</param>
        public DeleteHoldingCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IHoldingRepository holdingRepository)
            : base(eventBus, uow, holdingRepository)
        {
        }

        /// <summary>Handles the specified delete holding.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(DeleteHolding cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var holding = await this.GetHoldingByUid(cmd.HoldingUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            var beforeImageUploadDate = holding.ImageUploadDate;

            holding.Delete(cmd.UserId);
            if (!holding.IsValid())
            {
                this.AppValidationResult.Add(holding.ValidationResult);
                return this.AppValidationResult;
            }

            this.HoldingRepo.Update(holding);
            this.Uow.SaveChanges();

            if (beforeImageUploadDate.HasValue && holding.IsDeleted)
            {
                ImageHelper.DeleteOriginalAndCroppedImages(cmd.HoldingUid, FileRepositoryPathType.HoldingImage);
            }

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}