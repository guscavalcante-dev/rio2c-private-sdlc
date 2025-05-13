// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 10-07-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-07-2021
// ***********************************************************************
// <copyright file="UpdateOrganizationApiConfigurationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateOrganizationApiConfigurationCommandHandler</summary>
    public class UpdateOrganizationApiConfigurationCommandHandler : BaseOrganizationCommandHandler, IRequestHandler<UpdateOrganizationApiConfiguration, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly IOrganizationTypeRepository organizationTypeRepo;
        private readonly ILanguageRepository languageRepo;

        public UpdateOrganizationApiConfigurationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IOrganizationRepository organizationRepository,
            IEditionRepository editionRepository,
            IOrganizationTypeRepository organizationTypeRepository,
            ILanguageRepository languageRepository)
            : base(eventBus, uow, organizationRepository)
        {
            this.editionRepo = editionRepository;
            this.organizationTypeRepo = organizationTypeRepository;
            this.languageRepo = languageRepository;
        }

        /// <summary>Handles the specified update organization API configuration.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateOrganizationApiConfiguration cmd, CancellationToken cancellationToken)
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

            var edition = await this.editionRepo.GetAsync(cmd.EditionUid ?? Guid.Empty);
            var organizationType = await this.organizationTypeRepo.FindByUidAsync(cmd.OrganizationTypeUid);

            organization.UpdateApiConfiguration(
                edition,
                organizationType,
                cmd.IsApiDisplayEnabled,
                cmd.ApiHighlightPosition,
                cmd.UserId);
            if (!organization.IsValid())
            {
                this.AppValidationResult.Add(organization.ValidationResult);
                return this.AppValidationResult;
            }

            this.OrganizationRepo.Update(organization);

            #region Disable same highlight position of other organizations

            if (cmd.IsApiDisplayEnabled && cmd.ApiHighlightPosition.HasValue)
            {
                var sameHighlightPositionOrganizations = await this.OrganizationRepo.FindAllByHightlightPosition(
                    cmd.EditionId ?? 0,
                    organizationType?.Uid ?? Guid.Empty,
                    cmd.ApiHighlightPosition.Value,
                    organization.Uid);
                if (sameHighlightPositionOrganizations?.Any() == true)
                {
                    foreach (var sameHighlightPositionOrganization in sameHighlightPositionOrganizations)
                    {
                        sameHighlightPositionOrganization.DeleteApiHighlightPosition(edition, organizationType, cmd.UserId);
                        this.OrganizationRepo.Update(sameHighlightPositionOrganization);
                    }
                }
            }

            #endregion

            this.Uow.SaveChanges();
            this.AppValidationResult.Data = organization;

            return this.AppValidationResult;
        }

    }
}