// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-16-2020
// ***********************************************************************
// <copyright file="UpdateMusicPlayerExecutiveCollaboratorCommandHandler.cs" company="Softo">
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
    public class UpdateMusicPlayerExecutiveCollaboratorCommandHandler : BaseCollaboratorCommandHandler, IRequestHandler<UpdateMusicPlayerExecutiveCollaborator, AppValidationResult>
    {
        private readonly IUserRepository userRepo;
        private readonly IEditionRepository editionRepo;
        private readonly ICollaboratorTypeRepository collaboratorTypeRepo;
        private readonly IAttendeeOrganizationRepository attendeeOrganizationRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly ICollaboratorGenderRepository genderRepo;
        private readonly ICollaboratorIndustryRepository industryRepo;
        private readonly ICollaboratorRoleRepository roleRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateMusicPlayerExecutiveCollaboratorCommandHandler"/> class.</summary>
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
        public UpdateMusicPlayerExecutiveCollaboratorCommandHandler(
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
            this.languageRepo = languageRepository;
            this.roleRepo = roleRepo;
        }

        /// <summary>Handles the specified update collaborator.</summary>
        /// <param name="cmd"></param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateMusicPlayerExecutiveCollaborator cmd, CancellationToken cancellationToken)
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

            if (cmd.EditionsUids == null || (!cmd.HaveYouBeenToRio2CBefore ?? false))
            {
                cmd.EditionsUids = new List<Guid>();
            }

            // Before update values
            var beforeImageUploadDate = collaborator.ImageUploadDate;
            var languageDtos = await this.languageRepo.FindAllDtosAsync();
            var edition = await this.editionRepo.GetAsync(cmd.EditionUid ?? Guid.Empty);
            var collaboratorType = await this.collaboratorTypeRepo.FindByNameAsync(cmd.CollaboratorTypeName);
            var collaboratorGender = genderRepo.Get(cmd.CollaboratorGenderUid ?? Guid.Empty);
            var collaboratorRole = roleRepo.Get(cmd.CollaboratorRoleUid ?? Guid.Empty);
            var collaboratorIndustry = industryRepo.Get(cmd.CollaboratorIndustryUid ?? Guid.Empty);
            var editions = this.editionRepo.GetAll(e => cmd.EditionsUids.Contains(e.Uid)).ToList();

            List<Guid> attendeeOrganizationUids = cmd.AttendeeOrganizationBaseCommands?.Where(ao => ao.AttendeeOrganizationUid.HasValue)?.Select(aobc => aobc.AttendeeOrganizationUid.Value)?.ToList();

            collaborator.UpdateMusicPlayerExecutive(
                attendeeOrganizationUids.Any() ? await this.attendeeOrganizationRepo.FindAllByUidsAsync(attendeeOrganizationUids) : null,
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