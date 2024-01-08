// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 12-29-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-29-2023
// ***********************************************************************
// <copyright file="CreateInnovationPlayerExecutiveCollaboratorCommandHandler.cs" company="Softo">
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
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    public class CreateInnovationPlayerExecutiveCollaboratorCommandHandler : BaseCollaboratorCommandHandler, IRequestHandler<CreateInnovationPlayerExecutiveCollaborator, AppValidationResult>
    {
        private readonly IUserRepository userRepo;
        private readonly IEditionRepository editionRepo;
        private readonly ICollaboratorTypeRepository collaboratorTypeRepo;
        private readonly IAttendeeOrganizationRepository attendeeOrganizationRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly ICollaboratorGenderRepository genderRepo;
        private readonly ICollaboratorIndustryRepository industryRepo;
        private readonly ICollaboratorRoleRepository roleRepo;
        private readonly IActivityRepository activityRepo;
        private readonly IInterestRepository interestRepo;
        private readonly IInnovationOrganizationTrackOptionRepository innovationOrganizationTrackOptionRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInnovationPlayerExecutiveCollaboratorCommandHandler"/> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="collaboratorTypeRepository">The collaborator type repository.</param>
        /// <param name="attendeeOrganizationRepository">The attendee organization repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        /// <param name="genderRepository">The gender repository.</param>
        /// <param name="industryRepository">The industry repository.</param>
        /// <param name="roleRepository">The role repository.</param>
        /// <param name="activityRepository">The activity repository.</param>
        /// <param name="interestRepository">The interest repository.</param>
        /// <param name="innovationOrganizationTrackOptionRepository">The innovation organization track option repository.</param>
        public CreateInnovationPlayerExecutiveCollaboratorCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ICollaboratorRepository collaboratorRepository,
            IUserRepository userRepository,
            IEditionRepository editionRepository,
            ICollaboratorTypeRepository collaboratorTypeRepository,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            ILanguageRepository languageRepository,
            ICollaboratorGenderRepository genderRepository,
            ICollaboratorIndustryRepository industryRepository,
            ICollaboratorRoleRepository roleRepository,
            IActivityRepository activityRepository,
            IInterestRepository interestRepository,
            IInnovationOrganizationTrackOptionRepository innovationOrganizationTrackOptionRepository)
            : base(eventBus, uow, collaboratorRepository)
        {
            this.userRepo = userRepository;
            this.editionRepo = editionRepository;
            this.collaboratorTypeRepo = collaboratorTypeRepository;
            this.attendeeOrganizationRepo = attendeeOrganizationRepository;
            this.languageRepo = languageRepository;
            this.genderRepo = genderRepository;
            this.industryRepo = industryRepository;
            this.roleRepo = roleRepository;
            this.activityRepo = activityRepository;
            this.interestRepo = interestRepository;
            this.innovationOrganizationTrackOptionRepo = innovationOrganizationTrackOptionRepository;
        }

        /// <summary>Handles the specified create tiny collaborator.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateInnovationPlayerExecutiveCollaborator cmd, CancellationToken cancellationToken)
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
            var activities = await this.activityRepo.FindAllByProjectTypeIdAsync(ProjectType.Startup.Id);
            var interestsDtos = await this.interestRepo.FindAllDtosbyProjectTypeIdAsync(ProjectType.Startup.Id);
            var innovationOrganizationTrackOptions = await this.innovationOrganizationTrackOptionRepo.FindAllDtoAsync();

            // Create if the user was not found in database
            if (user == null)
            {
                // Interests
                var attendeeCollaboratorInterests = new List<AttendeeCollaboratorInterest>();
                if (cmd.Interests?.Any() == true)
                {
                    foreach (var interestBaseCommands in cmd.Interests)
                    {
                        foreach (var interestBaseCommand in interestBaseCommands?.Where(ibc => ibc.IsChecked)?.ToList())
                        {
                            attendeeCollaboratorInterests.Add(new AttendeeCollaboratorInterest(interestsDtos?.FirstOrDefault(id => id.Interest.Uid == interestBaseCommand.InterestUid)?.Interest, interestBaseCommand.AdditionalInfo, cmd.UserId));
                        }
                    }
                }

                List<Guid> attendeeOrganizationUids = cmd.AttendeeOrganizationBaseCommands?.Where(ao => ao.AttendeeOrganizationUid.HasValue)?.Select(aobc => aobc.AttendeeOrganizationUid.Value)?.ToList();

                var collaborator = Collaborator.CreateInnovationPlayerExecutiveCollaborator(
                    attendeeOrganizationUids.Any() ? await this.attendeeOrganizationRepo.FindAllByUidsAsync(attendeeOrganizationUids) : null,
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
                    cmd.AttendeeCollaboratorActivities?.Where(aca => aca.IsChecked)?.Select(aca => new AttendeeCollaboratorActivity(activities?.FirstOrDefault(a => a.Uid == aca.ActivityUid), aca.AdditionalInfo, cmd.UserId))?.ToList(),
                    attendeeCollaboratorInterests,
                    cmd.AttendeeCollaboratorInnovationOrganizationTracks?.Where(aciot => aciot.IsChecked)?.Select(aciot => new AttendeeCollaboratorInnovationOrganizationTrack(innovationOrganizationTrackOptions?.FirstOrDefault(ioto => ioto.Uid == aciot.InnovationOrganizationTrackOptionUid)?.InnovationOrganizationTrackOption, aciot.AdditionalInfo, cmd.UserId))?.ToList(),
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
                //TODO: This is not the correct way to instantiate a command. Create a new constructor accepting this parameters and use it.
                //TODO: Use "UpdateInnovationPlayerExecutiveCollaborator" instead of "UpdateAudiovisualPlayerExecutiveCollaborator" and test.
                var updateCmd = new UpdateAudiovisualPlayerExecutiveCollaborator
                {
                    CollaboratorUid = user.Collaborator.Uid,
                    IsAddingToCurrentEdition = true,
                    FirstName = cmd.FirstName,
                    LastNames = cmd.LastNames,
                    Email = cmd.Email,
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