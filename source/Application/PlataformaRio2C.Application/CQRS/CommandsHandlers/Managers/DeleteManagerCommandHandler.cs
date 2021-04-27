// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 04-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-24-2021
// ***********************************************************************
// <copyright file="DeleteManagerCommandHandler.cs" company="Softo">
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
    /// <summary>DeleteManagerCommandHandler</summary>
    public class DeleteManagerCommandHandler : BaseManagerCommandHandler, IRequestHandler<DeleteManager, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly ICollaboratorTypeRepository collaboratorTypeRepo;

        /// <summary>Initializes a new instance of the <see cref="DeleteCollaboratorCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="collaboratorTypeRepository">The collaborator type repository.</param>
        public DeleteManagerCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ICollaboratorRepository collaboratorRepository,
            IEditionRepository editionRepository,
            ICollaboratorTypeRepository collaboratorTypeRepository)
            : base(eventBus, uow, collaboratorRepository)
        {
            this.editionRepo = editionRepository;
            this.collaboratorTypeRepo = collaboratorTypeRepository;
        }

        /// <summary>Handles the specified delete collaborator.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(DeleteManager cmd, CancellationToken cancellationToken)
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

            // Before update values
            var beforeImageUploadDate = collaborator.ImageUploadDate;

            collaborator.Delete(
                edition,
                await this.collaboratorTypeRepo.FindByNameAsync(cmd.CollaboratorTypeName),
                cmd.UserId);
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