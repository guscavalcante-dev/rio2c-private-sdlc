// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-27-2019
// ***********************************************************************
// <copyright file="DeleteCollaboratorCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2c.Infra.Data.FileRepository.Helpers;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>DeleteCollaboratorCommandHandler</summary>
    public class DeleteCollaboratorCommandHandler : BaseCollaboratorCommandHandler, IRequestHandler<DeleteCollaborator, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly IOrganizationTypeRepository organizationTypeRepo;

        public DeleteCollaboratorCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ICollaboratorRepository collaboratorRepository,
            IEditionRepository editionRepository,
            IOrganizationTypeRepository organizationTypeRepository)
            : base(eventBus, uow, collaboratorRepository)
        {
            this.editionRepo = editionRepository;
            this.organizationTypeRepo = organizationTypeRepository;
        }

        /// <summary>Handles the specified delete collaborator.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(DeleteCollaborator cmd, CancellationToken cancellationToken)
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

            var edition = await this.editionRepo.GetAsync(cmd.EditionUid ?? Guid.Empty);
            var organizationType = await this.organizationTypeRepo.GetAsync(cmd.OrganizationType?.Uid ?? Guid.Empty);

            // Before update values
            var beforeImageUploadDate = collaborator.ImageUploadDate;

            collaborator.Delete(edition, organizationType, cmd.UserId);
            if (!collaborator.IsValid())
            {
                this.AppValidationResult.Add(collaborator.ValidationResult);
                return this.AppValidationResult;
            }

            this.CollaboratorRepo.Update(collaborator);
            this.Uow.SaveChanges();

            if (beforeImageUploadDate.HasValue && collaborator.IsDeleted)
            {
                ImageHelper.DeleteOriginalAndCroppedImages(cmd.CollaboratorUid, FileRepositoryPathType.UserImage);
            }

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}