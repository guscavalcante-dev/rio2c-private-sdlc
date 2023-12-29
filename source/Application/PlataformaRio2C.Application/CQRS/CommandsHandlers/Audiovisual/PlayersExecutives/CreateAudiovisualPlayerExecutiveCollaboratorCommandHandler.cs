// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 09-13-2021
// ***********************************************************************
// <copyright file="CreateAudiovisualPlayerExecutiveCollaboratorCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
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
    public class CreateAudiovisualPlayerExecutiveCollaboratorCommandHandler : BaseCollaboratorCommandHandler, IRequestHandler<CreateAudiovisualPlayerExecutiveCollaborator, AppValidationResult>
    {
        private readonly IUserRepository userRepo;
        private readonly IEditionRepository editionRepo;
        private readonly ICollaboratorTypeRepository collaboratorTypeRepo;
        private readonly IAttendeeOrganizationRepository attendeeOrganizationRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly ICollaboratorGenderRepository genderRepo;
        private readonly ICollaboratorIndustryRepository industryRepo;
        private readonly ICollaboratorRoleRepository roleRepo;

        /// <summary>Initializes a new instance of the <see cref="CreateAudiovisualPlayerExecutiveCollaboratorCommandHandler"/> class.</summary>
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
        public CreateAudiovisualPlayerExecutiveCollaboratorCommandHandler(
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
            this.roleRepo = roleRepo;
        }

        /// <summary>Handles the specified create collaborator.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateAudiovisualPlayerExecutiveCollaborator cmd, CancellationToken cancellationToken)
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

            // Create if the user was not found in database
            if (user == null)
            {
                var collaborator = Collaborator.CreateAudiovisualPlayerExecutiveCollaborator(
                    await this.attendeeOrganizationRepo.FindAllByUidsAsync(cmd.AttendeeOrganizationBaseCommands?.Where(aobc => aobc.AttendeeOrganizationUid.HasValue)?.Select(aobc => aobc.AttendeeOrganizationUid.Value)?.ToList()),
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
                var updateCmd = new UpdateAudiovisualPlayerExecutiveCollaborator
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