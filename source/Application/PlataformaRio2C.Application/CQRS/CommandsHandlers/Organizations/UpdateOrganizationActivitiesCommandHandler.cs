// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-22-2019
// ***********************************************************************
// <copyright file="UpdateOrganizationActivitiesCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
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
    /// <summary>UpdateOrganizationActivitiesCommandHandler</summary>
    public class UpdateOrganizationActivitiesCommandHandler : BaseOrganizationCommandHandler, IRequestHandler<UpdateOrganizationActivities, AppValidationResult>
    {
        private readonly IActivityRepository activityRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateOrganizationActivitiesCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="organizationRepository">The organization repository.</param>
        /// <param name="activityRepository">The activity repository.</param>
        public UpdateOrganizationActivitiesCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IOrganizationRepository organizationRepository,
            IActivityRepository activityRepository)
            : base(eventBus, uow, organizationRepository)
        {
            this.activityRepo = activityRepository;
        }

        /// <summary>Handles the specified update organization activities.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateOrganizationActivities cmd, CancellationToken cancellationToken)
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

            var activities = await this.activityRepo.FindAllByProjectTypeIdAsync(
                cmd.ProjectTypeId ?? ProjectType.AudiovisualBusinessRound.Id
            );

            organization.UpdateOrganizationActivities(
                cmd.OrganizationActivities
                    ?.Where(oa => oa.IsChecked)
                    ?.Select(oa => 
                        new OrganizationActivity(
                            activities?.FirstOrDefault(a => a.Uid == oa.ActivityUid),
                            oa.AdditionalInfo,
                            cmd.UserId
                        )
                    )
                    ?.ToList(),
                cmd.UserId,
                cmd.ProjectTypeId ?? ProjectType.AudiovisualBusinessRound.Id);
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