// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-10-2019
// ***********************************************************************
// <copyright file="UpdateOrganizationAddressCommandHandler.cs" company="Softo">
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
    /// <summary>UpdateOrganizationAddressCommandHandler</summary>
    public class UpdateOrganizationAddressCommandHandler : BaseOrganizationCommandHandler, IRequestHandler<UpdateOrganizationAddress, AppValidationResult>
    {
        private readonly ICountryRepository countryRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateOrganizationAddressCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="organizationRepository">The organization repository.</param>
        /// <param name="countryRepository">The country repository.</param>
        public UpdateOrganizationAddressCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IOrganizationRepository organizationRepository,
            ICountryRepository countryRepository)
            : base(eventBus, uow, organizationRepository)
        {
            this.countryRepo = countryRepository;
        }

        /// <summary>Handles the specified update organization address.</summary>
        /// <param name="cmd">The comand.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateOrganizationAddress cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var organization = await this.GetOrganizationByUid(cmd.OrganizationUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            organization.UpdateAddress(
                await this.countryRepo.GetAsync(cmd?.CountryUid ?? Guid.Empty),
                cmd.Address?.StateUid,
                cmd.Address?.StateName,
                cmd.Address?.CityUid,
                cmd.Address?.CityName,
                cmd.Address?.Address1,
                cmd.Address?.AddressZipCode,
                true, //TODO: get AddressIsManual from form
                cmd.UserId);
            if (!organization.IsValid())
            {
                this.AppValidationResult.Add(organization.ValidationResult);
                return this.AppValidationResult;
            }

            this.OrganizationRepo.Update(organization);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = organization;

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}