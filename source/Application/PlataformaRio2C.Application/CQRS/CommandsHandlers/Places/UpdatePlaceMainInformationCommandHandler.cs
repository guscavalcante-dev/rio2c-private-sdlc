// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-17-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="UpdatePlaceMainInformationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdatePlaceMainInformationCommandHandler</summary>
    public class UpdatePlaceMainInformationCommandHandler : PlaceBaseCommandHandler, IRequestHandler<UpdatePlaceMainInformation, AppValidationResult>
    {
        private readonly ICountryRepository countryRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdatePlaceMainInformationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="placeRepository">The place repository.</param>
        /// <param name="countryRepository">The country repository.</param>
        public UpdatePlaceMainInformationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IPlaceRepository placeRepository,
            ICountryRepository countryRepository)
            : base(eventBus, uow, placeRepository)
        {
            this.countryRepo = countryRepository;
        }

        /// <summary>Handles the specified update place main information.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdatePlaceMainInformation cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var place = await this.GetPlaceByUid(cmd.PlaceUid ?? Guid.Empty);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            place.UpdateMainInformation(
                cmd.Name,
                cmd.Type,
                cmd.Website,
                cmd.AdditionalInfo,
                await this.countryRepo.GetAsync(cmd.CountryUid ?? Guid.Empty),
                cmd.Address?.StateUid,
                cmd.Address?.StateName,
                cmd.Address?.CityUid,
                cmd.Address?.CityName,
                cmd.Address?.Address1,
                cmd.Address?.AddressZipCode,
                cmd.UserId);

            if (!place.IsValid())
            {
                this.AppValidationResult.Add(place.ValidationResult);
                return this.AppValidationResult;
            }

            this.PlaceRepo.Update(place);

            this.Uow.SaveChanges();
            this.AppValidationResult.Data = place;

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}