// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-07-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-07-2019
// ***********************************************************************
// <copyright file="CreateProjectCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
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
    /// <summary>CreateProjectCommandHandler</summary>
    public class CreateProjectCommandHandler : BaseProjectCommandHandler, IRequestHandler<CreateProject, AppValidationResult>
    {
        private readonly IProjectTypeRepository projectTypeRepo;
        private readonly IAttendeeOrganizationRepository attendeeOrganizationRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly ITargetAudienceRepository targetAudienceRepo;
        private readonly IInterestRepository interestRepo;

        public CreateProjectCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IProjectRepository projectRepository,
            IProjectTypeRepository projectTypeRepository,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            ILanguageRepository languageRepository,
            ITargetAudienceRepository targetAudienceRepository,
            IInterestRepository interestRepository)
            : base(eventBus, uow, projectRepository)
        {
            this.projectTypeRepo = projectTypeRepository;
            this.attendeeOrganizationRepo = attendeeOrganizationRepository;
            this.languageRepo = languageRepository;
            this.targetAudienceRepo = targetAudienceRepository;
            this.interestRepo = interestRepository;
        }

        /// <summary>Handles the specified create project.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateProject cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            //#region Initial validations

            //var existingOrganizationByName = this.OrganizationRepo.Get(o => o.Name == cmd.Name
            //                                                                && o.Holding.Uid == cmd.HoldingUid
            //                                                                && !o.IsDeleted);
            //if (existingOrganizationByName != null)
            //{
            //    this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityExistsWithSameProperty, Labels.APlayer, Labels.TheName, cmd.Name), new string[] { "Name" }));
            //}

            //if (!this.ValidationResult.IsValid)
            //{
            //    this.AppValidationResult.Add(this.ValidationResult);
            //    return this.AppValidationResult;
            //}

            //#endregion

            var languageDtos = await this.languageRepo.FindAllDtosAsync();

            var project = new Project(
                await this.projectTypeRepo.GetAsync(pt => pt.Uid == cmd.ProjectTypeUid),
                cmd.AttendeeOrganizationUid.HasValue ? await this.attendeeOrganizationRepo.GetAsync(ao => ao.Uid == cmd.AttendeeOrganizationUid) : null,
                cmd.NumberOfEpisodes ?? 0, //TODO: Set to nullable
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
                cmd.InterestsUids?.Any() == true ? await this.interestRepo.FindAllByUidsAsync(cmd.InterestsUids) : new List<Interest>(),
                //cmd.TargetAudiencesUids?.Any() == true ? await this.targetAudienceRepo.FindAllByUidsAsync(cmd.TargetAudiencesUids) : new List<TargetAudience>(),
                cmd.UserId);
            if (!project.IsValid())
            {
                this.AppValidationResult.Add(project.ValidationResult);
                return this.AppValidationResult;
            }

            this.ProjectRepo.Create(project);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = project;

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}