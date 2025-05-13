// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-18-2019
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 10-03-2024
// ***********************************************************************
// <copyright file="UpdateCollaboratorApiConfigurationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateCollaboratorApiConfigurationCommandHandler</summary>
    public class UpdateCollaboratorApiConfigurationCommandHandler : BaseCollaboratorCommandHandler, IRequestHandler<UpdateCollaboratorApiConfiguration, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly ICollaboratorTypeRepository collaboratorTypeRepo;
        private readonly ILanguageRepository languageRepo;

        public UpdateCollaboratorApiConfigurationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ICollaboratorRepository collaboratorRepository,
            IEditionRepository editionRepository,
            ICollaboratorTypeRepository collaboratorTypeRepository,
            ILanguageRepository languageRepository)
            : base(eventBus, uow, collaboratorRepository)
        {
            this.editionRepo = editionRepository;
            this.collaboratorTypeRepo = collaboratorTypeRepository;
            this.languageRepo = languageRepository;
        }

        /// <summary>Handles the specified update collaborator API configuration.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateCollaboratorApiConfiguration cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var collaborator = await this.GetCollaboratorByUid(cmd.CollaboratorUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            if (!collaborator.IsAbleToPublishToApi(cmd.EditionId.Value))
            {
                this.ValidationResult.Add(
                    new ValidationError(
                        Messages.PendingFieldsToPublish,
                        new string[] { "ToastrError" }
                    )
                );
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            var edition = await this.editionRepo.GetAsync(cmd.EditionUid ?? Guid.Empty);
            var collaboratorType = await this.collaboratorTypeRepo.FindByNameAsync(cmd.CollaboratorTypeName);

            collaborator.UpdateApiConfiguration(
                edition,
                collaboratorType,
                cmd.IsApiDisplayEnabled,
                cmd.ApiHighlightPosition,
                cmd.UserId);
            if (!collaborator.IsValid())
            {
                this.AppValidationResult.Add(collaborator.ValidationResult);
                return this.AppValidationResult;
            }

            this.CollaboratorRepo.Update(collaborator);

            #region Disable same highlight position of other collaborators

            if (cmd.IsApiDisplayEnabled && cmd.ApiHighlightPosition.HasValue)
            {
                var sameHighlightPositionCollaborators = await this.CollaboratorRepo.FindAllByHightlightPosition(
                    cmd.EditionId ?? 0,
                    collaboratorType?.Uid ?? Guid.Empty,
                    cmd.ApiHighlightPosition.Value,
                    collaborator.Uid);
                if (sameHighlightPositionCollaborators?.Any() == true)
                {
                    foreach (var sameHighlightPositionCollaborator in sameHighlightPositionCollaborators)
                    {
                        sameHighlightPositionCollaborator.DeleteApiHighlightPosition(edition, collaboratorType, cmd.UserId);
                        this.CollaboratorRepo.Update(sameHighlightPositionCollaborator);
                    }
                }
            }

            #endregion

            this.Uow.SaveChanges();
            this.AppValidationResult.Data = collaborator;

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }

    }
}