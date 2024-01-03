// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Elton Assunção
// Created          : 12-29-2023
//
// Last Modified By : Elton Assunção
// Last Modified On : 12-29-2023
// ***********************************************************************
// <copyright file="CreateMusicPlayerExecutiveCollaboratorCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2c.Infra.Data.FileRepository.Helpers;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    public class CreateMusicPlayerExecutiveCollaboratorCommandHandler : BaseCollaboratorCommandHandler, IRequestHandler<CreateMusicPlayerExecutiveCollaborator, AppValidationResult>
    {
        private readonly IUserRepository userRepo;
        private readonly IEditionRepository editionRepo;
        private readonly ICollaboratorTypeRepository collaboratorTypeRepo;
        private readonly IAttendeeOrganizationRepository attendeeOrganizationRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly ICollaboratorGenderRepository genderRepo;
        private readonly ICollaboratorIndustryRepository industryRepo;
        private readonly IActivityRepository activityRepo;
        private readonly IInterestRepository interestRepo;
        private readonly ITargetAudienceRepository targetAudienceRepo;
        private readonly ICollaboratorRoleRepository roleRepo;

        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="collaboratorTypeRepository">The collaborator type repository.</param>
        /// <param name="attendeeOrganizationRepository">The attendee organization repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        /// <param name="genderRepo">The gender repo.</param>
        /// <param name="industryRepo">The industry repo.</param>
        /// <param name="roleRepo">The role repo.</param>
        /// <param name="activityRepository">The activity repository.</param>
        /// <param name="interestRepository">The interest repository.</param>
        /// <param name="targetAudienceRepository">The target audience repository.</param>
        public CreateMusicPlayerExecutiveCollaboratorCommandHandler(
        IMediator eventBus,
            IUnitOfWork uow,
            ICollaboratorRepository collaboratorRepository,
            IUserRepository userRepository,
            IEditionRepository editionRepository,
            ICollaboratorTypeRepository collaboratorTypeRepository,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            ILanguageRepository languageRepository,
            ICollaboratorGenderRepository genderRepo,
            ICollaboratorIndustryRepository industryRepo,
            IActivityRepository activityRepository,
            IInterestRepository interestRepository,
            ITargetAudienceRepository targetAudienceRepository,
            ICollaboratorRoleRepository roleRepo)
            : base(eventBus, uow, collaboratorRepository)
        {
            this.userRepo = userRepository;
            this.editionRepo = editionRepository;
            this.collaboratorTypeRepo = collaboratorTypeRepository;
            this.attendeeOrganizationRepo = attendeeOrganizationRepository;
            this.languageRepo = languageRepository;
            this.genderRepo = genderRepo;
            this.industryRepo = industryRepo;
            this.activityRepo = activityRepository;
            this.interestRepo = interestRepository;
            this.targetAudienceRepo = targetAudienceRepository;
            this.roleRepo = roleRepo;
        }

        /// <summary>Handles the specified create tiny collaborator.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateMusicPlayerExecutiveCollaborator cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            #region Initial validations

            var user = await this.userRepo.GetAsync(u => u.Email == cmd.Email.Trim());

            // Return error only if the user is not deleted
            if (user != null && !user.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityExistsWithSameProperty, Labels.User.ToLowerInvariant(), $"{Labels.TheM.ToLowerInvariant()} {Labels.Email.ToLowerInvariant()}", cmd.Email), new string[] { "Email" }));
            }

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            var languageDtos = await this.languageRepo.FindAllDtosAsync();
            
            var activities = await this.activityRepo.FindAllByProjectTypeIdAsync(ProjectType.Music.Id);
            var interestsDtos = await this.interestRepo.FindAllDtosbyProjectTypeIdAsync(ProjectType.Music.Id);
            var targetAudiences = await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Music.Id);

            // Create if the user was not found in database
            if (user == null)
            {

                // Interests
                var attendeeCollaboratorInterests = new List<AttendeeCollaboratorInterest>();
                if (cmd.AttendeeCollaboratorInterests?.Any() == true)
                {
                    foreach (var interestBaseCommands in cmd.AttendeeCollaboratorInterests)
                    {
                        foreach (var interestBaseCommand in interestBaseCommands?.Where(ibc => ibc.IsChecked)?.ToList())
                        {
                            attendeeCollaboratorInterests.Add(new AttendeeCollaboratorInterest(interestsDtos?.FirstOrDefault(id => id.Interest.Uid == interestBaseCommand.InterestUid)?.Interest, interestBaseCommand.AdditionalInfo, cmd.UserId));
                        }
                    }
                }


                //todo: verificar com renan
                List<AttendeeOrganization> attendeeOrganization = new List<AttendeeOrganization>();
                if (cmd.AttendeeOrganizationBaseCommands.Where(x=>x.AttendeeOrganizationUid.HasValue).Any())
                    attendeeOrganization = await this.attendeeOrganizationRepo.FindAllByUidsAsync(cmd.AttendeeOrganizationBaseCommands?.Where(aobc => aobc.AttendeeOrganizationUid.HasValue)?.Select(aobc => aobc.AttendeeOrganizationUid.Value)?.ToList());
                

                var collaborator = Collaborator.CreateMusicPlayerExecutive(
                    attendeeOrganization,
                    await this.editionRepo.GetAsync(cmd.EditionUid ?? Guid.Empty),
                    await this.collaboratorTypeRepo.FindByNameAsync(cmd.CollaboratorTypeName),
                    cmd.BirthDate,
                    genderRepo.Get(cmd.CollaboratorGenderUid ?? Guid.Empty),
                    cmd.CollaboratorGenderAdditionalInfo,
                    roleRepo.Get(cmd.CollaboratorRoleUid ?? Guid.Empty),
                    cmd.CollaboratorRoleAdditionalInfo,
                    industryRepo.Get(cmd.CollaboratorIndustryUid ?? Guid.Empty),
                    cmd.CollaboratorIndustryAdditionalInfo,
                    cmd.HasAnySpecialNeeds,
                    cmd.SpecialNeedsDescription,
                    cmd.HaveYouBeenToRio2CBefore,
                    cmd.EditionsUids != null ? this.editionRepo.GetAll(e => cmd.EditionsUids.Contains(e.Uid)).ToList() : null,
                    cmd.FirstName,
                    cmd.LastNames,
                    cmd.Badge,
                    cmd.Email,
                    cmd.PhoneNumber,
                    cmd.CellPhone,
                    cmd.SharePublicEmail,
                    cmd.PublicEmail,
                    cmd.Website,
                    cmd.Linkedin,
                    cmd.Twitter,
                    cmd.Instagram,
                    cmd.Youtube,
                    cmd.CropperImage?.ImageFile != null,
                    cmd.JobTitles?.Select(d => new CollaboratorJobTitle(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                    cmd.MiniBios?.Select(d => new CollaboratorMiniBio(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                    cmd.IsVirtualMeeting,
                    cmd.AttendeeCollaboratorActivities?.Where(aca => aca.IsChecked)?.Select(aca => new AttendeeCollaboratorActivity(activities?.FirstOrDefault(a => a.Uid == aca.ActivityUid), aca.AdditionalInfo, cmd.UserId))?.ToList(),
                    attendeeCollaboratorInterests,
                    cmd.AttendeeCollaboratorTargetAudiences?.Where(ota => ota.IsChecked)?.Select(ota => new AttendeeCollaboratorTargetAudience(targetAudiences?.FirstOrDefault(a => a.Uid == ota.TargetAudienceUid), ota.AdditionalInfo, cmd.UserId))?.ToList(),
                    cmd.UserId);
                         
                if (!collaborator.IsValid())
                {
                    this.AppValidationResult.Add(collaborator.ValidationResult);
                    return this.AppValidationResult;
                }

                this.CollaboratorRepo.Create(collaborator);
                this.Uow.SaveChanges();
                this.AppValidationResult.Data = collaborator;
            }
            else
            {
                var updateCmd = new UpdateMusicPlayerExecutiveCollaborator
                {
                    CollaboratorUid = user.Collaborator.Uid,
                    IsAddingToCurrentEdition = true,
                    FirstName = cmd.FirstName,
                    LastNames = cmd.LastNames,
                    Badge = cmd.Badge,
                    Email = cmd.Email,
                    PhoneNumber = cmd.PhoneNumber,
                    CellPhone = cmd.CellPhone,
                    AttendeeOrganizationBaseCommands = cmd.AttendeeOrganizationBaseCommands,
                    JobTitles = cmd.JobTitles,
                    MiniBios = cmd.MiniBios,
                    CropperImage = cmd.CropperImage
                };
                updateCmd.UpdatePreSendProperties(cmd.CollaboratorTypeName, cmd.UserId, cmd.UserUid, cmd.EditionId, cmd.EditionUid, cmd.UserInterfaceLanguage);

                this.AppValidationResult = await this.CommandBus.Send(updateCmd, cancellationToken);
            }

            if (cmd.CropperImage?.ImageFile != null)
            {
                ImageHelper.UploadOriginalAndCroppedImages(
                    ((Collaborator)this.AppValidationResult.Data).Uid,
                    cmd.CropperImage.ImageFile,
                    cmd.CropperImage.DataX,
                    cmd.CropperImage.DataY,
                    cmd.CropperImage.DataWidth,
                    cmd.CropperImage.DataHeight,
                    FileRepositoryPathType.UserImage);
            }

            return this.AppValidationResult;
        }
    }
}