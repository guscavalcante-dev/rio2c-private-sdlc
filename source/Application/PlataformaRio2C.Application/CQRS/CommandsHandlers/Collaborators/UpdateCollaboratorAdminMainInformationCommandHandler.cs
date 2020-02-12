// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-16-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-18-2019
// ***********************************************************************
// <copyright file="UpdateCollaboratorAdminMainInformationCommandHandler.cs" company="Softo">
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
    /// <summary>UpdateCollaboratorAdminMainInformationCommandHandler</summary>
    public class UpdateCollaboratorAdminMainInformationCommandHandler : BaseCollaboratorCommandHandler, IRequestHandler<UpdateCollaboratorAdminMainInformation, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly ICollaboratorTypeRepository collaboratorTypeRepo;
        private readonly ILanguageRepository languageRepo;  
        private readonly ICollaboratorGenderRepository genderRepo;
        private readonly ICollaboratorIndustryRepository industryRepo;
        private readonly ICollaboratorRoleRepository roleRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateCollaboratorAdminMainInformationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="collaboratorTypeRepository">The collaborator type repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        public UpdateCollaboratorAdminMainInformationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ICollaboratorRepository collaboratorRepository,
            IEditionRepository editionRepository,
            ICollaboratorTypeRepository collaboratorTypeRepository,
            ILanguageRepository languageRepository,
            ICollaboratorGenderRepository genderRepo,
            ICollaboratorIndustryRepository industryRepo,
            ICollaboratorRoleRepository roleRepo)
            : base(eventBus, uow, collaboratorRepository)
        {
            this.editionRepo = editionRepository;
            this.collaboratorTypeRepo = collaboratorTypeRepository;
            this.languageRepo = languageRepository;
            this.genderRepo = genderRepo;
            this.industryRepo = industryRepo;
            this.roleRepo = roleRepo;
        }

        /// <summary>Handles the specified update collaborator admin main information.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateCollaboratorAdminMainInformation cmd, CancellationToken cancellationToken)
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
            if(cmd.EditionsUids == null || (!cmd.HaveYouBeenToRio2CBefore ?? false)) cmd.EditionsUids = new List<Guid>();
            collaborator.UpdateAdminMainInformation(
                await this.collaboratorTypeRepo.FindByNameAsunc(cmd.CollaboratorTypeName),
                cmd.FirstName,
                cmd.LastNames,
                cmd.Email,
                cmd.Badge,
                cmd.CellPhone,
                cmd.PhoneNumber,
                cmd.SharePublicEmail,
                cmd.PublicEmail,
                cmd.CropperImage?.ImageFile != null,
                cmd.CropperImage?.IsImageDeleted == true,
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
                await this.editionRepo.GetAsync(cmd.EditionUid ?? Guid.Empty),
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