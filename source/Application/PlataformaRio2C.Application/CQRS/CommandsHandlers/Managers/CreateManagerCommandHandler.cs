// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-10-2020
// ***********************************************************************
// <copyright file="CreateManagerCommandHandler.cs" company="Softo">
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
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateManagerCommandHandler</summary>
    public class CreateManagerCommandHandler : BaseCollaboratorCommandHandler, IRequestHandler<CreateManager, AppValidationResult>
    {
        private readonly IUserRepository userRepo;
        private readonly IEditionRepository editionRepo;
        private readonly ICollaboratorTypeRepository collaboratorTypeRepo;
        private readonly IAttendeeOrganizationRepository attendeeOrganizationRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly ICollaboratorGenderRepository genderRepo;
        private readonly ICollaboratorIndustryRepository industryRepo;
        private readonly ICollaboratorRoleRepository collaboratorRoleRepo;
        private readonly IRoleRepository roleRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateManagerCommandHandler"/> class.
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
        /// <param name="collaboratorRoleRepository">The collaborator role repository.</param>
        /// <param name="roleRepository">The role repository.</param>
        public CreateManagerCommandHandler(
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
            ICollaboratorRoleRepository collaboratorRoleRepository,
            IRoleRepository roleRepository)
            : base(eventBus, uow, collaboratorRepository)
        {
            this.userRepo = userRepository;
            this.editionRepo = editionRepository;
            this.collaboratorTypeRepo = collaboratorTypeRepository;
            this.attendeeOrganizationRepo = attendeeOrganizationRepository;
            this.languageRepo = languageRepository;
            this.genderRepo = genderRepository;
            this.industryRepo = industryRepository;
            this.collaboratorRoleRepo = collaboratorRoleRepository;
            this.roleRepo = roleRepository;
        }

        /// <summary>Handles the specified create collaborator.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateManager cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var collaboratorUid = Guid.NewGuid();

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

            Collaborator collaborator = null;

            if (cmd.CollaboratorTypeNames.HasValue())
            {
                collaborator = new Collaborator(
                                     await this.editionRepo.GetAsync(cmd.EditionUid ?? Guid.Empty),
                                     await this.collaboratorTypeRepo.FindByNamesAsync(cmd.CollaboratorTypeNames),
                                     cmd.FirstName,
                                     cmd.LastNames,
                                     cmd.Email,
                                     cmd.PasswordHash,
                                     cmd.UserId);
            }
            else
            {
                collaborator = new Collaborator(
                                    cmd.FirstName,
                                    cmd.LastNames,
                                    cmd.Email,
                                    cmd.PasswordHash,
                                    await this.roleRepo.FindByNameAsync(cmd.RoleName),
                                    cmd.UserId);
            }

            if (!collaborator.IsValid())
            {
                this.AppValidationResult.Add(collaborator.ValidationResult);
                return this.AppValidationResult;
            }

            this.CollaboratorRepo.Create(collaborator);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = collaborator;


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

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}