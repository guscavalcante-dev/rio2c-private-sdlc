// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-11-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-11-2019
// ***********************************************************************
// <copyright file="UpdateProjectInterestsCommandHandler.cs" company="Softo">
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
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateProjectInterestsCommandHandler</summary>
    public class UpdateProjectInterestsCommandHandler : BaseProjectCommandHandler, IRequestHandler<UpdateProjectInterests, AppValidationResult>
    {
        private readonly IProjectTypeRepository projectTypeRepo;
        private readonly IAttendeeOrganizationRepository attendeeOrganizationRepo;
        private readonly IInterestRepository interestRepo;
        private readonly ITargetAudienceRepository targetAudienceRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateProjectInterestsCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="projectRepository">The project repository.</param>
        /// <param name="projectTypeRepository">The project type repository.</param>
        /// <param name="attendeeOrganizationRepository">The attendee organization repository.</param>
        /// <param name="interestRepository">The interest repository.</param>
        /// <param name="targetAudienceRepository">The target audience repository.</param>
        public UpdateProjectInterestsCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IProjectRepository projectRepository,
            IProjectTypeRepository projectTypeRepository,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            IInterestRepository interestRepository,
            ITargetAudienceRepository targetAudienceRepository)
            : base(eventBus, uow, projectRepository)
        {
            this.projectTypeRepo = projectTypeRepository;
            this.attendeeOrganizationRepo = attendeeOrganizationRepository;
            this.interestRepo = interestRepository;
            this.targetAudienceRepo = targetAudienceRepository;
        }

        /// <summary>Handles the specified update project interests.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateProjectInterests cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var project = await this.GetProjectByUid(cmd.ProjectUid ?? Guid.Empty);

            #region Initial validations

            //// Check if the trade name already exists
            //var existingOrganizationByName = this.OrganizationRepo.Get(o => o.TradeName == cmd.TradeName
            //                                                                && o.HoldingId == organization.HoldingId
            //                                                                && o.Uid != cmd.OrganizationUid
            //                                                                && !o.IsDeleted);
            //if (existingOrganizationByName != null)
            //{
            //    this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityExistsWithSameProperty, Labels.APlayer, Labels.TheName, cmd.TradeName), new string[] { "TradeName" }));
            //}

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            project.UpdateInterests(
                //await this.projectTypeRepo.GetAsync(pt => pt.Uid == cmd.ProjectTypeUid),
                //cmd.AttendeeOrganizationUid.HasValue ? await this.attendeeOrganizationRepo.GetAsync(ao => ao.Uid == cmd.AttendeeOrganizationUid) : null,
                cmd.InterestsUids?.Any() == true ? await this.interestRepo.FindAllByUidsAsync(cmd.InterestsUids) : new List<Interest>(),
                cmd.UserId);

            project.UpdateTargetAudiences(
                //await this.projectTypeRepo.GetAsync(pt => pt.Uid == cmd.ProjectTypeUid),
                //cmd.AttendeeOrganizationUid.HasValue ? await this.attendeeOrganizationRepo.GetAsync(ao => ao.Uid == cmd.AttendeeOrganizationUid) : null,
                cmd.TargetAudiencesUids?.Any() == true ? await this.targetAudienceRepo.FindAllByUidsAsync(cmd.TargetAudiencesUids) : new List<TargetAudience>(),
                cmd.UserId);
            if (!project.IsValid())
            {
                this.AppValidationResult.Add(project.ValidationResult);
                return this.AppValidationResult;
            }

            this.ProjectRepo.Update(project);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = project;

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}