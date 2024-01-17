// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-06-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-17-2024
// ***********************************************************************
// <copyright file="OnboardCollaboratorDataCommandHandler.cs" company="Softo">
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
using PlataformaRio2c.Infra.Data.FileRepository.Helpers;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>OnboardCollaboratorDataCommandHandler</summary>
    public class OnboardCollaboratorDataCommandHandler : BaseCollaboratorCommandHandler, IRequestHandler<OnboardCollaboratorData, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly ICollaboratorGenderRepository genderRepo;
        private readonly ICollaboratorIndustryRepository industryRepo;
        private readonly ICollaboratorRoleRepository roleRepo;
        private readonly IActivityRepository activityRepo;
        private readonly IInterestRepository interestRepo;
        private readonly ITargetAudienceRepository targetAudienceRepo;
        private readonly IInnovationOrganizationTrackOptionRepository innovationOrganizationTrackOptionRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnboardCollaboratorDataCommandHandler" /> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        /// <param name="genderRepo">The gender repo.</param>
        /// <param name="industryRepo">The industry repo.</param>
        /// <param name="roleRepo">The role repo.</param>
        /// <param name="activityRepository">The activity repository.</param>
        /// <param name="interestRepository">The interest repository.</param>
        /// <param name="targetAudienceRepository">The target audience repository.</param>
        /// <param name="innovationOrganizationTrackOptionRepo">The innovation organization track option repo.</param>
        public OnboardCollaboratorDataCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ICollaboratorRepository collaboratorRepository,
            IEditionRepository editionRepository,
            ILanguageRepository languageRepository,
            ICollaboratorGenderRepository genderRepo,
            ICollaboratorIndustryRepository industryRepo,
            ICollaboratorRoleRepository roleRepo,
            IActivityRepository activityRepository,
            IInterestRepository interestRepository,
            ITargetAudienceRepository targetAudienceRepository,
            IInnovationOrganizationTrackOptionRepository innovationOrganizationTrackOptionRepo)
            : base(eventBus, uow, collaboratorRepository)
        {
            this.editionRepo = editionRepository;
            this.languageRepo = languageRepository;
            this.genderRepo = genderRepo;
            this.industryRepo = industryRepo;
            this.roleRepo = roleRepo;
            this.activityRepo = activityRepository;
            this.interestRepo = interestRepository;
            this.targetAudienceRepo = targetAudienceRepository;
            this.innovationOrganizationTrackOptionRepo = innovationOrganizationTrackOptionRepo;
        }

        /// <summary>Handles the specified onboard collaborator data.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(OnboardCollaboratorData cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var collaborator = await this.GetCollaboratorByUid(cmd.CollaboratorUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            // Before update values
            var beforeImageUploadDate = collaborator.ImageUploadDate;

            var languageDtos = await this.languageRepo.FindAllDtosAsync();
            var edition = await this.editionRepo.GetAsync(cmd.EditionUid ?? Guid.Empty);

            collaborator.OnboardData(
                edition,
                cmd.SharePublicEmail,
                cmd.PublicEmail,
                cmd.CropperImage?.ImageFile != null,
                cmd.JobTitles?.Select(d => new CollaboratorJobTitle(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.MiniBios?.Select(d => new CollaboratorMiniBio(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.BirthDate,
                genderRepo.Get(cmd.CollaboratorGenderUid ?? Guid.Empty),
                cmd.CollaboratorGenderAdditionalInfo,
                roleRepo.Get(cmd.CollaboratorRoleUid ?? Guid.Empty),
                cmd.CollaboratorRoleAdditionalInfo,
                industryRepo.Get(cmd.CollaboratorIndustryUid ?? Guid.Empty),
                cmd.CollaboratorIndustryAdditionalInfo,
                cmd.HasAnySpecialNeeds ?? false,
                cmd.SpecialNeedsDescription,
                cmd.HaveYouBeenToRio2CBefore,
                this.editionRepo.GetAll(e => cmd.EditionsUids.Contains(e.Uid)).ToList(),
                cmd.Website,
                cmd.Linkedin,
                cmd.Twitter,
                cmd.Instagram,
                cmd.Youtube,
                cmd.UserId);

            if (cmd.UserAccessControlDto.IsMusicPlayerExecutive())
            {
                var activities = await this.activityRepo.FindAllByProjectTypeIdAsync(ProjectType.Music.Id);
                var targetAudiences = await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Music.Id);

                // Interests
                var attendeeCollaboratorInterests = new List<AttendeeCollaboratorInterest>();
                if (cmd.MusicInterests?.Any() == true)
                {
                    var interestsDtos = await this.interestRepo.FindAllDtosbyProjectTypeIdAsync(ProjectType.Music.Id);

                    foreach (var interestBaseCommands in cmd.MusicInterests)
                    {
                        foreach (var interestBaseCommand in interestBaseCommands?.Where(ibc => ibc.IsChecked)?.ToList())
                        {
                            var interestDto = interestsDtos?.FirstOrDefault(id => id.Interest.Uid == interestBaseCommand.InterestUid);
                            attendeeCollaboratorInterests.Add(new AttendeeCollaboratorInterest(interestDto?.Interest, interestBaseCommand.AdditionalInfo, cmd.UserId));
                        }
                    }
                }

                collaborator.OnboardMusicPlayerData(
                    edition,
                    ProjectType.Music,
                    cmd.MusicAttendeeCollaboratorActivities?.Where(aca => aca.IsChecked)?.Select(aca => new AttendeeCollaboratorActivity(activities?.FirstOrDefault(a => a.Uid == aca.ActivityUid), aca.AdditionalInfo, cmd.UserId))?.ToList(),
                    cmd.MusicAttendeeCollaboratorTargetAudiences?.Where(ota => ota.IsChecked)?.Select(ota => new AttendeeCollaboratorTargetAudience(targetAudiences?.FirstOrDefault(a => a.Uid == ota.TargetAudienceUid), ota.AdditionalInfo, cmd.UserId))?.ToList(),
                    attendeeCollaboratorInterests,
                    cmd.UserId);
            }

            if (cmd.UserAccessControlDto.IsInnovationPlayerExecutive())
            {
                var activities = await this.activityRepo.FindAllByProjectTypeIdAsync(ProjectType.Startup.Id);
                var innovationOrganizationTrackOptions = await this.innovationOrganizationTrackOptionRepo.FindAllByGroupsUidsAsync(cmd.InnovationOrganizationTrackGroups
                                                                                                                                    ?.Where(ioto => ioto.IsChecked)
                                                                                                                                    ?.Select(ioto => ioto.InnovationOrganizationTrackOptionGroupUid));

                // Interests
                var attendeeCollaboratorInterests = new List<AttendeeCollaboratorInterest>();
                if (cmd.InnovationInterests?.Any() == true)
                {
                    var interestsDtos = await this.interestRepo.FindAllDtosbyProjectTypeIdAsync(ProjectType.Startup.Id);

                    foreach (var interestBaseCommands in cmd.InnovationInterests)
                    {
                        foreach (var interestBaseCommand in interestBaseCommands?.Where(ibc => ibc.IsChecked)?.ToList())
                        {
                            var interestDto = interestsDtos?.FirstOrDefault(id => id.Interest.Uid == interestBaseCommand.InterestUid);
                            attendeeCollaboratorInterests.Add(new AttendeeCollaboratorInterest(interestDto?.Interest, interestBaseCommand.AdditionalInfo, cmd.UserId));
                        }
                    }
                }

                collaborator.OnboardInnovationPlayerData(
                    edition,
                    ProjectType.Startup,
                    cmd.InnovationAttendeeCollaboratorActivities?.Where(aca => aca.IsChecked)?.Select(aca => new AttendeeCollaboratorActivity(activities?.FirstOrDefault(a => a.Uid == aca.ActivityUid), aca.AdditionalInfo, cmd.UserId))?.ToList(),
                    innovationOrganizationTrackOptions.Select(ioto => new AttendeeCollaboratorInnovationOrganizationTrack(ioto, string.Empty, cmd.UserId)).ToList(),
                    attendeeCollaboratorInterests,
                    cmd.UserId);
            }

            if (!collaborator.IsValid())
            {
                this.AppValidationResult.Add(collaborator.ValidationResult);
                return this.AppValidationResult;
            }

            this.CollaboratorRepo.Update(collaborator);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = collaborator;

            // Update images
            if (cmd.CropperImage?.ImageFile != null)
            {
                ImageHelper.UploadOriginalAndCroppedImages(
                    collaborator.Uid,
                    cmd.CropperImage.ImageFile,
                    cmd.CropperImage.DataX,
                    cmd.CropperImage.DataY,
                    cmd.CropperImage.DataWidth,
                    cmd.CropperImage.DataHeight,
                    FileRepositoryPathType.UserImage);
            }
            // Delete images
            else if (cmd.CropperImage?.IsImageDeleted == true && beforeImageUploadDate.HasValue)
            {
                ImageHelper.DeleteOriginalAndCroppedImages(collaborator.Uid, FileRepositoryPathType.UserImage);
            }

            return this.AppValidationResult;
        }
    }
}