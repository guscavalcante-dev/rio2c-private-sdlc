// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-17-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="CreatePlaceCommandHandler.cs" company="Softo">
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
    /// <summary>CreatePlaceCommandHandler</summary>
    public class CreatePlaceCommandHandler : PlaceBaseCommandHandler, IRequestHandler<CreatePlace, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;

        /// <summary>Initializes a new instance of the <see cref="CreatePlaceCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="placeRepository">The place repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        public CreatePlaceCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IPlaceRepository placeRepository,
            IEditionRepository editionRepository)
            : base(eventBus, uow, placeRepository)
        {
            this.editionRepo = editionRepository;
        }

        /// <summary>Handles the specified create place.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreatePlace cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            Place place;

            // New place
            if (!cmd.PlaceUid.HasValue)
            {
                var placeUid = Guid.NewGuid();

                place = new Place(
                    placeUid,
                    await this.editionRepo.GetAsync(cmd.EditionId ?? 0),
                    cmd.Name,
                    cmd.Type,
                    cmd.Website,
                    cmd.AdditionalInfo,
                    cmd.UserId);

                if (!place.IsValid())
                {
                    this.AppValidationResult.Add(place.ValidationResult);
                    return this.AppValidationResult;
                }

                this.PlaceRepo.Create(place);
            }
            // Adding place to edition
            else
            {
                place = await this.GetPlaceByUid(cmd.PlaceUid.Value);

                #region Initial validations

                if (!this.ValidationResult.IsValid)
                {
                    this.AppValidationResult.Add(this.ValidationResult);
                    return this.AppValidationResult;
                }

                #endregion

                place.Update(
                    await this.editionRepo.GetAsync(cmd.EditionId ?? 0),
                    cmd.Name,
                    cmd.Type,
                    cmd.Website,
                    cmd.AdditionalInfo,
                    cmd.UserId);

                if (!place.IsValid())
                {
                    this.AppValidationResult.Add(place.ValidationResult);
                    return this.AppValidationResult;
                }

                this.PlaceRepo.Update(place);
            }

            this.Uow.SaveChanges();
            this.AppValidationResult.Data = place;

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}