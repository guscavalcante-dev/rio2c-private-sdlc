// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 12-29-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-29-2023
// ***********************************************************************
// <copyright file="UpdateInnovationPlayerExecutiveCollaboratorCommandHandler.cs" company="Softo">
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
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    public class UpdateInnovationPlayerExecutiveCollaboratorCommandHandler : BaseCollaboratorCommandHandler, IRequestHandler<UpdateInnovationPlayerExecutiveCollaborator, AppValidationResult>
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
        /// Initializes a new instance of the <see cref="UpdateInnovationPlayerExecutiveCollaboratorCommandHandler" /> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="collaboratorTypeRepository">The collaborator type repository.</param>
        /// <param name="attendeeOrganizationRepository">The attendee organization repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        /// <param name="genderRepository">The gender repo.</param>
        /// <param name="industryRepository">The industry repo.</param>
        /// <param name="roleRepository">The role repo.</param>
        /// <param name="activityRepository">The activity repository.</param>
        /// <param name="interestRepository">The interest repository.</param>
        /// <param name="innovationOrganizationTrackOptionRepository">The innovation organization track option repository.</param>
        public UpdateInnovationPlayerExecutiveCollaboratorCommandHandler(
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
            this.languageRepo = languageRepository;
            this.roleRepo = roleRepository;
            this.activityRepo = activityRepository;
            this.interestRepo = interestRepository;
            this.innovationOrganizationTrackOptionRepo = innovationOrganizationTrackOptionRepository;
        }

        /// <summary>Handles the specified update collaborator.</summary>
        /// <param name="cmd"></param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateInnovationPlayerExecutiveCollaborator cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var collaborator = await this.GetCollaboratorByUid(cmd.CollaboratorUid);

            #region Initial validations

            // Check if exists an user with the same email
            var user = await this.userRepo.GetAsync(u => u.Email == cmd.Email.Trim() && !u.IsDeleted);
            if (user != null && (collaborator?.User == null || user.Uid != collaborator?.User?.Uid))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityExistsWithSameProperty, Labels.User.ToLowerInvariant(), $"{Labels.TheM.ToLowerInvariant()} {Labels.Email.ToLowerInvariant()}", cmd.Email), new string[] { "Email" }));
            }

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            if(cmd.EditionsUids == null || (!cmd.HaveYouBeenToRio2CBefore ?? false))
            {
                cmd.EditionsUids = new List<Guid>();
            }

            // Before update values
            var beforeImageUploadDate = collaborator.ImageUploadDate;

            var languageDtos = await this.languageRepo.FindAllDtosAsync();

            var attendeeOrganizations = cmd.AttendeeOrganizationBaseCommands == null ? new List <AttendeeOrganization>() :
                                        await this.attendeeOrganizationRepo.FindAllByUidsAsync(cmd.AttendeeOrganizationBaseCommands?.Where(aobc => aobc.AttendeeOrganizationUid.HasValue)?.Select(aobc => aobc.AttendeeOrganizationUid.Value)?.ToList());

            var edition = await this.editionRepo.GetAsync(cmd.EditionUid ?? Guid.Empty);
            var collaboratorType = await this.collaboratorTypeRepo.FindByNameAsync(cmd.CollaboratorTypeName);
            var collaboratorGender = genderRepo.Get(cmd.CollaboratorGenderUid ?? Guid.Empty);
            var collaboratorRole = roleRepo.Get(cmd.CollaboratorRoleUid ?? Guid.Empty);
            var collaboratorIndustry = industryRepo.Get(cmd.CollaboratorIndustryUid ?? Guid.Empty);
            var editions = this.editionRepo.GetAll(e => cmd.EditionsUids.Contains(e.Uid)).ToList();
            var interestsDtos = await this.interestRepo.FindAllDtosbyProjectTypeIdAsync(ProjectType.Startup.Id);
            var activities = await this.activityRepo.FindAllByProjectTypeIdAsync(ProjectType.Startup.Id);
            var innovationOrganizationTrackOptions = await this.innovationOrganizationTrackOptionRepo.FindAllDtoAsync();

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

            collaborator.UpdateInnovationPlayerExecutiveCollaborator(
                attendeeOrganizations,
                edition,
                collaboratorType,
                cmd.BirthDate,
                collaboratorGender,
                cmd.CollaboratorGenderAdditionalInfo,
                collaboratorRole,
                cmd.CollaboratorRoleAdditionalInfo,
                collaboratorIndustry,
                cmd.CollaboratorIndustryAdditionalInfo,
                cmd.HasAnySpecialNeeds ?? false,
                cmd.SpecialNeedsDescription,
                cmd.HaveYouBeenToRio2CBefore,
                editions,
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
                cmd.CropperImage?.IsImageDeleted == true,
                cmd.JobTitles?.Select(d => new CollaboratorJobTitle(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.MiniBios?.Select(d => new CollaboratorMiniBio(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.AttendeeCollaboratorActivities?.Where(aca => aca.IsChecked)?.Select(aca => new AttendeeCollaboratorActivity(activities?.FirstOrDefault(a => a.Uid == aca.ActivityUid), aca.AdditionalInfo, cmd.UserId))?.ToList(),
                attendeeCollaboratorInterests,
                cmd.AttendeeCollaboratorInnovationOrganizationTracks?.Where(aciot => aciot.IsChecked)?.Select(aciot => new AttendeeCollaboratorInnovationOrganizationTrack(innovationOrganizationTrackOptions?.FirstOrDefault(ioto => ioto.Uid == aciot.InnovationOrganizationTrackOptionUid)?.InnovationOrganizationTrackOption, aciot.AdditionalInfo, cmd.UserId))?.ToList(),
                true, // TODO: Get isAddingToCurrentEdition from command for UpdateCollaborator
                cmd.UserId);

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

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}