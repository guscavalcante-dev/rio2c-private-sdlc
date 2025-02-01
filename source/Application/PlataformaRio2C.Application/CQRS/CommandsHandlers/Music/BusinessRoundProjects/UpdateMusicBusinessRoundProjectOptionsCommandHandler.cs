// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Gilson Oliveira
// Created          : 01-30-2025
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 01-31-2025
// ***********************************************************************
// <copyright file="UpdateMusicBusinessRoundProjectOptionsCommandHandler.cs" company="Softo">
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
using PlataformaRio2C.Domain.Interfaces.Repositories.Music.Projects;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Domain.Dtos;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Interfaces.Repositories.Music.BusinessRoundProjects;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateMusicBusinessRoundProjectOptionsCommandHandler</summary>
    public class UpdateMusicBusinessRoundProjectOptionsCommandHandler : BaseCommandHandler, IRequestHandler<UpdateMusicBusinessRoundProjectOptions, AppValidationResult>
    {
        private readonly ILanguageRepository languageRepo;
        private readonly IMusicBusinessRoundProjectRepository musicBusinessRoundProjectRepo;
        private readonly IInterestRepository interestRepo;
        private readonly ITargetAudienceRepository targetAudienceRepo;
        private readonly IActivityRepository activityRepo;
        private readonly IPlayersCategoryRepository playerCategoryRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMusicBusinessRoundProjectOptionsCommandHandler" /> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="languageRepository">The language repository.</param>
        /// <param name="musicBusinessRoundProjectRepo">The music business round project repo.</param>
        /// <param name="interestRepository">The interest repository.</param>
        /// <param name="targetAudienceRepo">The target audience repository.</param>
        /// <param name="activityRepo">The activity repository.</param>
        /// <param name="playerCategoryRepo">The player category repository.</param>
        public UpdateMusicBusinessRoundProjectOptionsCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ILanguageRepository languageRepository,
            IMusicBusinessRoundProjectRepository musicBusinessRoundProjectRepo,
            IInterestRepository interestRepository,
            ITargetAudienceRepository targetAudienceRepo,
            IActivityRepository activityRepo,
            IPlayersCategoryRepository playerCategoryRepo
        )
            : base(eventBus, uow)
        {
            this.languageRepo = languageRepository;
            this.musicBusinessRoundProjectRepo = musicBusinessRoundProjectRepo;
            this.interestRepo = interestRepository;
            this.targetAudienceRepo = targetAudienceRepo;
            this.activityRepo = activityRepo;
            this.playerCategoryRepo = playerCategoryRepo;
        }

        /// <summary>Handles the specified update project main information.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateMusicBusinessRoundProjectOptions cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var musicProject = await this.GetMusicProjectByUid(cmd.MusicProjectUid ?? Guid.Empty);
            var interestsDtos = await this.interestRepo.FindAllDtosByProjectTypeIdAsync(ProjectType.Music.Id);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            #region Interests Iterations       

            var musicBusinessRoundProjectInterests = new List<MusicBusinessRoundProjectInterest>();
            if (cmd.Interests?.Any() == true)
            {
                foreach (var interestBaseCommands in cmd.Interests)
                {
                    foreach (var interestBaseCommand in interestBaseCommands?.Where(ibc => ibc.IsChecked)?.ToList())
                    {
                        musicBusinessRoundProjectInterests.Add(
                            new MusicBusinessRoundProjectInterest(
                                interestsDtos?.FirstOrDefault(id => id.Interest.Uid == interestBaseCommand.InterestUid)?.Interest, interestBaseCommand.AdditionalInfo, cmd.UserId
                            )
                        );
                    }
                }
            }
            
            #endregion

            musicProject.UpdateInterests(
                musicBusinessRoundProjectInterests,
                cmd.UserId
            );

            musicProject.UpdateTargetAudiences(
                cmd.TargetAudiencesUids?.Any() == true ? await this.targetAudienceRepo.FindAllByUidsAsync(cmd.TargetAudiencesUids) : new List<TargetAudience>(),
                cmd.UserId
            );

            musicProject.UpdateActivities(
                cmd.ActivitiesUids?.Any() == true ? await this.activityRepo.FindAllByUidsAsync(cmd.ActivitiesUids) : new List<Activity>(),
                cmd.UserId
            );

            musicProject.UpdatePlayerCategories(
                cmd.PlayerCategoriesUids?.Any() == true ? await this.playerCategoryRepo.FindAllByUidsAsync(cmd.PlayerCategoriesUids) : new List<PlayerCategory>(),
                cmd.UserId
            );

            musicProject.UpdatePlayerCategoriesThatHaveOrHadContract(cmd.PlayerCategoriesThatHaveOrHadContract);

            if (!musicProject.IsValid())
            {
                this.AppValidationResult.Add(musicProject.ValidationResult);
                return this.AppValidationResult;
            }

            this.musicBusinessRoundProjectRepo.Update(musicProject);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = musicProject;

            return this.AppValidationResult;
        }

        /// <summary>Gets the music project by uid.</summary>
        /// <param name="musicProjectUid">The music project uid.</param>
        /// <returns></returns>
        public async Task<MusicBusinessRoundProject> GetMusicProjectByUid(Guid musicProjectUid)
        {
            var musicProject = await this.musicBusinessRoundProjectRepo.GetAsync(musicProjectUid);
            if (musicProject == null || musicProject.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM), new string[] { "ToastrError" }));
                return null;
            }
            return musicProject;
        }
    }
}