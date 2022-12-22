// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : William Almado
// Created          : 10-15-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-22-2022
// ***********************************************************************
// <copyright file="UpdateCollaboratorSiteMainInformationCommandHandler.cs" company="Softo">
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
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateCollaboratorSiteMainInformationCommandHandler</summary>
    public class UpdateCollaboratorSiteMainInformationCommandHandler : BaseCollaboratorCommandHandler, IRequestHandler<UpdateCollaboratorSiteMainInformation, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly ILanguageRepository languageRepo;        
        private readonly ICollaboratorGenderRepository genderRepo;
        private readonly ICollaboratorIndustryRepository industryRepo;
        private readonly ICollaboratorRoleRepository roleRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateCollaboratorSiteMainInformationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        public UpdateCollaboratorSiteMainInformationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ICollaboratorRepository collaboratorRepository,
            IEditionRepository editionRepository,
            ILanguageRepository languageRepository,
            ICollaboratorGenderRepository genderRepo,
            ICollaboratorIndustryRepository industryRepo,
            ICollaboratorRoleRepository roleRepo)
            : base(eventBus, uow, collaboratorRepository)
        {
            this.editionRepo = editionRepository;
            this.languageRepo = languageRepository;
            this.genderRepo = genderRepo;
            this.industryRepo = industryRepo;
            this.roleRepo = roleRepo;
        }

        /// <summary>Handles the specified update collaborator site main information.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateCollaboratorSiteMainInformation cmd, CancellationToken cancellationToken)
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

            collaborator.UpdateSiteMainInformation(
                cmd.FirstName,
                cmd.LastNames,
                cmd.Badge,
                cmd.CellPhone,
                cmd.PhoneNumber,
                cmd.SharePublicEmail,
                cmd.PublicEmail,
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
                cmd.EditionsUids != null ? this.editionRepo.GetAll(e => cmd.EditionsUids.Contains(e.Uid)).ToList() : null,
                await this.editionRepo.GetAsync(cmd.EditionUid ?? Guid.Empty), 
                cmd.CropperImage?.ImageFile != null,
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