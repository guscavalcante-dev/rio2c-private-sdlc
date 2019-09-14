// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-13-2019
// ***********************************************************************
// <copyright file="OnboardPlayerInterestsCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
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
    /// <summary>OnboardPlayerInterestsCommandHandler</summary>
    public class OnboardPlayerInterestsCommandHandler : BaseOrganizationCommandHandler, IRequestHandler<OnboardPlayerInterests, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly IOrganizationTypeRepository organizationTypeRepo;
        private readonly IInterestRepository interestRepo;
        private readonly ILanguageRepository languageRepo;

        /// <summary>Initializes a new instance of the <see cref="OnboardPlayerInterestsCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="organizationRepository">The organization repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="organizationTypeRepository">The organization type repository.</param>
        /// <param name="interestRepository">The interest repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        public OnboardPlayerInterestsCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IOrganizationRepository organizationRepository,
            IEditionRepository editionRepository,
            IOrganizationTypeRepository organizationTypeRepository,
            IInterestRepository interestRepository,
            ILanguageRepository languageRepository)
            : base(eventBus, uow, organizationRepository)
        {
            this.editionRepo = editionRepository;
            this.organizationTypeRepo = organizationTypeRepository;
            this.interestRepo = interestRepository;
            this.languageRepo = languageRepository;
        }

        /// <summary>Handles the specified onboard player interests.</summary>
        /// <param name="cmd"></param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(OnboardPlayerInterests cmd, CancellationToken cancellationToken)
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

            var languageDtos = await this.languageRepo.FindAllDtosAsync();

            organization.OnboardInterests(
                await this.editionRepo.GetAsync(cmd.EditionUid ?? Guid.Empty),
                await this.organizationTypeRepo.GetAsync(cmd.OrganizationType?.Uid ?? Guid.Empty),
                cmd.RestrictionSpecifics?.Select(d => new OrganizationRestrictionSpecific(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.InterestsUids?.Any() == true ? await this.interestRepo.FindAllByUidsAsync(cmd.InterestsUids) : new List<Interest>(),
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