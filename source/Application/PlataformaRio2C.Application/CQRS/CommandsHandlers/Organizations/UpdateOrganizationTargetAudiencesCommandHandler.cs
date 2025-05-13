// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-10-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-23-2023
// ***********************************************************************
// <copyright file="UpdateOrganizationTargetAudiencesCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateOrganizationTargetAudiencesCommandHandler</summary>
    public class UpdateOrganizationTargetAudiencesCommandHandler : BaseOrganizationCommandHandler, IRequestHandler<UpdateOrganizationTargetAudiences, AppValidationResult>
    {
        private readonly ITargetAudienceRepository targetAudienceRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateOrganizationTargetAudiencesCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="organizationRepository">The organization repository.</param>
        /// <param name="targetAudienceRepository">The target audience repository.</param>
        public UpdateOrganizationTargetAudiencesCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IOrganizationRepository organizationRepository,
            ITargetAudienceRepository targetAudienceRepository)
            : base(eventBus, uow, organizationRepository)
        {
            this.targetAudienceRepo = targetAudienceRepository;
        }

        /// <summary>Handles the specified update organization target audiences.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateOrganizationTargetAudiences cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var organization = await this.GetOrganizationByUid(cmd.OrganizationUid);
            var targetAudiences = await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(
                cmd.ProjectTypeId
            );

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            organization.UpdateOrganizationTargetAudiences(
                cmd.OrganizationTargetAudiences
                    ?.Where(ota => ota.IsChecked)
                    ?.Select(ota =>
                        new OrganizationTargetAudience(
                            targetAudiences?.FirstOrDefault(a => a.Uid == ota.TargetAudienceUid),
                            ota.AdditionalInfo,
                            cmd.UserId
                        )
                    )
                    ?.ToList(),
                cmd.UserId,
                cmd.ProjectTypeId
            );
            if (!organization.IsValid())
            {
                this.AppValidationResult.Add(organization.ValidationResult);
                return this.AppValidationResult;
            }

            this.OrganizationRepo.Update(organization);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = organization;

            return this.AppValidationResult;
        }
    }
}