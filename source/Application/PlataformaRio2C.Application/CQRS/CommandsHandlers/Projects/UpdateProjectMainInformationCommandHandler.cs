// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-14-2019
// ***********************************************************************
// <copyright file="UpdateProjectMainInformationCommandHandler.cs" company="Softo">
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
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateProjectMainInformationCommandHandler</summary>
    public class UpdateProjectMainInformationCommandHandler : BaseProjectCommandHandler, IRequestHandler<UpdateProjectMainInformation, AppValidationResult>
    {
        private readonly ILanguageRepository languageRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateProjectMainInformationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="attendeeOrganizationRepository">The attendee organization repository.</param>
        /// <param name="projectRepository">The project repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        public UpdateProjectMainInformationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            IProjectRepository projectRepository,
            ILanguageRepository languageRepository)
            : base(eventBus, uow, attendeeOrganizationRepository, projectRepository)
        {
            this.languageRepo = languageRepository;
        }

        /// <summary>Handles the specified update project main information.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateProjectMainInformation cmd, CancellationToken cancellationToken)
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

            var languageDtos = await this.languageRepo.FindAllDtosAsync();

            project.UpdateMainInformation(
                //await this.projectTypeRepo.GetAsync(pt => pt.Uid == cmd.ProjectTypeUid),
                //cmd.AttendeeOrganizationUid.HasValue ? await this.attendeeOrganizationRepo.GetAsync(ao => ao.Uid == cmd.AttendeeOrganizationUid) : null,
                cmd.NumberOfEpisodes,
                cmd.EachEpisodePlayingTime,
                cmd.ValuePerEpisode,
                cmd.TotalValueOfProject,
                cmd.ValueAlreadyRaised,
                cmd.ValueStillNeeded,
                cmd.IsPitching ?? false,
                cmd.Titles?.Select(d => new ProjectTitle(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.LogLines?.Select(d => new ProjectLogLine(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.Summaries?.Select(d => new ProjectSummary(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.ProductPlans?.Select(d => new ProjectProductionPlan(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.AdditionalInformations?.Select(d => new ProjectAdditionalInformation(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
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