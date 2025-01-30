// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Gilson Oliveira
// Created          : 29-10-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-13-2025
// ***********************************************************************
// <copyright file="CreatePitchingProjectCommandHandler.cs" company="Softo">
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
    /// <summary>CreatePitchingProjectCommandHandler</summary>
    public class CreatePitchingProjectCommandHandler : BaseProjectCommandHandler, IRequestHandler<CreatePitchingProject, AppValidationResult>
    {
        private readonly IProjectTypeRepository projectTypeRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly ITargetAudienceRepository targetAudienceRepo;
        private readonly IInterestRepository interestRepo;
        private readonly IProjectModalityRepository projectModalityRepo;

        /// <summary>Initializes a new instance of the <see cref="CreatePitchingProjectCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="attendeeOrganizationRepository">The attendee organization repository.</param>
        /// <param name="projectRepository">The project repository.</param>
        /// <param name="projectTypeRepository">The project type repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        /// <param name="targetAudienceRepository">The target audience repository.</param>
        /// <param name="interestRepository">The interest repository.</param>
        /// <param name="projectModalityRepo">The project modality repository.</param>
        public CreatePitchingProjectCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            IProjectRepository projectRepository,
            IProjectTypeRepository projectTypeRepository,
            ILanguageRepository languageRepository,
            ITargetAudienceRepository targetAudienceRepository,
            IInterestRepository interestRepository,
            IProjectModalityRepository projectModalityRepo)
            : base(eventBus, uow, attendeeOrganizationRepository, projectRepository)
        {
            this.projectTypeRepo = projectTypeRepository;
            this.languageRepo = languageRepository;
            this.targetAudienceRepo = targetAudienceRepository;
            this.interestRepo = interestRepository;
            this.projectModalityRepo = projectModalityRepo;
        }

        /// <summary>Handles the specified create project.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreatePitchingProject cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var attendeeOrganization = await this.GetAttendeeOrganizationByUid(cmd.AttendeeOrganizationUid ?? Guid.Empty);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            var languageDtos = await this.languageRepo.FindAllDtosAsync();
            var interestsDtos = await this.interestRepo.FindAllDtosByProjectTypeIdAsync(ProjectType.Audiovisual.Id);

            // Interests
            var projectInterests = new List<ProjectInterest>();
            if (cmd.Interests?.Any() == true)
            {
                foreach (var interestBaseCommands in cmd.Interests)
                {
                    foreach (var interestBaseCommand in interestBaseCommands?.Where(ibc => ibc.IsChecked)?.ToList())
                    {
                        projectInterests.Add(new ProjectInterest(interestsDtos?.FirstOrDefault(id => id.Interest.Uid == interestBaseCommand.InterestUid)?.Interest, interestBaseCommand.AdditionalInfo, cmd.UserId));
                    }
                }
            }

            var projectModality = await this.projectModalityRepo.GetAsync(pm => pm.Uid == ProjectModality.Pitching.Uid && !pm.IsDeleted);

            attendeeOrganization.CreateProject(
                await this.projectTypeRepo.GetAsync(pt => pt.Uid == cmd.ProjectTypeUid && !pt.IsDeleted),
                cmd.TotalPlayingTime,
                cmd.NumberOfEpisodes,
                cmd.EachEpisodePlayingTime,
                cmd.ValuePerEpisode,
                cmd.TotalValueOfProject,
                cmd.ValueAlreadyRaised,
                cmd.ValueStillNeeded,
                cmd.HasAnyTypeOfFinancing,
                cmd.WhichTypeOfFinancingDescription,
                cmd.Titles?.Select(d => new ProjectTitle(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.LogLines?.Select(d => new ProjectLogLine(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.Summaries?.Select(d => new ProjectSummary(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.ProductPlans?.Select(d => new ProjectProductionPlan(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.AdditionalInformations?.Select(d => new ProjectAdditionalInformation(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                projectInterests,
                cmd.TargetAudiencesUids?.Any() == true ? await this.targetAudienceRepo.FindAllByUidsAsync(cmd.TargetAudiencesUids) : new List<TargetAudience>(),
                cmd.ImageLinks,
                cmd.TeaserLinks,
                cmd.UserId,
                projectModality
            );
            
            if (!attendeeOrganization.IsCreatePitchingValid())
            {
                this.AppValidationResult.Add(attendeeOrganization.ValidationResult);
                return this.AppValidationResult;
            }

            this.AttendeeOrganizationRepo.Update(attendeeOrganization);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = attendeeOrganization.GetLastCreatedProject();

            return this.AppValidationResult;
        }
    }
}