// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-07-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-13-2025
// ***********************************************************************
// <copyright file="CreateProjectCommandHandler.cs" company="Softo">
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
using PlataformaRio2C.Domain.Interfaces.Repositories.Music.BusinessRoundProjects;
using PlataformaRio2C.Domain.Interfaces.Repositories.Music.Projects;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateProjectCommandHandler</summary>
    public class CreateMusicBusinessRoundProjectCommandHandler : BaseMusicBusinessRoundProjectCommandHandler, IRequestHandler<CreateMusicBusinessRoundProject, AppValidationResult>
    {
        private readonly ILanguageRepository languageRepo;
        private readonly ITargetAudienceRepository targetAudienceRepo;
        private readonly IInterestRepository interestRepo;
        private readonly IMusicBusinessRoundProjectRepository musicBusinessRoundProjectRepo;
        private readonly IActivityRepository activityRepo;
        private readonly IPlayersCategoryRepository playersCategoryRepo;

        /// <summary>Initializes a new instance of the <see cref="CreateAudiovisualBusinessRoundProjectCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="attendeeOrganizationRepository">The attendee organization repository.</param>
        /// <param name="projectRepository">The project repository.</param>
        /// <param name="projectTypeRepository">The project type repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        /// <param name="targetAudienceRepository">The target audience repository.</param>
        /// <param name="interestRepository">The interest repository.</param>
        /// <param name="projectModalityRepo">The project modality repository.</param>
        public CreateMusicBusinessRoundProjectCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            ILanguageRepository languageRepository,
            ITargetAudienceRepository targetAudienceRepository,
            IInterestRepository interestRepository,
            IMusicBusinessRoundProjectRepository musicProjectRepo,
            IActivityRepository activityRepo,
            IPlayersCategoryRepository playersCategoryRepo)
            : base(eventBus, uow, attendeeOrganizationRepository, musicProjectRepo)
        {
            this.musicBusinessRoundProjectRepo = musicProjectRepo;
            this.languageRepo = languageRepository;
            this.targetAudienceRepo = targetAudienceRepository;
            this.interestRepo = interestRepository;
            this.activityRepo = activityRepo;
            this.playersCategoryRepo = playersCategoryRepo;
        }

        /// <summary>Handles the specified create project.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateMusicBusinessRoundProject cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            var languageDtos = await this.languageRepo.FindAllDtosAsync();
            var interestsDtos = await this.interestRepo.FindAllDtosByProjectTypeIdAsync(ProjectType.Music.Id);

            #region Interests Iterations
            // Interests
            var projectInterests = new List<MusicBusinessRoundProjectInterest>();
            if (cmd.Interests?.Any() == true)
            {
                foreach (var interestBaseCommands in cmd.Interests)
                {
                    foreach (var interestBaseCommand in interestBaseCommands?.Where(ibc => ibc.IsChecked)?.ToList())
                    {
                        projectInterests.Add(new MusicBusinessRoundProjectInterest(interestsDtos?.FirstOrDefault(id => id.Interest.Uid == interestBaseCommand.InterestUid)?.Interest, interestBaseCommand.AdditionalInfo, cmd.UserId));
                    }
                }
            }
            #endregion

            var musicProject = new MusicBusinessRoundProject(cmd.SellerAttendeeCollaboratorId, cmd.PlayerCategoriesThatHaveOrHadContract, cmd.AttachmentUrl, null
                , cmd.TargetAudiencesUids?.Any() == true ? await this.targetAudienceRepo.FindAllByUidsAsync(cmd.TargetAudiencesUids) : new List<TargetAudience>()
                , projectInterests
                , cmd.PlayerCategoriesUids?.Any() == true ? await this.playersCategoryRepo.FindAllByUidsAsync(cmd.PlayerCategoriesUids) : new List<PlayerCategory>()
                , cmd.ActivitiesUids?.Any() == true ? await this.activityRepo.FindAllByUidsAsync(cmd.ActivitiesUids) : new List<Activity>()
                , cmd.MusicBusinessRoundProjectExpectationsForMeetings?.Select(d => new MusicBusinessRoundProjectExpectationsForMeeting(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList()
                , cmd.UserId );


            this.musicBusinessRoundProjectRepo.Create(musicProject);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = musicProject;

            return this.AppValidationResult;

        }
    }
}